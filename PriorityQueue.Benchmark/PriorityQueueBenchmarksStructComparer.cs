using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace PriorityQueue
{
    public class BenchmarkStructComparer : IComparer<BenchmarkStruct>
    {
        public int Compare(BenchmarkStruct x, BenchmarkStruct y)
        {
            return x.Prioirity.CompareTo(y.Prioirity);
        }
    }
}