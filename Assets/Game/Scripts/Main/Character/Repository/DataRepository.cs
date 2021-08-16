#region

using Main.Character.Data;
using Utilities.Contract;
using Zenject;

#endregion

namespace Main.Character.Repository
{
    public class DataRepository : IDataRepository
    {
    #region Private Variables

        [Inject]
        private IDataRepository dataRepository;

    #endregion

    #region Public Methods

        public IActorData GetActorData(string actorDataId)
        {
            Contract.RequireString(actorDataId , "actorDataId");
            var actorData = dataRepository.GetActorData(actorDataId);
            Contract.EnsureNotNull(actorData , $"actorDataId: {actorDataId} , actorData");
            return actorData;
        }

    #endregion
    }
}