using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

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
            notifyIcon.Icon = new Icon("../../tray.ico");
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            // Get local IP address
            IPAddress[] ipAs = Dns.GetHostAddresses(Dns.GetHostName());
            ipBox.Text = (ipAs[2]).ToString();
            serverAddress = ipAs[2];

            server = new Server();
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
            server.Start(serverAddress, (int)eventsPortUpDown.Value);
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

        private IPAddress serverAddress;
        private int eventsPort;
        private NotifyIcon notifyIcon;
        private Server server;

        private void ServerGUI_Load(object sender, EventArgs e)
        {

        }

        private void ipBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            server.Stop();
        }
    }

}
