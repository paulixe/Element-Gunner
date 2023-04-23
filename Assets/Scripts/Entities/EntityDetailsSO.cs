using UnityEngine;
using static Utilities.ValidateUtilities;
namespace PII.Entities
{
    /// <summary>
    /// Data for an entity (enemy or player)
    /// </summary>
    [CreateAssetMenu(fileName = nameof(EntityDetailsSO) + "_", menuName = "Scriptable Objects/PII/" + nameof(EntityDetailsSO))]
    public class EntityDetailsSO : ScriptableObject
    {
        #region Tooltip
        [Tooltip("Name of the entity")]
        #endregion
        public string EntityName;
        #region Tooltip
        [Tooltip("Prefab for creating an entity with this object's data")]
        #endregion
        public GameObject Prefab;
        #region Tooltip
        [Tooltip("Velocity of the entity")]
        #endregion
        public float MoveSpeed = 10f;
        private void OnValidate()
        {
            ValidateCheckEmptyObject(this, nameof(Prefab), Prefab);
            ValidateCheckEmptyString(this, nameof(EntityName), EntityName);
        }
    }
}