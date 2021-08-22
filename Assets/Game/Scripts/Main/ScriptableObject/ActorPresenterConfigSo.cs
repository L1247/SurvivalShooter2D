#region

using System.Collections;
using System.Linq;
using AutoBot.Utilities;
using Main.Character.Repository;
using Main.System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorPresenterConfigSo" , menuName = "ActorPresenterConfigSo" , order = 0)]
    public class ActorPresenterConfigSo : ScriptableObject
    {
    #region Private Variables

        private ActorDataOverview actorDataOverview;

        private bool                init;
        private CharacterRepository characterRepository;

        private CharacterSpawner characterSpawner;

        [InlineButton("CreateActor")]
        [SerializeField]
        [ValueDropdown("GetAllActorDataIds")]
        private string actorDataId;

    #endregion

    #region Private Methods

        private void CreateActor()
        {
            characterSpawner.Spawn(actorDataId);
        }

        private IEnumerable GetAllActorDataIds()
        {
            if (actorDataOverview == null)
                return null;

            return actorDataOverview.FindAll().Select(data => data.ActorDataId)
                                    .Select(id => new ValueDropdownItem(id , id));
        }

        private void Init()
        {
            if (init) return;
            init = true;
            var sceneContext = FindObjectOfType<SceneContext>();
            var container    = sceneContext.Container;
            characterSpawner    = container.Resolve<CharacterSpawner>();
            characterRepository = container.Resolve<CharacterRepository>();
        }

        private void OnGUI()
        {
            var characterIdList = characterRepository.FindAllId();
        }


        [OnInspectorGUI]
        private void Test()
        {
            if (actorDataOverview == null)
                actorDataOverview = CustomEditorUtility.GetScriptableObject<ActorDataOverview>();

            if (UnityEngine.Application.isPlaying == false)
            {
                init = false;
                return;
            }

            Init();
            OnGUI();
        }

    #endregion
    }
}