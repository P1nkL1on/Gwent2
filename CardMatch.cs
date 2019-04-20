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
        public Player currentPlayer { get { return players[_currentPlayerIndex]; } }
        public List<Player> players = new List<Player>();
        public List<Card> cards = new List<Card>();
        private ConsoleWindowText topLeftTextBox;

        public void Log(string message)
        {
            topLeftTextBox.AddLog(message, ConsoleColor.Magenta);
        }
        public void Log(Card source, string message)
        {
            Log(String.Format("{0} : {1}", source.ToString(), message));
        }
        public void AddCardToGame(Card card) { 
            Log(card, "added to game"); 
            cards.Add(card);
            if (card._show == null)
                cards[0]._show._global.addContainerFor(card);
            
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

            topLeftTextBox = new ConsoleWindowText(Utils.leftTextColumnWidth, Utils.fieldHeigth - Utils.leftTextColumnHeigth);
            topLeftTextBox.AddOffset(0, Utils.leftTextColumnHeigth);
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
        public void _drawCard(Player player, Card exactCard)
        {
            if (exactCard != null)
                exactCard.move(Place.hand);
        }

        public List<Unit> _allUnitsInRow(int row, Player p)
        {
            return Select.Units(cards, Filter.anyUnitInBattlefield(), Filter.anyUnitHostBy(p), Filter.anyUnitInRow(row));
        }
        public void _setUnitToPositionInRow(Card newUnit, int wantPlace)
        {
            List<int> allRowIndices = _getIndicesOfUnits(Filter.anyAllyUnitInBattlefield(newUnit), Filter.anyUnitInRow((newUnit as Unit).row));
            if (allRowIndices.Count == 0)
                return;

            int newUnitIndex = cards.IndexOf(newUnit);
            if (allRowIndices.Count == 1)
            {
                int neighIndex = allRowIndices[0];
                if ((wantPlace == 0 && newUnitIndex > neighIndex)
                    || (wantPlace == 1 && newUnitIndex < neighIndex))
                {
                    Card tmp = newUnit;
                    cards[newUnitIndex] = cards[neighIndex];
                    cards[neighIndex] = tmp;
                }
                // else there is cool by this way
                return;
            }
            int newUnitInList = -1;
            for (int i = 0; i < allRowIndices.Count - 1; ++i)
                if (allRowIndices[i] <= newUnitIndex && allRowIndices[i + 1] >= newUnitIndex)
                { allRowIndices.Insert(i + 1, newUnitIndex); newUnitInList = i + 1;  break; }
            if (newUnitInList == -1)
            {
                if (newUnitIndex < allRowIndices[0]) { newUnitInList = 0; allRowIndices.Insert(0, newUnitIndex); }
                if (newUnitIndex > allRowIndices.Last()) { newUnitInList = allRowIndices.Count; allRowIndices.Add(newUnitIndex); }
            }
            // now we have an array like 1 4 6 [7] 10 11, where [7] is a position of choosen unit and he wants from ind3 to <wantPlace>
            // if wantPlace = 1, then -->1 [7] 4 6 10 11
            // and only theese cards should be moved out    4 6 [7]
            //                                              [7] 4 6
            // or even [5] 8 9 --> 8 9 [5]

            if (newUnitInList == wantPlace)
                return; //luckly this unit is on place he wants

            bool moveLeft = wantPlace < newUnitInList;
            if (moveLeft)
                for (int i = newUnitInList; i > wantPlace; i--)
                    cards[allRowIndices[i]] = cards[allRowIndices[i - 1]];
            else
                for (int i = newUnitInList; i < wantPlace; ++i)
                    cards[allRowIndices[i]] = cards[allRowIndices[i + 1]];
            
            cards[allRowIndices[wantPlace]] = newUnit;
        }
        public Unit _unitToTheLeft(Unit self)
        {
            var us = _allUnitsInRow(self.row, self.host);
            int index = us.IndexOf(self);
            if (index == -1)
                return null;    // critical error
            if (index == 0)
                return null;
            return us[index - 1];
        }
        public Unit _unitToTheRight(Unit self)
        {
            var us = _allUnitsInRow(self.row, self.host);
            int index = us.IndexOf(self);
            if (index > us.Count)
                return null;    // critical error
            if (index == us.Count - 1)
                return null;
            return us[index + 1];
        }
        public List<Unit> _adjastedUnits(Unit self, int nCountForEachSide)
        {
            var us = _allUnitsInRow(self.row, self.host);
            int index = us.IndexOf(self);
            int from = Math.Max(0, index - nCountForEachSide);
            int to = Math.Min(us.Count - 1, index + nCountForEachSide);
            List<Unit> res = new List<Unit>();
            for (int i = from; i < to; ++i)
                if (us[i] != self)
                    res.Add(us[i]);
            return res;
        }
        public Card _topCardOfDeck(Player player)
        {
            var deck = _deckOf(player);
            return deck.Count == 0 ? null : deck.First();
        }
        public List<Card> _topCardsOfDeck(Player player, int nCardCount)
        {
            var deck = _deckOf(player);
            if (deck.Count == 0)
                return new List<Card>();
            return deck.GetRange(0, Math.Min(nCardCount, deck.Count));
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
        List<Card> _handWithLeaderOf(Player player)
        {
            var res = Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.hand, player));
            var allPossibleLeadersToPlay = Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.leader, player));
            foreach (Card c in allPossibleLeadersToPlay)
                res.Add(c);
            return res;
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
        List<int> _getIndicesOfUnits(params UnitPredicat[] filters)
        {
            List<int> res = new List<int>();
            for (int ind = 0; ind < cards.Count; ++ind)
            {
                bool accepted = true;
                foreach (UnitPredicat f in filters)
                    if (cards[ind] as Unit != null && !f(cards[ind] as Unit))
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
        public void _clearAllRowEffects()
        {
            rowEffects.Clear();
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
        public int _scoreAtPlayersRow(Player p, int row)
        {
            int summ = 0;
            foreach (Unit u in Select.Units(cards, Filter.anyUnitInBattlefield(), Filter.anyUnitHostBy(p), Filter.anyUnitInRow(row)))
                summ += u.power;
            return summ;
        }
        public int _scoreOf(Player p)
        {
            int summ = 0;
            for (int i = 0; i < 3; ++i)
                summ += _scoreAtPlayersRow(p, i);
            return summ;
        }
        public RowEffect _rowEffectOn(int row, Player p)
        {
            foreach (RowEffect r in rowEffects)
                if (r.row == row && r.PlayerUnderEffect == p)
                    return r;
            return null;
        }
        void _makeLeadersVisibleToAll()
        {
            foreach (Card c in cards)
                if (c as Leader != null)
                    (c as Leader).makeVisibleAll();
        }

        public List<Player> currentlyWinning
        {
            get
            {
                List<int> scores = new List<int>();
                foreach (Player p in players)
                    scores.Add(_scoreOf(p));
                int winScore = scores.Max();
                List<int> winners = new List<int>();
                for (int i = 0; i < scores.Count; ++i)
                    if (scores[i] == winScore)
                        winners.Add(i);
                List<Player> winPlayers = new List<Player>();
                foreach (int winnerId in winners)
                    winPlayers.Add(players[winnerId]);
                return winPlayers;
            }
        }

        public bool everyPlayerPassed
        {
            get
            {
                foreach (Player p in players)
                    if (!p.passed)
                        return false;
                return true;
            }
        }

        public void Start()
        {
            foreach (Player p in players)
                _shuffleDeckOf(p);

            do
            {
                StartRound(++_round);

                while (!Turn()) ;

            } while (selectWinners().Count == 0);
        }

        //returns winners if game ends
        List<Player> selectWinners()
        {
            foreach (Player p in currentlyWinning)
            {
                Log(String.Format("{0} wins the round!", p.ToString()));
                p.roundsWin++;
            }
            List<Player> gameWinners = new List<Player>();
            foreach (Player p in players)
                if (p.roundsWin == 2)
                    gameWinners.Add(p);
            return gameWinners;
        }

        public void StartRound(int roundIndex)
        {
            // remove all units from field
            _clearAllRowEffects();
            foreach (Unit u in Select.Units(cards, Filter.anyUnitInBattlefield()))
                u.move(Place.graveyard);
            foreach (Player player in players)
            {
                player.passed = false;
                switch (roundIndex)
                {
                    case 1: _makeLeadersVisibleToAll(); _drawCard(player, 10); _mulliganOnRoundStart(player, 3); continue;
                    case 2: _drawCard(player, 2); _mulliganOnRoundStart(player, 2); continue;
                    case 3: _drawCard(player, 1); _mulliganOnRoundStart(player, 1); continue;
                    default: continue;
                }
            }
        }
        public bool Turn()
        {
            // if all playes passed then finish round
            if (everyPlayerPassed)
                return true;
            Console.SetCursorPosition(0, 0); Console.Write("Press any key..."); Console.ReadKey();

            topLeftTextBox.ClearLogWindow();
            // activate all turn start-triggers
            foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayer(currentPlayer)))
                c._onTurnStart(c);
            foreach (RowEffect r in rowEffects)
                if (r.PlayerUnderEffect == currentPlayer)
                    r.onTurnStart();
            // actions performed only if player has not passed yet
            if (!currentPlayer.passed)
            {
                // draw current state for human player
                //if (currentPlayer as PlayerHuman != null) { State(); Console.ReadLine(); State(); }
                Card selected = currentPlayer.selectCardOrNone(_handWithLeaderOf(currentPlayer), "Select a card to play in this turn or pass", "Pass");
                if (selected != null)
                {
                    //topLeftTextBox.AddLog("\n\n" + selected.ToFormat(), ConsoleColor.Cyan);
                    // current player plays a selected card
                    currentPlayer.playCard(selected);
                }
                else
                {
                    Log(String.Format("{0} passed!", currentPlayer.ToString()));
                    currentPlayer.passed = true;
                }
            }

            // activate all turn end-triigers
            foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayer(currentPlayer)))
                c._onTurnEnd(c);

            //if (currentPlayer as PlayerHuman != null) State();
            //

            _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
            return false;
        }

        //void State() { State(false); }
        //void State(bool showOnlyBattlefield)
        //{
        //    Console.SetCursorPosition(0, 0);
        //    //fieldState.Clear();
        //    int column = Utils.leftTextColumnWidth;
        //    foreach (Player p in players)
        //    {
        //        // set color
        //        Console.BackgroundColor = (p as PlayerHuman != null) ? ConsoleColor.Blue : ConsoleColor.DarkRed;
        //        // set vertical offset
        //        int line = Utils.fieldStartVerticalOffset;

        //        Console.SetCursorPosition(column, line++);
        //        ConsoleWrite(String.Format("{1}{0}",
        //            p.ToString(),
        //            String.Format("{0} {2} {1}", _scoreOf(p), (p.passed ? "PASSED" : ""), ("".PadLeft(p.roundsWin, '*')))
        //            .PadRight(Utils.fieldPerPlayerHorizontal / 2 - 3)),
        //            Utils.fieldPerPlayerHorizontal);
        //        line += SkipLines(1, Utils.fieldPerPlayerHorizontal);

        //        // battlefiedld all rows
        //        for (int row = 0; row < 3; ++row)
        //        {
        //            RowEffect re = null;
        //            foreach (RowEffect r in rowEffects) if (r.PlayerUnderEffect == p && r.row == row) re = r;

        //            Console.SetCursorPosition(column, line++);
        //            ConsoleWrite(String.Format("{2}{0} {1}", Utils.allRows[row], (re != null ? ("  " + re.ToString()) : ""), (_scoreAtPlayersRow(p, row) + "").PadRight(4)),
        //                Utils.fieldPerPlayerHorizontal);
        //            foreach (Unit u in Select.Cards(cards, Filter.anyCardHostByPlayerIn(Place.battlefield, p)))
        //                if (u.row == row)
        //                {
        //                    Console.SetCursorPosition(column, line++);
        //                    ConsoleWrite(String.Format("      {0}", u.Show(currentPlayer)), Utils.fieldPerPlayerHorizontal);
        //                }
        //        }
        //        if (showOnlyBattlefield)
        //            continue;

        //        // 5 line space between bf and other
        //        line += SkipLines(5, Utils.fieldPerPlayerHorizontal);
        //        // all non battlefield places
        //        foreach (Place place in Utils.allPlaces)
        //        {
        //            if (place == Place.battlefield)
        //                continue;
        //            line += SkipLines(1, Utils.fieldPerPlayerHorizontal);
        //            Console.SetCursorPosition(column, line++);
        //            ConsoleWrite(String.Format("  {0}'s {1}: ", p.ToString(), place.ToString()), Utils.fieldPerPlayerHorizontal);

        //            int nInvisibleCards = 0;
        //            foreach (Card c in Select.Cards(cards, Filter.anyCardHostByPlayerIn(place, p)))
        //            {
        //                Console.SetCursorPosition(column, line);
        //                string cardShow = c.Show(currentPlayer);
        //                bool isVisible = cardShow[0] != '?';
        //                if (!isVisible)
        //                {
        //                    nInvisibleCards++;
        //                    continue;
        //                }
        //                ConsoleWrite(String.Format("   {0}", c.Show(currentPlayer)), Utils.fieldPerPlayerHorizontal);
        //                line++;
        //            }
        //            if (nInvisibleCards > 0)
        //            {
        //                ConsoleWrite(String.Format("   {0} x{1}", Card.InvisibleCardString, nInvisibleCards), Utils.fieldPerPlayerHorizontal);
        //                line++;
        //            }
        //        }
        //        // add spacing to last
        //        line += SkipLines(Utils.fieldHeigth - line - 1, Utils.fieldPerPlayerHorizontal);
        //        column += Utils.fieldPerPlayerHorizontal;
        //    }
        //    Console.ResetColor();
        //    Console.SetCursorPosition(0, 0);
        //    //lastFieldState.Clear();
        //    //foreach (string s in fieldState)
        //    //    lastFieldState.Add(s);
        //}
        //void ClearText(int upToX)
        //{
        //    Console.SetCursorPosition(0, 0);
        //    Console.Write("".PadLeft(upToX));
        //    SkipLines(70, upToX);
        //    Console.SetCursorPosition(0, 1);
        //}

        //void ConsoleWriteLine(string message, ConsoleColor fore)
        //{
        //    Console.ForegroundColor = fore;
        //    Console.WriteLine(message);
        //    Console.ResetColor();
        //}
        //void ConsoleWrite(string message, int padMax, ConsoleColor fore)
        //{
        //    Console.ForegroundColor = fore;
        //    ConsoleWrite(message, padMax);
        //    Console.ResetColor();
        //}
        //void ConsoleWrite(String message, int padMax)
        //{
        //    string messagePadded = message.PadRight(padMax);
        //    //fieldState.Add(messagePadded);
        //    //bool reColor = (lastFieldState.IndexOf(messagePadded) < 0);
        //    //if (!reColor)
        //    //{
        //    Console.Write(messagePadded);
        //    //    return;
        //    //}

        //    //findAnyDiff(messagePadded);
        //}
        ////List<string> lastFieldState = new List<string>();
        ////List<string> fieldState = new List<string>();

        //int SkipLines(int nCount, int padMax)
        //{
        //    int x = Console.CursorLeft - padMax;
        //    int y = Console.CursorTop + 1;
        //    for (int i = 0; i <= nCount; ++i)
        //    {
        //        Console.SetCursorPosition(x, y + i);
        //        ConsoleWrite("", padMax);
        //    }
        //    return nCount;
        //}

        // RED MARKING

        //List<int> compareStrings(string a, string b, out int diff)
        //{
        //    List<int> res = new List<int>();
        //    bool isOk = true, wasOk = true;
        //    int resDiff = 0;
        //    for (int i = 0; i < Math.Min(a.Length, b.Length); ++i)
        //    {
        //        wasOk = isOk;
        //        isOk = (a[i] == b[i]);
        //        if (!isOk) resDiff++;
        //        if (isOk != wasOk)
        //            res.Add(i + (isOk? 1 : 0));
        //    }
        //    if (res.Count % 2 == 1)
        //        res.Add(Math.Min(a.Length, b.Length) - 1);
        //    diff = resDiff;
        //    return res;
        //}
        //bool findAnyDiff(string newString)
        //{
        //    int index = fieldState.Count, lastFrom = Math.Max(0, index - 5), lastTo = Math.Min(lastFieldState.Count - 1, index + 5);
        //    int bestComprehence = 100;
        //    List<int> bestMarkUp = new List<int>(){0, newString.Length};
        //    string bestRemember = "";
        //    for (int i = lastFrom; i < lastTo; ++i)
        //    {
        //        int comprehance = 0;
        //        List<int> markup = compareStrings(newString, lastFieldState[i], out comprehance);
        //        if (comprehance > bestComprehence)
        //            continue;
        //        bestComprehence = comprehance;
        //        bestMarkUp = markup;
        //        bestRemember = lastFieldState[i];
        //    }
        //    string[] parts = new string[bestMarkUp.Count + 1];
        //    for (int i = bestMarkUp.Count - 1; i >= 0; i--)
        //    {
        //        if (bestMarkUp[i] < 0) bestMarkUp[i] = 0;
        //        parts[i + 1] = newString.Substring(bestMarkUp[i]);
        //        newString = newString.Substring(0, bestMarkUp[i]);
        //    }
        //    parts[0] = newString;
        //    ConsoleColor defColor = Console.BackgroundColor;
        //    ConsoleColor mark = ConsoleColor.Red;
        //    bool isMarkingText = true;
        //    foreach (string part in parts)
        //    {
        //        isMarkingText = !isMarkingText;
        //        Console.BackgroundColor = isMarkingText ? mark : defColor;
        //        //Console.BackgroundColor = mark;
        //        Console.Write(part);
        //    }
        //    Console.BackgroundColor = defColor;

        //    if (bestRemember.Trim().Length > 0)
        //    {
        //        int rememberWidth = 20;
        //        int wasCaret = Console.CursorLeft;
        //        Console.SetCursorPosition(wasCaret - rememberWidth, Console.CursorTop);
        //        if (bestRemember.Trim().Length == 0)
        //            Console.Write("appeared");
        //        else
        //            Console.Write("was " + bestRemember.Substring(bestMarkUp[0], bestMarkUp[1] - bestMarkUp[0]));
        //        Console.SetCursorPosition(wasCaret, Console.CursorTop);
        //    }
        //    return bestMarkUp.Count > 0;
        //}
    }
}
