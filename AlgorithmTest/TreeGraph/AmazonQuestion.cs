using System.Diagnostics;

namespace AlgorithmTest.TreeGraph
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

    public class AmazonQuestion
    {

        public bool TreeEqual(TreeNode x, TreeNode y)
        {
            if (x == null && y == null)
                return true;
            
            if (x == null || y == null)
                return false;

            return x.val == y.val && 
                   TreeEqual(x.left, y.left) &&
                   TreeEqual(x.right, y.right);
        }

        public bool TraverSubTree(TreeNode sub, TreeNode tree)
        {
            return sub != null &&
                   (TreeEqual(sub, tree) || TraverSubTree(sub.left, tree)
                                         || TraverSubTree(sub.right, tree));
        }
    }
}