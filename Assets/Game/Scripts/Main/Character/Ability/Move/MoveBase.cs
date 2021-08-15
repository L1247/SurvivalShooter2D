#region

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class MoveBase : MonoBehaviour , IMove
    {
    #region Protected Variables

        protected bool move;

        protected Character character;
        protected Transform trans;

        [SerializeField]
        protected int moveSpeed = 3;

    #endregion

    #region Unity events

        protected virtual void Awake()
        {
            character = GetComponent<Character>();
            trans     = transform;
        }

        protected virtual void Update()
        {
            if (move) Move();
        }

    #endregion

    #region Public Methods

        [Button]
        public virtual void Move() { }

        public virtual void SetEnable(bool enable)
        {
            move = enable;
        }

    #endregion
    }
}