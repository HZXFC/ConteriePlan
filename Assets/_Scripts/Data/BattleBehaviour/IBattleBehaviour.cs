using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 战斗行为
    /// </summary>
    public interface IBattleBehaviour
    {
        /// <summary>
        /// 行为名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 可执行次数
        /// </summary>
        int Frequency { get; }

        /// <summary>
        /// 执行行为
        /// </summary>
        /// <param name="applicant">施加者</param>
        /// <param name="recipients">接受者组</param>
        void Process(CreatureInfoBase applicant, CreatureInfoBase[] recipients);
    }

    /// <summary>
    /// 战斗技能
    /// </summary>
    public interface IBattleSkill : IBattleBehaviour
    {

    }


    /// <summary>
    /// 一次性行为
    /// </summary>
    public interface IOneTimeBehaviourType
    {

    }
    /// <summary>
    /// 分步式行为
    /// </summary>
    public interface IStepByStepBehaviourType
    {

    }
    /// <summary>
    /// 万向行为
    /// </summary>
    public interface IUniversalBehaviourType
    {

    }
    /// <summary>
    /// 指定行为
    /// </summary>
    public interface IDesignationBehaviourType
    {
        /// <summary>
        /// 可指定范围
        /// </summary>
        float DesignationRange { get; }
    }
    /// <summary>
    /// 单体行为
    /// </summary>
    public interface IMonomerBehaviourType
    {

    }
    /// <summary>
    /// 群体行为
    /// </summary>
    public interface IGroupBehaviourType
    {
        /// <summary>
        /// 是否吸附施放者
        /// </summary>
        bool IsAdsorption { get; }
        /// <summary>
        /// 中心点（相对于持有者的位置）
        /// </summary>
        Vector3 CenterPoint { get; }

    }
}
