using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class HardQuestion
    {
        //https://leetcode.com/problems/trapping-rain-water/
        public int Trap(int[] height) {
            // Find Maximum height of bar from the left end upto an index i in the array left-max
            // Find maximum height of bar from the right end upto an index i in the array right-max
            // iterate over the height array and update answer
            // answer += min(left-max(i), right-max(i)) - height(i)
            
            // Dynamic Programming
            int answer = 0;
            int n = height.Length;
            var left_max = new int[n];
            var right_max = new int[n];

            left_max[0] = height[0];
            for (int i = 1; i < n; i++)
            {
                left_max[i] = Math.Max(height[i], left_max[i - 1]);
            }

            right_max[n - 1] = height[n - 1];
            for (int j = n - 2; j >=0; j--)
            {
                right_max[j] = Math.Max(height[j], right_max[j + 1]);
            }

            for (int i = 1; i < n - 1; i++)
            {
                answer += Math.Min(left_max[i], right_max[i]) - height[i];
            }

            return answer;
        }

        public int TrapStack(int[] height)
        {
            int answer = 0;
            int current = 0;
            Stack<int> stack = new Stack<int>();
            while (current < height.Length)
            {
                while (stack.Count != 0 && height[current]> height[stack.Peek()])
                {
                    int top = stack.Pop();
                    if (stack.Count == 0)
                    {
                        break;
                    }

                    int distance = current - stack.Peek() - 1;
                    int bounded_height = Math.Min(height[current], height[stack.Peek()]) - height[top];
                    answer += distance * bounded_height;
                }
                stack.Push(current++);
            }

            return answer;
        }
        
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