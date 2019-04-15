using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    delegate bool UnitPredicat (Unit target);
    delegate bool CardPredicat (Card target);

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
            return (t) => {
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
        public static UnitPredicat anyUnitSoldierOrMachine() { return (t) => { return t.hasTag(Tag.soldier) || t.hasTag(Tag.machine); }; }

        public static UnitPredicat anyUnitHasTag(params Tag[] anyOfTags)
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

        public static UnitPredicat anyUnitInBattlefield() { return anyUnitInPlace(Place.battlefield); }
        public static UnitPredicat anyOtherUnitInBattlefield(Unit source) { return anyOtherUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInBattlefield(Card source) { return anyAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInDiscrard(Card source) { return anyAllyUnitInPlace(source, Place.graveyard); }
        public static UnitPredicat anyAllyUnitInDeck(Card source) { return anyAllyUnitInPlace(source, Place.deck); }
        public static UnitPredicat anyAllyUnitInHand(Card source) { return anyAllyUnitInPlace(source, Place.hand); }
        public static UnitPredicat anyOtherAllyUnitInBattlefield(Unit source) { return anyOtherAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyEnemyUnitInBattlefield(Card source) { return anyEnemyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyOtherAllyUnitInBattlefieldHandDeck(Unit source) { return anyAllyUnitInPlace(source, true, Place.battlefield, Place.hand, Place.deck); }

        // <...> For cards

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
            return (c) => {
                foreach (Rarity rarity in acceptedColors)
                    if (c.rarity == rarity)
                        return true;
                return false;
            };
        }
        public static CardPredicat anyCardAllyInHand(Card source) { return anyCardAllyIn(Place.hand, source); }
        public static CardPredicat anyCardAllyInDeck(Card source) { return anyCardAllyIn(Place.deck, source); }
        

    }
}
