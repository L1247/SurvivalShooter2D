#region

using Main.Character.Repository;
using Main.SO;
using UnityEngine;
using Utilities.Contract;
using Zenject;

#endregion

namespace Main.System
{
    public class CharacterSpawner
    {
    #region Private Variables

        private readonly Character.Character.Factory characterFactory;

        [Inject]
        private DiContainer container;

        [Inject]
        private IDataRepository dataRepository;

    #endregion

    #region Constructor

        public CharacterSpawner(Character.Character.Factory characterFactory)
        {
            this.characterFactory = characterFactory;
        }

    #endregion

    #region Public Methods

        public void Spawn(string actorDataId)
        {
            Contract.RequireString(actorDataId , "actorDataId");
            var actorData = dataRepository.GetActorData(actorDataId) as ActorData;
            Contract.RequireNotNull(actorData , $"actorDataId: {actorDataId} actorData");
            var spawnPointName = $"SpawnPoint_{actorData.DisplayName}";
            var spawnPoint     = GameObject.Find(spawnPointName);
            Contract.RequireNotNull(spawnPoint , $"spawnPointName: {spawnPointName} spawnPoint");
            var characterPrefab = actorData.actorPrefab;
            Contract.RequireNotNull(characterPrefab , $"actorDataId: {actorDataId} characterPrefab");
            characterFactory.Create(actorData);
        }

    #endregion
    }
}