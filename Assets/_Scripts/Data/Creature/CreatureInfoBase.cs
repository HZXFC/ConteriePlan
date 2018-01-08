using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConteriePlan
{
    [Serializable]
    public abstract class CreatureInfoBase : ScriptableObject
    {
        /// <summary>
        /// 预制体
        /// </summary>
        [SerializeField] private Transform prefab = null;
        /// <summary>
        /// 战斗属性
        /// </summary>
        [SerializeField] protected BattleStatesDataes battleDataes;
        /// <summary>
        /// 攻击技能
        /// </summary>
        [SerializeField] private AttackBase attack = null;

        /// <summary>
        /// 场景中的游戏对象
        /// </summary>
        private Transform transform = null;
        /// <summary>
        /// 游戏对象的角色控制器组件
        /// </summary>
        private CharacterController characterController = null;

        /// <summary>
        /// 场景对象
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (this.transform == null)
                {
                    this.transform = Instantiate(this.prefab);
                    this.transform.name = this.prefab.name;
                }
                return this.transform;
            }
            set => this.transform = value;
        }
        /// <summary>
        /// 角色控制器
        /// </summary>
        public CharacterController CharacterController
        {
            get
            {
                if (this.characterController == null) this.characterController = this.transform.GetComponent<CharacterController>();
                return this.characterController;
            }
        }
        /// <summary>
        /// 攻击技能
        /// </summary>
        public AttackBase Attack { get => this.attack; set => this.attack = value; }


        #region 战斗临时属性
        /// <summary>
        /// 行动力
        /// </summary>
        public int ActionPower { get; set; }
        /// <summary>
        /// 移动量
        /// </summary>
        public float MoveAmount { get; set; }
        #endregion

        /// <summary>
        /// 当前生命值
        /// </summary>
        public int HealthPoint { get => battleDataes.HealthPoint; set => battleDataes.HealthPoint = value; }
        /// <summary>
        /// 生命上限
        /// </summary>
        public int HealthPointLimit { get => battleDataes.HealthPointLimit; set => battleDataes.HealthPointLimit = value; }
        /// <summary>
        /// 物攻
        /// </summary>
        public int AttackDamage { get => battleDataes.AttackDamage; set => battleDataes.AttackDamage = value; }
        /// <summary>
        /// 护甲
        /// </summary>
        public int Armor { get => battleDataes.Armor; set => battleDataes.Armor = value; }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agile { get => battleDataes.Agile; set => battleDataes.Agile = value; }
        /// <summary>
        /// 战斗时移动速度
        /// </summary>
        public float BattleMoveSpeed { get => battleDataes.BattleMoveSpeed; set => battleDataes.BattleMoveSpeed = value; }
        /// <summary>
        /// 移动量上限
        /// </summary>
        public float MoveAmountLimit { get => battleDataes.MoveAmountLimit; set => battleDataes.MoveAmountLimit = value; }

        /// <summary>
        /// 暴击伤害倍数(单位%)
        /// </summary>
        public int CritDamageMagnification { get => battleDataes.CritDamageMagnification; set => battleDataes.CritDamageMagnification = value; }
        /// <summary>
        /// 暴击概率(单位%)
        /// </summary>
        public int CritProbability { get => battleDataes.CritProbability; set => battleDataes.CritProbability = value; }
    }

    ///// <summary>
    ///// 战斗状态
    ///// </summary>
    //public enum BattleStatesType
    //{
    //    /// <summary>
    //    /// 一般状态（该成员进入场景后未释放任何技能与物品）
    //    /// </summary>
    //    NORMAL,
    //    /// <summary>
    //    /// 一次性行为执行中
    //    /// </summary>
    //    ONE_TIME_BEHAVIOUR,
    //    /// <summary>
    //    /// 分步式行为执行中
    //    /// </summary>
    //    SBS_BEHAVIOUR,
    //    /// <summary>
    //    /// 当前回合行为结束
    //    /// </summary>
    //    OVER,
    //}

    /// <summary>
    /// 基本属性
    /// </summary>
    [Serializable]
    public struct BaseStatesDataes
    {
        /// <summary>
        /// 等级
        /// </summary>
        [Tooltip("等级")] public int Level;
        /// <summary>
        /// 经验值
        /// </summary>
        [Tooltip("经验值")] public int Experience;
        /// <summary>
        /// 非战斗时移速
        /// </summary>
        [Tooltip("非战斗时移速")] public float BaseMoveSpeed;

        /// <summary>
        /// 幸运
        /// </summary>
        [Tooltip("幸运")] public int Lucky;
        /// <summary>
        /// 好感度
        /// </summary>
        [Tooltip("好感度")] public int Favorability;
    }

    /// <summary>
    /// 战斗属性
    /// </summary>
    [Serializable]
    public struct BattleStatesDataes
    {
        /// <summary>
        /// 当前生命值
        /// </summary>
        [Tooltip("当前生命值")] public int HealthPoint;
        /// <summary>
        /// 生命上限
        /// </summary>
        [Tooltip("生命上限")] public int HealthPointLimit;
        /// <summary>
        /// 生命成长率
        /// </summary>
        [Tooltip("生命成长率")] public float HealthPointGrowthRate;

        /// <summary>
        /// 当前魔法值
        /// </summary>
        [Tooltip("当前魔法值")] public int ManaPoint;
        /// <summary>
        /// 魔法上限
        /// </summary>
        [Tooltip("魔法上限")] public int ManaPointLimit;
        /// <summary>
        /// 魔法值成长率
        /// </summary>
        [Tooltip("魔法值成长率")] public float ManaPointGrowthRate;

        /// <summary>
        /// 移动量上限
        /// </summary>
        [Tooltip("移动量上限")] public float MoveAmountLimit;
        /// <summary>
        /// 移动量成长率
        /// </summary>
        [Tooltip("移动量成长率")] public float MoveAmountGrowthRate;

        /// <summary>
        /// 战斗时移速
        /// </summary>
        [Tooltip("战斗时移速")] public float BattleMoveSpeed;

        /// <summary>
        /// 敏捷
        /// </summary>
        [Tooltip("敏捷")] public int Agile;
        /// <summary>
        /// 敏捷成长率
        /// </summary>
        [Tooltip("敏捷成长率")] public float AgileGrowthRate;

        /// <summary>
        /// 命中
        /// </summary>
        [Tooltip("命中")] public int Hit;
        /// <summary>
        /// 命中成长率
        /// </summary>
        [Tooltip("命中成长率")] public float HitGrowthRate;

        /// <summary>
        /// 元气
        /// </summary>
        [Tooltip("元气")] public int Genki;
        /// <summary>
        /// 元气成长率
        /// </summary>
        [Tooltip("元气成长率")] public float GenkiGrowthRate;


        /// <summary>
        /// 物攻
        /// </summary>
        [Tooltip("物攻")] public int AttackDamage;
        /// <summary>
        /// 物攻成长率
        /// </summary>
        [Tooltip("物攻成长率")] public float AttackDamageGrowthRate;

        /// <summary>
        /// 护甲
        /// </summary>
        [Tooltip("护甲")] public int Armor;
        /// <summary>
        /// 护甲成长率
        /// </summary>
        [Tooltip("护甲成长率")] public float ArmorGrowthRate;

        /// <summary>
        /// 魔攻
        /// </summary>
        [Tooltip("魔攻")] public int MagicDamage;
        /// <summary>
        /// 魔攻成长率
        /// </summary>
        [Tooltip("魔攻成长率")] public float MagicDamageGrowthRate;

        /// <summary>
        /// 魔法防御
        /// </summary>
        [Tooltip("魔法防御")] public int MagicDefense;
        /// <summary>
        /// 魔法防御成长率
        /// </summary>
        [Tooltip("魔法防御成长率")] public float MagicDefenseGrowthRate;


        /// <summary>
        /// 暴击伤害倍数
        /// </summary>
        [Tooltip("暴击伤害倍数(单位%)")] public int CritDamageMagnification;
        /// <summary>
        /// 暴击概率
        /// </summary>
        [Tooltip("暴击概率(单位%)")] public int CritProbability;
    }

    /// <summary>
    /// 加成属性
    /// </summary>
    [Serializable]
    public struct AdditionStatesDataes
    {
        /// <summary>
        /// 体质
        /// </summary>
        [Tooltip("体质")] public int Somatoplasm;
        /// <summary>
        /// 智慧
        /// </summary>
        [Tooltip("智慧")] public int Wisdom;
    }

}