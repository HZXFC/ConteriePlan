using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 怪物基类
    /// </summary>
    [Serializable]
    public abstract class EnemyInfoBase : CreatureInfoBase
    {
        /// <summary>
        /// 怪物编号
        /// </summary>
        [SerializeField]
        private int enemyNum;

        /// <summary>
        /// 怪物编号(同一类型的怪物复数存在时以编号区分)
        /// </summary>
        public int EnemyNum { get => this.enemyNum; set => this.enemyNum = value; }

    }
}
