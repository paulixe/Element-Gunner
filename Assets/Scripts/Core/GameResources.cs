using PII.Dungeon;
using PII.Dungeon.DungeonGraph;
using PII.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities;
using static Utilities.ValidateUtilities;
namespace PII
{
    /// <summary>
    /// Reference to different assets
    /// </summary>
    [CreateAssetMenu(fileName = nameof(GameResources), menuName = "Scriptable Objects/PII/" + nameof(GameResources))]
    public class GameResources : ScriptableObject
    {
        #region Dungeon
        [Space(10)]
        [Header("Dungeon")]
        #endregion
        #region ToolTip
        [Tooltip("Every room templates that will be used to create the dungeon with DungeonBuilder")]
        #endregion
        public List<RoomTemplateSO> RoomTemplates;
        #region ToolTip
        [Tooltip("Every dungeon graph that will be used to create the dungeon with DungeonBuilder")]
        #endregion
        public List<DungeonGraphSO> GraphTemplates;
        #region ToolTip
        [Tooltip("Purple tile in collision tilemaps, we use it for creating the unwalkable grid in the A* algorithm")]
        #endregion
        public TileBase CollisionTile;
        #region ToolTip
        [Tooltip("Green tile in collision tilemaps, we use it for creating the movementPenalty grid in the A* algorithm")]
        #endregion
        public TileBase PreferredPathTile;

        #region Player
        [Space(10)]
        [Header("Player")]
        #endregion
        #region ToolTip
        [Tooltip("Figures on the player")]
        #endregion
        public EntityDetailsSO PlayerDetails;

        #region Enemies
        [Space(10)]
        [Header("Enemies")]
        #endregion
        #region ToolTip
        [Tooltip("List of the ennemies we can create in the dungeon")]
        #endregion
        public List<EntityDetailsSO> EnemyDetails;

        private static GameResources instance;
        public static GameResources Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GameResources>(nameof(GameResources));
                }
                return instance;
            }
        }
        private void OnValidate()
        {
            ValidateCheckEnumerableValues(this, nameof(RoomTemplates), RoomTemplates);
            ValidateCheckEnumerableValues(this, nameof(GraphTemplates), GraphTemplates);
            ValidateCheckEmptyObject(this, nameof(PlayerDetails), PlayerDetails);
        }
    }
}