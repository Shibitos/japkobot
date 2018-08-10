using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace japkobot
{
    class Utils
    {
        public static bool CompareColors(Color pixel, Color color)
        {
            return (pixel.R == color.R && pixel.G == color.G && pixel.B == color.B);
        }

        public static double CalcDist2D(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static void SetCursorPos(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new Point(x, y);
        }
    }
}
