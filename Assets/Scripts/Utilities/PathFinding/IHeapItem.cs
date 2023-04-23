using System;

namespace Utilities.PathFinding
{
    public interface IHeapItem<T> : IComparable<T>
    {
        public int HeapIndex { get; set; }
    }
}