#region

using System.Linq;
using Main.Character.Behaviour;
using Main.Character.Presenter;
using Main.Character.Repository;
using Zenject;

#endregion

namespace Main.Event
{
    public class CharacterEventHandler : IInitializable
    {
    #region Private Variables

        [Inject]
        private CharacterPresenter characterPresenter;

        [Inject]
        private CharacterRepository characterRepository;

        [Inject]
        private SignalBus signalBus;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            signalBus.Subscribe<TriggerEnter>(OnTriggerEnter);
            signalBus.Subscribe<TriggerExit>(OnTriggerExit);
            signalBus.Subscribe<CharacterDead>(OnCharacterDead);
            signalBus.Subscribe<CharacterHealthModified>(OnCharacterModified);
        }

    #endregion

    #region Private Methods

        private CharacterBehaviour GetCharacterBehaviour(string characterId)
        {
            var character      = characterRepository.FindById(characterId);
            var enemyBehaviour = character.characterBehaviour;
            return enemyBehaviour;
        }

        private void OnCharacterDead(CharacterDead obj)
        {
            var deadCharacterId        = obj.CharacterId;
            var deadCharacter          = characterRepository.FindById(deadCharacterId);
            var deadCharacterBehaviour = deadCharacter.characterBehaviour;
            deadCharacterBehaviour.Die();
            characterRepository.Remove(deadCharacterId);
            var allCharacter = characterRepository.GetAllCharacter();
            var sameTargetCharacters = allCharacter
                                       .Where(character => character.AttackAbility.AttackingCharacter == deadCharacter)
                                       .ToList();
            sameTargetCharacters.ForEach(character => character.characterBehaviour.ChooseANewTarget());
        }

        private void OnCharacterModified(CharacterHealthModified healthModified)
        {
            var characterId = healthModified.CharacterId;
            var amount      = healthModified.Damage;
            characterPresenter.ShowPopupText(characterId , amount);
        }

        private void OnTriggerEnter(TriggerEnter obj)
        {
            var enemyBehaviour = GetCharacterBehaviour(obj.CharacterId);
            enemyBehaviour.TriggerEnter(obj.Character);
        }

        private void OnTriggerExit(TriggerExit obj)
        {
            var enemyBehaviour = GetCharacterBehaviour(obj.CharacterId);
            enemyBehaviour.TriggerExit(obj.Character);
        }

    #endregion
    }
}