using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConteriePlan
{
    /// <summary>
    /// 输入控制器
    /// </summary>
    public static class InputController
    {
        public static Button AD { get; set; } = new Button("Horizontal");
        public static Button WS { get; set; } = new Button("Vertical");

        public static MouseButton LeftClick { get; set; } = new MouseButton(0);
        public static MouseButton RightClick { get; set; } = new MouseButton(1);
        public static MouseButton MiddleClick { get; set; } = new MouseButton(2);
        public static Vector2 MouseAxes => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        public static float MouseScrollWheel => Input.GetAxis("Mouse ScrollWheel");

        public static Key Up { get; set; } = new Key(KeyCode.UpArrow);
        public static Key Down { get; set; } = new Key(KeyCode.DownArrow);
        public static Key Left { get; set; } = new Key(KeyCode.LeftArrow);
        public static Key Right { get; set; } = new Key(KeyCode.RightArrow);
        public static Key LeftAlt { get; set; } = new Key(KeyCode.LeftAlt);

        public class Button
        {
            private string name;

            /// <summary>
            /// 按钮轴值（不按下时为0，按下后以设定速度向1运动）
            /// </summary>
            public float GetAxis { get { return Input.GetAxis(this.name); } }
            /// <summary>
            /// 按钮轴值源（不按下时为0，按下为1）
            /// </summary>
            public float GetAxisRaw { get { return Input.GetAxisRaw(this.name); } }

            /// <summary>
            /// 若指定按钮按下则为真
            /// </summary>
            public bool GetButton => Input.GetButton(this.name);
            /// <summary>
            /// 按钮按下的那一帧为真
            /// </summary>
            public bool GetButtonDown => Input.GetButtonDown(this.name);
            /// <summary>
            /// 按钮弹起的那一帧为真
            /// </summary>
            public bool GetButtonUp => Input.GetButtonUp(this.name);

            public bool GetPositiveButton => this.GetButton && this.GetAxisRaw > 0;
            public bool GetPositiveButtonDown => this.GetButtonDown && this.GetAxisRaw > 0;
            public bool GetPositiveButtonUp => this.GetButtonUp && this.GetAxisRaw > 0;

            public bool GetNegativeButton => this.GetButton && this.GetAxisRaw < 0;
            public bool GetNegativeButtonDown => this.GetButtonDown && this.GetAxisRaw < 0;
            public bool GetNegativeButtonUp => this.GetButtonUp && this.GetAxisRaw < 0;

            public Button(string name)
            {
                this.name = name;
            }
        }
        public class Key
        {
            private KeyCode keyCode;
            /// <summary>
            /// 若指定按键按下则为真
            /// </summary>
            public bool GetKey { get { return Input.GetKey(this.keyCode); } }
            /// <summary>
            /// 按键按下的那一帧为真
            /// </summary>
            public bool GetKeyDown { get { return Input.GetKeyDown(this.keyCode); } }
            /// <summary>
            /// 按键弹起的那一帧为真
            /// </summary>
            public bool GetKeyUp { get { return Input.GetKeyUp(this.keyCode); } }

            public Key(KeyCode name)
            {
                this.keyCode = name;
            }
        }
        public class MouseButton
        {
            private int button;
            public bool GetMouseButton => Input.GetMouseButton(this.button);
            public bool GetMouseButtonDown => Input.GetMouseButtonDown(this.button);
            public bool GetMouseButtonUp => Input.GetMouseButtonUp(this.button);

            public MouseButton(int b) => this.button = b;
        }
    }
}
