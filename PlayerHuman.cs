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
        }

        private ConsoleWindowText decideWindow = new ConsoleWindowText(Utils.leftTextColumnWidth, Utils.leftTextColumnHeigth);

        protected override int makeDescision(List<string> variants, string question)
        {
            if (variants.Count == 1)
                return 0;
            decideWindow.ClearLogWindow();

            decideWindow.AddLog(question.Length == 0 ? "Make a descision:" : (question + ":"));
            int index = 0;
            foreach (string v in variants)
                decideWindow.AddLog(String.Format("  {0} >\t{1}", ++index, v));
            int answer = -1;
            Console.WriteLine();
            do {
                try { answer = int.Parse(Console.ReadLine()) - 1; }
                catch (Exception e) { Console.Write("Try again: "); }
            } while (answer < 0);
            return answer;
        }
    }
}
