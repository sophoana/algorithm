using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    public class StringQuestion
    {
        public int FirstUniqChar(string s) {
            // have a dictionary to store char and its occurence
            // Find the first Unique char
            var dict = new Dictionary<char, int>();
            foreach (var sh in s)
            {
                if (dict.ContainsKey(sh))
                    dict[sh]++;
                else dict.Add(sh, 1);
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]) && dict[s[i]] == 1)
                    return i;
            }

            return -1;
        }
        
        public string FrequencySort(string s)
        {
            var dict = new Dictionary<char, int>();
            foreach (var sh in s.ToCharArray())
            {
                if (dict.ContainsKey(sh)) dict[sh]++;
                else dict.Add(sh, 1);
            }

            var ch = dict
                .OrderByDescending(x => x.Value)
                .Select(GetString);

            return GetString(ch);
        }

        [Fact]
        public void TestFrequencySort()
        {
            var input = "cccaaa";
            var result = FrequencySort(input);
            Assert.True("cccaaa" == result);
        }

        private string GetString(IEnumerable<string> str)
        {
            var sb = new StringBuilder();
            foreach (var s in str)
            {
                sb.Append(s);
            }

            return sb.ToString();
        }

        private string GetString(KeyValuePair<char, int> kv)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < kv.Value; i++)
            {
                sb.Append(kv.Key);
            }

            return sb.ToString();
        }
    }
}