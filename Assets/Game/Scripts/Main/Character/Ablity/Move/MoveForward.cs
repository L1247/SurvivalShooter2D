#region

#endregion

#region

using UnityEngine;

#endregion

namespace Main.Character.Ability.Move
{
    public class MoveForward : MoveBase
    {
    #region Protected Methods

        protected override void Move()
        {
            trans.Translate(trans.right * moveSpeed * Time.deltaTime);
        }

    #endregion
    }
}