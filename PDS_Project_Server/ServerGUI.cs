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
        private IPAddress _serverAddress;
        private int _eventsPort;
        private NotifyIcon _notifyIcon;
        private Server _server;
        private Icon _inactiveIcon;
        private Icon _activeIcon;

        public ServerGUI()
        {
            InitializeComponent();
            Resize += ServerGUI_Resize;

            // Notify icon initialization
            _notifyIcon = new NotifyIcon();
            _notifyIcon.BalloonTipText = "PDS_Project_Server";
            _inactiveIcon = new Icon("../../tray.ico");
            _activeIcon = new Icon("../../active.ico");
            _notifyIcon.Icon = _inactiveIcon;
            _notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            // Get local IP address
            IPAddress[] ipAs = Dns.GetHostAddresses(Dns.GetHostName());
            ipBox.Text = (ipAs[2]).ToString();
            _serverAddress = ipAs[2];

            _server = new Server(this.server_StateChange);
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
            _server.Start(_serverAddress, (int)eventsPortUpDown.Value, psswBox.Text);
        }

        private void ServerGUI_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                _notifyIcon.Visible = true;
                _notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


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
            _server.Stop();
        }

        delegate void stateChangedCallback(Server.State newState);
        private void server_StateChange(Server.State newState)
        {
            if (this.InvokeRequired)
            {
                stateChangedCallback cb = new stateChangedCallback(server_StateChange);
                this.Invoke(cb, new object[] {newState});
            }
            else
            {
                switch (newState)
                {
                    case Server.State.Waiting:
                    case Server.State.Connected:
                        if (newState == Server.State.Waiting)
                            statusLabel.ForeColor = Color.Orange;
                        else
                            statusLabel.ForeColor = Color.Green;
                        ipBox.Enabled = false;
                        eventsPortUpDown.Enabled = false;
                        clipboardUpDown.Enabled = false;
                        psswBox.Enabled = false;
                        startButton.Enabled = false;
                        stopButton.Enabled = true;
                        break;

                    case Server.State.Disconnected:
                        statusLabel.ForeColor = Color.Red;
                        ipBox.Enabled = true;
                        eventsPortUpDown.Enabled = true;
                        clipboardUpDown.Enabled = true;
                        psswBox.Enabled = true;
                        startButton.Enabled = true;
                        stopButton.Enabled = false;
                        break;

                    case Server.State.Active:
                        statusLabel.ForeColor = Color.Blue;
                        _notifyIcon.Icon = _activeIcon;
                        break;
                }

                statusLabel.Text = newState.ToString();
            }
        }
    }

}
