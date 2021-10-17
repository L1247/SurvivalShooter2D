#region

using System;
using Main.Character.Data;
using Main.Character.Presenter;
using Main.Character.Repository;
using Main.Event;
using Main.System;
using Main.System.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Application
{
    public class BattleBinder : MonoInstaller
    {
    #region Private Variables

        [Inject]
        private Settings settings;

    #endregion

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
            Container.BindFactory<IActorData , Character.Character , Character.Character.Factory>()
                     .FromComponentInNewPrefab(settings.CharacterPrefab);
        }

    #endregion

    #region Nested Types

        [Serializable]
        public class Settings
        {
        #region Public Variables

            [Required]
            public GameObject CharacterPrefab;

        #endregion
        }

    #endregion
    }
}