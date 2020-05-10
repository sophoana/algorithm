using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class PermutationQuestion
    {
        //https://leetcode.com/problems/burst-balloons/
        public int MaxCoins(int[] nums)
        {
            int n = nums.Length + 2;
            int[] ns = new int[n];

            for (int i = 0; i < nums.Length; i++)
                ns[i + 1] = nums[i];

            ns[0] = 1;
            ns[n - 1] = 1;
            int[,] dp = new int[n, n];

            for (int l = n - 2; l > -1; l--)
            {
                for (int r = l + 2; r < n; r++)
                {
                    for (int i = l + 1; i < r; i++)
                    {
                        dp[l, r] = Math.Max(dp[l, r], (dp[l, i] + dp[i, r] + (ns[i] * ns[l] * ns[r])));
                    }
                }
            }

            return dp[0, n - 1];
        }

        [Fact]
        public void Test_MaxCoins()
        {
            var input = new List<int> {3, 1, 5, 8};
            var expected = 167;
            var result = MaxCoins(input.ToArray());
            Assert.Equal(expected, result);
        }


        public int CountCoin(int[] coins, int target)
        {
            var dp = new int[coins.Length + 1];
            dp[0] = 1;
            foreach (var coin in coins)
            {
                for (int i = coin; i <= target; i++)
                {
                    dp[i] = dp[i] + dp[i - coin];
                }
            }

            return dp[target];
        }

        public int NumRollsToTarget(int d, int f, int target)
        {
            int[,] dp = new int[d + 1, target + 1];
            for (int i = 1; i <= d; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    if (j > i * f) continue;
                    else
                    {
                        for (int k = 1; k <= f && k <= j; k++)
                        {
                            dp[i, j] = (dp[i, j] + dp[i - 1, j - k]) % 1000000007;
                        }
                    }
                }
            }

            return dp[d, target];
        }

        private int NumsRollsEachFace(int d, int f, int target, IDictionary<string, int> memo)
        {
            int mod_factor = 1000000007;
            if ((target > d * f) || target < d) return 0;
            if (d == 1) return target <= f ? 1 : 0;
            string key = $"{d}{f}{target}";
            if (!memo.ContainsKey(key))
            {
                var current = 0;
                for (int i = f; i >= 0; i--)
                {
                    current += NumsRollsEachFace(d - 1, f, target, memo);
                    current %= mod_factor;
                }

                memo.Add(key, current);
            }

            return memo[key];
        }

        public IList<string> GeneratePermutation(string s)
        {
            IList<string> permutation = new List<string>();
            if (string.IsNullOrEmpty(s))
            {
                permutation.Add("");
                return permutation;
            }

            char first = s[0];
            string substring = s.Substring(1);
            IList<string> words = GeneratePermutation(substring);
            foreach (var word in words)
            {
                for (int i = 0; i <= word.Length; i++)
                {
                    var ass = InsertCharAt(word, first, i);
                    permutation.Add(ass);
                }
            }

            return permutation;
        }

        [Fact]
        public void Test_GeneratePermutation()
        {
            var input = "abc";
            var expected = new List<string>
            {
                "abc", "bac", "cab", "cba", "acb", "bca"
            };

            var result = GeneratePermutation(input);
            Assert.Contains(result, s => expected.Contains(s));
        }

        public string InsertCharAt(string str, char ch, int position)
        {
            return str.Insert(position, $"{ch}");
        }
    }

    public class DynamicProgrammingQuestion
    {
        public bool Exist(char[][] board, string word)
        {
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board[0].Length; col++)
                {
                    if (Deep(board, 0, word, row, col))
                        return true;
                }
            }

            return false;
        }

        [Fact]
        public void Test_FoundThatWord()
        {
            var input = new List<char[]>
            {
                new char[] {'A', 'B', 'C', 'E'},
                new char[] {'S', 'F', 'C', 'S'},
                new char[] {'A', 'D', 'E', 'E'}
            };

            var word = "ABCCED";
            var result = Exist(input.ToArray(), word);
        }

        private bool Deep(char[][] board, int idx, string word, int r, int c)
        {
            // if already checked, mark it as 0
            if (idx >= word.Length)
                return true;

            var row = board.Length;
            var col = board[0].Length;

            if (r < 0 || r == row || c < 0 || c == col ||
                board[r][c] != word[idx])
                return false;

            bool result = false;
            board[r][c] = '0';

            int[] rowOffSets = {0, 1, 0, -1};
            int[] colOffSets = {1, 0, -1, 0};

            for (int d = 0; d < 4; d++)
            {
                result = Deep(board, (idx + 1), word, r + rowOffSets[d], c + colOffSets[d]);
                if (result)
                    break;
            }

            board[r][c] = word[idx];
            return result;
        }


        //https://leetcode.com/problems/concatenated-words/
        public IList<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            // for each word in words
            // check if it's can be broken
            // if yes, add to result list.
            // return result
            var result = new List<string>();
            var set = new HashSet<string>(words);
            foreach (var word in words)
            {
                if (DFS_Wordbreak(word, set))
                    result.Add(word);
            }

            return result;
        }

        private bool DFS_Wordbreak(string word, ISet<string> map)
        {
            for (int i = 1; i < word.Length; i++)
            {
                var prefix = word.Substring(0, i);
                var suffix = word.Substring(i);

                if (map.Contains(prefix) && map.Contains(suffix))
                    return true;
                if (map.Contains(prefix) && DFS_Wordbreak(suffix, map))
                    return true;
                if (map.Contains(suffix) && DFS_Wordbreak(prefix, map))
                    return true;
            }

            return false;
        }

        [Fact]
        public void Test_FindAllConcat()
        {
            var input = new string[]
                {"cat", "cats", "catsdogcats", "dog", "dogcatsdog", "hippopotamuses", "rat", "ratcatdogcat"};
            var result = FindAllConcatenatedWordsInADict(input);
        }

        // TO Check
        public int[] NextGreaterElements(int[] nums)
        {
            int[] result = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = -1;
                for (int j = 1; j < nums.Length; j++)
                {
                    if (nums[(i + j) % nums.Length] > nums[i])
                    {
                        result[i] = nums[(i + j) % nums.Length];
                        break;
                    }
                }
            }

            return result;
        }

        // TO Check
        public bool CanCross(int[] stones)
        {
            var map = new Dictionary<int, ISet<int>>();
            for (int i = 0; i < stones.Length; i++)
            {
                map.Add(stones[i], new HashSet<int>());
            }

            map[0].Add(0);
            for (int i = 0; i < stones.Length; i++)
            {
                foreach (var k in map[stones[i]])
                {
                    for (int step = k - 1; step <= k + 1; step++)
                    {
                        if (step > 0 && map.ContainsKey(stones[i] + step))
                            map[stones[i] + step].Add(step);
                    }
                }
            }

            return map[stones[stones.Length - 1]].Count > 0;
        }

        //https://leetcode.com/problems/predict-the-winner/
        public bool PredictTheWinner(int[] nums)
        {
            int[] dp = new int[nums.Length];
            for (int s = nums.Length; s >= 0; s--)
            {
                for (int e = s + 1; e < nums.Length; e++)
                {
                    int a = nums[s] - dp[e];
                    int b = nums[e] - dp[e - 1];
                    dp[e] = Math.Max(a, b);
                }
            }

            return dp[nums.Length - 1] >= 0;
        }

        // (nums, 0, nums.Length - 1, 1)
        public int PredictTheWinner_BF(int[] nums, int s, int e, int turn)
        {
            if (s == e)
                return turn * nums[s];

            int a = turn * nums[s] + PredictTheWinner_BF(nums, s + 1, e, -turn);
            int b = turn * nums[e] + PredictTheWinner_BF(nums, s, e - 1, -turn);
            return turn * Math.Max(turn * a, turn * b);
        }

        [Fact]
        public void Test_PredictTheWinner()
        {
            var input = new int[] {1, 5, 2};
            var expeced = true;
        }

        //https://leetcode.com/problems/palindromic-substrings/solution/
        public int CountSubstrings(string s)
        {
            int n = s.Length;
            int answer = 0;
            for (int i = 0; i < 2 * n - 1; i++)
            {
                int left = i / 2;
                int right = left + i % 2;
                while (left >= 0 && right < n && s[left] == s[right])
                {
                    answer++;
                    left--;
                    right++;
                }
            }

            return answer;
        }

        [Fact]
        public void Test_CountSubstrings()
        {
            Assert.Equal(6, CountSubstrings("aaa"));
            Assert.Equal(3, CountSubstrings("abc"));
        }

        //https://leetcode.com/problems/word-break-ii/
        public IList<string> WordBreakII(string s, IList<string> wordDict)
        {
            return WordBreakII_BruteForce(s, wordDict);
        }

        public IList<string> WordBreakII_DP(string s, ISet<string> wordDict)
        {
            var dp = new List<string> [s.Length];
            var initial = new List<string>() {""};
            dp[0] = initial;

            for (int i = 1; i <= s.Length; i++)
            {
                var list = new List<string>();
                for (int j = 0; j < i; j++)
                {
                    if (dp[j].Count > 0 &&
                        wordDict.Contains(s.Substring(j, (i - j))))
                    {
                        foreach (var li in dp[j])
                        {
                            var str = li + (li.Equals("") ? "" : " ") + s.Substring(j, i - j);
                            list.Add(str);
                        }
                    }
                }

                dp[i] = list;
            }

            return dp[s.Length];
        }

        public IList<string> WordBreakII_BruteForce(string s, IList<string> dict)
        {
            return WordBreakII_BFHelper(s, new HashSet<string>(dict), 0);
        }

        private IList<string> WordBreakII_BFHelper(string s, ISet<string> set, int start)
        {
            Queue<string> list = new Queue<string>();
            if (start == s.Length)
                list.Enqueue("");

            for (int i = start + 1; i <= s.Length; i++)
            {
                int len = i - start;
                if (set.Contains(s.Substring(start, len)))
                {
                    var sub = WordBreakII_BFHelper(s, set, i);
                    foreach (var li in sub)
                    {
                        list.Enqueue(s.Substring(start, len) + (li.Equals("") ? "" : " ") + li);
                    }
                }
            }

            return list.ToList();
        }

        [Fact]
        public void Test_WordBreakII()
        {
            var input = "catsanddog";
            var dict = new List<string>
            {
                "cat", "cats", "and", "sand", "dog"
            };
            var result = WordBreakII(input, dict);
            var expected = new List<string>
            {
                "cats and dog",
                "cat sand dog"
            };

            Assert.Contains(result, s => expected.Contains(s));
        }

        //https://leetcode.com/problems/word-break/
        public bool WordBreakI(string s, IList<string> wordDict)
        {
            ISet<string> words = new HashSet<string>(wordDict);
            // foreach (var w in wordDict)
            //     if (!words.Contains(w))
            //         words.Add(w);

            return searchSubstring(s, 0, words);
        }

        public bool WordBreakDP(string s, IList<string> wordDict)
        {
            var set = new HashSet<string>(wordDict);
            return WordBreakDPHelper(s, set, 0, new bool? [s.Length]);
        }

        private bool WordBreakDPHelper(string s, ISet<string> dict, int start, bool?[] memo)
        {
            if (start == s.Length) return true;
            if (memo[start].HasValue)
                return memo[start].Value;

            for (int end = start + 1; end <= s.Length; end++)
            {
                int len = end - start;
                if (dict.Contains(s.Substring(start, len)) &&
                    WordBreakDPHelper(s, dict, end, memo))
                {
                    memo[start] = true;
                    return memo[start].Value;
                }
            }

            memo[start] = false;
            return memo[start].Value;
        }

        public bool WordBreakBFS(string s, IList<string> wordDict)
        {
            ISet<string> set = new HashSet<string>(wordDict);
            Queue<int> queue = new Queue<int>();
            var visited = new int[s.Length];
            queue.Enqueue(0);
            while (queue.Count != 0)
            {
                int start = queue.Dequeue();
                if (visited[start] == 0)
                {
                    for (int end = start + 1; end <= s.Length; end++)
                    {
                        if (set.Contains(s.Substring(start, (end - start))))
                        {
                            queue.Enqueue(end);
                            if (end == s.Length)
                                return true;
                        }
                    }

                    visited[start] = 1;
                }
            }

            return false;
        }

        [Fact]
        public void Test_WordBreakI()
        {
            var input = "leetcode";
            var dict = new List<string>
            {
                "leet", "code", "is", "love", "leet"
            };
            var result = WordBreakI(input, dict);
            Assert.True(result);
        }

        private bool searchSubstring(string s, int start, ISet<string> words)
        {
            if (start == s.Length)
                return true;

            for (int i = start + 1; i <= s.Length; i++)
            {
                int len = i - start;
                if (words.Contains(s.Substring(start, len)) &&
                    searchSubstring(s, i, words))
                    return true;
            }

            return false;
        }
    }
}