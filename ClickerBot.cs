using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace japkobot
{
    class ClickerBot : IBot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        
        private bool work = false;

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        private void MainLoop()
        {
            uint X, Y;
            while (work)
            {
                X = (uint)System.Windows.Forms.Cursor.Position.X;
                Y = (uint)System.Windows.Forms.Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                System.Threading.Thread.Sleep(200);
            }
        }

        public string GetId()
        {
            return "clicker";
        }

        public bool Start(Point WindowLG, Point WindowPD)
        {
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
    }
}
