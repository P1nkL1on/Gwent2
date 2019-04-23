using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnLeader : Spawner
    {
        // game mechanics
        public static Leader TacticalAdvantage(Player host, Match context)
        {
            Leader ta = new Leader();
            ta.setAttributes(Clan.neutral, Rarity.gold, "Tactical Advantage");
            ta.setUnitAttributes(0, Tag.special);
            ta.place = Place.banish;
            ta.SetDefaultHost(host, context);
            ta.timer = new Timer(ta, (s) => { }, 1, false);
            ta.setOnCardPlayed((t, card, X) =>
            {
                if (t.context.RoundNumber != 1 || card.host != t.host || !card.hasTag(Tag.leader))
                    return;
                if (t.timer.IsEverTicked())
                    return;
                t.timer.Tick();
                (card as Unit).boost(t, 10);
            }, "Single-Use: Whenever you play a Leader while first round, boost it by 10.");
            return ta;
        }
        // Skellige
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
                    s.host.playCard(CommonFunc.createToken(s.host.selectUnit(possibleSolders, s.QestionString()), s));
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

        // Nilfgaard
        public static Leader Usurper
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Usurper");
                self.setUnitAttributes(1, Tag.officer);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Create any Leader and boost it by 2.");

                return self;
            }
        }
        public static Leader EmhyrvarEmreis
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Emhyr var Emreis");
                self.setUnitAttributes(7, Tag.officer);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a card, then return a Bronze or Silver ally to your hand.");

                return self;
            }
        }
        public static Leader JanCalveit
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Jan Calveit");
                self.setUnitAttributes(5, Tag.officer);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at the top 3 cards from your deck, then play 1.");

                return self;
            }
        }
        public static Leader MorvranVoorhis
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Morvran Voorhis");
                self.setUnitAttributes(7, Tag.officer);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal up to 4 cards.");

                return self;
            }
        }

        // Monsters
        public static Leader EredinBreaccGlas
        {
            get
	        {
		        Leader self = new Leader();
		        self.setAttributes(Clan.monsters, Rarity.gold, "Eredin Breacc Glas");
		        self.setUnitAttributes(5, Tag.wildhunt);
		        self.setLeaderAttributes();
		
		        // Do not forget to check and RECHECK correctence of current ability,
		        // its triggering condition and signature of delegate!
		        self.setOnDeploy ((s, f) => 
		        {}, "Spawn a Bronze Wild Hunt unit.");
		
		        return self;
	        }
        }
        public static Leader UnseenElder
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.monsters, Rarity.gold, "Unseen Elder");
                self.setUnitAttributes(5, Tag.vampire);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Drain a unit by half.");

                return self;
            }
        }
        public static Leader WhisperingHillock
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.monsters, Rarity.gold, "Whispering Hillock");
                self.setUnitAttributes(6, Tag.relict);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Create a Bronze or Silver Organic card.");

                return self;
            }
        }
        public static Leader ArachasQueen
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.monsters, Rarity.gold, "Arachas Queen");
                self.setUnitAttributes(7, Tag.insectoid);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Immune.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume 3 allies and boost self by their power.");

                return self;
            }
        }
        public static Leader Dagon
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.monsters, Rarity.gold, "Dagon");
                self.setUnitAttributes(8, Tag.vodyanoi);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn Impenetrable Fog or Torrential Rain.");

                return self;
            }
        }

        // North
        public static Leader KingFoltest
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.northen, Rarity.gold, "King Foltest");
                self.setUnitAttributes(5, Tag.temeria);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost all other allies and your non-Spying units in hand and deck by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Leader KingHenselt
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.northen, Rarity.gold, "King Henselt");
                self.setUnitAttributes(3, Tag.kaedwen);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose a Bronze Machine or Kaedweni ally and play all copies of it from your deck.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Leader KingRadovidV
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.northen, Rarity.gold, "King Radovid V");
                self.setUnitAttributes(6, Tag.redania);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Toggle 2 units' Lock statuses. If enemies, deal 4 damage to them. Crew.");

                return self;
            }
        }
        public static Leader PrincessAdda
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.northen, Rarity.gold, "Princess Adda");
                self.setUnitAttributes(6, Tag.cursed);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Create a Bronze or Silver Cursed unit.");

                return self;
            }
        }

        // Scots
        public static Leader BrouverHoog
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Brouver Hoog");
                self.setUnitAttributes(4, Tag.dwarf);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a non-Spying Silver unit or a Bronze Dwarf from your deck.");

                return self;
            }
        }
        public static Leader Eithne
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Eithne");
                self.setUnitAttributes(5, Tag.dryad);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze or Silver special card.");

                return self;
            }
        }
        public static Leader Filavandrel
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Filavandrel");
                self.setUnitAttributes(4, Tag.elf);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Create a Silver special card.");

                return self;
            }
        }
        public static Leader FrancescaFindabair
        {
            get
            {
                Leader self = new Leader();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Francesca Findabair");
                self.setUnitAttributes(7, Tag.mage, Tag.elf);
                self.setLeaderAttributes();

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap a card with one of your choice and boost it by 3.");

                return self;
            }
        }
    }
}
