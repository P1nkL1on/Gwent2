using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class UnitStatus
    {
        bool _isSpy = false;
        bool _isLocked = false;
        public int armor = 0;
        public bool isSpy { get { return _isSpy; } set { _isSpy = value; } }
        public bool isLocked { get { return _isLocked; } set { _isLocked = value; } }

        public override string ToString()
        {
            if (armor == 0 && !_isSpy && !_isLocked)
                return "[NONE]";
            return String.Format("[{0}{1}armor={2}]",
                (_isSpy? "SPYING " : ""),
                (_isLocked? "LOCKED " : ""),
                armor);
        }
        public string ToStringBattlefield()
        {
            if (armor == 0 && !_isSpy && !_isLocked)
                return "";
            return String.Format("[{0}{1}{2}]",
                (_isSpy ? " SPY " : ""),
                (_isLocked ? " LOCK " : ""),
                (armor > 0)? "+" + armor : "");
        }
        public void ToggleSpying()
        {
            isSpy = !isSpy;
        }
        public void ToggleLocking()
        {
            isLocked = !isLocked;
        }
        public void Clear()
        {
            _isSpy = _isLocked = false;
            armor = 0;
        }
    }
}
