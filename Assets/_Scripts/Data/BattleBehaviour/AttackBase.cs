using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 普通攻击
    /// </summary>
    [Serializable]
    public abstract class AttackBase : ScriptableObject, IBattleBehaviour
    {
#pragma warning disable 
        /// <summary>
        /// 行为名
        /// </summary>
        [SerializeField]
        private string encName;
        /// <summary>
        /// 描述
        /// </summary>
        [SerializeField]
        private string description;
        /// <summary>
        /// 可执行次数
        /// </summary>
        [SerializeField]
        [Tooltip("可执行次数")]
        private int frequency;

        /// <summary>
        /// 名
        /// </summary>
        public string Name => this.encName;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description => this.description;
        /// <summary>
        /// 可执行次数
        /// </summary>
        public int Frequency { get => this.frequency; }

        ///// <summary>
        ///// 可指定范围（当攻击为指定时，该半径范围标识可被指定到的敌人）
        ///// </summary>
        //[SerializeField]
        //[Tooltip("可指定范围")]
        //private float designationRange;
        ///// <summary>
        ///// 线性攻击宽度
        ///// </summary>
        //[SerializeField]
        //[Tooltip("线性攻击宽度")]
        //private float edgeWidth;
        ///// <summary>
        ///// 群体攻击参数
        ///// </summary>
        //[SerializeField]
        //[Tooltip("群体攻击参数")]
        //private GroupSkill groupSkill;


        ///// <summary>
        ///// 可指定范围
        ///// </summary>
        //public float DesignationRange { get => designationRange; set => designationRange = value; }
        ///// <summary>
        ///// 线性攻击宽度
        ///// </summary>
        //public float EdgeWidth { get => edgeWidth; set => edgeWidth = value; }
        ///// <summary>
        ///// 群体攻击参数
        ///// </summary>
        //public GroupSkill GroupSkill { get; set; }

        //public override void Effect(CreatureInfoBase applicant, CreatureInfoBase recipient)
        //{
        //    MonoBehaviour.print($"Attack Effect已执行:施加者：{applicant.Transform.name} 接受者：{recipient.Transform.name}");
        //}

        public virtual void Process(CreatureInfoBase applicant, CreatureInfoBase[] recipients)
        {
            MonoBehaviour.print($"Attack Process已执行:施加者：{applicant.Transform.name} 接受者数量：{recipients.Length}");
            if (recipients.Length == 0) return;
            foreach (var recipient in recipients)
            {
                MonoBehaviour.print($"Attack Effect已执行:施加者：{applicant.Transform.name} 接受者：{recipient.Transform.name}");
            }
        }


        ///// <summary>
        ///// 创建一个普通攻击
        ///// </summary>
        ///// <param name="owner">持有者</param>
        ///// <param name="name">技能名称</param>
        ///// <param name="description">技能描述</param>
        ///// <param name="skillBehaviourType">技能施放类型</param>
        ///// <param name="attackFrequency">可攻击次数</param>
        ///// <param name="designationRange">若攻击为指定，其可选取敌人的范围</param>
        ///// <param name="edgeWidth">若技能为线性，其线性宽度</param>
        ///// <param name="groupSkill">若技能为群体，其群体技能参数</param>
        ///// <returns></returns>
        //public static AttackSkill CreateInstance(ICreature owner, string name, string description, SkillBehaviourType skillBehaviourType, int attackFrequency, float designationRange, float edgeWidth, GroupSkill groupSkill)
        //{
        //    //var attack = BattleSkillBase.GetBattleSkill(name, owner) as AttackSkill;
        //    //if (attack != null) return attack;
        //    //attack = Resources.Load($"{name}") as AttackSkill;
        //    var attack = ScriptableObject.CreateInstance<AttackSkill>();
        //    attack.SetBattleSkill(owner, skillBehaviourType, name, description);
        //    attack.AttackFrequency = attackFrequency;
        //    attack.DesignationRange = designationRange;
        //    attack.EdgeWidth = edgeWidth;
        //    attack.GroupSkill = groupSkill;
        //    return attack;
        //}
    }
}
