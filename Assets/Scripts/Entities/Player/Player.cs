using UnityEngine;
using UnityEngine.Rendering;
using Utilities;
using static Utilities.ValidateUtilities;
namespace PII.Entities
{
    /// <summary>
    /// Reference to the different components composing the player
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(Idle))]
    [RequireComponent(typeof(IdleEvent))]
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(AimEvent))]
    [RequireComponent(typeof(SortingGroup))]
    [RequireComponent(typeof(AnimatePlayer))]
    [RequireComponent(typeof(MovementByVelocityEvent))]
    [RequireComponent(typeof(MovementByVelocity))]
    [RequireComponent(typeof(FireEvent))]
    [RequireComponent(typeof(Fire))]
    [RequireComponent(typeof(FireAmmo))]
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public EntityDetailsSO PlayerDetails;
        [HideInInspector] public IdleEvent IdleEvent;
        [HideInInspector] public AimEvent AimEvent;
        [HideInInspector] public MovementByVelocityEvent MovementByVelocityEvent;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public FireEvent FireEvent;
        public void Awake()
        {
            FireEvent = GetComponent<FireEvent>();
            IdleEvent = GetComponent<IdleEvent>();
            AimEvent = GetComponent<AimEvent>();
            Animator = GetComponent<Animator>();
            MovementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        }
        private void OnValidate()
        {
            ValidateCheckEmptyObject(this, nameof(PlayerDetails), PlayerDetails);
        }
    }
}