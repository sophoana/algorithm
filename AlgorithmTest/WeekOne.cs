using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest
{
    public class WeekOne
    {
        public string Multiply(string num1, string num2)
        {
            return string.Empty;
        }

        public int alternate(string s)
        {
            var dict = new Dictionary<char, int>();
            int n1 = 0;
            int n2 = 0;
            foreach (char c in s.ToCharArray())
            {
                if (!dict.ContainsKey(c))
                    dict.Add(c, 1);
                else dict[c]++;

                // Update 2 higher
                var cur = dict[c];
                if (cur > n1) n1 = cur;
                else if (cur > n2) n2 = cur;
            }

            if (dict.Keys.Count < 2) return 0;
            return n1 + n2;
        }

        [Fact]
        public void TestAlternate()
        {
            var input = "beabeefeab";
            var result = alternate(input);
        }

        

        // [Fact]
        // public void TestCamelCase()
        // {
        //     var input = "saveChangesInTheEditor";
        //     var result = Camelcase(input);
        //
        //     Assert.True(result == 5);
        // }

        string twoStrings(string s1, string s2)
        {
            var hasSubString = false;
            var map1 = new HashSet<char>();
            for (int i = 0; i < s1.Length; i++)
            {
                if (!map1.Contains(s1[i]))
                    map1.Add(s1[i]);
            }

            hasSubString = s2.ToCharArray().Any(s => map1.Contains(s));

            return hasSubString ? "YES" : "NO";
        }

        [Fact]
        public void TestTwoStrings()
        {
            var s1 = "hello";
            var s2 = "world";

            var result = twoStrings(s1, s2);
        }

        //https://leetcode.com/problems/count-primes/
        public int CountPrimes(int n)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void TestCountPrime()
        {
            var input = 10;
            var expected = 4;
            var actual = CountPrimes(input);
            Assert.Equal(expected, actual);
        }

        //https://leetcode.com/problems/robot-return-to-origin/solution/
        public bool JudgeCircle(string moves)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] == 'U') y++;
                else if (moves[i] == 'D') y--;
                else if (moves[i] == 'L') x--;
                else if (moves[i] == 'R') x++;
            }

            return x == 0 && y == 0;
        }

        //https://www.hackerrank.com/challenges/2d-array/

        int hourglassSum(int[][] arr)
        {
            int max = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int cur = arr[i][j] + arr[i][j + 1] + arr[i][j + 2] +
                              arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2] + arr[i + 1][j + 1];

                    max = Math.Max(cur, max);

                    //Console.WriteLine(arr[i][0]);
                }
            }

            return max;
        }

        [Fact]
        public void TestHourGlassSum()
        {
            var arr = new int[6][]
            {
                new int[6] {1, 1, 1, 1, 1, 1},
                new int[6] {1, 1, 1, 1, 1, 1},
                new int[6] {1, 1, 1, 1, 1, 1},
                new int[6] {1, 1, 1, 1, 1, 1},
                new int[6] {1, 1, 1, 1, 1, 1},
                new int[6] {1, 1, 1, 1, 1, 1}
            };

            var result = hourglassSum(arr);
        }

        public string AddStrings(string num1, string num2)
        {
            int n = Math.Max(num1.Length, num2.Length);
            int idx1 = num1.Length - 1;
            int idx2 = num2.Length - 1;
            int carry = 0;
            string sum = string.Empty;
            for (int i = n - 1; i >= 0; i--)
            {
                int x = idx1 < 0 ? 0 : num1[idx1--] - '0';
                int y = idx2 < 0 ? 0 : num2[idx2--] - '0';
                int s = (x + y + carry) % 10;
                carry = (x + y + carry) / 10;

                sum = s + sum;
            }

            return carry != 0 ? carry + sum : sum;
        }

        public string AddStringsV1(string num1, string num2)
        {
            int len1 = num1.Length;
            int len2 = num2.Length;

            if (len1 > len2) return AddStringsV1(num2, num1);

            int carry = 0;
            string sum = string.Empty;

            for (int i = len1 - 1; i >= 0; i--)
            {
                int x = num1[i] - '0';
                int y = num2[i] - '0';

                sum = $"{(x + y + carry) % 10}{sum}";
                carry = (x + y + carry) / 10;
            }

            if (len1 != len2)
            {
                int idx = len2 - len1;
                while (--idx >= 0)
                {
                    int x = num2[idx] - '0';
                    sum = $"{(x + carry) % 10}{sum}";
                    carry = (x + carry) / 10;
                }
            }

            if (carry != 0)
            {
                sum = $"{carry}{sum}";
            }

            return sum;
        }

        [Fact]
        public void TestAddTwoNumber()
        {
            var result1 = AddStrings("9", "99");
            Assert.True(result1 == "108");
            Assert.Equal("20", AddStrings("1", "19"));
            Assert.Equal("20", AddStrings("19", "1"));
            Assert.Equal("1008", AddStrings("999", "9"));
        }

        public int countingValleys(int n, string s)
        {
            // Start and End at Sea Level - D and U equal
            // Pass a valley when Up to Sea Level - (D and U equal)
            // Not a valley when D to Sea Level

            // Edge Cases - DU and UD
            if (n == 2 && s[0] == 'D') return 1;
            if (n == 2 && s[1] == 'D') return 0;

            int valey = 0;
            int elevation = 0;
            for (int i = 0; i < n; i++)
            {
                if (s[i] == 'D') elevation++;
                else elevation--;

                if (elevation == 0 && s[i] == 'U') valey++;
            }

            return valey;
        }

        [Fact]
        public void TestCountValleys()
        {
            int n = 8;
            string s = "UDDDUDUU";
            var result = countingValleys(n, s);
            Assert.True(result == 1);
            Assert.True(countingValleys(2, "DU") == 1);
            Assert.Equal(0, countingValleys(2, "UD"));
        }

        public int jumpingOnCloud(int[] c)
        {
            int n = c.Length;
            if (n == 2) return 1;

            int idx = 0;
            int cnt = 0;
            for (int i = 0; i < n - 2; ++idx, i++)
            {
                if (c[i + 1] == 1 || c[i + 2] == 0)
                {
                    i++;
                    idx++;
                }

                cnt++;
            }

            if (idx == n - 2) cnt++;

            return cnt;
        }

        [Fact]
        public void TestJumpingOnCloud()
        {
            var input = new int[] {0, 0, 1, 0, 0, 1, 0};
            var result = jumpingOnCloud(input);
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var map = new Dictionary<string, List<string>>();
            foreach (var str in strs)
            {
                if (!map.ContainsKey(str))
                    map.Add(str, new List<string> {str});
            }

            foreach (var str in strs)
            {
                var reverse = new string(str.ToCharArray().Reverse().ToArray());
                if (map.ContainsKey(reverse))
                {
                    map[reverse].Add(str);
                }
            }

            IList<IList<string>> values = new List<IList<string>>();
            foreach (var key in map.Keys)
            {
                values.Add(map[key]);
            }

            return values;
        }

        private bool IsAnagram(string s1, string s2)
        {
            var reverse = s2.ToCharArray().Reverse();
            return s1.Equals(new string(reverse.ToArray()));
        }

        public int CountElements(int[] arr)
        {
            var set = new HashSet<int>();
            foreach (var a in arr)
                if (!set.Contains(a))
                    set.Add(a);

            var count = 0;
            foreach (var a in arr)
            {
                if (set.Contains(a + 1)) count++;
            }

            return count;
        }


        public void MoveZeroes(int[] nums)
        {
            var zero = nums.Count(x => x == 0);
            var arr = new int[nums.Length];
            var right = nums.Length - 1;
            var left = 0;
            for (int i = 0; i < nums.Length && left < right; i++)
            {
                if (nums[i] == 0) arr[right--] = 0;
                else arr[left++] = nums[i];
            }
        }

        public void MoveZeroes1(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;

            // Shift right pointer if It's already Zero
            while (right > 0)
            {
                if (nums[right] == 0) right--;
                else break;
            }

            while (left < right)
            {
                if (nums[right] == 0) right--;
                if (nums[left] == 0)
                {
                    int tmp = nums[left];
                    nums[left] = nums[right];
                    nums[right] = tmp;

                    right--;
                }

                left++;
            }
        }

        public int SingleNumber(int[] nums)
        {
            var set = new HashSet<int>();
            foreach (var num in nums)
            {
                if (set.Contains(num))
                    set.Remove(num);
                else
                {
                    set.Add(num);
                }
            }

            return set.FirstOrDefault();
        }
    }
}