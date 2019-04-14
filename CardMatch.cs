using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Match
    {
        public List<Player> players = new List<Player>();
        public List<Card> cards = new List<Card>();

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
        public void Log(Card source, string message)
        {
            Console.WriteLine(String.Format("{0} : {1}", source.ToString(), message));
        }

        public Match(List<Player> participants, List<List<Card>> decks)
        {
            for (int i = 0; i < participants.Count; ++i)
            {
                foreach (Card c in decks[i])
                    c.SetDefaultHost(participants[i], this);
                cards.AddRange(decks[i]);
            }
            players = participants;

            Test(0);
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public void Test(int playerIndex)
        {
            Console.Clear();
            Player currentPlayer = players[playerIndex];
            if (currentPlayer as PlayerHuman != null) State();

            var inDeck = Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.deck, currentPlayer));

            if (inDeck.Count == 0)
                return;
            currentPlayer.playCard(currentPlayer.selectCard(inDeck));
            Console.WriteLine("\n\nEnter to continue...");
            Console.ReadLine();
            Test((playerIndex + 1) % players.Count);
        }

        public void State()
        {
            Console.WriteLine("\nCurrent game state:");
            int column = 60;
            foreach (Player p in players)
            {
                int line = 5;
                foreach (Place place in Utils.allPossiblePlaces)
                {
                    line++;
                    Console.SetCursorPosition(column, line++);
                    Console.WriteLine(String.Format("  {0}'s {1}: ", p.ToString(), place.ToString()));

                    foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayerIn(place, p)))
                    {
                        Console.SetCursorPosition(column, line++);
                        //Console.BackgroundColor = players.IndexOf(p) == 0 ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
                        Console.WriteLine(String.Format("   {0}", c.ToString()));
                    }
                    //Console.ResetColor();
                }
                column += 40;
            }
            Console.SetCursorPosition(0, 5);
        }
    }
}
