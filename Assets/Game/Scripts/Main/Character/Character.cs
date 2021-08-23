#region

#region

using System;
using Character.Component;
using Main.Character.Ability.Attack;
using Main.Character.Ability.Move;
using Main.Character.Behaviour;
using Main.Character.Component;
using Main.Character.Repository;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities.Contract;
using Zenject;

#endregion

#if UNITY_EDITOR
#endif

#endregion

namespace Main.Character
{
    public class Character : MonoBehaviour
    {
    #region Public Variables

        public CharacterBehaviour characterBehaviour { get; private set; }
        public CharacterHealth    CharacterHealth    { get; private set; }

        public IAttack AttackAbility { get; private set; }

        public string Id { get; private set; }

    #endregion

    #region Private Variables

        private Animator        animator;
        private CharacterFacing characterFacing;

        [Inject]
        private CharacterRepository characterRepository;

        [Inject]
        private IDataRepository dataRepository;

        private IMove move;

        [Inject]
        private SignalBus signalBus;

        [SerializeField]
        private string actorDataId;

    #endregion

    #region Public Methods

        public void Attack(bool use , Character target = null)
        {
            AttackAbility?.SetEnable(use);
            AttackAbility?.SetTarget(target);
        }

        public Vector3 GetCurrentFacingVector()
        {
            if (characterFacing == null) return Vector3.right;
            return characterFacing.CurrentDirectionVector;
        }

        public void Init()
        {
            Id = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
            GetComponentOfCharacter();
            var defaultSpriteRight = true;
            var defaultFacingRight = true;
            try
            {
                var actorData = dataRepository.GetActorData(actorDataId);
                CharacterHealth    = new CharacterHealth(Id , actorData.SettingHealth , signalBus);
                defaultSpriteRight = actorData.DefaultSpriteRight;
                defaultFacingRight = actorData.DefaultFacingRight;
            }
            catch (PostConditionViolationException e) { }

            characterFacing?.SetDefaultSpriteRight(defaultSpriteRight);
            characterFacing?.SetFacing(defaultFacingRight);
        }

        public void Move(bool use)
        {
            move?.SetEnable(use);
        }

        public void PlayAnimation(string animationName)
        {
            animator?.Play(animationName);
        }

        public void SetFacing(bool faceRight)
        {
            characterFacing?.SetFacing(faceRight);
        }

        [Button]
        public void TakeDamage(int damage)
        {
            CharacterHealth?.Add(-damage);
        }

    #endregion

    #region Private Methods

        private void GetComponentOfCharacter()
        {
            characterBehaviour = GetComponent<CharacterBehaviour>();
            move               = GetComponent<IMove>();
            AttackAbility      = GetComponent<IAttack>();
            animator           = GetComponent<Animator>();
            characterFacing    = GetComponent<CharacterFacing>();
        }

    #endregion
    }
}