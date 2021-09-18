#region

#endregion

#region

using System;

#endregion

namespace Main.Character.Behaviour
{
    [Serializable]
    public class EnemyBehaviour : CharacterBehaviour
    {
    #region Constructor

        public EnemyBehaviour(Character character) : base(character) { }

    #endregion

    #region Public Methods

        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }

    #endregion
    }
}