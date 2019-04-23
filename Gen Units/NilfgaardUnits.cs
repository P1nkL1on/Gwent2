using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
        public static Unit Emissary
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Emissary");
                self.setUnitAttributes(2);
                // WARNING: current comments are actual to every spy unit
                self.setSpying();
                // ! the way to show a player class to play it to the other side
                // base Host is a pointer to Player, who is playing this card!
                self.setOnDeploy((s, f) =>
                {
                    // THE NEXT LINE IS neccessary too
                    (s as Unit).status.isSpy = true;

                    List<Unit> randomBronzePair =
                        Filter.randomUnitsFrom(
                            Select.Units(s.context.cards,
                            Filter.anyUnitInBaseHostDeck(s),
                            Filter.anyUnitHasColor(Rarity.bronze)),
                            2);
                    if (randomBronzePair.Count == 0)
                        return;
                    s.baseHost.playCard(
                        s.baseHost.selectUnit(randomBronzePair, s.QestionString()));
                }, "Spying.\nLook at 2 random Bronze units from your deck, then play 1.");
                return self;
            }
        }
        public static Unit Ambassador
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Ambassador");
                self.setUnitAttributes(2);
                self.setSpying();
                self.setOnDeploy((s, f) =>
                {
                    (s as Unit).status.isSpy = true;

                    Unit t = s.baseHost.selectUnit(
                        Select.Units(s.context.cards, Filter.anyUnitInBaseHostBattlefield(s)),
                        s.QestionString());
                    if (t != null)
                        t.boost(s, 12);

                }, "Spying.\nBoost an ally by 12.");
                return self;
            }
        }

    }
}
