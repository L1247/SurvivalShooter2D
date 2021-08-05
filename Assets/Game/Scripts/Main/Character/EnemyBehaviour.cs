#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character
{
    public class EnemyBehaviour : MonoBehaviour
    {
    #region Private Variables

        private Transform tran;

        private Vector3 currentDriectionVector;

        private Vector3 spawnPosition;

        [SerializeField]
        private bool faceRight;

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
        }

        private void Update()
        {
            ProcessDirectionVector();
            tran.position += currentDriectionVector * moveSpeed * Time.deltaTime;
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