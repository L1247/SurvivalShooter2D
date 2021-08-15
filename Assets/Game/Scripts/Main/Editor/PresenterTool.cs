#region

using Main.Character.Presenter;
using Main.Character.Repository;
using UnityEditor;
using UnityEngine;
using Zenject;

#endregion

namespace Game.Scripts.Main.Editor
{
    public class PresenterTool : EditorWindow
    {
    #region Private Variables

        private static CharacterPresenter  characterPresenter;
        private static CharacterRepository characterRepository;

    #endregion

    #region Private Methods

        private void OnGUI()
        {
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

        [MenuItem("Tools/PresenterTool _F10")]
        private static void ShowWindow()
        {
            var sceneContext = FindObjectOfType<SceneContext>();
            var container    = sceneContext.Container;
            characterPresenter  = container.Resolve<CharacterPresenter>();
            characterRepository = container.Resolve<CharacterRepository>();
            var window = GetWindow<PresenterTool>();
            window.titleContent = new GUIContent("PresenterTool");
            window.Show();
        }

    #endregion
    }
}