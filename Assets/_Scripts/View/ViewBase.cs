using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace ConteriePlan
{
    /// <summary>
    /// UI集基类
    /// UI集是整个窗口中的一部分UI的集合，每个UI集由一些UIBehaviour的实例构成。
    /// 它管理着它的所有子类，并提供自己的事件与行为。
    /// </summary>
    public abstract class ViewBase : MonoBehaviour
    {
        /*
         * UI 封装原理：将图片、文本、按钮这些部件放到一个空对象下制成预制体。
         * 使用时，添加事件然后放到 UI 摄像机下。停用时，移除事件后放到 Cache 中。
         * 
         * UI 监听事件：在某个子对象下添加EventTrigger,然后添加父对象脚本中的事件。
         * # 无需监听事件的部件关掉Raycaster
         */
        /// <summary>
        /// 在检视面板上拖入子部件
        /// </summary>
        [SerializeField]
        private List<UIBehaviour> loadList = new List<UIBehaviour>();

        /// <summary>
        /// 子部件清单(需要手动建立对象)
        /// </summary>
        private Dictionary<string, UIBehaviour> partList = null;


        /// <summary>
        /// 获取UI部件
        /// </summary>
        /// <typeparam name="T">部件类型</typeparam>
        /// <param name="name">部件名称</param>
        /// <returns></returns>
        public T GetUIPart<T>(string name) where T : UIBehaviour => this.partList[name] as T;

        /// <summary>
        /// 设置UI启用于关闭
        /// </summary>
        public virtual void SetEnabled(object sender, EventArgs<bool> args)
        {
            if (args == null) return;
            if (args.GetData)
            {
                this.GetComponent<RectTransform>().SetParent(GUIManager.GetInstance.UIParent);
                // 添加UI输入事件
            }
            else
            {
                // 设置缓存区为父对象
                this.GetComponent<RectTransform>().SetParent(GUIManager.GetInstance.CacheCanvas);
                // 移除UI输入事件
            }
        }

        protected virtual void Awake()
        {
            // 初始化子部件清单 ：  预先在检视面板上设置好需要绑定的子部件，并将该脚本绑定给UI集然后将其做成预制件
            this.partList = new Dictionary<string, UIBehaviour>();
            foreach (var ui in this.loadList)
            {
                this.partList.Add(ui.name, ui);
            }
        }
    }


}

