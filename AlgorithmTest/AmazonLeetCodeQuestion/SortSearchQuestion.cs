using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class SortSearchQuestion
    {
        //https://leetcode.com/problems/two-sum-less-than-k/
        public int TwoSumLessThanK(int[] A, int K) {
            Array.Sort(A);
            int left = 0;
            int right = A.Length - 1;
            int result = -1;
            while (left < right)
            {
                int low = A[left];
                int high = A[right];
                if (low + high == K) return low + high;
                if (low + high > K) right--;
                else
                {
                    if (result < (low + high))
                        result = low + high;
                    left++;
                }
            }

            return result;
        }

        [Fact]
        public void Test_ClosestTwoSum()
        {
            var input = new int[] {34, 23, 1, 24, 75, 33, 54, 8};
            var result = TwoSumLessThanK(input, 60);
            Assert.Equal(58, result);
        }
        
        public int NumWaysDP(int n)
        {
            if (n == 1 || n == 0) return 1;
            int[] dp = new  int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            
            for (int i = 2; i < n + 1; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        // TODO
        public int NumWaysX(int n, int [] x)
        { 
            // Possible Steps 1,3,5
            if (n == 0) return 1;
            var dp = new int [n + 1];
            dp[0] = 1;
            for (int i = 1; i < n + 1; i++)
            {
                var total = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    if (i - x[j] >= 0) total += dp[i - x[j]];
                }

                dp[i] = total;
            }

            return dp[n];
        }
        
        public int NumWays(int n)
        {
            if (n == 0) return 1;
            if (n == 1) return 1;
            return NumWays(n - 1) + NumWays(n - 2);
        }
        
        //https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/solution/
        public int[] TwoSumSortedArray(int[] nums, int target)
        {
            int left = 0, right = nums.Length;
            while (left < right)
            {
                if (nums[left] + nums[right] == target)
                    return new int[] {left + 1, right + 1};
                else if (nums[left] + nums[right] > target)
                    right--;

                else left++;
            }

            return new int[] {-1, -1};
        }

        //https://leetcode.com/problems/remove-duplicates-from-sorted-array/
        public int RemoveDuplicates(int[] nums)
        {
            int idx = 0;
            int ptr = 0;

            while (++ptr < nums.Length)
            {
                if (nums[idx] == ptr)
                {
                    ptr++;
                }
                else if (ptr - idx > 0)
                {
                    nums[++idx] = nums[ptr];
                }
            }

            return idx;
        }

        public int MaxProfit(int[] prices)
        {
            int minPrice = int.MaxValue;
            int maxProfit = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minPrice)
                    minPrice = prices[i];
                else if (prices[i] - minPrice > maxProfit)
                {
                    maxProfit = prices[i] - minPrice;
                }
            }

            return maxProfit;
        }

        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/solution/
        public int MaxProfit2(int[] prices)
        {
            int maxprofit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i - 1])
                    maxprofit += prices[i] - prices[i - 1];
            }

            return maxprofit;
        }

        //https://leetcode.com/problems/relative-sort-array/
        public int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < arr2.Length; i++)
            {
                dict.Add(arr2[i], i);
            }

            var result = arr1.OrderBy(x => x, new RelativeSortComparer(dict));
            return result.ToArray();
        }

        public class RelativeSortComparer : IComparer<int>
        {
            private readonly IDictionary<int, int> _dictionary;

            public RelativeSortComparer(IDictionary<int, int> dictionary)
            {
                _dictionary = dictionary;
            }

            public int Compare(int x, int y)
            {
                if (_dictionary.ContainsKey(x) && _dictionary.ContainsKey(y))
                    return _dictionary[x].CompareTo(_dictionary[y]);
                else if (_dictionary.ContainsKey(x))
                    return -1;
                else if (_dictionary.ContainsKey(y))
                    return 1;
                return x.CompareTo(y);
            }
        }

        [Fact]
        public void Test_RelativeSortArray()
        {
            var arr1 = new int[] {2, 3, 1, 3, 2, 4, 6, 7, 9, 2, 19};
            var arr2 = new int[] {2, 1, 4, 3, 9, 6};
            var result = RelativeSortArray(arr1, arr2);
            var expected = new[] {2, 2, 2, 1, 4, 3, 3, 9, 6, 7, 19};
            Assert.Equal(expected, result);
        }

        //https://leetcode.com/explore/interview/card/amazon/79/sorting-and-searching/2992/
        public int Search(int[] nums, int target)
        {
            int start = 0, end = nums.Length;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (nums[mid] == target) return mid;

                // Left side is non-roate array
                if (nums[start] <= nums[mid])
                {
                    if (nums[start] <= target && target < nums[mid]) end = mid - 1;
                    else start = mid + 1;
                }
                else
                {
                    if (nums[mid] < target && target <= nums[end]) start = mid + 1;
                    else end = mid - 1;
                }
            }

            return -1;
        }
    }
}