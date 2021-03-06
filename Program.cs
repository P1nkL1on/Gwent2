﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace Gwent2
{
    class Program
    {

        static void Main(string[] args)
        {
            PixDrawer.SetFullScreen(false);


            //CardParser.test2();

            //return;
            //PixDrawer p = PixDrawer.FromPalette("dd");
            //p.drawRectangle(new Rectangle(5, 6, 15, 10), Color.DarkCyan, PixDrawer.rectangleStyleWithBorder);
            //Console.ReadLine();
            //PixDrawer.testPixDrawer();

            //PicGenerator.test();
            //PicGenerator.byScreenAndName();
            //SpawnUnit.showCaseAllUnits();

            DeckBuilder db = new DeckBuilder();

            PlayerHuman bonnie = new PlayerHuman("Bonnie"), ark = new PlayerHuman("Ark");
            PlayerAI bot = new PlayerAI("A-Bot");
            Deck bonnieDeck = db.Load("WSkellige"), enemyDeck = db.Load("Learner");

            db.Edit(bonnie, bonnieDeck);

            Match newgame = new Match(
                new List<Player>() { bonnie, bot},
                new List<Deck>() { bonnieDeck, enemyDeck});
            FieldDrawer fd = new FieldDrawer(newgame, bonnie);

            fd.setAllCardPositions();
            fd.redraw();

            newgame.Start();
        }
    }
}
