using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class PlayerAI : Player
    {
        public PlayerAI() { _name = "AI"; }
        public PlayerAI(string Name)
        {
            _name = Name;
        }
    }
}
