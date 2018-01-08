using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : SettingBase<GameSettings>
    {
        
        /// <summary>
        /// 图形设定
        /// </summary>
        public GraphicSettings Graphics;
    }

    /// <summary>
    /// 图形设置
    /// </summary>
    [Serializable]
    public class GraphicSettings
    {
        [UnityEngine.Serialization.FormerlySerializedAs("ScreenWidth")]
        [SerializeField]
        private int screenWidth = 1920;
        [UnityEngine.Serialization.FormerlySerializedAs("ScreenHeight")]
        [SerializeField]
        private int screenHeight = 1080;

        public int ScreenWidth
        {
            get
            { return this.screenWidth; }
            set
            { this.screenWidth = value; }
        }
        public int ScreenHeight
        {
            get
            { return this.screenHeight; }
            set
            { this.screenHeight = value; }
        }
    }
}
