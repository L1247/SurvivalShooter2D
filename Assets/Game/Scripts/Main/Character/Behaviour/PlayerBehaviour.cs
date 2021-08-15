#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Behaviour
{
    public class PlayerBehaviour : CharacterBehaviour
    {
    #region Private Variables

        private bool isDead;

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_DIE = "Death";

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_IDLE = "Idle";

        [BoxGroup("Animation")]
        [SerializeField]
        private string ANIMATION_MOVE = "Move";

    #endregion

    #region Unity events

        // Start is called before the first frame update
        private void Start()
        {
            Move(true);
        }

    #endregion

    #region Public Methods

        public override void MakeCharacterDie()
        {
            isDead = true;
            Move(false);
            Attack(false);
            PlayAnimation(ANIMATION_DIE);
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void TriggerEnter(Character target)
        {
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

    #region Protected Methods

        protected override void Move(bool move)
        {
            base.Move(move);
            var animationName = move ? ANIMATION_MOVE : ANIMATION_IDLE;
            PlayAnimation(animationName);
        }

    #endregion
    }
}