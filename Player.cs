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

        public virtual Card selectCard(List<Card> fromList, string question)
        {
            var res = selectCards(fromList, 1, question);
            return res.Count == 0? null : res[0];
        }
        public virtual List<Card> selectCards(List<Card> fromList, int nCardsToSelect, string question)
        {
            if (fromList.Count <= nCardsToSelect)
                return fromList;
            List<Card> resChoise = new List<Card>();

            for (int i = 0; i < nCardsToSelect; ++i){
                List<string> options = new List<string>();
                foreach (Card u in fromList)
                    options.Add(u.ToString());

                int decidedIndex = makeDescision(options, question);
                resChoise.Add(fromList[decidedIndex]);
                fromList.RemoveAt(decidedIndex);
            }
            return resChoise;
        }
        public virtual Card selectCardOrNone(List<Card> fromList, string question)
        {
            var res = selectCardsOrNone(fromList, 1, question);
            return res.Count == 0 ? null : res[0];
        }
        public virtual List<Card> selectCardsOrNone(List<Card> fromList, int nUpToCardsToSelect, string question)
        {
            List<Card> resChoise = new List<Card>();
            for (int i = 0; i < nUpToCardsToSelect; ++i)
            {
                List<string> options = new List<string>();
                options.Add("None");
                foreach (Card u in fromList)
                    options.Add(u.ToString());
                int decidedIndex = makeDescision(options, question) - 1;
                if (decidedIndex < 0)
                    return resChoise;
                resChoise.Add(fromList[decidedIndex]);
                fromList.RemoveAt(decidedIndex);
            }
            return resChoise;
        }
        public virtual List<Unit> selectUnits(List<Unit> fromList, int nCardsToSelect, string question)
        {
            List<Card> fromListU = new List<Card>();
            foreach (Card c in fromList)
                if (c as Unit != null)
                fromListU.Add(c);

            var res = selectCards(fromListU, nCardsToSelect, question);
            fromListU.Clear();
            List<Unit> resU = new List<Unit>();
            foreach (Card c in res)
                resU.Add(c as Unit);
            return resU;
        }
        public virtual Unit selectUnit(List<Unit> fromList, string question)
        {
            List<Card> fromListU = new List<Card>();
            foreach (Card c in fromList)
                if (c as Unit != null)
                    fromListU.Add(c);

            Card choosen = selectCard(fromListU, question);
            return choosen == null ? null : (choosen as Unit);
        }

        public virtual void playCard(Card card)
        {
            if (card as Unit != null)
                (card as Unit).row = chooseRow("Select row for " + card.ToString());
            card.move(Place.battlefield);
        }

        public virtual int chooseRow(string question)
        {
            return makeDescision(Utils.allRows, question);
        }

        protected Random rnd = new Random();

        protected virtual int makeDescision (List<String> variants, string question){
            return rnd.Next(variants.Count);
        }
    }
}
