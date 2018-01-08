﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConteriePlan
{
    /// <summary>
    /// 道具基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class ItemBase /*: ArticleBase<T> where T : class, new()*/
    {
        // 设计思路：道具是一个可序列化的类，它和它的拥有者属于组合关系。当其拥有者生成对象时，同时生成该物品对象。物品生命周期与其拥有者相同。

        private CreatureInfoBase ownaer = null;

        /// <summary>
        /// 物品拥有者
        /// </summary>
        public CreatureInfoBase Ownaer => this.ownaer;

        public ItemBase(CreatureInfoBase ownaer)
        {
            this.ownaer = ownaer;
        }
    }
}
