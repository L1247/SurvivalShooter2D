#region

using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class CharacterBehaviour : MonoBehaviour
    {
    #region Private Variables

        private Character character;

    #endregion

    #region Unity events

        protected virtual void Awake()
        {
            character = GetComponent<Character>();
        }

    #endregion

    #region Public Methods

        public virtual void MakeCharacterDie() { }

        public virtual void TriggerEnter(Character target) { }
        public virtual void TriggerExit(Character  target) { }

    #endregion

    #region Protected Methods

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