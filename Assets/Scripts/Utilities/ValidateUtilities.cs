using System.Collections;
using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Functions used in <c>OnValidate</c> for making sure that fiels populated in the inspector are valid
    /// </summary>
    public static class ValidateUtilities
    {
        public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
        {
            if (stringToCheck == "")
            {
                Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name);
                return true;
            }
            return false;
        }
        public static bool ValidateCheckEmptyObject(Object thisObject, string fieldName, Object objectToCheck)
        {
            if (objectToCheck == null)
            {
                Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name);
                return true;
            }
            return false;
        }
        public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
        {
            bool error = false;
            int count = 0;
            foreach (var item in enumerableObjectToCheck)
            {
                if (item == null)
                {
                    Debug.Log(fieldName + " has null values in object " + thisObject.name);
                    error = true;
                }
                else
                {
                    count++;
                }
            }
            if (count == 0)
            {
                Debug.Log(fieldName + " has no values in object " + thisObject.name);
                error = true;
            }
            return error;
        }
    }
}