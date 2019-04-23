using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
        public static Unit Aglais
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Aglais");
                self.setUnitAttributes(8, Tag.dryad);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze or Silver special card from your opponent's graveyard, then Banish it.");

                return self;
            }
        }
        public static Unit Iorveth
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Iorveth");
                self.setUnitAttributes(6, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 8 damage to an enemy. If the unit was destroyed, boost all Elves in your hand by 1.");

                return self;
            }
        }
        public static Unit IorvethMeditation
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Iorveth: Meditation");
                self.setUnitAttributes(2, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Force 2 enemies on the same row to Duel each other.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit IsengrimFaoiltiarna
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Isengrim Faoiltiarna");
                self.setUnitAttributes(7, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver Ambush from your deck.");

                return self;
            }
        }
        public static Unit IsengrimOutlaw
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Isengrim: Outlaw");
                self.setUnitAttributes(2, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose One: Play a Bronze or Silver special card from your deck; or Create a Silver Elf.");

                return self;
            }
        }
        public static Unit IthlinneAegli
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Ithlinne Aegli");
                self.setUnitAttributes(2, Tag.mage, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze Spell, Boon or Hazard from your deck twice.");

                return self;
            }
        }
        public static Unit Milva
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Milva");
                self.setUnitAttributes(6, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Return each player's Highest Bronze or Silver unit to their deck.");

                return self;
            }
        }
        public static Unit MorennForestChild
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Morenn: Forest Child");
                self.setUnitAttributes(6, Tag.dryad);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Ambush: When your opponent plays a Bronze or Silver special card, flip over and cancel its ability.");

                return self;
            }
        }
        public static Unit Saskia
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Saskia");
                self.setUnitAttributes(11, Tag.aedirn, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap up to 2 cards for Bronze cards.");

                return self;
            }
        }
        public static Unit Schirru
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Schirru");
                self.setUnitAttributes(4, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn Scorch or Epidemic.");

                return self;
            }
        }
        public static Unit XavierMoran
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Xavier Moran");
                self.setUnitAttributes(10, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost this unit by the default power of the last other Dwarf you played.");

                return self;
            }
        }
        public static Unit ZoltanChivay
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Zoltan Chivay");
                self.setUnitAttributes(8, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose 3 units. Strengthen allies by 2 and move them to this row. Deal 2 damage to enemies and move them to the row opposite this unit.");

                return self;
            }
        }
        public static Unit Saesenthessis
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.gold, "Saesenthessis");
                self.setUnitAttributes(10, Tag.aedirn, Tag.draconid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 1 for each Dwarf ally and deal 1 damage to an enemy for each Elf ally.");

                return self;
            }
        }
        // < > SILVER
        public static Unit Aelirenn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Aelirenn");
                self.setUnitAttributes(6, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If 5 Elf allies are on the board on any turn end, Summon this unit to a random row.");

                return self;
            }
        }
        public static Unit BarclayEls
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Barclay Els");
                self.setUnitAttributes(2, Tag.dwarf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a random Bronze or Silver Dwarf from your deck and Strengthen it by 3.");

                return self;
            }
        }
        public static Unit Braenn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Braenn");
                self.setUnitAttributes(6, Tag.dryad);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal damage equal to this unit's power. If a unit was destroyed, boost all your other Dryads and Ambush units in hand, deck, and on board by 1.");

                return self;
            }
        }
        public static Unit CiaranaepEasnillen
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Ciaran aep Easnillen");
                self.setUnitAttributes(9, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Toggle a unit's Lock status and move it to this row on its side.");

                return self;
            }
        }
        public static Unit DennisCranmer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Dennis Cranmer");
                self.setUnitAttributes(8, Tag.dwarf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Strengthen all your other Dwarves in hand, deck, and on board by 1.");

                return self;
            }
        }
        public static Unit Eleyas
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Ele'yas");
                self.setUnitAttributes(10, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you draw this unit or return it to your deck, boost self by 2.");

                return self;
            }
        }
        public static Unit Malena
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Malena");
                self.setUnitAttributes(7, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Ambush: After 2 turns, flip over and Charm the Highest Bronze or Silver enemy with 5 power or less on turn start.");

                return self;
            }
        }
        public static Unit Milaen
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Milaen");
                self.setUnitAttributes(4, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 6 damage to the units at the end of a row.");

                return self;
            }
        }
        public static Unit Morenn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Morenn");
                self.setUnitAttributes(8, Tag.dryad);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Ambush: When a unit is played on your opponent's side, flip over and deal 7 damage to it.");

                return self;
            }
        }
        public static Unit PavkoGale
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Pavko Gale");
                self.setUnitAttributes(5, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver Item from your deck.");

                return self;
            }
        }
        public static Unit SheldonSkaggs
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Sheldon Skaggs");
                self.setUnitAttributes(9, Tag.dwarf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move all allies on this row to random rows and boost self by 1 for each.");

                return self;
            }
        }
        public static Unit Toruviel
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Toruviel");
                self.setUnitAttributes(6, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Ambush: When your opponent passes, flip over and boost 2 units on each side by 2.");

                return self;
            }
        }
        public static Unit Yaevinn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Yaevinn");
                self.setUnitAttributes(13, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Single-Use: Draw a special card and a unit. Keep one and return the other to your deck.");

                return self;
            }
        }
        public static Unit YarpenZigrin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Yarpen Zigrin");
                self.setUnitAttributes(8, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resilience.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever a Dwarf ally appears, boost self by 1.");

                return self;
            }
        }
        public static Unit EibhearHattori
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Eibhear Hattori");
                self.setUnitAttributes(3, Tag.elf, Tag.support, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a lower or equal Bronze or Silver Scoia'tael unit.");

                return self;
            }
        }
        public static Unit PaulieDahlberg
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.silver, "Paulie Dahlberg");
                self.setUnitAttributes(3, Tag.support, Tag.dwarf, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a non-Support Bronze Dwarf.");

                return self;
            }
        }
        // < > BRONZE
        public static Unit BlueMountainElite
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Blue Mountain Elite");
                self.setUnitAttributes(3, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon all copies of this unit to this row.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever this unit moves, boost it by 2.");

                return self;
            }
        }
        public static Unit DolBlathannaArcher
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dol Blathanna Archer");
                self.setUnitAttributes(7, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage, then deal 1 damage.");

                return self;
            }
        }
        public static Unit DolBlathannaBomber
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dol Blathanna Bomber");
                self.setUnitAttributes(6, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn an Incinerating Trap on an enemy row.");

                return self;
            }
        }
        public static Unit DolBlathannaBowman
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dol Blathanna Bowman");
                self.setUnitAttributes(7, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever an enemy moves, deal 2 damage to it.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever this unit moves, deal 2 damage to a random enemy.");

                return self;
            }
        }
        public static Unit DolBlathannaSentry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dol Blathanna Sentry");
                self.setUnitAttributes(2, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If in hand, deck or on board, boost self by 1 whenever you play a special card.");

                return self;
            }
        }
        public static Unit DwarvenAgitator
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dwarven Agitator");
                self.setUnitAttributes(1, Tag.support, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn a default copy of a random different Bronze Dwarf from your deck.");

                return self;
            }
        }
        public static Unit DwarvenMercenary
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dwarven Mercenary");
                self.setUnitAttributes(8, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move a unit to this row on its side. If it's an ally, boost it by 3.");

                return self;
            }
        }
        public static Unit DwarvenSkirmisher
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Dwarven Skirmisher");
                self.setUnitAttributes(6, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage to an enemy. If the unit was not destroyed, boost self by 3.");

                return self;
            }
        }
        public static Unit ElvenMercenary
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Elven Mercenary");
                self.setUnitAttributes(1, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at 2 random Bronze special cards from your deck, then play 1.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit ElvenScout
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Elven Scout");
                self.setUnitAttributes(10, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap a card.");

                return self;
            }
        }
        public static Unit ElvenSwordmaster
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Elven Swordmaster");
                self.setUnitAttributes(5, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal damage equal to this unit's power to an enemy.");

                return self;
            }
        }
        public static Unit Farseer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Farseer");
                self.setUnitAttributes(8, Tag.mage, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If any of your other allies or units in hand are boosted during your turn, boost self by 2 on turn end.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit HalfElfHunter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Half-Elf Hunter");
                self.setUnitAttributes(6, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn a Doomed default copy of this unit to the right of this unit.");

                return self;
            }
        }
        public static Unit HawkerHealer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Hawker Healer");
                self.setUnitAttributes(5, Tag.elf, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost 2 allies by 3.");

                return self;
            }
        }
        public static Unit HawkerSmuggler
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Hawker Smuggler");
                self.setUnitAttributes(7, Tag.elf, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever an enemy appears, boost self by 1.");

                return self;
            }
        }
        public static Unit HawkerSupport
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Hawker Support");
                self.setUnitAttributes(7, Tag.elf, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost a unit in your hand by 3.");

                return self;
            }
        }
        public static Unit MahakamDefender
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Mahakam Defender");
                self.setUnitAttributes(6, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resilience.");

                return self;
            }
        }
        public static Unit MahakamGuard
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Mahakam Guard");
                self.setUnitAttributes(4, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost an ally by 7.");

                return self;
            }
        }
        public static Unit MahakamMarauder
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Mahakam Marauder");
                self.setUnitAttributes(7, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever this unit's power changes, except when Reset, boost self by 2.");

                return self;
            }
        }
        public static Unit MahakamVolunteers
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Mahakam Volunteers");
                self.setUnitAttributes(3, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon all copies of this unit.");

                return self;
            }
        }
        public static Unit Pyrotechnician
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Pyrotechnician");
                self.setUnitAttributes(5, Tag.soldier, Tag.dwarf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage to a random enemy on each row.");

                return self;
            }
        }
        public static Unit Sage
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Sage");
                self.setUnitAttributes(2, Tag.mage, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze Alchemy or Spell card, then Banish it.");

                return self;
            }
        }
        public static Unit VriheddBrigade
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Brigade");
                self.setUnitAttributes(9, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Clear Hazards from its row and move a unit to this row on its side.");

                return self;
            }
        }
        public static Unit VriheddDragoon
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Dragoon");
                self.setUnitAttributes(8, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost a random non-Spying unit in your hand by 1 on turn end.");

                return self;
            }
        }
        public static Unit VriheddNeophyte
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Neophyte");
                self.setUnitAttributes(10, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost 2 random units in your hand by 1.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit VriheddOfficer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Officer");
                self.setUnitAttributes(5, Tag.elf, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap a card and boost self by its base power.");

                return self;
            }
        }
        public static Unit VriheddSappers
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Sappers");
                self.setUnitAttributes(11, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Ambush: After 2 turns, flip over on turn start.");

                return self;
            }
        }
        public static Unit VriheddVanguard
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Vrihedd Vanguard");
                self.setUnitAttributes(6, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost Elf allies by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you Swap this card, trigger its ability.");

                return self;
            }
        }
        public static Unit Wardancer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Wardancer");
                self.setUnitAttributes(3, Tag.soldier, Tag.elf);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever you Swap this unit, play it automatically on a random row.");

                return self;
            }
        }
        public static Unit Panther
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.scoetaels, Rarity.bronze, "Panther");
                self.setUnitAttributes(4, Tag.beast);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 7 damage to an enemy on a row with less than 4 units.");

                return self;
            }
        }
        

    }
}
