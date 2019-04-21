using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class DeckBuilder
    {
        ConsoleWindowText deckPreview = new ConsoleWindowText(Utils.fieldStartHorizontal, 45 /*max size of deck + 5*/);
        ConsoleWindowText allCardsPreview = new ConsoleWindowText(Utils.fieldStartHorizontal, 45 /*max size of deck + 5*/);
        ConsoleWindowText logger = new ConsoleWindowText(2 * Utils.fieldStartHorizontal, 10);


        public DeckBuilder()
        {
            allCardsPreview.AddOffset(Utils.fieldStartHorizontal, 0);
            logger.AddOffset(0, 45);
            logger.AddLog("Welcome to deck builder!", ConsoleColor.Cyan);
        }

        public void Edit(Player editor)
        {
            List<Card> allCards = DeckIO.invokeAllCards();
            foreach (Card c in allCards)
                allCardsPreview.AddLog(c.ToString(), UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(c.rarity)));
            Check(new List<Card>());
        }

        void Check(List<Card> deck)
        {
            List<string> warns = new List<string>(), errors = new List<string>();
            DeckIO.checkDeckStandart(deck, ref warns, ref errors);
            foreach (string w in warns)
                logger.AddLog(w, ConsoleColor.DarkYellow);
            foreach (string e in errors)
                logger.AddLog(e, ConsoleColor.Red);
        }
    }
}
