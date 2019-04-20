using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    enum Place
    {
        token,
        deck,
        hand,
        battlefield,
        graveyard,
        banish,
        handLeader
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
        /// <summary>
        /// special unit card type, used one per deck
        /// </summary>
        leader,
        /// <summary>
        /// doomed units will be banished instead of sending to graveyard after destroying or round end
        /// </summary>
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
        clanTordarroch,
        clanBrokvar,

        special,
        spell,
        item,
        alchemy,
        tactic,
        organic,
        hazzard,
        boon
    }

    class Utils
    {
        public static List<Place> allPlaces = new List<Place>() { Place.battlefield, Place.hand, Place.handLeader, Place.deck, Place.graveyard, Place.banish };
        public static List<string> allRows = new List<string>() { "melee", "ranged", "support"};

        // line out
        public static int leftTextColumnWidth = 45;
        public static int leftTextColumnHeigth = 44;
        public static int fieldHeigth = 70;
        public static int fieldStartHorizontal = leftTextColumnWidth + 2;
        public static int fieldPerPlayerHorizontal = 60;
        public static int fieldStartVerticalOffset = 2;

    }
}
