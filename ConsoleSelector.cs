using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Gwent2
{
    class ConsoleSelector
    {
        PixDrawer _drawer;
        Rectangle _rectangle;
        List<string> _values;
        string _question = "Select:";

        public ConsoleSelector(PixDrawer drawer, Rectangle where)
        {
            _drawer = drawer;
            _rectangle = where;
            _values = new List<string>();
        }
    }
}
