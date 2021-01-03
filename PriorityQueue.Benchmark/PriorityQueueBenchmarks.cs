using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace PriorityQueue
{
    [MemoryDiagnoser]
    public class PriorityQueueBenchmarks
    {
        private List<BenchmarkStruct> randomItems = new List<BenchmarkStruct>();
        private IComparer<BenchmarkStruct> maxComparer;

        [Params(1000, 10000)]
        public int N;
        
        [GlobalSetup]
        public void Setup()
        {
            maxComparer = new BenchmarkStructComparer();
            var random = new Random();
            for (int i = 0; i < N; i++)
            {
                var @struct = new BenchmarkStruct();
                @struct.Prioirity = random.Next();
                randomItems.Add(@struct);
            }
        }

        [Benchmark]
        public void Array()
        {
            var pq =  new PriorityQueueArray<BenchmarkStruct>(maxComparer, N);
            foreach (var item in randomItems)
            {
                pq.Enqueue(item);
            }
        
            for (int i = 0; i < randomItems.Count; i++)
            {
                pq.Dequeue();
            }
        }
        
        [Benchmark]
        public void List()
        {
            var pq =  new PriorityQueueList<BenchmarkStruct>(maxComparer);
            foreach (var item in randomItems)
            {
                pq.Enqueue(item);
            }
        
            for (int i = 0; i < randomItems.Count; i++)
            {
                pq.Dequeue();
            }
        }
        
        [Benchmark]
        public void SortedSet()
        {
            var pq =  new PriorityQueueSortedSet<BenchmarkStruct>(maxComparer);
            foreach (var item in randomItems)
            {
                pq.Enqueue(item);
            }
        
            for (int i = 0; i < randomItems.Count; i++)
            {
                pq.Dequeue();
            }
        }
        
        // [Benchmark]
        // public void Tree()
        // {
        //     var pq = pqTree;
        //     foreach (var item in randomItems)
        //     {
        //         pq.Enqueue(item);
        //     }
        //
        //     for (int i = 0; i < randomItems.Count; i++)
        //     {
        //         pq.Dequeue();
        //     }
        // }
        
        [Benchmark]
        public void LinkedList()
        {
            var pq =  new PriorityQueueLinkedList<BenchmarkStruct>(maxComparer);
            foreach (var item in randomItems)
            {
                pq.Enqueue(item);
            }
        
            for (int i = 0; i < randomItems.Count; i++)
            {
                pq.Dequeue();
            }
        }
    }
}