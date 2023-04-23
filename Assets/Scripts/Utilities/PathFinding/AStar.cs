using System.Collections.Generic;
using UnityEngine;

namespace Utilities.PathFinding
{
    /// <summary>
    /// A* algorithm, it uses a heap for the open nodes
    /// </summary>
    public static class AStar
    {
        public static Stack<Vector2Int> FindPathInstance(GridNode grid, Vector2Int beginning, Vector2Int goal)
        {
            Heap<Node> openPoints = new Heap<Node>(grid.Width * grid.Height);
            HashSet<Node> closedPoints = new HashSet<Node>();

            grid[beginning].GCost = 0;
            grid[beginning].HCost = Getdistance(goal, beginning);
            grid[beginning].Parent = null;

            openPoints.Add(grid[beginning]);

            while (openPoints.Count > 0)
            {
                Node current = openPoints.RemoveFirst();
                closedPoints.Add(current);

                if (Getdistance(current.Position, goal) <= 0)
                    return CreatePathStack(current);
                AddNeighbours(current, grid, goal, openPoints, closedPoints);

            }
            return null;

        }
        private static void AddNeighbours(Node node, GridNode grid, Vector2Int goal, Heap<Node> openPoints, HashSet<Node> closedPoints)
        {
            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (grid.IsWalkable(neighbour.Position) && !(closedPoints.Contains(neighbour)))
                {
                    int newMovementCostToNeighbour = Getdistance(node.Position, neighbour.Position) + node.GCost + grid.GetSpeedPenalty(neighbour.Position);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openPoints.Contains(neighbour))
                    {

                        neighbour.GCost = newMovementCostToNeighbour;

                        neighbour.HCost = Getdistance(neighbour.Position, goal);

                        neighbour.Parent = node;

                        if (!openPoints.Contains(neighbour))
                            openPoints.Add(neighbour);

                    }
                }
            }
        }
        private static Stack<Vector2Int> CreatePathStack(Node targetNode)
        {
            Stack<Vector2Int> movementPathStack = new Stack<Vector2Int>();
            Node nextNode = targetNode;

            while (nextNode != null)
            {
                movementPathStack.Push(nextNode.Position);

                nextNode = nextNode.Parent;
            }

            return movementPathStack;
        }
        public static int Getdistance(Vector2Int v, Vector2Int w)
        {
            int distX = Mathf.Abs(v.x - w.x);
            int distY = Mathf.Abs(v.y - w.y);
            if (distX > distY)
                return 14 * distY + 10 * (distX - distY);
            else
                return 14 * distX + 10 * (distY - distX);
        }
    }
}