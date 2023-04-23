using UnityEngine;
using Utilities;
using Utilities.ObjectPooling;
namespace PII.Entities
{
    /// <summary>
    /// Initialize an ammo when the fireAmmoEvent is published
    /// </summary>
    [RequireComponent(typeof(FireAmmoEvent))]
    public class FireAmmo : MonoBehaviour
    {
        FireAmmoEvent fireAmmoEvent;
        [SerializeField] AmmoDetailsSo ammoDetails;
        [SerializeField] Transform shootPosition;
        private void Awake()
        {
            fireAmmoEvent = GetComponent<FireAmmoEvent>();
        }
        private void OnEnable()
        {
            fireAmmoEvent.OnFireAmmo += FireAmmoEvent_OnFire;
        }
        private void OnDisable()
        {
            fireAmmoEvent.OnFireAmmo -= FireAmmoEvent_OnFire;
        }
        private void FireAmmoEvent_OnFire(FireAmmoEvent fireAmmoEvent, FireAmmoEventArgs fireAmmoEventArgs)
        {
            float aimAngle = fireAmmoEventArgs.AimAngle;
            Fire(aimAngle);
        }
        private void Fire(float aimAngle)
        {
            Ammo ammo = PoolManager.Instance.GetComponent(ammoDetails.Prefab, shootPosition.position, Quaternion.identity) as Ammo;
            ammo.Initialize(ammoDetails.Speed, aimAngle, ammoDetails.ElementDamages);
            ammo.gameObject.SetActive(true);
        }
        private void OnValidate()
        {
            ValidateUtilities.ValidateCheckEmptyObject(this, nameof(shootPosition), shootPosition);
            ValidateUtilities.ValidateCheckEmptyObject(this, nameof(ammoDetails), ammoDetails);
        }
    }
}