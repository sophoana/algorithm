using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace AlgorithmTest
{
    public static class LeetCodeUtil
    {
        static readonly HttpClient client = new HttpClient();


        // Gets input string from leetcode api
        public static async Task<string> GetString(string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }
    }
    
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

        public int StairCounting(int n)
        {
            if (n <= 2) return n;
            return StairCounting(n - 1) + StairCounting(n - 2);
        }

        [Fact]
        public void Test_StairCounting()
        {
            var one = StairCounting(4);
            var two = StairCounting(5);
            var three = StairCounting(6);
            var five = StairCounting(9);
            var four = StairCounting(10);
        }
        
        public string IntToRoman(int num) {
            int[] values = {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};    
            string[] symbols = {"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};

            var sb = new StringBuilder();
            for(int i=0; i<values.Length; i++){
                while(values[i] <= num){
                    num -= values[i];
                    sb.Append(symbols[i]);
                }
            }

            return sb.ToString();
        }
        

        //https://leetcode.com/problems/roman-to-integer/solution/
        public int RomanToInt(string s) {
            var map = new Dictionary<string, int>()
            {
                {"M", 1000}, {"D", 500}, {"C", 100}, {"L", 50}, {"X", 10}, {"V", 5}, {"I", 1}
            };

            int sum = 0;
            int i = 0;
            while (i < s.Length)
            {
                string currentSymbol = s.Substring(i, 1);
                int currentValue = map[currentSymbol];
                int nextValue = 0;

                if (i + 1 < s.Length)
                {
                    string nextSymbol = s.Substring(i + 1, 1);
                    nextValue = map[nextSymbol];
                }

                if (currentValue >= nextValue)
                {
                    sum += currentValue;
                    i++;
                }
                else
                {
                    sum = sum + (nextValue - currentValue);
                    i += 2;
                }
            }

            return sum;
        }

        [Fact]
        public void Test_RomanToInt()
        {
            var input = "MCMXCIV";// "MMCMLXXXIX";
            var expected = 1994; //2989;
            Assert.Equal(expected, RomanToInt(input));
            Assert.Equal(2989, RomanToInt("MMCMLXXXIX"));
            Assert.Equal(3, RomanToInt("III"));
            Assert.Equal(9, RomanToInt("IX"));
        }
        
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