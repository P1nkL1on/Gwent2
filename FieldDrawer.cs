using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Gwent2
{
    enum UpdateType
    {
        none,
        valueChanged,
        placeChanged
    }
    class CardRedrawContainer
    {
        public static int redrawSpeed = 100;
        // callback
        public Card _source;
        public FieldDrawer _global;

        // inner funcs
        ConsoleColor _rarityColor;
        bool _isUnit;
        public bool _isSelected = false;

        // pos
        Point _position;
        // type of update required
        UpdateType upd = UpdateType.placeChanged;
        public UpdateType requiredUpdate { get { return upd; } }
        public bool _doNotCleanLine = false;

        public CardRedrawContainer(Card source)
        {
            _source = source;
            _rarityColor = UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(source.rarity));
            _isUnit = _source as Unit != null;
            _position = new Point(0, 0);
            // provide a connection to source card
            source._show = this;
        }
        public override string ToString()
        {
            return String.Format("{0} {1}", upd.ToString(), _source);
        }
        public void setPosition(Point newPosition)
        {
            if (_position == newPosition)
                return;
            if (!_doNotCleanLine)
                _global.clearLine(_source.context.players.IndexOf(_source.host), _position.Y);
            _position = newPosition;
            upd = UpdateType.placeChanged;
        }
        public Point position { get { return _position;} }
        public void callAutoDraw(Player watcher)
        {
            if (!_doNotCleanLine)
                _global.clearLine(_source.context.players.IndexOf(_source.host), _position.Y);

            _global.setPosition(_position);
            if (_isSelected) _global.swapColor(ConsoleColor.Black);
            string shortName = _source.Show(watcher);
            if (shortName == Card.InvisibleCardString)
            {
                Console.Write(shortName);
                return;
            }
            // color a rarity only in case of card has been revealed
            _global.swapFontColor(_rarityColor);
            if (_isUnit)
            {
                int firstSpace = shortName.IndexOf(' ');
                Console.Write("[");
                _global.swapFontColor(UtilsDrawing.colorsOfPower(_source as Unit));
                Console.Write(shortName.Substring(1, firstSpace - 1));
                _global.popColor();
                Console.Write(shortName.Substring(firstSpace));
            }
            else
                Console.Write(shortName);
            
            _global.popColor();
            if (_isSelected) _global.popColor();
        }
        public void clearUpdate()
        {
            upd = UpdateType.none;
        }
        public void redrawCausedChangeValue()
        {
            if (upd == UpdateType.none)
                upd = UpdateType.valueChanged;
            callGlobalRedraw();
            _global.updateAllRowWeatherAndValues();
        }
        public void redrawCausedMove()
        {
            if (upd != UpdateType.placeChanged)
                upd = UpdateType.placeChanged;
            _global.setAllCardPositions();
            callGlobalRedraw();
        }
        public void redrawSelectedInstant()
        {
            if (upd == UpdateType.none)
                upd = UpdateType.valueChanged;
            callGlobalRedraw(false);
        }
        
        void callGlobalRedraw(bool sleep = true)
        {
            _global.redraw();
            if (sleep)Thread.Sleep(redrawSpeed);
        }
    }
    class FieldDrawer
    {
        Match _context;
        List<CardRedrawContainer> _containers = new List<CardRedrawContainer>();

        List<ConsoleColor> _playerColors = new List<ConsoleColor>() { 
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkGray
        };
        List<ConsoleColor> _lightPlayerColors = new List<ConsoleColor>() { 
            ConsoleColor.Blue,
            ConsoleColor.Red,
            ConsoleColor.Magenta,
            ConsoleColor.Green,
            ConsoleColor.Gray
        };
        Stack<ConsoleColor> _colorStackFore = new Stack<ConsoleColor>();
        Stack<ConsoleColor> _colorStackBack = new Stack<ConsoleColor>();

        void saveColor()
        {
            _colorStackFore.Push(Console.ForegroundColor);
            _colorStackBack.Push(Console.BackgroundColor);
        }
        public void popColor()
        {
            Console.ForegroundColor = _colorStackFore.Pop();
            Console.BackgroundColor = _colorStackBack.Pop();
        }
        public void swapColor(ConsoleColor back, ConsoleColor fore)
        {
            saveColor();
            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
        }
        public void swapColor(ConsoleColor back)
        {
            saveColor();
            Console.BackgroundColor = back;
        }
        public void swapFontColor(ConsoleColor fore)
        {
            saveColor();
            Console.ForegroundColor = fore;
        }

        private Player _watcher = null;
        public FieldDrawer(Match forGame, Player watcher)
        {
            _watcher = watcher;
            _context = forGame;

            for (int i = 0; i < _context.players.Count; ++i)
                clearLine(i);

            foreach (Card c in _context.cards)
                addContainerFor(c);
        }

        public void addContainerFor(Card c)
        {
            CardRedrawContainer crc = new CardRedrawContainer(c);
            crc._global = this;
            _containers.Add(crc);
        }

        public void clearLine(int playerIndex, int lineIndex, int upToLineIndex)
        {
            int X = Utils.fieldStartHorizontal + playerIndex * Utils.fieldPerPlayerHorizontal;
            int Y = Utils.fieldStartVerticalOffset;
            swapColor(_playerColors[playerIndex]);
            for (int i = lineIndex; i <= upToLineIndex; ++i)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write(("").PadLeft(Utils.fieldPerPlayerHorizontal));
            }
            popColor();
        }
        public void clearLine(int playerIndex, int lineIndex)
        {
            clearLine(playerIndex, lineIndex, lineIndex);
        }
        public void clearLine(int playerIndex)
        {
            clearLine(playerIndex, Utils.fieldStartVerticalOffset, Utils.fieldHeigth);
        }
        public void setLineFromContainer(int X, int Y)
        {
            Console.SetCursorPosition(X, Y + Utils.fieldStartVerticalOffset);
        }
        public void setPosition(Point xy)
        {
            setLineFromContainer(xy.X, xy.Y);
        }
        public void setAllCardPositions()
        {
            setAllCardPositions(_watcher);
        }

        List<List<int>> allRowLines = new List<List<int>>();
        public void updateAllRowWeatherAndValues()
        {
            for (int i = 0; i < allRowLines.Count; ++i)
            {
                swapColor(_lightPlayerColors[i], ConsoleColor.White);
                Player p = _context.players[i];
                int playerCardAllignLeft = Utils.fieldStartHorizontal + i * Utils.fieldPerPlayerHorizontal;
                List<int> rowLines = allRowLines[i];
                for (int j = 0; j < 3; ++j)
                {
                    RowEffect re = _context._rowEffectOn(j, p);

                    clearLine(i, rowLines[j] - 1, rowLines[j]);
                    Console.SetCursorPosition(playerCardAllignLeft, rowLines[j] + Utils.fieldStartVerticalOffset);
                    Console.Write(String.Format("{2}{0}{1}", Utils.allRows[j], (re != null ? ("  " + re.ToString()) : ""), (_context._scoreAtPlayersRow(p, j) + "").PadRight(4)));
                   
                }
                Console.SetCursorPosition(playerCardAllignLeft, Utils.fieldStartVerticalOffset);
                Console.Write(String.Format("{1}{0}",
                    p.ToString(),
                    String.Format("{0} {2} {1}", _context._scoreOf(p), (p.passed ? "PASSED" : ""),
                    ("".PadLeft(p.roundsWin, '*'))).PadRight(Utils.fieldPerPlayerHorizontal / 2 - 3)),
                    Utils.fieldPerPlayerHorizontal);
                popColor();
            }
        }
        public void setAllCardPositions(Player watcher)
        {
            allRowLines.Clear();
            int cardAllignOffset = 4;
            for (int i = 0; i < _context.players.Count; ++i)
            {
                Player p = _context.players[i];
                int playerCardAllignLeft = Utils.fieldStartHorizontal + i * Utils.fieldPerPlayerHorizontal;

                int currentLine = Utils.fieldStartVerticalOffset;

                List<int> rowLines = new List<int>();
                for (int row = 0; row < 3; ++row)
                {
                    rowLines.Add(currentLine + 1);
                    currentLine += 2;
                    foreach (Card u in Select.Cards(_context.cards, Filter.anyCardHostByPlayerIn(Place.battlefield, p)))
                        if (u as Unit != null && (u as Unit).row == row)
                            u._show.setPosition(new Point(playerCardAllignLeft + cardAllignOffset + 2, currentLine++));

                }
                allRowLines.Add(rowLines);

                // rest == non-battlefield
                List<int> linesForPlaces = new List<int>();
                List<int> cardsInPlaces = new List<int>();
                foreach (Place place in Utils.allPlaces)
                    if (place != Place.battlefield)
                    {
                        // skip 1 line each separation
                        linesForPlaces.Add(currentLine + 1);
                        currentLine += 2;
                        int cardsInPlace = 0;
                        bool lastWasInvisible = false;
                        foreach (Card c in Select.Cards(_context.cards, Filter.anyCardHostByPlayerIn(place, p)))
                        {
                            if (lastWasInvisible && !c.isVisibleTo(watcher)) currentLine--;
                            c._show.setPosition(new Point(playerCardAllignLeft + cardAllignOffset, currentLine));

                            currentLine++;
                            cardsInPlace++;
                            lastWasInvisible = !c.isVisibleTo(watcher);
                        }
                        cardsInPlaces.Add(cardsInPlace);
                        //if (cardsInPlace == 0 && place != Place.deck)
                        //    linesForPlaces[linesForPlaces.Count - 1] = -1;
                    }
                // indexes changes to ignore battlefield
                for (int j = 0; j < linesForPlaces.Count; ++j)
                {
                    clearLine(i, linesForPlaces[j] - 1, linesForPlaces[j]);
                    Console.SetCursorPosition(playerCardAllignLeft, linesForPlaces[j] + Utils.fieldStartVerticalOffset);
                    swapColor(_lightPlayerColors[i], ConsoleColor.White);
                    if (linesForPlaces[j] < 0) continue;
                    if (cardsInPlaces[j] > 0)
                        Console.Write(String.Format("{0}'s {1}:{2}",
                        p, Utils.allPlaces[j + 1],
                        String.Format("  <{0}>", cardsInPlaces[j])));
                    popColor();
                }
            }
            updateAllRowWeatherAndValues();
        }
        public void redraw()
        {
            redraw(_watcher);
        }
        public void redraw(Player watcher)
        {
            int pInd = 0;
            foreach (Player p in _context.players)
            {
                swapColor(_playerColors[pInd++]);
                //bool allBellowIsMoved = false;
                foreach (CardRedrawContainer r in _containers)
                    if (r._source.host == p)
                    {
                        if (r.requiredUpdate != UpdateType.none)
                        {
                            r.callAutoDraw(watcher);
                            r.clearUpdate();
                        }
                    }
                popColor();
            }
        }
    }
}
