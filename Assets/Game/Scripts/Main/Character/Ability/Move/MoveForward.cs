#region

#endregion

#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class MoveForward : MoveBase
    {
    #region Private Variables

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_IDLE = "Idle";

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_MOVE = "Move";

    #endregion

    #region Public Methods

        public override void Move()
        {
            var currentFacingVector = character.GetCurrentFacingVector();
            trans.Translate(currentFacingVector * moveSpeed * Time.deltaTime);
        }

        public override void SetEnable(bool enable)
        {
            base.SetEnable(enable);
            var animationName = move ? ANIMATION_MOVE : ANIMATION_IDLE;
            character.PlayAnimation(animationName);
        }

    #endregion
    }
}