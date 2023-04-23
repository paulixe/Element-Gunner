using UnityEngine;


namespace PII.Entities
{
    /// <summary>
    /// Move an object to another point when movementbypositionevent is published
    /// </summary>
    [RequireComponent(typeof(MovementByPositionEvent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementByPosition : MonoBehaviour
    {
        Rigidbody2D rb;
        MovementByPositionEvent movementByPositionEvent;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            movementByPositionEvent = GetComponent<MovementByPositionEvent>();
        }
        private void OnEnable()
        {
            movementByPositionEvent.OnMovementByPosition += MovementByPositionEvent_OnMovementByPosition;
        }
        private void OnDisable()
        {
            movementByPositionEvent.OnMovementByPosition -= MovementByPositionEvent_OnMovementByPosition;
        }
        private void MovementByPositionEvent_OnMovementByPosition
            (MovementByPositionEvent movementByPositionEvent, MovementByPositionEventArgs movementByPositionEventArgs)
        => MoveRigibody(movementByPositionEventArgs.TargetPosition, movementByPositionEventArgs.Speed);

        private void MoveRigibody(Vector3 targetPosition, float speed)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
}