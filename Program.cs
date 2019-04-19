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
            PixDrawer.SetFullScreen(false);

            Random rnd = new Random();
            while (true)
            {
                Effects.Trajectory(new Point(rnd.Next(200), rnd.Next(100)), new Point(rnd.Next(200), rnd.Next(100)), ConsoleColor.Green, 5, 4, 20, 15);
                //Target.drawArrow(new Point(rnd.Next(40), rnd.Next(40)), new Point(40 + rnd.Next(40), 40 + rnd.Next(40)));
                Console.ReadLine();
            }

            //PixDrawer p = PixDrawer.FromPalette("dd");
            //p.drawRectangle(new Rectangle(5, 6, 15, 10), Color.DarkCyan, PixDrawer.rectangleStyleWithBorder);
            //Console.ReadLine();
            //PixDrawer.testPixDrawer();

            //PicGenerator.test();
            //PicGenerator.byScreenAndName();
            //SpawnUnit.showCaseAllUnits();

            PlayerHuman bonnie = new PlayerHuman("Bonnie");

            Match newgame = new Match(
                new List<Player>() { bonnie, new PlayerAI("Jonson Bot") },
                new List<List<Card>>() { Deck.SkelligeTest, Deck.ComputerTest }
                );
            FieldDrawer fd = new FieldDrawer(newgame, bonnie);

            fd.setAllCardPositions();
            fd.redraw();

            newgame.Start();
        }
    }
}
