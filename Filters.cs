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
        public static UnitPredicat anyAllyUnitInPlace(Unit source, bool anyOther, params Place[] places)
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
        public static UnitPredicat anyAllyUnitInPlace(Unit source, Place place)
        {
            return (t) => { return t.host == source.host && t.place == place; };
        }
        public static UnitPredicat anyEnemyUnitInPlace(Unit source, Place place)
        {
            return (t) => { return t.host != source.host && t.place == place; };
        }
        public static UnitPredicat anyOtherUnitByName(Unit source) { return (t) => { return t.name != source.name; }; }
        public static UnitPredicat anyCopie(Unit source) { return (t) => { return t.name == source.name; }; }


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

        public static UnitPredicat anyOtherUnitInBattlefield(Unit source) { return anyOtherUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInBattlefield(Unit source) { return anyAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyAllyUnitInDiscrard(Unit source) { return anyAllyUnitInPlace(source, Place.discard); }
        public static UnitPredicat anyAllyUnitInDeck(Unit source) { return anyAllyUnitInPlace(source, Place.deck); }
        public static UnitPredicat anyOtherAllyUnitInBattlefield(Unit source) { return anyOtherAllyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyEnemyUnitInBattlefield(Unit source) { return anyEnemyUnitInPlace(source, Place.battlefield); }
        public static UnitPredicat anyOtherAllyUnitInBattlefieldHandDeck(Unit source) { return anyAllyUnitInPlace(source, true, Place.battlefield, Place.hand, Place.deck); }

        // <...> For cards

        public static CardPredicat anyCardIn(Place place)
        {
            return (c) => { return c.place == place; };
        }
        public static CardPredicat anyCardHostByPlayerIn(Place place, Player host)
        {
            return (c) => { return c.place == place && c.host == host; };
        }
        public static CardPredicat anyCardAllyIn(Place place, Card sameHostCard)
        {
            return (c) => { return c.place == place && c.host == sameHostCard.host; };
        }
    }
}
