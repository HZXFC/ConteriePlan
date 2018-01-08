using System.Collections.Generic;

namespace ConteriePlan
{
    /// <summary>
    /// 战斗角色行为状态机
    /// </summary>
    public class BattleCharacterBehaviourStateMachine
    {
        /// <summary>
        /// 战斗角色逻辑
        /// </summary>
        public BattleCharacterLogic CharacterLogic { get; set; }
        /// <summary>
        /// 当前角色的行为状态（该列表的第一个元素即为当前状态，最后一个元素为前一状态（待定））
        /// </summary>
        public List<BattleCharacterBehaviourStateBase> StateList { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public BattleCharacterBehaviourStateBase CurrentState => this.StateList.Peek();


        /// <summary>
        /// 修改状态
        /// </summary>
        /// <typeparam name="TState">要修改的状态类型</typeparam>
        /// <returns></returns>
        public TState ChangeState<TState>() where TState : BattleCharacterBehaviourStateBase, new()
        {
            // 执行上一个状态的收尾，然后从现有状态中抽出指定状态（或新建）压入表头，后执行状态初始化
            this.StateList.Peek().OnExit();// 上一状态收尾工作
                                           // 弹出第一个状态然后放到列表末尾
                                           // entity.StateList.InsertToLast(entity.StateList.ExtractFirst());
                                           // 弹出指定状态并压入到表头
            this.StateList.InsertToFirst(this.StateList.Extract(s => s is TState) ?? new TState().SetMachine(this));// 切换状态时会先检查列表中有无该状态，若有则将其放到第一位，若无则新建一个状态
            this.StateList.Peek().OnEnter();// 下一状态的初始化工作
            return this.StateList.Peek() as TState;
        }

        /// <summary>
        /// 执行状态方法
        /// </summary>
        public void Execute()
        {
            // 若当前状态无效则切换状态
            this.CurrentState.IsValid();
            this.CurrentState.OnUpdate();// 切换之后必须执行一次状态
        }

        /// <summary>
        /// 构造战斗角色行为状态机
        /// </summary>
        /// <param name="characterLogic">角色逻辑</param>
        /// <param name="initial">初始状态</param>
        public BattleCharacterBehaviourStateMachine(BattleCharacterLogic characterLogic, BattleCharacterBehaviourStateBase initial)
        {
            this.CharacterLogic = characterLogic;
            initial.SetMachine(this).OnEnter();// 初始化初始状态
            this.StateList = new List<BattleCharacterBehaviourStateBase>() { initial };// 添加初始状态
        }
    }
}
