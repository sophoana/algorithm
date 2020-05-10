namespace AlgorithmTest
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int x)
        {
            val = x;
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
                dfs(node.left);
                if (prev != null) FindMin(ans, node.val - prev.Value);

                prev = node.val;
                dfs(node.right);

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