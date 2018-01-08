using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace ConteriePlan
{
    /// <summary>
    /// 单例脚本管理器
    /// </summary>
    public class SingletonManager : MonoBehaviour
    {
        /// <summary>
        /// 容器
        /// </summary>
        private static GameObject container = null;
        /// <summary>
        /// 维护所有单例脚本映射
        /// </summary>
        private static Dictionary<string, MonoBehaviour> singletonMap = new Dictionary<string, MonoBehaviour>();


        /// <summary>
        /// 获取单例组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>() where T : MonoBehaviour, ISingletonMonoBehaviour
        {
            if (container == null)
            {
                //Debug.Log("创建单例管理器。");
                container = new GameObject();
                container.name = nameof(SingletonManager);
                container.AddComponent<SingletonManager>();
            }
            if (!singletonMap.ContainsKey(typeof(T).Name))
            {
                var i = container.AddComponent<T>();
                i.OnAddSingleton();
                singletonMap.Add(typeof(T).Name, i);
            }
            return singletonMap[typeof(T).Name] as T;
        }
        /// <summary>
        /// 移除单例组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RemoveInstance<T>() where T : MonoBehaviour, ISingletonMonoBehaviour
        {
            if (container != null && singletonMap.ContainsKey(typeof(T).Name))
            {
                (singletonMap[typeof(T).Name] as ISingletonMonoBehaviour).OnRemoveSingleton();
                Destroy(singletonMap[typeof(T).Name]);
                singletonMap.Remove(typeof(T).Name);
            }
        }


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            ExceptionHandler.GetInstance.ToString();
        }
        /// <summary>
        /// 程序结束时移除单例脚本管理器
        /// </summary>
        private void OnApplicationQuit()
        {
            foreach (var singleton in singletonMap)
            {
                (singleton.Value as ISingletonMonoBehaviour).OnRemoveSingleton();
                Destroy(singleton.Value);
            }
            singletonMap.Clear();
            if (container != null)
            {
                Destroy(container);
                container = null;
            }
        }
    }

    /// <summary>
    /// 单例Mono
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingletonMonoBehaviour
    {
        /// <summary>
        /// 在添加单例组件时发生
        /// </summary>
        void OnAddSingleton();
        /// <summary>
        /// 在移除单例组件时发生
        /// </summary>
        void OnRemoveSingleton();

        /// <summary>
        /// 销毁单例
        /// </summary>
        void DestroySelf();
    }
}
