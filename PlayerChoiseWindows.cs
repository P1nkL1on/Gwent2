using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class PlayerChoiseDialog
    {
        public static PreviewType previewType = PreviewType.inGame;

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
        public static int deckCreatingDialog(
            List<Card> deckCards, List<Card> collectionCads,
            ConsoleWindowText deckWindow, ConsoleWindowText collectionWindow,
            ConsoleWindowText descriptionWindow)
        {
            CardChoiseContext choiseCollection =
                CardChoiseContext.WithNoneOption(collectionCads, "COLLECTION", "Finish deck building");
            CardChoiseContext choiseDeck =
                CardChoiseContext.WithNoneOption(deckCards, "DECK", "");

            // turning a deck into Card and Count

            Console.CursorVisible = false;

            PreviewType wasPreviewType = previewType;
            previewType = PreviewType.inCollection;

            collectionWindow.ClearLogWindow();
            collectionWindow.AddLog((choiseCollection.Question.Length == 0 ? "Make a descision:" : (choiseCollection.Question + ":")).PadRight(collectionWindow.Width), ConsoleColor.Yellow, ConsoleColor.DarkGreen);
            int collectionSelected = 0, collectionFromIndex = 0;

            List<int> redrawCollection = new List<int>(); for (int i = 0; i < Math.Min(choiseCollection.OptionsCount, collectionWindow.Heigth); ++i) redrawCollection.Add(i);
            RedrawScrollCollection(redrawCollection, choiseCollection.ChoiseOptions, 0, 0, collectionWindow);

            ScrollChooser(ref collectionSelected, ref collectionFromIndex, choiseCollection, collectionWindow, descriptionWindow, ConsoleKey.Tab, ConsoleKey.LeftArrow,
                RedrawScrollCollection,
                () =>{
                    deckCards.Add(collectionCads[collectionSelected - 1].spawnCard());
                });

            choiseCollection.HighlightSelected(-1);
            collectionWindow.ClearLogWindow();

            previewType = wasPreviewType;
            return collectionSelected;
        }

        static void ScrollChooser(
            ref int answer, ref int intFrom,
            CardChoiseContext choise,
            ConsoleWindowText collectionWindow,
            ConsoleWindowText descriptionWindow,
            ConsoleKey exit,
            ConsoleKey use,
            UpdateCallBack redraw,
            CallBack onUseItem)
        {
            ConsoleKey pressed = ConsoleKey.NoName;
            do
            {
                pressed = Console.ReadKey().Key;
                // . . . . navigating in list
                List<int> needRedraw = new List<int>();
                if (pressed == ConsoleKey.DownArrow || pressed == ConsoleKey.UpArrow)
                {

                    int offset = pressed == ConsoleKey.DownArrow ? 1 : -1;

                    answer += offset;
                    if (answer < 0) answer = choise.OptionsCount - 1;
                    if (answer > choise.OptionsCount - 1) answer = 0;

                    if (answer < intFrom) intFrom = answer;
                    if (answer > intFrom + collectionWindow.Heigth - 1) intFrom = answer - collectionWindow.Heigth + 1;

                    for (int i = 0; i < Math.Min(collectionWindow.Heigth, choise.OptionsCount); ++i)
                        needRedraw.Add(intFrom + i);
                }
                if (needRedraw.Count > 0)
                {
                    choise.HighlightSelected(answer);
                    if (!choise.PreviewSelected(answer, descriptionWindow))
                    {
                        descriptionWindow.ClearLogWindow();
                        descriptionWindow.AddLog(choise.DescriptionForOption(answer));
                    }
                }
                redraw(needRedraw, choise.ChoiseOptions, answer, intFrom, collectionWindow);
                //
                if (pressed == use && answer != null)
                    onUseItem();
            } while (pressed != exit);
        }
        //choise.ChoiseOptions
        static void RedrawScrollCollection(List<int> needRedraw, List<string> choiseOptions, int answer, int intFrom, ConsoleWindowText collectionWindow)
        {
            foreach (int i in needRedraw)
            {
                if (i == answer) SwapColors();
                Console.SetCursorPosition(collectionWindow.X, collectionWindow.Y + i + 1 - intFrom);
                Console.Write((choiseOptions[i]).PadRight(collectionWindow.Width));
                if (i == answer) SwapColors();
            }
        }


        static string borderSymbols = "▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒▓▒";
        public static void PreviewCard(Card card, ConsoleWindowText window)
        {
            window.ClearLogWindow();
            if (card == null)
                return;
            var cardLines = (previewType == PreviewType.inGame ? card.ToFormat() : card.ToFormatCollection()).Split('\n');
            var clanColor = UtilsDrawing.please.getClosest(UtilsDrawing.colorsOfClan(card.clan));
            int wid = window.Width;
            string border = borderSymbols.Substring(0, wid), borderCap = borderSymbols.Substring(0, (wid - cardLines[0].Length) / 2 - 1);
            bool needOneExtra = (wid - cardLines[0].Length) % 2 == 1;

            ConsoleColor b = (ConsoleColor)clanColor._back, f = (ConsoleColor)clanColor._fore,
                fR = UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(card.rarity));
            bool isDescrip = false;
            for (int i = -1; i < cardLines.Length; ++i)
                if (i < 0 || i == 1) window.AddLog(border, f, b);    //borders
                else if (i == 0) window.AddLog(String.Format("{1} {0} {1}{2}", cardLines[i], borderCap, needOneExtra ? ("" + borderCap[0]) : ""), fR, b);//name
                else
                {
                    if (cardLines[i].Length > 0 && cardLines[i][0] == '_') isDescrip = true;
                    window.AddLog(cardLines[i], isDescrip ? ConsoleColor.DarkRed : ConsoleColor.Gray);
                }           //parameters
        }
    }
    enum PreviewType
    {
        inGame,
        inCollection
    }
    delegate void CallBack();
    delegate void UpdateCallBack(List<int> a, List<string> b, int c, int d, ConsoleWindowText e);
}
