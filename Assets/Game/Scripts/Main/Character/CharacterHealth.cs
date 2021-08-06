#region

using UnityEngine;

#endregion

namespace Main.Character
{
    public class CharacterHealth : MonoBehaviour
    {
    #region Private Variables

        private int currentHealth;

        [SerializeField]
        private int StartingHealth = 100;

    #endregion

    #region Unity events

        private void Awake()
        {
            currentHealth = StartingHealth;
        }

    #endregion

    #region Public Methods

        public void Add(int damage)
        {
            currentHealth += damage;
            if (currentHealth <= 0) Dead();
            Debug.Log($"currentHealth {currentHealth}");
        }

    #endregion

    #region Private Methods

        private void Dead()
        {
            Debug.Log("Dead");
        }

    #endregion
    }
}