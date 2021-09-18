#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    [Serializable]
    public abstract class CharacterBehaviour
    {
    #region Protected Variables

        protected bool       isDead;
        protected GameObject gameObject;

    #endregion

    #region Private Variables

        private BoxCollider2D boxCollider2D;
        private Character     character;

        private List<Character> triggerTargets = new List<Character>();

    #endregion

    #region Constructor

        public CharacterBehaviour(Character character)
        {
            this.character = character;
            boxCollider2D  = this.character.BoxCollider2D;
            gameObject     = this.character.gameObject;
            Move(true);
        }

    #endregion

    #region Public Methods

        public void ChooseANewTarget()
        {
            RemoveTarget(character.AttackAbility.AttackingCharacter);
            var noTargetCanChoose = triggerTargets.Count == 0;
            if (noTargetCanChoose) KeepMove();
            else Attack(true , triggerTargets[0]);
        }

        public virtual void Die()
        {
            isDead = true;
            Move(false);
            Attack(false);
            boxCollider2D.enabled = false;
        }

        public virtual void TriggerEnter(Character target)
        {
            AddTarget(target);
            if (isDead) return;
            if (IsAttacking()) return;
            StopAndAttack(target);
        }

        public virtual void TriggerExit(Character target)
        {
            RemoveTarget(target);
            if (isDead) return;
            if (character.AttackAbility.AttackingCharacter == target) ChooseANewTarget();
        }

    #endregion

    #region Protected Methods

        protected virtual void Attack(bool use , Character target = null)
        {
            character.Attack(use , target);
        }

        protected virtual void Move(bool use)
        {
            character.Move(use);
        }

        protected virtual void PlayAnimation(string animationName)
        {
            character.PlayAnimation(animationName);
        }

    #endregion

    #region Private Methods

        private void AddTarget(Character target)
        {
            if (triggerTargets.Contains(target) == false) triggerTargets.Add(target);
        }

        private bool IsAttacking()
        {
            return character.AttackAbility.AttackingCharacter != null;
        }

        private void KeepMove()
        {
            Attack(false);
            Move(true);
        }

        private void RemoveTarget(Character target)
        {
            if (triggerTargets.Contains(target)) triggerTargets.Remove(target);
        }

        private void StopAndAttack(Character target)
        {
            Move(false);
            Attack(true , target);
        }

    #endregion
    }
}