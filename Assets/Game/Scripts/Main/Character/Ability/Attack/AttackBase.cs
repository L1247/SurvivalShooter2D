#region

using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Attack
{
    public class AttackBase : MonoBehaviour , IAttack
    {
    #region Protected Variables

        protected bool      isAttacking;
        protected Character attackingCharacter;

        [SerializeField]
        protected float AttackSpeed = 0.5f;

        [SerializeField]
        protected int damage = 10;

    #endregion

    #region Unity events

        protected virtual void Awake()
        {
            var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
            Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                      .Where(l => isAttacking)
                      .Subscribe(_ => AttackPlayer()).AddTo(gameObject);
        }

    #endregion

    #region Public Methods

        [Button]
        public virtual void Attack()
        {
            AttackPlayer();
        }

        public void SetEnable(bool enable)
        {
            isAttacking = enable;
        }

        public void SetTarget(Character target)
        {
            attackingCharacter = target;
        }

    #endregion

    #region Private Methods

        private void AttackPlayer()
        {
            attackingCharacter?.TakeDamage(damage);
        }

    #endregion
    }
}