using System.Collections.Generic;
using UnityEngine;

namespace Utilities.PathFinding
{
    /// <summary>
    /// <see cref="GridNode">Grid</see> used for the <see cref="AStar"/> algorithm.
    /// </summary>
    [System.Serializable]
    public class GridNode
    {
        Node[,] grid;
        bool[,] walkableMatrix;
        int[,] penaltyMatrix;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GridNode(int[,] penaltyMatrix, bool[,] walkableCells) : this(penaltyMatrix.GetLength(0), penaltyMatrix.GetLength(0))
        {
            if (penaltyMatrix.GetLength(0) != walkableCells.GetLength(0))
                Debug.LogWarning(nameof(penaltyMatrix) + " and " + nameof(walkableCells) + " must have the same length");
            if (penaltyMatrix.GetLength(1) != walkableCells.GetLength(1))
                Debug.LogWarning(nameof(penaltyMatrix) + " and " + nameof(walkableCells) + " must have the same height");
            this.penaltyMatrix = penaltyMatrix;
            this.walkableMatrix = walkableCells;
        }

        public GridNode(int[,] penaltyMatrix) : this(penaltyMatrix.GetLength(0), penaltyMatrix.GetLength(0))
        {
            this.penaltyMatrix = penaltyMatrix;
        }
        public GridNode(bool[,] walkableCells) : this(walkableCells.GetLength(0), walkableCells.GetLength(0))
        {
            this.walkableMatrix = walkableCells;
        }
        public GridNode(int width, int height)
        {
            Width = width;
            Height = height;
            grid = new Node[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    grid[x, y] = new Node(new(x, y));
        }
        public Node this[Vector2Int position] => grid[position.x, position.y];
        public bool IsInBounds(Vector2Int cell)
            => cell.x >= 0 && cell.y >= 0 & cell.x < Width && cell.y < Height;
        public int GetSpeedPenalty(Vector2Int position)
        {
            if (penaltyMatrix == null)
                return 0;
            else
                return penaltyMatrix[position.x, position.y];
        }
        public bool IsWalkable(Vector2Int position)
        {
            if (walkableMatrix == null)
                return true;
            else
                return walkableMatrix[position.x, position.y];
        }
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> nodes = new List<Node>();
            int X = node.Position.x;
            int Y = node.Position.y;
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                    if (i != 0 || j != 0)
                        if (X + i >= 0 && Y + j >= 0 && X + i < Width && Y + j < Height)
                            nodes.Add(grid[X + i, Y + j]);

            return nodes;
        }

    }
}