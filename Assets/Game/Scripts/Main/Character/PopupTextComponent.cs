#region

using DG.Tweening;
using TMPro;
using UnityEngine;

#endregion

namespace Main.Character
{
    public class PopupTextComponent : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        private TMP_Text text;

    #endregion

    #region Unity events

        private void Start()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0 , 1.5f)
                       .OnComplete(() => Destroy(gameObject));
            transform.DOMoveY(3 , 1.5f);
        }

    #endregion

    #region Public Methods

        public void Show(string context , Color textColor)
        {
            text.color = textColor;
            text.text  = context;
        }

    #endregion
    }
}