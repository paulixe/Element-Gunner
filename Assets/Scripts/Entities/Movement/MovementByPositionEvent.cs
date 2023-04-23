using System;
using UnityEngine;
namespace PII.Entities
{
    /// <summary>
    /// Event for moving an object to another point
    /// </summary>
    public class MovementByPositionEvent : MonoBehaviour
    {
        public event Action<MovementByPositionEvent, MovementByPositionEventArgs> OnMovementByPosition;
        public void Call(Vector3 targetPosition, float speed)
        {
            MovementByPositionEventArgs eventArgs = new(targetPosition, speed);
            OnMovementByPosition?.Invoke(this, eventArgs);
        }
    }
    public class MovementByPositionEventArgs : EventArgs
    {
        public Vector3 TargetPosition;
        public float Speed;
        public MovementByPositionEventArgs(Vector3 targetPosition, float speed)
        {
            this.TargetPosition = targetPosition;
            this.Speed = speed;
        }
    }
}
