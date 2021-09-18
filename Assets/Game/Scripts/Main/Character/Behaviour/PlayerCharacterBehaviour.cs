#region

using System;

#endregion

namespace Main.Character.Behaviour
{
    [Serializable]
    public class PlayerCharacterBehaviour : CharacterBehaviour
    {
    #region Private Variables

        private string DIE = "Death";

    #endregion

    #region Constructor

        public PlayerCharacterBehaviour(Character character) : base(character) { }

    #endregion

    #region Public Methods

        public override void Die()
        {
            base.Die();
            PlayAnimation(DIE);
        }

    #endregion
    }
}