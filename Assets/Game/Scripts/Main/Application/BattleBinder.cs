#region

using Main.Character;
using Main.Event;
using Zenject;

#endregion

namespace Main.Application
{
    public class BattleBinder : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            // Event
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InputHorizontal>();
            Container.DeclareSignal<TriggerEnter>();
            Container.DeclareSignal<TriggerExit>();
            // System
            // Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.Bind<CharacterRepository>().AsSingle();
        }

    #endregion
    }
}