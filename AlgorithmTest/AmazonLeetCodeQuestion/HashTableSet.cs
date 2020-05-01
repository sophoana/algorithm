using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using Xunit;
using Assert = Xunit.Assert;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public static class DictionaryExtension
    {
        public static void AddOrUpdate(this Dictionary<int, int> dict, int key, int value)
        {
            if (dict.ContainsKey(key))
                dict[key] = dict[key] + value;
            else dict.Add(key, value);
        }
    }

    public class HashTableSet
    {
        // https://leetcode.com/problems/3sum-closest/submissions/
        public int ThreeSumClosest(int[] nums, int target)
        {
            // If the gap between the current sum and target
            // is smaller than the previous result. Take the Current

            Array.Sort(nums);
            int n = nums.Length;
            int cur = nums[0] + nums[1] + nums[n-1];

            for (int i = 0; i < n - 2; i++)
            {
                int left = i + 1;
                int right = n - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[right] + nums[left];
                    if (sum < target)
                    {
                        // The closest to target

                        cur = GetClosest(cur, sum, target);
                        left++;
                    }
                    else if (sum >= target)
                    {
                        cur = GetClosest(cur, sum, target);
                        right--;
                    }
                }
            }

            return cur;
        }

        private int GetClosest(int n1, int n2, int target)
        {
            return Math.Abs(n1 - target) > Math.Abs(n2 - target) ? n2 : n1;
        }

        [Fact]
        public void Test_ThreeSumClosest()
        {
            var input = new int[] {-1,2,1,-4 };//{1, 1, 1, 0};
            var target = 1;// -100;
            var result = ThreeSumClosest(input, target);
            var expected = 2;
            Assert.Equal(expected, result);
        }

        // https://leetcode.com/problems/3sum/solution/
        public IList<IList<int>> ThreeSum(int[] nums, int target = 0)
        {
            Array.Sort(nums);

            var result = new List<IList<int>>();

            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (i != 0 && nums[i - 1] != nums[i]) continue;

                // var trip = new List<int>();
                var left = i + 1;
                var right = nums.Length - 1;

                // target number is Zero a + b + c = 0
                // if (a + b + c) > 0 --> right --;
                // if (a + b + c) < 0 --> left ++;
                // if (a + b + c) == 0 --> keep the triplet
                // Repeat while left < right

                while (left < right)
                {
                    var sum = nums[i] + nums[left] + nums[right];

                    if (sum > target || (right < nums.Length - 1 && nums[right] == nums[right + 1])) right--;
                    else if (sum < target || (left > i + 1 && nums[left] == nums[left - 1])) left++;
                    else result.Add(new List<int>() {nums[i], nums[left++], nums[right--]});
                }
            }

            return result;
        }

        //https://leetcode.com/problems/reverse-integer/
        public int Reverse(int x)
        {
            // Check if x is positive or negative
            // Find length of x ex. 120 -> n=2 (10 ^ 2)
            // Using an array to store each digit
            var isPositive = -1 * x < x;
            var stack = new Queue<int>();
            var idx = 0;
            x = Math.Abs(x);

            while (x != 0)
            {
                stack.Enqueue(x % 10);
                x /= 10;
            }

            var result = 0;
            var n = stack.Count;
            for (int i = 1; i <= n; i++)
            {
                var digit = stack.Dequeue();
                var factor = Convert.ToInt32(Math.Pow(10, n - i));
                var tmp = digit * factor;

                if (tmp / factor != digit) return 0;
                if (result > result + tmp) return 0;
                result += tmp;
            }

            return isPositive ? result : -1 * result;
        }

        [Fact]
        public void Test_Reverse()
        {
            var input = 1563847412; //1534236469;
            var result = Reverse(input);
            var expected = 0;
            Assert.Equal(expected, result);
        }

        //https://leetcode.com/problems/minimum-time-visiting-all-points/
        public int MinTimeToVisitAllPoints(int[][] points)
        {
            // Must visit in Order
            // Iterate through all points, find shortest path to next point
            // accumulate total time. 

            // Shortest Path between two points is going diagonally wherever possible
            if (points.Length == 0 || points.Length == 1)
                return 0;

            int i = 0;
            int total = 0;
            for (int j = 1; i < points.Length; i++, j++)
            {
                var p1 = points[i];
                var p2 = points[j];
                total += ShortestPathBetweenTwoPoints(p1, p2);
            }

            return total;
        }

        private int ShortestPathBetweenTwoPoints(int[] p1, int[] p2)
        {
            // Diagonal means +- (x, y) at the same time
            // if x1 == x2 min(y1, y2)
            // if y1 == y2 min (x1, x2)
            // else -+ x or y until x or y euqal

            var x1 = p1[0];
            var x2 = p2[0];
            var y1 = p1[1];
            var y2 = p2[1];

            if (x1 == x2) return Math.Abs(y1 - y2);
            else if (y1 == y2) return Math.Abs(x1 - x2);

            var diffx = Math.Abs(x1 - x2);
            var diffy = Math.Abs(y1 - y2);

            return Math.Min(diffx, diffy) + Math.Abs(diffx - diffy);
        }

        public int[] NumSmallerByFrequency(string[] queries, string[] words)
        {
            var fw = words.Select(GetSmallestFrequency).OrderBy(x => x).ToList();
            var result = new List<int>();

            foreach (var query in queries)
            {
                var fq = GetSmallestFrequency(query);
                var count = fw.Count(x => x > fq);
                result.Add(count);
            }

            return result.ToArray();
        }

        [Fact]
        public void Test_NumSmallerByFrequency()
        {
            var queries = new string[] {"cdb"};
            var words = new string[] {"zaaaz"};
            var result = NumSmallerByFrequency(queries, words);
            var expected = new int[] {1};
            Assert.Equal(expected, result);
        }

        private int GetSmallestFrequency(string word)
        {
            if (string.IsNullOrEmpty(word))
                return 0;

            // Convert to Char Array and Order by it's value
            var ch = word.ToCharArray().OrderBy(x => x);
            var smallest = ch.First();
            return ch.Count(x => x.Equals(smallest));
        }

        public string DefangIP(string address)
        {
            var str = address.Split('.');
            var ip = string.Join("[.]", str);
            return ip;
        }

        //https://leetcode.com/problems/find-duplicate-file-in-system/
        public IList<IList<string>> FindDuplicate(string[] paths)
        {
            // Create a dictionary Key: file content, value: file path
            // Input String will be in "root/a 1.txt(abcd) 2.txt(efgh)"
            // split by space, 1st: DirectoryInfo, the rest is files
            // Each File has content in between ( )
            // Find Content of the file using $([a-zA-Z]) 

            var dict = new Dictionary<string, IList<string>>();
            foreach (var path in paths)
            {
                var str = path.Split(' ');
                var dir = str[0];
                for (int i = 1; i < str.Length; i++)
                {
                    var f = str[i];
                    var idx = f.IndexOf('(');
                    var fn = f.Substring(0, idx);
                    var c = f.Substring(idx + 1).Replace(')', ' ').Trim();
                    var dir_info = $"{dir}/{fn}";
                    if (dict.ContainsKey(c))
                        dict[c].Add(dir_info);
                    else dict.Add(c, new List<string> {dir_info});
                }
            }

            var result = dict.Where(x => x.Value.Count > 1)
                .Select(x => x.Value)
                .ToList();

            return result;
        }

        [Fact]
        public void TestFindDupliate()
        {
            var input = new string[]
                {"root/a 1.txt(abcd) 2.txt(efgh)", "root/c 3.txt(abcd)", "root/c/d 4.txt(efgh)", "root 4.txt(efgh)"};
            var result = FindDuplicate(input);
        }

        //https://leetcode.com/problems/valid-sudoku/solution/

        public bool IsValidSudoku(char[][] board)
        {
            var rows = new Dictionary<int, int>[9];
            var cols = new Dictionary<int, int>[9];
            var boxes = new Dictionary<int, int>[9];
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new Dictionary<int, int>();
                cols[i] = new Dictionary<int, int>();
                boxes[i] = new Dictionary<int, int>();
            }

            // Validate board
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char num = board[i][j];
                    if (num != '.')
                    {
                        int n = (int) num;
                        int b_idx = (i / 3) * 3 + j / 3;

                        // Store the Current Cell Value
                        rows[i].AddOrUpdate(n, 1);
                        cols[i].AddOrUpdate(n, 1);
                        boxes[i].AddOrUpdate(n, 1);

                        // Check
                        if (rows[i][n] > 1 || cols[j][n] > 1 || boxes[b_idx][n] > 1)
                            return false;
                    }
                }
            }

            return true;
        }


        public bool IsValidSudoku1(char[][] board)
        {
            var rows = new HashSet<char>[9];
            var cols = new HashSet<char>[9];
            var boxes = new HashSet<char>[9];

            // Initialise the set
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new HashSet<char>();
                cols[i] = new HashSet<char>();
                boxes[i] = new HashSet<char>();
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var num = board[i][j];
                    if (num.Equals('.')) continue;

                    var bIdx = (i / 3) * 3 + j / 3;

                    if (boxes[bIdx].Contains(num) ||
                        rows[i].Contains(num) ||
                        cols[j].Contains(num))
                        return false;

                    boxes[bIdx].Add(num);
                    rows[i].Add(num);
                    cols[j].Add(num);
                }
            }

            return true;
        }
    }


    #region Design Hash Set

    /**
     * Your MyHashSet object will be instantiated and called as such:
     * MyHashSet obj = new MyHashSet();
     * obj.Add(key);
     * obj.Remove(key);
     * bool param_3 = obj.Contains(key);
     */
    public class MyHashSet
    {
        /** Initialize your data structure here. */
        private int _hashFactor = 10000;

        private int[] _arr;

        public MyHashSet()
        {
            _arr = new int [_hashFactor];
        }

        public void Add(int key)
        {
            var hashValue = GetHashValue(key);
            _arr[hashValue] = key;
        }

        public void Remove(int key)
        {
            var hashValue = GetHashValue(key);
            try
            {
                _arr[hashValue] = 0;
            }
            catch (Exception e)
            {
            }
        }

        private int GetHashValue(int key)
        {
            return key % _hashFactor;
        }

        /** Returns true if this set contains the specified element */
        public bool Contains(int key)
        {
            try
            {
                return _arr[key] != 0;
            }
            catch
            {
                return false;
            }
        }
    }

    #endregion
}