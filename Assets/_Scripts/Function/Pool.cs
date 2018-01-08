using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> : Singleton<Pool<T>> where T : class, IPool, new()
    {
        // 本对象池实现原理：
        // 利用Stack将未使用的对象存储，取用时弹出Stack的第一个对象。若Stack中没有对象则新建一个对象
        // 对象池不管理使用中的对象，使用中的对象必须自己结束工作，并自己压栈。也可以自己销毁
        // !!!对象池不保留使用对象的引用
        // 清空对象池仅会清空未使用的对象
        // 对象池的生命周期存在于整个程序运行期间，不可被销毁（简单对象池是个普通单例）

        //使用方法:
        //var newObj = Pool<Object>.GetInstance.Take();// 取出对象
        //Pool<Object>.GetInstance.Restore(newObj);// 放回对象
        //Pool<Object>.GetInstance.Clear();// 清空未使用的对象

        /// <summary>
        /// 对象列表
        /// </summary>
        private Stack<T> ObjStack { get; set; }
        /// <summary>
        /// 池内对象数量
        /// </summary>
        public int Count => this.ObjStack.Count;

        /// <summary>
        /// 获取闲置池中一个对象，如果不存在则创建
        /// </summary>
        /// <typeparam name="UT"></typeparam>
        /// <returns></returns>
        public virtual T Take()
        {
            T obj = null;
            if (this.ObjStack == null) this.ObjStack = new Stack<T>();
            if (this.Count > 0) obj = this.ObjStack.Pop();// 从池中取一个可用对象
            if (obj == null) obj = new T();// 没有可用对象，创建之
            obj.OnTake();
            return obj;
        }
        /// <summary>
        /// 将使用完毕的对象放回对象池
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Restore(T obj)
        {
            if (this.ObjStack == null) this.ObjStack = new Stack<T>();
            this.ObjStack.Push(obj);
        }

        public virtual void Clear() => this.ObjStack.Clear();

        //public virtual void Restore(T mono)
        //{
        //    if (mono!=null&&mono.IsWork)
        //    {
        //        mono.IsWork = false;

        //    }
        //}

    }

    /// <summary>
    /// 简单Mono对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoPool<T> : Singleton<MonoPool<T>> where T : Component, IPool, new()
    {
        /// <summary>
        /// 对象列表
        /// </summary>
        private Stack<T> ObjStack { get; set; }
        /// <summary>
        /// 池内对象数量
        /// </summary>
        public int Count => this.ObjStack.Count;
        /// <summary>
        /// Mono预制体
        /// </summary>
        public T Perfab { get; set; } = null;
        ///// <summary>
        ///// 待机对象的父对象
        ///// </summary>
        //public Transform Parent { get; set; } = null;

        /// <summary>
        /// 获取闲置池中一个对象，如果不存在则创建
        /// </summary>
        /// <returns></returns>
        public T Take()
        {
            T mono = null;
            if (this.ObjStack == null) this.ObjStack = new Stack<T>();
            while (this.ObjStack.Count > 0)
            {
                mono = this.ObjStack.Pop();
                if (mono != null) break;// 若弹出的是空对象则重新弹出
            }
            if (mono == null)
            {
                if (this.Perfab == null)
                {
                    Debug.LogError($"{typeof(T).Name} 未提供预制件的初始化！");
                    return null;
                }
                mono = GameObject.Instantiate(this.Perfab);
                if (mono == null) return null;
            }
            mono.OnTake();
            return mono;
        }
        /// <summary>
        /// 将使用完毕的对象放回对象池
        /// </summary>
        /// <param name="mono"></param>
        public void Restore(T mono)
        {
            if (this.ObjStack == null) this.ObjStack = new Stack<T>();
            this.ObjStack.Push(mono);
        }
        /// <summary>
        /// 销毁池内存储的对象
        /// </summary>
        public void Clear()
        {
            while (this.ObjStack.Count > 0)
            {
                var obj = this.ObjStack.Pop();// 取出对象
                GameObject.Destroy(obj);// 销毁
            }
            // this.ObjStack.Clear();// 清空列表  
            //GameObject.Destroy(this.Parent);// 销毁父对象
        }
    }

    /// <summary>
    /// 池对象单元
    /// </summary>
    public interface IPool
    {
        ///// <summary>
        ///// 是否工作中
        ///// </summary>
        //bool IsWork { get; set; }

        /// <summary>
        /// 当获取对象时发生
        /// </summary>
        void OnTake();

        /// <summary>
        /// 自主销毁
        /// </summary>
        void Restore();
    }

    ///// <summary>
    ///// Mono对象池
    ///// </summary>
    //public interface IMonoPool : IPool
    //{
    //    /// <summary>
    //    /// 设置预制体
    //    /// </summary>
    //    //Transform GetPerfab();
    //}
}
