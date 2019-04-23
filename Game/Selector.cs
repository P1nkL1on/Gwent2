using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Select
    {
        public static List<Unit> Units(List<Card> fromList, params UnitPredicat[] filters)
        {
            List<Unit> res = new List<Unit>();
            foreach (Card c in fromList)
            {
                Unit u = c as Unit;
                if (u == null)
                    continue;
                bool acceptCard = true;
                foreach (UnitPredicat filter in filters)
                    if (!filter(u))
                    {
                        acceptCard = false;
                        break;
                    }
                if (!acceptCard)
                    continue;
                res.Add(u);
            }
            return res;
        }
        public static List<Special> Specials(List<Card> fromList, params SpecialPredicat[] filters)
        {
            List<Special> res = new List<Special>();
            foreach (Card c in fromList)
            {
                Special s = c as Special;
                if (s == null)
                    continue;
                bool acceptCard = true;
                foreach (SpecialPredicat filter in filters)
                    if (!filter(s))
                    {
                        acceptCard = false;
                        break;
                    }
                if (!acceptCard)
                    continue;
                res.Add(s);
            }
            return res;
        }
        public static List<Card> Cards(List<Card> fromList, params CardPredicat[] filters)
        {
            List<Card> res = new List<Card>();
            foreach (Card c in fromList)
            {
                bool acceptCard = true;
                foreach (CardPredicat filter in filters)
                    if (!filter(c))
                    {
                        acceptCard = false; 
                        break;
                    }
                if (!acceptCard)
                    continue;
                res.Add(c);
            }
            return res;
        }
        
    }
}
