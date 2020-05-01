using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest.HackerRankContest
{
    public class Question2
    {
        public static List<int> getMaxCharCount(string s, List<List<int>> queries)
        {
            // queries is a n x 2 array where queries[i][0] and queries[i][1]
            // represents x[i] and y[i] for the ith query.

            var result = new List<int>();
            foreach (var query in queries)
            {
                var startIdx = query[0];
                var length = Math.Abs(query[1] - query[0]) + 1;
                var sub = s.Substring(startIdx, length);
                result.Add(CountMaxChar(sub));
            }

            return result;
        }

        private static int CountMaxChar(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            var ch = s.OrderBy(char.ToLower).Last();
            var count = s.Count(x => char.ToLowerInvariant(ch) == char.ToLowerInvariant(x));
            return count;
        }

        [Fact]
        public void TestMaxCharCount()
        {
            var str = "aAabBcba";
            var queries = new List<List<int>>()
            {
                new List<int> {2, 6},
                new List<int> {1, 2},
                new List<int> {2, 2},
                new List<int> {0, 4},
                new List<int> {0, 7}
            };

            var expected = new List<int> {1, 2, 1, 2, 1};
            var result = getMaxCharCount(str, queries);
            Assert.Equal(expected, result);
        }
    }
}