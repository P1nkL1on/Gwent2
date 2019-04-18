using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace Gwent2
{
    class PixDrawer
    {
        public static void SetFullScreen()
        {
            ScreenSpacer.SetConsoleFont();
            int offset = 20;
            Console.SetBufferSize(Console.LargestWindowWidth - offset, Console.LargestWindowHeight * 50);
            Console.SetWindowSize(Console.LargestWindowWidth - offset, Console.LargestWindowHeight);
        }

        string _symbols = "░▒▓";
        //string _symbols = "`.-',-=+!\"#$%&*680?@";

        BitArray _colors = new BitArray(16, true);

        ScreenTaker st = new ScreenTaker();

        public List<PixColor> pallete;

        public PixDrawer(string _symbols, BitArray _colors)
        {
            this._symbols = _symbols;
            this._colors = _colors;
            pallete = createNewPalette("");

        }
        public PixDrawer(string _symbols)
        {
            this._symbols = _symbols;
            pallete = createNewPalette("");
        }

        static string palleteExtension = ".PLT";

        public PixDrawer(List<PixColor> pallete)
        {
            this.pallete = pallete;
        }


        public static PixDrawer Default()
        {
            return FromPalette("dd");
        }
        public static PixDrawer FromPalette(string palleteFileName)
        {
            return new PixDrawer(readPalleteFromFile("../Pallets/" + palleteFileName + palleteExtension));
        }



        public void Clear()
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        public void FillRectangle(PixColor clr, Rectangle r)
        {
            for (int i = 0; i < r.Height; ++i)
            {
                Console.SetCursorPosition(r.X, r.Y + i);
                char fill = clr.Paint();
                Console.Write("".PadLeft(r.Width - 1, fill));
            }
        }
        List<PixColor> CalibrateInRectangle(Rectangle r)
        {
            Clear();
            Console.WriteLine("Calibration requested. It will take near minute.\nPlease, before start move window to the top left screen corner.\nPress <Enter> to proceed calibration.");
            Console.ReadLine();
            List<PixColor> res = new List<PixColor>();
            int nCombo = 0;
            for (int i = 0; i < _colors.Length; ++i)
                for (int j = 0; j < _colors.Length; ++j)
                    if (_colors[i] && _colors[j])
                        foreach (char symbol in _symbols)
                        {
                            PixColor px = new PixColor(symbol, j, i);
                            FillRectangle(px, r);
                            px._colorApproach = approximateColorOfCurrentPrinted();
                            res.Add(px);
                            ++nCombo;
                            //if (i == j)
                            //    break;
                        }
            Clear();
            Console.WriteLine("Calibration finished with iteration count = " + nCombo);
            return res;
        }

        Color approximateColorOfCurrentPrinted()
        {
            Console.CursorVisible = false;
            Bitmap s = (Bitmap)st.CaptureScreen();
            //using (Graphics g = Graphics.FromImage(s))
            //{
            //    g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(80, 80, 160, 150));
            //    s.Save("res.png");
            //}
            int R = 0, G = 0, B = 0;
            for (int i = 80; i < 160; ++i)
                for (int j = 80; j < 150; ++j)
                {
                    Color pix = s.GetPixel(i, j);
                    R += pix.R; G += pix.G; B += pix.B;
                }
            s.Dispose();
            return Color.FromArgb((int)(R / 5600.0), (int)(G / 5600.0), (int)(B / 5600.0));
        }

        List<PixColor> createNewPalette(string saveFileName)
        {
            Console.CursorVisible = true;
            Rectangle calRec = new Rectangle(10, 5, 32, 18);
            var pallete = CalibrateInRectangle(calRec);
            Console.Write("\nEnter a name to save a pallete: ");
            string name = Console.ReadLine();
            Console.Write("Are you sure to save your new pallete with name '" + name + "'? [y/n]");
            if ("yY".IndexOf(Console.ReadKey().KeyChar) < 0)
            {
                Console.WriteLine("Aborting saving!");
                return pallete;
            }
            writePaletteToFile(pallete, "../Pallets/" + name + palleteExtension);
            return pallete;
        }

        static void writePaletteToFile(List<PixColor> pallete, string path)
        {
            string resText = "";
            foreach (PixColor px in pallete)
                resText += px.ToString() + Environment.NewLine;

            File.WriteAllText(path, resText);
        }

        static List<PixColor> readPalleteFromFile(string path)
        {

            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(path);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can not open file: " + e.Message);
                Console.ResetColor();
                return new List<PixColor>();
            }
            List<PixColor> res = new List<PixColor>();
            foreach (string s in lines)
            {
                try
                {
                    string[] sep = s.Split(' ');
                    Color apr = Color.FromArgb(
                        int.Parse(sep[0].Substring(1)),
                        int.Parse(sep[1].Substring(1)),
                        int.Parse(sep[2].Substring(1)));
                    string[] vals = sep[3].Split('_');
                    res.Add(new PixColor(vals[2][0], int.Parse(vals[0]), int.Parse(vals[1]), apr));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Can not read line: " + s + ",\nBecause of: " + e.Message);
                    Console.ResetColor();
                    return new List<PixColor>();
                }
            }
            Console.WriteLine("Pallete " + path + " loaded!");
            return res;
        }

        Random rnd = new Random();

        PixColor getRandomColor()
        {
            return pallete[rnd.Next(pallete.Count)];
        }

        public void drawRandomSquare(Rectangle place)
        {
            for (int j = 0; j < place.Height; j++)
            {
                setCursor(place, j);
                for (int i = 0; i < place.Width; i++)
                    getRandomColor().Paint();
            }
        }

        public void drawTestSquare(Rectangle place)
        {
            Color lastColor = Color.Black;
            PixColor lastUsed = new PixColor(' ', 0, 0, lastColor);

            for (int j = 0; j < place.Height; j++)
            {
                setCursor(place, j);
                for (int i = 0; i < place.Width; i++)
                {
                    Color pix = Color.FromArgb((int)(255.0 / place.Height * j), (int)(255.0 / place.Width * i), (int)(255.0 / (place.Width * place.Height) * i * j));
                    if (pix != lastColor)
                        lastUsed = getClosestColor(pix);

                    lastColor = pix;
                    lastUsed.Paint();
                }
            }
        }

        void setCursor(Rectangle rec, int line)
        {
            Console.SetCursorPosition(rec.X, rec.Y + Math.Min(rec.Height, Math.Max(0, line)));
        }

        public int drawImage(Bitmap image, Point leftTop, int Heigth)
        {
            float consoleCompression = 1.5f;

            float k = (float)Heigth / image.Height;
            int newWidth = (int)(image.Width * k * consoleCompression);
            drawImage(image, new Rectangle(leftTop.X, leftTop.Y, newWidth, Heigth));
            return newWidth;
        }

        PixColor getClosestColor(Color clr)
        {
            PixColor res = new PixColor(' ', 0, 0);
            int bestDiff = clr.R + clr.G + clr.B;

            foreach (PixColor px in pallete)
            {
                //int main = (clr.R > clr.G && clr.R > clr.B) ? 1 : (clr.G > clr.B) ? 2 : 3;
                int diff =
                    Math.Abs(px._colorApproach.R - clr.R) //* (main == 1 ? 2 : 1)
                    + Math.Abs(px._colorApproach.G - clr.G) //* (main == 2 ? 2 : 1)
                    + Math.Abs(px._colorApproach.B - clr.B); //* (main == 3 ? 2 : 1);
                if (diff >= bestDiff)
                    continue;
                bestDiff = diff;
                res = px;
            }
            return res;
        }

        public void drawImage(Bitmap image, Rectangle place)
        {
            bool smooth = false;

            Bitmap image2 = (smooth) ? new Bitmap(image, new Size(place.Width, place.Height)) : image;

            for (int j = 0; j < place.Height; j++)
            {
                setCursor(place, j);
                for (int i = 0; i < place.Width; i++)
                    getClosestColor(image2.GetPixel(
                            (int)((float)(i + 1) / place.Width * (image2.Width)) - 1,
                            (int)((float)(j + 1) / place.Height * (image2.Height)) - 1
                        )).Paint();
            }
        }

        public static string rectangleStyleSimple = "      ";
        public static string rectangleStyleStars = "******";
        public static string rectangleStyleWithBorder = "─│┌┐└┘";
        public static string rectangleStyleWithBorderDouble = "═║╔╗╚╝";

        public void drawRectangle(Rectangle place, Color color, string style)
        {
            PixColor clr = getClosestColor(color);
            clr.ApplyColorsToConsole();
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(place.X, place.Y);
            Console.Write(style[2] + "".PadLeft(place.Width - 2, style[0]) + style[3]);
            for (int i = 1; i <= place.Height - 1; i++)
            {
                Console.SetCursorPosition(place.X, place.Y + i);
                Console.Write(style[1]);
                Console.SetCursorPosition(place.X + place.Width - 1, place.Y + i);
                Console.Write(style[1]);
            }
            Console.SetCursorPosition(place.X, place.Y + place.Height);
            Console.Write(style[4] + "".PadLeft(place.Width - 2, style[0]) + style[5]);
            //Console.ResetColor();
        }

        public ConsoleColor getClosestFore(Color color)
        {
            return (ConsoleColor)getClosestColor(color)._fore;
        }
        public ConsoleColor getClosestBack(Color color)
        {
            return (ConsoleColor)getClosestColor(color)._back;
        }

        public void drawRectangle(Rectangle place, Color color)
        {
            drawRectangle(place, color, rectangleStyleSimple);
        }


        public static void testPixDrawer()
        {

            PixDrawer.SetFullScreen();

            PixDrawer pd;
            PixDrawer def = PixDrawer.FromPalette("dd");

            Console.WriteLine("<1> - Use \"defaultDots\" pallete;\n<2> - Load another existed pallete;\n<3> - Create new pallete;");
            char answer = Console.ReadKey().KeyChar;

            switch (answer)
            {
                case '2':
                    Console.Write("Enter a name of pallete: ");
                    pd = PixDrawer.FromPalette(Console.ReadLine()); Console.ReadLine(); break;
                case '3':
                    pd = new PixDrawer("░▒▓"); break;
                default:
                    pd = def; break;
            }

            int size = 145;
            bool compareHorizontal = true;

            do
            {
                pd.Clear();
                Console.Write("Write destination of source image: ");
                string imagePath = Console.ReadLine();
                int offset = pd.drawImage(new Bitmap(imagePath), new Point(2, 2), size);

                if (pd != def)
                    def.drawImage(new Bitmap(imagePath),
                        new Point(
                            2 + (compareHorizontal ? (offset + 2) : 0),
                            2 + (!compareHorizontal ? (size + 2) : 0)),
                        size);

            } while ("qQ".IndexOf(Console.ReadKey().KeyChar) < 0);
        }
    }
}
