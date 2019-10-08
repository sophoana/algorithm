using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class LinkedList_Test
    {
        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode curr = head;

            while (curr != null)
            {
                ListNode nextTemp = curr.next;
                curr.next = prev;
                prev = curr;
                curr = nextTemp;
            }

            return prev;

//            Stack<ListNode> stack = new Stack<ListNode>();
//            while (head != null)
//            {
//                stack.Push(head);
//                head = head.next;
//            }
//
//            ListNode answer = stack.Pop();
//            while (stack.Count != 0)
//            {
//                answer.next = stack.Pop();
//            }
//
//            return answer;
        }
    }

    // Definition for singly-linked list.
    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int x)
        {
            val = x;
        }
    }
}