using UnityEngine;


namespace PII.Entities
{
    /// <summary>
    /// Associate events with animator component of Enemy
    /// </summary>
    [RequireComponent(typeof(Enemy))]
    public class AnimateEnemy : MonoBehaviour
    {
        private Enemy enemy;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }
        private void OnEnable()
        {
            enemy.AimEvent.OnAim += AimEvent_OnAim;
            enemy.IdleEvent.OnIdle += IdleEvent_OnIdle;
            enemy.MovementByPositionEvent.OnMovementByPosition += MovementByPositionEvent_OnMovementByPosition;
        }
        private void OnDisable()
        {
            enemy.AimEvent.OnAim -= AimEvent_OnAim;
            enemy.IdleEvent.OnIdle -= IdleEvent_OnIdle;
            enemy.MovementByPositionEvent.OnMovementByPosition -= MovementByPositionEvent_OnMovementByPosition;
        }
        private void SetMoveState(bool isMoving)
        {
            enemy.Animator.SetBool(Settings.isMoving, isMoving);
            enemy.Animator.SetBool(Settings.isIdle, !isMoving);
        }
        private void MovementByPositionEvent_OnMovementByPosition
            (MovementByPositionEvent movementByPositionEvent, MovementByPositionEventArgs movementByPositionEventArgs)
            => SetMoveState(true);

        private void IdleEvent_OnIdle(IdleEvent idleEvent, IdleEventArgs eventArgs) => SetMoveState(false);
        private void AimEvent_OnAim(AimEvent aimEvent, AimEventArgs aimEventArgs)
        {
            ResetAimDirectionAnimator();
            switch (aimEventArgs.aimDirection)
            {
                case AimDirection.Left:
                    enemy.Animator.SetBool(Settings.aimLeft, true);
                    break;
                case AimDirection.UpLeft:
                    enemy.Animator.SetBool(Settings.aimUpLeft, true);
                    break;
                case AimDirection.Up:
                    enemy.Animator.SetBool(Settings.aimUp, true);
                    break;
                case AimDirection.UpRight:
                    enemy.Animator.SetBool(Settings.aimUpRight, true);
                    break;
                case AimDirection.Right:
                    enemy.Animator.SetBool(Settings.aimRight, true);
                    break;
                case AimDirection.Down:
                    enemy.Animator.SetBool(Settings.aimDown, true);
                    break;
            }
        }
        private void ResetAimDirectionAnimator()
        {
            enemy.Animator.SetBool(Settings.aimLeft, false);
            enemy.Animator.SetBool(Settings.aimUpLeft, false);
            enemy.Animator.SetBool(Settings.aimUp, false);
            enemy.Animator.SetBool(Settings.aimUpRight, false);
            enemy.Animator.SetBool(Settings.aimRight, false);
            enemy.Animator.SetBool(Settings.aimDown, false);

        }
    }
}