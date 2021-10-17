#region

using System;
using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    [Serializable]
    public class MoveSetting
    {
    #region Public Variables

        public int moveSpeed = 3;

    #endregion
    }

    [Serializable]
    public abstract class MoveBase : IMove
    {
    #region Protected Variables

        protected bool move;

        protected Character character;

        protected int       moveSpeed;
        protected Transform trans;

    #endregion

    #region Constructor

        public MoveBase(Character character)
        {
            this.character = character;
            if (character != null) trans = this.character.transform;
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

        public abstract MoveSetting GetSetting();

        public virtual void Move() { }

        public virtual void SetEnable(bool enable)
        {
            move = enable;
        }

        public virtual void SetSetting(MoveSetting setting)
        {
            moveSpeed = setting.moveSpeed;
        }

    #endregion
    }
}