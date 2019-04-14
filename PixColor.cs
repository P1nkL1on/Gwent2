using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gwent2
{
    struct PixColor
    {
        public char _symbol;
        public int _fore;
        public int _back;
        public Color _colorApproach;

        public PixColor(char symbol, int fore, int back, Color colorApproach)
        {
            _symbol = symbol;
            _fore = fore;
            _back = back;
            _colorApproach = colorApproach;
        }
        public PixColor(char symbol, int fore, int back)
        {
            _symbol = symbol;
            _fore = fore;
            _back = back;
            _colorApproach = Color.Black;
        }

        public char Paint()
        {
            ApplyColorsToConsole();

            Console.Write(_symbol);
            return _symbol;
        }

        public void ApplyColorsToConsole()
        {
            if ((int)Console.ForegroundColor != _fore)
                Console.ForegroundColor = (ConsoleColor)_fore;

            if ((int)Console.BackgroundColor != _back)
                Console.BackgroundColor = (ConsoleColor)_back;
        }

        public override string ToString()
        {
            return String.Format("R{0} G{1} B{2} {3}_{4}_{5}", 
                _colorApproach.R, _colorApproach.G, _colorApproach.B, 
                _fore, _back, _symbol);
        }
    }
}
