using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 导师
    /// </summary>
    [CreateAssetMenu(fileName = "TutorInfo", menuName = "Character/TutorInfo")]
    [Serializable]
    public class TutorInfo : CharacterInfoBase
    {
        [Obsolete("该属性对子类不可见", true)]
        public new int Favorability { get => base.Favorability; set => base.Favorability = value; }


    }
}
