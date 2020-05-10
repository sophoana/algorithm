using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class InterviewOne
    {
        private ISet<string> WORDS_PERFECT;

        private IDictionary<string, string> CAP_WORDS;

        private IDictionary<string, string> VOW_WORDS;

        //https://leetcode.com/problems/vowel-spellchecker/solution/
        public string[] Spellchecker(string[] wordlist, string[] queries)
        {
            WORDS_PERFECT = new HashSet<string>();
            CAP_WORDS = new Dictionary<string, string>();
            VOW_WORDS = new Dictionary<string, string>();

            foreach (var word in wordlist)
            {
                if (!WORDS_PERFECT.Contains(word))
                    WORDS_PERFECT.Add(word);

                var lower = word.ToLower();
                if (!CAP_WORDS.ContainsKey(lower))
                    CAP_WORDS.Add(lower, word);

                var dvLower = Devowel(lower);
                if (!VOW_WORDS.ContainsKey(dvLower))
                    VOW_WORDS.Add(dvLower, word);
            }

            string[] answer = new string[queries.Length];
            int i = 0;
            foreach (var query in queries)
            {
                answer[i++] = Check(query);
            }

            return answer;
        }

        public string Check(string query)
        {
            if (WORDS_PERFECT.Contains(query))
                return query;

            var lower = query.ToLower();
            if (CAP_WORDS.ContainsKey(lower))
                return CAP_WORDS[lower];

            var lowerVowel = Devowel(lower);
            if (VOW_WORDS.ContainsKey(lowerVowel))
                return VOW_WORDS[lowerVowel];

            return "";
        }

        public string Devowel(string word)
        {
            var sb = new StringBuilder();
            foreach (var ch in word.ToCharArray())
            {
                sb.Append(IsVowel(ch) ? '*' : ch);
            }

            return sb.ToString();
        }

        public bool IsVowel(char c)
        {
            // a, e, i, o, u
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }

        [Fact]
        public void Test_SpellCheck()
        {
            //["KiTe","kite","hare","Hare"]
            //["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]
            var wordList = new string[] {"KiTe", "kite", "hare", "Hare"};
            //var queries = new string[] { "kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"};
            var queries = new string[] { "keti","keet","keto"};
            var result = Spellchecker(wordList, queries);

        }

        //https://leetcode.com/problems/k-similar-strings
        public int KkSimilarity(string A, string B)
        {
            if (A.Equals(B))
                return 0;
            ISet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(A);
            visited.Add(A);
            int result = 0;

            while (queue.Count != 0)
            {
                result++;
                for (int sz = queue.Count; sz > 0; sz--)
                {
                    var s = queue.Dequeue();
                    int i = 0;
                    while (s[i] == B[i]) i++;
                    for (int j = i + 1; j < s.Length; j++)
                    {
                        if (s[j] == B[j] || s[j] != B[i]) continue;
                        string temp = Swap(s, i, j);
                        if (temp == B) return result;
                        if (!visited.Contains(temp))
                        {
                            visited.Add(temp);
                            queue.Enqueue(temp);
                        }
                    }
                }
            }

            return result;
        }

        private string Swap(string s, int i, int j)
        {
            char ich = s[i];
            char jch = s[j];
            var arr = s.ToCharArray();
            arr[i] = jch;
            arr[j] = ich;

            return new string(arr);
        }

        class LevelNode
        {
            public int Level { get; set; }
            public double Value { get; set; }
        }

        public IList<double> AverageOfLevels(TreeNode root)
        {
            var result = new List<double>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                long sum = 0;
                long count = 0;
                var temp = new Queue<TreeNode>();
                while (queue.Count != 0)
                {
                    TreeNode n = queue.Dequeue();
                    sum += n.val;
                    count++;
                    if (n.left != null)
                        temp.Enqueue(n.left);
                    if (n.right != null)
                        temp.Enqueue(n.right);
                }

                queue = temp;
                result.Add(sum * 1.0 / count);
            }

            return result;
        }

        public IList<double> AverageOfLevels_NOTUSE(TreeNode root)
        {
            IList<LevelNode> nodes = new List<LevelNode>();
            Queue<KeyValuePair<int, TreeNode>> queue = new Queue<KeyValuePair<int, TreeNode>>();

            queue.Enqueue(new KeyValuePair<int, TreeNode>(-1, null)); // When I see (-1, null) I finish one level
            queue.Enqueue(new KeyValuePair<int, TreeNode>(1, root));
            nodes.Add(new LevelNode
            {
                Level = 1, Value = root.val
            });

            int currentLevel = 1;
            IList<double> answer = new List<double>();

            while (queue.Count != 0)
            {
                var tmp = queue.Dequeue();

                if (tmp.Key == -1)
                {
                    answer.Add(nodes.Where(x => x.Level == currentLevel).Average(x => x.Value));
                    currentLevel++;
                    continue;
                }

                var left = tmp.Value.left;
                var right = tmp.Value.right;

                if (right != null)
                {
                    nodes.Add(new LevelNode
                    {
                        Level = currentLevel, Value = right.val
                    });
                    queue.Enqueue(new KeyValuePair<int, TreeNode>(currentLevel + 1, right));
                }

                if (left != null)
                {
                    nodes.Add(new LevelNode
                    {
                        Level = currentLevel, Value = left.val
                    });
                    queue.Enqueue(new KeyValuePair<int, TreeNode>(currentLevel + 1, left));
                }
            }


            return answer;
        }
    }
}