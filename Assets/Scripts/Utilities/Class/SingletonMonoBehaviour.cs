using UnityEngine;
namespace Utilities
{
    /// <summary>
    /// Class to inherit from if we want to create a singleton monobehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }
    }
}