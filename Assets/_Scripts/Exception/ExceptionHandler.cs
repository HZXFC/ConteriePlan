using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 异常捕捉器
    /// </summary>
    public class ExceptionHandler : MonoBehaviour, ISingletonMonoBehaviour
    {
#pragma warning disable
        /// <summary>
        /// 全局异常捕捉器
        /// </summary>
        public static ExceptionHandler GetInstance => SingletonManager.GetInstance<ExceptionHandler>();

        /// <summary>
        /// 是否作为异常处理者
        /// </summary>
        [SerializeField]
        private bool isHandler = false;
        /// <summary>
        /// 捕捉到异常时是否退出程序
        /// </summary>
        [SerializeField]
        private bool isQuitWhenException = true;
        /// <summary>
        /// 异常日志的保存路径（文件夹）
        /// </summary>
        [SerializeField]
        private string logPath;
        /// <summary>
        /// Bug反馈程序的启动路径
        /// </summary>
        [SerializeField]
        private string bugExePath;


        public void OnAddSingleton() { }
        public void OnRemoveSingleton() { }
        public void DestroySelf() { SingletonManager.RemoveInstance<ExceptionHandler>(); }

        private void Handler(string logStr, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
            {
                // string logPath = this.logPath + "/Error " + DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss") + ".log";
                //打印日志
                if (Directory.Exists(this.logPath))
                {
                    File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
                    File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
                    File.AppendAllText(logPath, "[exception message]:" + logStr + "\r\n");
                    File.AppendAllText(logPath, "[stack trace]:" + stackTrace + "\r\n");
                }
                // 错误日志不会关闭程序
                if (type == LogType.Error) return;

                //启动bug反馈程序
                if (File.Exists(bugExePath))
                {
                    ProcessStartInfo pros = new ProcessStartInfo();
                    pros.FileName = bugExePath;
                    pros.Arguments = "\"" + logPath + "\"";
                    Process pro = new Process();
                    pro.StartInfo = pros;
                    pro.Start();
                }
                //退出程序，bug反馈程序重启主程序
                if (this.isQuitWhenException)
                {
                    Application.Quit();
                }
            }
        }

        private void Awake()
        {
            logPath = Application.dataPath;
            if (this.isHandler)
            {
                Application.logMessageReceived += Handler;
            }
        }
        private void OnDestroy()
        {
            Application.logMessageReceived -= Handler;// 清楚注册
        }


    }
}
