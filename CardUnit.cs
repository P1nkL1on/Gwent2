using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Unit : Card
    {
        public UnitStatus status = new UnitStatus();

        int _defaultPower;
        int _basePower;
        int _power;
        public int row = -1;

        TriggerRecieve _onDamaged       = (s, by, X) => { s._context.Log(s, String.Format("damaged for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onWeakened      = (s, by, X) => { s._context.Log(s, String.Format("weakened for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onBuffed        = (s, by, X) => { s._context.Log(s, String.Format("buffed for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onStrengthled   = (s, by, X) => { s._context.Log(s, String.Format("strenghled for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onHealed        = (s, by, X) => { s._context.Log(s, String.Format("healed for {1} by {0}", by.ToString(), X)); };
        TriggerRecieve _onArmorGain     = (s, by, X) => { s._context.Log(s, String.Format("gain {1} armor from {0}", by.ToString(), X)); };

        TriggerUnitAction _onUnitDamaged = (s, by, X) => { /*s._context.Log(s, String.Format("watchs how {0} suffers {1} damage", by.ToString(), X));*/ };
        TriggerUnitSelf _onDestroy = (s, by) => { s._context.Log(s, "destroyed");};

        string _onDamagedAbility = "";
        string _onWeakenedAbility = "";
        string _onBuffedAbility = "";
        string _onStrengthledAbility = "";
        string _onHealedAbility = "";
        string _onArmorGainAbility = "";
        string _onUnitDamagedAbility = "";
        string _onDestroyAbility = "";

        public void setOnDamaged(TriggerRecieve trigger, string description) { _onDamaged = trigger; _onDamagedAbility = description; }
        public void setOnWeakened(TriggerRecieve trigger, string description) { _onWeakened = trigger; _onWeakenedAbility = description; }
        public void setOnBuffed(TriggerRecieve trigger, string description) { _onBuffed = trigger; _onBuffedAbility = description; }
        public void setOnStrengthled(TriggerRecieve trigger, string description) { _onStrengthled = trigger; _onStrengthledAbility = description; }
        public void setOnHealed(TriggerRecieve trigger, string description) { _onHealed = trigger; _onHealedAbility = description; }
        public void setOnArmorGain(TriggerRecieve trigger, string description) { _onArmorGain = trigger; _onArmorGainAbility = description; }

        public void setOnDestroy(TriggerUnitSelf trigger, string description) { _onDestroy = trigger; _onDestroyAbility = description; }
        public void setOnUnitDamaged(TriggerUnitAction trigger, string description) { _onUnitDamaged = trigger; _onUnitDamagedAbility = description; }

        public override int power { get { return _power; } }
        public int basePower { get { return _basePower; } }

        public virtual void setUnitAttributes (int DefaultPower, params Tag[] Tags){
            _defaultPower = _basePower = _power = DefaultPower;
            _tags = Tags.ToList();
        }

        public virtual void damage  (Card source, int X)
        {
            if (X <= 0)
                return;

            int unitHasArmor = status.armor;
            status.armor = Math.Max(0, unitHasArmor - X);
            X = X - unitHasArmor;
            if (X <= 0)
                return;

            _power -= X;
            _onDamaged(this, source, X);

            foreach (Card c in this.context.cards)
                if (c as Unit != null)
                    (c as Unit)._onUnitDamaged(c as Unit, this, X);

            if (isMustbeDestroyed)
                destroy(source);
        }
        public virtual void weaken(Card source, int X)
        {
            if (X <= 0)
                return;
            _power -= X;
            _basePower -= X;
            _onWeakened(this, source, X);

            if (isMustbeBanished)
                banish(source);
        }
        public virtual void buff(Card source, int X)
        {
            if (X <= 0)
                return;
            _power += X;
            _onBuffed(this, source, X);
        }
        public virtual void gainArmor(Card source, int X)
        {
            if (X <= 0)
                return;
            status.armor += X;
            _onArmorGain(this, source, X);
        }
        public virtual void strengthen(Card source, int X)
        {
            if (X <= 0)
                return;
            _power += X;
            _basePower += X;
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
            _onHealed(this, source, healCount);
        }
        public virtual void heal(Card source)
        {
            if (_power >= _basePower)
                return;
            int healCount = _basePower - _power;
            _power += healCount;
            _onHealed(this, source, healCount);
        }
        public virtual void restore(Card source)
        {
            _power = _basePower;
        }
        public virtual void destroy(Card source)
        {
            move(Place.graveyard);
            restore(this);
            _onDestroy(this, source);
        }
        public virtual void banish(Card source)
        {
            move(Place.banish);
            restore(this);
        }
        public bool isDamaged { get { return _power < _basePower; } }
        bool isMustbeDestroyed { get { return _power <= 0; } }
        bool isMustbeBanished { get { return _basePower <= 0; } }

        public override string ToString()
        {
            return String.Format("[{0} {1}]{2}", power, base.ToString(), status.ToStringBattlefield());
        }
        string tagsToString()
        {
            string tags = "";
            foreach (Tag t in _tags)
                tags += t.ToString() + ", ";
            return tags.Substring(0, tags.Length - 2);
        }
        public override string ToStringFull()
        {
            return String.Format("[{0} (base={1}, default={2}), status={3}) tags=[{5}] {4}]",
                _power, _basePower, _defaultPower, status.ToString(), base.ToStringFull(), tagsToString());
        }

        public override string ToFormatAbilities()
        {
            string baseAbilities = base.ToFormatAbilities();
            string abilities = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                baseAbilities.Length == 0 ? "" : baseAbilities,
                _onDestroyAbility.Length == 0 ? "" : (_onDestroyAbility + "\n"),
                _onDamagedAbility.Length == 0 ? "" : (_onDamagedAbility + "\n"),
                _onWeakenedAbility.Length == 0 ? "" : (_onWeakenedAbility + "\n"),
                _onBuffedAbility.Length == 0 ? "" : (_onBuffedAbility + "\n"),
                _onStrengthledAbility.Length == 0 ? "" : (_onStrengthledAbility + "\n"),
                _onHealedAbility.Length == 0 ? "" : (_onHealedAbility + "\n"),
                _onArmorGainAbility.Length == 0 ? "" : (_onArmorGainAbility + "\n"),
                _onUnitDamagedAbility.Length == 0 ? "" : (_onUnitDamagedAbility + "\n"));
            return String.Format("{0}{1}", abilities, AbilityHints.addHitsTo(abilities));
        }

        public override string ToFormat()
        {
            return String.Format("{0}\n\n{1} {2} unit\n{3}\n\nPower = {4} (base = {5}, default = {6})\nStatus = {7}\n\n{8}", 
                name.ToUpper(), clan.ToString(), rarity.ToString(), tagsToString(), 
                _power, _basePower, _defaultPower, status.ToString(), ToFormatAbilities());
        }
    }
}
