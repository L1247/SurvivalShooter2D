#region

using Main.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

namespace Character.Component
{
    public class CharacterHealth : MonoBehaviour
    {
    #region Private Variables

        [Inject]
        private SignalBus signalBus;

        private string characterId;

        [SerializeField]
        [ReadOnly]
        private int currentHealth;

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
            var character = GetComponent<Main.Character.Character>();
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