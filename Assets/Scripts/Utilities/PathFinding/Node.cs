using UnityEngine;

namespace Utilities.PathFinding
{
    /// <summary>
    /// Node used in the <see cref="AStar"/> algorithm
    /// </summary>
    public class Node : IHeapItem<Node>, INode<Node>
    {
        public int GCost { get; set; }
        public int HCost { get; set; }

        public int HeapIndex { get; set; }

        private Node parent;
        public Node Parent { get => parent; set { if (value == this) Debug.LogError("the parent node can't be himself"); parent = value; } }

        public Vector2Int Position
        {
            get; private set;
        }
        public int FCost => GCost + HCost;



        public int CompareTo(Node other)
        {
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = HCost.CompareTo(other.HCost);
            }
            return -compare;  // " - " because lower values have higher priority
        }
        public Node(Vector2Int position)
        {
            Position = position;
        }
    }
}