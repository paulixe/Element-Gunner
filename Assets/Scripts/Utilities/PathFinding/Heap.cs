namespace Utilities.PathFinding
{
    /// <summary>
    /// Heap used in the <see cref="AStar"/> algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Heap<T> where T : IHeapItem<T>
    {
        private int currentItemCount;
        public T[] heap;

        public int Count { get { return currentItemCount; } }
        public Heap(int maxHeapSize)
        {
            heap = new T[maxHeapSize];
        }
        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            heap[currentItemCount] = item;
            Sortup(item);
            currentItemCount++;
        }
        public T RemoveFirst()
        {
            T firstItem = heap[0];
            currentItemCount--;
            heap[0] = heap[currentItemCount];
            heap[0].HeapIndex = 0;
            SortDown(heap[0]);
            return firstItem;
        }
        public void Update(T item)
        {
            Sortup(item);
        }
        public bool Contains(T item)
        {

            return Equals(heap[item.HeapIndex], item) && item.HeapIndex < currentItemCount;

        }
        private void SortDown(T item)
        {
            int childIndexRight;
            int childIndexLeft;
            int swapIndex;
            while (true)
            {
                childIndexLeft = item.HeapIndex * 2 + 1;
                childIndexRight = item.HeapIndex * 2 + 2;

                if (childIndexLeft < currentItemCount)    //left child exists
                {
                    swapIndex = childIndexLeft;
                    if (childIndexRight < currentItemCount)  //right child exists
                    {
                        if (heap[childIndexLeft].CompareTo(heap[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }
                    if (item.CompareTo(heap[swapIndex]) < 0)
                    {
                        Swap(heap[swapIndex], item);
                    }
                    else  //already sorted
                    {
                        return;
                    }
                }
                else   //no childs
                {
                    return;
                }

            }

        }
        private void Sortup(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parentItem = heap[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }
        private void Swap(T itemA, T itemB)
        {
            heap[itemA.HeapIndex] = itemB;
            heap[itemB.HeapIndex] = itemA;
            int itemAheapIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAheapIndex;

        }
        public override string ToString()
        {
            string res = "";
            foreach (T n in heap)
            {
                if (n != null)
                {
                    res += n + "\n";
                }
            }
            return res;
        }
        public void Clear()
        {
            currentItemCount = 0;
        }
    }
}