using UnityEngine;


namespace PII.Entities
{
    /// <summary>
    /// associate player's events with the animator component 
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class AnimatePlayer : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }
        private void OnEnable()
        {
            player.AimEvent.OnAim += AimEvent_OnAim;
            player.IdleEvent.OnIdle += IdleEvent_OnIdle;
            player.MovementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        }
        private void OnDisable()
        {
            player.AimEvent.OnAim -= AimEvent_OnAim;
            player.IdleEvent.OnIdle -= IdleEvent_OnIdle;
            player.MovementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        }
        private void SetMoveState(bool isMoving)
        {
            player.Animator.SetBool(Settings.isMoving, isMoving);
            player.Animator.SetBool(Settings.isIdle, !isMoving);
        }
        private void MovementByVelocityEvent_OnMovementByVelocity
            (MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
            => SetMoveState(true);

        private void IdleEvent_OnIdle(IdleEvent idleEvent, IdleEventArgs eventArgs) => SetMoveState(false);
        private void AimEvent_OnAim(AimEvent aimEvent, AimEventArgs aimEventArgs)
        {
            ResetAimDirectionAnimator();
            switch (aimEventArgs.aimDirection)
            {
                case AimDirection.Left:
                    player.Animator.SetBool(Settings.aimLeft, true);
                    break;
                case AimDirection.UpLeft:
                    player.Animator.SetBool(Settings.aimUpLeft, true);
                    break;
                case AimDirection.Up:
                    player.Animator.SetBool(Settings.aimUp, true);
                    break;
                case AimDirection.UpRight:
                    player.Animator.SetBool(Settings.aimUpRight, true);
                    break;
                case AimDirection.Right:
                    player.Animator.SetBool(Settings.aimRight, true);
                    break;
                case AimDirection.Down:
                    player.Animator.SetBool(Settings.aimDown, true);
                    break;
            }
        }
        private void ResetAimDirectionAnimator()
        {
            player.Animator.SetBool(Settings.aimLeft, false);
            player.Animator.SetBool(Settings.aimUpLeft, false);
            player.Animator.SetBool(Settings.aimUp, false);
            player.Animator.SetBool(Settings.aimUpRight, false);
            player.Animator.SetBool(Settings.aimRight, false);
            player.Animator.SetBool(Settings.aimDown, false);

        }
    }
}