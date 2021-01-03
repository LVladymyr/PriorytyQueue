using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueueList<T> : IPriorityQueue<T>
    {
        private readonly List<T> heap;
        private readonly IComparer<T> comparer;

        public PriorityQueueList(IComparer<T> comparer)
        {
            this.heap = new List<T>() {default(T)};
            this.comparer = comparer;
        } 

        public void Enqueue(T item) 
        {
            this.heap.Add(item);
            MoveUp();
        }

        public T Dequeue()
        {
            var value = this.heap[1];
            Swap(this.heap, 1, this.heap.Count - 1);
            this.heap.RemoveAt(this.heap.Count - 1);
            MoveDown();
            return value;
        }

        private void MoveUp()
        {
            var i = this.heap.Count - 1;
            while (i != 1 && this.comparer.Compare(this.heap[i / 2], this.heap[i]) > 0) 
            {
                Swap(this.heap, i / 2, i);
                i /= 2;
            }
        }

        private void MoveDown()
        {
            var i = 1;
            while (i * 2 <= this.heap.Count - 1)
            {
                var next = i * 2;
                if (next < this.heap.Count - 1 && this.comparer.Compare(this.heap[next], this.heap[next + 1]) > 0) next++;
                if (this.comparer.Compare(this.heap[next], this.heap[i]) > 0) break;
                Swap(this.heap, next, i);
                i = next;
            }
        }

        private static void Swap(List<T> a, int indx1, int indx2)
        {
            T tmp = a[indx1];
            a[indx1] = a[indx2];
            a[indx2] = tmp;
        }
    }
}
