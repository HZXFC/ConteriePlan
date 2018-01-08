using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace ConteriePlan
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public static BlowFish ECBKey = Serialization.ReadBinaryFileToObj<BlowFish>(Application.streamingAssetsPath + "/Key.bin");

        /// <summary>
        /// 对象转化为二进制
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] FormatterObjToBytes(object obj)
        {
            if (obj == null) return null;
            try
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    return stream.GetBuffer();
                }
            }
            catch (Exception e)
            {
                Debug.Log("对象转化二进制失败！" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 二进制转化为对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static object FormatterBytesToObj(byte[] buffer)
        {
            if (buffer == null) return null;
            try
            {
                using (var stream = new MemoryStream(buffer))
                {
                    var formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Debug.Log("二进制转化对象失败！" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 将对象写入二进制文件
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="path">完全路径</param>
        public static void WriteObjToBinaryFile(string path, object obj)
        {
            if (obj == null) return;
            var buffer = FormatterObjToBytes(obj);
            if (buffer != null)
            {
                File.WriteAllBytes(path, buffer);
                Debug.Log("已将对象存入到二进制文件。");
            }
        }
        /// <summary>
        /// 从二进制文件读取对象
        /// </summary>
        /// <param name="path">文件全路径</param>
        /// <returns></returns>
        public static object ReadBinaryFileToObj(string path)
        {
            var buffer = File.ReadAllBytes(path);
            return FormatterBytesToObj(buffer);
        }
        /// <summary>
        /// 从二进制文件读取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">文件全路径</param>
        /// <returns></returns>
        public static T ReadBinaryFileToObj<T>(string path) where T : class
        {
            var buffer = File.ReadAllBytes(path);
            return FormatterBytesToObj(buffer) as T;
        }

        /// <summary>
        /// 生成加密Json文本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CreateEncryptionJson(object obj) => ECBKey.Encrypt_ECB(JsonUtility.ToJson(obj));
        /// <summary>
        /// 解密Json文本并覆盖对象成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T AnalysisEncryptionJson<T>(ref T obj, string json) => FromJson(ref obj, ECBKey.Decrypt_ECB(json));

        /// <summary>
        /// 将文本写入文件
        /// </summary>
        /// <param name="path">完全路径</param>
        /// <param name="text">写入的文本</param>
        public static void WriteStrToFile(string path, string text) => File.WriteAllText(path, text, Encoding.Unicode);
        /// <summary>
        /// 从文件读取文本
        /// </summary>
        /// <param name="path">完全路径</param>
        /// <returns></returns>
        public static string ReadStrFromFile(string path) => File.ReadAllText(path, Encoding.Unicode);

        /// <summary>
        /// 创建Json文本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj) => JsonUtility.ToJson(obj);
        /// <summary>
        /// 从Json文本覆盖对象成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(ref T obj, string json)
        {
            JsonUtility.FromJsonOverwrite(json, obj);
            return obj;
        }
        ///// <summary>
        ///// 存储对象到Json
        ///// </summary>
        ///// <param name="path">完全路径</param>
        ///// <param name="obj">对象</param>
        //public static void SaveObjToJson(string path, object obj)
        //{
        //    Debug.Log($"存储对象到：{path}");
        //    File.WriteAllText(path, JsonUtility.ToJson(obj, true), Encoding.Unicode);
        //}
        ///// <summary>
        ///// 读取Json中存储的数据到对象
        ///// </summary>
        ///// <param name="path">完全路径</param>
        ///// <param name="obj">对象</param>
        ///// <param name="isDecrypt">是否解密</param>
        //public static void LoadObjFromJson(string path, ref object obj)
        //{
        //    var text = File.ReadAllText(path, Encoding.Unicode);
        //    JsonUtility.FromJsonOverwrite(text, obj);
        //}
        ///// <summary>
        ///// 读取Json中存储的数据到对象
        ///// </summary>
        ///// <param name="path">完全路径</param>
        ///// <param name="obj">对象</param>
        ///// <param name="isDecrypt">是否解密</param>
        //public static void LoadObjFromJson<T>(string path, ref T obj)
        //{
        //    var text = File.ReadAllText(path, Encoding.Unicode);
        //    JsonUtility.FromJsonOverwrite(text, obj);
        //}
    }
}
