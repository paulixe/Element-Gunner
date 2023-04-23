using System.Collections;
using UnityEngine;

namespace PII.Entities
{
    /// <summary>
    /// Reference to the different component composing an enemy
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AnimateEnemy))]
    [RequireComponent(typeof(Idle))]
    [RequireComponent(typeof(AimEvent))]
    [RequireComponent(typeof(Idle))]
    [RequireComponent(typeof(IdleEvent))]
    [RequireComponent(typeof(MovementByPosition))]
    [RequireComponent(typeof(MovementByPositionEvent))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Health))]

    public class Enemy : MonoBehaviour
    {
        [HideInInspector] public Animator Animator;
        [HideInInspector] public IdleEvent IdleEvent;
        [HideInInspector] public AimEvent AimEvent;
        [HideInInspector] public MovementByPositionEvent MovementByPositionEvent;
        [HideInInspector] public DestroyedEvent DestroyedEvent;
        [HideInInspector] public EntityDetailsSO EnemyDetails { get; private set; }

        public void Initialize(EntityDetailsSO enemyDetails)
        {
            EnemyDetails = enemyDetails;
        }

        private void Awake()
        {
            DestroyedEvent = GetComponent<DestroyedEvent>();
            IdleEvent = GetComponent<IdleEvent>();
            AimEvent = GetComponent<AimEvent>();
            Animator = GetComponent<Animator>();
            MovementByPositionEvent = GetComponent<MovementByPositionEvent>();
        }
    }
}