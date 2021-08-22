#region

using Main.Character.Presenter;
using Main.Character.Repository;
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
            Container.DeclareSignal<CharacterHealthModified>();
            Container.BindInterfacesAndSelfTo<CharacterEventHandler>().AsSingle();
            // System
            Container.Bind<CharacterRepository>().AsSingle();
            Container.Bind<PopupTextSpawner>().AsSingle();
            Container.Bind<CharacterSpawner>().AsSingle();
            // Presenter
            Container.Bind<CharacterPresenter>().AsSingle();
            // Repository
            Container.Bind<IDataRepository>().To<DataRepository>().AsSingle();
        }

    #endregion
    }
}