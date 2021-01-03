using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueueSortedSet<T> : IPriorityQueue<T>
    {
        private readonly SortedSet<T> heap;

        public PriorityQueueSortedSet(IComparer<T> comparer)
        {
            this.heap = new SortedSet<T>(comparer);
        }

        public void Enqueue(T item)
        {
            this.heap.Add(item);
        }

        public T Dequeue()
        {
            var value = this.heap.Min;
            this.heap.Remove(value);
            return value;
        }
    }
}
