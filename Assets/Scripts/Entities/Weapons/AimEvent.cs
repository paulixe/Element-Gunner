using System;
using UnityEngine;

namespace PII.Entities
{
    /// <summary>
    /// event for aiming with an angle or a direction
    /// </summary>
    public class AimEvent : MonoBehaviour
    {
        public event Action<AimEvent, AimEventArgs> OnAim;
        public void Call(float aimAngle, Vector3 targetDirection)
        {
            AimDirection aimDirection = ProjectUtilities.GetAimDirection(aimAngle);
            AimEventArgs args = new(aimAngle, targetDirection, aimDirection);
            OnAim?.Invoke(this, args);
        }
    }
    public class AimEventArgs : EventArgs
    {
        public Vector3 targetDirection;
        public float aimAngle;
        public AimDirection aimDirection;
        public AimEventArgs(float aimAngle, Vector3 targetDirection, AimDirection aimDirection)
        {
            this.aimAngle = aimAngle;
            this.targetDirection = targetDirection;
            this.aimDirection = aimDirection;
        }
    }
}