using System.Collections.Generic;

namespace PriorityQueue
{
    public class MaxComparer : IComparer<int> 
    {
        public int Compare(int first, int second) 
        {
            return second - first;
        }
    }
}
