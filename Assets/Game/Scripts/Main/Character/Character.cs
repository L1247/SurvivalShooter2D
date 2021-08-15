#region

using System;
using Character.Component;
using Main.Character.Ability.Move;
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

        private Animator animator;

        private CharacterHealth characterHealth;

        [Inject]
        private CharacterRepository characterRepository;

        private IMove move;

    #endregion

    #region Unity events

        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
            characterBehaviour = GetComponent<CharacterBehaviour>();
            characterHealth    = GetComponent<CharacterHealth>();
            move               = GetComponent<IMove>();
            animator           = GetComponent<Animator>();
        }

    #endregion

    #region Public Methods

        public void Move(bool enable)
        {
            move?.SetMove(enable);
        }

        public void PlayAnimation(string animationName)
        {
            animator?.Play(animationName);
        }

        public void TakeDamage(int damage)
        {
            characterHealth?.Add(-damage);
        }

    #endregion
    }
}