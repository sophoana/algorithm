using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmTest.OOD
{
    //https://leetcode.com/problems/meeting-rooms-ii/
    public class MeetingRoomProblem
    {
        // public int MinMeetingRooms(int[][] intervals)
        // {
        //     var orderByStart = intervals.OrderBy(x => x[0]).ToList();
        //     var num = 0;
        //     var meeting = new Dictionary<int, List<int>>(); // Start - List of Ends
        //     foreach (var room in orderByStart)
        //     {
        //         var start = room[0];
        //         var end = room[1];
        //
        //         if (!meeting.ContainsKey(start))
        //         {
        //             meeting.Add(start, new List<int>{end});
        //             num++;
        //         }
        //     }
        // }
        
        public bool CanAttendMeetings(int[][] intervals) {
            // If he could attend meeting there is not clash in meeting
            // Sort Array by Start time
            var myComparer = new StartTimeComparer();
            Array.Sort(intervals, myComparer);

            for (int i = 0; i < intervals.Length-1; i++)
            {
                if (intervals[i][1] > intervals[i + 1][0])
                    return false;
            }

            return true;
        }

        public class StartTimeComparer : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                if (x != null) return x[0].CompareTo(y[0]);
                return 0;
            }
        }
        
        public int MinMeetingRooms(int[][] intervals)
        {
            if (intervals.Length == 0)
                return 0;

            var start = new int[intervals.Length];
            var end = new int[intervals.Length];
            
            // Fill up the array
            var idx = 0;
            foreach (var i in intervals)
            {
                start[idx] = i[0];
                end[idx] = i[1];
                idx++;
            }
            
            Array.Sort(start);
            Array.Sort(end);

            int startIdx = 0;
            int endIdx = 0;
            int nbRoom = 0;
            while (startIdx < intervals.Length)
            {
                if (start[startIdx] >= end[endIdx])
                {
                    nbRoom -= 1;
                    endIdx++;
                }

                nbRoom++;
                startIdx++;
            }

            return nbRoom;
        }
    }

    public enum Status
    {
        Occupied = 0,
        Freed = 1
    }

    public class Room
    {
        public Status Status { get; set; }
    }

    public class MeetingRoom : Room
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
}