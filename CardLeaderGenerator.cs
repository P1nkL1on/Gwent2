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
    }
}
