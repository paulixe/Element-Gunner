using UnityEngine;
using static Utilities.CommonUtilities;

namespace PII.Entities
{
    /// <summary>
    /// Associate player's inputs with the different events
    /// </summary>
    [RequireComponent(typeof(Player))]
    public class PlayerControl : MonoBehaviour
    {
        Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        public void Update()
        {
            MovementInput();
            AimInput(out float aimAngle, out Vector3 aimDirection);

            if (Input.GetMouseButton(0))
                FireInput(aimAngle);
        }
        private void FireInput(float aimAngle)
        {
            player.FireEvent.Call(aimAngle);
        }
        private void MovementInput()
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            Vector2 direction = new(xInput, yInput);
            if (Mathf.Abs(xInput) > 0 && Mathf.Abs(yInput) > 0)
                direction *= 0.7f;


            if (direction.magnitude > 0)
                player.MovementByVelocityEvent.Call(player.PlayerDetails.MoveSpeed, direction);
            else
                player.IdleEvent.Call();
        }
        private void AimInput(out float aimAngle, out Vector3 aimDirection)
        {
            Vector3 mouseWorldPos = GetMouseWorldPos();
            aimDirection = mouseWorldPos - player.transform.position;
            aimAngle = GetAngleFromVector(aimDirection);
            player.AimEvent.Call(aimAngle, aimDirection);
        }

    }
}