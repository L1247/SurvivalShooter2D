#region

using Main.Character;
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
        }

    #endregion

    #region Private Methods

        private void OnTriggerEnter(TriggerEnter obj)
        {
            var character      = characterRepository.FindById(obj.CharacterId);
            var enemyBehaviour = character.characterBehaviour;
            enemyBehaviour.OntriggerEnter(obj.Character);
        }

        private void OnTriggerExit(TriggerExit obj)
        {
            var character      = characterRepository.FindById(obj.CharacterId);
            var enemyBehaviour = character.characterBehaviour;
            enemyBehaviour.OntriggerExit(obj.Character);
        }

    #endregion
    }
}