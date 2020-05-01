using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace AlgorithmTest
{
    public class L
    {
        #region Add Binary

        public IList<int> AddToArrayForm(int[] A, int K)
        {
            var arr = getArrayFromInt(K);
            Array.Reverse(A);

            return AddTwoArray(A, arr);
        }

        private int[] ReverseArray(int[] a)
        {
            var arr = new int[a.Length];
            Array.Copy(a, arr, a.Length);
            Array.Reverse(arr);
            return arr;
        }

        [Fact]
        public void TestAddTwoArray()
        {
            var a = new int[] {0};
            var b = new int[] {8};
            var result = AddTwoArray(a, b);
        }

        public IList<int> AddTwoArray(int[] a, int[] b)
        {
            int aLen = a.Length;
            int bLen = b.Length;

            if (aLen < bLen)
                return AddTwoArray(b, a);

            var number = new int[aLen];
            var carry = 0;
            var idx = 0;

            for (int i = 0; i < bLen; i++)
            {
                idx = i;
                number[i] = (a[i] + b[i] + carry) % 10;
                carry = (a[i] + b[i] + carry) / 10;
            }

            while (++idx < aLen)
            {
                number[idx] = (a[idx] + carry) % 10;
                carry = (a[idx] + carry) / 10;
            }

            Array.Reverse(number);

            if (carry != 0)
            {
                var number1 = new int[aLen + 1];
                number1[0] = carry;

                Array.Copy(number, 0, number1, 1, aLen);

                return number1;
            }

            return number;
        }

        public int[] getArrayFromInt(int k)
        {
            IList<int> number = new List<int>();
            int n = k;
            while (n != 0)
            {
                var c = n % 10;
                number.Add(c);
                n = n / 10;
            }

            return number.ToArray();
        }

        [Fact]
        public void TestGetArrayFromInt()
        {
            var input = 5;
            var result = getArrayFromInt(input);

            Assert.True(result[0] == 5);
        }

        public int[] PlusOne(int[] digits)
        {
            int n = digits.Length;
            for (int i = n - 1; i >= 0; --i)
            {
                if (digits[i] == 9)
                    digits[i] = 0;
                else
                {
                    digits[i]++;
                    return digits;
                }
            }

            digits = new int[n + 1];
            digits[0] = 1;

            return digits;
        }

        [Fact]
        public void TestPlusOne()
        {
            var input = new int[] {9, 9, 9};
            var result = PlusOne(input);

            Assert.True(result.Length == 4);
        }

        public string AddBinary(string a, string b)
        {
            int aLen = a.Length;
            int bLen = b.Length;
            if (aLen < bLen) return AddBinary(b, a);
            var number = new List<Char>();
            var maxIdx = Math.Max(aLen, bLen);
            int carry = 0, j = bLen - 1;

            for (int i = maxIdx - 1; i > -1; i--)
            {
                if (a[i] == '1') carry++;
                if (j > -1 && b[j--] == '1') carry++;

                if (carry % 2 == 1) number.Add('1');
                else number.Add('0');

                carry /= 2;
            }

            if (carry == 1) number.Add('1');
            var arr = number.ToArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        #endregion

        #region AddTwoNumbers

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummyHead = new ListNode(0);
            ListNode p = l1, q = l2, curr = dummyHead;

            int carry = 0;

            while (p != null || q != null)
            {
                var x = p?.val ?? 0;
                var y = q?.val ?? 0;

                var sum = carry + x + y;
                carry = sum / 10;
                curr.next = new ListNode(sum % 10);
                curr = curr.next;

                if (p != null) p = p.next;
                if (q != null) q = q.next;
            }

            if (carry > 0)
                curr.next = new ListNode(carry);
            return dummyHead.next ?? dummyHead;
        }

        [Fact]
        public void TestAddTwoNumbers()
        {
            var l1 = getNode(342);
            var l2 = getNode(465);
            var result = AddTwoNumbers(l1, l2);
        }

        private int getNumber(ListNode node)
        {
            var number = string.Empty;
            while (node != null)
            {
                number = node.val + number;
                node = node.next;
            }

            return Convert.ToInt32(number);
        }

        private ListNode getNode(int number)
        {
            var chArr = number.ToString().ToCharArray();
            ListNode node = new ListNode(Convert.ToInt32(chArr[chArr.Length - 1].ToString()));
            ListNode dummyHead = node;
            for (int i = chArr.Length - 2; i >= 0; i--)
            {
                node.next = new ListNode(Convert.ToInt32(chArr[i].ToString()));
                node = node.next;
            }

            return dummyHead;
        }

        #endregion

        #region Region

        public int ClimbStairs(int n)
        {
            if (n == 1)
                return 1;

            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        #endregion
    }

    public class NodeFactory
    {
        public static ListNode CreateNode(IList<int> nums)
        {
            ListNode node = new ListNode(0);
            ListNode sentinel = node;
            if (nums == null || !nums.Any())
                return null;
            
            foreach (var num in nums)
            {
                node.next = new ListNode(num);
                node = node.next;
            }
            return sentinel.next;
        }
    }
    
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }
    }
}