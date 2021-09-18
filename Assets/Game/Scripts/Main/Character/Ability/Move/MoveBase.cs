#region

using System;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    [Serializable]
    public abstract class MoveBase : IMove
    {
    #region Protected Variables

        protected bool move;

        protected Character character;
        protected Transform trans;

        [SerializeField]
        protected int moveSpeed = 3;

    #endregion

    #region Constructor

        public MoveBase(Character character)
        {
            this.character = character;
            trans          = this.character.transform;
        }

    #endregion

    #region Unity events

        public virtual void Start() { }

        public virtual void Update()
        {
            if (move) Move();
        }

    #endregion

    #region Public Methods

        public virtual void Move() { }

        public virtual void SetEnable(bool enable)
        {
            move = enable;
        }

    #endregion
    }
}