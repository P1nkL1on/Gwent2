using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Match
    {
        int _currentPlayerIndex = 0;
        int _round = 0;
        Player currentPlayer { get { return players[_currentPlayerIndex]; } }
        public List<Player> players = new List<Player>();
        public List<Card> cards = new List<Card>();

        public void Log(string message)
        {
            ConsoleWriteLine(message, ConsoleColor.Magenta);
        }
        public void Log(Card source, string message)
        {
            Log(String.Format("{0} : {1}", source.ToString(), message));
        }
        public void AddCardToGame(Card card) { Log(card, "added to game"); cards.Add(card); }

        public Match(List<Player> participants, List<List<Card>> decks)
        {
            for (int i = 0; i < participants.Count; ++i)
            {
                foreach (Card c in decks[i])
                    c.SetDefaultHost(participants[i], this);
                cards.AddRange(decks[i]);
            }
            players = participants;

            Start();
        }

        void _mulliganOnRoundStart(Player player, int nMuliganCount)
        {
            var hand = _handOf(player);
            if (hand.Count == 0)    // nothing to mulligan
                return;

            for (int muliganUsed = 0; muliganUsed < nMuliganCount; ++muliganUsed)
            {
                Card choose = player.selectCardOrNone(_handOf(player), String.Format("Select card for mulligan [{0}/{1}]", muliganUsed, nMuliganCount));
                if (choose == null)
                    return; // stop, when selected no more cards
                // if selected, then mulligan it
                _mulliganCard(player, choose);
            }
        }
        public void _mulliganCard(Player player, Card card)
        {
            if (card.place != Place.hand)
                return;
            card.move(Place.deck);
            _shuffleDeckOf(player);
            _drawCard(player, 1);
        }
        public void _drawCard(Player player, int nCount)
        {
            for (int draw = 0; draw < nCount; ++draw)
            {
                Card topdeck = _topCardOfDeck(player);
                if (topdeck == null)
                    return;
                topdeck.move(Place.hand);
            }
        }

        public Card _topCardOfDeck(Player player)
        {
            var deck = _deckOf(player);
            return deck.Count == 0 ? null : deck.First();
        }
        public Unit _topUnitOfDeck(Player player, params UnitPredicat[] filters)
        {
            var anyAccepted = Select.Units(_deckOf(player), filters);
            return anyAccepted.Count == 0 ? null : anyAccepted.First();
        }
        public void _shuffleDeckOf(Player player)
        {
            _shuffleCards(_getIndicesOfCards(Filter.anyCardHostByPlayerIn(Place.deck, player)));
        }

        List<Card> _deckOf(Player player)
        {
            return Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.deck, player));
        }
        List<Card> _handOf(Player player)
        {
            return Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.hand, player));
        }

        Random shuffleRandomiser = new Random(DateTime.Now.Millisecond);
        void _shuffleCards(List<int> indices)
        {
            List<Card> listToShuffle = new List<Card>();
            List<Card> listShuffled = new List<Card>();
            for (int i = 0; i < indices.Count; ++i)
                listToShuffle.Add(cards[indices[i]]);
            for (int i = 0; i < indices.Count; ++i)
            {
                int passed = shuffleRandomiser.Next(listToShuffle.Count);
                listShuffled.Add(listToShuffle[passed]);
                listToShuffle.RemoveAt(passed);
            }
            for (int i = 0; i < indices.Count; ++i)
                cards[indices[i]] = listShuffled[i];
        }
        List<int> _getIndicesOfCards(params CardPredicat[] filters)
        {
            List<int> res = new List<int>();
            for (int ind = 0; ind < cards.Count; ++ind)
            {
                bool accepted = true;
                foreach (CardPredicat f in filters)
                    if (!f(cards[ind]))
                    {
                        accepted = false;
                        break;
                    }
                if (accepted)
                    res.Add(ind);
            }
            return res;
        }
        List<RowEffect> rowEffects = new List<RowEffect>();
        public void _addRowEffect(RowEffect rowEffect)
        {
            RowEffect conflict = null;
            foreach (RowEffect r in rowEffects)
                if (r.isConflictWith(rowEffect))
                    conflict = r;
            if (conflict != null)
                rowEffects.Remove(conflict);
            rowEffects.Add(rowEffect);
        }
        public void _removeRowEffect(Player player, int row, params CardPredicat[] filters)
        {
            RowEffect conflict = null;
            foreach (RowEffect r in rowEffects)
                if (r.row == row && r.PlayerUnderEffect == player)
                {
                    bool accepted = true;
                    foreach (CardPredicat f in filters)
                        if (!f(r.Source))
                            accepted = false;
                    if (accepted)
                        conflict = r;
                }
            if (conflict != null)
                rowEffects.Remove(conflict);
        }
        public void _removeAllNegativeRowEffectsFromPlayer(Player player)
        {
            for (int i = 0; i < rowEffects.Count; ++i)
                if (rowEffects[i].Source.hasTag(Tag.hazzard) && rowEffects[i].PlayerUnderEffect == player)
                {
                    rowEffects.RemoveAt(i);
                    i--;
                }
        }

        public void Start()
        {
            foreach (Player p in players)
                _shuffleDeckOf(p);

            Round(++_round);

            for (; ; )
                Turn();
        }

        public void Round(int roundIndex)
        {
            foreach (Player player in players)
                switch (roundIndex)
                {
                    case 1: _drawCard(player, 10); _mulliganOnRoundStart(player, 3); continue;
                    case 2: _drawCard(player, 2); _mulliganOnRoundStart(player, 2); continue;
                    case 3: _drawCard(player, 1); _mulliganOnRoundStart(player, 1); continue;
                    default: continue;
                }
        }

        public void Turn()
        {
            Console.Clear();
            // activate all turn start-triggers
            foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayer(currentPlayer)))
                c._onTurnStart(c);
            foreach (RowEffect r in rowEffects)
                if (r.PlayerUnderEffect == currentPlayer)
                    r.onTurnStart();
            // draw current state for human player
            if (currentPlayer as PlayerHuman != null) State();


            Card selected = currentPlayer.selectCard(_handOf(currentPlayer), "Select a card to play in this turn");
            ConsoleWriteLine("\n\n" + selected.ToFormat(), ConsoleColor.Cyan);

            // current player plays a selected card
            currentPlayer.playCard(selected);

            // activate all turn end-triigers
            foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayer(currentPlayer)))
                c._onTurnEnd(c);

            Console.WriteLine("Enter to continue...");
            Console.ReadLine();

            _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        }

        void State() { State(false); }
        void State(bool showOnlyBattlefield)
        {
            const int battlefieldStartTop = 5;
            const int handStartLeft = 60;
            const int horizontalSpaceForPlayer = 60;

            Console.WriteLine("\nCurrent game state:");
            int column = handStartLeft;
            foreach (Player p in players)
            {
                int line = battlefieldStartTop;
                // battlefiedld all rows
                for (int row = 0; row < 3; ++row)
                {
                    RowEffect re = null;
                    foreach (RowEffect r in rowEffects) if (r.PlayerUnderEffect == p && r.row == row) re = r;

                    Console.SetCursorPosition(column, line++);
                    Console.Write(Utils.allRows[row] + ":" + (re != null ? ("  " + re.ToString()) : ""));
                    foreach (Unit u in Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.battlefield, p)))
                        if (u.row == row)
                        {
                            Console.SetCursorPosition(column, line++);
                            Console.Write(String.Format("   {0}", u.Show(currentPlayer)));
                        }
                }
                if (showOnlyBattlefield)
                    continue;
                line += 5;
                // all non battlefield places
                foreach (Place place in Utils.allPlaces)
                {
                    if (place == Place.battlefield)
                        continue;
                    line++;
                    Console.SetCursorPosition(column, line++);
                    Console.Write(String.Format("  {0}'s {1}: ", p.ToString(), place.ToString()));

                    int nInvisibleCards = 0;
                    foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayerIn(place, p)))
                    {
                        Console.SetCursorPosition(column, line);
                        string cardShow = c.Show(currentPlayer);
                        bool isVisible = cardShow[0] != '?';
                        if (!isVisible)
                        {
                            nInvisibleCards++;
                            continue;
                        }
                        Console.Write(String.Format("   {0}", c.Show(currentPlayer)));
                        line++;
                    }
                    if (nInvisibleCards > 0)
                    {
                        Console.Write(String.Format("   {0} x{1}", Card.InvisibleCardString, nInvisibleCards));
                        line++;
                    }
                }
                column += horizontalSpaceForPlayer;
            }
            Console.SetCursorPosition(0, 0);
        }

        void ConsoleWriteLine(string message, ConsoleColor fore)
        {
            Console.ForegroundColor = fore;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
