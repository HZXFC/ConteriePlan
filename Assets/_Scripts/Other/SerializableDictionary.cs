using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{


    ///// <summary>
    ///// 可序列化的字典类
    ///// </summary>
    ///// <typeparam name="TKey"></typeparam>
    ///// <typeparam name="TValue"></typeparam>
    //[Serializable]
    //public class SerializableDictionary<TKey, TValue> :MonoBehaviour, ISerializationCallbackReceiver
    //{
    //    [SerializeField]
    //    private List<TKey> keys = new List<TKey>();
    //    [SerializeField]
    //    private List<TValue> values = new List<TValue>();
        
    //    public Dictionary<TKey, TValue> Dictionary { get; set; }

    //    public void OnAfterDeserialize()
    //    {
    //        this.Dictionary = new Dictionary<TKey, TValue>();
    //        int count = Mathf.Min(this.keys.Count, this.values.Count);
    //        for (int i = 0; i < count; i++)
    //        {
    //            this.Dictionary.Add(this.keys[i], this.values[i]);
    //        }
    //    }

    //    public void OnBeforeSerialize()
    //    {
    //        this.keys.Clear();
    //        this.values.Clear();
    //        this.keys.Capacity = this.Dictionary.Count;
    //        this.values.Capacity = this.Dictionary.Count;
    //        foreach (var kvp in this.Dictionary)
    //        {
    //            this.keys.Add(kvp.Key);
    //            this.values.Add(kvp.Value);
    //        }
    //    }
    //}
}
