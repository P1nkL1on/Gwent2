﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    partial class SpawnUnit : Spawner
    {
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
                            Filter.anyUnitHasTagAnyFrom(Tag.clanTuirseach)))
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
                    CommonFunc.dealDamage(s, 5);
                }, "Deal 5 damage.");
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
                    s.host.playCard(CommonFunc.createToken(TokenBear, s));
                }, "Spawn a Bear.");
                self.setDeployValue(Value.spawnToken(11));
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
                self.setOnUnitDamaged((s, otherCard, X) =>
                {
                    Unit unit = s as Unit, otherUnit = otherCard as Unit;
                    if (otherUnit == null)
                        return;
                    if (unit.place != Place.battlefield)
                        return;
                    if (otherCard.host != unit.host && otherUnit.row == unit.row)
                        unit.boost(unit, 1);
                }, "Whenever an enemy on the opposite row is damaged, boost self by 1.");
                return self;
            }
        }
        public static Unit PriestessofFreya
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Priestess of Freya");
                self.setUnitAttributes(1, Tag.support, Tag.clanHeyMaey, Tag.doomed);
                self.setOnDeploy((s, f) =>
                {
                    CommonFunc.resurrectAllyUnit(s, Filter.anyUnitHasTagAnyFrom(Tag.soldier), Filter.anyUnitHasColor(Rarity.bronze));
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
                        Filter.anyUnitHasTagAnyFrom(Tag.clanDimun),
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
                    CommonFunc.resurrectAllyUnit(s, Filter.anyUnitHasTagAnyFrom(Tag.machine), Filter.anyUnitHasColor(Rarity.bronze));
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
                    (s as Unit).row = CommonFunc.unitDecisionsRandomiser.Next(3);
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
                    CommonFunc.dealDamage(s, f == Place.graveyard ? 6 : 4);
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
                    s.context._removeRowEffect(s.host, (s as Unit).row, Filter.anyCardHasTagAnyFrom(Tag.hazard));
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
                        Filter.anyCardHasTagAnyFrom(Tag.hazard, Tag.organic),
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
                        Filter.anyCardHasTagAnyFrom(Tag.item),
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
                    Unit ally = s.host.selectUnit(
                        Select.Units(s.context.cards,
                            Filter.anyOtherAllyUnitInBattlefield(s as Unit),
                            Filter.anyUnitHasTagAnyFrom(Utils.possibleClans.ToArray())),
                        s.QestionString());
                    if (ally == null)
                        return;
                    Tag clanItHas = Tag.none;
                    foreach (Tag cl in Utils.possibleClans)
                        if (ally.hasTag(cl))
                            clanItHas = cl;
                    if (clanItHas == Tag.none)
                        return;
                    foreach (Unit u in Select.Units(s.context.cards,
                        Filter.anyAllyUnitInBattlefield(s),
                        Filter.anyUnitHasTagAnyFrom(clanItHas)))
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
                        Filter.anyUnitHasTagAnyFrom(Tag.soldier, Tag.machine),
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
        public static Unit RagingBear
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Raging Bear");
                self.setUnitAttributes(12, Tag.beast, Tag.cursed, Tag.cultist);
                return self;
            }
        }
        public static Unit RagingBerserker
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Raging Berserker");
                self.setUnitAttributes(8, Tag.soldier, Tag.cursed, Tag.cultist);
                self.setOnDamaged((s, u, X) => { if (s.place == Place.battlefield) CommonFunc.transformUnit(s as Unit, SpawnUnit.RagingBear, s); }, "When this unit is damaged or weakened, transform into a Raging Bear.");
                self.setOnWeakened((s, u, X) => { if (s.place == Place.battlefield) CommonFunc.transformUnit(s as Unit, SpawnUnit.RagingBear, s); }, "");
                return self;
            }
        }
        public static Unit SavageBear
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Savage Bear");
                self.setUnitAttributes(9, Tag.beast, Tag.cursed);
                self.setOnCardPlayed((s, f, X) =>
                {
                    // if bear is not on field || card has is on out field || card is not a unit
                    if (s.place != Place.battlefield || f.host == s.host || f as Unit == null)
                        return;
                    (f as Unit).damage(s, 1);
                }, "Whenever a unit is played from either hand on your opponent's side, deal 1 damage to it.");
                return self;
            }
        }
        public static Unit DimunWarship
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Warship");
                self.setUnitAttributes(7, Tag.machine, Tag.clanDimun);
                self.setOnDeploy((s, f) =>
                {
                    Unit target = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyOtherUnitInBattlefield(s as Unit)), s.QestionString());
                    if (target == null)
                        return;
                    for (int i = 0; i < 4; i++)
                        target.damage(s, 1);
                }, "Deal 1 damage to the same unit 4 times.");
                return self;
            }
        }
        public static Unit AnCraiteLongship
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Longship");
                self.setUnitAttributes(7, Tag.machine, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    Unit randomEnemy = Filter.randomUnitFrom(Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s)));
                    if (randomEnemy != null)
                        randomEnemy.damage(s, 2);
                }, "Deal 2 damage to a random enemy.");
                self.setOnCardDiscarded((s, f, X) =>
                {
                    // if is on battlefield and discarded card is yours
                    if (s.place == Place.battlefield && f.host == s.host)
                        s.repeatDeployAbility();
                }, "Repeat this ability whenever you Discard a card.");
                return self;
            }
        }
        public static Unit DimunLightLongship
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "Dimun Light Longship");
                self.setUnitAttributes(7, Tag.machine, Tag.clanDimun);
                self.setOnTurnEnd((s) =>
                {
                    if (s.place != Place.battlefield)
                        return;
                    Unit right = s.context._unitToTheRight(s as Unit);
                    if (right == null)
                        return;
                    right.damage(s, 1);
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
                self.setDeployValue((s, f) =>
                {
                    double drawValue = 0.0;
                    if (!s.timer.IsEverTicked())
                    {
                        // mean value of possible drawing card
                        drawValue = 9.9;
                    }
                    return drawValue - s.power; // is spy, so minus his power
                });
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
                                    Filter.anyUnitHasTagAnyFrom(Tag.cursed)));
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
                    u.row = CommonFunc.unitDecisionsRandomiser.Next(3);
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
        public static Unit Sigrdrifa
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Sigrdrifa");
                self.setUnitAttributes(3, Tag.support, Tag.doomed);
                self.setOnDeploy((s, f) =>
                {
                    CommonFunc.resurrectAllyUnit(s, Filter.anyUnitHasTagAnyFrom(Utils.possibleClans.ToArray()), Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver));
                }, "Resurrect a Bronze Soldier.");
                return self;
            }
        }
        //Harald Houndsnout
        //Blueboy Lugos
        //Donar an Hindar
        //Spectral Whale
        public static Unit GiantBoar
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Giant Boar");
                self.setUnitAttributes(8, Tag.beast);
                self.setOnDeploy((s, f) =>
                {
                    Unit randomAllyToDestroy = Filter.randomUnitFrom(Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit)));
                    if (randomAllyToDestroy == null)
                        return;
                    randomAllyToDestroy.destroy(s);
                    (s as Unit).boost(s, 10);
                }, "Destroy a random ally, excluding this unit, then boost self by 10.");
                return self;
            }
        }
        public static Unit HolgerBlackhand
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Holger Blackhand");
                self.setUnitAttributes(6, Tag.officer, Tag.clanDimun);
                self.setOnDeploy((s, f) =>
                {
                    bool destroyed = CommonFunc.dealDamage(s, 6);
                    Unit u = Filter.highestUnit(Select.Units(s.context.cards, Filter.anyAllyUnitInDiscard(s)));
                    if (u != null)
                        u.strengthen(s, 3);
                }, "Deal 6 damage. If the unit was destroyed, Strengthen the Highest unit in your graveyard by 3.");
                return self;
            }
        }
        public static Unit DraigBonDhu
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Draig Bon-Dhu");
                self.setUnitAttributes(6, Tag.support);
                self.setOnDeploy((s, f) =>
                {
                    foreach (Card u in s.host.selectCardsOrNone(
                        Select.Cards(s.context.cards,
                            Filter.anyUnit(),
                            Filter.anyCardInYourGraveyard(s),
                            Filter.nonLeaderCard()),
                    2, s.QestionString()))
                        (u as Unit).strengthen(s, 3);

                }, "Strengthen 2 non-Leader units in your graveyard by 3.");
                return self;
            }
        }
        public static Unit DjengeFrett
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Djenge Frett");
                self.setUnitAttributes(10, Tag.soldier, Tag.clanDimun);
                self.setOnDeploy((s, f) =>
                {
                    foreach (Unit u in s.host.selectUnits(
                        Select.Units(s.context.cards, Filter.anyOtherAllyUnitInBattlefield(s as Unit)),
                        2, s.QestionString()))
                    {
                        u.damage(s, 1);
                        (s as Unit).strengthen(s, 2);
                    }

                }, "Deal 1 damage to 2 allies and Strengthen self by 2 for each.");
                return self;
            }
        }
        public static Unit Derran
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Derran");
                self.setUnitAttributes(6, Tag.cursed, Tag.clanTuirseach);
                self.setOnUnitDamaged((s, otherCard, X) =>
                {
                    Unit unit = s as Unit, otherUnit = otherCard as Unit;
                    if (otherUnit != null && unit.place == Place.battlefield && otherCard.host != unit.host)
                        unit.boost(unit, 1);
                }, "Whenever an enemy is damaged, boost self by 1.");
                return self;
            }
        }
        public static Unit ChampionofHov
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.silver, "Champion of Hov");
                self.setUnitAttributes(7, Tag.ogroid);
                self.setOnDeploy((s, f) =>
                {
                    Unit target = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s)), s.QestionString());
                    if (target != null)
                        CommonFunc.duel(s as Unit, target);
                }, "Duel an enemy.");
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
        public static Unit Coral
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Coral");
                self.setUnitAttributes(5, Tag.mage);
                self.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards,
                            Filter.anyOtherUnitInBattlefield(s as Unit),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver)),
                        s.QestionString());
                    if (t != null)
                        CommonFunc.transformUnit(t, SpawnUnit.TokenJadeFigurine, s);
                }, "Transform a Bronze or Silver unit into a Jade Figurine.");
                return self;
            }
        }
        public static Unit BirnaBran
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Birna Bran");
                self.setUnitAttributes(6, Tag.officer, Tag.clanTuirseach);
                self.setOnDeploy((s, f) =>
                {
                    CommonFunc.applyHazzardOnDeployWithoutCard(s, SpawnSpecial.SkelligeStorm, SpawnSpecial.storm);
                }, "Apply Skellige Storm to an enemy row.");
                return self;
            }
        }
        public static Unit Hym
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Hym");
                self.setUnitAttributes(3, Tag.cursed);
                self.setOnDeploy((s, f) =>
                {
                    if (s.host.selectOption(ChoiseOptionContext.OneOfTwo(
                           "Play Cursed unit from your deck.",
                           "Create Silver unit from enemy deck."))
                           == 0)
                    {
                        Card cursedAllyFromDeck = s.host.selectCard(
                            Select.Cards(s.context.cards,
                            Filter.anyCardInYourDeck(s),
                            Filter.anyCardHasTagAnyFrom(Tag.cursed),
                            Filter.anyCardHasColor(Rarity.bronze, Rarity.silver)),
                        s.QestionString());
                        if (cursedAllyFromDeck != null)
                            s.host.playCard(cursedAllyFromDeck);
                    }
                    else
                    {
                        Card u = CommonFunc.createCard(s, s.context.startedDeck,
                            Filter.anyUnit(),
                            Filter.anyCardInEnemyDeck(s),
                            Filter.anyCardHasColor(Rarity.silver));
                        if (u != null)
                            s.host.playCard(u);
                    }
                }, "Choose One: Play a Bronze or Silver Cursed unit from your deck; or Create a Silver unit from your opponent's starting deck.");
                return self;
            }
        }
        public static Unit WildBoaroftheSea
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Wild Boar of the Sea");
                self.setUnitAttributes(8, Tag.machine, Tag.clanAnCraite);
                self.setOnDeploy((s, f) =>
                {
                    (s as Unit).gainArmor(s, 5);
                }, "5 Armor.");
                self.setOnTurnEnd((s) =>
                {
                    if (s.place != Place.battlefield) return;
                    Unit right = s.context._unitToTheRight(s as Unit),
                         left = s.context._unitToTheLeft(s as Unit);
                    if (right == null) right.damage(s, 1);
                    if (left == null) left.strengthen(s, 1);
                }, "On turn end, Strengthen the unit to the left by 1, then deal 1 damage to the unit to the right.");
                return self;
            }
        }
        public static Unit Ulfhedinn
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Ulfhedinn");
                self.setUnitAttributes(6, Tag.beast, Tag.cursed);
                self.setOnDeploy((s, f) =>
                {
                    var notDamaged = Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s), Filter.anyUnitNotDamaged());
                    var alreadyDamaged = Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s), Filter.anyUnitDamaged());
                    foreach (Unit e in notDamaged)
                        e.damage(s, 1);
                    foreach (Unit e in alreadyDamaged)
                        e.damage(s, 2);
                }, "Deal 1 damage to all enemies. If they were already damaged, deal 2 damage instead.");
                return self;
            }
        }
        public static Unit MadmanLugos
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Madman Lugos");
                self.setUnitAttributes(6, Tag.officer, Tag.clanDrummond);
                self.setOnDeploy((s, f) =>
                {
                    Card u = s.host.selectCard(
                        Select.Cards(s.context.cards,
                            Filter.anyUnit(),
                            Filter.anyCardInYourDeck(s as Unit),
                            Filter.anyCardHasColor(Rarity.bronze)),
                        s.QestionString());
                    if (u != null)
                    {
                        u.move(Place.graveyard);
                        CommonFunc.dealDamage(s, (u as Unit).basePower);
                    }
                }, "Discard a Bronze unit from your deck, then deal damage equal to its base power to an enemy.");
                return self;
            }
        }
        public static Unit Ermion
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.gold, "Ermion");
                self.setUnitAttributes(10, Tag.support, Tag.clanAnCraite);
                self.setOnDeploy(
                    (s, f) =>
                    {
                        s.context._drawCard(s.host, 2);
                        // short way discarding
                        var cardsToDiscard = s.host.selectCards(
                            Select.Cards(s.context.cards, Filter.anyCardInYourHand(s)),
                            2, "Discard a card");
                        foreach (Card c in cardsToDiscard)
                            c.move(Place.graveyard);
                    }, "Draw 2 cards, then Discard 2 cards.");
                return self;
            }
        }
    }
}
