using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class MockOne
    {
        public IList<IList<int>> Permute(int[] nums)
        {
            List<IList<int>> output = new List<IList<int>>();
            int n = nums.Length;
            permutation(n, nums.ToList(), output, 0);

            return output;
        }

        private void permutation(int n, List<int> nums, List<IList<int>> output, int first)
        {
            if (first == n)
                output.Add(new List<int>(nums));

            for (int i = first; i < n; i++)
            {
                Swap(nums, first, i);
                permutation(n, nums, output, first + 1);
                Swap(nums, first, i);
            }
        }

        private void Swap(List<int> nums, int i, int j)
        {
            var tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }
    }
    

    public class Trie
    {
        #region Definition

        class TrieNode
        {
            private readonly TrieNode[] _links;

            public TrieNode()
            {
                _links = new TrieNode[26];
            }

            public bool ContainsKey(char ch)
            {
                // Index is between 0-26 [a-z]
                
                var idx = ch - 'a';
                return _links[idx] != null;
            }

            public TrieNode Get(char ch)
            {
                var idx = ch - 'a';
                return _links[idx];
            }

            public void Put(char ch, TrieNode node)
            {
                var idx = ch - 'a';
                _links[idx] = node;
            }

            public bool IsEnd { get; set; }
        }

        #endregion
        
        private readonly IDictionary<string, int> _dictionary;
        private TrieNode root;
        
        /** Initialize your data structure here. */
        public Trie()
        {
            _dictionary = new Dictionary<string, int>();
            root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            if (_dictionary.ContainsKey(word)) _dictionary[word]++;
            else _dictionary.Add(word, 1);

            TrieNode node = root;
            for (int i = 0; i < word.Length; i++)
            {
                char currentChar = word[i];
                if(!node.ContainsKey(currentChar))
                    node.Put(currentChar, new TrieNode());
                node = node.Get(currentChar);
            }

            node.IsEnd = true;
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            return _dictionary.ContainsKey(word);
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            /* a, p, p, l, e
             *        a
             *     p
             * p
             */
            return SearchPrefix(prefix) != null;
        }

        private TrieNode SearchPrefix(string prefix)
        {
            TrieNode node = root;
            for (int i = 0; i < prefix.Length; i++)
            {
                char ch = prefix[i];
                if (node.ContainsKey(ch))
                    node = node.Get(ch);
                else
                {
                    return null;
                }
            }

            return node;
        }
    }
}