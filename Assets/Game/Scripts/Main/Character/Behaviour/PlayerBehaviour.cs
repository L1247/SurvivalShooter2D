#region

using System;
using System.Collections.Generic;
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

        private Character character;
        private Character currentAttackingEnemy;

        private List<string> attackAnimations;

        private readonly string ANIMATION_DIE = "Death";

        private readonly string ANIMATION_IDLE = "Idle";
        private readonly string ANIMATION_MOVE = "Move";

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float AttackSpeed = 0.5f;

        [SerializeField]
        private int damage = 10;

    #endregion

    #region Unity events

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            attackAnimations = new List<string>() { "Attack1" , "Attack2" , "Attack3" };
            SetMove(true);
            animator.Play(ANIMATION_MOVE);
        }

    #endregion

    #region Public Methods

        public override void MakeCharacterDie()
        {
            SetMove(false);
            attack = false;
            isDead = true;
            animator.Play(ANIMATION_DIE);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void TriggerEnter(Character target)
        {
            currentAttackingEnemy = target;
            SetMove(false);
            attack = true;
            animator.Play(ANIMATION_IDLE);
            var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);
            Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                      .TakeWhile(l => attack)
                      .Subscribe(PlayAttackAnimation).AddTo(gameObject);
        }

        public override void TriggerExit(Character target)
        {
            if (isDead) return;
            attack = false;
            SetMove(true);
            animator.Play(ANIMATION_MOVE);
        }

    #endregion

    #region Private Methods

        private string GetAttackAnimationName()
        {
            var randomValue = Random.Range(0 , 3);
            return attackAnimations[randomValue];
        }

        private void PlayAttackAnimation(long obj)
        {
            animator.Play(GetAttackAnimationName());
            currentAttackingEnemy.TakeDamage(damage);
        }

        private void SetMove(bool move)
        {
            character.SetMove(move);
        }

    #endregion
    }
}