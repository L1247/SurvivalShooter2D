#region

using AutoBot.Utilities;
using Character.Component;
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

        public CharacterHealth.Setting SettingHealth => settingHealth;

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
        [BoxGroup("血量資料")]
        [HideLabel]
        [Required]
        private CharacterHealth.Setting settingHealth;

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