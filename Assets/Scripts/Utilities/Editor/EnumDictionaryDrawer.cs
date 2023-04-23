using System;
using System.Collections;
using UnityEditor;
using UnityEditor.IMGUI;
using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Class to inherit from if you want to create the editor for an EnumDictionary with a special EnumType
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class EnumDictionaryDrawer<TEnum> : PropertyDrawer where TEnum : Enum
    {
        const string ENUM_DICTIONARY_VALUES_NAME = "values";
        const string PAIR_KEY_NAME = "Key";
        const string PAIR_VALUE_NAME = "Value";
        public int FieldOffset => EditorUtilities.FIELDS_OFFSET;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            float heightOffset = 0;

            Rect headerRect = new Rect(position.x, position.y + heightOffset, position.width, EditorGUIUtility.singleLineHeight);
            //Header line

            EditorGUI.PropertyField(headerRect, property, new(property.displayName), false);
            heightOffset += EditorGUIUtility.singleLineHeight;
            //Enum Iteration
            if (property.isExpanded)
            {
                foreach (var value in property.Enumerate(ENUM_DICTIONARY_VALUES_NAME))
                {
                    SerializedProperty pairProperty = value as SerializedProperty;
                    SerializedProperty keyProperty = pairProperty.FindPropertyRelative(PAIR_KEY_NAME);
                    SerializedProperty valueProperty = pairProperty.FindPropertyRelative(PAIR_VALUE_NAME);

                    string enumName = keyProperty.enumNames[keyProperty.enumValueIndex];
                    Rect newRect = new Rect(position.x + FieldOffset, position.y + heightOffset, position.width - FieldOffset, position.height);

                    EditorGUI.PropertyField(newRect, valueProperty, new(enumName), true);
                    heightOffset += EditorGUI.GetPropertyHeight(valueProperty);
                }
            }

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
                foreach (var value in property.Enumerate(ENUM_DICTIONARY_VALUES_NAME))
                {
                    SerializedProperty arrayItem = value as SerializedProperty;
                    totalHeight += EditorGUI.GetPropertyHeight(arrayItem);
                }
            return totalHeight;
        }

    }
}