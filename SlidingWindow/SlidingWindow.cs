using System;
using System.Linq;

namespace algorithm
{
    public class SlidingWindow
    {

        // https://leetcode.com/problems/max-consecutive-ones-iii/
        public static int LongestOnes(int[] A, int K)
        {
            int i = 0, j;
            for (j = 0; j < A.Length; ++j)
            {
                if (A[j] == 0) K--;
                if (K < 0 && A[i++] == 0) K++;
            }
            return j - i;
        }

        //https://leetcode.com/problems/minimum-swaps-to-group-all-1s-together/
        public static int MinSwaps(int[] data)
        {
            // Find Size of Windows

            int size = data.Count(x => x.Equals(1));
            int answer = data.Take(size).Count(x => x == 0);
            int cur = answer;

            for (int i = size; i < data.Length; i++)
            {
                cur += data[i] == 1 ? 0 : 1;
                cur -= data[i - size] == 1 ? 0 : 1;
                answer = Math.Min(cur, answer);
            }

            return answer;
        }

        //https://leetcode.com/problems/diet-plan-performance/
        /// <summary>
        /// Input: calories = [3,2], k = 2, lower = 0, upper = 1
        /// Output: 1
        ///
        /// Input: calories = [6,5,0,0], k = 2, lower = 1, upper = 5
        /// Output: 0
        ///
        /// 1 <= k <= calories.length <= 10^5
        /// 0 <= calories[i] <= 20000
        /// 0 <= lower <= upper
        /// </summary>
        public static int DietPlanPerformance(int[] calories, int k, int lower, int upper)
        {
            int result = 0;
            int n = calories.Length;
            var sum = new int[n];
            var s = 0;

            for (int i = 0; i < n; i++)
            {
                s += calories[i];
                sum[i] = s;
            }

            for (int i = 0; i <= n - k; i++)
            {
                int j = i + k - 1;
                int cur = sum[j];
                if (i - 1 >= 0)
                    cur = cur - sum[i - 1];

                if (cur > upper) result++;
                if (cur < lower) result--;
            }

            return result;
        }

        public static int[] MaxSlidingWindow(int[] input, int k)
        {
            int[] result = new int[input.Length - k + 1];
            for (int i = 0; i <= input.Length - k; i++)
            {
                var max = input[i];
                for (int j = 1; j < k; j++)
                {
                    if (input[i + j] > max) max = input[i + j];
                }

                result[i] = max;
            }

            return result;
        }

        public static int[] MinSlidingWindow(int[] input, int k)
        {
            int[] result = new int[input.Length - k + 1];
            for (int i = 0; i <= input.Length - k; i++)
            {
                var min = input[i];
                for (int j = 1; j < k; j++)
                {
                    if (input[i + j] < min) min = input[i + j];
                }

                result[i] = min;
            }

            return result;
        }
    }

}