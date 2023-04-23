using UnityEngine;
using Utilities;

namespace PII.Entities
{
    /// <summary>
    /// Class representing a kind of Ammo
    /// </summary>
    [CreateAssetMenu(fileName = nameof(AmmoDetailsSo) + "_", menuName = "Scriptable Objects/PII/" + nameof(AmmoDetailsSo))]
    public class AmmoDetailsSo : ScriptableObject
    {
        #region General
        [Space(10)]
        [Header("General")]
        #endregion
        #region Tooltip
        [Tooltip("Prefab used for instanciating this ammo")]
        #endregion
        public GameObject Prefab;

        #region Characteristic
        [Space(10)]
        [Header("Characteristic")]
        #endregion
        #region Tooltip
        [Tooltip("Velocity of the ammo")]
        #endregion
        public float Speed;
        #region Tooltip
        [Tooltip("Damage dealt with each element")]
        #endregion
        public EnumDictionary<Element, float> ElementDamages;
    }
}