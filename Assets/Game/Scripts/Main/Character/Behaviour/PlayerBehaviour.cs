#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace Main.Character.Behaviour
{
    public class PlayerBehaviour : CharacterBehaviour
    {
    #region Private Variables

        private bool attack;
        private bool isDead;

        private Character currentAttackingEnemy;


        [SerializeField]
        private float AttackSpeed = 0.5f;

        [SerializeField]
        private int damage = 10;


        [BoxGroup("Animation")]
        [SerializeField]
        private List<string> attackAnimationNames = new List<string>();

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_DIE = "Death";

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_IDLE = "Idle";

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_MOVE = "Move";

    #endregion

    #region Unity events

        // Start is called before the first frame update
        private void Start()
        {
            Move(true);
        }

    #endregion

    #region Public Methods

        public override void MakeCharacterDie()
        {
            Move(false);
            attack = false;
            isDead = true;
            PlayAnimation(ANIMATION_DIE);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void TriggerEnter(Character target)
        {
            currentAttackingEnemy = target;
            Move(false);
            attack = true;
            var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
            Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                      .TakeWhile(l => attack)
                      .Subscribe(PlayAttackAnimation).AddTo(gameObject);
        }

        public override void TriggerExit(Character target)
        {
            if (isDead) return;
            attack = false;
            Move(true);
        }

    #endregion

    #region Protected Methods

        protected override void Move(bool move)
        {
            base.Move(move);
            var animationName = move ? ANIMATION_MOVE : ANIMATION_IDLE;
            PlayAnimation(animationName);
        }

    #endregion

    #region Private Methods

        private string GetAttackAnimationName()
        {
            var randomValue = Random.Range(0 , 3);
            return attackAnimationNames[randomValue];
        }

        private void PlayAttackAnimation(long obj)
        {
            PlayAnimation(GetAttackAnimationName());
            currentAttackingEnemy.TakeDamage(damage);
        }

    #endregion
    }
}