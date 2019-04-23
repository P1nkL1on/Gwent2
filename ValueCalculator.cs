using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    // deploy == max value, which you can play from your hand
    delegate double deployValue(Card self, Place from);
    // stay == value of any unit in the battlefield
    delegate double battlefieldStayValue(Card self);

    class Value
    {
        public static deployValue justPower() { return (s, f) => { return (double)s.power; }; }
        public static deployValue spawnToken(double tokenValue) { return (s, f) => { return (double)s.power + tokenValue; }; }
        public static deployValue targetUnitDamage(int damageCount)
        {
            return (s, f) =>
            {
                return 0.0;
            };
        }
        public static battlefieldStayValue justStayPower() { return (s) => { return s.power; }; }

        public static double maxStayValue(List<Card> cs, out Card best)
        {
            best = null;
            double bestValue = double.MinValue;
            foreach (Card c in cs)
            {
                double val = c._stayValue(c);
                if (val < bestValue) continue;
                bestValue = val;
                best = c;
            }
            return bestValue;
        }
        public static double maxPlayValue(List<Card> cs, out Card best)
        {
            best = null;
            double bestValue = double.MinValue;
            foreach (Card c in cs)
            {
                double val = c._valueDeploy(c, Place.hand);
                if (val < bestValue) continue;
                bestValue = val;
                best = c;
            }
            return bestValue;
        }
        public static double meanPlayValue(List<Card> cs)
        {
            double summValue = 0;
            foreach (Card c in cs)
                summValue += c._valueDeploy(c, Place.hand);
            return summValue / cs.Count;
        }
        public static double meanTopdeckValue(Card asker)
        {
            return meanPlayValue(Select.Cards(asker.context.cards, Filter.anyCardInBaseHostDeck(asker)));
        }            
    }
}
