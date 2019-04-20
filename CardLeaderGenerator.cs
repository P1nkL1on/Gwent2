using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnLeader
    {
        public static Leader HaraldtheCripple
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.skellige, Rarity.gold, "Harald the Cripple");
                self.setUnitAttributes(6, Tag.clanAnCraite);
                self.setLeaderAttributes();
                self.setOnDeploy((s, f) =>
                {
                    for (int i = 0; i < 9; ++i)
                    {
                        Unit t = Filter.randomUnitFrom(Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s), Filter.anyUnitInRow((s as Unit).row)));
                        if (t != null)
                            t.damage(s, 1);
                    }
                }, "Deal 1 damage to a random enemy on the opposite row. Repeat 9 times.");
                return self;
            }
        }
        public static Leader BranTuirseach
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.skellige, Rarity.gold, "Bran Tuirseach");
                self.setUnitAttributes(2, Tag.clanTuirseach);
                self.setLeaderAttributes();
                self.setOnDeploy((s, f) =>
                {
                    foreach (Card c in s.host.selectCardsOrNone(Select.Cards(s.context.cards, Filter.anyCardInYourDeck(s)), 3, s.QestionString()))
                    {
                        c.move(Place.graveyard);
                        if (c as Unit != null)
                            (c as Unit).strengthen(s, 1);
                    }
                }, "Discard up to 3 cards from your deck and Strengthen them by 1.");
                return self;
            }
        }
        public static Leader EistTuirseach
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.skellige, Rarity.gold, "Eist Tuirseach");
                self.setUnitAttributes(5, Tag.clanTuirseach);
                self.setLeaderAttributes();
                self.setOnDeploy((s, f) =>
                {
                    List<Unit> possibleSolders = new List<Unit>() { 
                        SpawnUnit.TuirseachArcher, 
                        SpawnUnit.TuirseachAxeman,
                        SpawnUnit.TuirseachBearmaster,
                        SpawnUnit.TuirseachHunter,
                        SpawnUnit.TuirseachSkirmisher
                    };
                    s.host.playCard(SpawnUnit.createToken(s.host.selectUnit(possibleSolders, s.QestionString()), s));
                }, "Spawn a Bronze Clan Tuirseach Soldier.");
                return self;
            }
        }
        public static Leader CrachanCraite
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.skellige, Rarity.gold, "Crach an Craite");
                self.setUnitAttributes(5, Tag.clanAnCraite);
                self.setLeaderAttributes();
                self.setOnDeploy((s, f) =>
                {
                    Unit u = Filter.highestUnit(Select.Units(s.context.cards, Filter.anyAllyUnitInDeck(s), Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver), Filter.nonSpying()));
                    if (u == null)
                        return;
                    u.strengthen(s, 2);
                    s.host.playCard(u);

                }, "Strengthen the Highest non-Spying Bronze or Silver unit in your deck by 2 and play it.");
                return self;
            }
        }
    }
}
