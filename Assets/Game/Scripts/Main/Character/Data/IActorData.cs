#region

using Character.Component;

#endregion

namespace Main.Character.Data
{
    public interface IActorData
    {
    #region Public Variables

        bool                           DefaultFacingRight { get; }
        bool                           DefaultSpriteRight { get; }
        public CharacterHealth.Setting SettingHealth      { get; }
        public string                  ActorDataId        { get; }

    #endregion
    }
}