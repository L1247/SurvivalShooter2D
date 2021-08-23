#region

using Character.Component;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

#endregion

namespace CustomEditor
{
    [UnityEditor.CustomEditor(typeof(Main.Character.Character))]
    public class CharacterEditor : OdinEditor
    {
    #region Private Variables

        private static GUIStyle textureStyle;

        private CharacterHealth characterHealth;

    #endregion

    #region Public Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(10);
            var backgroundColor = GUI.backgroundColor;
            var color           = new Color(0.47f , 0.59f , 0.36f);
            GUI.backgroundColor = color;
            var currentHealth                          = 0;
            if (characterHealth != null) currentHealth = characterHealth.CurrentHealth;
            var text                                   = "\n";
            var healthText                             = $"\nCurrent Health: {currentHealth}";
            text += $"{healthText}";
            text += "\n";
            text += "\n";
            GUILayout.Box(text , textureStyle);
            GUI.backgroundColor = backgroundColor;
        }

    #endregion

    #region Protected Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            var character = target as Main.Character.Character;
            characterHealth = character.CharacterHealth;
            textureStyle = new GUIStyle
            {
                normal = new GUIStyleState
                {
                    background = Texture2D.whiteTexture ,
                    textColor  = Color.white
                }
            };
            textureStyle.alignment = TextAnchor.MiddleCenter;
        }

    #endregion
    }
}