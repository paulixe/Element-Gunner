using System;
using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Attribute for changing the scale displayed in the editor.
    ///<para> See also <see langword="ScaleFloatAttributeDrawer"/></para>
    /// </summary>
    public class ScaleFloatAttribute : PropertyAttribute
    {
        public float ScaleFactor;

        public ScaleFloatAttribute(float scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }
    }
}