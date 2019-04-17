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
                    SpawnSpecial.MahakamAle, SpawnSpecial.MahakamAle, SpawnSpecial.MahakamAle, 
                    SpawnSpecial.Spores, SpawnSpecial.Spores, SpawnSpecial.Spores, 
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
                int nCount = 10;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.TuirseachArcher);
                nCount = 2;
                while (nCount-- > 0)
                    deck.Add(SpawnUnit.Emissary);

                deck.AddRange(new List<Card>() { 
                    SpawnSpecial.GoldenFroth, SpawnSpecial.GoldenFroth, SpawnSpecial.GoldenFroth, SpawnSpecial.GoldenFroth, SpawnSpecial.GoldenFroth
                });

                return deck;
            }
        }
    }
}
