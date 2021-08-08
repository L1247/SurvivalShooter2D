#region

using Main.Character;
using Main.Event;
using Main.System;
using Main.System.Input;
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
            Container.DeclareSignal<CharacterDead>();
            Container.DeclareSignal<CharacterHurt>();
            Container.BindInterfacesAndSelfTo<CharacterEventHandler>().AsSingle();
            // System
            Container.Bind<CharacterRepository>().AsSingle();
            Container.Bind<PopupTextSpawner>().AsSingle();
        }

    #endregion
    }
}