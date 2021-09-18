#region

using Character.Component;
using Main.Character.Ability.Move;
using Main.Character.Behaviour;
using Main.Character.Component;

#endregion

namespace Main.Character.Data
{
    public interface IActorData
    {
    #region Public Variables

        public CharacterBehaviour CharacterBehaviour { get; }
        public MoveBase           MoveAbility        { get; }

        public CharacterFacing.Setting SettingFacing { get; }
        public CharacterHealth.Setting SettingHealth { get; }

    #endregion
    }
}