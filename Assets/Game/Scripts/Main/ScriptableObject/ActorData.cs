#region

using Main.Character.Data;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorData" , menuName = "Survival2D/ActorData" , order = 0)]
    public class ActorData : ScriptableObject , IActorData
    {
    #region Public Variables

        public int StartingHealth => startingHealth;

        public string ActorDataId => actorDataId;

        [LabelText("角色Prefab")]
        [Required]
        [PropertyOrder(1)]
        public GameObject actorPrefab;

    #endregion

    #region Private Variables

        [SerializeField]
        [LabelText("初始生命")]
        [ValidateInput("@startingHealth>0" , "can't be zero , or small than zero")]
        private int startingHealth = 100;

        [SerializeField]
        [LabelText("角色ID")]
        [Required]
        [PropertyOrder(0)]
        private string actorDataId;

    #endregion
    }
}