#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Component
{
    public class CharacterFacing : MonoBehaviour
    {
    #region Public Variables

        public Vector3 CurrentDirectionVector { get; private set; }

    #endregion

    #region Protected Variables

        [SerializeField]
        protected bool startingFaceRight;

        [SerializeField]
        protected SpriteRenderer spriteRenderer;

    #endregion

    #region Private Variables

        [SerializeField]
        private bool defaultSpriteRight;

    #endregion

    #region Unity events

        private void Awake()
        {
            SetFacing(startingFaceRight);
        }

    #endregion

    #region Public Methods

        [Button]
        public virtual void SetFacing(bool faceRight)
        {
            CurrentDirectionVector = faceRight ? Vector3.right : Vector3.left;
            HandleCharacterFace(faceRight);
        }

    #endregion

    #region Protected Methods

        protected virtual void HandleCharacterFace(bool faceRight)
        {
            var flip = defaultSpriteRight != faceRight;
            spriteRenderer.flipX = flip;
        }

    #endregion
    }
}