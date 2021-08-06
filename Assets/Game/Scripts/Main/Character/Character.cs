#region

using System;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Character
{
    public class Character : MonoBehaviour
    {
    #region Public Variables

        public CharacterBehaviour characterBehaviour { get; private set; }

        public string Id { get; private set; }

    #endregion

    #region Private Variables

        private CharacterHealth characterHealth;

        [Inject]
        private CharacterRepository characterRepository;

    #endregion

    #region Unity events

        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
            characterBehaviour = GetComponent<CharacterBehaviour>();
            characterHealth    = GetComponent<CharacterHealth>();
        }

    #endregion

    #region Public Methods

        public void TakeDamage(int damage)
        {
            characterHealth.Add(-damage);
        }

    #endregion
    }
}