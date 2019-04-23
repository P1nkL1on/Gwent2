using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Gwent2
{
    class UtilsDrawing
    {

        public static PixDrawer please = PixDrawer.Default();

        static Color bronzeColor = Color.FromArgb(122, 122, 0);
        static Color silverColor = Color.FromArgb(190, 192, 192);
        static Color goldenColor = Color.FromArgb(255, 122, 0);

        static Color neutralColor = Color.FromArgb(150, 40, 22);
        static Color skelligeColor = Color.FromArgb(110, 12, 122);
        static Color monsterColor = Color.FromArgb(220, 82, 50);
        static Color nilfgaardColor = Color.FromArgb(50, 42, 20);
        static Color northernColor = Color.FromArgb(30, 42, 222);

        public static Color colorOfRarity(Rarity rare)
        {
            switch (rare)
            {
                case Rarity.gold: return goldenColor;
                case Rarity.silver: return silverColor;
                case Rarity.bronze: return bronzeColor;
                default: return Color.FromArgb(0, 122, 255);
            }
        }
        public static Color colorsOfClan(Clan clan)
        {
            switch (clan)
            {
                case Clan.skellige: return skelligeColor;
                case Clan.monsters: return monsterColor;
                case Clan.nilfgaard: return nilfgaardColor;
                case Clan.scoetaels: return neutralColor;
                case Clan.northen: return northernColor;

                default: return neutralColor;
            }
        }
        public static ConsoleColor colorsOfPower(Unit unit)
        {
            if (unit.isDamaged) return ConsoleColor.Red;
            if (unit.isBoosted) return ConsoleColor.Green;
            return ConsoleColor.White;
        }

    }
    //class RedrawContainer
    //{
    //    public RedrawContainer(Card Source) { source = Source; }
    //    public Card source;
    //    public bool needRedraw = true;
    //}
    //class CardDrawer
    //{
    //    public CardDrawer(Match watchingFor)
    //    {
    //        // add all cards to redrawers
    //        foreach (Card c in watchingFor.cards)
    //            redrawContainers.Add(new RedrawContainer(c));
    //        // . . .
    //        Console.CursorVisible = false;
    //        //
    //        Paint();
    //    }

    //    List<RedrawContainer> redrawContainers = new List<RedrawContainer>();
    //    //PixDrawer drawer = PixDrawer.Default();
        

    //    void Paint()
    //    {
    //        int x = 5, y = 25;
    //        foreach (RedrawContainer rc in redrawContainers)
    //        {
    //            if (x > 100) { y += 9; x = 14; } else x += 9;
    //            drawSmallCard(rc, x, y);
    //        }
    //        Console.ResetColor();
    //    }



    //    public void drawSmallCard(RedrawContainer r, int x, int y)
    //    {
    //        if (!r.needRedraw)
    //            return;
    //        r.needRedraw = false;

    //        Card card = r.source;
    //        const int width = 9;
    //        const int heigth = 6;

    //        // border
    //        drawer.drawRectangle(new Rectangle(x, y, width, heigth), colorOfRarity(card.rarity), PixDrawer.rectangleStyleWithBorderDouble);

    //        //name
    //        string name = card.name;
    //        if (name.Length > width) name = name.Substring(0, width);
    //        setCursor(x, y - 1);
    //        Console.Write(name);

    //        // picture
    //        try
    //        {
    //            Bitmap b = (Bitmap)Image.FromFile("../Pixels/Undefined" + ".png");
    //            //Bitmap b = (Bitmap)Image.FromFile("../Cards/" + card.name+".png");
    //            drawer.drawImage(b, new Rectangle(x + 1, y + 1, width -2, heigth - 1));
    //            b.Dispose();
    //        }
    //        catch (Exception e)
    //        {
    //            Random rnd = new Random();
    //            for (int i = 1; i < width - 1; ++i) for (int j = 1; j < heigth; ++j)
    //                {
    //                    setCursor(x + i, y + j);
    //                    drawer.drawPixel(Color.FromArgb(rnd.Next(255), rnd.Next(120), rnd.Next(120))).Paint();
    //                }
    //        }
    //        // unit power and state
    //        Unit unit = card as Unit;
    //        //if (unit != null)
    //        {
    //            string power = card.power.ToString();
    //            if (power == "0") power = "*";
    //            int flagWidth = power.Length + 2;
    //            var clanColor = colorsOfClan(card.clan);
    //            for (int flagInd = 0; flagInd < flagWidth; ++flagInd)
    //            {
    //                setCursor(x + flagInd, y + heigth);
    //                PixColor clr = drawer.drawPixel(clanColor);
    //                if (flagInd % 2 == 1) clr = clr.Reversed;

    //                if (flagInd == 0 || flagInd == flagWidth - 1)
    //                {
    //                    clr.Paint();
    //                    continue;
    //                }
    //                clr.ApplyColorsToConsole();
    //                Console.ForegroundColor = (unit != null)? colorsOfPower(unit) : ConsoleColor.Yellow;
    //                Console.Write(power[flagInd - 1]);
    //            }
    //        }
    //    }
    //    Point lastSetedCursorPoint = new Point(0, 0);
    //    void setCursor(int x, int y)
    //    {
    //        lastSetedCursorPoint = new Point(x, y);
    //        Console.SetCursorPosition(x, y);
    //    }
    //    void setCursorNextLine()
    //    {
    //        setCursor(lastSetedCursorPoint.X, lastSetedCursorPoint.Y + 1);
    //        lastSetedCursorPoint = new Point(lastSetedCursorPoint.X, lastSetedCursorPoint.Y + 1);
    //    }

    //}
}
