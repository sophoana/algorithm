using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class EasyQuestion
    {

        public int FindMaxConsecutiveOnes(int[] nums)
        {
            int max = 0;
            int k = 1;
            Queue<int> idx0 = new Queue<int>();
            for (int left = 0, right = 0; right < nums.Length; right++)
            {
                if(nums[right]==0)
                    idx0.Enqueue(right);
                if (idx0.Count > k)
                {
                    left = idx0.Dequeue() + 1;
                }

                max = Math.Max(max, right - left + 1);
            }

            return max;
        }

        //https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters/solution/
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            int n = s.Length;
            if (n * k == 0) return 0;

            int left = 0;
            int right = 0;
            var dict = new Dictionary<char, int>();
            int max = 1;
            while (right < n)
            {
                if (dict.ContainsKey(s[right]))
                    dict[s[right]] = right++;
                else 
                    dict.Add(s[right], right ++);

                if (dict.Count == k + 1)
                {
                    var idx = dict.Min(x => x.Value);
                    dict.Remove(s[idx]);

                    left = idx + 1;
                }

                max = Math.Max(max, right - left);
            }
            return max;
        }

        // https://leetcode.com/articles/longest-substring-with-at-most-two-distinct-charac/
        public int LengthOfLongestSubstring(string s)
        {
            int n = s.Length;
            if (n < 3) return n;
            int left = 0;
            int right = 0;
            Dictionary<char, int> map = new Dictionary<char, int>();
            int max = 2;
            while (right < n)
            {
                if (map.Count < 3)
                {
                    if (map.ContainsKey(s[right]))
                        map[s[right]] = right++;
                    else
                        map.Add(s[right], right++);
                }

                if (map.Count == 3)
                {
                    int idx = map.Min(x => x.Value);
                    map.Remove(s[idx]);
                    left = idx + 1;
                }

                max = Math.Max(max, right - left);
            }

            return max;
        }

        [Fact]
        public void TestLongestSubstring()
        {
            Assert.Equal(5, LengthOfLongestSubstring("ccaabbb"));
            Assert.Equal(3, LengthOfLongestSubstring("eceba"));
        }

        //https://leetcode.com/problems/edit-distance/solution/
        public int MinDistance(string word1, string word2)
        {
            int l1 = word1.Length;
            int l2 = word2.Length;

            // in case one of them is empty
            if (l1 * l2 == 0) return l1 + l2;

            // Create table l1 + 1 x l2 + 1
            int[,] D = new int[l1 + 1, l2 + 1];
            for (int i = 0; i <= l1; i++)
            {
                D[i, 0] = i;
            }

            for (int j = 0; j <= l2; j++)
                D[0, j] = j;

            for (int i = 1; i <= l1; i++)
            {
                for (int j = 1; j <= l2; j++)
                {
                    int left = D[i - 1, j] + 1;
                    int bottomLeft = D[i - 1, j - 1];
                    int bottom = D[i, j - 1] + 1;

                    if (word1[i - 1] != word2[j - 1])
                        bottomLeft++;

                    D[i, j] = Math.Min(left, Math.Min(bottom, bottomLeft));
                }
            }

            return D[l1, l2];
        }

        [Fact]
        public void TestMinDistance()
        {
            var word1 = "intention";
            var word2 = "execution";
            var result = MinDistance(word1, word2);

            Assert.Equal(5, result);
        }

        public bool IsOneEditDistance(string s, string t)
        {
            // assume s is short than t
            var lens = s.Length;
            var lent = t.Length;
            if (lens > lent) return IsOneEditDistance(t, s);

            var diff = Math.Abs(lens - lent);
            if (diff > 1) return false;

            for (int i = 0; i < lens; i++)
            {
                if (s[i] != t[i])
                {
                    if (lens == lent)
                        return s.Substring(i + 1).Equals(t.Substring(i + 1));
                    else return s.Substring(i).Equals(t.Substring(i + 1));
                }
            }

            return lens + 1 == lent;
        }

        [Fact]
        public void TestIsOneEditDistance()
        {
            Assert.True(IsOneEditDistance("ab", "abc"));
            Assert.True(IsOneEditDistance("ab", "acb"));
            Assert.False(IsOneEditDistance("cab", "ad"));
            Assert.True(IsOneEditDistance("1203", "1213"));
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0)
                return "";

            string prefix = strs[0];
            for (int i = 1; i < strs.Length; i++)
            {
                while (strs[i].IndexOf(prefix, StringComparison.Ordinal) != 0)
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (string.IsNullOrEmpty(prefix)) return "";
                }
            }

            return prefix;
        }

        public string[] ReorderLogFiles(string[] logs)
        {
            Array.Sort(logs, MyComparision);
            return logs;
        }

        [Fact]
        public void TestString()
        {
            var lets1 = string.Compare("let1", "let3");
            var result = "a8".CompareTo("g1");
            var ordinal = string.CompareOrdinal("a8", "g1");
        }

        private int MyComparision(string x, string y)
        {
            var logX = x.Split(' ');
            var logY = y.Split(' ');

            // Both number put in original order
            // Letter log is before digit log
            // both letter, order by identifier if both equal

            var idx = logX[0];
            var idy = logY[0];

            var isLogXNumber = int.TryParse(logX[1], out var nx);
            var isLogYNumber = int.TryParse(logY[1], out var ny);

            if (!isLogXNumber && !isLogYNumber)
            {
                int cmp = String.Compare(logX[1], logY[1], StringComparison.Ordinal);
                if (cmp != 0) return cmp;
                return string.Compare(idx, idy, StringComparison.Ordinal);
            }

            return isLogXNumber ? (isLogYNumber ? 0 : 1) : -1;
        }
    }
}