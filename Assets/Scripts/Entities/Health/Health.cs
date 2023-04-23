using System;
using UnityEngine;
using Utilities;

namespace PII.Entities
{
    /// <summary>
    /// Manage the health of an entity
    /// </summary>
    [RequireComponent(typeof(DestroyedEvent))]
    public class Health : MonoBehaviour
    {
        DestroyedEvent destroyedEvent;
        public float MaxHealth = 100;
        public float CurrentHealth = 70;

        public EnumDictionary<Element, float> ElementResistance;

        private void Awake()
        {
            destroyedEvent = GetComponent<DestroyedEvent>();
        }
        private void OnEnable()
        {
            destroyedEvent.OnDestroy += DestroyedEvent_OnDestroy;
        }
        private void OnDisable()
        {
            destroyedEvent.OnDestroy -= DestroyedEvent_OnDestroy;
        }
        private void DestroyedEvent_OnDestroy(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(EnumDictionary<Element, float> damages)
        {
            foreach (Element e in Enum.GetValues(typeof(Element)))
            {
                CurrentHealth -= damages[e];
            }
            if (CurrentHealth <= 0)
                destroyedEvent.Call();
        }
    }
}