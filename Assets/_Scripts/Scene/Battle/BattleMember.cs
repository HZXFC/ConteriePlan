using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 战斗行动列表
    /// </summary>
    [Serializable]
    public class BattleMember
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        [SerializeField] private List<CharacterInfoBase> charaList = new List<CharacterInfoBase>(4) { null, null, null, null };
        /// <summary>
        /// 敌人列表
        /// </summary>
        [SerializeField] private List<EnemyInfoBase> enemyList = new List<EnemyInfoBase>();

        ///// <summary>
        ///// 行动值排序列表
        ///// </summary>
        //private List<CreatureInfoBase> actionMemberList = new List<CreatureInfoBase>();


        /// <summary>
        /// 成员列表（按照行动值排序）
        /// </summary>
        public List<CreatureInfoBase> ActionMemberList { get; set; } = new List<CreatureInfoBase>();
        /// <summary>
        /// 获取当前活动成员
        /// </summary>
        public CreatureInfoBase GetActiveMember
        {
            get
            {
                if (this.ActionMemberList == null || !(this.ActionMemberList.Count > 0)) return null;
                return this.ActionMemberList[0];
            }
        }
        ///// <summary>
        ///// 队长
        ///// </summary>
        //public ICharacter Leader
        //{
        //    get => (this.charaList != null && this.charaList.Count == 4) ? this.charaList[0] : null;
        //    set => this.charaList[0] = value;
        //}
        ///// <summary>
        ///// 角色1
        ///// </summary>
        //public ICharacter Chara1
        //{
        //    get => (this.charaList != null && this.charaList.Count == 4) ? this.charaList[1] : null;
        //    set => this.charaList[1] = value;
        //}
        ///// <summary>
        ///// 角色2
        ///// </summary>
        //public ICharacter Chara2
        //{
        //    get => (this.charaList != null && this.charaList.Count == 4) ? this.charaList[2] : null;
        //    set => this.charaList[2] = value;
        //}
        ///// <summary>
        ///// 角色3
        ///// </summary>
        //public ICharacter Chara3
        //{
        //    get => (this.charaList != null && this.charaList.Count == 4) ? this.charaList[3] : null;
        //    set => this.charaList[3] = value;
        //}
        public List<CharacterInfoBase> CharaList { get => this.charaList; set => this.charaList = value; }
        /// <summary>
        /// 敌人列表
        /// </summary>
        public List<EnemyInfoBase> EnemyList { get => this.enemyList; set => this.enemyList = value; }

        /// <summary>
        /// 生成行动列表，并返回行动值最高成员
        /// </summary>
        /// <returns></returns>
        public CreatureInfoBase GenerateActionList()
        {
            if (ActionMemberList == null) this.ActionMemberList = new List<CreatureInfoBase>();
            else this.ActionMemberList.Clear();

            if (this.charaList == null || this.charaList.Count <= 0
                || this.EnemyList == null || this.EnemyList.Count <= 0) return null;
            foreach (var c in this.charaList)
            {
                if (c == null) continue;
                this.ActionMemberList.Add(c);
            }
            foreach (var e in this.EnemyList)
            {
                if (e == null) continue;
                this.ActionMemberList.Add(e);
            }
            // 根据行动值 降序 排序列表
            this.ActionMemberList.Sort((CreatureInfoBase member1, CreatureInfoBase member2) => -member1.ActionPower.CompareTo(member2.ActionPower));
            return this.ActionMemberList.Count > 0 ? this.ActionMemberList.First() : null;
        }
        /// <summary>
        /// 刷新行动列表，并返回行动值最高成员
        /// </summary>
        public CreatureInfoBase RefreshActionList()
        {
            // 当前方案：所有成员当前行动值加上敏捷后，排序
            // 然后行动值由大到小进行排列

            if (this.ActionMemberList == null || !(this.ActionMemberList.Count > 0))
            {
                GenerateActionList();
                if (this.ActionMemberList == null || !(this.ActionMemberList.Count > 0)) return null;
            }
            // 将成员敏捷添加到其行动值中
            foreach (var m in this.ActionMemberList)
            {
                m.ActionPower += m.Agile;
            }
            // 根据行动值 降序 排序列表
            this.ActionMemberList.Sort((CreatureInfoBase member1, CreatureInfoBase member2) => -member1.ActionPower.CompareTo(member2.ActionPower));
            return this.ActionMemberList.Count > 0 ? this.ActionMemberList.First() : null;
        }

        /// <summary>
        /// 初始化活动成员
        /// </summary>
        /// <param name="creature"></param>
        public void InitializationActiveMember(CreatureInfoBase creature = null)
        {
            if (creature == null) creature = this.GetActiveMember;
            if (creature == null) return;

            creature.MoveAmount = creature.MoveAmountLimit;// 重置移动量
        }
                
    }
}
