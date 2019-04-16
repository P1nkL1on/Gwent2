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
                int prioritet0 = 0;
                int prioritet1 = 3;

                List<Card> deck = new List<Card>();

                for (int i = 0; i < 10; ++i)
                    deck.Add(SpawnSpecial.Reconnaissance);
                for (int i = 0; i < 5; ++i)
                    deck.Add(SpawnSpecial.AlzursThunder);
                for (int i = 0; i < 5; ++i)
                    deck.Add(SpawnUnit.HeymaeySpearmaiden);

                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.TuirseachBearmaster);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.TuirseachHunter);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.TuirseachArcher);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.TuirseachVeteran);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.AnCraiteWarrior);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.TuirseachSkirmisher);
                for (int i = 0; i < 7; ++i)
                    deck.Add(SpawnUnit.TuirseachAxeman);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.PriestessOfFreya);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.DrummondWarmonger);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.DimunPirateCaptain);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.DimunPirate);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.DimunCorsair);
                for (int i = 0; i < prioritet0; ++i)
                    deck.Add(SpawnUnit.BerserkerMarauder);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.AnCraiteWarcrier);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.AnCraiteRaider);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.AnCraiteMarauder);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.AnCraiteBlacksmith);
                for (int i = 0; i < prioritet1; ++i)
                    deck.Add(SpawnUnit.AnCraiteGreatsword);


                Random rnd = new Random();
                //foreach (Card c in deck)
                //    if (rnd.Next(4) == 0)
                //        c.place = Place.discard;
                return deck;
            }
        }
    }
}
