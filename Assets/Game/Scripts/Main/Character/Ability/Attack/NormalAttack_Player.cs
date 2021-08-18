#region

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Attack
{
    public class NormalAttack_Player : AttackBase
    {
    #region Private Variables

        [BoxGroup("Animation")]
        [SerializeField]
        private List<string> attackAnimationNames = new List<string>();

    #endregion

    #region Public Methods

        public override void Attack()
        {
            base.Attack();
            if (isAttacking == false) return;
            character.PlayAnimation(GetAttackAnimationName());
        }

    #endregion

    #region Private Methods

        private string GetAttackAnimationName()
        {
            var attackAnimationCount = attackAnimationNames.Count;
            var randomValue          = Random.Range(0 , attackAnimationCount);
            return attackAnimationNames[randomValue];
        }

    #endregion
    }
}