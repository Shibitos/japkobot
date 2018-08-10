using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace japkobot
{
    class Core
    {
        private Point windowLG;
        private Point windowPD;
        private bool detectedWindow = false;
        private string currentBot;
        private readonly IBot[] bots =
        {
            new DefBot(),
            new ClickerBot()
        };

        private readonly Color[] colorWindowLG =
        {
            Color.FromArgb(91, 84, 38),
            Color.FromArgb(34, 33, 19)
        };

        private readonly Color[] colorWindowPD =
        {
            Color.FromArgb(61, 57, 33),
            Color.FromArgb(34, 40, 29)
        };

        public bool RunBot(string id)
        {
            foreach (var bot in bots)
            {
                if (bot.GetId() == id)
                {
                    bot.Start(WindowLG, WindowPD);
                    CurrentBot = id;

                    return true;
                }
            }

            return false;
        }

        public void Stop()
        {
            foreach (var bot in bots)
            {
                if (bot.GetId() == CurrentBot)
                {
                    bot.Stop();
                }
            }
        }

        public bool DetectWindow()
        {
            var screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            bool foundLG = false;
            int width = screenBounds.Width * 10 / 8;
            int height = screenBounds.Height * 10 / 8;
            var screen = ScreenCapturer.CaptureArea(0, 0, width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = screen.GetPixel(x, y);
                    foreach (Color w in colorWindowLG)
                    {
                        if (Utils.CompareColors(c, w))
                        {
                            WindowLG = new Point(x, y);
                            foundLG = true;
                            break;
                        }
                    }
                    if (foundLG) break;
                }
                if (foundLG) break;
            }
            if (!foundLG) return false;

            for (int y = height - 1; y > 0; y--)
            {
                for (int x = width - 1; x > 0; x--)
                {
                    var c = screen.GetPixel(x, y);
                    foreach (Color w in colorWindowPD)
                    {
                        if (Utils.CompareColors(c, w))
                        {
                            WindowPD = new Point(x, y);
                            DetectedWindow = true;

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public Point WindowLG { get => windowLG; set => windowLG = value; }
        public Point WindowPD { get => windowPD; set => windowPD = value; }
        public string CurrentBot { get => currentBot; set => currentBot = value; }
        public bool DetectedWindow { get => detectedWindow; set => detectedWindow = value; }
    }
}
