
using System.Collections;
using UnityEditor;

namespace Utilities
{
    /// <summary>
    /// Utilities used in the editor
    /// </summary>
    public static class EditorUtilities
    {
        public const int FIELDS_OFFSET = 5;
        public static IEnumerable Enumerate(this SerializedProperty property, string arrayName)
        {
            SerializedProperty newProperty = property.FindPropertyRelative(arrayName);
            return newProperty.GetEnumerator().Iterate();
        }
    }
}