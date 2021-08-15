#region

#endregion

namespace Main.Character.Behaviour
{
    public class EnemyBehaviour : CharacterBehaviour
    {
    #region Unity events

        protected override void Awake()
        {
            base.Awake();
            Move(true);
        }

    #endregion

    #region Public Methods

        public override void TriggerEnter(Character target)
        {
            Move(false);
            Attack(true , target);
        }

        public override void TriggerExit(Character target)
        {
            Move(true);
            Attack(false , target);
        }

    #endregion
    }
}