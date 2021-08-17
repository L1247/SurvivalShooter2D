#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class CharacterBehaviour : MonoBehaviour
    {
    #region Protected Variables

        protected bool isDead;

        protected Character character;

    #endregion

    #region Private Variables

        private BoxCollider2D boxCollider2D;

    #endregion

    #region Unity events

        protected virtual void Awake()
        {
            character     = GetComponent<Character>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        protected virtual void Start()
        {
            Move(true);
        }

    #endregion

    #region Public Methods

        [Button]
        public virtual void Die()
        {
            isDead = true;
            Move(false);
            Attack(false);
            boxCollider2D.enabled = false;
        }

        public virtual void TriggerEnter(Character target)
        {
            if (isDead) return;
            Move(false);
            Attack(true , target);
        }

        public virtual void TriggerExit(Character target)
        {
            if (isDead) return;
            Attack(false);
            Move(true);
        }

    #endregion

    #region Protected Methods

        protected virtual void Attack(bool enable , Character target = null)
        {
            character.Attack(enable , target);
        }

        protected virtual void Move(bool move)
        {
            character.Move(move);
        }

        protected virtual void PlayAnimation(string animationName)
        {
            character.PlayAnimation(animationName);
        }

    #endregion
    }
}