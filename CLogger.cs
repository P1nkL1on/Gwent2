using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    public class ConsoleWindowText
    {
        ConsoleColor loggerBackground = ConsoleColor.Black;
        int startX = 0;
        int startY = 0;
        int wid;
        int hei;
        int usedSpace = 0;

        public ConsoleWindowText(int wid, int hei)
        {
            this.wid = wid;
            this.hei = hei;
            usedSpace = 0;
        }

        public void AddOffset(int xOff, int yOff)
        {
            startX += xOff;
            startY += yOff;
        }
        public int Width { get { return wid; } }
        public int Heigth { get { return hei; } }
        public int X { get { return startX; } }
        public int Y { get { return startY; } }
        public void AddLogWithCurrentColor(string message)
        {
            AddLog(message, Console.ForegroundColor, Console.BackgroundColor);
        }
        public void AddLog(string message)
        {
            AddLog(message, ConsoleColor.Gray, loggerBackground);
        }
        public void AddLog(string message, ConsoleColor fore)
        {
            AddLog(message, fore, loggerBackground);
        }
        public void AddLog(string message, ConsoleColor fore, ConsoleColor back)
        {
            if (message.IndexOf('\n') >= 0)
            {
                foreach (string messagePart in message.Split('\n'))
                    AddLog(messagePart, fore);
                return;
            }
            int approximateLength = message.Length / wid + 1;
            if (usedSpace + approximateLength > hei)
                ClearLogWindow(back);

            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
            while (message.Length > wid)
            {
                int separateOn = wid;
                do { separateOn--; } while (separateOn > 0 && message[separateOn] != ' ');
                if (separateOn == 0)
                    separateOn = wid;

                string wr = message.Substring(0, separateOn);
                message = message.Substring(separateOn + ((separateOn < wid)? 1 : 0));
                Console.SetCursorPosition(startX, startY + usedSpace);
                Console.Write(wr);
                usedSpace++;
            }
            Console.SetCursorPosition(startX, startY + usedSpace);
            Console.Write(message);
            usedSpace++;
            Console.ResetColor();
        }
        public void ClearLogWindow()
        {
            ClearLogWindow(ConsoleColor.Black);
        }
        public void ClearLogWindow(ConsoleColor back)
        {
            Console.BackgroundColor = back;
            for (int i = 0; i < hei; ++i)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write("".PadLeft(wid));
            }
            usedSpace = 0;
        }
    }
}
