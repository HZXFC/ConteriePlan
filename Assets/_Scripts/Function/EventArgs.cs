using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace ConteriePlan
{
    ///// <summary>
    ///// Unity事件类型模板（带有1个参数的事件）
    ///// </summary>
    ///// <typeparam name="T0"></typeparam>
    //[Serializable]
    //public class UnityEventHandler<T0> : UnityEvent<T0>
    //{

    //}
    ///// <summary>
    ///// Unity事件类型模板（带有2个参数的事件）
    ///// </summary>
    ///// <typeparam name="T0"></typeparam>
    ///// <typeparam name="T1"></typeparam>
    //[Serializable]
    //public class UnityEventHandler<T0, T1> : UnityEvent<T0, T1>
    //{

    //}
    ///// <summary>
    ///// Unity事件类型模板（带有3个参数的事件）
    ///// </summary>
    ///// <typeparam name="T0"></typeparam>
    ///// <typeparam name="T1"></typeparam>
    ///// <typeparam name="T2"></typeparam>
    //[Serializable]
    //public class UnityEventHandler<T0, T1, T2> : UnityEvent<T0, T1, T2>
    //{

    //}
    ///// <summary>
    ///// Unity事件类型模板（带有4个参数的事件）
    ///// </summary>
    ///// <typeparam name="T0"></typeparam>
    ///// <typeparam name="T1"></typeparam>
    ///// <typeparam name="T2"></typeparam>
    ///// <typeparam name="T3"></typeparam>
    //[Serializable]
    //public class UnityEventHandler<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3>
    //{

    //}

    /// <summary>
    /// 事件数据模板（传递一个数据）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        private T data;
        public T GetData => this.data;

        public EventArgs(T data) => this.data = data;
    }

    /// <summary>
    /// 事件数据模板（传递两个数据）
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class EventArgs<T1, T2> : EventArgs
    {
        private T1 data1;
        private T2 data2;
        public T1 GetData1 => this.data1;
        public T2 GetData2 => this.data2;

        public EventArgs(T1 data1, T2 data2)
        {
            this.data1 = data1;
            this.data2 = data2;
        }
    }

    /// <summary>
    /// 事件数据模板（传递三个数据）
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class EventArgs<T1, T2, T3> : EventArgs
    {
        private T1 data1;
        private T2 data2;
        private T3 data3;
        public T1 GetData1 => this.data1;
        public T2 GetData2 => this.data2;
        public T3 GetData3 => this.data3;

        public EventArgs(T1 data1, T2 data2, T3 data3)
        {
            this.data1 = data1;
            this.data2 = data2;
            this.data3 = data3;
        }
    }
}
