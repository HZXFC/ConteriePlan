using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ConteriePlan
{
    /// <summary>
    /// 技能栏UI集
    /// </summary>
    public class SkillListUISet : ViewBase
    {
        /// <summary>
        /// 显示技能数量上限
        /// </summary>
        public const int DISPLAY_SKILL_LIMIT = 6;


        /// <summary>
        /// 改变技能栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SetSkillBar(object sender, EventArgs<string[],int> args)
        {
            if (args == null) return;
            if (args.GetData1.Length == 0 || args.GetData1.Length > DISPLAY_SKILL_LIMIT) return;
            for (var i = 0; i < DISPLAY_SKILL_LIMIT; i++)
            {
                if (args.GetData1[i] == null) continue;
                this.GetUIPart<Text>($"Skill{(i + 1).ToString()}").text = args.GetData1[i];
            }
            
            if (args.GetData2 < 0 || args.GetData2 > DISPLAY_SKILL_LIMIT - 1) return;
            this.GetUIPart<Image>("Selected").rectTransform.SetParent(
               this.GetUIPart<Text>($"Skill{(args.GetData2 + 1).ToString()}").transform, false);
        }
    }
}
