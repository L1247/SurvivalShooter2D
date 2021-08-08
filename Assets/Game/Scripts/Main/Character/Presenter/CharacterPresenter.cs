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

        public void OnCharacterHurt(string hurtCharacterId , int hurtDamage)
        {
            var hurtCharacter = characterRepository.FindById(hurtCharacterId);
            var position      = hurtCharacter.transform.position;
            var textColor     = hurtDamage < 0 ? Color.red : Color.green;
            var context       = hurtDamage.ToString();
            popupTextSpawner.Spawn(position , textColor , context);
        }

    #endregion
    }
}