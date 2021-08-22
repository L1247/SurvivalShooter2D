#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class PlayerCharacterBehaviour : CharacterBehaviour
    {
    #region Private Variables

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_DIE = "Death";

    #endregion

    #region Public Methods

        public override void Die()
        {
            base.Die();
            PlayAnimation(ANIMATION_DIE);
        }

    #endregion
    }
}