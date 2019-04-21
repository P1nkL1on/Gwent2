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
        ConsoleWindowText previewContext = new ConsoleWindowText(30, 60);


        public DeckBuilder()
        {
            allCardsPreview.AddOffset(Utils.fieldStartHorizontal, 0);
            logger.AddOffset(0, 45);
            previewContext.AddOffset(Utils.fieldStartHorizontal * 2, 0);
            logger.AddLog("Welcome to deck builder!", ConsoleColor.Cyan);
        }

        public void Edit(Player editor)
        {
            List<Card> allCards = DeckIO.invokeAllCards();
            //foreach (Card c in allCards)
            //    allCardsPreview.AddLog(c.ToString(), UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(c.rarity)));
            Check(new List<Card>());
            PlayerChoiseDialog.ScrollDialog(CardChoiseContext.WithNoneOption(allCards, "Cards in library", "Finish deck building"), allCardsPreview, previewContext); 
        }

        bool Check(List<Card> deck)
        {
            List<string> warns = new List<string>(), errors = new List<string>();
            DeckIO.checkDeckStandart(deck, ref warns, ref errors);
            //ShowMessages(warns, errors);
            return errors.Count == 0;
        }

        void ShowMessages(List<string> warns, List<string> errors)
        {
            foreach (string w in warns)
                logger.AddLog("Warning: " + w, ConsoleColor.DarkYellow);
            foreach (string e in errors)
                logger.AddLog("Error:   " + e, ConsoleColor.Red);
        }

        public Deck Load(string fileName)
        {
            List<string> warns = new List<string>(), errors = new List<string>();
            Deck loaded = DeckIO.readDeckFromFile(fileName, ref warns, ref errors);
            logger.AddLog(String.Format("Loaded deck \"{0}\".", fileName));
            ShowMessages(warns, errors);
            return Check(loaded.cards) ? loaded : null;
        }
    }
}
