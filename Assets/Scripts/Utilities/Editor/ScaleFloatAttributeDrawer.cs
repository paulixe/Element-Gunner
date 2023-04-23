using UnityEditor;
using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Editor for <c>ScaleFloatAttribute</c>
    /// </summary>
    [CustomPropertyDrawer(typeof(ScaleFloatAttribute))]
    public class ScaleFloatAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ScaleFloatAttribute scaleFloatAttribute = (ScaleFloatAttribute)attribute;
            float scaleFactor = scaleFloatAttribute.ScaleFactor;

            EditorGUI.BeginProperty(position, label, property);
            property.floatValue = EditorGUI.FloatField(position, new GUIContent(property.displayName), scaleFactor * property.floatValue) / scaleFactor;

            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}