using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class MediumQuestion
    {
        //https://leetcode.com/problems/basic-calculator-ii/

        public int Calculate(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            Stack<int> stack = new Stack<int>();
            s += "+";
            char previous = '+';
            int number = 0;
            
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsDigit(c))
                {
                    number = number * 10 + c - '0';
                    continue;
                }
                if(c == ' ') continue;
                if(previous == '+') stack.Push(number);
                else if(previous == '-') stack.Push(-number);
                else if (previous == '*') stack.Push(stack.Pop() * number);
                else if(previous == '/') stack.Push(stack.Pop() / number);

                previous = c;
                number = 0;

            }

            int result = 0;
            while (stack.Count != 0) result += stack.Pop();
            return result;
        }

        [Fact]
        public void Test_Calculate()
        {
            var input = "3-2*2";
            var result = Calculate(input);
            Assert.Equal(7, result);
        }
        public int Calculate_II(string s)
        {
            var map = new Dictionary<char, Func<int, int, int>>();
            var set = new HashSet<string>
            {
                "+", "-", "*", "/"
            };

            // Define Map 

            map.Add('+', (x, y) => x + y);
            map.Add('-', (x, y) => x - y);
            map.Add('*', (x, y) => x * y);
            map.Add('/', (x, y) => x / y);

            // have one queue 
            var queue = new Queue<string>();
            var number = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                {
                    if (s[i] == '*' || s[i] == '/')
                    {
                        if (queue.Count != 0)
                        {
                            var prev = queue.Dequeue();
                            var tmp = map[s[i]].Invoke(Convert.ToInt32(number), Convert.ToInt32(prev));

                            queue.Enqueue(tmp.ToString());
                            number = string.Empty;
                        }
                    }
                    else if (s[i] == '+' || s[i] == '-')
                    {
                        queue.Enqueue(number);
                        queue.Enqueue($"{s[i]}");
                        number = string.Empty;
                    }
                }
                else if (s[i] != ' ')
                {
                    number = $"{s[i]}{number}";
                }
            }

            // Evaluate +- in queue
            int result = 0;
            while (queue.Count !=0)
            {
                var item = queue.Dequeue();
                if (set.Contains(item))
                {
                    var next = queue.Dequeue();
                    result = map[item.ToCharArray()[0]].Invoke(result, Convert.ToInt32(next));
                }
                else
                {
                    result += Convert.ToInt32(item);
                }
            }

            return result;
        }

        //https://leetcode.com/problems/merge-intervals/
        public int[][] Merge(int[][] intervals)
        {
            // Sort Interval by Element at index 0
            // Overlapped interval will be next to each other. 
            // To Merge overlapped interval - min(a[0], b[0]) - max(a[1], b[1])
            Array.Sort(intervals, new SortByFirstElement());
            var result = new LinkedList<int[]>();

            foreach (var interval in intervals)
            {
                if (result.Count == 0 || result.Last()[1] < interval[0])
                {
                    result.AddLast(interval);
                }
                else
                {
                    // overlap
                    var last = result.Last;
                    result.RemoveLast();
                    var item = new int[] {last.Value[0], Math.Max(last.Value[1], interval[1])};
                    result.AddLast(item);
                }
            }

            return result.ToArray();
        }

        [Fact]
        public void Test_Merge()
        {
            var input = new int[][]
            {
                new int[] {1, 3}, new int[] {2, 6}, new int[] {8, 10}, new int[] {15, 18}
            };
            var result = Merge(input);
        }

        private bool InRange(int a, int[] range)
        {
            return a >= range[0] && a <= range[1];
        }

        private class SortByFirstElement : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                if (x[0] == y[0])
                    return x[1] - y[1];
                return x[0] - y[0];
            }
        }


        public IList<List<int>> DivideGroup(int[] nums)
        {
            var maxSum = nums.Sum(x => x);
            var idx = 0;
            var curSum = 0;
            var maxDistance = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];
                var leftAvg = curSum / (i + 1);
                var rightAvg = (maxSum - curSum) / (nums.Length - i - 1);
                var diff = Math.Abs(leftAvg - rightAvg);
                if (diff >= maxDistance)
                {
                    maxDistance = diff;
                    idx = i;
                }
            }

            var leftGroup = new int[idx + 1];
            var rightGroup = new int[nums.Length - idx];
            Array.Copy(nums, leftGroup, idx + 1);
            Array.Copy(nums, rightGroup, nums.Length - idx);
            return new List<List<int>>
            {
                leftGroup.ToList(), rightGroup.ToList()
            };
        }

        //https://leetcode.com/problems/sliding-window-maximum/

        public int[] MaxSlidingWindow_Solution(int[] nums, int k)
        {
            // Using Dequeue
            var result = new List<int>();
            var queue = new PriorityQueue(k);
            int n = nums.Length;
            if (n * k == 0) return new int[0];

            for (int i = 0; i < k; i++)
            {
                queue.Enqueue(nums[i]);
            }

            result.Add(queue.Dequeue());
            int tail = 0;
            int head = k;
            while (head < n)
            {
                queue.Enqueue(nums[head++]);
                queue.Remove(nums[tail++]);
                if (!queue.IsEmpty)
                    result.Add(queue.Dequeue());
            }

            return result.ToArray();
        }

        [Fact]
        public void Test_MaxSlidingWindow_Solution()
        {
            var input = new int[] {2, 7};
            var k = 1;
            var result = MaxSlidingWindow_Solution(input, k);
        }

        public int[] MaxSlidingWindow_BF(int[] nums, int k)
        {
            int n = nums.Length;
            if (n * k == 0)
                return new int[0];

            var output = new int[n - k + 1];
            for (int i = 0; i < n - k + 1; i++)
            {
                int max = int.MinValue;
                for (int j = i; j < i + k; j++)
                {
                    max = Math.Max(max, nums[j]);
                }

                output[i] = max;
            }

            return output;
        }

        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            // Keep a max heap of size k
            // Loop through nums
            // When heap reach size k, Dequeue and add to result

            var result = new List<int>();
            var list = new LinkedList<int>();

            foreach (var n in nums)
            {
                if (list.Count == 0)
                    list.AddLast(n);
                else
                {
                    // Add Item to Queue
                    if (n >= list.Last.Value)
                        list.AddLast(n);
                    else if (n <= list.First.Value)
                        list.AddFirst(n);
                    else
                    {
                        //In Between
                        var tmp = list.First;
                        while (tmp != null && tmp.Value < n)
                        {
                            tmp = tmp.Next;
                        }

                        list.AddBefore(tmp, n);
                    }
                }

                if (list.Count != k) continue;
                result.Add(list.Last.Value);
                list.RemoveFirst();
            }

            return result.ToArray();
        }

        //https://leetcode.com/problems/top-k-frequent-words/
        public IList<string> TopKFrequent(string[] words, int k)
        {
            var freq = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (freq.ContainsKey(word))
                    freq[word]++;
                else freq.Add(word, 1);
            }

            var result = freq.OrderBy(x => x.Value * (-1))
                .ThenBy(x => x.Value)
                .Take(k)
                .Select(x => x.Key);

            return result.ToList();
        }

        public class OrderByFreq : IComparer<string>
        {
            private IDictionary<string, int> _frequency;

            public OrderByFreq(IDictionary<string, int> frequency)
            {
                this._frequency = frequency;
            }

            public int Compare(string a, string b)
            {
                if (_frequency.ContainsKey(a) && _frequency.ContainsKey(b))
                    return _frequency[a] - _frequency[b];
                else if (_frequency.ContainsKey(a))
                    return 1;
                else if (_frequency.ContainsKey(b))
                    return -1;
                return string.Compare(a, b, StringComparison.Ordinal);
            }
        }

        //https://leetcode.com/problems/kth-largest-element-in-an-array/solution/
        public int FindKthLargest(int[] nums, int k)
        {
            var queue = new PriorityQueue(k);
            foreach (var num in nums)
            {
                queue.Enqueue(num);
            }

            return queue.Dequeue();
        }

        [Fact]
        public void Test_FindKthLargest()
        {
            var input = new int[] {3, 2, 3, 1, 2, 4, 5, 5, 6};
            var result = FindKthLargest(input, 4);
            Assert.Equal(4, result);
        }

        public class PriorityQueue
        {
            private LinkedList<int> _list = new LinkedList<int>();
            private int _size;
            public bool IsEmpty => _list.Count == 0;

            public PriorityQueue(int size)
            {
                _size = size;
            }

            public void Enqueue(int num)
            {
                if (IsEmpty)
                    _list.AddLast(num);
                else
                {
                    if (num >= _list.Last.Value)
                        _list.AddLast(num);
                    else if (_list.First.Value >= num)
                        _list.AddFirst(num);
                    else
                    {
                        // In Betweeen
                        var tmp = _list.First;
                        while (tmp != null && tmp.Value < num)
                        {
                            tmp = tmp.Next;
                        }

                        _list.AddBefore(tmp, num);
                    }
                }

                // Remove if larger than k

                if (_list.Count > _size)
                    _list.RemoveFirst();
            }

            public int Dequeue()
            {
                if (_list.Count != 0)
                    return _list.First();

                throw new InvalidOperationException("List is empty");
            }

            public void Remove(int n)
            {
                if (_list.Count == 0)
                    return;
                var tmp = _list.First;
                while (tmp != null)
                {
                    if (tmp.Value == n)
                    {
                        _list.Remove(tmp);
                        break;
                    }

                    tmp = tmp.Next;
                }
            }
        }

        // Grep
        // Return a list of start indices of needle found in hay
        public IList<int> GrepCustom(string hay, string needle)
        {
            if (string.IsNullOrEmpty(hay) || string.IsNullOrEmpty(needle))
                return new List<int>();

            // Needle is longer than hay - not found.
            if (needle.Length > hay.Length) return new List<int>();
            int nPtr = 0;
            int nLen = needle.Length;
            IList<int> answer = new List<int>();
            // resume is cur + length of needle

            for (int i = 0; i < hay.Length; i++)
            {
                if (hay[i] == needle[nPtr])
                {
                    // Check if it's a needle
                    if (IsNeedle(hay, i, needle))
                    {
                        answer.Add(i);

                        // Reset needle pointer and jump i
                        nPtr = 0;
                        i = i + nLen;
                    }
                }
            }

            return answer;
        }

        [Fact]
        public void Test_GrepL()
        {
            var hay = "aaabcdddbbddddabcdefghi";
            var needle = "abc";
            var expected = new List<int> {2, 14};
            Assert.Equal(expected, GrepCustom(hay, needle));

            var expected1 = new List<int> {0, 1, 2, 3};
        }

        private bool IsNeedle(string hay, int start, string needle)
        {
            // if start index to the end is smaller than needle - false
            // if any char at ith not equal to needle at i then false
            // else true;

            // abcde - s=1 (5-1)
            int len = hay.Length - start;
            int nLen = needle.Length;
            if (nLen > len) return false;
            for (int i = 0; i < nLen; i++)
            {
                if (needle[i] != hay[start++]) return false;
            }

            return true;
        }

        //https://leetcode.com/problems/most-common-word/solution/
        public string MostCommonWord(string paragraph, string[] banned)
        {
            paragraph += ".";
            string answer = "";
            ISet<string> banset = new HashSet<string>();
            foreach (var s in banned)
            {
                if (!banset.Contains(s)) banset.Add(s);
            }

            var dict = new Dictionary<string, int>();
            var answerFreq = 0;
            var word = new StringBuilder();
            foreach (var ch in paragraph.ToCharArray())
            {
                if (!char.IsWhiteSpace(ch) && char.IsLetter(ch))
                {
                    word.Append(char.ToLower(ch));
                }
                else if (word.Length > 0)
                {
                    var fw = word.ToString();
                    if (!banset.Contains(fw))
                    {
                        if (dict.ContainsKey(fw)) dict[fw]++;
                        else dict.Add(fw, 1);

                        if (dict.ContainsKey(fw) && dict[fw] > answerFreq)
                        {
                            answer = fw;
                            answerFreq = dict[fw];
                        }
                    }

                    word = new StringBuilder();
                }
            }

            return answer;
        }

        [Fact]
        public void Test_MostCommonWords()
        {
            var paragraph = "Bob hit a ball, the hit BALL flew far after it was hit.";
            var banned = new string[] {"hit"};
            var result = MostCommonWord(paragraph, banned);
            Assert.Equal("ball", result);
        }

        public bool WordPattern(string pattern, string str)
        {
            // using two pointers and compare pattern to str
            int i = 0;
            int j = 1;
            int n = pattern.Length;
            var arr = str.Split(' ');

            if (n != arr.Length) return false;

            var patternMap = new Dictionary<char, string>();
            var wordMap = new Dictionary<string, char>();

            for (i = 0; i < n; i++)
            {
                if (!patternMap.ContainsKey(pattern[i]))
                    patternMap.Add(pattern[i], arr[i]);
                else
                {
                    if (patternMap[pattern[i]] != arr[i]) return false;
                }

                if (!wordMap.ContainsKey(arr[i]))
                    wordMap.Add(arr[i], pattern[i]);
                else if (wordMap[arr[i]] != pattern[i]) return false;
            }

            return true;
        }

        [Fact]
        public void Test_WordPattern()
        {
            Assert.False(WordPattern("abba", "dog cat cat fish"));
            Assert.True(WordPattern("abba", "dog cat cat dog"));
            Assert.False(WordPattern("aaaa", "dog cat cat dog"));
            Assert.False(WordPattern("abba", "dog dog dog dog"));
        }

        //https://leetcode.com/problems/longest-palindrome/
        // Build Map of Char. 
        // The longest palindrome is the sum of all even char pair
        // + biggest odd char - if any. 
        public int LongestPalindromeL(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            var dict = new Dictionary<char, int>();
            foreach (var ch in s.ToCharArray())
            {
                if (dict.ContainsKey(ch))
                    dict[ch]++;
                else dict.Add(ch, 1);
            }

            var evenPair = dict.Where(x => x.Value % 2 == 0)
                .Select(x => x.Value)
                .Sum();

            var odd = dict.Where(x => x.Value % 2 == 1)
                .OrderBy(x => x.Value)
                .Select(x => (x.Value - 1));

            var oddsum = odd.Sum();
            var oddcount = odd.Count();

            if (oddcount == 0)
                return evenPair;

            return evenPair + (oddsum + 1);
        }

        [Fact]
        public async void Test_LongestPalindromeL()
        {
            var input = "abccccdd";
            var expected = 7;
            var url = "https://leetcode.com/submissions/detail/332852357/testcase/";
            var input2 =
                "civilwartestingwhetherthatnaptionoranynartionsoconceivedandsodedicatedcanlongendureWeareqmetonagreatbattlefiemldoftzhatwarWehavecometodedicpateaportionofthatfieldasafinalrestingplaceforthosewhoheregavetheirlivesthatthatnationmightliveItisaltogetherfangandproperthatweshoulddothisButinalargersensewecannotdedicatewecannotconsecratewecannothallowthisgroundThebravelmenlivinganddeadwhostruggledherehaveconsecrateditfaraboveourpoorponwertoaddordetractTgheworldadswfilllittlenotlenorlongrememberwhatwesayherebutitcanneverforgetwhattheydidhereItisforusthelivingrathertobededicatedheretotheulnfinishedworkwhichtheywhofoughtherehavethusfarsonoblyadvancedItisratherforustobeherededicatedtothegreattdafskremainingbeforeusthatfromthesehonoreddeadwetakeincreaseddevotiontothatcauseforwhichtheygavethelastpfullmeasureofdevotionthatweherehighlyresolvethatthesedeadshallnothavediedinvainthatthisnationunsderGodshallhaveanewbirthoffreedomandthatgovernmentofthepeoplebythepeopleforthepeopleshallnotperishfromtheearth";
            Assert.Equal(expected, LongestPalindromeL(input));
            Assert.Equal(983, LongestPalindromeL(input2));
            Assert.Equal(2, LongestPalindromeL("bb"));
        }

        //https://leetcode.com/problems/palindrome-permutation/
        // 2 cases - Length of S is even and odd
        // Even - Permutation can form a Palindrome when 
        // it has even pairs of char. //'aababxxyyaaoo'
        //
        // Odd - Only one Odd Char, the rest even pair. 'abbccddzz'
        // I Can use hash map to store char and its occurance
        // Then check the logic above
        public bool CanPermutePalindrome(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 1) return false;
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                    dict[s[i]]++;
                else dict.Add(s[i], 1);
            }

            var oddCount = dict.Count(x => x.Value % 2 == 1);
            if (s.Length % 2 == 0)
                return oddCount == 0;
            return oddCount == 1;
        }

        //https://leetcode.com/problems/longest-palindromic-substring/solution/
        // The longest palindrome of substring is the string itself. 
        // In that case, if we check around the center of the string
        // 
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 1) return "";
            int left = 0;
            int right = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int len1 = ExploreAroundCenter(s, i, i); // Cover 'aba'
                int len2 = ExploreAroundCenter(s, i, i + 1); // Cover 'bb'
                int len = Math.Max(len1, len2);
                if (len > right - left)
                {
                    left = i - (len - 1) / 2; // i = 1, len = 1, -> left = 1 - 0 = 1
                    right = i + len / 2; // right = 1
                }
            }

            return s.Substring(left, (right - left + 1));
        }

        [Fact]
        public void Test_LongestPalindrome()
        {
            var input = "cbbd";
            var result = LongestPalindrome(input);
            Assert.Equal("bb", result);
        }

        private int ExploreAroundCenter(string s, int left, int right)
        {
            int l = left;
            int r = right;

            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                l--;
                r++;
            }

            return r - l - 1;
        }

        //https://leetcode.com/problems/binary-tree-vertical-order-traversal/
        public IList<IList<int>> VerticalOrder(TreeNode root)
        {
            return null;
        }
    }
}