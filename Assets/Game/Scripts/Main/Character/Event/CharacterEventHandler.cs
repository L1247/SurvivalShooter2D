#region

using Main.Character;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Event
{
    public class CharacterEventHandler : IInitializable
    {
    #region Private Variables

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
            signalBus.Subscribe<CharacterHurt>(OnCharacterHurt);
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
            var characterId        = obj.CharacterId;
            var characterBehaviour = GetCharacterBehaviour(characterId);
            characterBehaviour.MakeCharacterDie();
        }

        private void OnCharacterHurt(CharacterHurt hurt)
        {
            var hurtCharacterId = hurt.CharacterId;
            var hurtCharacter   = characterRepository.FindById(hurtCharacterId);
            Debug.Log($"OnCharacterHurt {hurtCharacter} , damage {hurt.Damage}");
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