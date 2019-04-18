using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Gwent2
{
    class CardRedrawContainer
    {
        public Card _source;
        ConsoleColor _rarityColor;
        public FieldDrawer _parent;
        bool _isUnit;
        bool _needUpdate;
        Point _position;

        public CardRedrawContainer(Card source)
        {
            _source = source;
            _rarityColor = UtilsDrawing.please.getClosestFore(UtilsDrawing.colorOfRarity(source.rarity));
            _isUnit = _source as Unit != null;
            _needUpdate = true;
            _position = new Point(0, 0);

            source._show = this;
        }
        public void setPosition(Point newPosition)
        {
            _needUpdate = (newPosition != _position);
            if (_needUpdate)

            _position = newPosition;
        }
        public void setUpdate()
        {
            _needUpdate = true;
        }
    }
    class FieldDrawer
    {
        Match _context;

        List<CardRedrawContainer> _containers = new List<CardRedrawContainer>();

        List<Player> _mustBeUpdateHost = new List<Player>();
        List<Place> _mustBeUpdates = new List<Place>();

        public void addUpdateIssue(Player host, Place place)
        {
            _mustBeUpdateHost.Add(host);
            _mustBeUpdates.Add(place);
        }

        public FieldDrawer(Match forGame)
        {
            _context = forGame;
            foreach (Card c in forGame.cards)
            {
                CardRedrawContainer crc = new CardRedrawContainer(c);
                crc._parent = this;
                _containers.Add(crc);
            }
        }

        void reArrangePlaces()
        {

        }

        void accquireRepaint()
        {
            for (int i = 0; i < _mustBeUpdateHost.Count; ++i)
            {
                Player who = _mustBeUpdateHost[i];
                Place where = _mustBeUpdates[i];

                foreach (Card u in Select.Cards(_context.cards, Filter.anyCardHostByPlayerIn(where, who)))
                    u._show.setUpdate();
            }
        }
    }
}
