﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class DefaultDeck
    {
        public static Deck SkelligeTest
        {
            get
            {
                List<Card> cards = new List<Card>();
                cards.AddRange(new List<Card>() {
                    SpawnLeader.BranTuirseach,

                    SpawnUnit.DrummondWarmonger,
                    SpawnUnit.SvanrigeTuirseach,
                    SpawnUnit.TuirseachArcher,
                    SpawnUnit.TuirseachArcher,
                    SpawnUnit.DjengeFrett,
                    SpawnSpecial.StribogRunestone,
                    SpawnUnit.GiantBoar,
                    SpawnSpecial.OrnamentalSword
                });
                return Deck.FromCards(cards, "Skellige Test");
            }
        }

        public static Deck ComputerTest
        {
            get
            {
                List<Card> cards = new List<Card>();
                int nCount = 10;
                while (nCount-- > 0)
                    cards.Add(SpawnSpecial.Restore);
                //nCount = 2;
                //while (nCount-- > 0)
                //    deck.Add(SpawnUnit.Emissary);

                //deck.AddRange(new List<Card>() { 
                //    SpawnSpecial.TorrentialRain, SpawnSpecial.BitingFrost, SpawnSpecial.ImpenetrableFog,
                //});
                cards.Add(SpawnLeader.HaraldtheCripple);
                
                return Deck.FromCards(cards, "Computer Test");
            }
        }
        public static Deck AllGameCards
        {
            get
            {
                return Deck.FromCards(Filter.randomCardsFrom(DeckIO.invokeAllCards(), 40), "All cards");
            }
        }
    }
}
