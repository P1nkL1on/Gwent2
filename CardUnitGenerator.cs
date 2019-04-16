using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnUnit
    {
        static Random unitDecisionsRandomiser = new Random();
        //delegate void TriggerMove (Card self, Place from);
        //delegate void TrigerRecieve(Unit self, Card source, int X);

        static Unit createToken(Unit preset, Card source)
        {
            preset.SetDefaultHost(source.host, source.context);
            source.context.AddCardToGame(preset);
            return preset;
        }
        static void resurrectAllyUnit(Card self, params UnitPredicat[] filters)
        {
            // select one card from discard
            List<UnitPredicat> fs = new List<UnitPredicat>();
            fs.Add(Filter.anyAllyUnitInDiscrard(self as Unit));
            fs.AddRange(filters);

            Unit u = self.host.selectUnit(
                        Select.Units(self.context.cards,
                        fs.ToArray()), self.QestionString());
            // the player plays a card if its exists
            if (u != null)
                self.host.playCard(u);
        }
        static void dealDamage(Card self, int damageValue)
        {
            Unit t = self.host.selectUnit(
                        Select.Units(self.context.cards, Filter.anyOtherUnitInBattlefield(self as Unit)),
                        self.QestionString());
            if (t != null)
                t.damage(self, damageValue);
        }

        public static Unit AnCraiteWarrior
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Warrior");
                self.setUnitAttributes(12, Tag.soldier, Tag.clanAnCraite);
                self.setOnDeploy((s, f) => { (s as Unit).damage(s, 1); }, "Deal 1 damage to self.");
                return self;
            }
        }
        public static Unit TuirseachVeteran
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Veteran");
                self.setUnitAttributes(7, Tag.support, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    foreach (Unit a in
                        Select.Units(s.context.cards,
                            Filter.anyOtherAllyUnitInBattlefieldHandDeck(s as Unit),
                            Filter.anyUnitHasTag(Tag.clanTuirseach)))
                        a.strengthen(s as Unit, 1);
                }, "Strengthen all your other Clan Tuirseach units in hand, deck, and on board by 1.");
                return self;
            }
        }
        public static Unit TuirseachSkirmisher
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Skirmisher");
                self.setUnitAttributes(8, Tag.soldier, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    if (f == Place.graveyard) (s as Unit).strengthen(s, 3);
                },
                    "Whenever this unit is Resurrected, Strengthen it by 3.");
                return self;
            }
        }
        public static Unit TuirseachHunter
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Hunter");
                self.setUnitAttributes(6, Tag.soldier, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    dealDamage(s, 5);
                }, "Deal 5 damage.");
                return self;
            }
        }
        public static Unit TokenBear
        {
            get
            {
                Unit self = new Unit();
                self.setAttributesToken(Clan.skellige, Rarity.bronze, "Bear");
                self.setUnitAttributes(11, Tag.beast, Tag.cursed);
                return self;
            }
        }
        public static Unit TuirseachBearmaster
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Bearmaster");
                self.setUnitAttributes(1, Tag.soldier, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    s.host.playCard(createToken(TokenBear, s));
                }, "Spawn a Bear.");
                return self;
            }
        }
        public static Unit TuirseachArcher
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Archer");
                self.setUnitAttributes(8, Tag.soldier, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    List<Unit> ts = s.host.selectUnits(
                        Select.Units(s.context.cards, Filter.anyOtherUnitInBattlefield(s as Unit)), 3,
                        s.QestionString());
                    foreach (Unit t in ts)
                        t.damage(s, 1);
                }, "Deal 1 damage to 3 units.");
                return self;
            }
        }
        public static Unit TuirseachAxeman
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Tuirseach Axeman");
                self.setUnitAttributes(6, Tag.soldier, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    (s as Unit).gainArmor(s, 2);
                }, "2 Armor.");
                self.setOnUnitDamaged((s, otherUnit, X) =>
                {
                    Unit unit = s as Unit;
                    if (unit.place != Place.battlefield)
                        return;
                    if (otherUnit.host != unit.host && otherUnit.row == unit.row)
                        unit.boost(unit, 1);
                }, "Whenever an enemy on the opposite row is damaged, boost self by 1.");
                return self;
            }
        }
        public static Unit PriestessOfFreya
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Priestess of Freya");
                self.setUnitAttributes(1, Tag.support, Tag.clanHeyMaey, Tag.doomed);
                self.setOnDeploy((s, f) =>
                {
                    resurrectAllyUnit(s, Filter.anyUnitHasTag(Tag.soldier), Filter.anyUnitHasColor(Rarity.bronze));
                }, "Resurrect a Bronze Soldier.");
                return self;
            }
        }
        public static Unit DrummondWarmonger
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Drummond Warmonger");
                self.setUnitAttributes(8, Tag.soldier, Tag.clanDrummond);
                self.setOnDeploy((s, f) =>
                {
                    Card u = s.host.selectCard(
                        Select.Cards(s.context.cards,
                        Filter.anyCardAllyInDeck(s as Unit),
                        Filter.anyCardHasColor(Rarity.bronze)), s.QestionString());
                    if (u != null)
                        u.move(Place.graveyard);
                }, "Discard a Bronze card from your deck.");
                return self;
            }
        }
        public static Unit DimunPirateCaptain
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Pirate Captain");
                self.setUnitAttributes(1, Tag.officer, Tag.clanDimun);
                self.setOnDeploy((s, f) =>
                {
                    Unit u = s.host.selectUnit(
                        Select.Units(s.context.cards,
                        Filter.anyAllyUnitInDeck(s as Unit),
                        Filter.anyUnitHasTag(Tag.clanDimun),
                        Filter.anyUnitHasColor(Rarity.bronze),
                        Filter.anyOtherUnitByName(s as Unit)),
                        s.QestionString());
                    if (u != null)
                        s.host.playCard(u);
                }, "Play a different Bronze Dimun unit from your deck.");
                return self;
            }
        }
        public static Unit DimunPirate
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Pirate");
                self.setUnitAttributes(11, Tag.soldier, Tag.clanDimun);
                self.setOnDeploy((s, f) =>
                {
                    List<Unit> us = Select.Units(s.context.cards,
                        Filter.anyAllyUnitInDeck(s as Unit),
                        Filter.anyCopie(s as Unit));
                    foreach (Unit u in us)
                        u.move(Place.graveyard);
                }, "Discard all copies of this unit from your deck.");
                return self;
            }
        }
        public static Unit DimunCorsair
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Corsair");
                self.setUnitAttributes(3, Tag.support, Tag.clanDimun, Tag.doomed);
                self.setOnDeploy((s, f) =>
                {
                    resurrectAllyUnit(s, Filter.anyUnitHasTag(Tag.machine), Filter.anyUnitHasColor(Rarity.bronze));
                }, "Resurrect a Bronze Machine.");
                return self;
            }
        }
        public static Unit BerserkerMarauder
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Berserker Marauder");
                self.setUnitAttributes(9, Tag.soldier, Tag.cursed, Tag.cultist);
                self.setOnDeploy((s, f) =>
                {
                    int buff =
                        Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit), Filter.anyUnitDamagedOrCursed())
                        .Count;
                    (s as Unit).boost(s, buff);
                },
                    "Boost self by 1 for each damaged or Cursed ally.");
                return self;
            }
        }
        public static Unit AnCraiteWarcrier
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Warcrier");
                self.setUnitAttributes(5, Tag.support, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit)),
                        s.QestionString());
                    if (t != null)
                        t.boost(s, t.power / 2);
                }, "Boost an ally by half its power.");
                return self;
            }
        }
        public static Unit AnCraiteRaider
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Raider");
                self.setUnitAttributes(4, Tag.soldier, Tag.clanAnCraite);
                self.setOnDiscard((s, f) =>
                {
                    // ERROR -> random row, not one
                    (s as Unit).row = unitDecisionsRandomiser.Next(3);
                    s.move(Place.battlefield);
                }, "Whenever you Discard this unit, Resurrect it on a random row.");
                return self;
            }
        }
        public static Unit AnCraiteMarauder
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Marauder");
                self.setUnitAttributes(7, Tag.soldier, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    dealDamage(s, f == Place.graveyard ? 6 : 4);
                }, "Deal 4 damage. If Resurrected, deal 6 damage instead.");
                return self;
            }
        }
        public static Unit AnCraiteBlacksmith
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Blacksmith");
                self.setUnitAttributes(9, Tag.support, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit)),
                        s.QestionString());
                    if (t != null)
                    {
                        t.strengthen(s, 2);
                        t.gainArmor(s, 2);
                    }
                }, "Strengthen an ally by 2 and give it 2 Armor.");
                return self;
            }
        }
        public static Unit AnCraiteGreatsword
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Greatsword");
                self.setUnitAttributes(8, Tag.soldier, Tag.clanAnCraite);
                self.timer = new Timer(self, (s) =>
                {
                    if (!(s as Unit).isDamaged)
                        return;
                    (s as Unit).heal(s);
                    (s as Unit).strengthen(s, 2);
                }, 2, true);
                self.setOnTurnStart((s) =>
                {
                    if (s.place != Place.battlefield)
                        return;
                    s.timer.Tick();
                }, "Every 2 turns, if damaged, Heal self and Strengthen by 2 on turn start.");
                return self;
            }
        }
        public static Unit AnCraiteArmorsmith
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Armorsmith");
                self.setUnitAttributes(7, Tag.support, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    List<Unit> ts = s.host.selectUnits(
                        Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit)), 2,
                        s.QestionString());
                    foreach (Unit t in ts)
                    {
                        t.heal(s);
                        t.gainArmor(s, 3);
                    }
                }, "Heal 2 allies and give them 3 Armor.");
                return self;
            }
        }
        //Dimun Smuggler
        //Drummond Queensguard
        //Drummond Shieldmaid
        //Heymaey Flaminica
        //Heymaey Herbalist
        //Heymaey Protector
        //Heymaey Skald
        public static Unit HeymaeySpearmaiden
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Heymaey Spearmaiden");
                self.setUnitAttributes(2, Tag.support, Tag.clanHeyMaey);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards,
                        Filter.anyOtherAllyUnitInBattlefield(s as Unit),
                        Filter.anyUnitSoldierOrMachine(),
                        Filter.anyUnitHasColor(Rarity.bronze)),
                        s.QestionString());

                    if (t != null)
                    {
                        var topCopy = s.context._topUnitOfDeck(s.host, Filter.anyCopie(t));
                        if (topCopy == null)
                            return;
                        t.damage(s, 1);
                        s.host.playCard(topCopy);
                    }
                }, "Deal 1 damage to a Bronze Machine or Soldier ally, then play a copy of it from your deck.");
                return self;
            }
        }
        //Raging Berserker
        //Savage Bear
        //Dimun Warship
        //An Craite Longship
        public static Unit Vabjorn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Vabjorn");
                self.setUnitAttributes(11, Tag.cursed, Tag.cultist);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s)), 
                        s.QestionString());
                    if (t == null)
                        return;
                    if (!t.isDamaged)
                        t.damage(s, 2);
                    else
                        t.destroy(s);
                },
                    "Deal 2 damage to an enemy. If it was already damaged, destroy it instead.");
                return self;
            }
        }
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
                        s.context._randomUnitFrom(
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

                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s as Unit)),
                        s.QestionString());
                    if (t != null)
                        t.boost(s, 12);
                    
                }, "Spying.\nBoost an ally by 12.");
                return self;
            }
        }
    }
}
