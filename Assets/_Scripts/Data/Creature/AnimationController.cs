using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConteriePlan
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator = null;


        public void Hit()
        {
            // print("已执行Hit");
        }

        private void Start()
        {
            if (this.animator == null) this.animator = this.GetComponent<Animator>();

        }
        private void Update()
        {
            this.GetComponent<CharacterController>().SimpleMove(Vector3.zero);
        }
    }

}


//private void Update()
//{
//    //if (InputController.LeftAlt.GetKey)
//    //{
//    //    BattleLogic.mainCamera.PositionTarget.Rotate(Vector3.up, InputController.MouseAxes.x * BattleLogic.cameraRotateSpeed * Time.deltaTime, Space.Self);
//    //}
//    //else
//    //{
//    //    BattleLogic.mainCamera.PositionTarget.localEulerAngles = Vector3.zero;
//    //    // 鼠标左右控制角色视角
//    //    this.transform.Rotate(Vector3.up, InputController.MouseAxes.x * BattleLogic.cameraRotateSpeed * Time.deltaTime, Space.World);
//    //}
//}
//private void FixedUpdate()
//{
//    //// 以镜头正方向为准移动物体
//    //var moveValue = Quaternion.Euler(this.transform.eulerAngles.ScaleAndReturn(0, 1, 0)) * new Vector3(InputController.AD.GetAxis, 0, InputController.WS.GetAxis);
//    //moveValue *= this.moveSpeed;
//    //this.moveDir = this.moveDir.SetValueAndReturn(moveValue.x, z: moveValue.z);

//    //if (this.characterController.isGrounded)
//    //{
//    //    this.moveDir = this.moveDir.SetValueAndReturn(y: 0);
//    //}
//    //else
//    //{
//    //    this.moveDir += Physics.gravity * Time.fixedDeltaTime;
//    //    if (this.moveDir.y < -10) this.moveDir.y = -10f;// 掉落速度不超过10
//    //}

//    //this.characterController.Move(this.moveDir * Time.fixedDeltaTime);

//}