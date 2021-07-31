using Main.Event;
using Zenject;

namespace Main.System
{
    public class InputManager : IInitializable , ITickable
    {
    #region Private Variables

        private bool initialized;

        private          float  lastHorzonTalValue = -999;
        private readonly int    playerId           = 0;

        [Inject]
        private SignalBus signalBus;

    #endregion

    #region Public Methods

        public void Initialize()
        {
        }

        public void Tick()
        {
            GetInputValue();
        }

    #endregion

    #region Private Methods

        private void GetInputValue()
        {
            // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
            // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.
            // get input by name or action id
            // -1 : left , 0 : no press , 1 : right
            var horizontalValue   = UnityEngine.Input.GetAxisRaw("Move Horizontal");
            if (lastHorzonTalValue != horizontalValue)
                signalBus.Fire(new InputHorizontal((int)horizontalValue));
            lastHorzonTalValue = horizontalValue;
        }

    #endregion
    }
}