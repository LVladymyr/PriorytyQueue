using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueueLinkedList<T> : IPriorityQueue<T>
    {
        private readonly LinkedList<T> heap;
        private readonly IComparer<T> comparer;

        public PriorityQueueLinkedList(IComparer<T> comparer)
        {
            this.heap = new LinkedList<T>();
            this.comparer = comparer;
        } 

        public void Enqueue(T item)
        {
            this.heap.AddLast(item);
            MoveUp();
        }

        public T Dequeue()
        {
            var value = this.heap.First.Value;
            Swap(this.heap.First, this.heap.Last);
            this.heap.RemoveLast();
            MoveDown();
            return value;
        }

        private void MoveUp()
        {
            var indx = this.heap.Count / 2;
            var last = this.heap.Last;
            var next = last;
            while (indx != 1 && this.comparer.Compare(last.Value, next.Value) > 0) 
            {
                Swap(last, next);
                indx /= 2;
                last = next;
                next = ScrollBackward(next, indx);
            }
        }

        private static LinkedListNode<T> ScrollForward(LinkedListNode<T> node, int count)
        {
            while (count-- > 0)
            {
                node = node.Next;
            }
            return node;
        }
        
        private static LinkedListNode<T> ScrollBackward(LinkedListNode<T> node, int count)
        {
            while (count-- > 0)
            {
                node = node.Previous;
            }
            return node;
        }
        
        private void MoveDown()
        {
            var indx = 1;
            var prev = this.heap.First;
            var cnt = this.heap.Count;
            while (prev != null && indx * 2 <= cnt)
            {
                var next = ScrollForward(prev, indx);
                indx *= 2;
                if (indx < this.heap.Count && this.comparer.Compare(next.Value, next.Next.Value) > 0)
                {
                    indx++;
                    next = ScrollForward(next, 1);
                }
                if (this.comparer.Compare(next.Value, prev.Value) > 0) break;
                Swap(prev, next);
                prev = next;
            }
        }

        private static void Swap(LinkedListNode<T> i1, LinkedListNode<T> i2)
        {
            T tmp = i1.Value;
            i1.Value = i2.Value;
            i2.Value = tmp;
        }
    }
}
