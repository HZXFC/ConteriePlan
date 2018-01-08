using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    ///// <summary>
    ///// 战斗行为基类
    ///// </summary>
    //[Serializable]
    //public abstract class TempBattleBehaviourBase : ScriptableObject,IBattleBehaviour
    //{
    //    // 设计思路1：行为以Json形式加密存储在其拥有者的对象里。
    //    // 设计思路2：行为是一个可序列化的类，它和它的拥有者属于组合关系。当其拥有者生成对象时，同时生成该行为对象。行为生命周期与其拥有者相同。

    //    //private static Dictionary<string,BattleSkillBase> battleSkillDic = new Dictionary<string, BattleSkillBase>();
    //    ///// <summary>
    //    ///// 检测是否已经创建了拥有者的行为
    //    ///// </summary>
    //    //protected BattleSkillBase IsSkillAlreadyCreate(string skillName,ICreature owner)
    //    //{
    //    //    if (battleSkillDic.ContainsKey(skillName))
    //    //    {
    //    //        if (battleSkillDic[skillName] == null)
    //    //        {
    //    //            battleSkillDic.Remove(skillName);// 字典中包含键但其值为空
    //    //            return null;
    //    //        }
    //    //        else
    //    //        {
    //    //            if (battleSkillDic[skillName].Owner.Equals(owner))
    //    //                return battleSkillDic[skillName];// 该对象已经生成了指定行为
    //    //            else return null;
    //    //        }
    //    //    }
    //    //    return null;
    //    //}


    //    ///// <summary>
    //    ///// 行为施放类型
    //    ///// </summary>
    //    //[SerializeField]
    //    //[EnumFlags]
    //    //private BehaviourType behaviourType;
    //    /// <summary>
    //    /// 行为名
    //    /// </summary>
    //    [SerializeField]
    //    private string behaviourName;
    //    /// <summary>
    //    /// 描述
    //    /// </summary>
    //    [SerializeField]
    //    private string description;
    //    /// <summary>
    //    /// 可执行次数
    //    /// </summary>
    //    [SerializeField]
    //    [Tooltip("可执行次数")]
    //    private int frequency;

    //    ///// <summary>
    //    ///// 行为持有者
    //    ///// </summary>
    //    //public ICreature Owner { get; set; }
    //    ///// <summary>
    //    ///// 行为施放类型
    //    ///// </summary>
    //    //public BehaviourType Type { get => this.behaviourType; set => this.behaviourType = value; }
    //    /// <summary>
    //    /// 行为名称
    //    /// </summary>
    //    public string Name { get => this.behaviourName; set => this.behaviourName = value; }
    //    /// <summary>
    //    /// 描述
    //    /// </summary>
    //    public string Description { get => this.description; set => this.description = value; }
    //    /// <summary>
    //    /// 可执行次数
    //    /// </summary>
    //    public int Frequency { get => frequency; set => frequency = value; }
    //    ///// <summary>
    //    ///// 一次还是分步
    //    ///// </summary>
    //    //public BehaviourType IsOnetimeOrSBS //=> this.SkillBehaviourType & (SkillBehaviourType.ONE_TIME | SkillBehaviourType.STEP_BY_STEP);
    //    //{
    //    //    get
    //    //    {
    //    //        if (((this.Type & BehaviourType.ONE_TIME) != 0) && ((this.Type & BehaviourType.STEP_BY_STEP) != 0))
    //    //            Debug.LogError("同时指定了行为为一次与分步");
    //    //        return this.Type & (BehaviourType.ONE_TIME | BehaviourType.STEP_BY_STEP);
    //    //    }
    //    //}
    //    ///// <summary>
    //    ///// 万向还是指定
    //    ///// </summary>
    //    //public BehaviourType IsUniversalOrDesignation //=> this.BehaviourType & (BehaviourType.UNIVERSAL | BehaviourType.DESIGNATION);
    //    //{
    //    //    get
    //    //    {
    //    //        if (((this.Type & BehaviourType.UNIVERSAL) != 0) && ((this.Type & BehaviourType.DESIGNATION) != 0))
    //    //            Debug.LogError("同时指定了行为为万向与指定");
    //    //        return this.Type & (BehaviourType.UNIVERSAL | BehaviourType.DESIGNATION);
    //    //    }
    //    //}
    //    ///// <summary>
    //    ///// 单体还是群体
    //    ///// </summary>
    //    //public BehaviourType IsMonomerOrGroup// => this.BehaviourType & (BehaviourType.MONOMER | BehaviourType.GROUP);
    //    //{
    //    //    get
    //    //    {
    //    //        if (((this.Type & BehaviourType.MONOMER) != 0) && ((this.Type & BehaviourType.GROUP) != 0))
    //    //            Debug.LogError("同时指定了行为为单体与群体");
    //    //        return this.Type & (BehaviourType.MONOMER | BehaviourType.GROUP);
    //    //    }
    //    //}



    //    ///// <summary>
    //    ///// 创建行为对象
    //    ///// </summary>
    //    ///// <param name="text"></param>
    //    ///// <returns></returns>
    //    //public static BattleSkillBase FromJson(ICreature owner, string text)
    //    //{
    //    //    var textList = text.Split("|:Skill:|".ToCharArray());
    //    //    if (textList.Length != 2) return null;
    //    //    var skill = ScriptableObject.CreateInstance(Type.GetType(textList[0])) as BattleSkillBase;
    //    //    skill.Owner = owner;
    //    //    return skill;
    //    //    //var skill = Activator.CreateInstance(type) as BattleSkillBase;
    //    //    //Serialization.AnalysisEncryptionJson(ref skill, textList[1]);
    //    //    //  return skill;
    //    //}
    //    ///// <summary>
    //    ///// 将行为存储为Json
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public string ToJson()
    //    //{
    //    //    return this.GetType().FullName + "|:Skill:|" + Serialization.ToJson(this);
    //    //}

    //    /// <summary>
    //    /// 行为效果
    //    /// </summary>
    //    /// <param name="applicant">行为施放者</param>
    //    /// <param name="recipients">行为接受者</param>
    //    public abstract void Effect(CreatureInfoBase applicant, CreatureInfoBase recipient);
    //    /// <summary>
    //    /// 行为流程
    //    /// </summary>
    //    /// <param name="applicant">施加者</param>
    //    /// <param name="recipients">接受者</param>
    //    public abstract void Process(CreatureInfoBase applicant, CreatureInfoBase[] recipients);

    //    //public virtual void SetBattleSkill(ICreature owner, SkillBehaviourType skillBehaviourType, string skillName, string description)
    //    //{
    //    //    this.SkillName = skillName;
    //    //    this.Description = description;

    //    //    // 当传入的行为行为类型指明了：
    //    //    // 一次或分步
    //    //    // 万向或指定
    //    //    // 单体或群体
    //    //    if ((
    //    //        (((skillBehaviourType & SkillBehaviourType.ONE_TIME) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.STEP_BY_STEP) != 0)) &&
    //    //        (((skillBehaviourType & SkillBehaviourType.UNIVERSAL) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.DESIGNATION) != 0)) &&
    //    //        (((skillBehaviourType & SkillBehaviourType.MONOMER) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.GROUP) != 0))
    //    //      ) == false)
    //    //    {
    //    //        Debug.LogError("传入的行为行为缺少应有的类型！！");
    //    //        return;
    //    //    }
    //    //    this.SkillBehaviourType = skillBehaviourType;
    //    //}
    //    //public static T CreateInstance<T>(SkillBehaviourType skillBehaviourType) where T : BattleSkillBase
    //    //{
    //    //    var skill = ScriptableObject.CreateInstance<T>();

    //    //    // 当传入的行为行为类型指明了：
    //    //    // 一次或分步
    //    //    // 万向或指定
    //    //    // 单体或群体
    //    //    if ((
    //    //        (((skillBehaviourType & SkillBehaviourType.ONE_TIME) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.STEP_BY_STEP) != 0)) &&
    //    //        (((skillBehaviourType & SkillBehaviourType.UNIVERSAL) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.DESIGNATION) != 0)) &&
    //    //        (((skillBehaviourType & SkillBehaviourType.MONOMER) != 0) ||
    //    //        ((skillBehaviourType & SkillBehaviourType.GROUP) != 0))
    //    //      ) == false)
    //    //    {
    //    //        Debug.LogError("传入的行为行为缺少应有的类型！！");
    //    //    }
    //    //    return skill;
    //    //}
    //}
    

    ///// <summary>
    ///// 行为施放类型
    ///// </summary>
    //[Flags]
    //public enum BehaviourType
    //{
    //    /// <summary>
    //    /// 一次性行为
    //    /// </summary>
    //    ONE_TIME = 1,
    //    /// <summary>
    //    /// 分步式行为
    //    /// </summary>
    //    STEP_BY_STEP = 2,
    //    /// <summary>
    //    /// 万向
    //    /// </summary>
    //    UNIVERSAL = 4,
    //    /// <summary>
    //    /// 指定
    //    /// </summary>
    //    DESIGNATION = 8,
    //    /// <summary>
    //    /// 单体
    //    /// </summary>
    //    MONOMER = 16,
    //    /// <summary>
    //    /// 群体
    //    /// </summary>
    //    GROUP = 32,
    //}

    ///// <summary>
    ///// 群体行为参数
    ///// </summary>
    //[Serializable]
    //public struct GroupSkill
    //{
    //    /// <summary>
    //    /// 是否吸附施放者
    //    /// </summary>
    //    public bool IsAdsorption;
    //    /// <summary>
    //    /// 群体行为中心点相对于持有者的位置
    //    /// </summary>
    //    public Vector3 CenterPoint;
    //    // 暂时以圆形行为做测试
    //    /// <summary>
    //    /// 行为半径
    //    /// </summary>
    //    public float Radius;

    //    /// <summary>
    //    /// 创建群体行为参数
    //    /// </summary>
    //    /// <param name="isAdsorption">是否吸附施放者</param>
    //    /// <param name="centerPoint">群体行为中心点相对于持有者的位置</param>
    //    /// <param name="radius">行为半径</param>
    //    public GroupSkill(bool isAdsorption, Vector3 centerPoint, float radius)
    //    {
    //        this.IsAdsorption = isAdsorption;
    //        this.CenterPoint = centerPoint;
    //        this.Radius = radius;
    //    }
    //}



    //public interface IPool<T>
    //{
    //    T Allocate();
    //    bool Recycle(T obj);
    //}
    //public interface IObjectFactory<T>
    //{
    //    T Create();
    //}

    //public abstract class Pool<T>:IPool<T>
    //{
    //    protected Stack<T> CacheStack = new Stack<T>();
    //    protected IObjectFactory<T> Factory;
    //    protected int MaxCount = 5;

    //    public int CurCount => CacheStack.Count;

    //    public virtual T Allocate()
    //    {
    //        return CacheStack.Count == 0
    //            ? Factory.Create()
    //            : CacheStack.Pop();
    //    }

    //    public abstract bool Recycle(T obj);
    //}

}
