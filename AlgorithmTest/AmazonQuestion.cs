using System.Collections.Generic;
using Xunit;

namespace AlgorithmTest
{
    public class AmazonQuestion
    {
        [Fact]
        public void Test()
        {
            Assert.True(1 + 1 == 2);
        }

        class UnionFind
        {
            private int count;
            private int[] parent;
            private int[] rank;

            public UnionFind(int m, int n, int[,] grid)
            {
                count = 0;
                parent = new int[m * n];
                rank = new int[n * n];

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (grid[i, j] == 1)
                        {
                            parent[i * n + j] = i * n + j;
                            count++;
                        }

                        rank[i * n + j] = 0;
                    }
                }
            }

            public int find(int i)
            {
                if (parent[i] != i) parent[i] = find(parent[i]);
                return parent[i];
            }

            public void union(int x, int y)
            {
                // union with rank
                int rootx = find(x);
                int rooty = find(y);
                if (rootx != rooty)
                {
                    if (rank[rootx] > rank[rooty])
                    {
                        parent[rooty] = rootx;
                    }
                    else if (rank[rootx] < rank[rooty])
                    {
                        parent[rootx] = rooty;
                    }
                    else
                    {
                        parent[rooty] = rootx;
                        rank[rootx] += 1;
                    }

                    --count;
                }
            }

            public int GetCount()
            {
                return count;
            }
        }


        int numberAmazonTreasureTrucks(int rows, int column, int[,] grid)
        {
            if (grid == null || grid.Length == 0)
                return 0;

            UnionFind uf = new UnionFind(rows, column, grid);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    if (grid[r, c] == 1)
                    {
                        grid[r, c] = 0;
                        if (r - 1 >= 0 && grid[r - 1, c] == 1)
                        {
                            uf.union(r * column + c, (r - 1) * column + c);
                        }

                        if (r + 1 < rows && grid[r + 1, c] == 1)
                        {
                            uf.union(r * column + c, (r + 1) * column + c);
                        }

                        if (c - 1 >= 0 && grid[r, c - 1] == 1)
                        {
                            uf.union(r * column + c, r * column + c - 1);
                        }

                        if (c + 1 < column && grid[r, c + 1] == 1)
                        {
                            uf.union(r * column + c, r * column + c + 1);
                        }
                    }
                }
            }

            return uf.GetCount();
        }
    }
}