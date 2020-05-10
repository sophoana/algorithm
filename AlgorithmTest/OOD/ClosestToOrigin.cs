using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmTest.OOD
{
    public class ClosestToOrigin
    {
        //https://leetcode.com/problems/k-closest-points-to-origin/
        
        public int[][] KClosest(int[][] points, int K) {
            // Build List of Points
            
            var myPoints = new List<Point>();
            foreach (var point in points)
            {
                myPoints.Add(new Point(point[0], point[1]));
            }

            var sorted = myPoints.OrderBy(x => x.Distance)
                .Select(x => new int[] {x.X, x.Y})
                .Take(K)
                .ToArray();

            return sorted;
        }
    }

    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public double Distance { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            CalculateDistance();
        }

        private void CalculateDistance()
        {
            Distance = Math.Sqrt((X * X) + (Y * Y));
        }
    }
}