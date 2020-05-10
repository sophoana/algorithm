using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    //https://leetcode.com/problems/course-schedule-ii/solution/
    public class CourseScheduleQuestion
    {
        private readonly int WHITE = 1;
        private readonly int GRAY = 2;
        private readonly int BLACK = 3;

        private bool _isPossible;
        private IDictionary<int, int> _color;
        private IDictionary<int, IList<int>> _adjacents;
        private IList<int> _topologicalOrder;

        private void Init(int numCourses)
        {
            _isPossible = true;
            _color = new Dictionary<int, int>();
            _adjacents = new Dictionary<int, IList<int>>();
            _topologicalOrder = new List<int>();

            for (int i = 0; i < numCourses; i++)
            {
                _color.Add(i, WHITE);
            }
        }

        private void DFS(int node)
        {
            if (_isPossible == false)
                return;

            if (_color.ContainsKey(node))
                _color[node] = GRAY;

            // Traverse neighboring vertex

            if (_adjacents.ContainsKey(node))
                foreach (var neighbor in _adjacents[node])
                {
                    if (_color[neighbor] == this.WHITE)
                        DFS(neighbor);
                    else if (_color[neighbor] == GRAY)
                    {
                        this._isPossible = false;
                    }
                }

            // Recursive end. Put Node to Black
            _color[node] = this.BLACK;
            _topologicalOrder.Add(node);
        }

        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            Init(numCourses);

            // Create adjacency list for graph
            for (int i = 0; i < prerequisites.Length; i++)
            {
                int dest = prerequisites[i][0];
                int source = prerequisites[i][1];
                var adj = _adjacents.ContainsKey(source) ? _adjacents[source] : new List<int>();
                adj.Add(dest);

                if (_adjacents.ContainsKey(source))
                    _adjacents[source] = adj;
                else _adjacents.Add(source, adj);
            }

            // if the node is unprocessed then call dfs on it.
            for (int i = 0; i < numCourses; i++)
            {
                if (_color[i] == WHITE)
                    DFS(i);
            }

            int[] order;
            if (_isPossible)
            {
                order = new int [numCourses];
                for (int i = 0; i < numCourses; i++)
                {
                    // Pop stack
                    order[i] = _topologicalOrder[numCourses - i - 1];
                }
            }
            else
            {
                order = new int [0];
            }

            return order;
        }

        [Fact]
        public void Test_CourseSchedule()
        {
            var input = new int[][] {new int[] { 0,1}};
            var num = 2;
            var output = FindOrder(num, input);
        }
    }
}