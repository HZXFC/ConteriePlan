using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 选择技能状态
    /// </summary>
    public class CharaSkillSelectState : BattleCharacterBehaviourStateBase
    {
        /// <summary>
        /// （封装）选择的角色技能编号在显示列表下
        /// </summary>
        private int encSelectedNumInShowList = 1;
        /// <summary>
        /// 技能显示列表
        /// </summary>
        private List<int> showSkillList = new List<int>();

        /// <summary>
        /// 显示技能上限
        /// </summary>
        private int displaySkillLimit => SkillListUISet.DISPLAY_SKILL_LIMIT;
        /// <summary>
        /// 选择的列表项
        /// </summary>
        private int selectedNumInShowList
        {
            get => this.encSelectedNumInShowList;
            set
            {
                if (value < 0) value = 0;
                if (value >= this.charaSkill.Count) value = this.charaSkill.Count - 1;
                if (value >= this.displaySkillLimit) value = this.displaySkillLimit - 1;
                this.encSelectedNumInShowList = value;
            }
        }
        /// <summary>
        /// 当前活动角色
        /// </summary>
        private CharacterInfoBase activeCharacter => this.Machine.CharacterLogic.ActiveCharacter;
        /// <summary>
        /// 角色技能列表
        /// </summary>
        private List<BattleSkillBase> charaSkill
        {
            get
            {
                if (this.activeCharacter.BattleSkillList == null) this.activeCharacter.BattleSkillList = new List<BattleSkillBase>();
                return this.activeCharacter.BattleSkillList;
            }
        }
        /// <summary>
        /// 选择的角色技能
        /// </summary>
        private IBattleSkill selectedSkill => this.charaSkill[this.showSkillList[this.selectedNumInShowList]];


        /// <summary>
        /// 列表上滚
        /// </summary>
        public void RoolOn()
        {
            if (this.charaSkill.Count < 2) return;// 没有或只有1个技能
            if (this.selectedNumInShowList == 0)
            {
                // 需要上滚列表
                if (!(this.showSkillList.Contains(0) || this.showSkillList.Contains(-1)))// 列表如果没有到顶
                {
                    this.showSkillList.RemoveAt(this.showSkillList.Count - 1);// 移除最后一项
                    this.showSkillList.InsertToFirst(this.showSkillList.First() - 1);// 添加到第一项
                }
            }
            else this.selectedNumInShowList--;// 选择项-1
            RefreshShowList();
        }
        /// <summary>
        /// 列表下滚
        /// </summary>
        public void RoolDown()
        {
            if (this.charaSkill.Count < 2) return;// 没有或只有1个技能
            if (this.selectedNumInShowList == this.displaySkillLimit - 1)
            {
                // 需要下滚列表
                if (!this.showSkillList.Contains(this.charaSkill.Count - 1) && !this.showSkillList.Contains(-1))// 列表如果没有到底
                {
                    this.showSkillList.RemoveAt(0);// 移除第一项
                    this.showSkillList.Add(this.showSkillList.Last() + 1);// 添加到最后一项
                }
            }
            else if (this.selectedNumInShowList < this.charaSkill.Count - 1)
                this.selectedNumInShowList++;
            RefreshShowList();
        }
        /// <summary>
        /// 刷新显示列表
        /// </summary>
        private void RefreshShowList()
        {
            var skillList = new string[this.displaySkillLimit];// 显示的文字列表
            for (var i = 0; i < this.displaySkillLimit; i++)
            {
                if (this.showSkillList[i] == -1 || this.showSkillList[i] >= this.charaSkill.Count) skillList[i] = "";
                else skillList[i] = this.charaSkill[this.showSkillList[i]].Name;
            }
            GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).SetSkillBar(this, new EventArgs<string[], int>(skillList, this.selectedNumInShowList));
        }

        //private int SetSkillNum(int num)
        //{
        //    if (num < 0) return this.selectedSkillNumInShowList;// 输入错误
        //    if (num > this.charaSkillCount - 1) num = this.charaSkillCount - 1;// 编号不超过角色拥有的技能数量
        //    if (num == this.selectedSkillNumInShowList) return num;// 无更新

        //    if (!this.showSkillList.Contains(num))
        //    {
        //        if (num > this.selectedSkillNumInShowList)// 下滚
        //        {
        //            this.showSkillList.RemoveAt(0);
        //            this.showSkillList.Add(num);
        //        }
        //        else if (num < this.selectedSkillNumInShowList)// 上滚
        //        {
        //            this.showSkillList.RemoveAt(this.showSkillList.Count - 1);
        //            this.showSkillList.InsertToFirst(num);
        //        }
        //        // 更新列表显示
        //        var skillList = new string[this.displaySkillLimit];
        //        for (int i = 0; i < this.displaySkillLimit; i++)
        //        {
        //            skillList[i] = this.activeCharacter.BattleSkill[this.showSkillList[i]].BehaviourName;
        //        }
        //        GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).SetSkillName(this, new EventArgs<string[]>(skillList));
        //    }
        //    GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).ChangeSkill(this, new EventArgs<int>(num % this.displaySkillLimit));
        //    return num;
        //}
        ///// <summary>
        ///// 设置技能编号
        ///// </summary>
        ///// <param name="num"></param>
        //public int SetSkillNum(int num)
        //{
        //    if (num < 0) return this.selectedSkillNum;// 输入错误
        //    if (num > this.charaSkillCount - 1) num = this.charaSkillCount - 1;// 编号不超过角色拥有的技能数量
        //    if (num == this.selectedSkillNum) return num;// 无更新

        //    var page = (num / this.displaySkillLimit) + 1;// 技能在列表的页面
        //    var disPos = num % this.displaySkillLimit;// 技能在列表的显示位置
        //    MonoBehaviour.print($"{page},{disPos}");
        //    if (page != this.currentPage)
        //    {
        //        // 更新页面技能显示
        //        this.currentPage = page;
        //        var skillList = new string[this.displaySkillLimit];
        //        for (int i = 0; i < this.displaySkillLimit; i++)
        //        {
        //            var itemNum = (this.currentPage - 1) * this.displaySkillLimit + i;
        //            if (itemNum < this.charaSkillCount)
        //                skillList[i] = this.activeCharacter.BattleSkill[itemNum].BehaviourName;
        //            else skillList[i] = "";
        //        }
        //        GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).SetSkillName(this, new EventArgs<string[]>(skillList));
        //    }

        //    GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).ChangeSkill(this, new EventArgs<int>(disPos));
        //    return num;
        //}

        public override bool IsValid()
        {
            // 点击了右键,取消
            if (InputController.RightClick.GetMouseButtonDown)
            {
                this.Machine.ChangeState<CharaAttackMoveAndBehaSelectState>();
                return false;
            }
            // 点击了左键，执行
            else if (InputController.LeftClick.GetMouseButtonDown)
            {
                //if (this.selectedSkill is IOneTimeBehaviourType)
                //{
                //    this.Machine.ChangeState<CharaOneTimeBehaviourState>().SetBehaviour(this.selectedSkill);
                //}
                return false;
            }
            return true;
        }
        public override void OnEnter()
        {
            this.selectedNumInShowList = 0;// 重置选择技能编号
            this.showSkillList = new List<int>();// 重置技能显示列表
            for (int i = 0; i < this.displaySkillLimit; i++)
            {
                if (i >= this.charaSkill.Count) this.showSkillList.Add(-1);// 可显示的列表项多余技能数量，以-1填充
                else this.showSkillList.Add(i);
            }
            RefreshShowList();// 第一次更新列表

            // 开启列表
            GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).SetEnabled(this, new EventArgs<bool>(true));
        }
        public override void OnExit()
        {
            // 关闭列表
            GUIManager.GetInstance.GetUISet<SkillListUISet>(nameof(SkillListUISet)).SetEnabled(this, new EventArgs<bool>(false));
        }
        public override void OnUpdate()
        {
            // 输入修改行为
            if (InputController.MouseScrollWheel > 0 || InputController.WS.GetPositiveButtonDown) RoolOn();
            else if (InputController.MouseScrollWheel < 0 || InputController.WS.GetNegativeButtonDown) RoolDown();

        }


    }
}
