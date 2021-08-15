#region

using Main.Character.Presenter;
using Main.Character.Repository;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "TestSO" , menuName = "TEST_SO" , order = 0)]
    public class TestSO : ScriptableObject
    {
    #region Private Variables

        private bool               init;
        private CharacterPresenter characterPresenter;

        private CharacterRepository characterRepository;

    #endregion

    #region Public Methods

        public void Init()
        {
            if (init) return;
            init = true;
            var sceneContext = FindObjectOfType<SceneContext>();
            var container    = sceneContext.Container;
            characterPresenter  = container.Resolve<CharacterPresenter>();
            characterRepository = container.Resolve<CharacterRepository>();
        }

    #endregion

    #region Private Methods

        private void OnEnable()
        {
            Debug.Log("TEST_SO OnEnable");
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
            var characterIdList = characterRepository.FindAllId();
            GUILayout.BeginVertical();
            GUILayout.Label("Character Id List");
            foreach (var id in characterIdList)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Id: {id} ");
                if (GUILayout.Button("Show PopUpText"))
                    characterPresenter.ShowPopupText(id , -Random.Range(1 , 101));
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

    #endregion
    }
}