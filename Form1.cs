using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace japkobot
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [FlagsAttribute]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        private Core core;

        public Form1()
        {
            InitializeComponent();
            core = new Core();

            RegisterHotKey(this.Handle, 0, 0, Keys.H.GetHashCode());
            RegisterHotKey(this.Handle, 1, 0, Keys.Escape.GetHashCode());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Disable screensaver
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.K) //DEV
            {
                MessageBox.Show("" + core.WindowLG.X + " : " + core.WindowLG.Y);
                MessageBox.Show("" + core.WindowPD.X + " : " + core.WindowPD.Y);
            }
            if (e.KeyCode == Keys.P)
            {
                var screen = ScreenCapturer.CaptureArea(0, 0, 1400, 1000);
                var c = screen.GetPixel(1259, 919);
                MessageBox.Show("" + c.R + " - " + c.G + " - " + c.B);
            }
            if (e.KeyCode == Keys.S)
            {
                var width = core.WindowPD.X - core.WindowLG.X;
                var height = core.WindowPD.Y - core.WindowLG.Y;
                var screen = ScreenCapturer.CaptureArea(core.WindowLG.X, core.WindowLG.Y, width, height);
                screen.Save("scr.bmp");
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF); 
                if (key == Keys.H)
                {
                    if (core.DetectedWindow)
                    {
                        var checkedButton = Controls.OfType<RadioButton>()
                                          .FirstOrDefault(r => r.Checked);
                        if (core.RunBot(checkedButton.Name))
                        {
                            labelActive.Text = "zapierdala";
                            labelActive.ForeColor = Color.Green;
                        }
                        else MessageBox.Show("Wystapil blad przy odpalaniu bociszcza");
                    }
                    else MessageBox.Show("Nie wykryto ekranu gry");
                } else if (key == Keys.Escape)
                {
                    core.Stop();
                    labelActive.Text = "nieaktywny";
                    labelActive.ForeColor = Color.Red;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            //MessageBox.Show("" + Bot.ScreenWidth + ":" + Bot.ScreenHeight);
            //pictureBox1.Image = ScreenCapturer.CaptureArea(Bot.MiddleX - Bot.R1, Bot.MiddleY - Bot.R1, Bot.R1 * 2, Bot.R1 * 2);
            //pictureBox1.Image = ScreenCapturer.CaptureArea(Bot.MiddleX - Bot.R1, Bot.MiddleY - Bot.R1, (Bot.MiddleX + Bot.R1) - Bot.ScreenWidth, (Bot.MiddleY + Bot.R1) - Bot.ScreenHeight);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Screensaver re-enabled
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            UnregisterHotKey(this.Handle, 0);
            UnregisterHotKey(this.Handle, 1);
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!core.DetectWindow()) MessageBox.Show("Wystapil blad przy wykrywaniu ekranu");
        }
    }
}
