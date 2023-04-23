using UnityEngine;


namespace PII.Entities
{
    /// <summary>
    /// Immobilize this gameobject when the idleEvent is published
    /// </summary>
    [RequireComponent(typeof(IdleEvent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Idle : MonoBehaviour
    {
        private IdleEvent idleEvent;
        private Rigidbody2D rb;
        #region Initialization
        void Awake()
        {
            idleEvent = GetComponent<IdleEvent>();
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            idleEvent.OnIdle += IdleEvent_OnIdle;
        }
        private void OnDisable()
        {
            idleEvent.OnIdle -= IdleEvent_OnIdle;
        }
        #endregion
        private void IdleEvent_OnIdle(IdleEvent idleEvent, IdleEventArgs eventArgs)
        {
            rb.velocity = new(0, 0);
        }
    }
}