using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Player
    {
        protected string _name;
        protected string name { get { return _name; } }

        public override string ToString()
        {
            return String.Format("{0}", name);
        }

        public virtual Card selectCard(List<Card> fromList)
        {
            var res = selectCards(fromList, 1);
            return res.Count == 0? null : res[0];
        }
        public virtual List<Card> selectCards(List<Card> fromList, int nCardsToSelect)
        {
            if (fromList.Count <= nCardsToSelect)
                return fromList;
            List<Card> resChoise = new List<Card>();

            for (int i = 0; i < nCardsToSelect; ++i){
                List<string> options = new List<string>();
                foreach (Card u in fromList)
                    options.Add(u.ToString());

                int decidedIndex = makeDescision(options);
                resChoise.Add(fromList[decidedIndex]);
                fromList.RemoveAt(decidedIndex);
            }
            return resChoise;
        }
        public virtual List<Unit> selectUnits(List<Unit> fromList, int nCardsToSelect)
        {
            List<Card> fromListU = new List<Card>();
            foreach (Card c in fromList)
                if (c as Unit != null)
                fromListU.Add(c);

            var res = selectCards(fromListU, nCardsToSelect);
            fromListU.Clear();
            List<Unit> resU = new List<Unit>();
            foreach (Card c in res)
                resU.Add(c as Unit);
            return resU;
        }
        public virtual Unit selectUnit(List<Unit> fromList)
        {
            List<Card> fromListU = new List<Card>();
            foreach (Card c in fromList)
                if (c as Unit != null)
                    fromListU.Add(c);

            Card choosen = selectCard(fromListU);
            return choosen == null ? null : (choosen as Unit);
        }

        public virtual void playCard(Card card)
        {
            if (card as Unit != null)
                (card as Unit).row = 1;
            card.move(Place.battlefield);
        }

        protected Random rnd = new Random();

        protected virtual int makeDescision (List<String> variants){
            return rnd.Next(variants.Count);
        }
    }
}
