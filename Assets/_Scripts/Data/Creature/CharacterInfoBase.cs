using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 角色基类
    /// </summary>
    [Serializable]
    public abstract class CharacterInfoBase : CreatureInfoBase, IMagicTypeCreature, ISkillTypeCreature
    {
        // 角色型对象应当继承此类，此类应负责将角色数据存储在本地，并且使用时提供全局唯一对象

        /// <summary>
        /// 基本属性
        /// </summary>
        [SerializeField] private BaseStatesDataes baseDataes;
        /// <summary>
        /// 加成属性
        /// </summary>
        [SerializeField] private AdditionStatesDataes additionDataes;
        /// <summary>
        /// 技能列表
        /// </summary>
        [SerializeField] private List<BattleSkillBase> encSkillList = new List<BattleSkillBase>();

        /// <summary>
        /// 战斗技能列表
        /// </summary>
        public List<BattleSkillBase> BattleSkillList { get => this.encSkillList; set => this.encSkillList = value; }

        #region 角色属性
        /// <summary>
        /// 等级
        /// </summary>
        public virtual int Level { get => this.baseDataes.Level; set => this.baseDataes.Level = value; }
        /// <summary>
        /// 经验值
        /// </summary>
        public virtual int Experience { get => baseDataes.Experience; set => baseDataes.Experience = value; }
        /// <summary>
        /// 非战时移速
        /// </summary>
        public virtual float BaseMovespeed { get => baseDataes.BaseMoveSpeed; set => baseDataes.BaseMoveSpeed = value; }

        /// <summary>
        /// 幸运
        /// </summary>
        public virtual int Lucky { get => baseDataes.Lucky; set => baseDataes.Lucky = value; }
        /// <summary>
        /// 好感度
        /// </summary>
        public virtual int Favorability { get => baseDataes.Favorability; set => baseDataes.Favorability = value; }

        /// <summary>
        /// 生命成长率
        /// </summary>
        public virtual float HealthPointGrowthRate { get => battleDataes.HealthPointGrowthRate; set => battleDataes.HealthPointGrowthRate = value; }
        /// <summary>
        /// 当前魔法值
        /// </summary>
        public virtual int ManaPoint { get => battleDataes.ManaPoint; set => battleDataes.ManaPoint = value; }
        /// <summary>
        /// 魔法上限
        /// </summary>
        public virtual int ManaPointLimit { get => battleDataes.ManaPointLimit; set => battleDataes.ManaPointLimit = value; }
        /// <summary>
        /// 魔法值成长率
        /// </summary>
        public virtual float ManaPointGrowthRate { get => battleDataes.ManaPointGrowthRate; set => battleDataes.ManaPointGrowthRate = value; }

        /// <summary>
        /// 敏捷成长率
        /// </summary>
        public virtual float AgileGrowthRate { get => battleDataes.AgileGrowthRate; set => battleDataes.AgileGrowthRate = value; }

        ///// <summary>
        ///// 战斗时移动速度
        ///// </summary>
        //public virtual float BattleMoveSpeed { get => battleDataes.BattleMoveSpeed; set => battleDataes.BattleMoveSpeed = value; }
        ///// <summary>
        ///// 移动量上限
        ///// </summary>
        //public virtual float MoveAmountLimit { get => battleDataes.MoveAmountLimit; set => battleDataes.MoveAmountLimit = value; }
        /// <summary>
        /// 命中
        /// </summary>
        public virtual int Hit { get => battleDataes.Hit; set => battleDataes.Hit = value; }
        /// <summary>
        /// 命中成长率
        /// </summary>
        public virtual float HitGrowthRate { get => battleDataes.HitGrowthRate; set => battleDataes.HitGrowthRate = value; }
        /// <summary>
        /// 元气
        /// </summary>
        public virtual int Genki { get => battleDataes.Genki; set => battleDataes.Genki = value; }
        /// <summary>
        /// 元气成长率
        /// </summary>
        public virtual float GenkiGrowthRate { get => battleDataes.GenkiGrowthRate; set => battleDataes.GenkiGrowthRate = value; }

        /// <summary>
        /// 物攻成长率
        /// </summary>
        public virtual float AttackDamageGrowthRate { get => battleDataes.AttackDamageGrowthRate; set => battleDataes.AttackDamageGrowthRate = value; }
        ///// <summary>
        ///// 护甲
        ///// </summary>
        //public virtual int Armor { get => battleDataes.Armor; set => battleDataes.Armor = value; }
        /// <summary>
        /// 护甲成长率
        /// </summary>
        public virtual float ArmorGrowthRate { get => battleDataes.ArmorGrowthRate; set => battleDataes.ArmorGrowthRate = value; }
        /// <summary>
        /// 魔攻
        /// </summary>
        public virtual int MagicDamage { get => battleDataes.MagicDamage; set => battleDataes.MagicDamage = value; }
        /// <summary>
        /// 魔攻成长率
        /// </summary>
        public virtual float MagicDamageGrowthRate { get => battleDataes.MagicDamageGrowthRate; set => battleDataes.MagicDamageGrowthRate = value; }
        /// <summary>
        /// 魔法防御
        /// </summary>
        public virtual int MagicResistance { get => battleDataes.MagicDefense; set => battleDataes.MagicDefense = value; }
        /// <summary>
        /// 魔法防御成长率
        /// </summary>
        public virtual float MagicResistanceGrowthRate { get => battleDataes.MagicDefenseGrowthRate; set => battleDataes.MagicDefenseGrowthRate = value; }

        #endregion

    }



}
