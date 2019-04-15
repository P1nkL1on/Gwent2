using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnSpecial
    {
        public static Special AlzursThunder
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Alzur's Thunder");
                spec.setSpecialAttributes(Tag.spell);
                spec.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyUnitInBattlefield()), s.QestionString());
                    if (t != null)
                        t.damage(s, 9);
                }, "Deal 9 damage.");
                return spec;
            }
        }
        public static Special WyvernScaleShield
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Wyvern Scale Shield");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    var possibleTargetsForBuff 
                        = Select.Units(s.context.cards, Filter.anyUnitInBattlefield());
                    var possibleSourceOfBuff 
                        = Select.Units(s.context.cards, 
                            Filter.anyAllyUnitInHand(s),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver));
                    if (possibleSourceOfBuff.Count == 0 || possibleTargetsForBuff.Count == 0)
                        return; // no targets

                    Unit so = s.host.selectUnit(possibleSourceOfBuff, "Select a Bronze or Silver unit in your hand");
                    Unit to = s.host.selectUnit(possibleTargetsForBuff, "Select a unit to buff");
                    
                    to.buff(s, so.basePower);    

                }, "Boost a unit by the base power of a Bronze or Silver unit in your hand.");
                return spec;
            }
        }
    }
}
