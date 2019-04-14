using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnUnit
    {
        //delegate void TriggerMove (Card self, Place from);
        //delegate void TrigerRecieve(Unit self, Card source, int X);


        static Unit createToken(Unit preset, Card source)
        {
            preset.SetDefaultHost(source.host, source.context);
            source.context.AddCard(preset);
            return preset;
        }

        public static Unit AnCraiteWarrior
        {
            get
            {
                Unit self = new Unit();
                self.setAttributes(Clan.skellige, Rarity.bronze, "An Craite Warrior");
                self.setUnitAttributes(12, Tag.soldier, Tag.clanAnCraite);
                self.setOnDeploy((s, f) => { (s as Unit).damage(s, 1); });
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
                        a.stregthen(s as Unit, 1);
                });
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
                self.setOnDeploy((s, f) => { if (f == Place.discard)(s as Unit).stregthen(s, 3); });
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
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards, Filter.anyOtherUnitInBattlefield(s as Unit)));
                    if (t != null)
                        t.damage(s, 5);
                });
                return self;
            }
        }
        public static Unit TokenBear
        {
            get
            {
                Unit self = new Unit();
                self.setAttributesToken(Clan.skellige, Rarity.bronze, "Bear");
                self.setUnitAttributes(11, Tag.beast);
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
                });
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
                        Select.Units(s.context.cards, Filter.anyOtherUnitInBattlefield(s as Unit)), 3);
                    foreach (Unit t in ts)
                        t.damage(s, 1);
                });
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
                });
                self.setOnUnitDamaged((s, otherUnit, X) =>
                {
                    Unit unit = s as Unit;
                    if (unit.place != Place.battlefield)
                        return;
                    if (otherUnit.host != unit.host && otherUnit.row == unit.row)
                        unit.buff(unit, 1);
                });
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
                    Unit u = s.host.selectUnit(
                        Select.Units(s.context.cards, 
                        Filter.anyAllyUnitInDiscrard(s as Unit), 
                        Filter.anyUnitHasTag(Tag.soldier),
                        Filter.anyUnitHasColor(Rarity.bronze)));
                    if (u != null)
                        s.host.playCard(u);
                });
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
                    Unit u = s.host.selectUnit(
                        Select.Units(s.context.cards,
                        Filter.anyAllyUnitInDeck(s as Unit),
                        Filter.anyUnitHasColor(Rarity.bronze)));
                    if (u != null)
                        u.move(Place.discard);
                });
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
                        Filter.anyOtherUnitByName(s as Unit)));
                    if (u != null)
                        s.host.playCard(u);
                });
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
                        u.move(Place.discard);
                });
                return self;
            }
        }
    }
}
