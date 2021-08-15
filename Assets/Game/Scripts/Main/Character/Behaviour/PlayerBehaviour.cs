#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class PlayerBehaviour : CharacterBehaviour
    {
    #region Private Variables

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_DIE = "Death";

    #endregion

    #region Unity events

        private void Start()
        {
            Move(true);
        }

    #endregion

    #region Public Methods

        public override void MakeCharacterDie()
        {
            base.MakeCharacterDie();
            Move(false);
            Attack(false);
            PlayAnimation(ANIMATION_DIE);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void TriggerEnter(Character target)
        {
            if (isDead) return;
            Move(false);
            Attack(true , target);
        }

        public override void TriggerExit(Character target)
        {
            if (isDead) return;
            Attack(false);
            Move(true);
        }

    #endregion
    }
}