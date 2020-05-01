using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest
{
    public class AmazonQuestion
    {
        public bool IsNumber(string s)
        {
            return false;
        }

        //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/902/
        public string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
                return "";

            var dictT = new Dictionary<char, int>();
            foreach (var cht in t.ToCharArray())
            {
                if (dictT.ContainsKey(cht)) dictT[cht]++;
                else dictT.Add(cht, 1);
            }

            int requiredSize = dictT.Count;
            int left = 0, right = 0;
            int formed = 0;

            var dictCounts = new Dictionary<char, int>();
            int[] answer = {-1, 0, 0};
            while (right < s.Length)
            {
                char c = s[right];
                if (dictCounts.ContainsKey(c)) dictCounts[c]++;
                else dictCounts.Add(c, 1);

                if (dictT.ContainsKey(c) && dictCounts[c] == dictT[c]) formed++;

                while (left <= right && formed == requiredSize)
                {
                    c = s[left];
                    if (answer[0] == -1 || right - left + 1 < answer[0])
                    {
                        answer[0] = right - left + 1;
                        answer[1] = left;
                        answer[2] = right;
                    }

                    dictCounts[c]--;
                    if (dictT.ContainsKey(c) && dictCounts[c] == dictT[c]) formed--;

                    left++;
                }

                right++;
            }

            return answer[0] == -1 ? "" : s.Substring(answer[1], 1 + answer[2] - answer[1]);
        }


        //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/2970/
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var map = new Dictionary<string, List<string>>();
            foreach (var str in strs)
            {
                var sorted = str.ToCharArray().OrderBy(x => x);
                var key = new string(sorted.ToArray());
                if (map.ContainsKey(key))
                {
                    map[key].Add(str);
                }
                else
                {
                    map.Add(key, new List<string> {str});
                }
            }

            var result = new List<IList<string>>();
            foreach (var key in map.Keys)
            {
                result.Add(map[key]);
            }

            return result;
        }

        [Fact]
        public void Test_GroupAnagrams()
        {
            var input = new string[] {"eat", "tea", "tan", "ate", "nat", "bat"};
            IList<IList<string>> result = GroupAnagrams(input);
        }

        //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/2961/
        public int LengthOfLongestSubstringUsingSet(string s)
        {
            int n = s.Length;
            var set = new HashSet<char>();
            int ans = 0, i = 0, j = 0;
            while (i < n && j < n)
            {
                if (!set.Contains(s[j]))
                {
                    set.Add(s[j++]);
                    ans = Math.Max(ans, j - i);
                }
                else
                {
                    set.Remove(s[i++]);
                }
            }

            return ans;
        }

        [Fact]
        public void Test_LengthOfLongestSubstringUsingSet()
        {
            var input = "abcedefghijk";
            var output = LengthOfLongestSubstringUsingSet(input);
            Assert.Equal(8, output);
        }

        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s)) return true;
            var dict = new Dictionary<char, char>
            {
                {')', '('}, {'}', '{'}, {']', '['}
            };

            var stack = new Stack<char>();
            foreach (var t in s)
            {
                if (dict.ContainsKey(t))
                {
                    if (stack.Count == 0 || dict[t] != stack.Peek()) return false;
                    stack.Pop();
                }
                else
                {
                    stack.Push(t);
                }
            }

            return stack.Count == 0;
        }

        [Fact]
        public void TestIsValid()
        {
            var input = "(){}[]";
            var result = IsValid(input);
            // Assert.True(result);
            Assert.False(IsValid("]["));
        }

        public bool ValidPalindrome(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    int j = s.Length - 1 - i;
                    return IsPalindromeRange(s, i + 1, j) ||
                           IsPalindromeRange(s, i, j - 1);
                }
            }

            return true;
        }

        private bool IsPalindromeRange(string s, int i, int j)
        {
            for (int k = i; k < i + (j - i) / 2; k++)
            {
                if (s[k] != s[j - k + i]) return false;
            }

            return true;
        }

        //https://leetcode.com/problems/valid-palindrome/
        public bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            var str = s.ToCharArray()
                .Where(x => !char.IsPunctuation(x) && char.IsLetterOrDigit(x))
                .Select(x => x)
                .ToArray();

            if (str.Length == 0 || str.Length == 1)
                return true;

            var left = 0;
            var right = str.Length - 1;
            while (left < right)
            {
                if (char.ToLower(str[left++]) != char.ToLower(str[right--]))
                    return false;
            }

            return true;
        }

        [Fact]
        public void TestValidPalindrome()
        {
            var input = "A man, a plan, a canal: Panama";
            var result = IsPalindrome(input);
            Assert.True(result);
            Assert.False(IsPalindrome("race a car"));
        }

        [Fact]
        public void Test()
        {
            Assert.True(1 + 1 == 2);
        }

        class UnionFind
        {
            private int count;
            private int[] parent;
            private int[] rank;

            public UnionFind(int m, int n, int[,] grid)
            {
                count = 0;
                parent = new int[m * n];
                rank = new int[n * n];

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (grid[i, j] == 1)
                        {
                            parent[i * n + j] = i * n + j;
                            count++;
                        }

                        rank[i * n + j] = 0;
                    }
                }
            }

            public int find(int i)
            {
                if (parent[i] != i) parent[i] = find(parent[i]);
                return parent[i];
            }

            public void union(int x, int y)
            {
                // union with rank
                int rootx = find(x);
                int rooty = find(y);
                if (rootx != rooty)
                {
                    if (rank[rootx] > rank[rooty])
                    {
                        parent[rooty] = rootx;
                    }
                    else if (rank[rootx] < rank[rooty])
                    {
                        parent[rootx] = rooty;
                    }
                    else
                    {
                        parent[rooty] = rootx;
                        rank[rootx] += 1;
                    }

                    --count;
                }
            }

            public int GetCount()
            {
                return count;
            }
        }


        int numberAmazonTreasureTrucks(int rows, int column, int[,] grid)
        {
            if (grid == null || grid.Length == 0)
                return 0;

            UnionFind uf = new UnionFind(rows, column, grid);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    if (grid[r, c] == 1)
                    {
                        grid[r, c] = 0;
                        if (r - 1 >= 0 && grid[r - 1, c] == 1)
                        {
                            uf.union(r * column + c, (r - 1) * column + c);
                        }

                        if (r + 1 < rows && grid[r + 1, c] == 1)
                        {
                            uf.union(r * column + c, (r + 1) * column + c);
                        }

                        if (c - 1 >= 0 && grid[r, c - 1] == 1)
                        {
                            uf.union(r * column + c, r * column + c - 1);
                        }

                        if (c + 1 < column && grid[r, c + 1] == 1)
                        {
                            uf.union(r * column + c, r * column + c + 1);
                        }
                    }
                }
            }

            return uf.GetCount();
        }
    }
}