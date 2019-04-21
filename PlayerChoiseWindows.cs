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
                    // if can find a card to preview, then use a funcyion
                    // PreviewCard (below)
                    // else just ask for a casual desciption and write it
                    if (!choise.PreviewSelected(answer, descriptionWindow))
                    {
                        descriptionWindow.ClearLogWindow();
                        descriptionWindow.AddLog(choise.DescriptionForOption(answer));
                    }
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
        //
        static string borderSymbols = "▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒";
        //
        public static void PreviewCard(Card card, ConsoleWindowText window)
        {
            window.ClearLogWindow();
            if (card == null)
                return;
            var cardLines = card.ToFormat().Split('\n');
            var clanColor = UtilsDrawing.please.getClosest(UtilsDrawing.colorsOfClan(card.clan));
            int wid = window.Width;
            string border = borderSymbols.Substring(0, wid), borderCap = borderSymbols.Substring(0, (wid - cardLines[0].Length) / 2 - 1);
            bool needOneExtra = (wid - cardLines[0].Length) % 2 == 1;

            ConsoleColor b = (ConsoleColor)clanColor._back, f = (ConsoleColor)clanColor._fore,
                fR = UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(card.rarity));
            bool isDescrip = false;
            for (int i = -1; i < cardLines.Length; ++i)
                if (i < 0 || i == 1)window.AddLog(border, f, b);    //borders
                else if (i == 0) window.AddLog(String.Format("{1} {0} {1}{2}", cardLines[i], borderCap, needOneExtra ? ("" + borderCap[0]) : ""), fR, b);//name
                else {
                    if (cardLines[i].Length > 0 && cardLines[i][0] == '-') isDescrip = true;
                    window.AddLog(cardLines[i], isDescrip? ConsoleColor.DarkGray : ConsoleColor.Gray); 
                }           //parameters
        }
    }
}
