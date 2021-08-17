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

        public string ActorDataId => actorDataId;

        [LabelText("角色Prefab")]
        [Required]
        [PropertyOrder(1)]
        public GameObject actorPrefab;

    #endregion

    #region Private Variables

        [SerializeField]
        [LabelText("角色ID")]
        [Required]
        [PropertyOrder(0)]
        private string actorDataId;

    #endregion
    }
}