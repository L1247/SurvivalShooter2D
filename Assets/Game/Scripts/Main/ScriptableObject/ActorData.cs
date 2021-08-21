#region

using AutoBot.Utilities;
using Main.Character.Data;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorData" , menuName = "Survival2D/ActorData" , order = 0)]
    public class ActorData : ScriptableObject , IActorData
    {
    #region Public Variables

        public bool DefaultFacingRight => defaultFacingRight;

        public bool DefaultSpriteRight => defaultSpriteRight;

        public int StartingHealth => startingHealth;

        public string ActorDataId => actorDataId;

        [LabelText("角色Prefab")]
        [Required]
        [PropertyOrder(2)]
        public GameObject actorPrefab;

    #endregion

    #region Private Variables

        [SerializeField]
        [LabelText("預設產生時面右")]
        [PropertyOrder]
        private bool defaultFacingRight;

        [LabelText("預設圖片面右")]
        [SerializeField]
        [PropertyOrder]
        private bool defaultSpriteRight;

        [SerializeField]
        [LabelText("初始生命")]
        [ValidateInput("@startingHealth>0" , "can't be zero , or small than zero")]
        [PropertyOrder]
        private int startingHealth = 100;

        [SerializeField]
        [PropertyOrder(-1)]
        private Sprite preview;

        [SerializeField]
        [LabelText("角色ID")]
        [Required]
        [PropertyOrder(1)]
        private string actorDataId;

    #endregion

    #region Private Methods

        [OnInspectorGUI]
        [PropertyOrder(-10)]
        private void ShowPreview()
        {
            if (preview == null) return;
            var assetPath = CustomEditorUtility.GetAssetPath(preview);
            // var sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>().ToArray();
            var sprite  = CustomEditorUtility.LoadAssetAtPath<Sprite>(assetPath);
            var texture = sprite.texture;
            GUILayout.Label(texture);
        }

    #endregion
    }
}