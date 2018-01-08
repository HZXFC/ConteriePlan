using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 战斗系统
    /// </summary>
    public class BattleSystem : MonoBehaviour
    {
        /// <summary>
        /// 战斗场景相机
        /// </summary>
        [SerializeField] private new BattleCamera camera;
        /// <summary>
        /// 战斗成员
        /// </summary>
        [SerializeField] private BattleMember member;
        /// <summary>
        /// 战斗角色逻辑
        /// </summary>
        [SerializeField] private BattleCharacterLogic characterLogic = null;

        /// <summary>
        /// 临时 战斗UI
        /// </summary>
        [SerializeField] private List<ViewBase> tempViewList;

        /// <summary>
        /// 主相机
        /// </summary>
        public BattleCamera Camera => this.camera;
        /// <summary>
        /// 战斗成员
        /// </summary>
        public BattleMember Member => this.member;
        /// <summary>
        /// 战斗角色逻辑
        /// </summary>
        public BattleCharacterLogic CharacterLogic => this.characterLogic;


        /// <summary>
        /// 设置光标显示模式
        /// </summary>
        /// <param name="visible">是否显示</param>
        /// <param name="locked">0=无限制，1=游戏中心，2=游戏窗口范围内</param>
        public void SetCursor(bool visible, CursorLockMode locked)
        {
            Cursor.visible = visible;
            Cursor.lockState = locked;
        }

        /// <summary>
        /// 战斗流程
        /// </summary>
        /// <returns></returns>
        public IEnumerator BattleProcess()
        {
            // 重置所有成员行动值为0
            foreach (var m in this.Member.ActionMemberList)
                m.ActionPower = 0;

            // 重复执行回合步骤
            while (true)
            {
                // 判断场上是否有一方全灭
                //foreach (var m in null)
                //{
                //    break;
                //}

                // 按行动值降序排列，并设置当前行动成员
                var activeMember = this.Member.RefreshActionList();
                this.Member.InitializationActiveMember(activeMember);

                //print(activeMember.name + "---" +
                //    this.Member.ActionMemberList[0].ActionPower.ToString() + ":" +
                //     this.Member.ActionMemberList[1].ActionPower.ToString() + ":" +
                //      this.Member.ActionMemberList[2].ActionPower.ToString() + ":");

                // 相机的设置，有待调整
                this.Camera.SetTargetParent(activeMember.Transform, activeMember.CharacterController.height, 0);

                // 开始执行当前成员的回合逻辑
                if (activeMember is CharacterInfoBase)
                    yield return this.StartCoroutine(this.CharacterLogic.ActiveCharaBehaviour(activeMember as CharacterInfoBase));
                else if (activeMember is EnemyInfoBase)
                {
                    // 在这里实现怪物的Ai逻辑
                    activeMember.ActionPower = 0;
                }

                print("结束了一回合");
            }
        }

        private void Awake()
        {
            // 在这里加载资源与初始化

            // 设置UI组与缓存组
            GUIManager.GetInstance.CacheCanvas = GameObject.Find("CacheCanvas").transform;
            if (!GUIManager.GetInstance.CacheCanvas) Debug.LogError("未找到CacheCanvas！");
            GUIManager.GetInstance.UIParent = GameObject.Find("BattleUI").transform;
            if (!GUIManager.GetInstance.UIParent) Debug.LogError("未找到UIParent！");
            // 加载UI集，临时写法
            foreach (var ui in this.tempViewList)
            {
                GUIManager.GetInstance.AddUISet(ui.GetType().Name, ui);
            }

            if (this.Camera == null) this.camera = new BattleCamera();
            if (this.Member == null) this.member = new BattleMember();
            if (this.CharacterLogic == null) this.characterLogic = new BattleCharacterLogic();

            this.CharacterLogic.Initialization(this);

            // 由关卡场景传入成员列表
            var m1 = (Resources.Load<TutorInfo>("_Data/TutorInfo"));
            m1.Transform = GameObject.Find("P1").transform;
            var m2 = Instantiate(Resources.Load<TestEnemyInfo>("_Data/TestEnemyInfo"));
            m2.Transform = GameObject.Find("ILtan1").transform;
            m2.name = "IL1";
            var m3 = Instantiate(Resources.Load<TestEnemyInfo>("_Data/TestEnemyInfo"));
            m3.Transform = GameObject.Find("ILtan2").transform;
            m3.name = "IL2";

            this.Member.CharaList[0] = m1;
            this.Member.EnemyList.Add(m2);
            this.Member.EnemyList.Add(m3);
        }
        private void Start()
        {
            StartCoroutine(this.BattleProcess());// 开始执行战斗逻辑
        }
        private void Update()
        {
            // 调试用退出
            if (Input.GetKeyDown(KeyCode.Escape))
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

        }
    }
}



//  GameSettings.Instance.SaveToJSON(Application.streamingAssetsPath + $"/{nameof(GameSettings)}.json");
//  GameSettings.LoadFromJSON(Application.streamingAssetsPath + $"/{nameof(GameSettings)}.json");

//print(Application.dataPath);// 游戏本地路径
//print(Application.persistentDataPath);// C盘文档路径
//print(Application.streamingAssetsPath);// 本地流文件路径
//print(Application.temporaryCachePath);// C盘临时文件路径



///// <summary>
///// 战斗系统
///// </summary>
//public class BattleSystem : MonoBehaviour
//{
//    // 需要实现的功能
//    // 控制战斗流程的脚本
//    // 战斗系统应当拥有固定的UI清单。当战斗开始时加载UI至UI管理器。（战斗结束时隐藏或移除战斗UI）
//    // 控制角色行为的脚本
//    // 监控用户输入与UI事件输入

//    //private IEnumerator DownloadAssetBundle()
//    //{

//    //    // 使用 UnityWebRequest 来来下载服务器资源
//    //    using (var w3 = UnityEngine.Networking.UnityWebRequest.GetAssetBundle(@"file:///D:/dnoki/main/testassetbundle"))
//    //    {
//    //        yield return w3.Send();

//    //        if (w3.isError)
//    //        {
//    //            Debug.Log(w3.error);
//    //        }
//    //        else
//    //        {
//    //            //var t = w3.downloadHandler.text;
//    //            //print(w3.downloadHandler.text);

//    //            var ad = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(w3);
//    //            Instantiate(ad.LoadAsset<GameObject>("Assets/Resources/Player.prefab"));
//    //        }
//    //    }

//    //    // 不缓存，这种下载方式不会缓存到本地，退出游戏后需要重新下载
//    //    //using (WWW w3 = new WWW(""))
//    //    //{
//    //    //    yield return w3;

//    //    //    if (w3.error != null)
//    //    //    {
//    //    //        downloadData = w3.error;
//    //    //        yield return null;
//    //    //    }

//    //    //    var ab = w3.assetBundle;
//    //    //    print("获取的下载文本" + w3.text);
//    //    //}
//    //}
//}
