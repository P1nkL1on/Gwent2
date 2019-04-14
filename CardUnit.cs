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
        List<Tag> _tags;
        public int row = -1;
        public bool hasTag(Tag tag) { return _tags.IndexOf(tag) >= 0; }

        TriggerRecieve _onDamaged        = (s, by, X) => { s._context.Log(s, String.Format("damaged by {0} for {1}", by.ToString(), X)); };
        TriggerRecieve _onWeakened       = (s, by, X) => { s._context.Log(s, String.Format("weakened by {0} for {1}", by.ToString(), X)); };
        TriggerRecieve _onBuffed         = (s, by, X) => { s._context.Log(s, String.Format("buffed by {0} for {1}", by.ToString(), X)); };
        TriggerRecieve _onStrengthled    = (s, by, X) => { s._context.Log(s, String.Format("strenghled by {0} for {1}", by.ToString(), X)); };
        TriggerRecieve _onHealed         = (s, by, X) => { s._context.Log(s, String.Format("healed by {0} for {1}", by.ToString(), X)); };
        TriggerRecieve _onArmorGain      = (s, by, X) => { s._context.Log(s, String.Format("gain {1} armor from {0}", by.ToString(), X)); };

        TriggerUnitAction _onUnitDamaged = (s, by, X) => { /*s._context.Log(s, String.Format("watchs how {0} suffers {1} damage", by.ToString(), X));*/ };

        public void setOnDamaged(TriggerRecieve trigger) { _onDamaged = trigger; }
        public void setOnWeakened(TriggerRecieve trigger) { _onWeakened = trigger; }
        public void setOnBuffed(TriggerRecieve trigger) { _onBuffed = trigger; }
        public void setOnStrengthled(TriggerRecieve trigger) { _onStrengthled = trigger; }
        public void setOnHealed(TriggerRecieve trigger) { _onHealed = trigger; }
        public void setOnArmorGain(TriggerRecieve trigger) { _onArmorGain = trigger; }

        public void setOnUnitDamaged(TriggerUnitAction trigger) { _onUnitDamaged = trigger; }

        public override int power { get { return _power; } }

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
        }
        public virtual void weaken(Card source, int X)
        {
            if (X <= 0)
                return;
            _power -= X;
            _basePower -= X;
            _onWeakened(this, source, X);
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
        public virtual void stregthen(Card source, int X)
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

        public override string ToString()
        {
            return String.Format("[{0} {1}]", power, base.ToString());
        }
        public override string ToStringFull()
        {
            string tags = "";
            foreach (Tag t in _tags)
                tags += t.ToString() + ", ";

            return String.Format("[{0} (base={1}, default={2}), status={3}) tags=[{5}] {4}]",
                _power, _basePower, _defaultPower, status.ToString(), base.ToStringFull(), tags.Substring(0, tags.Length - 2));
        }
    }
}
