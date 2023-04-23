using System;
using UnityEngine;
namespace PII.Entities
{
    /// <summary>
    /// event called when we want to fire an ammo
    /// </summary>
    public class FireAmmoEvent : MonoBehaviour
    {
        public event Action<FireAmmoEvent, FireAmmoEventArgs> OnFireAmmo;
        public void Call(float aimAngle)
        {
            FireAmmoEventArgs eventArgs = new(aimAngle);
            OnFireAmmo?.Invoke(this, eventArgs);
        }
    }
    public class FireAmmoEventArgs : EventArgs
    {
        public float AimAngle;
        public FireAmmoEventArgs(float aimAngle)
        {
            AimAngle = aimAngle;
        }
    }
}
