#region

using Main.Event;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Character
{
    public class CharacterHealth : MonoBehaviour
    {
    #region Private Variables

        private int currentHealth;

        [Inject]
        private SignalBus signalBus;

        private string characterId;

        [SerializeField]
        private int StartingHealth = 100;

    #endregion

    #region Unity events

        private void Awake()
        {
            currentHealth = StartingHealth;
        }

        private void Start()
        {
            var character = GetComponent<Character>();
            characterId = character.Id;
        }

    #endregion

    #region Public Methods

        public void Add(int amount)
        {
            currentHealth += amount;
            signalBus.Fire(new CharacterHealthModified(characterId , amount));
            if (currentHealth <= 0) Dead();
        }

    #endregion

    #region Private Methods

        private void Dead()
        {
            signalBus.Fire(new CharacterDead(characterId));
        }

    #endregion
    }
}