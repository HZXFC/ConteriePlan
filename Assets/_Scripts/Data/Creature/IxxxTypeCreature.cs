using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConteriePlan
{
    /// <summary>
    /// 带技能型生物
    /// </summary>
    public interface ISkillTypeCreature
    {
        /// <summary>
        /// 技能列表
        /// </summary>
        List<BattleSkillBase> BattleSkillList { get; }
    }

    /// <summary>
    /// 魔法型生物
    /// </summary>
    public interface IMagicTypeCreature
    {
        /// <summary>
        /// 魔攻
        /// </summary>
        int MagicDamage { get; set; }
        /// <summary>
        /// 魔法防御
        /// </summary>
        int MagicResistance { get; set; }
        /// <summary>
        /// 当前魔法值
        /// </summary>
        int ManaPoint { get; set; }
        /// <summary>
        /// 魔法上限
        /// </summary>
        int ManaPointLimit        { get; set; }

    }

    ///// <summary>
    ///// 魔法型角色
    ///// </summary>
    //public interface IMagicTypeCharacter : IMagicTypeCreature
    //{
    //    /// <summary>
    //    /// 魔攻成长率
    //    /// </summary>
    //    float MagicDamageGrowthRate { get; set; }
    //    /// <summary>
    //    /// 魔法防御成长率
    //    /// </summary>
    //    float MagicResistanceGrowthRate { get; set; }
    //    /// <summary>
    //    /// 魔法值成长率
    //    /// </summary>
    //    float ManaPointGrowthRate { get; set; }
    //}
}
