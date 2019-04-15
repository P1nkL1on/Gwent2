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
            Console.WriteLine(question.Length == 0? "Make a descision:" : (question + ":"));
            int index = 0;
            foreach (string v in variants)
                Console.WriteLine(String.Format("  {0} >\t{1}", ++index, v));
            int answer = int.Parse(Console.ReadLine()) - 1;
            return answer;
        }
    }
}
