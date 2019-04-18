using System;
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
            PixDrawer.SetFullScreen();

            //Random rnd = new Random();
            //while (true)
            //{
            //    Effects.Trajectory(new Point(rnd.Next(40), rnd.Next(40)), new Point(20 + rnd.Next(40), 20 + rnd.Next(40)), ConsoleColor.Green, 5, 2, 20, 25);
            //    Console.ReadLine();
            //}

            //PixDrawer p = PixDrawer.FromPalette("dd");
            //p.drawRectangle(new Rectangle(5, 6, 15, 10), Color.DarkCyan, PixDrawer.rectangleStyleWithBorder);
            //Console.ReadLine();
            //PixDrawer.testPixDrawer();

            //PicGenerator.test();
            //PicGenerator.byScreenAndName();
            //SpawnUnit.showCaseAllUnits();

            Match newgame = new Match(
                new List<Player>() { new PlayerHuman("Bonnie"), new PlayerAI("Jonson Bot") },
                new List<List<Card>>() { Deck.SkelligeTest, Deck.ComputerTest }
                );
            FieldDrawer fd = new FieldDrawer(newgame);
            //CardDrawer cd = new CardDrawer(newgame);
            newgame.Start();
        }
    }
}
