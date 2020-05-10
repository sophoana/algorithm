using System;
using System.Collections;
using System.Collections.Generic;
using Xunit.Sdk;

namespace AlgorithmTest.TreeGraph
{
    public class WallAndGateQuestion
    {
        private static int EMPTY = int.MaxValue;
        private static int GATE = 0;
        private static int WALL = 1;

        private static List<int[]> DIRECRION = new List<int[]>
        {
            new[] {1, 0}, new[] {0, 1}, new[] {0, -1}, new[] {-1, 0}
        };

        public void WallAndGates(int[][] rooms)
        {
            int row = rooms.Length;
            if (row == 0) return;

            int col = rooms[0].Length;
            Queue<int[]> queue = new Queue<int[]>();

            // Add Gate into the queue
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (rooms[r][c] == GATE)
                        queue.Enqueue(new[] {r, c});
                }
            }

            // Queue and Check for Distance to Gate
            while (queue.Count != 0)
            {
                int[] point = queue.Dequeue();
                int r = point[0];
                int c = point[1];
                foreach (var direction in DIRECRION)
                {
                    int currRow = r + direction[0];
                    int currCol = c + direction[1];
                    if (currRow < 0 || currCol < 0 ||
                        currCol <= col || currRow <= row ||
                        rooms[currRow][currCol] != EMPTY)
                        continue;

                    rooms[currRow][currCol] = rooms[r][c] + 1;
                    queue.Enqueue(new[] {currRow, currCol});
                }
            }
        }

        public void BruteForce(int[][] rooms)
        {
            if (rooms.Length == 0) return;
            for (int row = 0; row < rooms.Length; row++)
            {
                for (int col = 0; col < rooms[0].Length; col++)
                {
                    if (rooms[row][col] == EMPTY)
                        rooms[row][col] = GetDistanceToNearestGate(rooms, row, col);
                }
            }
        }

        private int GetDistanceToNearestGate(int[][] rooms, int startRow, int startCol)
        {
            int m = rooms.Length;
            int n = rooms[0].Length;
            int[][] distance = new int[m][];
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] {startRow, startCol});

            while (queue.Count != 0)
            {
                int[] point = queue.Dequeue();
                int row = point[0];
                int col = point[1];
                foreach (var d in DIRECRION)
                {
                    int r = row + d[0];
                    int c = col + d[1];

                    // within range
                    if (r < 0 || c < 0 || r >= m || c >= n || rooms[r][c] == WALL || distance[r][c] != 0)
                        continue;

                    distance[r][c] = distance[row][col] + 1;

                    if (rooms[r][c] == GATE)
                        return distance[r][c];

                    queue.Enqueue(new[] {r, c});
                }
            }

            return int.MaxValue;
        }
    }
}