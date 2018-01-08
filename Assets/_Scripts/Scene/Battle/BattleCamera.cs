using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 战斗场景相机
    /// </summary>
    [Serializable]
    public class BattleCamera
    {
        /// <summary>
        /// 主相机
        /// </summary>
        [SerializeField]
        private MainCameraBehaviour mainCamera = null;
        /// <summary>
        /// 镜头旋转速度
        /// </summary>
        [SerializeField]
        private float cameraFreeRotateSpeed = 20f;
        /// <summary>
        /// 缩放速度
        /// </summary>
        [SerializeField]
        private float scaleSpeed = 1f;
        /// <summary>
        /// 倾斜速度
        /// </summary>
        [SerializeField]
        private float tiltSpeed = 1f;

        /// <summary>
        /// 主相机
        /// </summary>
        public MainCameraBehaviour MainCamera
        {
            get
            {
                if (this.mainCamera == null) this.mainCamera = MonoBehaviour.FindObjectOfType<MainCameraBehaviour>();
                return this.mainCamera;
            }
            set => this.mainCamera = value;
        }
        /// <summary>
        /// 跟随目标
        /// </summary>
        public Transform PositionTarget
        {
            get => this.MainCamera.PositionTarget;
            set => this.MainCamera.PositionTarget = value;
        }
        /// <summary>
        /// 主要注视目标
        /// </summary>
        public Transform WatchTargetMajor
        {
            get => this.MainCamera.WatchTargetMajor;
            set => this.MainCamera.WatchTargetMajor = value;
        }
        /// <summary>
        /// 次要注视目标
        /// </summary>
        public Transform WatchTargetMinor
        {
            get => this.MainCamera.WatchTargetMinor;
            set => this.MainCamera.WatchTargetMinor = value;
        }
        /// <summary>
        /// 相机目标父对象
        /// </summary>
        public Transform TargetParent
        {
            get => this.PositionTarget.parent;
            set => this.PositionTarget.parent = value;
        }

        /// <summary>
        /// 设置目标父对象
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="watchMajorHeight">主要注视目标高度</param>
        /// <param name="watchMinorHeight">次要注视目标高度</param>
        public void SetTargetParent(Transform parent, float watchMajorHeight = 0f, float watchMinorHeight = 0f)
        {
            this.TargetParent = parent;
            this.MainCamera.CameraPositionReset();
            this.WatchTargetMajor.localPosition = Vector3.up * watchMajorHeight;
            this.WatchTargetMinor.localPosition = Vector3.up * watchMinorHeight;
        }

        /// <summary>
        /// 镜头自由旋转
        /// </summary>
        /// <param name="isCanRotateActiveMember"></param>
        /// <param name="activeChara"></param>
        public void CameraTargetFreeRotate()
        {
            if (InputController.LeftAlt.GetKey)
                this.PositionTarget.Rotate(Vector3.up, InputController.MouseAxes.x.Normalized() * this.cameraFreeRotateSpeed * Time.deltaTime);
            else
            {
                // 重置目标相机旋转以使其正方向对准绑定角色正方向
                this.mainCamera.PositionTarget.localEulerAngles = Vector3.zero;
            }
        }

        /// <summary>
        /// 输入系统控制主镜头位置
        /// </summary>
        /// <param name="canScale">允许推拉镜头</param>
        /// <param name="canTilt">允许升降镜头</param>
        public void MainCameraPos(bool canScale = true, bool canTilt = true)
        {
            if (canScale)
            {
                var f = (InputController.Left.GetKey ? -1f : 0f) + (InputController.Right.GetKey ? 1f : 0f);
                this.MainCamera.SetDeltaDistance(/*InputController.MouseScrollWheel.Normalized()*/f * this.scaleSpeed * Time.deltaTime);
            }
            if (canTilt)
            {
                var dH = 0f;
                if (InputController.Up.GetKey) dH = 1;
                else if (InputController.Down.GetKey) dH = -1;
                dH *= this.tiltSpeed * Time.deltaTime;
                this.MainCamera.SetDeltaHeight(dH);
            }
        }
    }
}
