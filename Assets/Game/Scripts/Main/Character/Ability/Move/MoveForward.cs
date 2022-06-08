#region

#endregion

#region

using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

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
        private string             ANIMATION_MOVE;
        private string             ANIMATION_IDLE;

    #endregion

    #region Constructor

        public MoveForward() { }
        public MoveForward(Character character) : base(character) { }

    #endregion

    #region Public Methods

        public override MoveSetting GetSetting()
        {
            return new MoveForwardSetting();
        }

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

        public override void SetSetting(MoveSetting setting)
        {
            base.SetSetting(setting);
            moveSetting = setting as MoveForwardSetting;
            Assert.IsNotNull(moveSetting , "moveSetting == null");
            ANIMATION_IDLE = moveSetting.ANIMATION_IDLE;
            ANIMATION_MOVE = moveSetting.ANIMATION_MOVE;
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