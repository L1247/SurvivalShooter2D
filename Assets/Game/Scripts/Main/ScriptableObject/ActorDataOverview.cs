#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorDataOverview" , menuName = "Survival2D/ActorDataOverview" , order = 0)]
    public class ActorDataOverview : ScriptableObject
    {
    #region Private Variables

        [SerializeField]
        private List<ActorData> actorDatas = new List<ActorData>();

    #endregion

    #region Public Methods

        public ActorData FindActorData(string actorDataId)
        {
            return actorDatas.Find(data => data.ActorDataId == actorDataId);
        }

    #endregion
    }
}