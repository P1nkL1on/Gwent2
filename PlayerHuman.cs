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

        protected override int makeDescision(List<string> variants, string question)
        {
            if (variants.Count == 1)
                return 0;
            Console.WriteLine(question.Length == 0 ? "Make a descision:" : (question + ":"));
            int index = 0;
            foreach (string v in variants)
                Console.WriteLine(String.Format("  {0} >\t{1}", ++index, v));
            int answer = -1;
            do {
                try { answer = int.Parse(Console.ReadLine()) - 1; }
                catch (Exception e) { Console.Write("Try again: "); }
            } while (answer < 0);
            return answer;
        }
    }
}
