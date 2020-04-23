using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal;
using Xunit;

namespace AlgorithmTest
{
    public class HackerRank
    {
        #region Array

        [Fact]
        public void TestRepeatString()
        {
            var str = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var n = 534802106762;

            var result = repeatedString(str, n);
        }
        
        public long repeatedString(string s, long n)
        {
            // aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            // 534802106762
            if (string.IsNullOrEmpty(s))
                return 0;
            
            var len = s.Length;
            if (len > n)
            {
                var sub = s.Substring(0, (int)n);
                return CountA(sub);
            }

            var count = CountA(s);
            var remaining = (int) (n % len);
            var factor = (n / len);

            return (count * factor) + CountA(s.Substring(0, remaining));
        }

        private long CountA(string s)
        {
            return s.ToCharArray().Count(x => x == 'a');
        }

        #endregion
        
        #region 2020-04-22

        private static IDictionary<char, char> _vowel = new Dictionary<char, char>
        {
            {'a', 'a'}, {'e', 'e'}, {'i', 'i'}, {'o', 'o'}, {'u', 'u'}
        };

        public static string findSubstring(string s, int k)
        {
            var charArr = s.ToCharArray();
            var count = charArr.Take(k).Count(x => _vowel.ContainsKey(x));
            var currMax = count;
            var maxStr = count == 0 ? "Not found!" : s.Substring(0, k);

            for (int i = k; i < s.Length; i++)
            {
                if (_vowel.ContainsKey(charArr[i - k])) count--;
                if (_vowel.ContainsKey(charArr[i])) count++;
                if (count > currMax)
                {
                    currMax = count;
                    maxStr = s.Substring((i - k) + 1, k);
                }
            }

            return maxStr;
        }

        public static int getBattery(List<int> events)
        {
            var cur = 50;
            foreach (var val in events)
            {
                cur += val;
                cur = Math.Max(cur, 100);
            }

            return cur;
        }

        [Fact]
        public void Test()
        {
            var input = "azerdii";
            var result = findSubstring(input, 5);
        }

        [Fact]
        public void TestMaxArea()
        {
            int w = 4, h = 3;
            List<bool> types = new List<bool> {true, true};
            List<int> dist = new List<int> {1, 3};
            var result = getMaxArea(w, h, types, dist);
        }


        public static List<long> getMaxArea(int w, int h, List<bool> boundaryType, List<int> boundaryDist)
        {
            if (boundaryDist.Count != boundaryType.Count)
                return new List<long>();

            var maxArea = new List<long>();
            var curV = h;
            var curH = w;

            for (int i = 0; i < boundaryDist.Count; i++)
            {
                if (boundaryType[i])
                {
                    // Vertical - Height
                    maxArea.Add(curH * Math.Min(curV, boundaryDist[i]));
                    curV = Math.Max(0, curV - boundaryDist[i]);
                }
                else
                {
                    // Horizontal - Width

                    maxArea.Add(Math.Min(curH, boundaryDist[i]) * curV);
                    curH = Math.Max(0, curH - boundaryDist[i]);
                }
            }

            return maxArea;
        }

        #endregion
    }
}