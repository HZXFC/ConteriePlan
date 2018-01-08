using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 导师攻击
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "TutorAttack")]
    public class TutorAttack : AttackBase, IOneTimeBehaviourType, IDesignationBehaviourType, IGroupBehaviourType
    {
        /// <summary>
        /// 可指定范围
        /// </summary>
        [SerializeField]
        private float designationRange = 20f;

        public bool IsAdsorption => true;
        public Vector3 CenterPoint => Vector3.zero;
        /// <summary>
        /// 可指定范围
        /// </summary>
        public float DesignationRange => this.designationRange;
    }
}
