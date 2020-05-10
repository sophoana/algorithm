using System.Collections.Concurrent;

namespace AlgorithmTest.AmazonLeetCodeQuestion
{
    public class HitCounter
    {
        // 300 seconds - 5 minutes
        private int[] _times = new int[300];
        private int[] _hits = new int[300];

        public HitCounter()
        {
            
        }

        public void Hit(int timestamp)
        {
            int index = timestamp % 300;
            if (_times[index] != timestamp)
            {
                _times[index] = timestamp;
                _hits[index] = 1;
            }
            else
            {
                _hits[index]++;
            }
        }

        public int GetHits(int timestamp)
        {
            int total = 0;
            for (int i = 0; i < 300; i++)
            {
                if (timestamp - _times[i] < 300)
                {
                    total += _hits[i];
                }
            }

            return total;
        }
    }
}