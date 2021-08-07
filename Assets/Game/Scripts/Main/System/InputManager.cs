#region

using Main.Event;
using UnityEngine;
using Zenject;

#endregion

namespace Main.System
{
    public class InputManager : IInitializable , ITickable
    {
    #region Private Variables

        private bool  initialized;
        private float lastHorizontalValue = -999;

        [Inject]
        private SignalBus signalBus;

    #endregion

    #region Public Methods

        public void Initialize() { }

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
            var horizontalValue = Input.GetAxisRaw("Move Horizontal");
            if (lastHorizontalValue != horizontalValue)
                signalBus.Fire(new InputHorizontal((int)horizontalValue));
            lastHorizontalValue = horizontalValue;
        }

    #endregion
    }
}