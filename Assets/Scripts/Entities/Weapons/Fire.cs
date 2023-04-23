using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace PII.Entities
{
    /// <summary>
    /// Registers to the FireEvent, it publishs a fireAmmoEvent according to each FireModule
    /// </summary>
    [RequireComponent(typeof(FireAmmoEvent))]
    [RequireComponent(typeof(FireEvent))]
    public class Fire : MonoBehaviour
    {
        public List<FireModule> FireModules;
        FireEvent fireEvent;
        FireAmmoEvent fireAmmoEvent;
        private void Awake()
        {
            fireAmmoEvent = GetComponent<FireAmmoEvent>();
            fireEvent = GetComponent<FireEvent>();
        }
        private void Update()
        {
            foreach (FireModule module in FireModules)
            {
                module.ShootCooldown -= Time.deltaTime;
            }
        }
        private void OnEnable()
        {
            fireEvent.OnFire += FireEvent_OnFire;
        }
        private void OnDisable()
        {
            fireEvent.OnFire -= FireEvent_OnFire;
        }
        void FireEvent_OnFire(FireEvent fireEvent, FireEventArgs fireEventArgs)
        {
            foreach (FireModule fireModule in FireModules)
                if (fireModule.ShootCooldown < 0)
                {
                    fireModule.ShootCooldown = fireModule.ShootTimeRefresh;
                    FireWith(fireModule, fireEventArgs);
                }
        }
        void FireWith(FireModule fireModule, FireEventArgs fireEventArgs)
        {
            foreach (float angle in fireModule.angles)
                fireAmmoEvent.Call(fireEventArgs.AimAngle * Mathf.Deg2Rad + angle);
        }
        private void OnValidate()
        {
            ValidateUtilities.ValidateCheckEnumerableValues(this, nameof(FireModules), FireModules);
        }
    }
}