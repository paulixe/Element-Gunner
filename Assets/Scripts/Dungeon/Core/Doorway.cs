using UnityEngine;
namespace PII.Dungeon
{
    /// <summary>
    /// Represents a doorway in a room template, there isn't necessary a door for each doorway
    /// </summary>
    [System.Serializable]
    public class Doorway
    {
        #region Tooltip
        [Tooltip("Position of the doorway relative to the room")]
        #endregion
        public Vector2Int position;
        #region Tooltip
        [Tooltip("N,S,W,E ?")]
        #endregion
        public Orientation orientation;
        #region Tooltip
        [Tooltip("Door prefab that will be instantied for this doorway")]
        #endregion
        public GameObject doorPrefab;
        #region Tooltip
        [Tooltip("The Upper Left Position To Start Copying From")]
        #endregion
        public Vector2Int doorwayStartCopyPosition;
        #region Tooltip
        [Tooltip("The width of tiles in the doorway to copy over")]
        #endregion
        public int doorwayCopyTileWidth;
        #region Tooltip
        [Tooltip("The height of tiles in the doorway to copy over")]
        #endregion
        public int doorwayCopyTileHeight;

        [HideInInspector]
        public bool isConnected = false;
        [HideInInspector]
        public bool isUnavailable = false;

        public Doorway(Doorway doorwayToCopy)
        {
            position = doorwayToCopy.position;
            orientation = doorwayToCopy.orientation;
            doorPrefab = doorwayToCopy.doorPrefab;
            doorwayStartCopyPosition = doorwayToCopy.doorwayStartCopyPosition;
            doorwayCopyTileWidth = doorwayToCopy.doorwayCopyTileWidth;
            doorwayCopyTileHeight = doorwayToCopy.doorwayCopyTileHeight;
        }
    }
}
