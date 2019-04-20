using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    delegate bool UnitPredicat(Unit target);
    delegate bool CardPredicat(Card target);
    delegate bool SpecialPredicat(Special target);

    class Filter
    {

        // <...> For Units

        public static UnitPredicat anyUnitInPlace(Place place)
        {
            return (t) => { return t.place == place; };
        }
        public static UnitPredicat anyOtherUnitInPlace(Unit source, Place place)
        {
            return (t) => { return t != source && t.place == place; };
        }
        public static UnitPredicat anyOtherUnitNotInPlace(Unit source, Place place)
        {
            return (t) => { return t != source && t.place != place; };
        }
        public static UnitPredicat anyOtherAllyUnitInPlace(Unit source, Place place)
        {
            return (t) => { return t != source && t.host == source.host && t.place == place; };
        }
        public static UnitPredicat anyAllyUnitInPlace(Card source, bool anyOther, params Place[] places)
        {
            return (t) =>
            {
                if ((anyOther && t == source) || t.host != source.host)
                    return false;
                foreach (Place place in places)
                    if (t.place == place)
                        return true;
                return false;
            };
        }
        public static UnitPredicat anyAllyUnitInPlace(Card source, Place place)
        {
            return (t) => { return t.host == source.host && t.place == place; };
        }
        public static UnitPredicat anyEnemyUnitInPlace(Card source, Place place)
        {
            return (t) => { return t.host != source.host && t.place == place; };
        }
        public static UnitPredicat anyOtherUnitByName(Unit source) { return (t) => { return t.name != source.name; }; }
        public static UnitPredicat anyCopie(Unit source) { return (t) => { return t.name == source.name; }; }
        public static UnitPredicat anyUnitDamaged() { return (t) => { return t.isDamaged; }; }
        public static UnitPredicat anyUnitDamagedOrCursed() { return (t) => { return t.isDamaged || t.hasTag(Tag.cursed); }; }
        public static UnitPredicat anyUnitInRow(int row) { return (t) => { return t.row == row; }; }
        public static UnitPredicat anyUnitNotInRow(int row) { return (t) => { return t.row != row; }; }
        public static UnitPredicat anyUnitHostBy(Player player) { return (t) => { return t.host == player; }; }
        
        public static CardPredicat anyCardHasTagAnyFrom(params Tag[] anyOfTags)
        {
            return (t) =>
            {
                foreach (Tag tag in anyOfTags)
                    if (t.hasTag(tag))
                        return true;
                return false;
            };
        }
        public static UnitPredicat anyUnitHasTagAnyFrom(params Tag[] anyOfTags)
        {
            return (t) =>
            {
                foreach (Tag tag in anyOfTags)
                    if (t.hasTag(tag))
                        return true;
                return false;
            };
        }
        public static UnitPredicat anyUnitHasTagNot(params Tag[] anyOfProhibitedTags)
        {
            return (t) =>
            {
                foreach (Tag tag in anyOfProhibitedTags)
                    if (t.hasTag(tag))
                        return false;
                return true;
            };
        }
        public static UnitPredicat anyUnitHasColor(params Rarity[] anyOfRarity)
        {
            return (t) =>
            {
                foreach (Rarity rarity in anyOfRarity)
                    if (t.rarity == rarity)
                        return true;
                return false;
            };
        }
        public static UnitPredicat anyUnitHasClan(Clan clan){ return (t) => { return t.clan == clan; }; }

        public static UnitPredicat anyUnitHasColorNot(params Rarity[] anyOfProhibitedRarity)
        {
            return (t) =>
            {
                foreach (Rarity rarity in anyOfProhibitedRarity)
                    if (t.rarity == rarity)
                        return false;
                return true;
            };
        }
        public static UnitPredicat nonLeader() { return (t) => { return !t.hasTag(Tag.leader); }; }
        public static CardPredicat nonLeaderCard() { return (t) => { return !t.hasTag(Tag.leader); }; }
        public static UnitPredicat nonSpying() { return (t) => { return !t.isSpy; }; }
        public static UnitPredicat anyUnitInBaseHostDeck(Card source) { return (t) => { return t.place == Place.deck && t.host == source.baseHost; }; }
        public static UnitPredicat anyUnitInBaseHostBattlefield(Card source) { return (t) => { return t.place == Place.battlefield && t.host == source.baseHost; }; }

        public static UnitPredicat anyUnitInBattlefield() { return anyUnitInPlace(Place.battlefield); }
        public static UnitPredicat anyOtherUnitInBattlefield(Unit source) { return anyOtherUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInBattlefield(Card source) { return anyAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInDiscard(Card source) { return anyAllyUnitInPlace(source, Place.graveyard); }
        public static UnitPredicat anyAllyUnitInDeck(Card source) { return anyAllyUnitInPlace(source, Place.deck); }
        public static UnitPredicat anyAllyUnitInHand(Card source) { return anyAllyUnitInPlace(source, Place.hand); }
        public static UnitPredicat anyOtherAllyUnitInBattlefield(Unit source) { return anyOtherAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyOtherAllyUnitInDeck(Unit source) { return anyOtherAllyUnitInPlace(source, Place.deck); }
        public static UnitPredicat anyEnemyUnitInBattlefield(Card source) { return anyEnemyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyOtherAllyUnitInBattlefieldHandDeck(Unit source) { return anyAllyUnitInPlace(source, true, Place.battlefield, Place.hand, Place.deck); }

        // <...> For cards
        public static CardPredicat anyUnit(){return (c) => { return c as Unit != null; };}
        public static CardPredicat anyCardIn(Place place)
        {
            return (c) => { return c.place == place; };
        }
        public static CardPredicat anyCardHostByPlayer(Player host)
        {
            return (c) => { return c.host == host; };
        }
        public static CardPredicat anyCardHostByPlayerIn(Place place, Player host)
        {
            return (c) => { return c.place == place && c.host == host; };
        }
        public static CardPredicat anyCardAllyIn(Place place, Card sameHostCard)
        {
            return (c) => { return c.place == place && c.host == sameHostCard.host; };
        }
        public static CardPredicat anyCardHasColor(params Rarity[] acceptedColors)
        {
            return (c) =>
            {
                foreach (Rarity rarity in acceptedColors)
                    if (c.rarity == rarity)
                        return true;
                return false;
            };
        }
        public static CardPredicat anyCardInYourGraveyard(Card source) { return anyCardAllyIn(Place.graveyard, source); }
        public static CardPredicat anyCardInYourHand(Card source) { return anyCardAllyIn(Place.hand, source); }
        public static CardPredicat anyCardInYourDeck(Card source) { return anyCardAllyIn(Place.deck, source); }
        public static CardPredicat anyCardInBaseHostDeck(Card source) { return (c) => { return c.host == source.baseHost && c.place == Place.deck; }; }

        static Random randomiser = new Random();
        public static List<Card> randomCardsFrom(List<Card> originalList, int nCount)
        {
            List<Card> res = new List<Card>();
            List<Card> from = originalList;
            for (int i = 0; i < nCount; ++i)
            {
                if (from.Count <= 0)
                    return res;
                int geted = randomiser.Next(from.Count);
                res.Add(from[geted]);
                from.RemoveAt(geted);
            }
            return res;
        }
        public static List<Unit> randomUnitsFrom(List<Unit> originalList, int nCount)
        {
            List<Unit> res = new List<Unit>();
            List<Unit> from = originalList;
            for (int i = 0; i < nCount; ++i)
            {
                if (from.Count <= 0)
                    return res;
                int geted = randomiser.Next(from.Count);
                res.Add(from[geted]);
                from.RemoveAt(geted);
            }
            return res;
        }
        public static Unit randomUnitFrom(List<Unit> originalList)
        {
            List<Unit> res = randomUnitsFrom(originalList, 1);
            return res.Count == 0 ? null : res[0];
        }
        public static Card randomCardFrom(List<Card> originalList)
        {
            List<Card> res = randomCardsFrom(originalList, 1);
            return res.Count == 0 ? null : res[0];
        }
        public static List<Unit> lowestUnits(List<Unit> originalList)
        {
            if (originalList.Count == 0)
                return new List<Unit>();
            if (originalList.Count == 1)
                return originalList;
            // find min power
            int minPower = originalList[0].power;
            for (int i = 1; i < originalList.Count; ++i)
                if (minPower > originalList[i].power)
                    minPower = originalList[i].power;
            List<Unit> res = new List<Unit>();
            foreach (Unit u in originalList)
                if (u.power == minPower)
                    res.Add(u);
            return res;
        }
        public static List<Unit> highestUnits(List<Unit> originalList)
        {
            if (originalList.Count == 0)
                return new List<Unit>();
            if (originalList.Count == 1)
                return originalList;
            // find min power
            int maxPower = originalList[0].power;
            for (int i = 1; i < originalList.Count; ++i)
                if (maxPower < originalList[i].power)
                    maxPower = originalList[i].power;
            List<Unit> res = new List<Unit>();
            foreach (Unit u in originalList)
                if (u.power == maxPower)
                    res.Add(u);
            return res;
        }
        public static Unit lowestUnit(List<Unit> originalList) { 
            var res = randomUnitsFrom(lowestUnits(originalList), 1);
            return res.Count == 0 ? null : res[0];
        }
        public static Unit highestUnit(List<Unit> originalList) {
            var res = randomUnitsFrom(highestUnits(originalList), 1);
            return res.Count == 0 ? null : res[0];
        }
    }
}
