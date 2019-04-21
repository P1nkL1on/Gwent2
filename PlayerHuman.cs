using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class PlayerHuman : Player
    {
        public PlayerHuman(string Name)
        {
            _name = Name;
            descriptionWindow.AddOffset(0, Utils.leftTextColumnHeigth / 2);
        }

        private ConsoleWindowText decideWindow = new ConsoleWindowText(Utils.leftTextColumnWidth, Utils.leftTextColumnHeigth);
        private ConsoleWindowText descriptionWindow = new ConsoleWindowText(Utils.leftTextColumnWidth, Utils.leftTextColumnHeigth);

        // with readings
        //protected override int makeDescision(ChoiseContext choise)
        //{
        //    if (choise.OptionsCount == 1)
        //        return 0;

        //    decideWindow.AddLog(choise.Question.Length == 0 ? "Make a descision:" : (choise.Question + ":"));
        //    int index = 0;
        //    foreach (string v in choise.ChoiseOptions)
        //        decideWindow.AddLog(String.Format("  {0} >\t{1}", ++index, v));
        //    int answer = -1;
        //    Console.WriteLine();
        //    do {
        //        try { answer = int.Parse(Console.ReadLine()) - 1; }
        //        catch (Exception e) { Console.Write("Try again: "); }
        //    } while (answer < 0);

        //    decideWindow.AddLog(choise.DescriptionForOption(answer));
        //    Console.ReadLine();
        //    decideWindow.ClearLogWindow();
        //    return answer;
        //}


        protected override int makeDescision(ChoiseContext choise)
        {
            return PlayerChoiseDialog.classicSmallDialog(choise, decideWindow, descriptionWindow);
        }
    }
}
