#region

using Character.Component;
using Main.Character.Component;

#endregion

namespace Main.Character.Data
{
    public interface IActorData
    {
    #region Public Variables

        public CharacterFacing.Setting SettingFacing { get; }
        public CharacterHealth.Setting SettingHealth { get; }

    #endregion
    }
}