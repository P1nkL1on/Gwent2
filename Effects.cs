using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Drawing;

namespace Gwent2
{
    class Effects
    {
        //static string frames = " ░▒▓█";
        static string frames = " .o@";
        //static string frames = " .,oO0";

        public static void Trajectory(Point from, Point to, ConsoleColor fore, int speedTravel, int tailSpeedTravel, int tailOffset, int timeForFrame)
        {
            Console.CursorVisible = false;
            List<Point> path = new List<Point>();

            Point currentPoint = from;
            do
            {
                path.Add(currentPoint);
                if (to.X < currentPoint.X) currentPoint.X--;
                if (to.X > currentPoint.X) currentPoint.X++;
                if (to.Y < currentPoint.Y) currentPoint.Y--;
                if (to.Y > currentPoint.Y) currentPoint.Y++;
            }
            while (currentPoint.X != to.X || currentPoint.Y != to.Y);

            int timePerSegment = timeForFrame;//Math.Max(1, overallTime / (path.Count));

            int nowHead = 0, nowTail = -tailOffset;

            Console.ForegroundColor = fore;

            for (; nowTail < path.Count; )
            {
                nowHead += speedTravel;
                nowTail += tailSpeedTravel;
                int length = nowHead - nowTail;
                // make i =0 to let a - - - - -  - path
                for (int i = -tailSpeedTravel + 1; i < length; ++i)
                {
                    int pathIndex = nowTail + i;
                    if (pathIndex >= path.Count || pathIndex < 0)
                        continue;

                    char symbol = (i <= 0) ? frames[0] : frames[Math.Min((int)(i * 1.0 / (length -1) * frames.Length + 1), frames.Length - 1)];
                    
                    Console.SetCursorPosition(path[pathIndex].X, path[pathIndex].Y);
                    Console.Write(symbol);
                }
                Thread.Sleep(timePerSegment);
            }
        }
    }
}
