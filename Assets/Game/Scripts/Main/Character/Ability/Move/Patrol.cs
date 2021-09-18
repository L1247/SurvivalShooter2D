#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class Patrol : MoveBase
    {
    #region Private Variables

        private Vector3 spawnPosition;

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

    #endregion

    #region Constructor

        public Patrol(Character character) : base(character) { }

    #endregion

    #region Unity events

        public override void Start()
        {
            ProcessPatrolPositions();
        }

    #endregion

    #region Public Methods

        public override void Move()
        {
            DetectFacing();
            trans.position += character.GetCurrentFacingVector() * moveSpeed * Time.deltaTime;
        }

    #endregion

    #region Private Methods

        private void DetectFacing()
        {
            var positionX = trans.position.x;
            if (positionX < leftPatrolX) character.SetFacing(true);
            if (positionX > rightPatrolX) character.SetFacing(false);
        }

        private void DrawLine(float patrolX , Vector3 spawnPosition)
        {
            var spawnPositionY = spawnPosition.y;
            var spawnPositionZ = spawnPosition.z;
            var startPoint     = new Vector3(patrolX , spawnPositionY ,        spawnPositionZ);
            var endPoint       = new Vector3(patrolX , spawnPositionY - 2.5f , spawnPositionZ);
            Gizmos.DrawLine(startPoint , endPoint);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (UnityEngine.Application.isPlaying == false)
                ProcessPatrolPositions();
            DrawLine(leftPatrolX ,  spawnPosition);
            DrawLine(rightPatrolX , spawnPosition);
        }

        private void ProcessPatrolPositions()
        {
            spawnPosition = trans.position;
            var spawnPositionX = spawnPosition.x;
            leftPatrolX  = spawnPositionX - patrolOffsetX;
            rightPatrolX = spawnPositionX + patrolOffsetX;
        }

    #endregion
    }
}