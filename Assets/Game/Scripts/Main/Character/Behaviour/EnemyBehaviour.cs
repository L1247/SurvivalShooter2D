#region

#endregion

namespace Main.Character.Behaviour
{
    public class EnemyBehaviour : CharacterBehaviour
    {
    #region Public Methods

        public override void Die()
        {
            base.Die();
            gameObject.SetActive(false);
        }

    #endregion
    }
}