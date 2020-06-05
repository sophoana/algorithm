using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    public class UniqueBinaryTree
    {
        public int[] SortedSquares(int[] A)
        {
            Array.Sort(A, (x, s) => { return 1; });
            return new[] {2};
        }

        private string GetString(int idx, int n, string[] memo)
        {
            if (idx <= 1)
            {
                return "0";
            }

            if (memo[idx] != null)
                return memo[idx];

            var prev = memo[idx - 1];
            var next = string.Empty;

            if (!string.IsNullOrEmpty(prev))

                foreach (var ch in prev.ToCharArray())
                {
                    if (ch == '0')
                        next = next + "01";
                    else if (ch == '1')
                        next = next + "10";
                }

            memo[idx] = next;
            return memo[idx];
        }

        public int KThGrammer(int N, int K)
        {
            // if (N == 1) return 0;
            // if (K <= 1)
            //     return KThGrammer(N - 1, K);
            //
            // return KThGrammer(N - 1, K - (1 << N - 2)) ^ 1;

            if (N == 1)
                return 0;
            return (~K & 1) ^ KThGrammer(N - 1, (K + 1) / 2);
        }

        [Fact]
        public void Test_GetString()
        {
            var memo = new string[6];
            var result = GetString(5, 5, memo);
            var result1 = KThGrammer(2, 1);
        }

        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
                return new List<TreeNode>();
            return GenerateTrees(1, n);
        }

        private IList<TreeNode> GenerateTrees(int start, int end)
        {
            List<TreeNode> all = new List<TreeNode>();
            if (start > end)
            {
                all.Add(null);
                return all;
            }

            for (int i = start; i <= end; i++)
            {
                // all left possible subtree
                var left_tree = GenerateTrees(start, i - 1);

                var right_tree = GenerateTrees(i + 1, end);

                foreach (var left in left_tree)
                {
                    foreach (var right in right_tree)
                    {
                        var current = new TreeNode(i);
                        current.left = left;
                        current.right = right;

                        all.Add(current);
                    }
                }
            }

            return all;
        }
    }
}