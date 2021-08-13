#region

using Character.Component;
using UnityEngine;
using Zenject;

#endregion

namespace Main.System
{
    public class PopupTextSpawner
    {
    #region Private Variables

        [Inject(Id = "PopupTextPrefab")]
        private GameObject popupTextPrefab;

    #endregion

    #region Public Methods

        public void Spawn(Vector3 position , Color textColor , string context)
        {
            var popupTextInstance  = Object.Instantiate(popupTextPrefab , position , Quaternion.identity);
            var popupTextComponent = popupTextInstance.GetComponent<PopupTextComponent>();
            popupTextComponent.Show(context , textColor);
        }

    #endregion
    }
}