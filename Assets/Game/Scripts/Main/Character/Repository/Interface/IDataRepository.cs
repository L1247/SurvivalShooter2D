#region

using Main.Character.Data;

#endregion

namespace Main.Character.Repository
{
    public interface IDataRepository
    {
    #region Public Methods

        public IActorData GetActorData(string actorDataId);

    #endregion
    }
}