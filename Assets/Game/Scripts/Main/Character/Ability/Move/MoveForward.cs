#region

#endregion

#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    [Serializable]
    public class MoveForward : MoveBase
    {
    #region Public Variables

        public MoveForwardSetting MoveSetting;

    #endregion

    #region Private Variables

        private MoveForwardSetting moveSetting;

    #endregion

    #region Constructor

        public MoveForward(Character character) : base(character) { }

    #endregion

    #region Public Methods

        public override void Move()
        {
            var currentFacingVector = character.GetCurrentFacingVector();
            trans.Translate(currentFacingVector * moveSetting.moveSpeed * Time.deltaTime);
        }

        public override void SetEnable(bool enable)
        {
            base.SetEnable(enable);
            var animationName = move ? moveSetting.ANIMATION_MOVE : moveSetting.ANIMATION_IDLE;
            character.PlayAnimation(animationName);
        }

        public override void SetSetting(MoveSetting moveSetting)
        {
            this.moveSetting = moveSetting as MoveForwardSetting;
        }

        public void SetSetting(MoveForwardSetting setting)
        {
            moveSetting = setting;
        }

    #endregion

    #region Nested Types

        [Serializable]
        public class MoveForwardSetting : MoveSetting
        {
        #region Public Variables

            [BoxGroup("Animation")]
            public string ANIMATION_IDLE = "Idle";

            [BoxGroup("Animation")]
            public string ANIMATION_MOVE = "Move";

        #endregion
        }

    #endregion
    }
}