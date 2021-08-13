#region

using System;
using Character.Component;
using Main.Character.Ablity;
using Main.Character.Behaviour;
using Main.Character.Repository;
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

        private MoveForward moveForward;

    #endregion

    #region Unity events

        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
            characterBehaviour = GetComponent<CharacterBehaviour>();
            characterHealth    = GetComponent<CharacterHealth>();
            moveForward        = GetComponent<MoveForward>();
        }

    #endregion

    #region Public Methods

        public void Move(bool move)
        {
            moveForward.Move(move);
        }

        public void TakeDamage(int damage)
        {
            characterHealth.Add(-damage);
        }

    #endregion
    }
}