#region

using EditorUtilities;
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

        [SerializeField]
        [BoxGroup("CreateActor")]
        private ActorName actorName;

    #endregion

    #region Private Methods

        [Button]
        [BoxGroup("CreateActor")]
        private void CreateActor()
        {
            characterSpawner.Spawn(actorName.Id);
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

        private void OnGUI() { }


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