﻿using System.Collections.Generic;
using UnityEngine;
namespace PII.Dungeon
{
    /// <summary>
    /// Wrapper for the data representing a room
    /// </summary>
    [CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]
    public class RoomTemplateSO : ScriptableObject
    {

        #region Header ROOM PREFAB

        [Space(10)]
        [Header("ROOM PREFAB")]

        #endregion Header ROOM PREFAB

        #region Tooltip

        [Tooltip("The gameobject prefab for the room (this will contain all the tilemaps for the room and environment game objects")]

        #endregion Tooltip

        public GameObject prefab;


        #region Header ROOM CONFIGURATION

        [Space(10)]
        [Header("ROOM CONFIGURATION")]

        #endregion Header ROOM CONFIGURATION
        #region ToolTip
        [Tooltip("The element of the room. This will impact the content of the chest and which monsters are spawned")]
        #endregion
        public Element Element;
        #region Tooltip

        [Tooltip("The room type. The room types correspond to the room nodes used in the room node graph.")]

        #endregion Tooltip

        public RoomType RoomType;

        #region Tooltip

        [Tooltip("If you imagine a rectangle around the room tilemap that just completely encloses it, the room lower bounds represent the bottom left corner of that rectangle. This should be determined from the tilemap for the room (using the coordinate brush pointer to get the tilemap grid position for that bottom left corner (Note: this is the local tilemap position and NOT world position")]

        #endregion Tooltip

        public Vector2Int lowerBounds;

        #region Tooltip

        [Tooltip("If you imagine a rectangle around the room tilemap that just completely encloses it, the room upper bounds represent the top right corner of that rectangle. This should be determined from the tilemap for the room (using the coordinate brush pointer to get the tilemap grid position for that top right corner (Note: this is the local tilemap position and NOT world position")]

        #endregion Tooltip

        public Vector2Int upperBounds;

        #region Tooltip

        [Tooltip("There should be a maximum of four doorways for a room - one for each compass direction.  These should have a consistent 3 tile opening size, with the middle tile position being the doorway coordinate 'position'")]

        #endregion Tooltip

        [SerializeField] public List<Doorway> doorwayList;

        #region Tooltip

        [Tooltip("Each possible spawn position (used for enemies and chests) for the room in tilemap coordinates should be added to this array")]

        #endregion Tooltip

        public Vector2Int[] spawnPositionArray;

        /// <summary>
        /// Returns the list of Entrances for the room template
        /// </summary>
        public List<Doorway> GetDoorwayList()
        {
            return doorwayList;
        }
    }

}