#region

using System;
using UnityEngine;
using Zenject;

#endregion

namespace Main.Character
{
    public class Character : MonoBehaviour
    {
    #region Public Variables

        public string Id { get; private set; }

    #endregion

    #region Private Variables

        [Inject]
        private CharacterRepository characterRepository;

    #endregion

    #region Unity events

        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
            characterRepository.Register(Id , this);
        }

    #endregion
    }
}