using System;
using UnityEngine;
namespace PII.Entities
{
    /// <summary>
    /// event called when the left mouse button is being pressed
    /// </summary>
    public class FireEvent : MonoBehaviour
    {
        public event Action<FireEvent, FireEventArgs> OnFire;
        public void Call(float aimAngle)
        {
            FireEventArgs eventArgs = new(aimAngle);
            OnFire?.Invoke(this, eventArgs);
        }
    }
    public class FireEventArgs : EventArgs
    {
        public float AimAngle;
        public FireEventArgs(float aimAngle)
        {
            AimAngle = aimAngle;

        }
    }
}
