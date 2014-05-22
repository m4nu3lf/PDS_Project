using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDS_Project_Server
{
    public partial class ServerGUI : Form
    {
        public ServerGUI()
        {
            InitializeComponent();
            Resize += ServerGUI_Resize;

            // Notify icon initialization
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "PDS_Project_Server";
            notifyIcon.Icon = new Icon("tray.ico");
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ServerGUI_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private NotifyIcon notifyIcon;
    }

}
