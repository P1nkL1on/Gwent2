using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class UnitStatus
    {
        public bool isSpy = false;
        public bool isLocked = false;
        public int armor = 0;

        public override string ToString()
        {
            if (armor == 0 && !isSpy && !isLocked)
                return "[NONE]";
            return String.Format("[{0}{1}armor={2}]",
                (isSpy? "SPYING " : ""),
                (isLocked? "LOCKED " : ""),
                armor);
        }
        public string ToStringBattlefield()
        {
            if (armor == 0 && !isSpy && !isLocked)
                return "";
            return String.Format("[{0}{1}{2}]",
                (isSpy ? "SPY " : ""),
                (isLocked ? "LOCK " : ""),
                (armor > 0)? "+" + armor : "");
        }
    }
}
