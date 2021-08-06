#region

using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

#endregion

namespace Main.Character
{
    public class EnemyBehaviour : CharacterBehaviour
    {
    #region Private Variables

        private bool      isAttacking;
        private Character attackingCharacter;

        private Transform tran;

        private Vector3 currentDriectionVector;

        private Vector3 spawnPosition;

        [SerializeField]
        private bool faceRight;

        [SerializeField]
        private float AttackSpeed = 0.5f;

        [SerializeField]
        [ReadOnly]
        [BoxGroup("ReadOnly")]
        private float leftPatrolX;

        [SerializeField]
        private float moveSpeed = 3f;

        [SerializeField]
        private float patrolOffsetX;

        [SerializeField]
        [ReadOnly]
        [BoxGroup("ReadOnly")]
        private float rightPatrolX;

        [SerializeField]
        private int damage = 10;

        [SerializeField]
        [Required]
        private SpriteRenderer spriteRenderer;

    #endregion

    #region Unity events

        private void Awake()
        {
            tran                   = transform;
            currentDriectionVector = faceRight ? Vector3.right : Vector3.left;
            ProcessPatrolPositions();
            HandleCharacterFace();

            var attackTimeSpan = TimeSpan.FromSeconds(AttackSpeed);

            Observable.Interval(attackTimeSpan , Scheduler.MainThread)
                      .Where(l => IsAttacking())
                      .Subscribe(AttackPlayer);
        }

        private void Update()
        {
            // stop moving on player triggered
            if (IsAttacking() == false) Move();
        }

    #endregion

    #region Public Methods

        public override void OntriggerEnter(Character target)
        {
            attackingCharacter = target;
            isAttacking        = true;
        }

        public override void OntriggerExit(Character target)
        {
            isAttacking = false;
            if (attackingCharacter == target) attackingCharacter = null;
        }

    #endregion

    #region Private Methods

        private void AttackPlayer(long obj)
        {
            attackingCharacter.TakeDamage(damage);
        }

        private void DrawLine(float patrolX , Vector3 spawnPosition)
        {
            var spawnPositionY = spawnPosition.y;
            var spawnPositionZ = spawnPosition.z;
            var startPoint     = new Vector3(patrolX , spawnPositionY ,        spawnPositionZ);
            var endPoint       = new Vector3(patrolX , spawnPositionY - 2.5f , spawnPositionZ);
            Gizmos.DrawLine(startPoint , endPoint);
        }

        private void HandleCharacterFace()
        {
            spriteRenderer.flipX = faceRight;
        }

        private bool IsAttacking()
        {
            return isAttacking;
        }

        private void Move()
        {
            ProcessDirectionVector();
            tran.position += currentDriectionVector * moveSpeed * Time.deltaTime;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (UnityEngine.Application.isPlaying == false)
                ProcessPatrolPositions();
            DrawLine(leftPatrolX ,  spawnPosition);
            DrawLine(rightPatrolX , spawnPosition);
        }

        private void ProcessDirectionVector()
        {
            var positionX = tran.position.x;
            if (positionX < leftPatrolX)
            {
                currentDriectionVector = Vector3.right;
                faceRight              = true;
                HandleCharacterFace();
            }

            if (positionX > rightPatrolX)
            {
                currentDriectionVector = Vector3.left;
                faceRight              = false;
                HandleCharacterFace();
            }
        }

        private void ProcessPatrolPositions()
        {
            spawnPosition = transform.position;
            var spawnPositionX = spawnPosition.x;
            leftPatrolX  = spawnPositionX - patrolOffsetX;
            rightPatrolX = spawnPositionX + patrolOffsetX;
        }

    #endregion
    }
}