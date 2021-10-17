#region

using System;
using Character.Component;
using Main.Character.Ability.Attack;
using Main.Character.Ability.Move;
using Main.Character.Behaviour;
using Main.Character.Component;
using Main.Character.Data;
using Main.Character.Repository;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion


namespace Main.Character
{
    public class Character : MonoBehaviour
    {
    #region Public Variables

        public BoxCollider2D BoxCollider2D { get; private set; }

        public CharacterBehaviour CharacterBehaviour { get; private set; }
        public CharacterHealth    CharacterHealth    { get; private set; }

        public IAttack        AttackAbility  { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public string Id { get; private set; }

    #endregion

    #region Private Variables

        private Animator        animator;
        private CharacterFacing characterFacing;

        [Inject]
        private CharacterRepository characterRepository;

        private IMove move;

        [Inject]
        private SignalBus signalBus;

    #endregion

    #region Unity events

        private void Update()
        {
            move.Update();
        }

    #endregion

    #region Public Methods

        public void Attack(bool use , Character target = null)
        {
            AttackAbility?.SetEnable(use);
            AttackAbility?.SetTarget(target);
        }

        [Inject]
        public void Construct(IActorData actorData)
        {
            // var characterInstance = container.InstantiatePrefab(characterPrefab , spawnPoint.transform);
            // characterInstance.transform.localPosition = Vector3.zero;
            // characterInstance.transform.parent        = null;
            var moveAbilityType = actorData.Move;
            move = (IMove)Activator.CreateInstance(moveAbilityType , new object[] { this });
            move.SetSetting(actorData.MoveSetting);
            move.Start();
            animator       = GetComponent<Animator>();
            BoxCollider2D  = GetComponent<BoxCollider2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Id             = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
            CharacterHealth = new CharacterHealth(Id , actorData.SettingHealth , signalBus);
            characterFacing = new CharacterFacing(SpriteRenderer , actorData.SettingFacing);
            var behaviourType = actorData.CharacterBehaviour.GetType();
            CharacterBehaviour = (CharacterBehaviour)Activator.CreateInstance(behaviourType , new object[] { this });
        }

        // Note that we can't use a constructor anymore since we are a MonoBehaviour now

        public Vector3 GetCurrentFacingVector()
        {
            if (characterFacing == null) return Vector3.right;
            return characterFacing.CurrentDirectionVector;
        }

        public void Move(bool use)
        {
            move.SetEnable(use);
        }

        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
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
            move          = GetComponent<IMove>();
            AttackAbility = GetComponent<IAttack>();
        }

    #endregion

    #region Nested Types

        public class Factory : PlaceholderFactory<IActorData , Character> { }

    #endregion
    }
}