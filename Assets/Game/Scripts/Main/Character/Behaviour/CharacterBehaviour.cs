#region

using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class CharacterBehaviour : MonoBehaviour
    {
    #region Public Methods

        public virtual void MakeCharacterDie() { }

        public virtual void TriggerEnter(Character target) { }
        public virtual void TriggerExit(Character  target) { }

    #endregion
    }
}