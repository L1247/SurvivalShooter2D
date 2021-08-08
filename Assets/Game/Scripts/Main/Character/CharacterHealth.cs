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

        public void Add(int damage)
        {
            currentHealth += damage;
            if (currentHealth <= 0) Dead();
            Debug.Log($"currentHealth {currentHealth}");
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