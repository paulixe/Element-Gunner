using UnityEditor;
using UnityEngine;
namespace Utilities.ObjectPooling
{
    /// <summary>
    /// <see cref="Pool"/> represents the information used for creating a queue of objects in <see cref="PoolManager"/>
    /// </summary>
    [System.Serializable]
    public struct Pool
    {
        public int Capacity;
        public GameObject Prefab;
        public string ComponentName;

    }
}