#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class Patrol : MoveBase
    {
    #region Private Variables

        private Vector3 currentDirectionVector;

        private Vector3 spawnPosition;

        [SerializeField]
        private bool faceRight;

        [SerializeField]
        [ReadOnly]
        [BoxGroup("ReadOnly")]
        private float leftPatrolX;

        [SerializeField]
        [LabelText("巡邏的間距")]
        [PropertyOrder(0)]
        private float patrolOffsetX = 3f;

        [SerializeField]
        [ReadOnly]
        [BoxGroup("ReadOnly")]
        private float rightPatrolX;

        [SerializeField]
        [Required]
        private SpriteRenderer spriteRenderer;

    #endregion

    #region Unity events

        protected override void Awake()
        {
            base.Awake();
            currentDirectionVector = faceRight ? Vector3.right : Vector3.left;
            ProcessPatrolPositions();
            HandleCharacterFace();
        }

    #endregion

    #region Protected Methods

        protected override void Move()
        {
            ProcessDirectionVector();
            trans.position += currentDirectionVector * moveSpeed * Time.deltaTime;
        }

    #endregion

    #region Private Methods

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
            var positionX = trans.position.x;
            if (positionX < leftPatrolX)
            {
                currentDirectionVector = Vector3.right;
                faceRight              = true;
                HandleCharacterFace();
            }

            if (positionX > rightPatrolX)
            {
                currentDirectionVector = Vector3.left;
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