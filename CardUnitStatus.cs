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
            return String.Format("[{0}{1}armor={2}]",
                (isSpy? "SPYING " : ""),
                (isLocked? "LOCKED " : ""),
                armor);
        }
    }
}
