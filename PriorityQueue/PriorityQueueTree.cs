using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueueTree<T> : IPriorityQueue<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Head { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Next { get; set; }
        }


        private Node heap;
        private Node activeLayer;
        private IComparer<T> comparer;

        public PriorityQueueTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
            this.heap = null;
        }

        public void Enqueue(T item)
        {
            var node = Append(item);
            MoveUp(node);
        }

        public T Dequeue()
        {
            var value = this.heap.Value;
            var child = GetLatestChild();
            if (child != null)
            {
                Swap(heap, child);
                Remove(child);
                MoveDown();
            }

            return value;
        }

        private void MoveDown()
        {
            var prev = this.heap;
            while (prev.Left != null)
            {
                Node next = prev.Left;
                if (prev.Right != null && prev.Left != null)
                {
                    next = comparer.Compare(prev.Right.Value, next.Value) > 0 ? next : prev.Right;
                }

                if (comparer.Compare(next.Value, prev.Value) > 0) break;
                Swap(next, prev);
                prev = next;
            }
        }

        private void Remove(Node node)
        {
            // clean up next ptr
            Node child;
            if (activeLayer != null)
            {
                if (activeLayer.Left != node)
                {
                    child = activeLayer.Left;
                }
                else
                {
                    child = activeLayer;
                }

                while (child.Next != node && child.Next != null)
                {
                    child = child.Next;
                }

                child.Next = null;
            }

            // clean up left / right ptr
            if (node.Head != null)
            {
                var parent = node.Head;
                if (parent.Left == node)
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }
            }
        }

        private void MoveUp(Node node)
        {
            while (node.Head != null && comparer.Compare(node.Head.Value, node.Value) > 0)
            {
                Swap(node, node.Head);
                node = node.Head;
            }
        }

        private Node Append(T item)
        {
            var node = new Node() {Value = item};
            if (heap == null && activeLayer == null)
            {
                heap = activeLayer = node;
                return node;
            }

            var parent = activeLayer;
            while (parent.Left != null && parent.Right != null && parent.Next != null)
            {
                if (parent.Next != null)
                {
                    parent = parent.Next;
                }
                else
                {
                    parent = activeLayer = activeLayer.Left;
                }
            }

            if (parent.Left == null)
            {
                parent.Left =  node;
            }
            else
            {
                parent.Right = node;
            }

            node.Head = parent;
            var childLayer = GetLatestChild();
            if (childLayer != node)
            {
                childLayer.Next = node;
            }

            return node;
        }

        private Node GetLatestChild()
        {
            if (activeLayer == null)
            {
                return null;
            }

            Node child;
            if (activeLayer.Left != null)
            {
                child = activeLayer.Left;
            }
            else
            {
                child = activeLayer;
                activeLayer = activeLayer.Head;
            }

            while (child.Next != null)
            {
                child = child.Next;
            }

            return child;
        }

        private static void Swap(Node i1, Node i2)
        {
            T tmp = i1.Value;
            i1.Value = i2.Value;
            i2.Value = tmp;
        }
    }
}
