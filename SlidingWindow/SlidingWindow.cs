using System;

namespace algorithm
{
    public class SlidingWindow
    {
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