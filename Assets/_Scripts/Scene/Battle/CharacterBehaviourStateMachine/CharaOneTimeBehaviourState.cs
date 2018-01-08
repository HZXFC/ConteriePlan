//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//namespace ConteriePlan
//{
//    /// <summary>
//    /// 角色一次性行为
//    /// </summary>
//    public class CharaOneTimeBehaviourState : BattleCharacterBehaviourStateBase
//    {
//        private IBattleBehaviour selectedBehaviour = null;
//        /// <summary>
//        /// 可执行次数
//        /// </summary>
//        private int frequency = 0;

//        /// <summary>
//        /// 选择的行为
//        /// </summary>
//        public IBattleBehaviour SelectedBehaviour => this.selectedBehaviour;

//        ///// <summary>
//        ///// 攻击技能
//        ///// </summary>
//        //public void Attack()
//        //{
//        //    if (InputController.LeftClick.GetMouseButtonDown && this.frequency > 0)
//        //    {
//        //        this.frequency--;
//        //        this.selectedBehaviour.Process(this.Machine.CharacterLogic.ActiveCharacter, this.Machine.CharacterLogic.SelectedMembers.ToArray());
//        //    }
//        //}

//        /// <summary>
//        /// 设置行为
//        /// </summary>
//        /// <param name="behaviour"></param>
//        public CharaOneTimeBehaviourState SetBehaviour(IBattleBehaviour behaviour)
//        {
//            this.selectedBehaviour = behaviour;
//            this.frequency = this.selectedBehaviour.Frequency;
//            return this;
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//        public override void OnEnter()
//        {

//        }
//        public override void OnUpdate()
//        {
//            if (InputController.LeftClick.GetMouseButtonDown && this.frequency > 0)
//            {
//                this.frequency--;
//                //this.selectedBehaviour.Process(this.Machine.CharacterLogic.ActiveCharacter, this.Machine.CharacterLogic.SelectedMembers.ToArray());
//                MonoBehaviour.print($"剩余次数：{this.frequency}");
//            }
//        }
//    }
//}
