using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class PlayerChoiseDialog
    {
        protected static void SwapColors()
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = tmp;
        }
        
        public static int classicSmallDialog(ChoiseContext choise, ConsoleWindowText decideWindow, ConsoleWindowText descriptionWindow)
        {
            Console.CursorVisible = false;
            if (choise.OptionsCount == 1)
                return 0;

            decideWindow.ClearLogWindow();
            decideWindow.AddLog(choise.Question.Length == 0 ? "Make a descision:" : (choise.Question + ":"));
            int answer = 0, index = 0, lineIndex = 0;
            foreach (string v in choise.ChoiseOptions)
            {
                if (lineIndex == answer) SwapColors();
                decideWindow.AddLogWithCurrentColor(String.Format("    {1}", ++index, v));
                ++lineIndex;
            }
            ConsoleKey pressed = ConsoleKey.NoName;
            do
            {
                pressed = Console.ReadKey().Key;
                List<int> needRedraw = new List<int>();
                if (pressed == ConsoleKey.DownArrow)
                {
                    needRedraw.Add(answer);
                    answer = (answer + 1) % choise.OptionsCount;
                    needRedraw.Add(answer);
                }
                if (pressed == ConsoleKey.UpArrow)
                {
                    needRedraw.Add(answer);
                    answer = (answer == 0 ? choise.OptionsCount : answer) - 1;
                    needRedraw.Add(answer);
                }
                if (needRedraw.Count > 0)
                {
                    choise.HighlightSelected(answer);
                    descriptionWindow.ClearLogWindow();
                    descriptionWindow.AddLog(choise.DescriptionForOption(answer), ConsoleColor.Cyan);
                }
                foreach (int i in needRedraw)
                {
                    if (i == answer) SwapColors();
                    Console.SetCursorPosition(decideWindow.X, decideWindow.Y + i + 1);
                    Console.Write(String.Format("    {1}", i + 1, choise.ChoiseOptions[i]));
                    if (i == answer) SwapColors();
                }
            } while (pressed != ConsoleKey.Enter);

            choise.HighlightSelected(-1);
            decideWindow.ClearLogWindow();
            return answer;
        }
    }
}
