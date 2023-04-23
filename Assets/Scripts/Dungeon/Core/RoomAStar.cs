using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utilities.PathFinding;

namespace PII.Dungeon
{
    /// <summary>
    /// <list type="bullet">
    ///    <item>Create the gridNode for this room</item>
    ///    <item>Returns the path with a beginning postion and an end position (world position)</item>
    ///     </list>
    /// </summary>
    [RequireComponent(typeof(InstantiatedRoom))]
    public class RoomAStar : MonoBehaviour
    {
        GridNode gridNode;
        InstantiatedRoom room;
        private void Awake()
        {
            room = GetComponent<InstantiatedRoom>();
        }
        private void Start()
        {
            ResetGridNode();
        }
        public void ResetGridNode()
        {
            gridNode = new GridNode(CreateSpeedPenaltyMatrix(), CreateWalkableMatrix());
        }

        public Stack<Vector3> GetPath(Vector3 beginning, Vector3 end)
        {
            Vector3Int cellBeginning = room.Grid.WorldToCell(beginning);
            Vector3Int cellEnd = room.Grid.WorldToCell(end);


            Vector2Int cellBeginningRelativeToBottomLeft =
                new Vector2Int(cellBeginning.x, cellBeginning.y) - room.Room.RoomTemplate.lowerBounds;
            Vector2Int cellEndRelativeToBottomLeft =
                new Vector2Int(cellEnd.x, cellEnd.y) - room.Room.RoomTemplate.lowerBounds;

            cellBeginningRelativeToBottomLeft = ClampVectorToGrid(cellBeginningRelativeToBottomLeft);
            cellEndRelativeToBottomLeft = ClampVectorToGrid(cellEndRelativeToBottomLeft);

            Stack<Vector2Int> gridPath = AStar.FindPathInstance(gridNode, cellBeginningRelativeToBottomLeft, cellEndRelativeToBottomLeft);

            Stack<Vector3> worldPath = new Stack<Vector3>();
            foreach (Vector2Int cell in gridPath.Reverse())
            {

                Vector3 worldPos = room.Grid.CellToWorld((Vector3Int)(cell + room.Room.RoomTemplate.lowerBounds));
                worldPath.Push(worldPos);
            }

            return worldPath;
        }
        private Vector2Int ClampVectorToGrid(Vector2Int cell)
            => new(Mathf.Clamp(cell.x, 0, gridNode.Width - 1), Mathf.Clamp(cell.y, 0, gridNode.Height - 1));

        private int[,] CreateSpeedPenaltyMatrix()
        {
            Vector2Int gridSize = room.Room.upperBounds - room.Room.lowerBounds;
            int[,] penaltyMatrix = new int[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y) + room.Room.RoomTemplate.lowerBounds;
                    TileBase tile = room.CollisionTilemap.GetTile((Vector3Int)cellPos);

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
            Vector2Int gridSize = room.Room.upperBounds - room.Room.lowerBounds;
            bool[,] walkableMatrix = new bool[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Vector2Int cellPos = new Vector2Int(x, y) + room.Room.RoomTemplate.lowerBounds;
                    TileBase tile = room.CollisionTilemap.GetTile((Vector3Int)cellPos);

                    bool isWalkable = true;
                    if (tile == GameResources.Instance.CollisionTile)
                        isWalkable = false;

                    walkableMatrix[x, y] = isWalkable;
                }
            }
            return walkableMatrix;
        }
    }
}