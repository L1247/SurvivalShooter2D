#region

using System;
using UniRx;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class EnemyBehaviour : CharacterBehaviour
    {
    #region Private Variables

        private bool      isAttacking;
        private Character attackingCharacter;

        [SerializeField]
        private float AttackSpeed = 0.5f;

        [SerializeField]
        private int damage = 10;

    #endregion

    #region Unity events

        protected override void Awake()
        {
            base.Awake();
            Move(true);
            var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
            Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                      .Where(l => isAttacking)
                      .Subscribe(AttackPlayer).AddTo(gameObject);
        }

    #endregion

    #region Public Methods

        public override void TriggerEnter(Character target)
        {
            isAttacking = true;
            Move(false);
            attackingCharacter = target;
        }

        public override void TriggerExit(Character target)
        {
            isAttacking = false;
            Move(true);
            if (attackingCharacter == target) attackingCharacter = null;
        }

    #endregion

    #region Private Methods

        private void AttackPlayer(long obj)
        {
            attackingCharacter.TakeDamage(damage);
        }

    #endregion
    }
}