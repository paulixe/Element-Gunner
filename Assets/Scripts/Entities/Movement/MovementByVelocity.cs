using UnityEngine;


namespace PII.Entities
{
    /// <summary>
    /// move an object with its velocity in the rigibody
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MovementByVelocityEvent))]
    public class MovementByVelocity : MonoBehaviour
    {
        Rigidbody2D rb;
        MovementByVelocityEvent movementByVelocityEvent;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        }
        private void OnEnable()
        {
            movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        }
        private void OnDisable()
        {
            movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        }
        private void MovementByVelocityEvent_OnMovementByVelocity
            (MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
        => SetRigidbodyVelocity(movementByVelocityEventArgs.MoveSpeed, movementByVelocityEventArgs.direction);

        private void SetRigidbodyVelocity(float moveSpeed, Vector2 direction)
        {
            rb.velocity = moveSpeed * direction;
        }
    }
}