using UnityEngine;
using static Utilities.ValidateUtilities;

namespace PII.Entities
{
    /// <summary>
    /// Rotate a transform when the aimEvent is published
    /// </summary>
    [RequireComponent(typeof(AimEvent))]
    public class Aim : MonoBehaviour
    {
        [SerializeField] Transform weaponRotationPoint;

        private AimEvent aimEvent;

        private void Awake()
        {
            aimEvent = GetComponent<AimEvent>();
        }
        private void OnEnable()
        {
            aimEvent.OnAim += AimEvent_OnAim;
        }
        private void OnDisable()
        {
            aimEvent.OnAim -= AimEvent_OnAim;
        }
        private void AimEvent_OnAim(AimEvent aimEvent, AimEventArgs aimEventArgs)
        {
            TargetAt(aimEventArgs.aimAngle, aimEventArgs.targetDirection, aimEventArgs.aimDirection);
        }
        private void TargetAt(float aimAngle, Vector3 targetDirection, AimDirection aimDirection)
        {
            weaponRotationPoint.eulerAngles = new(0, 0, (aimAngle));

            switch (aimDirection)
            {
                case AimDirection.Right:
                case AimDirection.UpRight:
                case AimDirection.Up:
                case AimDirection.Down:
                    weaponRotationPoint.localScale = new(1, 1, 0);
                    break;
                case AimDirection.Left:
                case AimDirection.UpLeft:
                    weaponRotationPoint.localScale = (new(1, -1, 0));
                    break;

            }

        }
        private void OnValidate()
        {
            ValidateCheckEmptyObject(this, nameof(weaponRotationPoint), weaponRotationPoint);
        }
    }
}