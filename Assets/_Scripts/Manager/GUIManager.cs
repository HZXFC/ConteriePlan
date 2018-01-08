using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class GUIManager : MonoBehaviour, ISingletonMonoBehaviour
    {
        /// <summary>
        /// 取得UI管理器实例
        /// </summary>
        public static GUIManager GetInstance => SingletonManager.GetInstance<GUIManager>();

        /// <summary>
        /// 场景中的所有UI
        /// </summary>
        private Dictionary<string, ViewBase> UIList = new Dictionary<string, ViewBase>();

        /// <summary>
        /// UI缓存区
        /// </summary>
        public Transform CacheCanvas { get; set; }
        /// <summary>
        /// UI父对象
        /// </summary>
        public Transform UIParent { get; set; }

       // public UISetBase[] GetUIlist => this.UIList.Values.ToArray();

        public void OnAddSingleton() { }
        public void OnRemoveSingleton() { }
        public void DestroySelf() { SingletonManager.RemoveInstance<GUIManager>(); }

        /// <summary>
        /// 获取场景中的UI集
        /// </summary>
        /// <typeparam name="T">UI具体类型</typeparam>
        /// <param name="name">UI集名称</param>
        /// <returns></returns>
        public T GetUISet<T>(string name) where T : ViewBase
        {
            if (this.UIList.ContainsKey(name)) return this.UIList[name] as T;
            else
            {
                Debug.LogWarning($"尝试取得的UI({typeof(T).Name}:{name})在场景中不存在。");
                return null;
            }
        }
        /// <summary>
        /// 添加一个UI集到场景
        /// </summary>
        /// <param name="name">UI集名称</param>
        /// <param name="ui">UI集对象</param>
        public void AddUISet(string name, ViewBase ui)
        {
            if (this.UIList.ContainsKey(name)) return;
            this.UIList.Add(name, ui);
        }
        /// <summary>
        /// 销毁并移除场景中的UI
        /// </summary>
        /// <param name="name"></param>
        public void RemoveUISet(string name)
        {
            if (UIList.ContainsKey(name))
            {
                Destroy(UIList[name]);// 销毁UI集
                UIList.Remove(name);
            }
        }

        private void Awake()
        {

        }
    }
}
