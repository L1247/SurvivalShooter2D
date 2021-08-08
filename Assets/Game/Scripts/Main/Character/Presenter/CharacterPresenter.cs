#region

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

        [SerializeField]
        private GameObject popupTextPrefab;

    #endregion

    #region Public Methods

        public void OnCharacterHurt(string hurtCharacterId , int hurtDamage)
        {
            var hurtCharacter      = characterRepository.FindById(hurtCharacterId);
            var position           = hurtCharacter.transform.position;
            var popupTextInstance  = Instantiate(popupTextPrefab , position , Quaternion.identity);
            var popupTextComponent = popupTextInstance.GetComponent<PopupTextComponent>();
            var textColor          = hurtDamage < 0 ? Color.red : Color.green;
            var context            = hurtDamage.ToString();
            popupTextComponent.Show(context , textColor);
        }

    #endregion
    }
}