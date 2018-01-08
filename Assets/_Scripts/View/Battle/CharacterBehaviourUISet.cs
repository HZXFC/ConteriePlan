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
    /// 行为栏
    /// </summary>
    public class CharacterBehaviourUISet : ViewBase
    {
        /// <summary>
        /// 设置行为名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SetBehaviourName(object sender, EventArgs<string[]> args)
        {
            if (args == null) return;
            for (var i = 0; i < 4; i++)
            {
                this.GetUIPart<Text>($"Behaviour{(i + 1).ToString()}Text").text = args.GetData[i];
            }
        }

        public void SetSelectedBehaviour(object sender, EventArgs<int> args)
        {
            var selected = this.GetUIPart<Image>("Selected");
            if (args.GetData == 0)
            {
                selected.rectTransform.SetParent(this.transform, false);
                selected.enabled = false;
            }
            else if (0 < args.GetData && args.GetData < 5)
            {
                selected.rectTransform.SetParent(this.GetUIPart<Text>($"Behaviour{(args.GetData).ToString()}Text").transform, false);
                selected.enabled = true;
            }
        }


        protected override void Awake()
        {
            base.Awake();
        }
    }


}
