using System;
using System.Linq;
using System.Numerics;
using Xunit;

namespace Tests
{
    public class SlidingWindowTest
    {
        
        public int FindContentChildren(int[] g, int[] s) {
            // https://leetcode.com/problems/assign-cookies/
            int answer = 0;
            Array.Sort(g);
            Array.Sort(s);

            int sIdx = 0;
            int gIdx = 0;

            while (sIdx < s.Length && gIdx < g.Length)
            {
                if (s[sIdx] >= g[gIdx])
                {
                    answer++;
                    sIdx++;
                    gIdx++;
                }
                else
                {
                    sIdx++;
                }
            }

            return answer;
        }
        
        public int MaxNumberOfApples(int[] arr)
        {
            //https://leetcode.com/problems/how-many-apples-can-you-put-into-the-basket/
            Array.Sort(arr);
            int answer = 0;
            int sum = 5000;
            for (int i = 0; i < arr.Length; i++)
            {
                sum = sum - arr[i];
                if (sum > 0) answer++;
                else break;
            }

            return answer;
        }

        public int[] PrevPermOpt1(int[] A)
        {
            //https://leetcode.com/problems/previous-permutation-with-one-swap/

            int startIdx = FindIdx(A);
            if (startIdx == A.Length - 1 || startIdx == A.Length - 2) return A;

            int secondMaxIdx = A.Take(startIdx + 1).Max();
            int idxMaxa = A.ToList().IndexOf(secondMaxIdx);

            return A;

            int FindMax(int[] arr, int idx, int f)
            {
                int idxMax = 0;
                int curMax = 0;
                while (idx <= arr.Length)
                {
                    if (arr[idx] > curMax)
                    {
                        curMax = arr[idx];
                        idxMax = idx;
                    }

                    idx++;
                }

                return idxMax;
            }

            int FindIdx(int[] arr)
            {
                int idx = 0;
                while (idx <= A.Length)
                {
                    if (A[idx] == 1) idx++;
                    else break;
                }

                return idx;
            }

            void Swap(int[] arr, int i, int j)
            {
                int tmp = arr[i];
                arr[i] = arr[j];
                arr[j] = tmp;
            }
        }

        public bool IsMonotonic(int[] A)
        {
            //https://leetcode.com/problems/monotonic-array/
            if (A.Length == 1) return true;
            int plus = 0;
            int minus = 0;
            for (int i = 1; i < A.Length; i++)
            {
                int diff = A[i] - A[i - 1];
                if (diff == 0)
                {
                    plus++;
                    minus++;
                }
                else if (diff > 0)
                {
                    plus++;
                }
                else
                {
                    minus++;
                }
            }

            return plus == A.Length - 1 || minus == A.Length - 1;
        }

        public int SumOfDigits(int[] A)
        {
            var min = A.ToList().Min();
            var str = min.ToString().ToCharArray().Sum(x => (int) x);

            return 1 - str % 2;
        }

        [Fact]
        public void Test1()
        {
            var input = new int[] {69, 48, 39, 9, 63, 31, 13, 38, 43, 42, 56, 45, 80, 11, 57, 10, 26, 23, 99, 47, 40};
            var answer = SumOfDigits(input);
        }

        public int MinFunction(int[] A)
        {
            int min = A[0];
            int i = 0;
            int j = A.Length;
            while (i <= j)
            {
                min = Math.Min(A[i], min);
                min = Math.Min(A[j], min);
                i++;
                j--;
            }

            return min;
        }
    }
}