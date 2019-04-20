using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnUnit
    {
        // random
        static Random unitDecisionsRandomiser = new Random();

        // universal templates
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
            fs.Add(Filter.anyAllyUnitInDiscard(self as Unit));
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

        // ||| SKELIGE |||
        // < > bronze skellige
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
                    List<Unit> bts = Select.Units(s.context.cards, Filter.anyOtherUnitInBattlefield(s as Unit));
                    List<Unit> ts = s.host.selectUnits(
                        bts, 3,
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
                        Filter.anyCardInYourDeck(s as Unit),
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
        public static Unit DrummondQueensguard
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Drummond Queensguard");
                self.setUnitAttributes(4, Tag.soldier, Tag.clanDrummond);
                self.setOnDeploy((s, f) =>
                {
                    foreach (Unit u in Select.Units(s.context.cards,
                        Filter.anyAllyUnitInDiscard(s as Unit),
                        Filter.anyCopie(s as Unit)))
                    {
                        u.move(Place.battlefield);
                        u.row = (s as Unit).row;
                    }
                }, "Resurrect all copies of this unit on this row.");
                return self;
            }
        }
        public static Unit DrummondShieldmaid
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Drummond Shieldmaid");
                self.setUnitAttributes(3, Tag.soldier, Tag.clanDrummond);
                self.setOnDeploy((s, f) =>
                {
                    foreach (Unit u in Select.Units(s.context.cards,
                        Filter.anyAllyUnitInDeck(s as Unit),
                        Filter.anyCopie(s as Unit)))
                    {
                        u.move(Place.battlefield);
                        u.row = (s as Unit).row;
                    }
                }, "Summon all copies of this unit to this row.");
                return self;
            }
        }
        public static Unit HeymaeyFlaminica
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Heymaey Flaminica");
                self.setUnitAttributes(10, Tag.support, Tag.clanHeyMaey);
                self.setOnDeploy((s, f) =>
                {
                    s.context._removeRowEffect(s.host, (s as Unit).row, Filter.anyCardHasTag(Tag.hazzard));
                    s.host.selectUnits(
                        Select.Units(s.context.cards,
                        Filter.anyOtherAllyUnitInBattlefield(s as Unit),
                        Filter.anyUnitNotInRow((s as Unit).row)),
                        2, s.QestionString());
                }, "Clear Hazards from the row and move 2 allies to it.");
                return self;
            }
        }
        public static Unit HeymaeyHerbalist
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Heymaey Herbalist");
                self.setUnitAttributes(2, Tag.support, Tag.clanHeyMaey);
                self.setOnDeploy((s, f) =>
                {
                    Card item = Filter.randomCardFrom(Select.Cards(s.context.cards,
                        Filter.anyCardInYourDeck(s),
                        Filter.anyCardHasTag(Tag.hazzard, Tag.organic),
                        Filter.anyCardHasColor(Rarity.bronze)));
                    if (item != null)
                        s.host.playCard(item);
                }, "Play a random Bronze Organic or Hazard card from your deck.");
                return self;
            }
        }
        public static Unit HeymaeyProtector
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Heymaey Protector");
                self.setUnitAttributes(2, Tag.soldier, Tag.clanHeyMaey);
                self.setOnDeploy((s, f) =>
                {
                    Card item = s.host.selectCard(Select.Cards(s.context.cards,
                        Filter.anyCardInYourDeck(s),
                        Filter.anyCardHasTag(Tag.item),
                        Filter.anyCardHasColor(Rarity.bronze)),
                        s.QestionString());
                    if (item != null)
                        s.host.playCard(item);
                }, "Play a Bronze Item from your deck.");
                return self;
            }
        }
        public static Unit HeymaeySkald
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Heymaey Skald");
                self.setUnitAttributes(9, Tag.support, Tag.clanHeyMaey);
                self.setOnDeploy((s, f) =>
                {
                    List<Tag> possibleClans = new List<Tag>(){
                        Tag.clanAnCraite,
                        Tag.clanDimun,
                        Tag.clanDrummond,
                        Tag.clanHeyMaey,
                        Tag.clanTuirseach,
                        Tag.clanTordarroch,
                        Tag.clanBrokvar
                    };
                    Unit ally = s.host.selectUnit(
                        Select.Units(s.context.cards,
                            Filter.anyOtherAllyUnitInBattlefield(s as Unit),
                            Filter.anyUnitHasTag(possibleClans.ToArray())),
                        s.QestionString());
                    if (ally == null)
                        return;
                    Tag clanItHas = Tag.none;
                    foreach (Tag cl in possibleClans)
                        if (ally.hasTag(cl))
                            clanItHas = cl;
                    if (clanItHas == Tag.none)
                        return;
                    foreach (Unit u in Select.Units(s.context.cards,
                        Filter.anyAllyUnitInBattlefield(s),
                        Filter.anyUnitHasTag(clanItHas)))
                        u.boost(s, 1);
                }, "Boost all allies from a Clan of your choice by 1.");
                return self;
            }
        }
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
                        Filter.anyUnitHasTag(Tag.soldier, Tag.machine),
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
        public static Unit DimunLightLongship
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Light Longship");
                self.setUnitAttributes(7, Tag.machine, Tag.clanDimun);
                self.setOnTurnEnd((s) =>
                {
                    Unit left = s.context._unitToTheLeft(s as Unit);
                    if (left == null)
                        return;
                    left.damage(s, 1);
                    (s as Unit).boost(s, 2);
                }, "On turn end, deal 1 damage to the unit to the right, then boost self by 2.");
                return self;
            }
        }
        
        // < > silver skellige
        public static Unit JuttaanDimun
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Jutta an Dimun");
                self.setUnitAttributes(13, Tag.soldier, Tag.clanDimun);
                self.setOnDeploy((s, f) => { (s as Unit).damage(s, 1); }, "Deal 1 damage to self.");
                return self;
            }
        }
        public static Unit Yoana
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Yoana");
                self.setUnitAttributes(6, Tag.support, Tag.clanTordarroch);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards,
                            Filter.anyOtherAllyUnitInBattlefield(s as Unit),
                            Filter.anyUnitDamaged()),
                        s.QestionString());
                    if (t != null)
                    {
                        int healthMissed = t.basePower - t.power;
                        if (healthMissed <= 0) return;
                        t.heal(s);
                        t.boost(s, healthMissed);
                    }
                }, "Heal an ally, then boost it by the amount Healed.");
                return self;
            }
        }
        public static Unit Udalryk
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Udalryk");
                self.setUnitAttributes(13, Tag.cursed, Tag.clanBrokvar);
                self.setSpying();
                self.timer = new Timer(self, (s) =>
                {
                    (s as Unit).status.isSpy = true;

                    Card willBeDiscraded = null;
                    Card willBeDrawn =
                        s.baseHost.selectOneAndReturnRest(
                        s.context._topCardsOfDeck(s.baseHost, 2),
                        out willBeDiscraded,
                        "Select a card to draw. The other one will be discarded.");

                    if (willBeDrawn != null) s.context._drawCard(s.baseHost, willBeDrawn);
                    if (willBeDiscraded != null) willBeDiscraded.move(Place.graveyard);
                }, 1, false);
                self.setOnDeploy((s, f) =>
                {
                    // use only once
                    s.timer.Tick();
                }, "Spying.\nLook at 2 random Bronze units from your deck, then play 1.");
                return self;
            }
        }
        public static Unit SvanrigeTuirseach
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Svanrige Tuirseach");
                self.setUnitAttributes(9, Tag.officer, Tag.clanTuirseach);
                self.setOnDeploy(
                    (s, f) =>
                    {
                        s.context._drawCard(s.host, 1);
                        // short way discarding
                        Card cardToDiscard = s.host.selectCard(
                            Select.Cards(s.context.cards, Filter.anyCardInYourHand(s)),
                            "Discard a card");
                        if (cardToDiscard != null)
                            cardToDiscard.move(Place.graveyard);
                    },
                    "Draw a card, then Discard a card.");
                return self;
            }
        }
        public static Unit Skjall
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Skjall");
                self.setUnitAttributes(5, Tag.cursed, Tag.clanHeyMaey);
                self.setOnDeploy(
                    (s, f) =>
                    {
                        Unit u =
                            Filter.randomUnitFrom(
                                Select.Units(s.context.cards,
                                    Filter.anyAllyUnitInDeck(s),
                                    Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver),
                                    Filter.anyUnitHasTag(Tag.cursed)));
                        if (u != null)
                            u.host.playCard(u);
                    },
                    "Play a random Bronze or Silver Cursed unit from your deck.");
                return self;
            }
        }
        public static Unit Morkvarg
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Morkvarg");
                self.setUnitAttributes(9, Tag.cursed, Tag.beast);
                self.setOnDiscard((s, f) =>
                {
                    Unit u = s as Unit;
                    u.weaken(s, u.basePower - u.basePower / 2);
                    u.row = unitDecisionsRandomiser.Next(3);
                    s.move(Place.battlefield);
                },
                    "Whenever discarded, resurrect on a random row, then weaken self by half.");

                self.setOnDestroy((s, f) =>
                {
                    Unit u = s as Unit;
                    u.weaken(s, u.basePower - u.basePower / 2);
                    s.move(Place.battlefield);
                },
                    "Whenever destroyed, resurrect on a random row, then weaken self by half.");
                return self;
            }
        }

        // < > golden skellige
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

        // ||| NILFGAARD |||
        // < > bronze inlfgaard
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

        // misc
        public static void showCaseAllUnits()
        {
            Type spawner = (new SpawnUnit()).GetType();
            foreach (var unitMethod in spawner.GetMethods())
            {
                Console.Clear();
                try
                {
                    Console.WriteLine(((unitMethod.Invoke(new SpawnUnit(), null)) as Unit).ToFormat());
                    Console.ReadLine();
                }
                catch (Exception e) { }
            }
        }
    }
}
