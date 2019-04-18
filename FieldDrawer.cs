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
        // callback
        public Card _source;
        public FieldDrawer _global;

        // inner funcs
        ConsoleColor _rarityColor;
        bool _isUnit;

        // pos
        Point _position;
        // type of update required
        UpdateType upd = UpdateType.placeChanged;
        public UpdateType requiredUpdate { get { return upd; } }

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
            if (_position != newPosition)
                _global.clearLine(_source.context.players.IndexOf(_source.host), _position.Y);
            _position = newPosition;
            upd = UpdateType.placeChanged;
        }
        public void callAutoDraw(Player watcher)
        {
            _global.clearLine(_source.context.players.IndexOf(_source.host), _position.Y);
            _global.setPosition(_position);
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
        }
        public void redrawCausedMove()
        {
            if (upd != UpdateType.placeChanged)
                upd = UpdateType.placeChanged;
            _global.setAllCardPositions();
            callGlobalRedraw();
        }
        void callGlobalRedraw()
        {
            _global.redraw();
            Thread.Sleep(400);
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
        public void swapColor(ConsoleColor fore, ConsoleColor back)
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

            _playerColors.Reverse();
            for (int i = 0; i < _context.players.Count; ++i)
                clearLine(i);

            _playerColors.Reverse();

            int pos = 0;
            foreach (Card c in _context.cards)
            {
                CardRedrawContainer crc = new CardRedrawContainer(c);
                crc._global = this;
                _containers.Add(crc);
            }

            //setAllCardPositions();
            //redraw();
        }

        public void clearLine(int playerIndex, int lineIndex, int upToLineIndex)
        {
            int X = Utils.fieldStartHorizontal + playerIndex * Utils.fieldPerPlayerHorizontal;
            int Y = Utils.fieldStartVerticalOffset;
            swapColor(_playerColors[playerIndex]);
            for (int i = lineIndex; i <= upToLineIndex; ++i)
            {
                Console.SetCursorPosition(X, Y + i);
                Console.Write((i + "").PadLeft(Utils.fieldPerPlayerHorizontal));
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
        public void setAllCardPositions(Player watcher)
        {
            int cardAllignOffset = 4;
            for (int i = 0; i < _context.players.Count; ++i)
            {
                Player p = _context.players[i];
                int playerCardAllignLeft = Utils.fieldStartHorizontal + i * Utils.fieldPerPlayerHorizontal;

                int currentLine = Utils.fieldStartVerticalOffset;



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
                    }
                // indexes changes to ignore battlefield
                for (int j = 0; j < Utils.allPlaces.Count - 1; ++j)
                {
                    clearLine(i, linesForPlaces[j] - 1, linesForPlaces[j]);
                    Console.SetCursorPosition(playerCardAllignLeft, linesForPlaces[j] + Utils.fieldStartVerticalOffset);
                    Console.Write(String.Format("{0}'s {1}:{2}", 
                        p, Utils.allPlaces[j + 1], 
                        cardsInPlaces[j] > 0 ? String.Format("  <{0}>", cardsInPlaces[j]) : ""));
                }
            }
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
