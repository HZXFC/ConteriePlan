using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ConteriePlan
{
    /// <summary>
    /// 移动量UI
    /// </summary>
    [Serializable]
    public class MoveQuantityUISet : ViewBase
    {
        public void SetMoveQuantity(object sender, EventArgs<CharacterInfoBase> args)
        {
            if (args == null) return;
            this.GetUIPart<Image>("MoveQuantityBar").fillAmount = args.GetData.MoveAmount / args.GetData.MoveAmountLimit;
        }

        protected override void Awake()
        {
            base.Awake();            
        }
    }
}
