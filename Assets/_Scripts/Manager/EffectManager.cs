using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 效果管理器
    /// </summary>
    public class EffectManager : MonoBehaviour, ISingletonMonoBehaviour
    {
        /// <summary>
        /// 获取效果管理器实例
        /// </summary>
        public static EffectManager GetInstance => SingletonManager.GetInstance<EffectManager>();

        /// <summary>
        /// 效果根父对象
        /// </summary>
        [SerializeField]
        private Transform effectFatherObj = null;

        /// <summary>
        /// 效果父对象列表
        /// </summary>
        private List<Transform> effectParentList = null;


        public void OnAddSingleton()
        {
            if (this.effectFatherObj == null)
            {
                // 创建效果根父对象
                this.effectFatherObj = new GameObject(nameof(EffectManager)).transform;
                DontDestroyOnLoad(this.effectFatherObj);
            }
        }
        public void OnRemoveSingleton()
        {
            Destroy(this.effectFatherObj);
        }
        public void DestroySelf() { SingletonManager.RemoveInstance<EffectManager>(); }

        /// <summary>
        /// 获取效果父对象
        /// </summary>
        /// <typeparam name="T">效果类型</typeparam>
        /// <param name="effect">效果</param>
        /// <returns></returns>
        public Transform GetEffectParent<T>() where T : MonoBehaviour, IEffect
        {
            Transform parent = null;
            if (this.effectFatherObj == null)
            {
                // 创建效果根父对象
                OnAddSingleton();
            }
            if (this.effectParentList == null) this.effectParentList = new List<Transform>();
            if (this.effectParentList.Count > 0)
            {
                parent = this.effectParentList.Find(e => e.name == typeof(T).Name);
            }
            if (parent == null)
            {
                // 未找到效果的父对象，将新建
                parent = new GameObject(typeof(T).Name).transform;
                this.effectParentList.Add(parent);
                parent.parent = this.effectFatherObj;
            }
            return parent;
        }
    }
}
