using System;
using System.Diagnostics;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class HardQuestion
    {
        //https://leetcode.com/problems/reverse-nodes-in-k-group/solution/
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode ptr = head;
            ListNode kTail = null;
            ListNode newHead = null;

            while (ptr != null)
            {
                int count = 0;
                ptr = head;

                while (count < k && ptr != null)
                {
                    ptr = ptr.next;
                    count++;
                }
                
                // If we counted k-nodes - reverse them
                if (count == k)
                {
                    ListNode revHead = ReverseLinkedList(head, k);
                    if (newHead == null)
                        newHead = revHead;

                    if (kTail != null)
                        kTail.next = revHead;

                    kTail = head;
                    head = ptr;
                }
            }

            if (kTail != null)
                kTail.next = head;

            return newHead ?? head;
        }

        public ListNode ReverseLinkedList(ListNode head, int k)
        {
            // Reverse k nodes of the given linked list
            // this function assumes that the list contains
            // at least k node.
            ListNode newHead = null;
            ListNode ptr = head;
            while (k > 0)
            {
                ListNode next = ptr.next;
                ptr.next = newHead;
                newHead = ptr;
                
                // Move Pointer
                ptr = next;

                k--;
            }

            return newHead;
        }

        #region Recursive Approach

        public ListNode ReverseKGroupRecursive(ListNode head, int k)
        {
            int count = 0;
            ListNode ptr = head;
            while (count < k && ptr != null)
            {
                ptr = ptr.next;
                count++;
            }

            if (count == k)
            {
                ListNode reversedHead = ReverseLinkedListRecursive(head, k);
                head.next = ReverseKGroupRecursive(ptr, k);
                return reversedHead;
            }

            return head;
        }
        
        public ListNode ReverseLinkedListRecursive(ListNode head, int k)
        {
            ListNode newHead = null;
            ListNode ptr = head;

            while (k > 0)
            {
                ListNode nextNode = ptr.next;
                ptr.next = newHead;
                newHead = ptr;
                ptr = nextNode;
                k--;
            }

            return newHead;
        }

        #endregion
    }
}