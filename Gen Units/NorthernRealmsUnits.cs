using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
        public static Unit BloodyBaron
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Bloody Baron");
                self.setUnitAttributes(10, Tag.temeria, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If in hand, deck, or on board, boost self by 1 whenever an enemy is destroyed.");

                return self;
            }
        }
        public static Unit Dandelion
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Dandelion");
                self.setUnitAttributes(11, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost 3 units in your deck by 2.");

                return self;
            }
        }
        public static Unit JohnNatalis
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "John Natalis");
                self.setUnitAttributes(6, Tag.temeria, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver Tactic from your deck.");

                return self;
            }
        }
        public static Unit KeiraMetz
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Keira Metz");
                self.setUnitAttributes(6, Tag.mage, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn Alzur's Thunder, Thunderbolt or Arachas Venom.");

                return self;
            }
        }
        public static Unit Kiyan
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Kiyan");
                self.setUnitAttributes(4, Tag.cursed, Tag.witcher);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose One: Create a Bronze or Silver Alchemy card; or Play a Bronze or Silver Item from your deck.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit PhilippaEilhart
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Philippa Eilhart");
                self.setUnitAttributes(1, Tag.mage, Tag.redania);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 6 damage to an enemy, then deal 5, 4, 3, 2 and 1 damage to random enemies. Cannot damage the same enemy twice in a row.");

                return self;
            }
        }
        public static Unit Priscilla
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Priscilla");
                self.setUnitAttributes(3, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost 5 random allies by 3.");

                return self;
            }
        }
        // Suggest anything to improove this card!
        public static Unit RocheMerciless
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Roche: Merciless");
                self.setUnitAttributes(6, Tag.temeria, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Destroy a face-down Ambush enemy.");

                return self;
            }
        }
        public static Unit SeltkirkofGulet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Seltkirk of Gulet");
                self.setUnitAttributes(8, Tag.cursed, Tag.aedirn, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Duel an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "3 Armor.");

                return self;
            }
        }
        public static Unit Shani
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Shani");
                self.setUnitAttributes(4, Tag.redania, Tag.support, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a non-Cursed Bronze or Silver unit and give it 2 Armor.");

                return self;
            }
        }
        public static Unit SigismundDijkstra
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Sigismund Dijkstra");
                self.setUnitAttributes(4, Tag.redania);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play the top 2 cards from your deck.");

                return self;
            }
        }
        public static Unit Vandergrift
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Vandergrift");
                self.setUnitAttributes(7, Tag.cursed, Tag.kaedwen, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage to all enemies. If a unit is destroyed, apply Ragh Nar Roog to its row.");

                return self;
            }
        }
        public static Unit VernonRoche
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.gold, "Vernon Roche");
                self.setUnitAttributes(3, Tag.temeria, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 7 damage to an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "At game start, add a Blue Stripe Commando to your deck.");

                return self;
            }
        }
        // < > SILVER
        public static Unit HubertRejk
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Hubert Rejk");
                self.setUnitAttributes(7, Tag.vampire);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Drain all boosts from units in your deck.");

                return self;
            }
        }
        public static Unit MargaritaofAretuza
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Margarita of Aretuza");
                self.setUnitAttributes(6, Tag.mage, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reset a unit and toggle its Lock status.");

                return self;
            }
        }
        public static Unit Nenneke
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Nenneke");
                self.setUnitAttributes(10, Tag.support, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Return 3 Bronze or Silver units from the graveyard to your deck.");

                return self;
            }
        }
        public static Unit Odrin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Odrin");
                self.setUnitAttributes(8, Tag.soldier, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Move to a random row and boost all other allies on it by 1 on turn start.");

                return self;
            }
        }
        public static Unit PrinceStennis
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Prince Stennis");
                self.setUnitAttributes(3, Tag.aedirn, Tag.officer);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play the top non-Spying Bronze or Silver unit from your deck and give it 5 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "3 Armor.");

                return self;
            }
        }
        public static Unit PrincessPavetta
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Princess Pavetta");
                self.setUnitAttributes(3, Tag.mage, Tag.cintra);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Return each player's Lowest Bronze or Silver unit to their deck.");

                return self;
            }
        }
        public static Unit RonvidtheIncessant
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Ronvid the Incessant");
                self.setUnitAttributes(11, Tag.soldier, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect on a random row with 1 power on turn end.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit SabrinaGlevissig
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Sabrina Glevissig");
                self.setUnitAttributes(3, Tag.mage, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");

                return self;
            }
        }
        public static Unit SabrinasSpecter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Sabrina's Specter");
                self.setUnitAttributes(3, Tag.mage, Tag.cursed, Tag.doomed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Resurrect a Bronze Cursed unit.");

                return self;
            }
        }
        public static Unit SiledeTansarville
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Sile de Tansarville");
                self.setUnitAttributes(4, Tag.mage);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a Bronze or Silver special card, then draw a card.");

                return self;
            }
        }
        public static Unit Thaler
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Thaler");
                self.setUnitAttributes(13, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spying.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Single-Use: Draw 2 cards, keep one and return the other to your deck.");

                return self;
            }
        }
        public static Unit Trollololo
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Trollololo");
                self.setUnitAttributes(11, Tag.redania, Tag.ogroid);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "9 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deathwish: Set the power of all units on the row to the power of the Lowest unit on the row.");

                return self;
            }
        }
        public static Unit Ves
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Ves");
                self.setUnitAttributes(12, Tag.soldier, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap up to 2 cards.");

                return self;
            }
        }
        public static Unit VincentMeis
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Vincent Meis");
                self.setUnitAttributes(9, Tag.beast, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Destroy the Armor of all units, then boost self by half the value destroyed.");

                return self;
            }
        }
        public static Unit Botchling
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Botchling");
                self.setUnitAttributes(10, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon a Lubberkin to this row.");

                return self;
            }
        }
        public static Unit FoltestsPride
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Foltest's Pride");
                self.setUnitAttributes(10, Tag.machine, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to an enemy and move it to the row above. ");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Repeat its ability.");

                return self;
            }
        }
        public static Unit Lubberkin
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.silver, "Lubberkin");
                self.setUnitAttributes(5, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon a Botchling to this row.");

                return self;
            }
        }
        // < > BRONZE
        public static Unit AedirnianMauler
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Aedirnian Mauler");
                self.setUnitAttributes(7, Tag.soldier, Tag.aedirn);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 4 damage to an enemy.");

                return self;
            }
        }
        public static Unit AretuzaAdept
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Aretuza Adept");
                self.setUnitAttributes(3, Tag.mage, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Play a random Bronze Hazard from your deck.");

                return self;
            }
        }
        public static Unit BanArdTutor
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Ban Ard Tutor");
                self.setUnitAttributes(9, Tag.mage, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Swap a card in your hand with a Bronze special card from your deck.");

                return self;
            }
        }
        public static Unit BlueStripeCommando
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Blue Stripe Commando");
                self.setUnitAttributes(3, Tag.soldier, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever a different Temerian ally with the same power is played, Summon a copy of this unit to a random row.");

                return self;
            }
        }
        public static Unit BlueStripeScout
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Blue Stripe Scout");
                self.setUnitAttributes(3, Tag.soldier, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost all Temerian allies and your non-Spying Temerian units in hand and deck with the same power as this unit by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit CursedKnight
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Cursed Knight");
                self.setUnitAttributes(8, Tag.cursed, Tag.aedirn);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Transform a Cursed ally into a default copy of this unit.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "2 Armor.");

                return self;
            }
        }
        public static Unit DamnedSorceress
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Damned Sorceress");
                self.setUnitAttributes(4, Tag.mage, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If there is a Cursed unit on this row, deal 7 damage.");

                return self;
            }
        }
        public static Unit DunBanner
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Dun Banner");
                self.setUnitAttributes(4, Tag.soldier, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If you are losing by more than 25 on turn start, Summon this unit to a random row.");

                return self;
            }
        }
        public static Unit FieldMedic
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Field Medic");
                self.setUnitAttributes(8, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost Soldier allies by 1.");

                return self;
            }
        }
        public static Unit KaedweniCavalry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Kaedweni Cavalry");
                self.setUnitAttributes(8, Tag.soldier, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Destroy a unit's Armor, then boost self by the amount destroyed.");

                return self;
            }
        }
        public static Unit KaedweniKnight
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Kaedweni Knight");
                self.setUnitAttributes(8, Tag.soldier, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 5 if played from the deck.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "2 Armor.");

                return self;
            }
        }
        public static Unit KaedweniRevenant
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Kaedweni Revenant");
                self.setUnitAttributes(4, Tag.cursed, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "When you play your next Spell or Item, Spawn a Doomed default copy of this unit on its row.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "1 Armor.");

                return self;
            }
        }
        public static Unit KaedweniSergeant
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Kaedweni Sergeant");
                self.setUnitAttributes(9, Tag.kaedwen);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Clear Hazards from its row.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "3 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit PoorFingInfantry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Poor F'ing Infantry");
                self.setUnitAttributes(6, Tag.soldier, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Spawn Left Flank Infantry and Right Flank Infantry to the left and right of this unit, respectively.");

                return self;
            }
        }
        public static Unit ReaverHunter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Reaver Hunter");
                self.setUnitAttributes(6, Tag.redania, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost all copies of this unit in hand, deck, and on board by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Repeat its ability whenever a copy of this unit is played.");

                return self;
            }
        }
        public static Unit ReaverScout
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Reaver Scout");
                self.setUnitAttributes(1, Tag.redania, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Choose a different Bronze ally and play a copy of it from your deck.");

                return self;
            }
        }
        public static Unit RedanianElite
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Redanian Elite");
                self.setUnitAttributes(8, Tag.redania, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever this unit's Armor reaches 0, boost self by 5.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "4 Armor.");

                return self;
            }
        }
        public static Unit RedanianKnight
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Redanian Knight");
                self.setUnitAttributes(7, Tag.redania, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If this unit has no Armor, boost it by 2 and give it 2 Armor on turn end.");

                return self;
            }
        }
        public static Unit RedanianKnightElect
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Redanian Knight-Elect");
                self.setUnitAttributes(7, Tag.redania, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "If this unit has Armor on turn end, boost adjacent units by 1.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "2 Armor.");

                return self;
            }
        }
        public static Unit SiegeMaster
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Siege Master");
                self.setUnitAttributes(6, Tag.kaedwen, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Heal an allied Bronze or Silver Machine and repeat its ability.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit SiegeSupport
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Siege Support");
                self.setUnitAttributes(7, Tag.kaedwen, Tag.support);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Whenever an ally appears, boost it by 1. If it's a Machine, also give it 1 Armor.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crew.");

                return self;
            }
        }
        public static Unit TemerianDrummer
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Temerian Drummer");
                self.setUnitAttributes(5, Tag.support, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost an ally by 6.");

                return self;
            }
        }
        public static Unit TemerianInfantry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Temerian Infantry");
                self.setUnitAttributes(3, Tag.soldier, Tag.temeria);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Summon all copies of this unit to this row.");

                return self;
            }
        }
        public static Unit TormentedMage
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Tormented Mage");
                self.setUnitAttributes(2, Tag.mage, Tag.cursed);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Look at 2 Bronze Spells or Items from your deck, then play 1.");

                return self;
            }
        }
        public static Unit TridamInfantry
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Tridam Infantry");
                self.setUnitAttributes(10, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "4 Armor.");

                return self;
            }
        }
        public static Unit WitchHunter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Witch Hunter");
                self.setUnitAttributes(3, Tag.redania, Tag.soldier);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Reset a unit. If it's a Mage, play a copy of this unit from your deck.");

                return self;
            }
        }
        public static Unit Ballista
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Ballista");
                self.setUnitAttributes(6, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage to an enemy and 4 other random enemies with the same power.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Repeat its ability.");

                return self;
            }
        }
        public static Unit BatteringRam
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Battering Ram");
                self.setUnitAttributes(6, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 3 damage to an enemy. If it's destroyed, deal 3 damage to another enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Increase initial damage by 1.");

                return self;
            }
        }
        public static Unit ReinforcedBallista
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Reinforced Ballista");
                self.setUnitAttributes(7, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 2 damage to an enemy.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Repeat its ability.");

                return self;
            }
        }
        public static Unit ReinforcedTrebuchet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Reinforced Trebuchet");
                self.setUnitAttributes(8, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage to a random enemy on turn end.");

                return self;
            }
        }
        public static Unit SiegeTower
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Siege Tower");
                self.setUnitAttributes(8, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Boost self by 2.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Repeat its ability.");

                return self;
            }
        }
        public static Unit Trebuchet
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.northen, Rarity.bronze, "Trebuchet");
                self.setUnitAttributes(7, Tag.machine);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Deal 1 damage to 3 adjacent enemies.");
                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                self.setOnDeploy((s, f) =>
                { }, "Crewed: Increase damage by 1.");

                return self;
            }
        }
     
        

    }
}
