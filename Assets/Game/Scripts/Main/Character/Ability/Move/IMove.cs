namespace Main.Character.Ability.Move
{
    public interface IMove : IAbility
    {
    #region Public Methods

        public void Move();

        void SetSetting(MoveSetting setting);

    #endregion
    }
}