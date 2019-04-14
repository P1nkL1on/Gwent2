using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gwent2
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.CursorSize = 5;
        //    Console.WriteLine(Console.CursorSize);
        //    Console.WriteLine("Why you gay?");
        //    Console.ReadLine();
        //}
        static void Main(string[] args)
        {
            PixDrawer.SetFullScreen();
            //PixDrawer p = PixDrawer.FromPalette("dd");
            //p.drawRectangle(new Rectangle(5, 6, 15, 10), Color.DarkCyan, PixDrawer.rectangleStyleWithBorder);
            //Console.ReadLine();
            //PixDrawer.testPixDrawer();

            Match newgame = new Match(
                new List<Player>() { new PlayerHuman("Bonnie"), new PlayerAI("Jonson Bot") },
                new List<List<Card>>() { Deck.SkelligeTest, Deck.SkelligeTest }
                );
        }
    }
}
