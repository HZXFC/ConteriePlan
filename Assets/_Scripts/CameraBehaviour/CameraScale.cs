using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 3D摄像机自适应
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraScale : MonoBehaviour
    {
        public void SetCameraScale()
        {
            int setHeight = 1920;
            int setWidth = 1080;
            int manualHeight;
            if (Convert.ToSingle(Screen.height) / Screen.width > Convert.ToSingle(setHeight) / setWidth)
                manualHeight = Mathf.RoundToInt(Convert.ToSingle(setWidth) / Screen.width * Screen.height);
            else manualHeight = setHeight;
            //var camera = GetComponent<Camera>();
            //var scale = manualHeight / Convert.ToSingle(setHeight);
            GetComponent<Camera>().fieldOfView *= manualHeight / Convert.ToSingle(setHeight);
        }


        private void Start()
        {
            SetCameraScale();
        }
    }
}
