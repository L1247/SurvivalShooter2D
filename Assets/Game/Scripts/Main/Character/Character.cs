#region

using System;
using Character.Component;
using Main.Character.Ability.Attack;
using Main.Character.Ability.Move;
using Main.Character.Behaviour;
using Main.Character.Component;
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

        private Animator        animator;
        private CharacterFacing characterFacing;

        private CharacterHealth characterHealth;

        [Inject]
        private CharacterRepository characterRepository;

        private IAttack attack;

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
            attack             = GetComponent<IAttack>();
            animator           = GetComponent<Animator>();
            characterFacing    = GetComponent<CharacterFacing>();
        }

    #endregion

    #region Public Methods

        public void Attack(bool enable , Character target = null)
        {
            attack?.SetEnable(enable);
            attack?.SetTarget(target);
        }

        public Vector3 GetCurrentFacingVector()
        {
            return characterFacing.CurrentDirectionVector;
        }

        public void Move(bool enable)
        {
            move?.SetEnable(enable);
        }

        public void PlayAnimation(string animationName)
        {
            animator?.Play(animationName);
        }

        public void SetFacing(bool faceRight)
        {
            characterFacing.SetFacing(faceRight);
        }

        public void TakeDamage(int damage)
        {
            characterHealth?.Add(-damage);
        }

    #endregion
    }
}