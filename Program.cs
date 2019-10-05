using System;

namespace algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[] { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0 };
            var result = SlidingWindow.LongestOnes(input, 2);
        }

        static void TestDietPlan()
        {
            var input = new int[] { 1, 3, 1, 3, 5, 3, 6, 7, -3, -1 };
            var result = SlidingWindow.MaxSlidingWindow(input, 3);

            input = new int[] { 6, 5, 0, 0 };
            var s = SlidingWindow.DietPlanPerformance(input, 2, 1, 5);
        }
    }
}
