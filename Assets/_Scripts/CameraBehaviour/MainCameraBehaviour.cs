using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    [RequireComponent(typeof(Camera))]
    public class MainCameraBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 相机追随目标
        /// </summary>
        [SerializeField]
        public Transform PositionTarget = null;
        /// <summary>
        /// 主要注视目标
        /// </summary>
        [SerializeField]
        public Transform WatchTargetMajor = null;
        /// <summary>
        /// 次要注视目标
        /// </summary>
        [SerializeField]
        public Transform WatchTargetMinor = null;
        /// <summary>
        /// 与目标前后距离
        /// </summary>
        [SerializeField]
        private float distance = 5f;
        /// <summary>
        /// 与目标水平距离
        /// </summary>
        [SerializeField]
        private float horizontalDistance = 5f;
        /// <summary>
        /// 与目标垂直距离
        /// </summary>
        [SerializeField]
        private float height = 5f;
        /// <summary>
        /// 追随速度
        /// </summary>
        [SerializeField]
        private float followSpeed = 1f;
        /// <summary>
        /// 旋转速度
        /// </summary>
        [SerializeField]
        private float rotateSpeed = 1f;
        
        ///// <summary>
        ///// 最大仰角
        ///// </summary>
        //[SerializeField]
        //[Range(0, 90)]
        //private float maxElevationAngle = 15f;
        /// <summary>
        /// 最大俯角
        /// </summary>
        [SerializeField]
        [Range(0, 90)]
        private float maxDepressionAngle = 50f;
        ///// <summary>
        ///// 最大高度差
        ///// </summary>
        //[SerializeField]
        //private float maxHeight = 5f;
        /// <summary>
        /// 最小高度差
        /// </summary>
        [SerializeField]
        private float minHeight = -1.5f;
        /// <summary>
        /// 最大距离
        /// </summary>
        [SerializeField]
        private float maxDistance = -2f;
        /// <summary>
        /// 最小距离
        /// </summary>
        [SerializeField]
        private float minDistance = -15f;
        

        /// <summary>
        /// 距离判定
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private float DistanceJudge(float d)
        {
            var result = d;
            result = result > this.maxDistance ? this.maxDistance : result;
            result = result < this.minDistance ? this.minDistance : result;

            return result;
        }
        /// <summary>
        /// 倾斜判定
        /// </summary>
        /// <param name="h">当前高度</param>
        /// <param name="cameraPos">相机位置</param>
        /// <param name="targetPos">追随目标位置</param>
        /// <returns></returns>
        private float TiltJudge(float h)
        {
            if (h == 0) return h;
            // 水平相机与目标的距离
            var w = Vector3.Distance(this.PositionTarget.position, this.transform.position.SetValueAndReturn(y: this.PositionTarget.position.y));// ！开方

            // 相机与x轴夹角
            // var angle = Mathf.Atan2(h, w) * Mathf.Rad2Deg;// Atan2:从坐标轴原点起指向(x,y)点的射线与x轴正方向之间的夹角,返回值的单位为弧度

            var maxH = Mathf.Abs(w * Mathf.Tan(this.maxDepressionAngle * Mathf.Deg2Rad));// 最大高度差
            var minH = this.minHeight + this.PositionTarget.localPosition.y;

            if (h > maxH) return maxH;
            else if (h < minH) return minH;
            else return h;
        }

        /// <summary>
        /// 设置相机变化距离
        /// </summary>
        /// <param name="deltaDistance"></param>
        public void SetDeltaDistance(float deltaDistance)
        {
            this.distance = DistanceJudge(deltaDistance + this.distance);
        }
        /// <summary>
        /// 设置相机变化高度
        /// </summary>
        /// <param name="deltaHeight"></param>
        public void SetDeltaHeight(float deltaHeight)
        {
            // 相机的高度 ,来自输入的值
            this.height = TiltJudge(deltaHeight + this.height);            
        }
        /// <summary>
        /// 归零相机目标本地坐标与旋转
        /// </summary>
        public void CameraPositionReset()
        {
            this.PositionTarget.localPosition = Vector3.zero;
            this.PositionTarget.localEulerAngles = Vector3.zero;
            this.WatchTargetMajor.localPosition = Vector3.zero;
            this.WatchTargetMinor.localPosition = Vector3.zero;
        }
        
        private void Awake()
        {
            if (this.PositionTarget == null)
            {
                var obj = GameObject.Find("MainCameraTarget");
                if (obj == null) this.PositionTarget = new GameObject("MainCameraTarget").transform;
                else this.PositionTarget = obj.transform;
            }
            if (this.WatchTargetMajor == null)
            {
                var obj = GameObject.Find("WatchTargetMajor");
                if (obj == null) this.WatchTargetMajor = new GameObject("WatchTargetMajor").transform;
                else this.WatchTargetMajor = obj.transform;
            }
            if (this.WatchTargetMinor == null)
            {
                var obj = GameObject.Find("WatchTargetMinor");
                if (obj == null) this.WatchTargetMinor = new GameObject("WatchTargetMinor").transform;
                else this.WatchTargetMinor = obj.transform;
            }
            if (!this.WatchTargetMajor.parent.Equals(this.PositionTarget))
                this.WatchTargetMajor.parent = this.PositionTarget;
            if (!this.WatchTargetMinor.parent.Equals(this.PositionTarget))
                this.WatchTargetMinor.parent = this.PositionTarget;
        }
        private void Update()
        {
            // 相机应当保持目标到给定数值的偏移
            // ！目标不应该进行X轴和Z轴的旋转

            //// 相机的距离，+来自输入的值
            //var tempDistance = SetDistance() + this.distance;
            //this.distance = DistanceJudge(tempDistance);

            // 确定相机在目标点的水平位置
            var targetPos = new Vector3(
                this.horizontalDistance,
                0, // 在旋转前使相机和目标保持水平
                this.distance);
            // 旋转点，使着相机Z坐标轴永远等于目标Z坐标轴
            targetPos = Quaternion.AngleAxis(this.PositionTarget.eulerAngles.y, this.PositionTarget.up) * targetPos;
            targetPos += this.PositionTarget.position;

            // 弧插值
            // 对a与b进行线性插值
            var temp = Vector3.Lerp(
                this.transform.position.SetValueAndReturn(y: this.PositionTarget.position.y),// 使当前相机位置和目标保持水平
                targetPos,
                this.followSpeed * Time.deltaTime);
            // 计算出中心点向ab插值结果的方向,(保持相机的水平轴)
            temp = (temp - this.PositionTarget.position).normalized;
            // 返回中心点向该方向移动radius个距离后的点
            targetPos = this.PositionTarget.position + temp * Vector3.Distance(this.PositionTarget.position, targetPos);


            //// 相机的高度 ,来自输入的值
            //var tempHeight = SetTilt() + this.height;
            //this.height = TiltJudge(tempHeight, targetPos, this.PositionTarget.position);
            // 确定相机与目标的高度差
            targetPos.y = Mathf.Lerp(this.transform.position.y, this.PositionTarget.position.y + this.height, this.followSpeed * Time.deltaTime);


            // 取得主次注视目标的中间位置
            // 相机指向主次中间位置的方向
            var targetRotate = Quaternion.LookRotation(
                (Vector3.Lerp(this.WatchTargetMajor.position, this.WatchTargetMinor.position, .5f)
                - targetPos).normalized);
            // 对当前角度与目标角度进行插值
            targetRotate = Quaternion.Lerp(
                Quaternion.Euler(this.transform.eulerAngles.ScaleAndReturn(1, 1, 0)),
                Quaternion.Euler(targetRotate.eulerAngles.ScaleAndReturn(1, 1, 0)),
                this.rotateSpeed * Time.deltaTime);



            this.transform.position = targetPos;
            this.transform.rotation = targetRotate;
        }
    }
}
