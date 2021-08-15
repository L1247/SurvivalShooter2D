#region

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

        private bool                init;
        private CharacterPresenter  characterPresenter;
        private CharacterRepository characterRepository;

        [InlineButton("CreateActor")]
        [SerializeField]
        private string ActorDataId;

    #endregion

    #region Private Methods

        private void CreateActor()
        {
            Debug.Log($"ActorDataId {ActorDataId}");
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