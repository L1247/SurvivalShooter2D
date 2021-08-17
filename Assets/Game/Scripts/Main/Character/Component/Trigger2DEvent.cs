#region

using Main.Event;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

#endregion

namespace Character.Component
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
            var character = GetComponent<Main.Character.Character>();
            characterId = character.Id;
            RegisterTriggerEvent();
        }

    #endregion

    #region Private Methods

        private void RegisterTriggerEvent()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(collider2D => collider2D.CompareTag(TAG_TARGET))
                .Subscribe(TriggerEnter)
                .AddTo(gameObject);
            this.OnTriggerExit2DAsObservable()
                .Where(collider2D => collider2D.CompareTag(TAG_TARGET))
                .Subscribe(TriggerExit)
                .AddTo(gameObject);
        }

        private void TriggerEnter(Collider2D obj)
        {
            var character = obj.GetComponent<Main.Character.Character>();
            signalBus.Fire(new TriggerEnter(characterId , character));
        }


        private void TriggerExit(Collider2D obj)
        {
            var character = obj.GetComponent<Main.Character.Character>();
            signalBus.Fire(new TriggerExit(characterId , character));
        }

    #endregion
    }
}