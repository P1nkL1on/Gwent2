using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
        public static Unit TokenJadeFigurine
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.neutral, Rarity.silver, "Jade Figurine");
                self.setUnitAttributes(2, Tag.doomed);
                return self;
            }
        }
        public static Unit TokenBear
        {
            get
            {
                Unit self = new Unit();
                self.setAttributesToken(Clan.neutral, Rarity.bronze, "Bear");
                self.setUnitAttributes(11, Tag.beast, Tag.cursed);
                return self;
            }
        }
        // < > bronze
        // < > silver
        // < > gold
        public static Unit VesemirMentor
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.neutral, Rarity.gold, "Vesemir: Mentor");
                self.setUnitAttributes(6, Tag.witcher);
                self.setOnDeploy((s, f) =>
                {
                    Card item = s.host.selectCard(
                        Select.Cards(s.context.cards,
                            Filter.anyCardInYourDeck(s),
                            Filter.anyCardHasTagAnyFrom(Tag.alchemy),
                            Filter.anyCardHasColor(Rarity.bronze, Rarity.silver)),
                        s.QestionString());
                    if (item != null)
                        s.host.playCard(item);
                }, "Play a Bronze or Silver Alchemy card from your deck.");
                return self;
            }
        }
        public static Unit GeraltofRivia
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.neutral, Rarity.gold, "Geralt of Rivia");
                self.setUnitAttributes(15, Tag.witcher);
                return self;
            }
        }
    }
}
