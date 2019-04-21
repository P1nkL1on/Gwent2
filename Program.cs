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

            //PixDrawer p = PixDrawer.FromPalette("dd");
            //p.drawRectangle(new Rectangle(5, 6, 15, 10), Color.DarkCyan, PixDrawer.rectangleStyleWithBorder);
            //Console.ReadLine();
            //PixDrawer.testPixDrawer();

            //PicGenerator.test();
            //PicGenerator.byScreenAndName();
            //SpawnUnit.showCaseAllUnits();

            PlayerHuman bonnie = new PlayerHuman("Bonnie");


            DeckBuilder db = new DeckBuilder();
            //db.Edit(bonnie);
            //Console.ReadLine();

            Match newgame = new Match(
                new List<Player>() { bonnie, new PlayerAI("Jonson Bot") },
                new List<Deck>() { DefaultDeck.SkelligeTest, DefaultDeck.ComputerTest }
                );
            FieldDrawer fd = new FieldDrawer(newgame, bonnie);

            fd.setAllCardPositions();
            fd.redraw();

            newgame.Start();
        }
    }
}
