#region

using Main.System;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Character
{
    public class CharacterPresenter : MonoBehaviour
    {
    #region Private Variables

        [Inject]
        private CharacterRepository characterRepository;

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

    #endregion
    }
}