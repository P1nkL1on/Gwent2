using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Card
    {
        public Place place;
        protected String _name;
        protected Clan _clan;
        protected Rarity _rarity;
        protected Player _baseHost;
        protected Player _host;
        public String name { get { return _name; } }
        public Clan clan { get { return _clan; } }
        public Rarity rarity { get { return _rarity; } }
        public Player host { get { return _host; } }
        public virtual int power { get { return 0; } }
        public Match context { get { return _context; } }

        protected Match _context;

        public virtual void move(Place to) {
            Place previousPlace = place;
            place = to;
            triggerMove(previousPlace); 
        }

        protected virtual void triggerMove(Place from)
        {
            Place to = place;
            if (to == Place.hand) _onDrawn(this, from);
            if (to == Place.battlefield) _onDeploy(this, from);
            if (to == Place.discard && from == Place.battlefield) _onDestroy(this, from);
            if (to == Place.discard && from == Place.hand) _onDiscard(this, from);
            if (to == Place.banish) _onBanish(this, from);
            if (to == Place.deck && from == Place.hand) _onSwap(this, from);
            if (to == Place.deck && from != Place.hand) _onShuffled(this, from);
            if (to == Place.battlefield && from == Place.battlefield) _onMove(this, from);
        }

        TriggerMove _onMove = (s, f) => { s._context.Log(s, "moved"); };
        TriggerMove _onDeploy = (s, f) => { s._context.Log(s, "deployed"); };
        TriggerMove _onDrawn = (s, f) => { s._context.Log(s, "drawned"); };
        TriggerMove _onDiscard = (s, f) => { s._context.Log(s, "discarded"); };
        TriggerMove _onDestroy = (s, f) => { s._context.Log(s, "destroyed"); };
        TriggerMove _onBanish = (s, f) => { s._context.Log(s, "banished"); };
        TriggerMove _onSwap = (s, f) => { s._context.Log(s, "swapped"); };
        TriggerMove _onShuffled = (s, f) => { s._context.Log(s, "shuffled"); };

        public void setOnMove(TriggerMove trigger) { _onMove = trigger; }
        public void setOnDeploy(TriggerMove trigger) { _onDeploy = trigger; }
        public void setOnDrawn(TriggerMove trigger) { _onDrawn = trigger; }
        public void setOnDiscard(TriggerMove trigger) { _onDiscard = trigger; }
        public void setOnDestroy(TriggerMove trigger) { _onDestroy = trigger; }
        public void setOnBanish(TriggerMove trigger) { _onBanish = trigger; }
        public void setOnSwap(TriggerMove trigger) { _onSwap = trigger; }
        public void setOnShuffled(TriggerMove trigger) { _onShuffled = trigger; }

        public void setAttributes(Match Context, Player H, Clan C, Rarity R, String N, Place P)
        {
            _context = Context;
            _baseHost = H;
            _host = H;
            _clan = C;
            _rarity = R;
            _name = N;
            place = P;
        }
        public void setAttributes(Clan C, Rarity R, String Name)
        {
            _context = null;
            _baseHost = null;
            _host = null;
            _clan = C;
            _rarity = R;
            _name = Name;
            place = Place.deck;
        }
        public void setAttributesToken(Clan C, Rarity R, String Name)
        {
            _context = null;
            _baseHost = null;
            _host = null;
            _clan = C;
            _rarity = R;
            _name = Name;
            place = Place.token;
        }

        public override string ToString()
        {
            return name;
        }
        public virtual string ToStringFull()
        {
            return String.Format("{0} ({1} {2} owned by {3}) in {4}'s {5}",
                name.ToUpper(), rarity.ToString(), clan.ToString(), _baseHost.ToString(), _host.ToString(), place.ToString().ToUpper());
        }
        public virtual void SetDefaultHost(Player Host, Match Context)
        {
            _baseHost = Host;
            _host = Host;
            _context = Context;
        }
    }

    delegate void TriggerMove(Card self, Place from);
    delegate void TriggerRecieve(Unit self, Card source, int X);
    delegate void TriggerUnitAction(Unit self, Unit source, int X);

}
