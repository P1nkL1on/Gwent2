using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class CommonFunc
    {
        public static int createChoiseOptionCount = -1;
        public static Random unitDecisionsRandomiser = new Random();

        // ||| universal templates |||
        // < >
        public static List<Card> allCards = DeckIO.invokeAllCards();
        public static Unit transformUnit(Unit target, Unit transformInto, Card by)
        {
            var token = transformInto;
            // make same host
            token.SetDefaultHost(target.host, target.context);
            // make same position
            token.place = target.place;
            token.row = target.row;
            // change in _cards array of game
            token.context.ReplaceCardInGame(token, target);
            // make visible to all others
            token.makeVisibleAll();
            // ask redrawing
            token._show.redrawCausedChangeValue();
            return token;
        }
        public static Unit createToken(Unit preset, Card source)
        {
            preset.SetDefaultHost(source.host, source.context);
            source.context.AddCardToGame(preset);
            return preset;
        }
        public static void resurrectAllyUnit(Card self, params UnitPredicat[] filters)
        {
            // select one card from discard
            List<UnitPredicat> fs = new List<UnitPredicat>();
            fs.Add(Filter.anyAllyUnitInDiscard(self as Unit));
            fs.AddRange(filters);

            Unit u = self.host.selectUnit(
                        Select.Units(self.context.cards,
                        fs.ToArray()), self.QestionString());
            // the player plays a card if its exists
            if (u != null)
                self.host.playCard(u);
        }
        public static bool dealDamage(Card self, int damageValue)
        {
            Unit t = self.host.selectUnit(
                        Select.Units(self.context.cards, Filter.anyOtherUnitInBattlefield(self as Unit)),
                        self.QestionString());
            if (t != null)
                return t.damage(self, damageValue);
            return false;
        }
        public static Unit weatherMage(string Name, string description, Clan clan, Card option1, Card option2, Card option3, params Tag[] tags)
        {
            Unit self = new Unit();
            self.setAttributes(clan, Rarity.silver, Name);
            self.setUnitAttributes(4, tags);
            self.setOnDeploy((s, f) =>
            {
                List<Card> vars = new List<Card>() { option1, option2, option3 };
                s.host.playCard(SpawnSpecial.addSpecialToGame(s.host.selectCard(vars, s.QestionString()) as Special, s));
                s._show.redrawCausedChangeValue();
            }, description);
            return self;
        }
        public static void duel(Unit self, Unit fightWith)
        {
            for (; ; )
            {
                bool opponentDead = fightWith.damage(self, self.power);
                if (opponentDead) return;
                bool selfDead = self.damage(fightWith, fightWith.power);
                if (selfDead) return;
            }
        }
        public static void applyHazzardOnDeployWithoutCard(Card s, Special analogCard, TriggerTurnRowEffect hazzardEffect)
        {
            Player enemy = s.host.chooseEnemy(s.context, s.QestionString());
            int row = s.host.chooseEnemyRow(enemy, s.QestionString());

            // there is a little trick
            // instead of creating unique birna roweffect,
            // we create a DOOMed version of skelligian storm
            // and apply (DO NOT PLAY) it to choosen row
            // after it it will dissappear in banish

            Special st = SpawnSpecial.addSpecialToGame(analogCard, s);
            st.move(Place.banish);
            RowEffect hazz = new RowEffect(st, enemy, row);
            hazz.SetBehaviour(hazzardEffect);
        }
        public static Card createCard(Card source, List<Card> listOriginal, params CardPredicat[] filters)
        {
            if (listOriginal.Count == 0)
                return null;
            var fs = filters.ToList();
            fs.Add((crd) => { return crd.name != source.name; });
            filters = fs.ToArray();

            Card c = source.host.selectCard(
                Filter.randomCardsFrom(
                    Select.Cards(listOriginal, filters),
                    createChoiseOptionCount),
                source.QestionString());
            if (c == null)
                return null;
            if (c as Unit != null)
            {
                Unit u = c.spawnDefaultCopy(source.host, source) as Unit;
                u.context.Log(u, "unit created by " + source.ToString());
                return u;
            }
            if (c as Special != null)
            {
                Special s = c.spawnDefaultCopy(source.host, source) as Special;
                s.context.Log(s, "special created by " + source.ToString());
                return s;
            }
            return null;
        }
       
        
    }

    
}
