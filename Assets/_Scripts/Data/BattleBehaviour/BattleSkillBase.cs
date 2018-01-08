using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    [Serializable]
    public abstract class BattleSkillBase : ScriptableObject, IBattleSkill
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


        public virtual void Process(CreatureInfoBase applicant, CreatureInfoBase[] recipients)
        {
            MonoBehaviour.print($"{this.Name} Process已执行:施加者：{applicant.Transform.name} 接受者数量：{recipients.Length}");
            if (recipients.Length == 0) return;
            foreach (var recipient in recipients)
            {
                MonoBehaviour.print($"{this.Name} Effect已执行:施加者：{applicant.Transform.name} 接受者：{recipient.Transform.name}");
            }
        }
    }
}
