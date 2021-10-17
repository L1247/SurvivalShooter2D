#region

using Main.Application;
using Main.SO;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#endregion

public class GameSetting : ScriptableObjectInstaller<GameSetting>
{
#region Public Variables

    [HideLabel]
    [BoxGroup("BattleBinder Settings")]
    public BattleBinder.Settings battleBinder;

#endregion

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
        Container.BindInstance(battleBinder).IfNotBound();
    }

#endregion
}