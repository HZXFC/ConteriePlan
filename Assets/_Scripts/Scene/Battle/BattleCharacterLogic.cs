using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    [Serializable]
    public partial class BattleCharacterLogic // 战斗角色逻辑部分
    {
        private BattleSystem battleSys = null;
        /// <summary>
        /// 是否允许玩家控制旋转
        /// </summary>
        [SerializeField] private bool isCanRotateActiveMember = true;
        /// <summary>
        /// 角色旋转速度
        /// </summary>
        [SerializeField] private float charaRotateSpeed = 20f;
        /// <summary>
        /// 当前活动角色
        /// </summary>
        private CharacterInfoBase activeCharacter = null;

        /// <summary>
        /// 战斗系统
        /// </summary>
        public BattleSystem BattleSystem => this.battleSys;
        /// <summary>
        /// 当前角色的行为状态机
        /// </summary>
        public BattleCharacterBehaviourStateMachine ActiveCharaBehaviourState { get; set; }
        /// <summary>
        /// 当前活动角色
        /// </summary>
        public CharacterInfoBase ActiveCharacter
        {
            get
            {
                if (this.activeCharacter == null) this.activeCharacter = this.BattleSystem.Member.GetActiveMember is CharacterInfoBase ? this.BattleSystem.Member.GetActiveMember as CharacterInfoBase : null;
                return this.activeCharacter;
            }
            set => this.activeCharacter = value;
        }


        /// <summary>
        /// 角色旋转
        /// </summary>
        public void CharaRotate()
        {
            if (this.isCanRotateActiveMember && !InputController.LeftAlt.GetKey)
                this.ActiveCharacter.Transform.Rotate(Vector3.up, InputController.MouseAxes.x * this.charaRotateSpeed * Time.deltaTime);
        }
        /// <summary>
        /// 角色移动
        /// </summary>
        /// <param name="justGravity">仅应用重力</param>
        public void CharaMove(bool justGravity = false)
        {
            if (justGravity)
            {
                this.ActiveCharacter.CharacterController.SimpleMove(Vector3.zero);
                return;
            }
            if (this.ActiveCharacter.MoveAmount > 0)
            {
                // 以自身正方向为准移动物体
                var moveValue = this.ActiveCharacter.Transform.TransformDirection(new Vector3(InputController.AD.GetAxis, 0, InputController.WS.GetAxis));
                moveValue *= Time.deltaTime * this.ActiveCharacter.BattleMoveSpeed;

                // 减去响应的移动量
                this.ActiveCharacter.MoveAmount -= moveValue.sqrMagnitude;
                this.ActiveCharacter.CharacterController.SimpleMove(moveValue);

                GUIManager.GetInstance.GetUISet<MoveQuantityUISet>(nameof(MoveQuantityUISet)).SetMoveQuantity(this, new EventArgs<CharacterInfoBase>(this.activeCharacter));
                //this.BattleSystem.Event.OnCharaDataChanged(this.ActiveCharacter);
            }
        }

        /// <summary>
        /// 当前活动角色执行回合
        /// </summary>
        /// <param name="activeChara">当前活动角色</param>
        /// <returns></returns>
        public IEnumerator ActiveCharaBehaviour(CharacterInfoBase activeChara)
        {
            if (activeChara == null) yield break;
            this.ActiveCharacter = activeChara;

            // 初始化状态
            if (this.ActiveCharaBehaviourState == null) this.ActiveCharaBehaviourState = new BattleCharacterBehaviourStateMachine(this, new CharaAttackMoveAndBehaSelectState());
            if (!(this.ActiveCharaBehaviourState.CurrentState is CharaAttackMoveAndBehaSelectState)) this.ActiveCharaBehaviourState.ChangeState<CharaAttackMoveAndBehaSelectState>();

            while (true)
            {
                // 镜头自由旋转
                this.BattleSystem.Camera.CameraTargetFreeRotate();

                // 当前成员回合已结束
                if (this.ActiveCharaBehaviourState.CurrentState is CharaFinishState)
                {
                    this.ActiveCharaBehaviourState.CurrentState.OnExit();
                    break;
                }
                    

                // 当前角色状态为一般状态

                // 这里将确定角色移动旋转，敌人捕捉，选择技能

                // 执行当前角色状态
                this.ActiveCharaBehaviourState.Execute();



                //Physics.CheckBox;
                //Physics.CheckCapsule;
                //Physics.CheckSphere;
                //Physics.OverlapBox;
                //Physics.OverlapCapsule;
                //Physics.OverlapSphere;
                //Physics.Raycast;
                //Physics.Linecast;
                //Physics.BoxCast;
                //Physics.CapsuleCast;
                //Physics.SphereCast;

                //var attack = activeMember.BattleSkill.Find(skill => skill.SkillName == "Tutor@Attack");
                //if (attack != null) attack.SkillProcess(activeMember, new ICreature[] { activeMember });
                // m.BattleSkill.First().SkillProcess(null, null);
                yield return null;
            }
            
        }

        /// <summary>
        /// 初始化角色逻辑
        /// </summary>
        /// <param name="system"></param>
        public BattleCharacterLogic Initialization(BattleSystem system)
        {
            this.battleSys = system;
            return this;
        }
    }
}
