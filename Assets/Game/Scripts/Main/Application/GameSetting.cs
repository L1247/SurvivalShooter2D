#region

using Main.SO;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

public class GameSetting : ScriptableObjectInstaller<GameSetting>
{
#region Private Variables

    [SerializeField]
    [Required]
    private ActorDataOverview actorDataOverview;

    [SerializeField]
    [Required]
    private GameObject PopupTextPrefab;

#endregion

#region Public Methods

    public override void InstallBindings()
    {
        Container.BindInstance(PopupTextPrefab).WithId("PopupTextPrefab");
        Container.BindInstance(actorDataOverview);
    }

#endregion
}