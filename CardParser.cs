using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gwent2
{
    class CardParser
    {
        //public static Unit Ermion
        //{
        //    get
        //    {
        //        Unit self = new Unit();
        //        self.setAttributes(Clan.skellige, Rarity.gold, "Ermion");
        //        self.setUnitAttributes(10, Tag.support, Tag.clanAnCraite);

        //        return self;
        //    }
        //}
        //public static Special Swallow
        //{
        //    get
        //    {
        //        Special spec = new Special();
        //        spec.setAttributes(Clan.neutral, Rarity.bronze, "Swallow");
        //        spec.setSpecialAttributes(Tag.alchemy, Tag.item);

        //        return spec;
        //    }
        //}
        //public static Leader HaraldtheCripple
        //{
        //    get
        //    {
        //        Leader self = new Leader();
        //        self.setAttributes(Clan.skellige, Rarity.gold, "Harald the Cripple");
        //        self.setUnitAttributes(6, Tag.clanAnCraite);
        //        self.setLeaderAttributes();

        //        return self;
        //    }
        //}

        public static void test()
        {

            Rarity currentRare = Rarity.none;
            Clan currentClan = Clan.nilfgaard;

            Parser currentType = Parser.units;

            int linesOfCard = 0;
            string[] lines = File.ReadAllLines("../Parse/Nilf.txt");
            List<string> res = new List<string>();
            List<string> currentCard = new List<string>();

            foreach (string line in lines)
            {
                if (line.IndexOf("--") == 0)
                {
                    string rest = line.Substring(2);
                    if (rest == "GOLD") currentRare = Rarity.gold;
                    if (rest == "SILVER") currentRare = Rarity.silver;
                    if (rest == "BRONZE") currentRare = Rarity.bronze;
                    if (rest == "SPECIALS") currentType = Parser.specials;
                    if (rest == "UNITS") currentType = Parser.units;
                    if (rest == "LEADERS") currentType = Parser.leaders;
                    continue;
                }
                if (line == "")
                {
                    if (linesOfCard == 2)
                    {
                        res.AddRange(currentCard);
                        res.AddRange(new List<string>() { "\t\t", "\t\treturn " + (currentType == Parser.specials ? "spec;" : "self;"), "\t}", "}" });
                        currentCard.Clear();
                        linesOfCard = 0;
                    }
                    continue;
                }
                if (linesOfCard == 0)
                {
                    linesOfCard++;
                    string name = line;
                    bool needchanges = line.Last() == '?';
                    if (needchanges)
                    {
                        currentCard.Add("// Suggest anything to improove this card!");
                        name = name.Substring(0, name.Length - 1);
                    }
                    string nameNoSpace = "";
                    foreach (char c in name)
                        if (Card.Alphabet.IndexOf(c) >= 0)
                            nameNoSpace += c;
                    switch (currentType)
                    {
                        case Parser.units: currentCard.AddRange(new List<string>() { "public static Unit " + nameNoSpace, "{", "\tget", "\t{", "\t\tUnit self = new Unit();" }); break;
                        case Parser.leaders: currentCard.AddRange(new List<string>() { "public static Leader " + nameNoSpace, "{", "\tget", "\t{", "\t\tLeader self = new Leader();" }); break;
                        default: currentCard.AddRange(new List<string>() { "public static Special " + nameNoSpace, "{", "\tget", "\t{", "\t\tSpecial spec = new Special();" }); break;
                    }
                    currentCard.Add(String.Format("\t\t{0}.setAttributes(Clan.{1}, Rarity.{2}, \"{3}\");",
                        currentType == Parser.specials ? "spec" : "self", currentClan.ToString(), currentRare.ToString(), name));
                    //self.setAttributes(Clan.skellige, Rarity.gold, "Harald the Cripple");
                    continue;
                }
                if (linesOfCard == 1)
                {
                    linesOfCard++;
                    List<string> tagsAndPower = line.Split(' ').ToList();
                    int power = 0;
                    if (currentType != Parser.specials)
                    {
                        power = int.Parse(tagsAndPower.Last());
                        tagsAndPower.Remove(tagsAndPower.Last());
                    }
                    //self.setUnitAttributes(6, Tag.clanAnCraite);
                    //spec.setSpecialAttributes(Tag.alchemy, Tag.item);
                    string tagString = "";
                    foreach (string tag in tagsAndPower)
                        if (tag != "Leader" && tag != "Special")
                            tagString += String.Format("Tag.{0}{1}", tag.ToLower(), tag == tagsAndPower.Last() ? "" : ", ");
                    if (power == 0)
                        currentCard.Add(String.Format("\t\tspec.setSpecialAttributes({0});", tagString));
                    else
                        currentCard.Add(String.Format("\t\tself.setUnitAttributes({1}, {0});", tagString, power));
                    if (currentType == Parser.leaders)
                        currentCard.Add("\t\tself.setLeaderAttributes();");
                    currentCard.Add("\t\t");
                    continue;
                }
                if (linesOfCard == 2)
                {
                    currentCard.Add("\t\t// Do not forget to check and RECHECK correctence of current ability,");
                    currentCard.Add("\t\t// its triggering condition and signature of delegate!");
                    currentCard.Add(String.Format("\t\t{0}.setOnDeploy ((s, f) => ", currentType == Parser.specials ? "spec" : "self"));
                    currentCard.Add("\t\t{}, \"" + line+"\");");
                    continue;
                }
            }
            string resString = "";
            foreach (string s in res) resString += s + Environment.NewLine;
            File.WriteAllText("../Parse/Nilf_parsed.txt", resString);
            int x = 10;
        }
    }
    enum Parser { units, leaders, specials }
}
