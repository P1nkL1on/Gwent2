using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    enum Place
    {
        token = -1,
        deck = 0,
        hand = 1,
        battlefield = 2,
        graveyard = 3,
        banish = 4
    }

    enum Rarity
    {
        none = -1,
        bronze = 0,
        silver = 1,
        gold = 2
    }

    enum Clan
    {
        neutral = 0,
        northen = 1,
        skellige = 2,
        nilfgaard = 3,
        scoetaels = 4,
        monsters = 5
    }

    enum Tag
    {
        none,

        doomed,
        cursed,

        beast,
        machine,
        soldier,
        officer,
        support,
        cultist,

        clanAnCraite,
        clanTuirseach,
        clanHeyMaey,
        clanDrummond,
        clanDimun,

        special,
        spell,
        item,
        alchemy,
        tactic
    }

    class Utils
    {
        public static List<Place> allPossiblePlaces = new List<Place>() { Place.battlefield, Place.hand, Place.deck, Place.graveyard, Place.banish };
    }
}
