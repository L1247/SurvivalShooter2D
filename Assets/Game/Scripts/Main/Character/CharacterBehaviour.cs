#region

using UnityEngine;

#endregion

namespace Main.Character
{
    public class CharacterBehaviour : MonoBehaviour
    {
    #region Public Methods

        public virtual void OntriggerEnter(Character target) { }
        public virtual void OntriggerExit(Character  target) { }

    #endregion
    }
}