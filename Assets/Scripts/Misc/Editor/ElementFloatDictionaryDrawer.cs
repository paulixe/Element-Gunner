
using UnityEditor;
using Utilities;
namespace PII
{
    /// <summary>   
    /// Instantiate enumDictionaryDrawer<Element,float>
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumDictionary<Element, float>))]
    public class ElementFloatDictionaryDrawer : EnumDictionaryDrawer<Element>
    {
    }
}