using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConteriePlan
{
    /// <summary>
    /// 单例模式模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class, new()
    {
        private static T instance;
        private static readonly object locked = new object();
        /// <summary>
        /// 获取实例
        /// </summary>
        public static T GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (locked)
                    {
                        if (instance == null) instance = new T();
                    }
                }
                return instance;
            }
        }
    }
}
