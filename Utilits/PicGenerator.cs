using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO;

namespace Gwent2
{
    class PicGenerator
    {
        // 90% zoome if right half of chrome
        // in 
        //gwent.seven7y.com/ob-cards.php?faction=Skellige&lang=en
        //
        public static void test()
        {
            PixDrawer prewiewDrawer = PixDrawer.FromPalette("dd2");
            int hei = 0;
            int maxSize = 160;
            int size = maxSize;
            foreach (string s in Directory.GetFiles("../Cards", "*.png"))//.Select(Path.GetFileName))
            {
                Bitmap pic = (Bitmap)Image.FromFile(s);
                size = maxSize;
                for (int i = 0; i < 4; ++i)
                {
                    size /= 2;
                    prewiewDrawer.drawImage(pic, new Rectangle(5, 5 + hei, size, size));
                    hei += size + 2;
                }
                pic.Dispose();
            }
            Console.ReadLine();
        }

        public static void byScreenAndName()
        {
            bool isAnimationCapturing = false;
            int folder = 0;

            Console.WindowWidth /= 2;
            PixDrawer prewiewDrawer = PixDrawer.Default();
            Rectangle drawZone = new Rectangle(20, 20, 60, 60);
            Rectangle screenZone = new Rectangle(1601, 167, 183, 304);
            ScreenTaker cam = new ScreenTaker();
            while (true)
            {
                Console.Clear();
                int frameCount = 1;
                if (isAnimationCapturing)
                {
                    Console.WriteLine("How much frames: ");
                    frameCount = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Enter to capture");
                    Console.ReadLine();
                }

                List<Bitmap> frames = new List<Bitmap>();
                for (int f = 0; f < frameCount; ++f)
                {
                    Bitmap screen = (Bitmap)cam.CaptureScreen();
                    Bitmap res = new Bitmap(screenZone.Width, screenZone.Height);
                    for (int i = 0; i < screenZone.Width; ++i)
                        for (int j = 0; j < screenZone.Height; ++j)
                            res.SetPixel(i, j, screen.GetPixel(screenZone.X + i, screenZone.Y + j));
                    screen.Dispose();
                    frames.Add(res);
                    if (isAnimationCapturing) Thread.Sleep(200);
                }
                Console.WriteLine("Drawing...");

                for (int i = 0, j = 0; i < frameCount; i ++, ++j)
                    prewiewDrawer.drawImage(frames[i], drawZone);

                Console.ResetColor();
                string answer = "";
                if (!isAnimationCapturing)
                {
                    Console.WriteLine("\n\nEnter <X> to skip saving, otherwise enter name: ");
                    answer = Console.ReadLine();
                    if ("xX".IndexOf(answer) >= 0)
                        continue;
                    frames[0].Save(String.Format("../Cards/{0}.png", answer));
                }
                else
                {
                    answer = "card" + (++folder);
                    int index = 0;
                    Directory.CreateDirectory("../Cards/" + answer);
                    foreach (Bitmap frame in frames)
                        frame.Save(String.Format("../Cards/{0}/frame{1}.png", answer, index++));
                }
            }
        }
    }
}
