namespace Main.Character.Data
{
    public interface IActorData
    {
    #region Public Variables

        bool DefaultFacingRight { get; }
        bool DefaultSpriteRight { get; }

        public int StartingHealth { get; }

        public string ActorDataId { get; }

    #endregion
    }
}