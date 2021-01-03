using System;
using System.Collections.Generic;
using Xunit;

namespace PriorityQueue
{
    public class PriorityQueueTests
    {

        [Fact]
        public void ArrayBased()
        {
            var sut = new PriorityQueueArray<int>(new MaxComparer(), 10);
            sut.Enqueue(15);
            sut.Enqueue(14);
            sut.Enqueue(11);
            sut.Enqueue(10);
            sut.Enqueue(12);
            Assert.Equal(15, sut.Dequeue());
            Assert.Equal(14, sut.Dequeue());
            Assert.Equal(12, sut.Dequeue());
            Assert.Equal(11, sut.Dequeue());
            Assert.Equal(10, sut.Dequeue());
        }
        
        [Fact]
        public void ListBased()
        {
            var sut = new PriorityQueueList<int>(new MaxComparer());
            sut.Enqueue(15);
            sut.Enqueue(14);
            sut.Enqueue(11);
            sut.Enqueue(10);
            sut.Enqueue(12);
            Assert.Equal(15, sut.Dequeue());
            Assert.Equal(14, sut.Dequeue());
            Assert.Equal(12, sut.Dequeue());
            Assert.Equal(11, sut.Dequeue());
            Assert.Equal(10, sut.Dequeue());
        }
        
        [Fact]
        public void LinkedListBased()
        {
            var sut = new PriorityQueueLinkedList<int>(new MaxComparer());
            sut.Enqueue(15);
            sut.Enqueue(14);
            sut.Enqueue(11);
            sut.Enqueue(10);
            sut.Enqueue(12);
            Assert.Equal(15, sut.Dequeue());
            Assert.Equal(14, sut.Dequeue());
            Assert.Equal(12, sut.Dequeue());
            Assert.Equal(11, sut.Dequeue());
            Assert.Equal(10, sut.Dequeue());
        }
        
        [Fact]
        public void TreeListBased()
        {
            var sut = new PriorityQueueTree<int>(new MaxComparer());
            sut.Enqueue(9);
            sut.Enqueue(15);
            sut.Enqueue(14);
            sut.Enqueue(11);
            sut.Enqueue(10);
            sut.Enqueue(12);
            Assert.Equal(15, sut.Dequeue());
            Assert.Equal(14, sut.Dequeue());
            Assert.Equal(12, sut.Dequeue());
            Assert.Equal(11, sut.Dequeue());
            Assert.Equal(10, sut.Dequeue());
            Assert.Equal(9, sut.Dequeue());
        }
        
        [Fact]
        public void TreeListBasedLarge()
        {
            var items = new List<int>();
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                items.Add(random.Next());
            }
            var pq = new PriorityQueueTree<int>(new MaxComparer());
            foreach (var item in items)
            {
                pq.Enqueue(item);
            }

            for (int i = 0; i < items.Count; i++)
            {
                pq.Dequeue();
            }
        }
        
        [Fact]
        public void SortedSetBased()
        {
            var sut = new PriorityQueueSortedSet<int>(new MaxComparer());
            sut.Enqueue(15);
            sut.Enqueue(14);
            sut.Enqueue(11);
            sut.Enqueue(10);
            sut.Enqueue(12);
            Assert.Equal(15, sut.Dequeue());
            Assert.Equal(14, sut.Dequeue());
            Assert.Equal(12, sut.Dequeue());
            Assert.Equal(11, sut.Dequeue());
            Assert.Equal(10, sut.Dequeue());
        }
    }
}
