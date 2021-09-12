#region

using rStarTools.Scripts.StringList;
using UnityEngine;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorDataOverview" , menuName = "Survival2D/ActorDataOverview" , order = 0)]
    public class ActorDataOverview : DataOverviewBase<ActorDataOverview , ActorData> { }
}