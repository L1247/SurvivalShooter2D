#region

using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Main.SO
{
    public interface IEnemyAttack
    {
    #region Public Methods

        void Attack();

    #endregion
    }

    [Serializable]
    public abstract class EnemyAttack : IEnemyAttack
    {
    #region Public Variables

        public int damage;

    #endregion

    #region Public Methods

        public virtual void Attack() { }

    #endregion
    }

    [Serializable]
    public class KnifeAttack : EnemyAttack
    {
    #region Public Variables

        public float distance;

    #endregion

    #region Public Methods

        public override void Attack()
        {
            /*SOMETHING*/
        }

    #endregion
    }

    [Serializable]
    public class GunAttack : EnemyAttack
    {
    #region Public Variables

        public float distance;
        public int   ammo;

    #endregion

    #region Public Methods

        public override void Attack()
        {
            /*SOMETHING*/
        }

    #endregion
    }

    [CreateAssetMenu]
    public class EnemyData : SerializedScriptableObject
    {
    #region Public Variables

        // [ValueDropdown("GetEnemyAttacks")]
        // [OdinSerialize]
        [TypeFilter("GetEnemyAttacks")]
        [BoxGroup]
        public EnemyAttack attack;

    #endregion

    #region Private Methods

        private IEnumerable GetEnemyAttacks()
        {
            var q = typeof(IEnemyAttack).Assembly
                                        .GetTypes()
                                        .Where(x => x.IsAbstract == false)
                                        .Where(x => x.IsGenericTypeDefinition == false)
                                        .Where(x => typeof(IEnemyAttack).IsAssignableFrom(x));
            // .Select(x => new ValueDropdownItem(x.Name , x)); //用TypeFilter的話就不會有這行
            return q;
        }

    #endregion
    }
}