using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Compatibility;
using Xunit;

namespace Tests
{
    public class DynamicProgrammingTest
    {
        public int ThreeSumClosest(int[] nums, int target)
        {
            //https://leetcode.com/problems/3sum-closest/

            return 0;
        }
        
        public int MaxDistance(IList<IList<int>> arrays) {
            //https://leetcode.com/problems/maximum-distance-in-arrays/
            int min = 10001;
            int max = -10001;

            foreach (var array in arrays)
            {
                min = Math.Min(min, array.Min());
                max = Math.Max(max, array.Max());
            }

            return Math.Abs(min - max);
        }

        [Fact]
        public void TestLongestOnes()
        {
            var intput = new int[] {1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0};
            var k = 2;
            var result = LongestOnes(intput, k);
            Assert.True(6 == result);
        }
        public int LongestOnes(int[] A, int K)
        {
            //https://leetcode.com/problems/max-consecutive-ones-iii/
            int i = 0;
            int j = 0;
            for (j = 0; j < A.Length; j++)
            {
                if (A[j] == 0) K--;
                if (K < 0 && A[i++] ==0) K++;
            }

            return j - i;
        }

        [Fact]
        public void TestIntersection()
        {
            var nums1 = new int[] {1, 2, 2, 1};
            var nums2 = new int[] {2, 2};
            var result = Intersection(nums1, nums2);
            Assert.True(new int[] {2} == result);
        }

        public int[] Intersection(int[] nums1, int[] nums2)
        {
            //https://leetcode.com/problems/intersection-of-two-arrays/
            var counter = new SortedList<int, int>();
            foreach (var n in nums1.Distinct())
            {
                if (counter.ContainsKey(n)) counter[n]++;
                else counter.Add(n, 1);
            }

            foreach (var n in nums2.Distinct())
            {
                if (counter.ContainsKey(n)) counter[n]++;
                else counter.Add(n, 1);
            }

            return counter.Where(x => x.Value == 2).Select(x => x.Key).ToArray();
        }

        public IList<int> ArraysIntersection(int[] arr1, int[] arr2, int[] arr3)
        {
            //https://leetcode.com/problems/intersection-of-three-sorted-arrays/
            var counter = new SortedList<int, int>();
            foreach (var a in arr1)
            {
                if (counter.ContainsKey(a)) counter[a]++;
                else counter.Add(a, 1);
            }

            foreach (var a in arr2)
            {
                if (counter.ContainsKey(a)) counter[a]++;
                else counter.Add(a, 1);
            }

            foreach (var a in arr3)
            {
                if (counter.ContainsKey(a)) counter[a]++;
                else counter.Add(a, 1);
            }

            var result = counter.Where(x => x.Value == 3).Select(x => x.Key).ToList();
            return result;
        }

        [Fact]
        public void TestHighFive()
        {
            //var input = new int[] {1, 7, 3, 6, 5, 6};
            var input = new int[] {-1, -1, -1, -1, -1, 0};
            var result = PivotIndex(input);
        }

        public int PivotIndex(int[] nums)
        {
            //https://leetcode.com/problems/find-pivot-index/
            var sum = 0;
            var leftSum = 0;
            foreach (var num in nums) sum += num;
            for (int i = 0; i < nums.Length; i++)
            {
                if (leftSum == sum - leftSum - nums[i]) return i;
                leftSum += nums[i];
            }
//            var left = new int[nums.Length];
//            var right = new int[nums.Length];
//
//            var sl = 0;
//            var sr = 0;
//
//            var i = 0;
//            var j = nums.Length - 1;
//
//            while (i < nums.Length && j >= 0)
//            {
//                sl += nums[i];
//                sr += nums[j];
//
//                left[i] = sl;
//                right[j] = sr;
//
//                i++;
//                j--;
//            }
//
//            j = 0;
//            i = nums.Length - 1;
//            while (j < nums.Length && i >= 0)
//            {
//                if (left[j] > right[i]) i--;
//                else if (left[j] < right[i]) j++;
//                else if (i + 1 == j - 1) return j + 1;
//                else
//                {
//                    i--;
//                    j++;
//                }
//            }

            return -1;
        }

        public bool CheckPossibility(int[] nums)
        {
            //https://leetcode.com/problems/non-decreasing-array/
            var sorted = nums.OrderBy(x => x).ToList();
            int up = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != sorted[i]) up++;
                if (up >= 2) break;
            }

            return up < 2;
        }

        public int[][] HighFive(int[][] items)
        {
            //https://leetcode.com/problems/high-five/
            var scores = new SortedDictionary<int, IList<int>>();
            foreach (var item in items)
            {
                var id = item[0];
                var score = item[1];

                if (scores.ContainsKey(id))
                    scores[id].Add(score);
                else
                {
                    scores.Add(id, new List<int>() {score});
                }
            }

            var answer = from dict in scores
                let s = (int) dict.Value.OrderBy(x => x).TakeLast(5).Average()
                select new int[2] {dict.Key, s};

            return answer.ToArray();
        }

        public bool IsSubsequence(string s, string t)
        {
            // https://leetcode.com/problems/is-subsequence/
            // s is sub of t

            if (string.IsNullOrEmpty(s)) return true;
            if (string.IsNullOrEmpty(t)) return false;

            int sIdx = 0;
            int tIdx = 0;

            while (sIdx < s.Length && tIdx < t.Length)
            {
                if (s[sIdx] == t[tIdx]) sIdx++;
                tIdx++;
            }

            return sIdx == s.Length;
        }
    }
}