using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueueArray<T> : IPriorityQueue<T>
    {
        private readonly T[] heap;
        private readonly IComparer<T> comparer;
        private int indx;

        public PriorityQueueArray(IComparer<T> comparer, int maxSize)
        {
            this.indx = 0;
            this.heap = new T[maxSize + 1];
            this.comparer = comparer;
        } 

        public void Enqueue(T item) 
        {
            this.heap[++indx] = item;
            MoveUp();
        }

        public T Dequeue()
        {
            var value = this.heap[1];
            Swap(this.heap, 1, this.indx);
            this.indx--;
            MoveDown();
            this.heap[this.indx + 1] = default(T);
            return value;
        }

        private void MoveUp() 
        {
            var i = indx;
            while (i != 1 && this.comparer.Compare(this.heap[i / 2], this.heap[i]) > 0) 
            {
                Swap(this.heap, i / 2, i);
                i /= 2;
            }
        }

        private void MoveDown()
        {
            var i = 1;
            while (i * 2 <= this.indx)
            {
                var next = i * 2;
                if (next < this.indx && this.comparer.Compare(this.heap[next], this.heap[next + 1]) > 0) next++;
                if (this.comparer.Compare(this.heap[next], this.heap[i]) > 0) break;
                Swap(this.heap, next, i);
                i = next;
            }
        }

        private static void Swap(T[] a, int indx1, int indx2)
        {
            T tmp = a[indx1];
            a[indx1] = a[indx2];
            a[indx2] = tmp;
        }
    }
}
