#region

using UnityEngine;
using Zenject;

#endregion

public class GameSetting : ScriptableObjectInstaller<GameSetting>
{
#region Private Variables

    [SerializeField]
    private GameObject PopupTextPrefab;

#endregion

#region Public Methods

    public override void InstallBindings()
    {
        Container.BindInstance(PopupTextPrefab).WithId("PopupTextPrefab");
    }

#endregion
}