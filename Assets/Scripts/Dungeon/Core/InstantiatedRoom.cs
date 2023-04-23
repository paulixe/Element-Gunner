using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.PathFinding;

namespace PII.Dungeon
{
    /// <summary>
    /// Instantiated room:
    ///     - Has collider for detecting when we change from one room to another so we can change
    /// the current room in Dungeon Manager
    ///     - Manage doors state
    ///     - Reference to the different tilemaps
    /// 
    /// </summary>
    [RequireComponent(typeof(RoomAStar))]
    public class InstantiatedRoom : MonoBehaviour
    {
        [HideInInspector] public Room Room;
        [HideInInspector] public Grid Grid;
        [HideInInspector] Tilemap groundTilemap;
        [HideInInspector] Tilemap decoration1Tilemap;
        [HideInInspector] Tilemap decoration2Tilemap;
        [HideInInspector] public Tilemap FrontTilemap;
        [HideInInspector] public Tilemap CollisionTilemap;
        [HideInInspector] Tilemap minimapTilemap;
        [HideInInspector] public bool HasBeenVisited = false;


        [HideInInspector] public RoomAStar RoomAStar;
        List<Door> doors = new List<Door>();
        #region Initialization
        public void Initialize(Room room)
        {
            this.Room = room;
            PopulateVariables();
            BlockUnusedDoorways();
            SpawnDoors();
            DisableCollisionTilemapRendering();
        }
        private void SpawnDoors()
        {
            doors.Clear();

            if (Room.RoomType == RoomType.CorridorEW || Room.RoomType == RoomType.CorridorNS) return;
            float tileDistance = 1;

            foreach (Doorway doorway in Room.Doorways)
            {
                if (doorway.isConnected)
                {
                    GameObject door = Instantiate(doorway.doorPrefab, transform);
                    doors.Add(door.GetComponent<Door>());
                    switch (doorway.orientation)
                    {
                        case Orientation.north:
                            door.transform.localPosition = new Vector3(doorway.position.x + 0.5f * tileDistance, doorway.position.y + tileDistance);
                            break;
                        case Orientation.south:
                            door.transform.localPosition = new Vector3(doorway.position.x + 0.5f * tileDistance, doorway.position.y);
                            break;
                        case Orientation.east:
                            door.transform.localPosition = new Vector3(doorway.position.x + tileDistance, doorway.position.y + 1.25f * tileDistance);
                            break;
                        case Orientation.west:
                            door.transform.localPosition = new Vector3(doorway.position.x, doorway.position.y + 1.25f * tileDistance);
                            break;
                    }
                }
            }
        }
        private void PopulateVariables()
        {
            RoomAStar = GetComponent<RoomAStar>();
            Grid = GetComponentInChildren<Grid>();
            Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tilemap in tilemaps)
            {
                if (tilemap.gameObject.CompareTag(Settings.GROUND_TILEMAP_TAG))
                    groundTilemap = tilemap;
                else if (tilemap.gameObject.CompareTag(Settings.DECORATION1_TILEMAP_TAG))
                    decoration1Tilemap = tilemap;
                else if (tilemap.gameObject.CompareTag(Settings.DECORATION2_TILEMAP_TAG))
                    decoration2Tilemap = tilemap;
                else if (tilemap.gameObject.CompareTag(Settings.FRONT_TILEMAP_TAG))
                    FrontTilemap = tilemap;
                else if (tilemap.gameObject.CompareTag(Settings.COLLISION_TILEMAP_TAG))
                    CollisionTilemap = tilemap;
                else if (tilemap.gameObject.CompareTag(Settings.MINIMAP_TILEMAP_TAG))
                    minimapTilemap = tilemap;
            }
        }
        private void BlockUnusedDoorways()
        {
            foreach (Doorway doorway in Room.Doorways)
            {
                if (!doorway.isConnected)
                {
                    BlockDoorwayOnTilemap(groundTilemap, doorway);
                    BlockDoorwayOnTilemap(decoration1Tilemap, doorway);
                    BlockDoorwayOnTilemap(decoration2Tilemap, doorway);
                    BlockDoorwayOnTilemap(FrontTilemap, doorway);
                    BlockDoorwayOnTilemap(CollisionTilemap, doorway);
                    BlockDoorwayOnTilemap(minimapTilemap, doorway);
                }

            }
        }
        private void BlockDoorwayOnTilemap(Tilemap tilemap, Doorway doorway)
        {
            switch (doorway.orientation)
            {
                case Orientation.north:
                case Orientation.south:
                    BlockDoorwayOnTilemapHorizontally(tilemap, doorway);
                    break;
                case Orientation.east:
                case Orientation.west:
                    BlockDoorwayTilemapVertically(tilemap, doorway);
                    break;
            }
        }
        private void BlockDoorwayOnTilemapHorizontally(Tilemap tilemap, Doorway doorway)
        {
            Vector2Int startPosition = doorway.doorwayStartCopyPosition;

            for (int x = 0; x < doorway.doorwayCopyTileWidth; x++)
            {
                for (int y = 0; y < doorway.doorwayCopyTileHeight; y++)
                {
                    Vector3Int positonOfTileCopied = new(startPosition.x + x, startPosition.y - y, 0);
                    Vector3Int positionOfTilePast = positonOfTileCopied + Vector3Int.right;
                    Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(positonOfTileCopied);
                    tilemap.SetTile(positionOfTilePast, tilemap.GetTile(positonOfTileCopied));
                    tilemap.SetTransformMatrix(positionOfTilePast, transformMatrix);
                }
            }
        }
        private void BlockDoorwayTilemapVertically(Tilemap tilemap, Doorway doorway)
        {
            Vector2Int startPosition = doorway.doorwayStartCopyPosition;

            for (int x = 0; x < doorway.doorwayCopyTileWidth; x++)
            {
                for (int y = 0; y < doorway.doorwayCopyTileHeight; y++)
                {
                    Vector3Int positonOfTileCopied = new(startPosition.x + x, startPosition.y - y, 0);
                    Vector3Int positionOfTilePast = positonOfTileCopied + Vector3Int.down;
                    Matrix4x4 transformMatrix = tilemap.GetTransformMatrix(positonOfTileCopied);
                    tilemap.SetTile(positionOfTilePast, tilemap.GetTile(positonOfTileCopied));
                    tilemap.SetTransformMatrix(positionOfTilePast, transformMatrix);
                }
            }
        }
        private void DisableCollisionTilemapRendering()
        {
            CollisionTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
        }
        #endregion
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If the player triggered the collider
            if (collision.tag == Settings.PLAYER_TAG && this != DungeonManager.Instance.GetCurrentRoom())
            {
                // Call room changed event
                StaticEventHandler.ChangeRoom(this);
                HasBeenVisited = true;
            }
        }
        public void LockDoors()
        {
            foreach (Door door in doors)
                door.LockDoor();
        }
        public void OpenDoors()
        {
            foreach (Door door in doors)
                door.OpenDoor();
        }

        public Vector3 GetCenterPosition()
        {
            Vector3Int centerGridPosition = (Vector3Int)(Room.lowerBounds + Room.upperBounds) / 2;
            return Grid.CellToWorld(centerGridPosition);
        }
        public Vector3 GetClosestSpawningPositionTo(Vector3 position)
        {
            Vector2Int[] spawnArray = Room.RoomTemplate.spawnPositionArray;

            Vector3 closestSpawningPosition = Vector3.zero;
            float MinDistanceToPosition = 100000;

            foreach (Vector2Int spawnGridPosition in spawnArray)
            {
                Vector3 worldPosition = Grid.CellToWorld((Vector3Int)spawnGridPosition);
                float distance = Vector3.Distance(worldPosition, position);

                if (distance < MinDistanceToPosition)
                {
                    closestSpawningPosition = worldPosition;
                    MinDistanceToPosition = distance;
                }
            }

            return closestSpawningPosition;
        }
    }
}