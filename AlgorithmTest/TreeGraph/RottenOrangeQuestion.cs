using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace AlgorithmTest.TreeGraph
{
    //https://leetcode.com/problems/rotting-oranges/solution/
    public class RottenOrangeQuestion
    {
        public int OrangeRotting(int[][] grid)
        {
            Queue<KeyValuePair<int, int>> queue = new Queue<KeyValuePair<int, int>>();

            // Build Set of Rotten Orange
            int freshOranges = 0;
            int rows = grid.Length;
            int cols = grid[0].Length;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r][c] == 2)
                        queue.Enqueue(new KeyValuePair<int, int>(r, c));
                    else if (grid[r][c] == 1) freshOranges++;
                }
            }

            // start - when we reach here, it's a round
            queue.Enqueue(new KeyValuePair<int, int>(-1, -1));

            // Start the Search
            int elapsed = -1;
            int[][] directions = {new int[] {-1, 0}, new int[] {0, 1}, new int[] {1, 0}, new int[] {0, -1}};
            while (queue.Count != 0)
            {
                var pair = queue.Dequeue();
                int row = pair.Key;
                int col = pair.Value;

                if (row == -1)
                {
                    elapsed++;

                    // TO Avoid Endless Loop
                    if (queue.Count != 0)
                        queue.Enqueue(new KeyValuePair<int, int>(-1, -1));
                }
                else
                {
                    foreach (var d in directions)
                    {
                        int neighborRow = row + d[0];
                        int neighborCol = col + d[1];

                        if (neighborRow >= 0 && neighborRow < rows &&
                            neighborCol >= 0 && neighborCol < cols)
                        {
                            if (grid[neighborRow][neighborCol] == 1)
                            {
                                grid[neighborRow][neighborCol] = 2;
                                freshOranges--;
                                queue.Enqueue(new KeyValuePair<int, int>(neighborRow, neighborCol));
                            }
                        }
                    }
                }
            }

            return freshOranges == 0 ? elapsed : -1;
        }

        [Fact]
        public void Test_OrangeRotten()
        {
            var input = new int[][]
            {
                new int[] {2, 1, 1},
                new int[] {1, 1, 0},
                new int[] {0, 1, 1}
            };

            var result = OrangeRotting(input);
            Assert.Equal(4, result);
        }
    }
}