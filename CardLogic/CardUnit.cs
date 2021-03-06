﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

namespace Gwent2
{
    class Unit : Card
    {
        public UnitStatus status = new UnitStatus();

        int _defaultPower;
        int _basePower;
        int _power;
        bool _isSpy = false;
        public int row = -1;

        TriggerRecieve _onDamaged = (s, by, X) => { s._context.Log(s, String.Format("damaged for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onWeakened = (s, by, X) => { s._context.Log(s, String.Format("weakened for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onBoosted = (s, by, X) => { s._context.Log(s, String.Format("boosted for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onStrengthled = (s, by, X) => { s._context.Log(s, String.Format("strenghled for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onHealed = (s, by, X) => { s._context.Log(s, String.Format("healed for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onArmorGain = (s, by, X) => { s._context.Log(s, String.Format("gain {1} armor from {0}", by.ToString(), X)); };

        TriggerUnitAction _onUnitDamaged = (s, by, X) => { /*s._context.Log(s, String.Format("watchs how {0} suffers {1} damage", by.ToString(), X));*/ };
        public TriggerUnitAction _onCardPlayed = (s, by, X) => { };
        public TriggerUnitAction _onCardDiscarded = (s, by, X) => { };
        TriggerUnitSelf _onDestroy = (s, by) => { s._context.Log(s, "destroyed"); };

        string _onDamagedAbility = "";
        string _onWeakenedAbility = "";
        string _onBoostedAbility = "";
        string _onStrengthledAbility = "";
        string _onHealedAbility = "";
        string _onArmorGainAbility = "";
        string _onCardPlayedAbility = "";
        string _onCardDiscardedAbility = "";
        string _onUnitDamagedAbility = "";
        string _onDestroyAbility = "";

        public void setOnDamaged(TriggerRecieve trigger, string description) { _onDamaged = trigger; _onDamagedAbility = description; }
        public void setOnWeakened(TriggerRecieve trigger, string description) { _onWeakened = trigger; _onWeakenedAbility = description; }
        public void setOnBoosted(TriggerRecieve trigger, string description) { _onBoosted = trigger; _onBoostedAbility = description; }
        public void setOnStrengthled(TriggerRecieve trigger, string description) { _onStrengthled = trigger; _onStrengthledAbility = description; }
        public void setOnHealed(TriggerRecieve trigger, string description) { _onHealed = trigger; _onHealedAbility = description; }
        public void setOnArmorGain(TriggerRecieve trigger, string description) { _onArmorGain = trigger; _onArmorGainAbility = description; }

        public void setOnDestroy(TriggerUnitSelf trigger, string description) { _onDestroy = trigger; _onDestroyAbility = description; }
        public void setOnUnitDamaged(TriggerUnitAction trigger, string description) { _onUnitDamaged = trigger; _onUnitDamagedAbility = description; }
        public void setOnCardPlayed(TriggerUnitAction trigger, string description) { _onCardPlayed = trigger; _onCardPlayedAbility = description; }
        public void setOnCardDiscarded(TriggerUnitAction trigger, string description) { _onCardDiscarded = trigger; _onCardDiscardedAbility = description; }

        public override int power { get { return _power; } }
        public int basePower { get { return _basePower; } }
        public bool isSpy { get { return _isSpy; } }
        public void setSpying() { _isSpy = true; }
        public void setSpyHost(Player spyHost, Player playedBy) { _host = spyHost; _baseHost = playedBy; }
        public void setCharmHost(Player charmer) { _host = charmer; status.SetSpying(_baseHost != charmer); }

        public override Card spawnDefaultCopy(Player newHost, Card sourceOfMakeingCopy)
        {
            Unit copy = this.spawnCard() as Unit;
            copy.SetDefaultHost(newHost, sourceOfMakeingCopy.context);
            return CommonFunc.createToken(copy, sourceOfMakeingCopy);
        }
        public override Card spawnCard()
        {
            string methodName = name, callMethodName = "";
            for (int i = 0; i < methodName.Length; ++i)
                if (alphabet.IndexOf(methodName[i]) >= 0)
                    callMethodName += methodName[i];
            SpawnUnit spun = new SpawnUnit();
            PropertyInfo m = spun.GetType().GetProperty(callMethodName);
            return m.GetMethod.Invoke(spun, null) as Unit;
        }
        public virtual void setUnitAttributes(int DefaultPower, params Tag[] Tags)
        {
            _defaultPower = _basePower = _power = DefaultPower;
            _tags = Tags.ToList();
        }
        public virtual void move(Card source, int rowTo)
        {
            if (row == rowTo)
                return;
            row = rowTo;
            _show.redrawCausedMove();
            _onMove(source, Place.battlefield);
        }
        public virtual void setBasePowerTo(Card source, int X)
        {
            //Effects.Trajectory(source, this, showAction.boost);
            _basePower = _power = X;
        }
        // return was unit destroyed or not
        public virtual bool damage(Card source, int X)
        {
            // can not deal any damage to unit in deck, graveyard or banish
            // in hand unit can be only SET HEALTH to low his power
            // in battlefield all actions are inlaw
            if (place != Place.battlefield)
                return false;
            if (X <= 0)
                return false;

            int unitHasArmor = status.armor;
            status.armor = Math.Max(0, unitHasArmor - X);
            X = X - unitHasArmor;
            if (X <= 0)
                return false;

            _power -= X;
            Effects.Trajectory(source, this, showAction.damage);
            _show.redrawCausedChangeValue();
            _onDamaged(this, source, X);

            foreach (Card c in this.context.cards)
                if (c as Unit != null)
                    (c as Unit)._onUnitDamaged(c as Unit, this, X);

            bool mustBeDestroyed = isMustbeDestroyed;
            if (mustBeDestroyed)
                destroy(source);

            return mustBeDestroyed;
        }
        public virtual void weaken(Card source, int X)
        {
            if (X <= 0)
                return;
            _power -= X;
            _basePower -= X;
            Effects.Trajectory(source, this, showAction.weaken);
            _show.redrawCausedChangeValue();
            _onWeakened(this, source, X);

            if (isMustbeBanished)
                banish(source);
        }
        public virtual void boost(Card source, int X)
        {
            if (X <= 0)
                return;
            _power += X;
            Effects.Trajectory(source, this, showAction.boost);
            _show.redrawCausedChangeValue();
            _onBoosted(this, source, X);
        }
        public virtual void gainArmor(Card source, int X)
        {
            if (X <= 0)
                return;
            status.armor += X;
            Effects.Trajectory(source, this, showAction.armor);
            _show.redrawCausedChangeValue();
            _onArmorGain(this, source, X);
        }
        public virtual void strengthen(Card source, int X)
        {
            if (X <= 0)
                return;
            _power += X;
            _basePower += X;
            Effects.Trajectory(source, this, showAction.stretngthlen);
            _show.redrawCausedChangeValue();
            _onStrengthled(this, source, X);
        }
        public virtual void heal(Card source, int X)
        {
            if (X <= 0)
                return;
            int healthMissed = _basePower - _power;
            if (healthMissed <= 0)
                return;
            int healCount = Math.Min(X, healthMissed);
            _power += healCount;
            _show.redrawCausedChangeValue();
            _onHealed(this, source, healCount);
        }
        public virtual void heal(Card source)
        {
            if (_power >= _basePower)
                return;
            int healCount = _basePower - _power;
            _power += healCount;
            _show.redrawCausedChangeValue();
            _onHealed(this, source, healCount);
        }
        public virtual void reset(Card source)
        {
            _power = _basePower;
            _show.redrawCausedChangeValue();
        }
        public virtual void destroy(Card source)
        {
            move(Place.graveyard);
            _onDestroy(this, source);
            this.status.Clear();
        }
        public virtual void banish(Card source)
        {
            move(Place.banish);
            this.status.Clear();
        }
        public bool isDamaged { get { return _power < _basePower; } }
        public bool isBoosted { get { return _power > _basePower; } }
        bool isMustbeDestroyed { get { return _power <= 0; } }
        bool isMustbeBanished { get { return _basePower <= 0; } }

        public override string ToString()
        {
            return String.Format("[{0} {1}]{2}", power, base.ToString(), status.ToStringBattlefield());
            //return String.Format("[{0} {1}]{2}__{3}{4}_{5}", power, base.ToString(), status.ToStringBattlefield(),
            //    _host == null ? 'X' : _host.ToString()[0],
            //    _baseHost == null ? 'X' : _baseHost.ToString()[0], pid
            //    );
        }
        public override string ToStringFull()
        {
            return String.Format("[{0} (base={1}, default={2}), status={3}) tags=[{5}] {4}]",
                _power, _basePower, _defaultPower, status.ToString(), base.ToStringFull(), tagsToString());
        }

        public override string ToFormatAbilities()
        {
            string baseAbilities = base.ToFormatAbilities();
            string abilities = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                baseAbilities.Length == 0 ? "" : baseAbilities,
                _onDestroyAbility.Length == 0 ? "" : (_onDestroyAbility + "\n"),
                _onDamagedAbility.Length == 0 ? "" : (_onDamagedAbility + "\n"),
                _onWeakenedAbility.Length == 0 ? "" : (_onWeakenedAbility + "\n"),
                _onBoostedAbility.Length == 0 ? "" : (_onBoostedAbility + "\n"),
                _onStrengthledAbility.Length == 0 ? "" : (_onStrengthledAbility + "\n"),
                _onHealedAbility.Length == 0 ? "" : (_onHealedAbility + "\n"),
                _onArmorGainAbility.Length == 0 ? "" : (_onArmorGainAbility + "\n"),
                _onUnitDamagedAbility.Length == 0 ? "" : (_onUnitDamagedAbility + "\n"),
                _onCardPlayedAbility.Length == 0 ? "" : (_onCardPlayedAbility + "\n"),
                _onCardDiscardedAbility.Length == 0 ? "" : (_onCardDiscardedAbility + "\n")
                );
            string res = String.Format("{0}{1}", abilities, AbilityHints.addHitsTo(abilities));
            return res.Trim().Length == 0? "No abilities." : res;
        }

        public override string ToFormat()
        {
            return String.Format("{0}\n\n{1} {2} unit\n{3}\n\nPower = {4} (base = {5}, default = {6})\nStatus = {7}\n\n{8}",
                name.ToUpper(), clan.ToString(), rarity.ToString(), tagsToString(),
                _power, _basePower, _defaultPower, status.ToString(), ToFormatAbilities());
        }
        public override string ToFormatCollection()
        {
            return String.Format("{0}\n\n{1} {2} unit\n{3}\n\nPower {4}.\n\n{5}",
                name.ToUpper(), clan.ToString(), rarity.ToString(), tagsToString(),_basePower, ToFormatAbilities());
        }
    }
}
