using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 设置类基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SettingBase<T> : ScriptableObject where T : ScriptableObject
    {
        // 设置类是游戏整个生命周期都存在的唯一全局对象

        /// <summary>
        /// 唯一的实例
        /// </summary>
        private static T instance;
        private static readonly object locked = new object();
        /// <summary>
        /// 获得实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locked)
                    {
                        //#if UNITY_EDITOR
                        //                        if (instance == null)
                        //                            InitializeFromDefault(UnityEditor.AssetDatabase.LoadAllAssetsAtPath($"Resources").FirstOrDefault(asset => asset is T) as T);
                        //#endif
                        if (instance == null)
                            instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 保存当前设置到Json
        /// </summary>
        /// <param name="path">完全路径</param>
        /// <param name="isEncrypt">是否加密</param>
        public void SaveToJSON(string path, bool isEncrypt = true)
        {
            if (isEncrypt) Serialization.WriteStrToFile(path, Serialization.CreateEncryptionJson(this));
            else Serialization.WriteStrToFile(path, JsonUtility.ToJson(this, true));
        }
        /// <summary>
        /// 从json读取对象
        /// </summary>
        /// <param name="path">完全路径</param>
        /// <param name="isEncrypt">是否解密</param>
        /// <returns></returns>
        public static T LoadFromJSON(string path, bool isEncrypt = true)
        {
            if (!instance) DestroyImmediate(instance);// 立即删除对象
            instance = ScriptableObject.CreateInstance<T>();
            // 读取json的对象的数据。
            if (isEncrypt) Serialization.AnalysisEncryptionJson<T>(ref instance, Serialization.ReadStrFromFile(path));
            else JsonUtility.FromJsonOverwrite(Serialization.ReadStrFromFile(path), instance);

            // 它可以通过GC被直接destroy掉（如果没有任何引用的话）
            // 如果不想被GC的话，可以使用HideFlags.HideAndDontSave
            instance.hideFlags = HideFlags.HideAndDontSave;
            return instance;
        }

        ///// <summary>
        ///// 初始化设置为默认文件
        ///// </summary>
        ///// <param name="settings"></param>
        //public static void InitializeFromDefault(T settings)
        //{
        //    if (instance) DestroyImmediate(instance);// DestroyImmediate立即对对像进行销毁
        //    instance = Instantiate(settings);
        //    instance.hideFlags = HideFlags.HideAndDontSave;// 隐藏并不可编辑
        //}
    }
}
