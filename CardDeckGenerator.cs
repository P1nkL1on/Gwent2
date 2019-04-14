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
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.TuirseachBearmaster);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.TuirseachHunter);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.TuirseachVeteran);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.AnCraiteWarrior);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.TuirseachSkirmisher);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.TuirseachAxeman);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.PriestessOfFreya);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.DrummondWarmonger);
                for (int i = 0; i < 3; ++i)
                    deck.Add(SpawnUnit.DimunPirateCaptain);
                for (int i = 0; i < 11; ++i)
                    deck.Add(SpawnUnit.DimunPirate);

                Random rnd = new Random();
                //foreach (Card c in deck)
                //    if (rnd.Next(4) == 0)
                //        c.place = Place.discard;
                return deck;
            }
        }
    }
}
