#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Component
{
    public class CharacterFacing
    {
    #region Public Variables

        public Vector3 CurrentDirectionVector { get; private set; }

    #endregion

    #region Private Variables

        private readonly Setting        setting;
        private readonly SpriteRenderer spriteRenderer;

    #endregion

    #region Constructor

        public CharacterFacing(SpriteRenderer spriteRenderer , Setting setting)
        {
            this.spriteRenderer = spriteRenderer;
            this.setting        = setting;
            SetFacing(setting.DefaultFacingRight);
        }

    #endregion

    #region Public Methods

        [Button]
        public void SetFacing(bool faceRight)
        {
            CurrentDirectionVector = faceRight ? Vector3.right : Vector3.left;
            HandleCharacterFace(faceRight);
        }

    #endregion

    #region Protected Methods

        protected virtual void HandleCharacterFace(bool faceRight)
        {
            var flip = setting.DefaultSpriteRight != faceRight;
            spriteRenderer.flipX = flip;
        }

    #endregion

    #region Nested Types

        [Serializable]
        public class Setting
        {
        #region Public Variables

            public bool DefaultFacingRight => defaultFacingRight;

            public bool DefaultSpriteRight => defaultSpriteRight;

        #endregion

        #region Private Variables

            [SerializeField]
            [LabelText("預設面向方向為右")]
            private bool defaultFacingRight;

            [SerializeField]
            [LabelText("預設Sprite為右")]
            private bool defaultSpriteRight;

        #endregion
        }

    #endregion
    }
}