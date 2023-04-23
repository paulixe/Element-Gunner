using PII.Dungeon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities;
using Utilities.PathFinding;
namespace PII
{

    /// <summary>
    /// Used for testing the A* algorithm ( press I to set the beginning tile and O to set the end)
    /// </summary>
    public class TestAStar : MonoBehaviour
    {
        private Vector2Int beginning;
        private Vector2Int end;
        [SerializeField] TileBase pathTile;
        private Tilemap pathTilemap;
        [SerializeField] private GridNode gridNode;
        InstantiatedRoom CurrentRoom => DungeonManager.Instance.GetCurrentRoom();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                SetBeginning();
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetEnd();
                Draw();
            }
        }
        private void OnEnable()
        {
            StaticEventHandler.OnRoomChange += OnRoomChange;
        }
        private void OnDisable()
        {
            StaticEventHandler.OnRoomChange -= OnRoomChange;
        }

        private void OnRoomChange(RoomChangedEventArgs roomChangedEventArgs)
        {
            SetPathTilemap(roomChangedEventArgs.CurrentRoom);
        }
        private void ClearTilemap()
        {
            pathTilemap.ClearAllTiles();
        }
        private void SetPathTilemap(InstantiatedRoom roomEntered)
        {
            Grid grid = roomEntered.gameObject.GetComponentInChildren<Grid>();
            Tilemap dd = roomEntered.FrontTilemap;
            pathTilemap = Instantiate(dd, grid.transform);
            gridNode = new GridNode(CreateSpeedPenaltyMatrix(), CreateWalkableMatrix());
        }
        private int[,] CreateSpeedPenaltyMatrix()
        {
            Vector2Int gridSize = CurrentRoom.Room.upperBounds - CurrentRoom.Room.lowerBounds;
            int[,] penaltyMatrix = new int[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y) + CurrentRoom.Room.RoomTemplate.lowerBounds;
                    TileBase tile = CurrentRoom.CollisionTilemap.GetTile((Vector3Int)cellPos);

                    int penalty = 30;
                    if (tile == GameResources.Instance.PreferredPathTile)
                        penalty = 0;

                    penaltyMatrix[x, y] = penalty;
                }
            }
            return penaltyMatrix;
        }
        private bool[,] CreateWalkableMatrix()
        {
            Vector2Int gridSize = CurrentRoom.Room.upperBounds - CurrentRoom.Room.lowerBounds;
            bool[,] walkableMatrix = new bool[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y) + CurrentRoom.Room.RoomTemplate.lowerBounds;
                    TileBase tile = CurrentRoom.CollisionTilemap.GetTile((Vector3Int)cellPos);

                    bool isWalkable = true;
                    if (tile == GameResources.Instance.CollisionTile)
                        isWalkable = false;

                    walkableMatrix[x, y] = isWalkable;
                }
            }
            return walkableMatrix;
        }
        private void SetBeginning()
        {
            Vector3Int beginning3D = pathTilemap.WorldToCell(CommonUtilities.GetMouseWorldPos());
            beginning = new Vector2Int(beginning3D.x, beginning3D.y) - CurrentRoom.Room.RoomTemplate.lowerBounds;
        }
        private void SetEnd()
        {
            Vector3Int end3D = pathTilemap.WorldToCell(CommonUtilities.GetMouseWorldPos());
            end = new Vector2Int(end3D.x, end3D.y) - CurrentRoom.Room.RoomTemplate.lowerBounds;
        }
        private void Draw()
        {
            ClearTilemap();
            Stack<Vector2Int> path = AStar.FindPathInstance(gridNode, beginning, end);
            while (path.TryPop(out Vector2Int cell))
            {
                pathTilemap.SetTile(new Vector3Int(cell.x, cell.y, 0) + (Vector3Int)CurrentRoom.Room.RoomTemplate.lowerBounds, pathTile);
            }
        }
    }
}