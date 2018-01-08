using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 角色攻击移动检测与行为选择
    /// </summary>
    public class CharaAttackMoveAndBehaSelectState : BattleCharacterBehaviourStateBase
    {
        /// <summary>
        /// 选择的行为栏项目
        /// </summary>
        private int selectedBehaItem = 0;

        /// <summary>
        /// 战斗系统
        /// </summary>
        public BattleSystem BattleSystem => this.Machine.CharacterLogic.BattleSystem;
        /// <summary>
        /// 当前活动角色
        /// </summary>
        public CharacterInfoBase ActiveCharacter => this.Machine.CharacterLogic.ActiveCharacter;
        /// <summary>
        /// 角色行为列表
        /// </summary>
        private string[] activeCharaBehaviourList = new string[4] { "", "", "", "" };
        /// <summary>
        /// 选择的行为栏项目
        /// </summary>
        public int SelectedBehaItem
        {
            get => this.selectedBehaItem;
            set
            {
                if (value > 4) value = 4;
                if (value < 0) value = 0;
                if (this.selectedBehaItem != value)
                {
                    this.selectedBehaItem = value;
                    GUIManager.GetInstance.GetUISet<CharacterBehaviourUISet>(nameof(CharacterBehaviourUISet)).SetSelectedBehaviour(this, new EventArgs<int>(this.selectedBehaItem));
                }
            }
        }
        /// <summary>
        /// 角色攻击
        /// </summary>
        public AttackBase CharaAttack => this.ActiveCharacter.Attack;

        /// <summary>
        /// 选择的成员
        /// </summary>
        public List<CreatureInfoBase> SelectedMembers { get; set; }
        /// <summary>
        /// 选择范围特效
        /// </summary>
        public IEffect RangeEffect { get; set; }


        /// <summary>
        /// 改变行为栏项目
        /// </summary>
        public void ChangeBehaBar()
        {
            var selectedCBNum = this.SelectedBehaItem;
            // 输入修改行为
            //if (InputController.Up.GetKeyDown) selectedCBNum--;
            //else if (InputController.Down.GetKeyDown) selectedCBNum++;
            if (InputController.MouseScrollWheel > 0) selectedCBNum--;
            else if (InputController.MouseScrollWheel < 0) selectedCBNum++;

            // 必须选择一个行为
            if (selectedCBNum == 0) selectedCBNum += 1;
            // 若攻击范围内没有敌人，不允许选择攻击行为
            if (this.SelectedMembers.Count == 0 && selectedCBNum == 1)
                selectedCBNum = 2;
            this.SelectedBehaItem = selectedCBNum;

            // 设置行为栏的UI
            if (this.SelectedMembers.Count > 0)// 攻击范围内有敌人                        
                SetActiveCharaBehaviour("攻击");
            else SetActiveCharaBehaviour("攻击(灰色)");
        }

        /// <summary>
        /// 添加被选择的成员
        /// </summary>
        /// <param name="creature"></param>
        public void AddSelectedMember(CreatureInfoBase creature)
        {
            if (creature == null) return;
            this.SelectedMembers.Add(creature);// 添加进列表
            SelectEffect.GetPoolObject.Take().Open(creature.Transform, .5f, 20);// 为被选择的怪物添加选择效果
        }
        /// <summary>
        /// 移除被选择的成员
        /// </summary>
        /// <param name="creature"></param>
        public void RemoveSelectedMember(CreatureInfoBase creature)
        {
            if (creature == null) return;
            // 被选择的对象已脱离攻击范围
            creature.Transform.GetComponentInChildren<SelectEffect>()?.Restore();// 移除选择效果
            this.SelectedMembers.Remove(creature);// 移出选择列表
        }
        /// <summary>
        /// 判断进入角色选择范围的敌人
        /// </summary>
        public void JudgeEnemyInRange()
        {
            var enemys = new List<EnemyInfoBase>();

            if (this.CharaAttack is IDesignationBehaviourType designationBeha)// 角色的普攻为指定型技能
                enemys = CircleRangeDetection(this.ActiveCharacter.Transform, designationBeha.DesignationRange);
            else if (this.ActiveCharacter.Attack is IUniversalBehaviourType)// 活动角色的普攻为万向型技能
            {

            }

            // 判断捕捉到的敌人
            if (enemys.Count > 0)
            {
                foreach (var enemy in enemys)
                {
                    if (this.SelectedMembers.Contains(enemy)) continue;// 已经捕捉到了该敌人，跳过
                    else this.AddSelectedMember(enemy);// 新捕捉到的敌人，添加
                }
            }

            // 判断被选择的对象是否已脱离攻击范围
            for (var i = this.SelectedMembers.Count - 1; i >= 0; i--)
            {
                if (this.SelectedMembers[i] is EnemyInfoBase && !enemys.Contains(this.SelectedMembers[i] as EnemyInfoBase))
                    this.RemoveSelectedMember(this.SelectedMembers[i]);// 被选择的对象已脱离攻击范围
            }
        }

        /// <summary>
        /// 圆形范围检测
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public List<EnemyInfoBase> CircleRangeDetection(Transform owner, float radius)
        {
            // 开启普攻范围指示圈效果
            if (this.RangeEffect == null)
                this.RangeEffect = CircleSelectRangeEffect.GetPoolObject.Take().Open(owner, radius);
            if (this.RangeEffect is CircleSelectRangeEffect == false)
            {
                // 当前攻击指示圈特效并非指定型攻击特效
                this.RangeEffect.Restore();// 销毁当前并添加新的
                this.RangeEffect = CircleSelectRangeEffect.GetPoolObject.Take().Open(owner, radius);
            }

            var enemys = new List<EnemyInfoBase>();
            // 捕捉当前角色可攻击范围内的敌人
            var enemyColliders = Physics.OverlapSphere(owner.position, radius, (1 << 9));
            // 捕捉到的敌人
            foreach (var enemyCollider in enemyColliders)
            {
                var enemy = this.BattleSystem.Member.EnemyList.Find(member => member.Transform.Equals(enemyCollider.transform));
                if (enemy == null || enemy is EnemyInfoBase == false) continue;
                enemys.Add(enemy);
            }
            return enemys;
        }

        /// <summary>
        /// 设置活动角色行为列表
        /// </summary>
        /// <param name="behaviourList"></param>
        public void SetActiveCharaBehaviour(string[] behaviourList)
        {
            if (behaviourList == null || behaviourList.Length != 4) return;
            if (this.activeCharaBehaviourList == null) this.activeCharaBehaviourList = new string[4] { "", "", "", "" };
            var changed = false;
            for (var i = 0; i < 4; i++)
            {
                if (behaviourList[i] != null && this.activeCharaBehaviourList[i] != behaviourList[i])
                {
                    this.activeCharaBehaviourList[i] = behaviourList[i];
                    changed = true;
                }
            }
            if (changed) GUIManager.GetInstance.GetUISet<CharacterBehaviourUISet>(nameof(CharacterBehaviourUISet)).SetBehaviourName(this, new EventArgs<string[]>(this.activeCharaBehaviourList));
            //if (changed) this.BattleSystem.Event.OnCharaBehaviourChanged(this.activeCharaBehaviourList);
        }
        /// <summary>
        /// 设置活动角色行为列表
        /// </summary>
        /// <param name="behaviour1"></param>
        /// <param name="behaviour2"></param>
        /// <param name="behaviour3"></param>
        /// <param name="behaviour4"></param>
        public void SetActiveCharaBehaviour(string behaviour1 = null, string behaviour2 = null, string behaviour3 = null, string behaviour4 = null)
        {
            SetActiveCharaBehaviour(new string[4] { behaviour1, behaviour2, behaviour3, behaviour4 });
        }

        public override bool IsValid()
        {
            // 点击了左键，执行行为
            if (InputController.LeftClick.GetMouseButtonDown)
            {
                if (this.SelectedBehaItem == 2) this.Machine.ChangeState<CharaSkillSelectState>();
                if (this.selectedBehaItem == 4) this.Machine.ChangeState<CharaFinishState>();
                return false;
            }
            return true;
        }
        public override void OnEnter()
        {
            if (this.activeCharaBehaviourList == null) this.activeCharaBehaviourList = new string[4] { "", "", "", "" };
            if (this.SelectedMembers == null) this.SelectedMembers = new List<CreatureInfoBase>();
            SetActiveCharaBehaviour("攻击", "技能", "物品", "防御");

            this.BattleSystem.SetCursor(false, CursorLockMode.Locked);
        }
        public override void OnUpdate()
        {
            //镜头推拉升降
            this.Machine.CharacterLogic.BattleSystem.Camera.MainCameraPos();

            // 行为选择
            ChangeBehaBar();

            // 角色转向，移动
            this.Machine.CharacterLogic.CharaRotate();
            this.Machine.CharacterLogic.CharaMove();
            GUIManager.GetInstance.GetUISet<TestUISet>(nameof(TestUISet)).SetInfo(this, new EventArgs<CharacterInfoBase>(this.ActiveCharacter));

            // 判断进入攻击范围内的敌人
            JudgeEnemyInRange();
        }
    }
}

