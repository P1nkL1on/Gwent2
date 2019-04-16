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
                "Spying", "Status for a unit played on or moved to the opposite side of the battlefield."
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

            res = ((res.Length > 0) ? "---------------" : "") + "\n" + res;
            return res;
        }
    }
}
