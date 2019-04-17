using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Deck
    {
        public static List<Card> SkelligeTest
        {
            get
            {
                List<Card> deck = new List<Card>();
                deck.AddRange(new List<Card>() { 
                    SpawnUnit.AnCraiteRaider,SpawnUnit.AnCraiteRaider,
                    SpawnUnit.AnCraiteRaider,SpawnUnit.AnCraiteRaider,
                    SpawnUnit.DrummondWarmonger, SpawnUnit.DrummondWarmonger, SpawnUnit.DrummondWarmonger, 
                    SpawnUnit.Morkvarg, SpawnUnit.Morkvarg,
                    SpawnUnit.Yoana, SpawnUnit.JuttaanDimun, SpawnUnit.Udalryk, SpawnUnit.SvanrigeTuirseach, SpawnUnit.Skjall
                });
                //int nCount = 10;
                //while (nCount-- > 0)
                //    deck.Add(SpawnUnit.DrummondShieldmaid);
                return deck;
            }
        }

        public static List<Card> ComputerTest
        {
            get
            {
                List<Card> deck = new List<Card>();
                int nCount = 15;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.Vabjorn);
                nCount = 2;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.Emissary);

                deck.AddRange(new List<Card>() { 
                    SpawnSpecial.TorrentialRain, SpawnSpecial.BitingFrost, SpawnSpecial.ImpenetrableFog,
                });

                return deck;
            }
        }
    }
}
