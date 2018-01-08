using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConteriePlan
{
    public class ShowFPS : MonoBehaviour
    {
        [SerializeField]
        private float fpsMeasuringDelta = 2.0f;

        private float timePassed;
        private int m_FrameCount = 0;
        private float m_FPS = 0.0f;

        private void Awake()
        {
            // 设置渲染频率 // 注意，如果开启了垂直同步则无视此设定
            Application.targetFrameRate = -1;
        }

        private void Start()
        {
            timePassed = 0.0f;
        }

        private void Update()
        {
            m_FrameCount = m_FrameCount + 1;
            timePassed = timePassed + Time.deltaTime;

            if (timePassed > fpsMeasuringDelta)
            {
                m_FPS = m_FrameCount / timePassed;

                timePassed = 0.0f;
                m_FrameCount = 0;
            }
        }

        private void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1.0f, 0.5f, 0.0f);   //设置字体颜色的
            bb.fontSize = 40;       //当然，这是字体大小

            //居中显示FPS
            GUI.Label(new Rect((Screen.width / 2) - 40, 0, 200, 200), "FPS: " + m_FPS, bb);
        }
    }

}