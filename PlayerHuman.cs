using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class PlayerHuman : Player
    {
        public PlayerHuman(string Name)
        {
            _name = Name;
        }

        private ConsoleWindowText decideWindow = new ConsoleWindowText(Utils.leftTextColumnWidth, Utils.leftTextColumnHeigth);

        //protected override int makeDescision(ChoiseContext choise)
        //{
        //    if (choise.OptionsCount == 1)
        //        return 0;

        //    decideWindow.AddLog(choise.Question.Length == 0 ? "Make a descision:" : (choise.Question + ":"));
        //    int index = 0;
        //    foreach (string v in choise.ChoiseOptions)
        //        decideWindow.AddLog(String.Format("  {0} >\t{1}", ++index, v));
        //    int answer = -1;
        //    Console.WriteLine();
        //    do {
        //        try { answer = int.Parse(Console.ReadLine()) - 1; }
        //        catch (Exception e) { Console.Write("Try again: "); }
        //    } while (answer < 0);

        //    decideWindow.AddLog(choise.DescriptionForOption(answer));
        //    Console.ReadLine();
        //    decideWindow.ClearLogWindow();
        //    return answer;
        //}
        protected void SwapColors()
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = tmp;
        }

        protected override int makeDescision(ChoiseContext choise)
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
                decideWindow.AddLogWithCurrentColor(String.Format("  {0} >\t{1}", ++index, v));
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
                    choise.HighlightSelected(answer);
                foreach (int i in needRedraw)
                {
                    if (i == answer) SwapColors();
                    Console.SetCursorPosition(decideWindow.X, decideWindow.Y + i + 1);
                    Console.Write(String.Format("  {0} >\t{1}", i + 1, choise.ChoiseOptions[i]));
                    if (i == answer) SwapColors();
                }
            } while (pressed != ConsoleKey.Enter);

            decideWindow.ClearLogWindow();
            return answer;
        }
    }
}
