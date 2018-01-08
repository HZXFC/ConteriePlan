using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    public class CharaFinishState : BattleCharacterBehaviourStateBase
    {
        public override bool IsValid()
        {
            return true;
        }
        public override void OnUpdate()
        {

        }
        public override void OnExit()
        {
            this.Machine.CharacterLogic.ActiveCharacter.ActionPower = 0;// 清空行动力
        }
    }
}
