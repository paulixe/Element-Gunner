using System.Collections.Generic;
using UnityEngine;
using Utilities;
namespace PII.Entities
{
    /// <summary>
    /// Is used for firing ammo, each 'angle' represents an ammo launched
    /// with an offset of 'angle' from  player's weapon forward
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FireModule) + "_", menuName = "Scriptable Objects/PII/" + nameof(FireModule))]
    public class FireModule : ScriptableObject
    {
        #region Tooltip
        [Tooltip("Angles representing every Ammo, in radians")]
        #endregion
        [ScaleFloat(Mathf.Rad2Deg)]
        public List<float> angles;

        #region Tooltip
        [Tooltip("Time to fire again ammo")]
        #endregion
        [Range(0.05f, 10)]
        public float ShootTimeRefresh = 0.3f;

        [HideInInspector]
        public float ShootCooldown;
    }
}