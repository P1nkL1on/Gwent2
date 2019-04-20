using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Linq;

namespace Gwent2
{
    class Deck
    {
        string _name = "Unnamed deck";
        List<Card> _cards = new List<Card>();
        public List<Card> cards { get { return _cards; } }

        Deck(string Name, List<Card> Cards)
        {
            if (Name.Length > 0) _name = Name;
            _cards = Cards;
        }
        public static Deck FromCards(List<Card> Cards, string Name)
        {
            return new Deck(Name, Cards);
        }
    }

    class DeckIO
    {
        static List<Card> invokeAllCards()
        {
            // misc
            //public static void showCaseAllUnits()
            //{
            //    Type spawner = (new SpawnUnit()).GetType();
            //    foreach (var unitMethod in spawner.GetMethods())
            //    {
            //        Console.Clear();
            //        try
            //        {
            //            Console.WriteLine(((unitMethod.Invoke(new SpawnUnit(), null)) as Unit).ToFormat());
            //            Console.ReadLine();
            //        }
            //        catch (Exception e) { }
            //    }
            //}
            SpawnUnit su = new SpawnUnit();
            SpawnSpecial ss = new SpawnSpecial();
            SpawnLeader sl = new SpawnLeader();

            List<Type> tps = new List<Type>() { su.GetType(), ss.GetType(), sl.GetType() };
            List<Card> allCardsInGame = new List<Card>();
            int tIndex = 0;
            foreach (Type t in tps)
            {
                ++tIndex;
                foreach (var m in t.GetMethods())
                    try
                    {
                        Card c = null;
                        if (tIndex == 1) c = m.Invoke(su, null) as Card;
                        if (tIndex == 2) c = m.Invoke(ss, null) as Card;
                        if (tIndex == 3) c = m.Invoke(sl, null) as Card;
                        if (c != null)
                            allCardsInGame.Add(c);
                    }
                    catch (Exception e) { }
            }
            return allCardsInGame;
        }
        /// <summary>
        /// Return a deck of cards by names. If there is no card with such name, there will be NULL in its place.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        static List<Card> invokeCardsByNames(List<string> names)
        {
            List<Card> res = new List<Card>();
            var allCard = invokeAllCards();
            foreach (string name in names)
            {
                bool found = false;
                foreach (Card c in allCard)
                    if (c.name.ToUpper() == name.ToUpper())
                    {
                        res.Add(c);
                        found = true;
                        break;
                    }
                if (!found)
                    res.Add(null);
            }
            return res;
        }

        static List<string> clanNames = new List<string>() { 
            "remember to uppercase them",
            "NORTHERN REALMS",
            "SKELLIGE",
            "NILFGAARD",
            "SCOIA’TAEL",
            "MONSTERS"
        };

        public static Deck readDeckFromFile(string fileName)
        {
            List<string> _warnings = new List<string>(), _errors = new List<string>(), _cardNames = new List<string>();
            string[] deckLines = File.ReadAllLines("../Decks/" + fileName + ".txt");

            string availableCountCharacters = "123";
            string availableClanNames = ""; for (int i = 1; i < clanNames.Count; ++i) availableClanNames += clanNames[i] + ((i < clanNames.Count - 1) ? ", " : "");

            int readenIndex = 0;
            for (int i = 0; i < deckLines.Length; ++i)
            {
                string line = deckLines[i].Trim();
                if (line.Length == 0)
                    continue;
                ++readenIndex;
                if (readenIndex == 1) // read fraction and leader in first line
                {
                    string[] split = line.Split('–');
                    string fraction = split[0].Trim().ToUpper(), leader = split[1].Trim();
                    if (clanNames.IndexOf(fraction) < 0)
                        _warnings.Add(String.Format("Can not define a fraction of deck \"{0}\", because given \"{1}\".\nAcceptable fractions are: {2}", fileName, fraction, availableClanNames));
                    _cardNames.Add(leader);
                    continue;
                }
                // read a card name
                int count = availableCountCharacters.IndexOf(line.Last()) + 1;
                string cardName = line;
                if (count >= 1)
                    cardName = cardName.Substring(0, cardName.LastIndexOf(' '));
                else
                    count = 1;
                for (int cc = 0; cc < count; ++cc)
                    _cardNames.Add(cardName);
            }
            List<Card> invokedCards = invokeCardsByNames(_cardNames);

            var cards = checkDeckStandart(_cardNames, invokedCards, ref _warnings, ref _errors);

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string w in _warnings)
                Console.WriteLine(w);

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string e in _errors)
                Console.WriteLine(e);

            Console.ResetColor();
            Console.ReadLine();

            if (cards == null)
                return null;
            return Deck.FromCards(cards, fileName);
        }

        static List<Card> checkDeckStandart(List<string> loadingNames, List<Card> invokedCards, ref List<string> warnings, ref List<string> errors)
        {
            // equale to Enum Rarity, 3 is leaders
            List<Card> resDeck = new List<Card>();
            List<Dictionary<string, int>> copies 
                = new List<Dictionary<string,int>>(){
                    new Dictionary<string,int>(), 
                    new Dictionary<string,int>(), 
                    new Dictionary<string,int>(), 
                    new Dictionary<string,int>()};
            for (int i = 0; i < loadingNames.Count; ++i)
            {
                string name = loadingNames[i];
                Card c = invokedCards[i];
                if (c == null)
                {
                    string warning = String.Format("Can not invoke card \"{0}\".", name);
                    if (warnings.IndexOf(warning) < 0)warnings.Add(warning);
                    continue;
                }
                resDeck.Add(c);

                int di = (c as Leader != null)? 3 : (int)c.rarity;
                int prevCount = 0;
                if (copies[di].TryGetValue(name, out prevCount))
                    copies[di][name]++;
                else
                    copies[di].Add(name, 1);
            }
            int nCards = resDeck.Count - 1; // 1 leader is not in the deck
            if (copies[3].Count != 1)errors.Add("Deck must contain exactly one Leader!");
            if (nCards < 25)errors.Add("Deck must contain at least 25 cards!");
            if (nCards > 40)errors.Add("Deck must contain not more then 40 cards!");
            if (copies[2].Count > 4) errors.Add("Deck must contain not more then 4 Gold cards!");
            if (copies[1].Count > 6) errors.Add("Deck must contain not more then 6 Silver cards!");
            foreach (int bronzeCount in copies[1].Values.ToList())if (bronzeCount > 1)errors.Add("Deck must contain not more then 1 copiy of each Gold card!");
            foreach (int bronzeCount in copies[2].Values.ToList())if (bronzeCount > 1)errors.Add("Deck must contain not more then 1 copiy of each Silver card!");
            foreach (int bronzeCount in copies[0].Values.ToList())if (bronzeCount > 3)errors.Add("Deck must contain not more then 3 copies of each Bronze card!");
            return errors.Count > 0? null : resDeck;
        }
    }
}
