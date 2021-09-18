namespace Main.Character.Ability
{
    public interface IAbility
    {
    #region Unity events

        void Start();
        void Update();

    #endregion

    #region Public Methods

        void SetEnable(bool enable);

    #endregion
    }
}