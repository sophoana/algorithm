using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class LinkedListQuestion
    {
        public ListNode GetIntersectionNodeUsingMap(ListNode headA, ListNode headB) {
            var set  = new HashSet<ListNode>();
            while (headA != null)
            {
                if (!set.Contains(headA))
                    set.Add(headA);
                headA = headA.next;
            }

            while (headB != null)
            {
                if (set.Contains(headB))
                    return headB;
                headB = headB.next;
            }
            return null;
        }
        
        //https://leetcode.com/problems/merge-k-sorted-lists/solution/
        public ListNode MergeKLists(List<ListNode> lists)
        {
            if (lists == null || !lists.Any()) return null;
            if (lists.Count == 1) return lists.First();

            ListNode node1 = lists.First();

            for (int i = 1; i < lists.Count; i++)
            {
                var node2 = lists[i];
                node1 = MergeTwoLists(node1, node2);
            }

            return node1;
        }

        private ListNode MergeKListsSortedList(List<ListNode> lists)
        {
            ListNode point = new ListNode(0);
            ListNode head = point;
            
            var queue = new SortedList<int, ListNode>();

            foreach (var node in lists)
            {
                if (node != null)
                    queue.Add(node.val, node);
            }

            while (queue.Count != 0)
            {
                var val = queue.FirstOrDefault();
                ListNode node = val.Value;
                point.next = new ListNode(val.Key);
                point = point.next;
                node = node.next;
                
                if(node != null)
                    queue.Add(node.val, node);
            }

            return head.next;
        }

        [Fact]
        public void TestMergeKLists()
        {
            // TODO:
        }

        //https://leetcode.com/problems/linked-list-cycle/solution/
        public bool HasCycle(ListNode head)
        {
            // There are two approach
            // 1. use map to determine whether the node has been visited before
            // 2. use 2 pointers, if the 2 pointers meet, it has cycle.

            return HasCycle2Pointers(head);
        }

        [Fact]
        public void TestHasCycle()
        {
            var arr = new List<int> {3, 2, 0, -4};
            var input = NodeFactory.CreateNode(arr);
            var result = HasCycle(input);
            Assert.True(result);
        }

        private bool HasCycle2Pointers(ListNode node)
        {
            if (node == null || node.next == null)
                return false;

            ListNode fast = node, slow = node;
            while (fast != null && fast.next != null)
            {
                if (fast == slow) return true;
                fast = fast.next.next;
                slow = slow.next;
            }

            return false;
        }

        private bool HasCycleMap(ListNode node)
        {
            if (node == null || node.next == null)
                return false;

            var set = new HashSet<ListNode>();
            while (node != null)
            {
                if (set.Contains(node))
                    return true;

                set.Add(node);
                node = node.next;
            }

            return false;
        }

        //https://leetcode.com/problems/remove-duplicates-from-sorted-list/
        public ListNode DeleteDuplicates(ListNode head)
        {
            // 1- Have 2 pointer - current and Previous
            // 2- Move the them together if current.next not null, has different value
            // 3- otherwise, move current until null or meet next different value
            // 4- Point prev.next to current. Repeat step 2.

            ListNode curr = head;
            while (curr != null && curr.next != null)
            {
                if (curr.next.val == curr.val)
                    curr.next = curr.next.next;
                else
                {
                    curr = curr.next;
                }
            }

            return head;
        }

        //https://leetcode.com/problems/sort-list/submissions/
        public ListNode SortList(ListNode head)
        {
            // 1- Check if the head or head.next is null
            // 2- Find the middle node - unlink one node before the middle
            // 3- Recursively repeat step 2.
            // 4- Merge two nodes orderly

            if (head == null || head.next == null) return head;

            // Find middle Node and cut before the middle node
            ListNode prev = null;
            ListNode slow = head, fast = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                prev = slow;
                slow = slow.next;
            }

            // Cut connection at Previous 
            prev.next = null;

            // Sort half node using recursive
            ListNode node1 = SortList(head);
            ListNode node2 = SortList(slow);

            // Merge the 2 nodes
            return MergeTwoLists(node1, node2);
        }

        private ListNode MergeSort2Nodes(ListNode node1, ListNode node2)
        {
            ListNode sentinel = new ListNode(0);
            ListNode prev = sentinel;

            while (node1 != null && node2 != null)
            {
                if (node1.val > node2.val)
                {
                    prev.next = node2;
                    node2 = node2.next;
                }
                else
                {
                    prev.next = node1;
                    node1 = node1.next;
                }

                // Finish the last node
                if (node1 != null)
                    prev.next = node1;
                if (node2 != null)
                    prev.next = node2;
            }

            return sentinel.next;
        }


        // https://leetcode.com/problems/reorder-list/solution/
        public void ReorderList(ListNode head)
        {
            if (head == null) return;

            // Find the middle of linked list 
            ListNode slow = head, fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            // Reserve the second part
            ListNode prev = null;
            ListNode cur = slow, tmp;
            while (cur != null)
            {
                tmp = cur.next;

                cur.next = prev;
                prev = cur;
                cur = tmp;
            }

            // Merge list
            ListNode first = head;
            ListNode second = prev;
            while (second.next != null)
            {
                tmp = first.next;
                first.next = second;
                first = tmp;

                tmp = second.next;
                second.next = first;
                second = tmp;
            }
        }


        //https://leetcode.com/problems/plus-one-linked-list/solution/
        public ListNode PlusOne(ListNode head)
        {
            ListNode sentinel = new ListNode(0);
            sentinel.next = head;

            ListNode notNide = sentinel;

            while (head != null)
            {
                if (head.val != 9) notNide = head;
                head = head.next;
            }

            notNide.val++;
            notNide = notNide.next;

            while (notNide != null)
            {
                notNide.val = 0;
                notNide = notNide.next;
            }

            return sentinel.val != 0 ? sentinel : sentinel.next;
        }

        public ListNode MiddleNode(ListNode head)
        {
            return MiddleNodeIntuitive(head);
        }

        private static ListNode MiddleNodeFastSlow(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;
        }


        private static ListNode MiddleNodeIntuitive(ListNode head)
        {
            IList<ListNode> arr = new List<ListNode>();
            int idx = 0;
            while (head != null)
            {
                arr.Add(head);
                idx++;
                head = head.next;
            }

            return arr[idx / 2];
        }

        //https://leetcode.com/problems/merge-two-sorted-lists/
        public ListNode MergeTwoLists(ListNode node1, ListNode node2)
        {
            if (node1 == null)
                return node2;
            else if (node2 == null)
                return node1;
            else
            {
                if (node1.val < node2.val)
                {
                    node1.next = MergeTwoLists(node1.next, node2);
                    return node1;
                }

                node2.next = MergeTwoLists(node1, node2.next);
                return node2;
            }
        }

        public ListNode MergeTwoListsIterative(ListNode l1, ListNode l2)
        {
            ListNode prehead = new ListNode(-1);
            ListNode prev = prehead;

            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    prev.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    prev.next = l2;
                    l2 = l2.next;
                }

                prev = prev.next;
            }

            prev.next = l1 ?? l2;

            return prehead.next;
        }
    }
}