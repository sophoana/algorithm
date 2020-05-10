using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    public class CriticalNetworkQuestion_OLD
    {
        //https://leetcode.com/problems/critical-connections-in-a-network/

        private IDictionary<int, List<int>> _graph = new Dictionary<int, List<int>>();
        private IList<int> _lowestRank = new List<int>();
        private IList<bool> _visited = new List<bool>();

        private void Init(int n, IList<IList<int>> connections)
        {
            var adjacency = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                var dest = connections[i][0];
                var source = connections[i][1];

                if (adjacency.ContainsKey(dest)) adjacency[dest].Add(source);
                else adjacency.Add(dest, new List<int> {source});

                if (adjacency.ContainsKey(source)) adjacency[source].Add(dest);
                else adjacency.Add(source, new List<int> {dest});

                // Update lowest Rank
                _lowestRank.Add(i);
                _visited.Add(false);
            }

            _graph = adjacency;
        }

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            int currentRank = 0;
            Init(n, connections);
            List<IList<int>> result = new List<IList<int>>();
            int prevVertex = -1;
            int currentVertex = 0;

            // Call 
            DFS(result, currentRank, currentVertex, prevVertex);

            return result;
        }

        private void DFS(IList<IList<int>> result, int currentRank, int currentVertex, int prevVertex)
        {
            _visited[currentVertex] = true;
            _lowestRank[currentVertex] = currentRank;

            foreach (var node in _graph[currentVertex])
            {
                if (node == prevVertex) continue;

                if (!_visited[node])
                {
                    DFS(result, currentRank + 1, currentVertex, node);
                }

                _lowestRank[currentVertex] = Math.Min(_lowestRank[currentVertex], _lowestRank[node]);
                if (_lowestRank[node] >= currentRank + 1)
                    result.Add(new List<int> {currentVertex, node});
            }
        }

        private static IList<IList<int>> ThisIsWrong(int n, IList<IList<int>> connections)
        {
            var adjacents = new Dictionary<int, IList<int>>();

            for (int i = 0; i < n; i++)
            {
                var dest = connections[i][0];
                var source = connections[i][1];

                if (adjacents.ContainsKey(dest)) adjacents[dest].Add(source);
                else adjacents.Add(dest, new List<int> {source});

                if (adjacents.ContainsKey(source)) adjacents[source].Add(dest);
                else adjacents.Add(source, new List<int> {dest});
            }

            var critial = adjacents.Where(x => x.Value.Count == 1)
                .Select(x => new List<int> {x.Key, x.Value[0]});

            var result = new List<IList<int>>();
            foreach (var c in critial)
            {
                result.Add(c);
            }

            return result;
        }

        [Fact]
        public void Test_CriticalConnections()
        {
            // var input = new List<IList<int>>
            // {
            //     new List<int> {1, 0},
            //     new List<int> {1, 2},
            //     new List<int> {2, 0}, 
            //     new List<int> {1, 3}
            // };

            var input = new List<IList<int>>
            {
                new List<int> {0, 1},
                new List<int> {1, 2},
                new List<int> {2, 0},
                new List<int> {1, 3},
                new List<int> {3, 4},
                new List<int> {4, 5},
                new List<int> {5, 3},
            };
            var result = CriticalConnections(6, input);
            var expected = new List<IList<int>>
            {
                new List<int> {1, 3}, new List<int> {3, 1}
            };

            Assert.Contains(result, x => expected.Contains(x));
        }
    }
}