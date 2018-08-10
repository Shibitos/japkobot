using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace japkobot
{
    class DefBot : IBot
    {
        private Point windowMiddle;
        private int R;
        private bool work = false;

        private readonly Color[] colorRing =
        {
            Color.FromArgb(153, 39, 41),
            Color.FromArgb(185, 38, 42),
            Color.FromArgb(107, 97, 52), //zielone main

            //Color.FromArgb(74, 66, 36) //zielone jakis odcien maly
        };

        private void MainLoop()
        {
            int minX, minY;
            double minDist;
            Point start = new Point(WindowMiddle.X - R1, WindowMiddle.Y - R1);
            int D = R1 * 2;
            while (work)
            {
                var area = ScreenCapturer.CaptureArea(start.X, start.Y, D, D);
                minX = minY = 0;
                minDist = R1;
                for (int y = 0; y < area.Height; y++)
                {
                    for (int x = 0; x < area.Width; x++)
                    {
                        var c = area.GetPixel(x, y);
                        foreach (Color w in colorRing)
                        {
                            if (Utils.CompareColors(c, w))
                            {
                                var tmpDist = Utils.CalcDist2D(x, y, R1, R1);
                                if (tmpDist < minDist)
                                {
                                    minDist = tmpDist;
                                    minX = x;
                                    minY = y;
                                }
                            }
                        }
                    }
                }
                Utils.SetCursorPos((int)(0.8 * (start.X + minX)), (int)(0.8 * (start.Y + minY)));
            }
        }

        public string GetId()
        {
            return "def";
        }

        public bool Start(Point WindowLG, Point WindowPD)
        {
            WindowMiddle = new Point((int)((WindowPD.X + WindowLG.X) / 2d) + 1, (int)((WindowPD.Y + WindowLG.Y) / 1.95d) + 1);
            R1 = (int)((WindowPD.Y - WindowLG.Y) / 7.5d) + 1;
            work = true;
            System.Threading.Thread t = new System.Threading.Thread(MainLoop);
            t.Start();

            return true;
        }

        public void Stop()
        {
            work = false;
        }
        
        public bool Work { get => work; set => work = value; }
        public int R1 { get => R; set => R = value; }
        public Point WindowMiddle { get => windowMiddle; set => windowMiddle = value; }
    }
}
