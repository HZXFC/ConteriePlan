using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 静态功能、扩展函数
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// 大于0返回1，小于0返回-1
        /// </summary>
        /// <param name="origi"></param>
        /// <returns></returns>
        public static float Normalized(this float origi)
        {
            if (origi > 0) return 1;
            else if (origi < 0) return -1;
            else return 0;
        }

        /// <summary>
        /// 修改为输入的值并返回结果
        /// </summary>
        /// <param name="origi"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 SetValueAndReturn(this Vector3 origi, float? x = null, float? y = null, float? z = null)
        {
            if (x != null) origi.x = x.Value;
            if (y != null) origi.y = y.Value;
            if (z != null) origi.z = z.Value;
            return origi;
        }
        /// <summary>
        /// 缩放向量并返回结果
        /// </summary>
        /// <param name="origi"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ScaleAndReturn(this Vector3 origi, float x = 1, float y = 1, float z = 1) => new Vector3(origi.x * x, origi.y * y, origi.z * z);
        /// <summary>
        /// 加上一个向量并返回结果
        /// </summary>
        /// <param name="origi"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 PlusAndReturn(this Vector3 origi, float x = 0, float y = 0, float z = 0) => origi + new Vector3(x, y, z);
        
        ///// <summary>
        ///// 获取 UI 的 rectTransform
        ///// </summary>
        ///// <param name="origi"></param>
        ///// <returns></returns>
        //public static RectTransform GetRectTransform(this GameObject origi) => origi.transform as RectTransform;

        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        public struct Bezier
        {
            public Vector3 P0 { get; set; }
            public Vector3 P1 { get; set; }
            public Vector3 P2 { get; set; }
            public Vector3 P3 { get; set; }

            /// <summary>
            /// 一次方贝塞尔曲线
            /// </summary>
            /// <param name="t"></param>
            /// <returns></returns>
            public Vector3 OnePowerBezierCurve(float t)
            {
                Vector3 B = Vector3.zero;
                float t1 = (1 - t);
                B = t1 * P0 + P1 * t;
                //B.y = t1*P0.y + P1.y*t;  
                //B.z = t1*P0.z + P1.z*t;  
                return B;
            }
            /// <summary>
            /// 二次方贝塞尔曲线
            /// </summary>
            /// <param name="t"></param>
            /// <returns></returns>
            public Vector3 TwoPowerBezierCurve(float t)
            {
                Vector3 B = Vector3.zero;
                float t1 = (1 - t) * (1 - t);
                float t2 = t * (1 - t);
                float t3 = t * t;
                B = P0 * t1 + 2 * t2 * P1 + t3 * P2;
                //B.y = P0.y*t1 + 2*t2*P1.y + t3*P2.y;  
                //B.z = P0.z*t1 + 2*t2*P1.z + t3*P2.z;  
                return B;
            }
            /// <summary>
            /// 三次方贝塞尔曲线
            /// </summary>
            /// <param name="t"></param>
            /// <returns></returns>
            public Vector3 ThreePowerBezierCurve(float t)
            {
                Vector3 B = Vector3.zero;
                float t1 = (1 - t) * (1 - t) * (1 - t);
                float t2 = (1 - t) * (1 - t) * t;
                float t3 = t * t * (1 - t);
                float t4 = t * t * t;
                B = P0 * t1 + 3 * t2 * P1 + 3 * t3 * P2 + P3 * t4;
                //B.y = P0.y*t1 + 3*t2*P1.y + 3*t3*P2.y + P3.y*t4;  
                //B.z = P0.z*t1 + 3*t2*P1.z + 3*t3*P2.z + P3.z*t4;  
                return B;
            }

            /// <summary>
            /// 创建贝塞尔曲线
            /// </summary>
            /// <param name="p0">起始点1</param>
            /// <param name="p1">控制点或终止点2</param>
            /// <param name="p2">控制点或终止点3</param>
            /// <param name="p3">终止点4</param>
            public Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
            {
                this.P0 = p0;
                this.P1 = p1;
                this.P2 = p2;
                this.P3 = p3;
            }
        }
        
        /// <summary>
        /// 从列表中提取指定索引的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Extract<T>(this List<T> origi, int index)
        {
            if (index < 0 || index > origi.Count-1) return default(T);
            var result = origi[index];
            origi.RemoveAt(index);
            return result;
        }
        /// <summary>
        /// 从列表中提取第一个找到的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <param name="soure"></param>
        /// <returns></returns>
        public static T Extract<T>(this List<T> origi, T soure)
        {
            var result = origi.Find(s => s.Equals(soure));
            if (result == null) return default(T);
            origi.Remove(result);
            return result;
        }
        /// <summary>
        /// 从列表中提取第一个复合条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T Extract<T>(this List<T> origi, Predicate<T> match)
        {
            var result = origi.Find(match);
            if (result == null) return default(T);
            origi.Remove(result);
            return result;
        }
        /// <summary>
        /// 弹出第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <returns></returns>
        public static T ExtractFirst<T>(this List<T> origi) => origi.Extract(0);
        /// <summary>
        /// 弹出最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <returns></returns>
        public static T ExtractLast<T>(this List<T> origi) => origi.Extract(origi.Count - 1);
        /// <summary>
        /// 返回第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <returns></returns>
        public static T Peek<T>(this List<T> origi) => origi[0];
        /// <summary>
        /// 插入元素到列表开始处
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <param name="soure"></param>
        public static void InsertToFirst<T>(this List<T> origi, T soure) => origi.Insert(0, soure);
        /// <summary>
        /// 插入元素到列表末尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origi"></param>
        /// <param name="soure"></param>
        public static void InsertToLast<T>(this List<T> origi, T soure) => origi.Insert(origi.Count - 1, soure);
        //public class DOTweenMethods
        //{
        //    private float myValue;

        //    public void Program()
        //    {
        //        var t = DOTween.To(() => this.myValue, x => this.myValue = x, 100, 1);

        //        UniRx.Observable.EveryUpdate().Subscribe(_ => MonoBehaviour.print("UniRx：这个lambada函数将会每帧被调用"));
        //        var temp = UniRx.Observable.IntervalFrame(5).Subscribe(_ => MonoBehaviour.print("UniRx：这个lambada函数将会每隔5帧执行一次"));
        //        temp.Dispose();// 使用 Dispose 取消该协程的调用
        //    }
        //}
    }
}