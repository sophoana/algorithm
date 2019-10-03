using System;

namespace algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[] { 1, 3, 1, 3, 5, 3, 6, 7, -3, -1 };
            var result = SlidingWindow.MaxSlidingWindow(input, 3);
        }
    }
}
