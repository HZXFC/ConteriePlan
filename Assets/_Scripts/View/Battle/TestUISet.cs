using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ConteriePlan
{
    // 支持监听事件列表
    //IPointerEnterHandler - OnPointerEnter - 当指针进入所述对象调用
    //IPointerExitHandler - OnPointerExit - 被叫当指针离开对象
    //IPointerDownHandler - OnPointerDown - 当按下对象上的指针调用
    //IPointerUpHandler - OnPointerUp - 当指示器被释放（称为原始按压对象）上调用
    //IPointerClickHandler - OnPointerClick - 当按下并在同一对象上发布了一个指针调用
    //IInitializePotentialDragHandler - OnInitializePotentialDrag - 当拖动目标被发现时调用，可以用来初始化值
    //IBeginDragHandler - OnBeginDrag - 调用拖动时拖动对象是即将开始
    //IDragHandler - ondrag当 - 称为拖动对象当拖动正在发生
    //IEndDragHandler - OnEndDrag - 称为拖动对象当拖动结束
    //IDropHandler - OnDrop - 调用的对象上，其中一拖完成
    //IScrollHandler - OnScroll - 被叫当鼠标滚轮滚动
    //IUpdateSelectedHandler - OnUpdateSelected - 被叫所选择的对象的每个刻度上
    //ISelectHandler - ONSELECT - 被叫当对象成为选择的对象
    //IDeselectHandler - OnDeselect - 被叫所选择的对象上变为取消选定
    //IMoveHandler - OnMove - 当移动事件发生时（左，右，上，下，ECT）调用
    //ISubmitHandler - 的OnSubmit - 当按下提交按钮调用
    //ICancelHandler - OnCancel - 当按下取消按钮调用
    [Serializable]
    public class TestUISet : ViewBase
    {
        //public EventTrigger EventTrigger = null;



        /// <summary>
        /// 设置血量
        /// </summary>
        /// <param name="healthPoint"></param>
        /// <param name="healthPointLimit"></param>
        public void SetInfo(object sender, EventArgs<CharacterInfoBase> args)
        {
            if (args == null) return;
            this.GetUIPart<Text>("HP").text = Mathf.CeilToInt(args.GetData.HealthPoint).ToString("0");
            this.GetUIPart<Image>("HPBar").fillAmount = (float)args.GetData.HealthPoint / args.GetData.HealthPointLimit;
            this.GetUIPart<Text>("MP").text = Mathf.CeilToInt(args.GetData.ManaPoint).ToString("0");
            this.GetUIPart<Image>("MPBar").fillAmount = (float)args.GetData.ManaPoint / args.GetData.ManaPointLimit;

            this.GetUIPart<Text>("AD").text = args.GetData.AttackDamage.ToString();
            this.GetUIPart<Text>("AR").text = args.GetData.Armor.ToString();
            this.GetUIPart<Text>("MD").text = args.GetData.MagicDamage.ToString();
            this.GetUIPart<Text>("MR").text = args.GetData.MagicResistance.ToString();

            this.GetUIPart<Text>("Lv").text = args.GetData.Level.ToString();
        }

        protected override void Awake()
        {
            base.Awake();
            //this.PartList = new Dictionary<string, UIBehaviour>();
            //PartList.Add("HP", this.transform.Find("HP").GetComponent<Text>());
            //PartList.Add("HPBar", this.transform.Find("HPBar").GetComponent<Image>());



            //this.EventTrigger = GetComponent<EventTrigger>();
            //this.EventTrigger.triggers = new List<EventTrigger.Entry>();

            //// 定义需要绑定的事件类型
            //var entry = new EventTrigger.Entry();
            //// 赋值
            //entry.eventID = EventTriggerType.PointerClick;
            //entry.callback = new EventTrigger.TriggerEvent();
            //// 添加监听事件
            //entry.callback.AddListener(new UnityAction<BaseEventData>(Click));

            //this.EventTrigger.triggers.Add(entry);
        }
        public void Click(BaseEventData eventData)
        {
            print("事件已响应");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            print("OnPointerClick事件已响应");
        }
    }
    
}
