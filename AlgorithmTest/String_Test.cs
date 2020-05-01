using System.Linq;
using Xunit;

namespace Tests
{
    public class String_Test
    {
        [Fact]
        public void Test_CountLetters()
        {
            var input = "aaaaaaaaaa";
            var result = CountLetters(input);
        }

        public int CountLetters_Version2(string s)
        {
            var count = 1;
            var result = 0;
            for (var i = 1; i < s.Length; i++)
            {
                if (i == s.Length || s[i] != s[i - 1])
                {
                    result += count * (count + 1) / 2;
                    count = 1;
                }
                else count++;
            }

            return result;
        }

        public int CountLetters(string S)
        {
            //https://leetcode.com/problems/count-substrings-with-only-one-distinct-letter/

            var count = 0;
            var n = 1;
            var arr = S.ToCharArray();
            char prev = arr.First();

            for (int i = 1; i < arr.Length; i++)
            {
                char ch = arr[i];
                if (prev != ch)
                {
                    count += n * (n + 1) / 2;
                    prev = ch;
                    n = 1;
                }
                else
                {
                    n++;
                }
            }

            count += n * (n + 1) / 2;
            return count;
        }
    }
}