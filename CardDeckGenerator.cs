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
                    SpawnSpecial.BitingFrost, SpawnSpecial.BitingFrost, SpawnSpecial.BitingFrost, 
                    SpawnSpecial.ImpenetrableFog, SpawnSpecial.ImpenetrableFog, SpawnSpecial.ImpenetrableFog, 
                    SpawnSpecial.TorrentialRain, SpawnSpecial.TorrentialRain, SpawnSpecial.TorrentialRain, 
                    SpawnUnit.Emissary,
                    SpawnUnit.Ambassador,
                    SpawnSpecial.Reconnaissance, SpawnSpecial.Reconnaissance, SpawnSpecial.Reconnaissance, SpawnSpecial.Reconnaissance, 
                    SpawnSpecial.AlzursThunder, SpawnSpecial.AlzursThunder, SpawnSpecial.AlzursThunder, 
                    //SpawnSpecial.StammelfordsTremor, SpawnSpecial.StammelfordsTremor, SpawnSpecial.StammelfordsTremor,
                    SpawnUnit.TuirseachVeteran, SpawnUnit.TuirseachVeteran, SpawnUnit.TuirseachVeteran, SpawnUnit.TuirseachVeteran
                });

                return deck;
            }
        }

        public static List<Card> ComputerTest
        {
            get
            {
                List<Card> deck = new List<Card>();
                int nCount = 20;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.TuirseachBearmaster);
                nCount = 5;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.Emissary);

                return deck;
            }
        }
    }
}
