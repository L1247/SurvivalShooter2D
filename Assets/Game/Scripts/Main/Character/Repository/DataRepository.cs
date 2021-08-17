#region

using Main.Character.Data;
using Main.SO;
using Utilities.Contract;
using Zenject;

#endregion

namespace Main.Character.Repository
{
    public class DataRepository : IDataRepository
    {
    #region Private Variables

        [Inject]
        private ActorDataOverview actorDataOverview;

    #endregion

    #region Public Methods

        public IActorData GetActorData(string actorDataId)
        {
            Contract.RequireString(actorDataId , "actorDataId");
            var actorData = actorDataOverview.FindActorData(actorDataId);
            Contract.EnsureNotNull(actorData , $"actorDataId: {actorDataId} , actorData");
            return actorData;
        }

    #endregion
    }
}