#region

using System.Collections;
using System.Linq;
using AutoBot.Utilities;
using Main.Character.Presenter;
using Main.Character.Repository;
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
        private CharacterPresenter  characterPresenter;
        private CharacterRepository characterRepository;

        [InlineButton("CreateActor")]
        [SerializeField]
        [ValueDropdown("GetAllActorDataIds")]
        private string actorDataId;

    #endregion

    #region Private Methods

        private void CreateActor()
        {
            characterPresenter.SpawnCharacter(actorDataId);
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
            characterPresenter  = container.Resolve<CharacterPresenter>();
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