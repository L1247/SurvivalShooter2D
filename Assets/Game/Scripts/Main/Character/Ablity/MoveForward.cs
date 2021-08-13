#region

using UnityEngine;

#endregion

namespace Main.Character.Ablity
{
    public class MoveForward : MonoBehaviour
    {
    #region Private Variables

        private bool move;

        private Transform trans;

        [SerializeField]
        private int moveSpeed = 3;

    #endregion

    #region Unity events

        private void Awake()
        {
            trans = transform;
        }

        private void Update()
        {
            if (move) Move();
        }

    #endregion

    #region Public Methods

        public void Move(bool move)
        {
            this.move = move;
        }

    #endregion

    #region Private Methods

        private void Move()
        {
            trans.Translate(trans.right * moveSpeed * Time.deltaTime);
        }

    #endregion
    }
}