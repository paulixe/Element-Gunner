using System;
using System.Collections.Generic;
using UnityEngine;
using static Utilities.ValidateUtilities;
namespace Utilities.ObjectPooling
{
    /// <summary>
    /// Singleton you can use to <see cref="GetComponent(GameObject, Vector3, Quaternion)">get componnent</see> from a pool
    /// </summary>
    public class PoolManager : SingletonMonoBehaviour<PoolManager>
    {
        public Pool[] Pools;
        Dictionary<int, Queue<Component>> poolComponents = new Dictionary<int, Queue<Component>>();

        protected override void Awake()
        {
            base.Awake();
            foreach (var pool in Pools)
                CreatePool(pool.Prefab, pool.Capacity, Type.GetType(pool.ComponentName));


        }
        private void CreatePool(GameObject prefab, int poolSize, Type componentType)
        {
            int prefabId = prefab.GetInstanceID();
            GameObject poolAnchor = new GameObject(prefab.name + "_Anchor");
            poolAnchor.transform.SetParent(transform);
            poolComponents.Add(prefabId, new Queue<Component>());


            for (int i = 0; i < poolSize; i++)
            {
                GameObject newGameObject = Instantiate(prefab, poolAnchor.transform);
                poolComponents[prefabId].Enqueue(newGameObject.GetComponent(componentType));
                newGameObject.SetActive(false);
            }
        }
        public Component GetComponent(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            int prefabId = prefab.GetInstanceID();
            Component component = poolComponents[prefabId].Dequeue();
            ResetComponent(component, prefab, position, rotation);
            poolComponents[prefabId].Enqueue(component);
            return component;
        }
        private void ResetComponent(Component component, GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (component.gameObject.activeSelf)
                component.gameObject.SetActive(false);
            component.transform.position = position;
            component.transform.rotation = rotation;
            component.transform.localScale = prefab.transform.localScale;
        }
        #region validate
        private void CheckMonoValidity(Type classType)
        {
            if (!classType.IsSubclassOf(typeof(MonoBehaviour)))
                Debug.Log("Each Pool Should have a monoscript with a subtype of Monobehaviour");
        }
        [ContextMenu("validate")]
        private void OnValidate()
        {
            int index = 0;
            foreach (Pool pool in Pools)
            {
                ValidateCheckEmptyObject(this, nameof(pool.Prefab) + $"_{index}", pool.Prefab);
                ValidateCheckEmptyString(this, nameof(pool.ComponentName) + $"_{index}", pool.ComponentName);

                Type classType = Type.GetType(pool.ComponentName);

                if (classType != null)
                {
                    CheckMonoValidity(classType);

                    if (pool.Prefab != null && pool.Prefab.GetComponent(classType) == null)
                        Debug.Log($"The prefab {pool.Prefab.name} must have the component {classType.Name} attached to it");
                }
                else
                    Debug.Log(pool.ComponentName + " isn't a valid type");
                index++;
            }

        }
        #endregion
    }
}