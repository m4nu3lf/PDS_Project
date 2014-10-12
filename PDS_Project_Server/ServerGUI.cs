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
using System.Threading;
using System.IO;

namespace PDS_Project_Server
{
    public partial class ServerGUI : Form
    {
        private NotifyIcon _notifyIcon;
        private EventServer _evtServer;
        private ClipboardServer _clpbServer;
        private Icon _inactiveIcon;
        private Icon _activeIcon;
        private Blinking _blinking;
        private String _addressHint;

        public ServerGUI()
        {
            InitializeComponent();
            Resize += ServerGUI_Resize;

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // Notify icon initialization
            _notifyIcon = new NotifyIcon();
            _notifyIcon.BalloonTipText = "PDS_Project_Server";
            _inactiveIcon = new Icon("tray.ico");
            _activeIcon = new Icon("active.ico");
            _notifyIcon.Icon = _inactiveIcon;
            _notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            // Read settings
            _addressHint = (string)Properties.Settings.Default["IpAddress"];
            eventsPortUpDown.Value = (ushort)Properties.Settings.Default["EventsPort"];
            clipboardUpDown.Value = (ushort)Properties.Settings.Default["ClipboardPort"];
            psswBox.Text = (string)Properties.Settings.Default["Password"];
            autorunCheckBox.Checked = (bool)Properties.Settings.Default["Autorun"];

            _evtServer = new EventServer(this.server_StateChange);
            _clpbServer = new ClipboardServer(this.server_StateChange);

            _blinking = new Blinking();

            updateIpAddressListBox();

            if (autorunCheckBox.Checked)
            {
                WindowState = FormWindowState.Minimized;
                startServers();
            }
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

        private void startServers()
        {
            IPAddress address = (IPAddress)ipListBox.SelectedItem;
            if (ipListBox.SelectedItem != null)
            {
                _evtServer.Start(address, (int)eventsPortUpDown.Value, psswBox.Text);
                _clpbServer.Start(address, (int)clipboardUpDown.Value, psswBox.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startServers();
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
            _evtServer.Stop();
            _clpbServer.Stop();
        }

        delegate void stateChangedCallback(EventServer.StateBase newState);
        private void server_StateChange(EventServer.StateBase newState)
        {
            if (this.InvokeRequired)
            {
                stateChangedCallback cb = new stateChangedCallback(server_StateChange);
                this.Invoke(cb, new object[] {newState});
            }
            else if (_evtServer != null && _clpbServer != null)
            {
                if ((_evtServer.State is Server.WaitingState
                    && _clpbServer.State is Server.WaitingState) ||
                    (_evtServer.State is Server.ConnectedState
                    && _clpbServer.State is Server.ConnectedState) ||
                    (_evtServer.State is EventServer.AuthenticatedState
                    && _clpbServer.State is ClipboardServer.AuthenticatedState))
                {
                    ipListBox.Enabled = false;
                    updateButton.Enabled = false;
                    eventsPortUpDown.Enabled = false;
                    clipboardUpDown.Enabled = false;
                    psswBox.Enabled = false;
                    startButton.Enabled = false;
                    stopButton.Enabled = true;
                    if (_evtServer.State is Server.WaitingState)
                        statusLabel.ForeColor = Color.Orange;
                    else
                        statusLabel.ForeColor = Color.Green;
                    statusLabel.Text = _evtServer.State.GetMsg();
                }
                else if (_evtServer.State is Server.DisconnectedState
                        && _clpbServer.State is Server.DisconnectedState)
                {
                    statusLabel.ForeColor = Color.Red;
                    statusLabel.Text = _evtServer.State.GetMsg();
                    ipListBox.Enabled = true;
                    updateButton.Enabled = true;
                    eventsPortUpDown.Enabled = true;
                    clipboardUpDown.Enabled = true;
                    psswBox.Enabled = true;
                    startButton.Enabled = true;
                    stopButton.Enabled = false;
                }
                else if (_evtServer.State is EventServer.ActiveState)
                {
                    _blinking.Blink();
                    _notifyIcon.Icon = _activeIcon;
                    statusLabel.ForeColor = Color.Blue;
                    statusLabel.Text = _evtServer.State.GetMsg();
                }
            }
        }

        private void psswBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void eventsPortUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void updateIpAddressListBox()
        {
            ipListBox.SelectedItem = null;
            ipListBox.Items.Clear();

            // Look for an IPv4 address
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == AddressFamily.InterNetwork && address.ToString() != "127.0.0.1" ||
                    address.AddressFamily == AddressFamily.InterNetworkV6 && address.ToString() != "::1"
                    && address.IsIPv6LinkLocal)
                    ipListBox.Items.Add(address);
            }
            ipListBox.Items.Add(IPAddress.Any);
            ipListBox.Items.Add(IPAddress.IPv6Any);

            List<IPAddress> addresses = ipListBox.Items.OfType<IPAddress>().ToList();
            foreach (object address in addresses)
            {
                if (address.ToString() == _addressHint)
                    ipListBox.SelectedItem = address;
            }
            if (ipListBox.SelectedItem == null)
            {
                ipListBox.SelectedItem = ipListBox.Items[0];
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            updateIpAddressListBox();
        }

        private void ipListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ipListBox.SelectedItem != null)
                _addressHint = ipListBox.SelectedItem.ToString();
        }

    }

}
