using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 圆形选择范围效果
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class CircleSelectRangeEffect : MonoBehaviour, IEffect
    {
        /// <summary>
        /// 获取池对象
        /// </summary>
        public static MonoPool<CircleSelectRangeEffect> GetPoolObject
        {
            get
            {
                var pool = MonoPool<CircleSelectRangeEffect>.GetInstance;
                if (pool.Perfab == null) pool.Perfab = Resources.Load<CircleSelectRangeEffect>($"Effects/{nameof(CircleSelectRangeEffect)}");
                return pool;
            }
        }
        /// <summary>
        /// 获取效果父对象（隐藏时的）
        /// </summary>
        public static Transform GetHideParent => EffectManager.GetInstance.GetEffectParent<CircleSelectRangeEffect>();

        /// <summary>
        /// 线渲染器
        /// </summary>
        [SerializeField]
        private LineRenderer lineRenderer = null;
        //[SerializeField] private Vector3 center = Vector3.zero;
        //[SerializeField] private float radius = 5;
        //[SerializeField] private int accuracy = 3;


        /// <summary>
        /// 当获取对象时发生
        /// </summary>
        public void OnTake() { }
        /// <summary>
        /// 关闭并恢复到对象池
        /// </summary>
        public void Restore()
        {
            this.gameObject.SetActive(false);
            this.transform.parent = CircleSelectRangeEffect.GetHideParent;
            CircleSelectRangeEffect.GetPoolObject.Restore(this);
        }


        /// <summary>
        /// 获取一个由一组点构成的圆
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="radius">半径</param>
        /// <param name="accuracy">顶点数量</param>
        /// <returns></returns>
        public Vector3[] GetCircle(Vector3 center, float radius, int accuracy)
        {
            if (accuracy < 3) accuracy = 3;
            float angle = 360f / accuracy;

            var result = new Vector3[accuracy];
            result[0] = center + Vector3.forward * radius;// 计算第一个点

            for (int i = 1; i < accuracy; i++)// 循环计算每个点
            {
                result[i] = Quaternion.AngleAxis(angle * i, Vector3.up) * result[0];
            }
            return result;
        }

        /// <summary>
        /// 打开圆圈特效
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="radius">圆圈半径</param>
        /// <param name="accuracy">量化数量</param>
        public CircleSelectRangeEffect Open(Transform parent, float radius, int accuracy = 5)
        {
            this.transform.parent = parent;
            this.transform.localPosition = Vector3.zero;
            this.lineRenderer.positionCount = accuracy;
            this.lineRenderer.SetPositions(GetCircle(Vector3.zero, radius, accuracy));
            if (this.gameObject.activeInHierarchy == false) this.gameObject.SetActive(true);
            return this;
        }

        private void Awake()
        {
            if (this.lineRenderer == null) this.lineRenderer = this.GetComponent<LineRenderer>();
            this.lineRenderer.loop = true;
            this.lineRenderer.useWorldSpace = false;
            //this.lineRenderer.widthMultiplier = 0.2f;
        }
        private void Update()
        {
            this.transform.Rotate(0, 360 * Time.deltaTime, 0);
        }
    }
}
