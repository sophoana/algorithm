using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest.HackerRankContest
{
    public class Question1
    {
        public static int maxScore(List<int> a, int m)
        {
            // if a count == m return sum of a
            // Sort product by its value
            // Check the count of products, 
            // to determine the last segment products
            // if count % m == 0, divide them evenly
            // otherwise remaining item are in last segment

            if (a == null || !a.Any())
                return 0;

            int n = a.Count;
            int remain = n % m;
            int segments = n / m;
            int result = 0;
            a = a.OrderBy(x => x).ToList();
            for (int i = 0; i < segments; i++)
            {
                var cur = a.Skip(i * m).Take(m).Sum();
                if (i == segments - 1)
                {
                    cur = a.Skip(i * m).Take(m + remain).Sum();
                }

                result = (int) ((result + (i+ 1) * cur) % (Math.Pow(10, 9) + 7));
            }

            return result;
        }

        [Fact]
        public void TestMaxScore()
        {
            var input = new List<int>() {1, 5, 4, 2, 3};
            var result = maxScore(input, 2);
            Assert.Equal(27, result);
            
            input = new List<int>(){4,1,9,7};
            Assert.Equal(21, maxScore(input, 4));
            
            input = new List<int>(){1};
            Assert.Equal(1, maxScore(input, 1));
        }
    }
}