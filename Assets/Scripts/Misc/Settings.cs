using UnityEngine;

namespace PII
{
    /// <summary>
    /// Hard coded values
    /// </summary>
    public static class Settings
    {
        #region Tags
        public const string PLAYER_TAG = "Player";

        public const string GROUND_TILEMAP_TAG = "groundTilemap";
        public const string DECORATION1_TILEMAP_TAG = "decoration1Tilemap";
        public const string DECORATION2_TILEMAP_TAG = "decoration2Tilemap";
        public const string FRONT_TILEMAP_TAG = "frontTilemap";
        public const string COLLISION_TILEMAP_TAG = "collisionTilemap";
        public const string MINIMAP_TILEMAP_TAG = "minimapTilemap";
        #endregion

        #region AnimatorStates
        public static int doorOpen = Animator.StringToHash("open");
        public static int aimUp = Animator.StringToHash("aimUp");
        public static int aimUpRight = Animator.StringToHash("aimUpRight");
        public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
        public static int aimRight = Animator.StringToHash("aimRight");
        public static int aimLeft = Animator.StringToHash("aimLeft");
        public static int aimDown = Animator.StringToHash("aimDown");
        public static int isIdle = Animator.StringToHash("isIdle");
        public static int isMoving = Animator.StringToHash("isMoving");
        public static int rollUp = Animator.StringToHash("rollUp");
        public static int rollRight = Animator.StringToHash("rollRight");
        public static int rollLeft = Animator.StringToHash("rollLeft");
        public static int rollDown = Animator.StringToHash("rollDown");
        #endregion

        #region others
        public static Vector2 TileOffset = new Vector2(0.5f, 0.5f);
        public static Vector2 AmmoSpawnPos = new(1.115f, 0.277f);
        #endregion
    }
}