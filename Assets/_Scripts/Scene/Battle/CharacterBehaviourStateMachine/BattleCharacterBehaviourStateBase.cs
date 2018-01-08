namespace ConteriePlan
{
    /// <summary>
    /// 战斗角色行为状态基类
    /// </summary>
    public abstract class BattleCharacterBehaviourStateBase : IStateBase<BattleCharacterBehaviourStateMachine>
    {
        protected BattleCharacterBehaviourStateMachine Machine = null;

        /// <summary>
        /// 判断当前状态是否有效，如果无效则切换下一个状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract bool IsValid();
        /// <summary>
        /// 进入改该状态时,所做的初始化动作
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnEnter() { }
        /// <summary>
        /// 退出状态时的收尾工作
        /// </summary>
        /// <param name="entity"></param>
        public virtual void OnExit() { }
        /// <summary>
        /// 执行状态逻辑行为
        /// </summary>
        /// <param name="entity"></param>
        public abstract void OnUpdate();

        /// <summary>
        /// 设置状态机对象
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public BattleCharacterBehaviourStateBase SetMachine(BattleCharacterBehaviourStateMachine machine)
        {
            this.Machine = machine;
            return this;
        }
    }
}
