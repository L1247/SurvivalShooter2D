namespace Main.Character.Ability.Attack
{
    public interface IAttack : IAbility
    {
    #region Public Variables

        public Character AttackingCharacter { get; }

    #endregion

    #region Public Methods

        public void Attack();
        public void SetTarget(Character target);

    #endregion
    }
}