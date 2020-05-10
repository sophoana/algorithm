using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    public class GraphNode
    {
        public int Id { get; set; }
        public int LowLink { get; set; }
        public IList<GraphNode> Adjacency { get; set; }
    }

    public class CriticalNetworkQuestion
    {
        private static int T = 1;

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (var connection in connections)
            {
                graph[connection[0]].Add(connection[1]);
                graph[connection[1]].Add(connection[0]);
            }

            int[] timestamp = new int[n];
            IList<IList<int>> criticalConnection = new List<IList<int>>();
            DepthFirstSearch(n, graph, timestamp, 0, -1, criticalConnection);

            return criticalConnection;
        }

        private int DepthFirstSearch(int n, List<int>[] graph, int[] timestamp, int i, int parent,
            IList<IList<int>> criticalConnection)
        {
            if (timestamp[i] != 0)
                return timestamp[i];
            
            timestamp[i] = T++;

            int minTimeStamp = int.MaxValue;
            foreach (var neighbor in graph[i])
            {
                if (neighbor == parent)
                    continue;
                int neighborTimestamp = DepthFirstSearch(n, graph, timestamp, neighbor, i, criticalConnection);
                minTimeStamp = Math.Min(minTimeStamp, neighborTimestamp);
            }

            if (minTimeStamp >= timestamp[i])
                if (parent >= 0)
                    criticalConnection.Add(new List<int> {parent, i});

            return Math.Min(timestamp[i], minTimeStamp);
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
                new List<int> {1, 3}//, new List<int> {3, 1}
            };

           // Assert.Contains(result, x => expected.Contains(x));
           Assert.Equal(expected, result);
        }
    }
}