using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Drawing;

namespace Gwent2
{
    enum showAction
    {
        damage,
        boost,
        stretngthlen,
        weaken,
        armor
    }

    class Effects
    {
        //static string frames = " ░▒▓█";
        static string frames = " .oO";

        public static void Trajectory(Card source, Card self, showAction type)
        {
            if (type == showAction.damage) Trajectory(pointOf(source), pointOf(self), ConsoleColor.Red, 5, 5, 20, 15);
            if (type == showAction.armor) Trajectory(pointOf(source), pointOf(self), ConsoleColor.Yellow, 4, 2, 35, 18);
            if (type == showAction.boost) Trajectory(pointOf(source), pointOf(self), ConsoleColor.Green, 4, 2, 15, 22);
            if (type == showAction.stretngthlen) Trajectory(pointOf(source), pointOf(self), ConsoleColor.White, 4, 2, 5, 30);
            if (type == showAction.weaken) Trajectory(pointOf(source), pointOf(self), ConsoleColor.Magenta, 4, 2, 15, 30);
        }
        static Point pointOf(Card c)
        {
            if (c._show.position.X < Utils.fieldPerPlayerHorizontal + Utils.fieldStartHorizontal)
                return new Point(c._show.position.X + c.ToString().Length, c._show.position.Y);
            else
                return new Point(c._show.position.X - 1, c._show.position.Y);
        }
        
        static void Trajectory(Point from, Point to, ConsoleColor fore, int speedTravel, int tailSpeedTravel, int tailOffset, int timeForFrame)
        {
            if (from == to)
                return;
            int border = Utils.fieldStartHorizontal + Utils.fieldPerPlayerHorizontal - 10;
            List<int> availableTopIndices = new List<int>() {  border - 2, border -1};
            Console.CursorVisible = false;
            List<Point> path = new List<Point>();

            Point currentPoint = from;
            int findTop = (from.X < availableTopIndices.First() && to.X < availableTopIndices.First())? 1 
                : (from.X > availableTopIndices.Last() && to.X > availableTopIndices.Last())? -1 : 0;
            do
            {
                path.Add(currentPoint);
                int _x = currentPoint.X, _y = currentPoint.Y;
                bool canGoTopDown = availableTopIndices.IndexOf(_x) >= 0;
                if (to.Y != _y && canGoTopDown)
                {
                    if (to.Y > _y) { _y++; }
                    if (to.Y < _y) { _y--; }
                    findTop = 0;
                }
                else
                {
                    if (findTop == 0)
                    {
                        if (to.X > _x) { _x++; }
                        if (to.X < _x) { _x--; }
                        if (to.X == _x) {
                            if (_x < availableTopIndices.Last()) findTop = 1;
                            else if (_x > availableTopIndices.First()) findTop = -1;
                        }
                    }
                    else
                        _x += findTop;
                }
                currentPoint = new Point(_x, _y);
                //Console.WriteLine(_x + "/" + _y);
            } while (currentPoint.X != to.X || currentPoint.Y != to.Y);

            int timePerSegment = timeForFrame;//Math.Max(1, overallTime / (path.Count));

            int nowHead = 0, nowTail = -tailOffset;

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
                    
                    // kostil coloring background
                    Console.BackgroundColor = (path[pathIndex].X < Utils.fieldStartHorizontal + Utils.fieldPerPlayerHorizontal) ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;

                    Console.SetCursorPosition(path[pathIndex].X, path[pathIndex].Y + Utils.fieldStartVerticalOffset);
                    Console.Write(symbol);
                }
                Thread.Sleep(timePerSegment);
            }
        }
    }
}
