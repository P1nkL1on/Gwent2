﻿using System;
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
        public Timer timer = new Timer();
        public String name { get { return _name; }}
        public Clan clan { get { return _clan; } }
        public Rarity rarity { get { return _rarity; } }
        public Player host { get { return _host; } }
        public Player baseHost { get { return _baseHost; } }
        public virtual int power { get { return 0; } }
        public Match context { get { return _context; } }
        protected List<Tag> _tags;
        public bool hasTag(Tag tag) { return _tags.IndexOf(tag) >= 0; }
        protected List<Player> _visibleTo = new List<Player>();
        protected void makeVisibleTo(Player watcher) { if (_visibleTo.IndexOf(watcher) < 0)_visibleTo.Add(watcher); }
        protected void makeVisibleAll() { _visibleTo.Clear();  foreach (Player p in context.players) _visibleTo.Add(p); }

        protected Match _context;

        public virtual void move(Place to) {
            // banishing doomed cards
            if (to == Place.graveyard && hasTag(Tag.doomed))
                to = Place.banish;

            Place previousPlace = place;
            place = to;
            // make cards removing from deck visible to host/all
            setVisible(previousPlace);
            // triggers
            triggerMove(previousPlace); 
        }

        protected virtual void triggerMove(Place from)
        {
            Place to = place;
            if (to == Place.hand) _onDrawn(this, from);
            if (to == Place.battlefield) _onDeploy(this, from);
            if (to == Place.graveyard && (from == Place.hand || from == Place.deck)) _onDiscard(this, from);
            if (to == Place.banish) _onBanish(this, from);
            if (to == Place.deck && from == Place.hand) _onSwap(this, from);
            if (to == Place.deck && from != Place.hand) _onShuffled(this, from);
            if (to == Place.battlefield && from == Place.battlefield) _onMove(this, from);
        }

        protected virtual void setVisible(Place from)
        {
            Place to = place;
            if (to == Place.deck) { _visibleTo.Clear(); return; }
            if (to == Place.hand) { makeVisibleTo(host); return;}
            if (to != Place.banish) makeVisibleAll();
        }

        public TriggerTurn _onTurnStart = (s) => { /*s._context.Log(s, "starts a new turn");*/ };
        public TriggerTurn _onTurnEnd = (s) => { /*s._context.Log(s, "ends a turn");*/ };

        protected TriggerMove _onMove = (s, f) => { s._context.Log(s, "moved"); };
        TriggerMove _onDeploy = (s, f) => { s._context.Log(s, "deployed"); };
        TriggerMove _onDrawn = (s, f) => { s._context.Log(s, "drawed"); };
        TriggerMove _onDiscard = (s, f) => { s._context.Log(s, "discarded"); };
        TriggerMove _onBanish = (s, f) => { s._context.Log(s, "banished"); };
        TriggerMove _onSwap = (s, f) => { s._context.Log(s, "swapped"); };
        TriggerMove _onShuffled = (s, f) => { s._context.Log(s, "shuffled"); };

        string _onMoveAbility = "";
        string _onDeployAbility = "";
        string _onDrawnAbility = "";
        string _onDiscardAbility = "";
        string _onBanishAbility = "";
        string _onSwapAbility = "";
        string _onShuffleAbility = "";
        string _onTurnStartAbility = "";
        string _onTurnEndAbility = "";

        public void setOnMove(TriggerMove trigger, string description) { _onMove = trigger; _onMoveAbility = description; }
        public void setOnDeploy(TriggerMove trigger, string description) { _onDeploy = trigger; _onDeployAbility = description; }
        public void setOnDrawn(TriggerMove trigger, string description) { _onDrawn = trigger; _onDrawnAbility = description; }
        public void setOnDiscard(TriggerMove trigger, string description) { _onDiscard = trigger; _onDiscardAbility = description; }
        public void setOnBanish(TriggerMove trigger, string description) { _onBanish = trigger; _onBanishAbility = description; }
        public void setOnSwap(TriggerMove trigger, string description) { _onSwap = trigger; _onSwapAbility = description; }
        public void setOnShuffled(TriggerMove trigger, string description) { _onShuffled = trigger; _onShuffleAbility = description; }

        public void setOnTurnStart(TriggerTurn trigger, string description) { _onTurnStart = trigger; _onTurnStartAbility = description; }
        public void setOnTurnEnd(TriggerTurn trigger, string description) { _onTurnEnd = trigger; _onTurnEndAbility = description; }

        public virtual string ToFormatAbilities()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                _onDeployAbility.Length == 0? "" : (_onDeployAbility+"\n"),
                _onDiscardAbility.Length == 0 ? "" : (_onDiscardAbility + "\n"),
                _onBanishAbility.Length == 0 ? "" : (_onBanishAbility + "\n"),
                _onDrawnAbility.Length == 0 ? "" : (_onDrawnAbility + "\n"),
                _onShuffleAbility.Length == 0 ? "" : (_onShuffleAbility + "\n"),
                _onMoveAbility.Length == 0 ? "" : (_onMoveAbility + "\n"),
                _onSwapAbility.Length == 0 ? "" : (_onSwapAbility + "\n"),
                _onTurnStartAbility.Length == 0 ? "" : (_onTurnStartAbility + "\n"),
                _onTurnEndAbility.Length == 0 ? "" : (_onTurnEndAbility + "\n"));
        }

        public virtual string ToFormat()
        {
            return name;
        }

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
        public static string InvisibleCardString { get { return "".PadLeft(10, '?'); } }
        public virtual string Show(Player watcher)
        {
            return _visibleTo.IndexOf(watcher) >= 0 ? ToString() : InvisibleCardString;
        }
        public virtual string QestionString()
        {
            return String.Format("Select target for {0}", name);
        }
        protected string tagsToString()
        {
            if (_tags.Count == 0)
                return "no tags";
            string tags = "";
            foreach (Tag t in _tags)
                tags += t.ToString() + ", ";
            return tags.Substring(0, tags.Length - 2);
        }
    }

    delegate void TriggerTurnRowEffect(RowEffect self);
    delegate void TriggerTurn(Card self);
    delegate void TriggerMove(Card self, Place from);
    delegate void TriggerRecieve(Unit self, Card source, int X);
    delegate void TriggerUnitAction(Unit self, Unit source, int X);
    delegate void TriggerUnitSelf(Unit self, Card source);

}
