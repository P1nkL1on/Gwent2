using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {// < > GOLD
        public static Unit BrewessRitual
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Brewess: Ritual");
                self.setUnitAttributes(1, Tag.mage, Tag.doomed, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect 2 Bronze Deathwish units.");

                return self;
            }
        }
        public static Unit CaranthirArFeiniel
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Caranthir Ar-Feiniel");
                self.setUnitAttributes(9, Tag.wildhunt, Tag.mage, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move an enemy to the row opposite this unit and apply Biting Frost to that row.");

                return self;
            }
        }
        public static Unit Draug
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Draug");
                self.setUnitAttributes(10, Tag.cursed, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect units as 1-power Draugirs until you fill this row.");

                return self;
            }
        }
        public static Unit Geels
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Ge'els");
                self.setUnitAttributes(1, Tag.wildhunt, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at a random Gold and Silver card from your deck, then play 1 and move the other to the top of the deck.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit Imlerith
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Imlerith");
                self.setUnitAttributes(9, Tag.wildhunt, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 4 damage to an enemy. If the enemy is under Biting Frost, deal 8 damage instead.");

                return self;
            }
        }
        public static Unit ImlerithSabbath
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Imlerith: Sabbath");
                self.setUnitAttributes(5, Tag.wildhunt, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Every turn, Duel the Highest enemy on turn end. If this unit survives, Heal it by 2 and give it 2 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "2 Armor.");

                return self;
            }
        }
        public static Unit Miruna
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Miruna");
                self.setUnitAttributes(4, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "After 2 turns, Charm the Highest enemy on the opposite row on turn start.");

                return self;
            }
        }
        public static Unit WeavessIncantation
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Weavess: Incantation");
                self.setUnitAttributes(4, Tag.mage, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose One: Strengthen all your other Relicts in hand, deck, and on board by 2; or Play a Bronze or Silver Relict from your deck and Strengthen it by 2.");

                return self;
            }
        }
        public static Unit WhispessTribute
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Whispess: Tribute");
                self.setUnitAttributes(6, Tag.mage, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver Organic card from your deck.");

                return self;
            }
        }
        public static Unit Caretaker
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Caretaker");
                self.setUnitAttributes(4, Tag.doomed, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze or Silver unit from your opponent's graveyard.");

                return self;
            }
        }
        public static Unit Kayran
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Kayran");
                self.setUnitAttributes(5, Tag.insectoid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume a unit with 7 power or less and boost self by its power.");

                return self;
            }
        }
        public static Unit OldSpeartip
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Old Speartip");
                self.setUnitAttributes(10, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to 5 random enemies on the opposite row.");

                return self;
            }
        }
        public static Unit OldSpeartipAsleep
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Old Speartip: Asleep");
                self.setUnitAttributes(12, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Strengthen all your other Ogroids in hand, deck, and on board by 1.");

                return self;
            }
        }
        public static Unit WoodlandSpirit
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.gold, "Woodland Spirit");
                self.setUnitAttributes(5, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn 3 Wolves on the melee row and apply Impenetrable Fog to the opposite row.");

                return self;
            }
        }
        // < > SILVER
        public static Unit Brewess
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Brewess");
                self.setUnitAttributes(8, Tag.mage, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon Whispess and Weavess to this row.");

                return self;
            }
        }
        public static Unit Nithral
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Nithral");
                self.setUnitAttributes(6, Tag.wildhunt, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 6 damage to an enemy. Increase damage by 1 for each Wild Hunt unit in your hand.");

                return self;
            }
        }
        public static Unit SheTrollofVergen
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "She-Troll of Vergen");
                self.setUnitAttributes(1, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze Deathwish unit from your deck, Consume it and boost self by its base power.");

                return self;
            }
        }
        public static Unit Weavess
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Weavess");
                self.setUnitAttributes(6, Tag.mage, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon Brewess and Whispess to this row.");

                return self;
            }
        }
        public static Unit Whispess
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Whispess");
                self.setUnitAttributes(6, Tag.mage, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon Brewess and Weavess to this row.");

                return self;
            }
        }
        public static Unit AddaStriga
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Adda: Striga");
                self.setUnitAttributes(6, Tag.cursed, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 8 damage to a non-Monster faction unit.");

                return self;
            }
        }
        public static Unit Frightener
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Frightener");
                self.setUnitAttributes(13, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Single-Use: Move an enemy to this row and draw a card.");

                return self;
            }
        }
        public static Unit Golyat
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Golyat");
                self.setUnitAttributes(10, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 7.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever this unit is damaged, deal 2 damage to self.");

                return self;
            }
        }
        public static Unit Ifrit
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Ifrit");
                self.setUnitAttributes(8, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn 3 Lesser Ifrits to the right of this unit.");

                return self;
            }
        }
        public static Unit ImperialManticore
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Imperial Manticore");
                self.setUnitAttributes(13, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "No ability.");

                return self;
            }
        }
        public static Unit Jotunn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Jotunn");
                self.setUnitAttributes(6, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move 3 enemies to the row opposite this unit and deal 2 damage to them. If that row is under Biting Frost, deal 3 damage instead.");

                return self;
            }
        }
        public static Unit Maerolorn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Maerolorn");
                self.setUnitAttributes(4, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze Deathwish unit from your deck.");

                return self;
            }
        }
        public static Unit Morvudd
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Morvudd");
                self.setUnitAttributes(6, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Toggle a unit's Lock status. If it was an enemy, halve its power.");

                return self;
            }
        }
        public static Unit Mourntart
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Mourntart");
                self.setUnitAttributes(5, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume all Bronze and Silver units in your graveyard and boost self by 1 for each.");

                return self;
            }
        }
        public static Unit Nekurat
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Nekurat");
                self.setUnitAttributes(5, Tag.vampire);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn Moonlight.");

                return self;
            }
        }
        public static Unit Ozzrel
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Ozzrel");
                self.setUnitAttributes(5, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume a Bronze or Silver unit from either graveyard and boost by its power.");

                return self;
            }
        }
        public static Unit Ruehin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Ruehin");
                self.setUnitAttributes(8, Tag.insectoid, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Strengthen all your other Insectoids and Cursed units in hand, deck, and on board by 1.");

                return self;
            }
        }
        public static Unit ToadPrince
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.silver, "Toad Prince");
                self.setUnitAttributes(6, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Draw a unit, then Consume a unit in your hand and boost self by its power.");

                return self;
            }
        }
        // < > BRONZE
        public static Unit IceGiant
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Ice Giant");
                self.setUnitAttributes(6, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost by 7 if Biting Frost is anywhere on the board.");

                return self;
            }
        }
        public static Unit Nekker
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Nekker");
                self.setUnitAttributes(4, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If in hand, deck, or on board, boost self by 1 whenever you Consume a card.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deathwish: Summon a copy of this unit to the same position.");

                return self;
            }
        }
        public static Unit NekkerWarrior
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Nekker Warrior");
                self.setUnitAttributes(9, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose a Bronze ally and add 2 copies of it to the bottom of your deck.");

                return self;
            }
        }
        public static Unit Siren
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Siren");
                self.setUnitAttributes(4, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play Moonlight from your deck.");

                return self;
            }
        }
        public static Unit WildHuntNavigator
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wild Hunt Navigator");
                self.setUnitAttributes(3, Tag.wildhunt, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose a Bronze non-Mage Wild Hunt ally and play a copy of it from your deck.");

                return self;
            }
        }
        public static Unit WildHuntRider
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wild Hunt Rider");
                self.setUnitAttributes(10, Tag.wildhunt, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Increase the damage dealt by Biting Frost on the opposite row by 1.");

                return self;
            }
        }
        public static Unit WildHuntWarrior
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wild Hunt Warrior");
                self.setUnitAttributes(7, Tag.wildhunt, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage to an enemy. If the enemy is destroyed or is under Biting Frost, boost self by 2.");

                return self;
            }
        }
        public static Unit AlphaWerewolf
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Alpha Werewolf");
                self.setUnitAttributes(10, Tag.beast, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn a Wolf on each side of this unit on contact with Full Moon.");

                return self;
            }
        }
        public static Unit AncientFoglet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Ancient Foglet");
                self.setUnitAttributes(10, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost by 1 if Impenetrable Fog is on the board on turn end.");

                return self;
            }
        }
        public static Unit ArachasBehemoth
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Arachas Behemoth");
                self.setUnitAttributes(8, Tag.insectoid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "The next 4 times you Consume a unit, Spawn an Arachas Hatchling on a random row.");

                return self;
            }
        }
        public static Unit ArachasDrone
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Arachas Drone");
                self.setUnitAttributes(3, Tag.insectoid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon all copies of this unit to this row.");

                return self;
            }
        }
        public static Unit Archespore
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Archespore");
                self.setUnitAttributes(7, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move to a random row and deal 1 damage to a random enemy on turn start.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deathwish: Deal 4 damage to a random enemy.");

                return self;
            }
        }
        public static Unit Archgriffin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Archgriffin");
                self.setUnitAttributes(10, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Clear Hazards on its row.");

                return self;
            }
        }
        public static Unit Barbegazi
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Barbegazi");
                self.setUnitAttributes(6, Tag.insectoid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resilience.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume an ally and boost self by its power.");

                return self;
            }
        }
        public static Unit BridgeTroll
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Bridge Troll");
                self.setUnitAttributes(10, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move a Hazard on an enemy row to a different enemy row.");

                return self;
            }
        }
        public static Unit CelaenoHarpy
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Celaeno Harpy");
                self.setUnitAttributes(6, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn 2 Harpy Eggs to the left of this unit.");

                return self;
            }
        }
        public static Unit Cockatrice
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Cockatrice");
                self.setUnitAttributes(6, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reset a unit.");

                return self;
            }
        }
        public static Unit Cyclops
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Cyclops");
                self.setUnitAttributes(11, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Destroy an ally and deal damage equal to its power to an enemy.");

                return self;
            }
        }
        public static Unit Dao
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "D'ao");
                self.setUnitAttributes(6, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deathwish: Spawn 2 Lesser D'ao on this row.");

                return self;
            }
        }
        public static Unit Drowner
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Drowner");
                self.setUnitAttributes(7, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move an enemy to the row opposite this unit and deal 2 damage to it. If that row is under a Hazard, deal 4 damage instead.");

                return self;
            }
        }
        public static Unit Ekimmara
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Ekimmara");
                self.setUnitAttributes(5, Tag.vampire);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Drain a unit by 3.");

                return self;
            }
        }
        public static Unit Fiend
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Fiend");
                self.setUnitAttributes(11, Tag.relict);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "No ability.");

                return self;
            }
        }
        public static Unit Foglet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Foglet");
                self.setUnitAttributes(4, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you apply Impenetrable Fog to an enemy row, Summon a copy of this unit to the opposite row.");

                return self;
            }
        }
        public static Unit Forktail
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Forktail");
                self.setUnitAttributes(8, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume 2 allies and boost self by their power.");

                return self;
            }
        }
        public static Unit Ghoul
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Ghoul");
                self.setUnitAttributes(4, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume a Bronze or Silver unit from your graveyard and boost self by its power.");

                return self;
            }
        }
        public static Unit Griffin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Griffin");
                self.setUnitAttributes(9, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Trigger the Deathwish of a Bronze ally.");

                return self;
            }
        }
        public static Unit Harpy
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Harpy");
                self.setUnitAttributes(4, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you destroy an allied Beast, Summon a copy of this unit to the same position.");

                return self;
            }
        }
        public static Unit IceTroll
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Ice Troll");
                self.setUnitAttributes(4, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Duel an enemy. If it's under Biting Frost, deal double damage.");

                return self;
            }
        }
        public static Unit Lamia
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Lamia");
                self.setUnitAttributes(6, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 4 damage to an enemy. If the enemy is under Blood Moon, deal 7 damage instead.");

                return self;
            }
        }
        public static Unit Rotfiend
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Rotfiend");
                self.setUnitAttributes(8, Tag.necrophage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deathwish: Deal 2 damage to units on the opposite row.");

                return self;
            }
        }
        public static Unit Slyzard
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Slyzard");
                self.setUnitAttributes(2, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume a different Bronze unit from your graveyard, then play a copy of it from your deck.");

                return self;
            }
        }
        public static Unit VranWarrior
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Vran Warrior");
                self.setUnitAttributes(6, Tag.soldier, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Consume the unit to the right and boost self by its power.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Every 2 turns, repeat its ability on turn start.");

                return self;
            }
        }
        public static Unit Werecat
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Werecat");
                self.setUnitAttributes(5, Tag.beast, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 5 damage to an enemy, then deal 1 damage to all enemies under Blood Moon.");

                return self;
            }
        }
        public static Unit Werewolf
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Werewolf");
                self.setUnitAttributes(7, Tag.beast, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Immune.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost this unit by 7 on first contact with Full Moon.");

                return self;
            }
        }
        public static Unit WildHuntDrakkar
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wild Hunt Drakkar");
                self.setUnitAttributes(7, Tag.wildhunt, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost all Wild Hunt allies by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever another Wild Hunt ally appears, boost it by 1.");

                return self;
            }
        }
        public static Unit WildHuntHound
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wild Hunt Hound");
                self.setUnitAttributes(4, Tag.wildhunt, Tag.construct);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play Biting Frost from your deck.");

                return self;
            }
        }
        public static Unit Wyvern
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.monsters, Rarity.bronze, "Wyvern");
                self.setUnitAttributes(6, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 5 damage to an enemy.");

                return self;
            }
        }
        // < > SPECIALS
        // < > SILVER

    }
}
