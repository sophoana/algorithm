namespace AlgorithmTest
{
    public class TreeNode
    {
        public int Val;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int x)
        {
            Val = x;
        }
    }

    public class TreeQuestion
    {
        #region BST

        public int MinDiffInBST(TreeNode root)
        {
            int? prev = null;
            int ans = int.MaxValue;

            void dfs(TreeNode node)
            {
                if (node == null) return;
                dfs(node.Left);
                if (prev != null) FindMin(ans, node.Val - prev.Value);

                prev = node.Val;
                dfs(node.Right);

            }

            int FindMin(int a, int b)
            {
                return a > b ? b : a;
            }


            return ans;
        }

        #endregion
    }
}