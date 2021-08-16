#region

using Main.Character.Repository;
using Main.SO;
using Main.System;
using UnityEngine;
using Utilities.Contract;
using Zenject;

#endregion

namespace Main.Character.Presenter
{
    public class CharacterPresenter
    {
    #region Private Variables

        [Inject]
        private CharacterRepository characterRepository;

        [Inject]
        private DiContainer container;

        [Inject]
        private IDataRepository dataRepository;

        [Inject]
        private PopupTextSpawner popupTextSpawner;

    #endregion

    #region Public Methods

        public void ShowPopupText(string characterId , int amount)
        {
            var character = characterRepository.FindById(characterId);
            var position  = character.transform.position;
            var textColor = amount <= 0 ? Color.red : Color.green;
            var context   = amount.ToString();
            popupTextSpawner.Spawn(position , textColor , context);
        }

        public void SpawnCharacter(string actorDataId)
        {
            Contract.RequireString(actorDataId , "actorDataId");
            var spawnPointName = $"SpawnPoint_{actorDataId}";
            var spawnPoint     = GameObject.Find(spawnPointName);
            Contract.RequireNotNull(spawnPoint , $"spawnPointName: {spawnPointName} spawnPoint");
            var actorData = dataRepository.GetActorData(actorDataId) as ActorData;
            Contract.RequireNotNull(actorData , $"actorDataId: {actorDataId} actorData");
            var characterPrefab = actorData.actorPrefab;
            Contract.RequireNotNull(characterPrefab , $"actorDataId: {actorDataId} characterPrefab");
            var characterInstance = container.InstantiatePrefab(characterPrefab , spawnPoint.transform);
            characterInstance.transform.localPosition = Vector3.zero;
            characterInstance.transform.parent        = null;
        }

    #endregion
    }
}