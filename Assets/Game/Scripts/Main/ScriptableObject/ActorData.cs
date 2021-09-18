#region

using System;
using System.Collections.Generic;
using System.Linq;
using Character.Component;
using EditorUtilities;
using JetBrains.Annotations;
using Main.Character.Ability.Move;
using Main.Character.Behaviour;
using Main.Character.Component;
using Main.Character.Data;
using rStarTools.Scripts.StringList;
using rStarTools.Scripts.StringList.Custom_Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.SO
{
    [CreateAssetMenu(fileName = "ActorData" , menuName = "Survival2D/ActorData" , order = 0)]
    public class ActorData : SODataBase<ActorDataOverview> , IActorData
    {
    #region Public Variables

        public CharacterBehaviour CharacterBehaviour => characterBehaviour;
        public MoveBase           MoveAbility        => moveBase;

        public CharacterFacing.Setting SettingFacing => settingFacing;

        public CharacterHealth.Setting SettingHealth => settingHealth;

        [LabelText("角色Prefab")]
        [Required]
        [PropertyOrder(2)]
        [OnValueChanged("ChangePreview")]
        [AssetSelector(Paths = "Assets/Game/Prefab/Actor")]
        public GameObject actorPrefab;

    #endregion

    #region Private Variables

        [Required]
        [HideLabel]
        [SerializeField]
        [BoxGroup("CharacterBehaviour")]
        [TypeFilter("GetCharacterBehaviour")]
        private CharacterBehaviour characterBehaviour;

        [HideLabel]
        [Required]
        [SerializeField]
        [ColoredBoxGroup("Ability" , 1 , 0 , 0 , 1)]
        [TypeFilter("GetMoveBase")]
        private MoveBase moveBase;

        [SerializeField]
        [BoxGroup("Sprite面相資料")]
        [HideLabel]
        [Required]
        private CharacterFacing.Setting settingFacing;

        [SerializeField]
        [BoxGroup("血量資料")]
        [HideLabel]
        [Required]
        private CharacterHealth.Setting settingHealth;

        [SerializeField]
        [PropertyOrder(-1)]
        [PreviewField(Height = 100 , Alignment = ObjectFieldAlignment.Center)]
        [HideLabel]
        private Sprite preview;

    #endregion

    #region Private Methods

        private void ChangePreview()
        {
            if (actorPrefab == null) return;
            var spriteRenderer = actorPrefab.GetComponent<SpriteRenderer>();
            preview = spriteRenderer.sprite;
            CustomEditorUtility.SetDirty(this);
            CustomEditorUtility.SaveAssets();
        }

        [UsedImplicitly]
        private IEnumerable<Type> GetCharacterBehaviour()
        {
            var q = typeof(CharacterBehaviour).Assembly
                                              .GetTypes()
                                              .Where(x => x.IsAbstract == false)
                                              .Where(x => x.IsGenericTypeDefinition == false)
                                              .Where(x => typeof(CharacterBehaviour).IsAssignableFrom(x));
            return q;
        }

        [UsedImplicitly]
        private IEnumerable<Type> GetMoveBase()
        {
            var q = typeof(MoveBase).Assembly
                                    .GetTypes()
                                    .Where(x => x.IsAbstract == false)
                                    .Where(x => x.IsGenericTypeDefinition == false)
                                    .Where(x => typeof(MoveBase).IsAssignableFrom(x));
            return q;
        }


        [OnInspectorInit]
        private void InspectorInit()
        {
            ChangePreview();
        }

    #endregion
    }
}