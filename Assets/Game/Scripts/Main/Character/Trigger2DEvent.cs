#region

using Main.Event;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Character
{
    public class Trigger2DEvent : MonoBehaviour
    {
    #region Private Variables

        [Inject]
        private SignalBus signalBus;

        private string characterId;

        [SerializeField]
        private string TAG_TARGET = "Player";

    #endregion

    #region Unity events

        private void Start()
        {
            var character = GetComponent<Character>();
            characterId = character.Id;
            RegisterTriggerEvent();
        }

    #endregion

    #region Private Methods

        private void OnTriggerEnter2D(Collider2D obj)
        {
            var character = obj.GetComponent<Character>();
            signalBus.Fire(new TriggerEnter(characterId , character));
        }


        private void OnTriggerExit2D(Collider2D obj)
        {
            var character = obj.GetComponent<Character>();
            signalBus.Fire(new TriggerExit(characterId , character));
        }

        private void RegisterTriggerEvent()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(collider2D => collider2D.CompareTag(TAG_TARGET))
                .Subscribe(OnTriggerEnter2D)
                .AddTo(gameObject);
            this.OnTriggerExit2DAsObservable()
                .Where(collider2D => collider2D.CompareTag(TAG_TARGET))
                .Subscribe(OnTriggerExit2D)
                .AddTo(gameObject);
        }

    #endregion
    }
}