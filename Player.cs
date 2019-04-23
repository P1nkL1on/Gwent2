using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Player
    {
        public int roundsWin = 0;
        public bool passed = false;
        protected string _name;
        protected string name { get { return _name; } }

        public override string ToString()
        {
            return String.Format("{0}", name);
        }
        //public virtual void SetContext(Match context) { /*prohibit*/}
        public virtual Card selectCard(List<Card> fromList, string question)
        {
            var res = selectCards(fromList, 1, question);
            return res.Count == 0 ? null : res[0];
        }
        public virtual Card selectOneAndReturnRest(List<Card> fromList, out Card anotherCard, string question)
        {
            anotherCard = null;
            if (fromList.Count == 0)
                return null;
            if (fromList.Count == 1)
                return fromList[0];

            var res = selectCard(fromList, question);
            anotherCard = res == fromList[0] ? fromList[1] : fromList[0];
            return res;
        }
        public virtual List<Card> selectCards(List<Card> fromList, int nCardsToSelect, string question)
        {
            if (fromList.Count <= nCardsToSelect)
                return fromList;
            List<Card> resChoise = new List<Card>();

            for (int i = 0; i < nCardsToSelect; ++i)
            {
                int decidedIndex = makeDescision(CardChoiseContext.Default(fromList, question));
                resChoise.Add(fromList[decidedIndex]);
                fromList.RemoveAt(decidedIndex);
            }
            return resChoise;
        }
        public virtual Card selectCardOrNone(List<Card> fromList, string question, string extraName = "none")
        {
            var res = selectCardsOrNone(fromList, 1, question, extraName);
            return res.Count == 0 ? null : res[0];
        }
        public virtual List<Card> selectCardsOrNone(List<Card> fromList, int nUpToCardsToSelect, string question, string extraName = "none")
        {
            List<Card> resChoise = new List<Card>();
            for (int i = 0; i < nUpToCardsToSelect; ++i)
            {
                int decidedIndex = makeDescision(CardChoiseContext.WithNoneOption(fromList, question, extraName)) - 1;
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

            List<Unit> resU = new List<Unit>();
            foreach (Card c in res)
                resU.Add(c as Unit);
            fromListU.Clear();
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
        public virtual int selectOption(ChoiseOptionContext options)
        {
            return makeDescision(options);
        }
        public virtual void playCard(Card card)
        {
            card.context.Log(card, "played by " + this.ToString());
            Unit unit = card as Unit;
            if (unit != null)
                if (!unit.isSpy)
                    choosePlaceForUnit(unit);
                else
                {
                    unit.setSpyHost(chooseEnemy(card.context, "Select new host for " + card.ToString()), this);
                    choosePlaceForSpyUnit(unit);
                }
            card.move(Place.battlefield);
        }

        public virtual int chooseRow(string question)
        {
            return makeDescision(new RowChoiseContext(this, question));
        }
        public virtual int chooseEnemyRow(Player enemy, string question)
        {
            return makeDescision(new RowChoiseContext(enemy, question));
        }
        public virtual void choosePlaceForUnit(Unit unit){
            unit.row = chooseRow("Select row for " + unit.ToString());
            int wantedPlace = chooseUnitsPlaceInRow(unit.context._allUnitsInRow(unit.row, unit.host));
            unit.context._setUnitToPositionInRow(unit, wantedPlace);
        }
        public virtual void choosePlaceForSpyUnit(Unit unit)
        {
            unit.row = chooseEnemyRow(unit.host, String.Format("Select {0}'s row for {1}", unit.host.ToString(), unit.ToString()));
            int wantedPlace = chooseUnitsPlaceInRow(unit.context._allUnitsInRow(unit.row, unit.host));
            unit.context._setUnitToPositionInRow(unit, wantedPlace);
        }
        public virtual Player chooseEnemy(Match context, string question)
        {
            List<Player> enemies = new List<Player>();
            foreach (Player p in context.players)
                if (p != this)
                    enemies.Add(p);
            if (enemies.Count == 1)
                return enemies[0];
            return enemies[makeDescision(new PlayerChoiseContext(enemies, question))];
        }
        public virtual int chooseUnitsPlaceInRow(List<Unit> neigthboors)
        {
            return makeDescision(CardChoiseContext.WithNoneOption(neigthboors, "Select a unit to left", "Become the most left"));
        }

        protected Random rnd = new Random();

        protected virtual int makeDescision(ChoiseContext choiseContext)
        {
            return rnd.Next(choiseContext.OptionsCount);
        }
    }
}
