using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    [CreateAssetMenu(fileName = "@TestSkill1", menuName = "Skill/@TestSkill1")]
    public class TutorTestSkill : BattleSkillBase, IOneTimeBehaviourType, IDesignationBehaviourType, IMonomerBehaviourType
    {
        /// <summary>
        /// 可指定范围
        /// </summary>
        private float designationRange = 2f;

        /// <summary>
        /// 可指定范围
        /// </summary>
        public float DesignationRange => this.designationRange;
    }
}
