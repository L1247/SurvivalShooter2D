#region

using System;
using Main.Event;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

namespace Character.Component
{
    public class CharacterHealth
    {
    #region Private Variables

        private int currentHealth;

        private readonly SignalBus signalBus;

        private readonly string characterId;

    #endregion

    #region Constructor

        public CharacterHealth(string id , Setting setting , SignalBus signalBus)
        {
            this.signalBus = signalBus;
            currentHealth  = setting.StartingHealth;
            characterId    = id;
        }

    #endregion

    #region Public Methods

        public void Add(int amount)
        {
            currentHealth += amount;
            signalBus.Fire(new CharacterHealthModified(characterId , amount));
            if (currentHealth <= 0) Dead();
        }

    #endregion

    #region Private Methods

        private void Dead()
        {
            signalBus.Fire(new CharacterDead(characterId));
        }

    #endregion

    #region Nested Types

        [Serializable]
        public class Setting
        {
        #region Public Variables

            public int StartingHealth => startingHealth;

        #endregion

        #region Private Variables

            [SerializeField]
            [LabelText("初始生命")]
            [ValidateInput("@startingHealth>0" , "can't be zero , or small than zero")]
            [PropertyOrder]
            private int startingHealth = 100;

        #endregion
        }

    #endregion
    }
}