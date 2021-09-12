#region

using System;
using rStarTools.Scripts.StringList;

#endregion

namespace Main.SO
{
    [Serializable]
    public class ActorName : NameBase<ActorDataOverview>
    {
    #region Protected Variables

        protected override string LabelText => "角色名稱:";

    #endregion
    }
}