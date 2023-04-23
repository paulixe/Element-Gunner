using UnityEngine;
using Utilities;

namespace PII.Entities
{
    /// <summary>
    /// Class for initializin an ammo an detect collision
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo : MonoBehaviour
    {
        float speed;
        Vector3 aimDirection;
        EnumDictionary<Element, float> damageInfo;
        public void Initialize(float speed, float aimAngle, EnumDictionary<Element, float> damageInfo)
        {
            this.speed = speed;
            this.damageInfo = damageInfo;
            aimDirection = CommonUtilities.GetVectorFromAngle(aimAngle);
            transform.eulerAngles = new(0, 0, aimAngle);
        }
        private void Update()
        {
            transform.position += aimDirection * speed * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            DealDamage(collision);
            gameObject.SetActive(false);
        }
        private void DealDamage(Collider2D collision)
        {
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damageInfo);
            }
        }
    }
}