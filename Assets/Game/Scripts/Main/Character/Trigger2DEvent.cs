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

        [SerializeField]
        private string TAG_TARGET = "Player";

    #endregion

    #region Unity events

        private void Awake()
        {
            RegisterTriggerEvent();
        }

    #endregion

    #region Private Methods

        private void OnTriggerEnter2D(Collider2D obj)
        {
            signalBus.Fire(new TriggerEnter());
            // enemyBehaviour.playerHealth = obj.GetComponent<CharacterHealth>();
        }

        private void OnTriggerExit2D(Collider2D obj)
        {
            signalBus.Fire(new TriggerExit());
            // enemyBehaviour.playerHealth = null;
        }

        private void RegisterTriggerEvent()
        {
            this
                .OnTriggerEnter2DAsObservable()
                .Where(collider2D => collider2D.CompareTag(TAG_TARGET))
                .Subscribe(OnTriggerEnter2D);
            this
                .OnTriggerExit2DAsObservable()
                .Where(collider2D =>
                           collider2D.CompareTag(TAG_TARGET))
                .Subscribe(OnTriggerExit2D);
        }

    #endregion
    }
}