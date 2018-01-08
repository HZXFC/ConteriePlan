using System.Collections.Generic;

namespace ConteriePlan
{
    /// <summary>
    /// 状态类型基类（状态模式）
    /// </summary>
    /// <typeparam name="T">状态需要的参数对象</typeparam>
    public interface IStateBase<T>
    {
        /// <summary>
        /// 进入改该状态时,所做的初始化动作
        /// </summary>
        /// <param name="entity"></param>
        void OnEnter();
        /// <summary>
        /// 执行状态逻辑行为
        /// </summary>
        /// <param name="entity"></param>
        void OnUpdate();
        ////获取消息,并对消息进行处理
        //public virtual bool OnMessage(T entity,Enum msg) { return true; }
        /// <summary>
        /// 退出状态时的收尾工作
        /// </summary>
        /// <param name="entity"></param>
        void OnExit();
    }
}
