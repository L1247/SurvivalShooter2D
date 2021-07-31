using Main.Event;
using Main.System;
using Zenject;

namespace Main.Application
{
    public class BattleBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Event
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InputHorizontal>();
            // System
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}