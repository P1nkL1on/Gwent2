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
        // < > GOLD
        // < > LEADERS
        // < > UNITS
        public static Unit CahirDyffryn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Cahir Dyffryn");
                self.setUnitAttributes(1, Tag.officer, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Leader.");

                return self;
            }
        }
        public static Unit LeoBonhart
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Leo Bonhart");
                self.setUnitAttributes(7, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal one of your units and deal damage equal to its base power to an enemy.");

                return self;
            }
        }
        public static Unit LethoofGulet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Letho of Gulet");
                self.setUnitAttributes(1, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Apply Lock status to 2 units on this row, then Drain all their power.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit LethoKingslayer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Letho: Kingslayer");
                self.setUnitAttributes(5, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose One: Destroy an enemy Leader, then boost self by 5; or Play a Bronze or Silver Tactic from your deck.");

                return self;
            }
        }
        public static Unit MennoCoehoorn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Menno Coehoorn");
                self.setUnitAttributes(8, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 4 damage to an enemy. If it's Spying, destroy it instead.");

                return self;
            }
        }
        public static Unit RainfarnofAttre
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Rainfarn of Attre");
                self.setUnitAttributes(5, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver Spying unit from your deck.");

                return self;
            }
        }
        public static Unit Shilard
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Shilard");
                self.setUnitAttributes(9, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Truce: Draw a card from both decks. Keep one and give the other to your opponent.");

                return self;
            }
        }
        public static Unit StefanSkellen
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Stefan Skellen");
                self.setUnitAttributes(10, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move any card in your deck to the top. If it's a non-Spying unit, boost it by 5.");

                return self;
            }
        }
        public static Unit TiborEggebracht
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Tibor Eggebracht");
                self.setUnitAttributes(10, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Truce: Boost self by 15, then your opponent draws a Revealed Bronze card.");

                return self;
            }
        }
        public static Unit VattierdeRideaux
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Vattier de Rideaux");
                self.setUnitAttributes(11, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal up to 2 of your cards, then Reveal the same number of your opponent's randomly.");

                return self;
            }
        }
        public static Unit Vilgefortz
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Vilgefortz");
                self.setUnitAttributes(9, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Destroy an ally, then play the top card of your deck; or Truce: Destroy an enemy, then your opponent draws and Reveals a Bronze card.");

                return self;
            }
        }
        public static Unit Xarthisius
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Xarthisius");
                self.setUnitAttributes(13, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at your opponent's deck and move a card to the bottom.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit YenneferEnchantress
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Yennefer: Enchantress");
                self.setUnitAttributes(5, Tag.mage, Tag.aedirn);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn the last Bronze or Silver Spell you played.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit YenneferNecromancer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.gold, "Yennefer: Necromancer");
                self.setUnitAttributes(5, Tag.mage, Tag.aedirn);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze or Silver Soldier from your opponent's graveyard.");

                return self;
            }
        }
        // < > SILVER
        public static Unit Albrich
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Albrich");
                self.setUnitAttributes(10, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Truce: Each player draws a card. The opponent's card is Revealed.");

                return self;
            }
        }
        public static Unit AssirevarAnahid
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Assire var Anahid");
                self.setUnitAttributes(11, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Return 2 Bronze or Silver cards from either graveyard to their respective decks.");

                return self;
            }
        }
        public static Unit Auckes
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Auckes");
                self.setUnitAttributes(7, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Toggle 2 units' Lock statuses.");

                return self;
            }
        }
        public static Unit Cantarella
        {
            get
	{
		Unit self = new Unit();
		self.setAttributes(Clan.nilfgaard, Rarity.silver, "Cantarella");
		self.setUnitAttributes(13);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Spying.");
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Single-Use: Draw 2 cards. Keep one and move the other to the bottom of your deck.");
		
		return self;
	}
        }
        public static Unit CeallachDyffryn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Ceallach Dyffryn");
                self.setUnitAttributes(2, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn an Ambassador, Assassin or Emissary.");

                return self;
            }
        }
        public static Unit Cynthia
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Cynthia");
                self.setUnitAttributes(5, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal the Highest unit in your opponent's hand and boost self by its power.");

                return self;
            }
        }
        public static Unit FalseCiri
        {
            get
	{
		Unit self = new Unit();
		self.setAttributes(Clan.nilfgaard, Rarity.silver, "False Ciri");
		self.setUnitAttributes(8);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Spying. If Spying, boost self by 1 on turn start and when this player passes, move to the opposite row.");
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Deathwish: Destroy the Lowest unit on the row.");
		
		return self;
	}
        }
        // Suggest anything to improove this card!
        public static Unit FringillaVigo
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Fringilla Vigo");
                self.setUnitAttributes(1, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Copy the power from the unit to the left to the unit to the right.");

                return self;
            }
        }
        public static Unit HenryvarAttre
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Henry var Attre");
                self.setUnitAttributes(9, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Conceal any number of units. If allies, boost by 2. If enemies, deal 2 damage.");

                return self;
            }
        }
        public static Unit JoachimdeWett
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Joachim de Wett");
                self.setUnitAttributes(5, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play the top non-Spying Bronze or Silver unit from your deck and boost it by 10.");

                return self;
            }
        }
        public static Unit PeterSaarGwynleve
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Peter Saar Gwynleve");
                self.setUnitAttributes(6, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reset an ally and Strengthen it by 3; or Reset an enemy and Weaken it by 3.");

                return self;
            }
        }
        public static Unit Serrit
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Serrit");
                self.setUnitAttributes(6, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 7 damage to an enemy; or Set a Revealed opposing unit's power to 1.");

                return self;
            }
        }
        public static Unit Sweers
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Sweers");
                self.setUnitAttributes(9, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose an enemy or a Revealed unit in your opponent's hand, then move all copies of it from their deck to the graveyard.");

                return self;
            }
        }
        public static Unit TheGuardian
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "The Guardian");
                self.setUnitAttributes(11, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Add a Lesser Guardian to the top of your opponent's deck.");

                return self;
            }
        }
        public static Unit Vreemde
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Vreemde");
                self.setUnitAttributes(4, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Create a Bronze Nilfgaardian Soldier.");

                return self;
            }
        }
        public static Unit Vrygheff
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Vrygheff");
                self.setUnitAttributes(5, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze Machine from your deck.");

                return self;
            }
        }
        // < > BRONZE
        public static Unit AlbaArmoredCavalry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Alba Armored Cavalry");
                self.setUnitAttributes(7, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever an ally appears, boost self by 1.");

                return self;
            }
        }
        public static Unit AlbaPikeman
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Alba Pikeman");
                self.setUnitAttributes(3, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon all copies of this unit to this row.");

                return self;
            }
        }
        public static Unit AlbaSpearman
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Alba Spearman");
                self.setUnitAttributes(10, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 1 whenever either player draws a card.");

                return self;
            }
        }
        public static Unit Alchemist
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Alchemist");
                self.setUnitAttributes(9, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal 2 cards.");

                return self;
            }
        }
        public static Unit Assassin
        {
            get
	{
		Unit self = new Unit();
		self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Assassin");
		self.setUnitAttributes(1);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Spying.");
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Deal 10 damage to the unit to the left.");
		
		return self;
	}
        }
        public static Unit CombatEngineer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Combat Engineer");
                self.setUnitAttributes(6, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost an ally by 5.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit DaerlanSoldier
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Daerlan Soldier");
                self.setUnitAttributes(4, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you Reveal this unit, play it automatically on a random row and draw a card.");

                return self;
            }
        }
        public static Unit DeithwenArbalest
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Deithwen Arbalest");
                self.setUnitAttributes(7, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage to an enemy. If it's Spying, deal 6 damage instead.");

                return self;
            }
        }
        public static Unit ImperaBrigade
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Impera Brigade");
                self.setUnitAttributes(6, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 2 for each Spying enemy. Boost self by 2 whenever a Spying enemy appears.");

                return self;
            }
        }
        public static Unit ImperaEnforcers
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Impera Enforcers");
                self.setUnitAttributes(6, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "For each Spying enemy that appears during your turn, deal 2 damage to an enemy on turn end.");

                return self;
            }
        }
        public static Unit Infiltrator
        {
            get
	{
		Unit self = new Unit();
		self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Infiltrator");
		self.setUnitAttributes(10);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		self.setOnDeploy ((s, f) => 
		{}, "Toggle a unit's Spying status.");
		
		return self;
	}
        }
        public static Unit MagneDivision
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Magne Division");
                self.setUnitAttributes(3, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a random Bronze Item from your deck.");

                return self;
            }
        }
        public static Unit MasterofDisguise
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Master of Disguise");
                self.setUnitAttributes(11, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Conceal 2 cards.");

                return self;
            }
        }
        public static Unit NauzicaaBrigade
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Nauzicaa Brigade");
                self.setUnitAttributes(5, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 7 damage to a Spying unit. If it was destroyed, Strengthen self by 4.");

                return self;
            }
        }
        public static Unit NauzicaaSergeant
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Nauzicaa Sergeant");
                self.setUnitAttributes(7, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Clear Hazards from its row and boost an ally or a Revealed unit in your hand by 3.");

                return self;
            }
        }
        public static Unit NilfgaardianKnight
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Nilfgaardian Knight");
                self.setUnitAttributes(12, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "2 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reveal a random card in your hand, with priority given to Bronze, then Silver, then Gold.");

                return self;
            }
        }
        public static Unit Recruit
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Recruit");
                self.setUnitAttributes(1, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a random different Bronze Soldier from your deck.");

                return self;
            }
        }
        public static Unit SlaveDriver
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Slave Driver");
                self.setUnitAttributes(10, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Set an ally's power to 1 and deal damage to an enemy by the amount of power lost.");

                return self;
            }
        }
        public static Unit SlaveHunter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Slave Hunter");
                self.setUnitAttributes(8, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Charm a Bronze enemy with 3 power or less.");

                return self;
            }
        }
        public static Unit SlaveInfantry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Slave Infantry");
                self.setUnitAttributes(3, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn a Doomed default copy of this unit on your other rows.");

                return self;
            }
        }
        public static Unit Spotter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Spotter");
                self.setUnitAttributes(5, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by the base power of a Revealed Bronze or Silver unit.");

                return self;
            }
        }
        public static Unit StandardBearer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Standard Bearer");
                self.setUnitAttributes(8, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost an ally by 2 whenever you play a Soldier.");

                return self;
            }
        }
        public static Unit VenendalElite
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Venendal Elite");
                self.setUnitAttributes(1, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Switch this unit's power with that of a Revealed unit.");

                return self;
            }
        }
        public static Unit VicovaroMedic
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Vicovaro Medic");
                self.setUnitAttributes(1, Tag.support, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze unit from your opponent's graveyard.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit VicovaroNovice
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Vicovaro Novice");
                self.setUnitAttributes(2, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at 2 random Bronze Alchemy cards from your deck, then play 1.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit ViperWitcher
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Viper Witcher");
                self.setUnitAttributes(5, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage for each Alchemy card in your starting deck.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Special Treason
        {
            get
	{
		Special spec = new Special();
		spec.setAttributes(Clan.nilfgaard, Rarity.silver, "Treason");
		spec.setSpecialAttributes(Tag.tactic);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		spec.setOnDeploy ((s, f) => 
		{}, "Force 2 adjacent enemies to Duel each other.");
		
		return spec;
	}
        }
        // < > BRONZE
        // Suggest anything to improove this card!
        public static Special Ointment
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.nilfgaard, Rarity.bronze, "Ointment");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                spec.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze unit with 5 power or less.");

                return spec;
            }
        }
        // < > UNITS
        // < > SILVER
        public static Unit HeftyHelge
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.silver, "Hefty Helge");
                self.setUnitAttributes(8, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage to all enemies except those on the opposite row. If this unit was Revealed, deal damage to all enemies instead.");

                return self;
            }
        }
        // < > BRONZE
        public static Unit FireScorpion
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Fire Scorpion");
                self.setUnitAttributes(5, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 5 damage to an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you Reveal this unit, trigger its ability.");

                return self;
            }
        }
        public static Unit ImperialGolem
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Imperial Golem");
                self.setUnitAttributes(3, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon a copy of this unit to a random row whenever you Reveal a card in your opponent's hand.");

                return self;
            }
        }
        public static Unit Mangonel
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Mangonel");
                self.setUnitAttributes(7, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to a random enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Repeat this ability whenever you Reveal a card.");

                return self;
            }
        }
        public static Unit RotTosser
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Rot Tosser");
                self.setUnitAttributes(8, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn a Cow Carcass on an enemy row.");

                return self;
            }
        }
        public static Unit Sentry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.nilfgaard, Rarity.bronze, "Sentry");
                self.setUnitAttributes(8, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost all allied copies of a Soldier by 2.");

                return self;
            }
        }

    }
}
