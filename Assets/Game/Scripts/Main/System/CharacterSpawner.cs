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

        [Inject]
        private DiContainer container;

        [Inject]
        private IDataRepository dataRepository;

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
            var characterInstance = container.InstantiatePrefab(characterPrefab , spawnPoint.transform);
            characterInstance.transform.localPosition = Vector3.zero;
            characterInstance.transform.parent        = null;
            var character = characterInstance.GetComponent<Character.Character>();
            character.Init(actorData);
        }

    #endregion
    }
}