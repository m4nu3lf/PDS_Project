using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PDS_Project_Server
{
    class Blinking
    {
        private Form _leftBorder;
        private Form _rightBorder;
        private Form _bottomBorder;
        private Form _topBorder;
        private Thread _blinkThread;
        private AutoResetEvent _blinkEvnt;
        private bool _running = true;

        public Blinking()
        {
            _running = true;
            _blinkEvnt = new AutoResetEvent(false);
            _blinkThread = new Thread(this.Run);
            _blinkThread.Start();
        }

        private void Run()
        {
            _leftBorder = new Form();
            _rightBorder = new Form();
            _bottomBorder = new Form();
            _topBorder = new Form();

            _leftBorder.FormBorderStyle = FormBorderStyle.None;
            _rightBorder.FormBorderStyle = FormBorderStyle.None;
            _bottomBorder.FormBorderStyle = FormBorderStyle.None;
            _topBorder.FormBorderStyle = FormBorderStyle.None;

            _leftBorder.ShowInTaskbar = false;
            _rightBorder.ShowInTaskbar = false;
            _bottomBorder.ShowInTaskbar = false;
            _topBorder.ShowInTaskbar = false;

            _leftBorder.TopMost = true;
            _rightBorder.TopMost = true;
            _bottomBorder.TopMost = true;
            _topBorder.TopMost = true;

            _leftBorder.BackColor = Color.Yellow;
            _rightBorder.BackColor = Color.Yellow;
            _bottomBorder.BackColor = Color.Yellow;
            _topBorder.BackColor = Color.Yellow;

            while (true)
            {
                _blinkEvnt.WaitOne();
                if (!_running) break;
                for (int i = 0; i < 3; i++)
                {
                    Show();
                    Thread.Sleep(100);
                    Hide();
                    Thread.Sleep(100);
                }
            }
        }

        private void Show()
        {
            int screenWidth = SystemInformation.PrimaryMonitorSize.Width;
            int screenHeight = SystemInformation.PrimaryMonitorSize.Height;

            int borderThikness = (int) (screenHeight * 0.01);

            _leftBorder.Show();
            _rightBorder.Show();
            _bottomBorder.Show();
            _topBorder.Show();

            _leftBorder.Left = 0;
            _leftBorder.Top = 0;
            _leftBorder.Height = screenHeight;
            _leftBorder.Width = borderThikness;

            _rightBorder.Left = screenWidth - borderThikness;
            _rightBorder.Top = 0;
            _rightBorder.Height = screenHeight;
            _rightBorder.Width = borderThikness;

            _bottomBorder.Left = 0;
            _bottomBorder.Top = screenHeight - borderThikness;
            _bottomBorder.Height = borderThikness;
            _bottomBorder.Width = screenWidth;

            _topBorder.Left = 0;
            _topBorder.Top = 0;
            _topBorder.Height = borderThikness;
            _topBorder.Width = screenWidth;


        }

        private void Hide()
        {
            _leftBorder.Hide();
            _rightBorder.Hide();
            _bottomBorder.Hide();
            _topBorder.Hide();
        }

        public void Blink()
        {
            _blinkEvnt.Set();
        }

        public void Terminate()
        {
            _running = false;
            _blinkEvnt.Set();
        }
    }
}
