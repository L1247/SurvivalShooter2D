#region

using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class MoveBase : MonoBehaviour , IMove
    {
    #region Protected Variables

        protected bool      move;
        protected Transform trans;

        [SerializeField]
        protected int moveSpeed = 3;

    #endregion

    #region Unity events

        protected virtual void Awake()
        {
            trans = transform;
        }

        protected virtual void Update()
        {
            if (move) Move();
        }

    #endregion

    #region Public Methods

        public virtual void SetMove(bool enable)
        {
            move = enable;
        }

    #endregion

    #region Protected Methods

        protected virtual void Move() { }

    #endregion
    }
}