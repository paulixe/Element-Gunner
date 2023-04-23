using System;
using UnityEngine;

namespace PII.Entities
{
    /// <summary>
    /// event for moving an object with its velocity
    /// </summary>
    public class MovementByVelocityEvent : MonoBehaviour
    {
        public event Action<MovementByVelocityEvent, MovementByVelocityEventArgs> OnMovementByVelocity;
        public void Call(float moveSpeed, Vector2 direction)
        {
            MovementByVelocityEventArgs args = new(moveSpeed, direction);
            OnMovementByVelocity?.Invoke(this, args);
        }
    }
    public class MovementByVelocityEventArgs : EventArgs
    {
        public float MoveSpeed;
        public Vector2 direction;

        public MovementByVelocityEventArgs(float moveSpeed, Vector2 direction)
        {
            MoveSpeed = moveSpeed;
            this.direction = direction;
        }
    }
}