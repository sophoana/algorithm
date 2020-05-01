using System;
using System.Linq;
using Xunit;

namespace AlgorithmTest.ThirtyDayChallenge
{
    public class WeekFour
    {
        public bool CheckValidString(string s)
        {
            // Keep score of ( and ) where ( +1 and ) -1
            // Keep count of *
            // if score == 0 and count of * even return true
            // if score != 0, find offset = count of * - abs(score)
            // if offset is even return true, 
            // else return false

            var score = 0;
            var count = 0;
            foreach (var ch in s.ToCharArray())
            {
                if (ch == '(') score++;
                else if (ch == ')')
                {
                    if (score + count <= 0) return false;
                    score--;
                }
                else if (ch == '*') count++;
            }

            if (score == 0 )
                return true;

            var offset = count - Math.Abs(score);
            if (offset >= 0)
                return true;

            return false;
        }

        [Fact]
        public void TestValidParenthesis()
        {
            var input = ")(";
            Assert.False(CheckValidString(input));
            Assert.True(CheckValidString("()"));
            Assert.True(CheckValidString("(*)"));
            Assert.True(CheckValidString("(*))"));
            Assert.True(CheckValidString("((**()"));
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            int prod = 1;
            for (int i = 1; i < n; i++)
            {
                int cur = 1;
                int j = i;

                while (j < n) cur = cur * nums[j++];
                result[i - 1] = cur * prod;
                prod = prod * nums[i - 1];

                if (i == n - 1)
                    result[i] = prod;
            }

            return result;
        }

        [Fact]
        public void TestProductExceptSelt()
        {
            var input = new int[] {1, 2, 3, 4};
            var result = ProductExceptSelf(input);
        }

        public int[] ProductExceptSelf1(int[] nums)
        {
            var prod = 1;
            var result = new int[nums.Length];
            foreach (var num in nums)
            {
                prod = prod * num;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = nums[i] == 0 ? prod : prod / nums[i];
            }

            return result;
        }

        public int NumIslands(char[][] grid)
        {
            if (grid == null || grid.Length == 0)
                return 0;

            int nr = grid.Length;
            int nc = grid[0].Length;
            int num = 0;

            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        num++;
                        SearchDFS(i, j, grid);
                    }
                }
            }

            return num;
        }

        private void SearchDFS(int i, int j, char[][] grid)
        {
            int nr = grid.Length;
            int nc = nr != 0 ? grid[0].Length : 0;

            if (i < 0 || j < 0 || i >= nr || j >= nc || grid[i][j] == '0')
                return;

            // Mark as visited '0'
            grid[i][j] = '0';
            SearchDFS(i - 1, j, grid);
            SearchDFS(i + 1, j, grid);
            SearchDFS(i, j - 1, grid);
            SearchDFS(i, j + 1, grid);
        }
    }
}