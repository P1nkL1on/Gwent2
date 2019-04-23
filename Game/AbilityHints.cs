using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class AbilityHints
    {
        static List<string> keyvalues = new List<string>(){
                "Armor", "Absorbs a given amount of damage, then is removed.",
                "Spawn", "Add a card to the game.",
                "Resurrect", "Play from your graveyard.",
                "Strengthen", "Increase the base power of a unit.",
                "Discard", "Move a card from your hand to the graveyard.",
                "Heal", "If a unit's current power is lower than its base power, restore it either to base power or by the amount specified.",
                "Spying", "Status for a unit played on or moved to the opposite side of the battlefield.",
                "Hazard", "Negative row effect. Replaced by other row effects and removed on round end.",
                "Boon", "Positive row effect. Replaced by other row effects and removed on round end.",
                "Lowest", "Lowest power, ties are resolved randomly.",
                "Highest", "Highest power, ties are resolved randomly.",
                "Single-Use", "This card's ability can be used only once per game.",
                "Doomed","Status that removes the unit from the game when it leaves the battlefield.",
                "Duel", "Units take turns dealing damage equal to their power until one of them is destroyed.",
                "Charm", "Move an enemy to the opposite row.",
                "Weaken", "Decrease the base power of a unit. If it falls below 1, remove it from the game. Does not trigger Deathwish abilities.",
                "Reset", "Restore a card to its default state (as it would appear in the Deck Builder).",
                "Reveal", "Show a card to both players, then hide it back in the hand or deck.",
                "Drain", "Deal damage and boost self by the same amount.",
                "Lock", "Status that disables a unit's abilities.",
                "Truce", "If neither player has passed.",
                "Revealed", "A card in the hand that has been turned over.",
                "Conceal", "Turn over a face-up card in hand.",
                "Crew", "This unit triggers the ability of Crewed units played adjacent to it.",
                "Create", "Spawn one of 3 randomly selected cards from the specified source."
        };
        public static string addHitsTo(string sourceAbilities)
        {
            string res = "";
            string where = sourceAbilities.ToLower();
            for (int i = 0; i < keyvalues.Count; i += 2)
            {
                string abilityName = keyvalues[i].ToLower();
                if (where.IndexOf(abilityName) < 0)
                    continue;
                res += String.Format("{0}: {1}\n", keyvalues[i], keyvalues[i+1]);
            }

            res = ((res.Length > 0) ? "".PadRight(30, '_') : "") + "\n\n" + res;
            return res;
        }
    }
}
