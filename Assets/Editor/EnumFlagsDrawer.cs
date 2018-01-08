﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace ConteriePlan
{
    /// <summary>
    /// 将Flag枚举变为复选框
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumFlags))]
    public class EnumFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int buttonsIntValue = 0;
            int enumLength = property.enumNames.Length;
            bool[] buttonPressed = new bool[enumLength];
            float buttonWidth = (position.width - EditorGUIUtility.labelWidth) / enumLength;

            EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), label);

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < enumLength; i++)
            {

                // Check if the button is/was pressed 
                if ((property.intValue & (1 << i)) == 1 << i)
                {
                    buttonPressed[i] = true;
                }

                Rect buttonPos = new Rect(position.x + EditorGUIUtility.labelWidth + buttonWidth * i, position.y, buttonWidth, position.height);

                buttonPressed[i] = GUI.Toggle(buttonPos, buttonPressed[i], property.enumNames[i], "Button");

                if (buttonPressed[i])
                    buttonsIntValue += 1 << i;
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = buttonsIntValue;
            }
        }
    }
}
