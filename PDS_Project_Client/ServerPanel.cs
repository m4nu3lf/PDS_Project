using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using PDS_Project_Common;
using System.IO;

namespace PDS_Project_Client
{
    public class ServerPanel : System.Windows.Forms.Panel
    {

        /* GRAPHICS VARIABLES */

        private System.Windows.Forms.Button connectB;
        private System.Windows.Forms.Button disconnectB;
        private System.Windows.Forms.Button sendCBB;
        private System.Windows.Forms.Button getCBB;

        private System.Windows.Forms.Label serverActive;
        private System.Windows.Forms.Label connectionStatus;

        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label serverIndex;


        private System.Windows.Forms.Label hotkey;
        private System.Windows.Forms.Label hkLabel;

        /* datas */

        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label pswLabel;
        private System.Windows.Forms.Label eportLabel;
        private System.Windows.Forms.Label dportLabel;

        /* textbox */

        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.TextBox tb_PSW;
        private System.Windows.Forms.TextBox tb_DP;
        private System.Windows.Forms.TextBox tb_EP;

        /* layout */

        private System.Windows.Forms.TableLayoutPanel tlp;
        



        /* CONTROL VARIABLES */

        private Host _host;

        private int i;

        public VirtualKeyShort hk { get; set; }

        private Thread cDeamon;     //Deamon used to connect
        private Thread dDeamon;     //Deamon used to disconnect
        private Thread aDeamon;     //Deamon used to manage the Active Label

        private Thread CBDeamon;    //Deamon used to send Clipboard Datas

        private bool c_flag;        //Connection flag


        private String IP;
        private String PSW;
        private UInt16 EP;
        private UInt16 DP;


        delegate void UsefulDelegate(); //used to change labels


        public ServerPanel(int index, Host h)
        {
            i = index;      // index of the server
            _host = h;      // host global object
            c_flag = false;

            switch (i)
            {
                case 0:
                    hk = VirtualKeyShort.KEY_1;
                    break;

                case 1:
                    hk = VirtualKeyShort.KEY_2;
                    break;

                case 2:
                    hk = VirtualKeyShort.KEY_3;
                    break;

                case 3:
                    hk = VirtualKeyShort.KEY_4;
                    break;
            }

            this.InitializeComponent();

        }



        private void InitializeComponent()
        {
            this.tlp = new System.Windows.Forms.TableLayoutPanel();

            this.tb_IP = new System.Windows.Forms.TextBox();
            this.tb_PSW = new System.Windows.Forms.TextBox();
            this.tb_DP = new System.Windows.Forms.TextBox();
            this.tb_EP = new System.Windows.Forms.TextBox();

            this.pswLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.eportLabel = new System.Windows.Forms.Label();
            this.dportLabel = new System.Windows.Forms.Label();
            this.serverIndex = new System.Windows.Forms.Label();
            this.hotkey = new System.Windows.Forms.Label();
            this.hkLabel = new System.Windows.Forms.Label();
            this.connectionStatus = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.serverActive = new System.Windows.Forms.Label();

            this.connectB = new System.Windows.Forms.Button();
            this.disconnectB = new System.Windows.Forms.Button();

            this.sendCBB = new System.Windows.Forms.Button();
            this.getCBB = new System.Windows.Forms.Button();

            this.tlp.SuspendLayout();
            this.SuspendLayout();



            // 
            // ServerPanel
            // 
            this.Location = new System.Drawing.Point(12, 12);
            this.Name = "ServerPanel";
            this.Size = new System.Drawing.Size(200, 421);
            this.TabIndex = 0;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlp);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);
            this.PerformLayout();


            // 
            // tb_IP
            // 
            this.tb_IP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_IP.Location = new System.Drawing.Point(103, 3);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(94, 25);
            this.tb_IP.TabIndex = 0;


            // 
            // tb_PSW
            // 
            this.tb_PSW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_PSW.Location = new System.Drawing.Point(103, 28);
            this.tb_PSW.Name = "tb_PSW";
            this.tb_PSW.Size = new System.Drawing.Size(94, 25);
            this.tb_PSW.TabIndex = 0;


            // 
            // tb_DP
            // 
            this.tb_DP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_DP.Location = new System.Drawing.Point(103, 53);
            this.tb_DP.Name = "tp_DP";
            this.tb_DP.Size = new System.Drawing.Size(94, 25);
            this.tb_DP.TabIndex = 0;


            // 
            // tb_EP
            // 
            this.tb_EP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_EP.Location = new System.Drawing.Point(103, 78);
            this.tb_EP.Name = "tb_EP";
            this.tb_EP.Size = new System.Drawing.Size(94, 25);
            this.tb_EP.TabIndex = 0;



            // 
            // IP
            // 
            this.ipLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ipLabel.Location = new System.Drawing.Point(3, 25);
            this.ipLabel.Name = "IP";
            this.ipLabel.Size = new System.Drawing.Size(94, 25);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP";


            // 
            // Password
            // 
            this.pswLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pswLabel.Location = new System.Drawing.Point(3, 0);
            this.pswLabel.Name = "Password";
            this.pswLabel.Size = new System.Drawing.Size(94, 25);
            this.pswLabel.TabIndex = 0;
            this.pswLabel.Text = "Password";

            
            // 
            // Dport
            // 
            this.dportLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dportLabel.Location = new System.Drawing.Point(3, 75);
            this.dportLabel.Name = "Dport";
            this.dportLabel.Size = new System.Drawing.Size(94, 25);
            this.dportLabel.TabIndex = 0;
            this.dportLabel.Text = "Port:Data";


            // 
            // Eport
            // 
            this.eportLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.eportLabel.Location = new System.Drawing.Point(3, 50);
            this.eportLabel.Name = "Eport";
            this.eportLabel.Size = new System.Drawing.Size(94, 25);
            this.eportLabel.TabIndex = 0;
            this.eportLabel.Text = "Port:Event";


            // 
            // serverIndex
            // 
            this.serverIndex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverIndex.Location = new System.Drawing.Point(3, 50);
            this.serverIndex.Name = "serverIndex";
            this.serverIndex.Size = new System.Drawing.Size(94, 25);
            this.serverIndex.TabIndex = 0;
            this.serverIndex.Text = "Server " + (i+1).ToString() + " :";


            // 
            // connectionStatus
            // 
            this.connectionStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectionStatus.Location = new System.Drawing.Point(3, 50);
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.ForeColor = System.Drawing.Color.Red;
            this.connectionStatus.Size = new System.Drawing.Size(94, 25);
            this.connectionStatus.TabIndex = 0;
            this.connectionStatus.Text = "Disconnected";


            // 
            // statusLabel
            // 
            this.status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.status.Location = new System.Drawing.Point(3, 50);
            this.status.Name = "statusLabel";
            this.status.Size = new System.Drawing.Size(94, 25);
            this.status.TabIndex = 0;
            this.status.Text = "Status: ";


            // 
            // serverActive
            // 
            this.serverActive.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverActive.Location = new System.Drawing.Point(3, 50);
            this.serverActive.Name = "serverActive";
            this.serverActive.ForeColor = System.Drawing.Color.Red;
            this.serverActive.Size = new System.Drawing.Size(94, 25);
            this.serverActive.TabIndex = 0;
            this.serverActive.Text = "Not Active";


            // 
            // hkLabel
            // 
            this.hkLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hkLabel.Location = new System.Drawing.Point(3, 50);
            this.hkLabel.Name = "hkLabel";
            this.hkLabel.Size = new System.Drawing.Size(94, 25);
            this.hkLabel.TabIndex = 0;
            this.hkLabel.Text = "Switch HK: ";



            // 
            // hotkey
            // 
            this.hotkey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkey.Location = new System.Drawing.Point(3, 50);
            this.hotkey.Name = "hotkey";
            this.hotkey.Size = new System.Drawing.Size(100, 25);
            this.hotkey.TabIndex = 0;
            this.hotkey.Text =  hk.ToString();
            this.hotkey.ForeColor = System.Drawing.Color.Blue;



            // 
            // connectB
            // 
            this.connectB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectB.Location = new System.Drawing.Point(677, 9);
            this.connectB.Name = "connectB";
            this.connectB.Size = new System.Drawing.Size(94, 25);
            this.connectB.TabIndex = 0;
            this.connectB.Text = "Connect";
            this.connectB.UseVisualStyleBackColor = true;

            // 
            // bdisconnectB
            // 
            this.disconnectB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.disconnectB.Location = new System.Drawing.Point(571, 9);
            this.disconnectB.Name = "disconnectB";
            this.disconnectB.Size = new System.Drawing.Size(94, 25);
            this.disconnectB.TabIndex = 0;
            this.disconnectB.Text = "Disconnect";
            this.disconnectB.Enabled = false;
            this.disconnectB.UseVisualStyleBackColor = true;

            // 
            // sendCB
            // 
            this.sendCBB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendCBB.Location = new System.Drawing.Point(677, 9);
            this.sendCBB.Name = "sendCBB";
            this.sendCBB.Size = new System.Drawing.Size(94, 25);
            this.sendCBB.TabIndex = 0;
            this.sendCBB.Text = "Send CB";
            this.sendCBB.Enabled = false;
            this.sendCBB.UseVisualStyleBackColor = true;


            // 
            // getB
            // 
            this.getCBB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.getCBB.Location = new System.Drawing.Point(677, 9);
            this.getCBB.Name = "getCBB";
            this.getCBB.Size = new System.Drawing.Size(94, 25);
            this.getCBB.TabIndex = 0;
            this.getCBB.Text = "Get CB";
            this.getCBB.Enabled = false;
            this.getCBB.UseVisualStyleBackColor = true;



            // 
            // tlp init
            // 

            this.tlp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.AutoSize = true;
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));


            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 9;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlp.Size = new System.Drawing.Size(200, 100);
            this.tlp.TabIndex = 0;
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();

            //
            // Adding elements to Layout
            //

            this.tlp.Controls.Add(this.serverIndex, 0, 0);
            this.tlp.Controls.Add(this.connectionStatus, 1, 0);
            this.tlp.Controls.Add(this.status, 0, 1);
            this.tlp.Controls.Add(this.serverActive, 1, 1);


            this.tlp.Controls.Add(this.tb_IP, 1, 2);
            this.tlp.Controls.Add(this.tb_PSW, 1, 3);
            this.tlp.Controls.Add(this.tb_EP, 1, 4);
            this.tlp.Controls.Add(this.tb_DP, 1, 5);

            this.tlp.Controls.Add(this.ipLabel, 0, 2);
            this.tlp.Controls.Add(this.pswLabel, 0, 3);
            this.tlp.Controls.Add(this.eportLabel, 0, 4);
            this.tlp.Controls.Add(this.dportLabel, 0, 5);

            this.tlp.Controls.Add(this.sendCBB, 0, 7);
            this.tlp.Controls.Add(this.getCBB, 1, 7);

            this.tlp.Controls.Add(this.hkLabel, 0, 6);
            this.tlp.Controls.Add(this.hotkey, 1, 6);

            this.tlp.Controls.Add(this.connectB, 0, 8);
            this.tlp.Controls.Add(this.disconnectB, 1, 8);

            /* Events */

            connectB.Click += new EventHandler(this.connectB_click);
            disconnectB.Click += new EventHandler(this.disconnectB_click);

            sendCBB.Click += new EventHandler(this.sendCB_click);
            getCBB.Click += new EventHandler(this.getCB_click);


            //DEFAULT CONFIG

            this.tb_IP.Text = "169.254.162.184";
            //this.tb_IP.Text = "172.20.90.244";
            this.tb_EP.Text = "200" + i.ToString();
            this.tb_DP.Text = "300" + i.ToString();
            this.tb_PSW.Text = "12345";

        }


        /* Updating the hotkey */

        public void ChangeHK(VirtualKeyShort nVKS)
        {
            hk = nVKS;
            this.hotkey.Text = nVKS.ToString();
        }




        /* GESTIONE CONNESSIONE */


        private void connectB_click(Object sender, EventArgs e)
        {
            IP = tb_IP.Text;
            PSW = tb_PSW.Text;

            bool err_flag = false;

            try
            {
                EP = Convert.ToUInt16(tb_EP.Text);
            }
            catch (FormatException)
            {
                tb_EP.Text = "Invalid Port";
                err_flag = true;
            }

            try
            {
                DP = Convert.ToUInt16(tb_DP.Text);
            }
            catch (FormatException)
            {
                tb_DP.Text = "Invalid Port";
                err_flag = true;
            }


            if (err_flag) return;


            this.connectionStatus.ForeColor = System.Drawing.Color.Orange;
            this.connectionStatus.Text = "Connecting...";
            this.connectB.Enabled = false;
            tb_IP.Enabled = false;
            tb_PSW.Enabled = false;
            tb_EP.Enabled = false;
            tb_DP.Enabled = false;

            this.cDeamon = new Thread(new ThreadStart(this.Connect));
            this.cDeamon.Start();

            //Console.WriteLine("Connection Deamon Created");

        }

        private void Connect() {
            Socket tmpE = new Socket(SocketType.Stream, ProtocolType.Tcp); //event socket
            Socket tmpD = new Socket(SocketType.Stream, ProtocolType.Tcp); //clipboard socket
            object o = null;

            try
            {

                Console.WriteLine("Connecting EventSocket To -> " + IP + ":" + EP.ToString());
                tmpE.Connect(IP, EP);

                /* Authentication on EventSocket*/

                MsgStream.Send(new AuthMsg(PSW), tmpE);
                o = MsgStream.Receive(tmpE);

                if ((o is AckMsg) && (((AckMsg)o).ack)) Console.WriteLine("Connection completed successful on EventSocket.");
                else throw new Exception();


                Console.WriteLine("Connecting DataSocket To -> " + IP + ":" + DP.ToString());
                tmpD.Connect(IP, DP);

                /* Authentication on DataSocket */
                Console.WriteLine("Starting authentication protocol.");
                MsgStream.Send(new AuthMsg(PSW), tmpD);
                o = MsgStream.Receive(tmpD);

                if ((o is AckMsg) && (((AckMsg)o).ack)) Console.WriteLine("Connection completed successful on DataSocket.");
                else throw new Exception();

                _host.es(tmpE, i);
                _host.ds(tmpD, i);
                this.Connected();

            }
            catch(Exception)
            {
                tmpE.Close();
                tmpD.Close(); 
                this.Disconnected();
            }
            
        }


        private void Connected()
        {

            if (this.disconnectB.InvokeRequired)
            {
                UsefulDelegate ud = new UsefulDelegate(Connected);
                this.Invoke(ud);
            }
            else
            {
                this.connectionStatus.ForeColor = System.Drawing.Color.Green;
                this.connectionStatus.Text = "Connected";
                c_flag = true;
                

                tb_IP.Enabled = false;
                tb_PSW.Enabled = false;
                tb_EP.Enabled = false;
                tb_DP.Enabled = false;
                this.disconnectB.Enabled = true;
                this.getCBB.Enabled = true;
                this.sendCBB.Enabled = true;
                this.connectB.Enabled = false;
            }

        }



        /* GESTIONE DISCONNESSIONE */


        private void disconnectB_click(Object sender, EventArgs e)
        {
            if (c_flag)
            {
                this.dDeamon = new Thread(new ThreadStart(this.Disconnect));
                this.dDeamon.Start();
            }

        }

        public void DisconnectionReq()
        {
            if (c_flag)
            {
                this.dDeamon = new Thread(new ThreadStart(this.Disconnect));
                this.dDeamon.Start();
            }
        }


        private void Disconnect()
        {
            Socket tmpE = _host.es(i);
            Socket tmpD = _host.ds(i);
            _host.es(null, i);
            _host.ds(null, i);

            try
            {
                Console.WriteLine("Disconnecting EventSocket -> " + IP + ":" + EP.ToString());
                tmpE.Shutdown(SocketShutdown.Both);
                tmpE.Close();

                Console.WriteLine("Disconnecting DataSocket-> " + IP + ":" + DP.ToString());
                tmpD.Shutdown(SocketShutdown.Both);
                tmpD.Close();
            }
            catch (Exception)
            {
                if ((tmpE != null) && (tmpE.Connected) && (tmpD != null) && (tmpD.Connected))
                {
                    _host.es(tmpE, i);
                    _host.ds(tmpD, i);
                    return;
                } 
            }

            this.Disconnected();

        }


        private void Disconnected()
        {

            if (this.disconnectB.InvokeRequired)
            {
                try
                {
                    UsefulDelegate ud = new UsefulDelegate(Disconnected);
                    this.Invoke(ud);
                }
                catch (System.ObjectDisposedException)
                {
                    Console.WriteLine("Error while disconnecting.");
                }
            }
            else
            {
                this.connectionStatus.ForeColor = System.Drawing.Color.Red;
                this.connectionStatus.Text = "Disconnected";
                this.serverActive.ForeColor = System.Drawing.Color.Red;
                serverActive.Text = "Not Active";
                c_flag = false;

                tb_IP.Enabled = true;
                tb_PSW.Enabled = true;
                tb_EP.Enabled = true;
                tb_DP.Enabled = true;
                this.disconnectB.Enabled = false;
                this.getCBB.Enabled = false;
                this.sendCBB.Enabled = false;
                this.connectB.Enabled = true;
            }

        }



        /* GESTIONE ATTIVAZIONE */


        public void Deactivate() 
        {
            if (c_flag)
            {
                this.aDeamon = new Thread(new ThreadStart(this.Deactivation));
                this.aDeamon.Start();
            }
        }


        public void Activate()
        {
            if (c_flag)
            {
                this.aDeamon = new Thread(new ThreadStart(this.Activation));
                this.aDeamon.Start();
            }
        }


        public void Deactivation()
        {
            if (this.serverActive.InvokeRequired)
            {
                UsefulDelegate ud = new UsefulDelegate(Deactivation);
                this.Invoke(ud);
            }
            else
            {
                //Console.WriteLine("Deactivation...");
                serverActive.Text = "Not Active";
                this.serverActive.ForeColor = System.Drawing.Color.Red;
            }
        }


        public void Activation()
        {

            if (this.serverActive.InvokeRequired)
            {
                UsefulDelegate ud = new UsefulDelegate(Activation);
                this.Invoke(ud);
            }
            else
            {
                //Console.WriteLine("Activation...");
                serverActive.Text = "Active";
                this.serverActive.ForeColor = System.Drawing.Color.Green;
            }

        }





        /* CLIPBOARD: Buttons' Events and Managing */


        private void getCB_click(Object o, EventArgs e)
        {
            _host.EnqueueCBMsg(new GetMsgCBP(i));
        }

        private void sendCB_click(Object o, EventArgs e)
        {
            sendCBB.Enabled = false;

            StartSendingCB();
        }

        public void StartSendingCB()
        {
            CBDeamon = new Thread(sendClipboardDatas);
            CBDeamon.SetApartmentState(ApartmentState.STA);
            CBDeamon.Start();
        }

        
        private void sendClipboardDatas()
        {
            //MessageBox.Show("Sending clipboard to server:" + i.ToString() + " .\nYou will be advised when the transfer is completed.", "Starting Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            Socket s = _host.ds(i);
            if( s == null ) return;

            IDataObject clipboardData = Clipboard.GetDataObject();
            string[] formats = clipboardData.GetFormats();
            foreach (string format in formats)
            {
                if (format == DataFormats.FileDrop)
                    continue;
                MsgStream.Send(new DataMsgCBP(format, clipboardData.GetData(format)),
                    _host.ds(i));
            }

            /*if (Clipboard.ContainsData(DataFormats.Text))
            {
                string txt = Clipboard.GetText();
                MsgStream.Send(new TextMsgCBP(txt), );
                //MessageBox.Show("Clipboard sent to server:" + i.ToString() + " .\nCheck on it.", "Transfer Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
            }*/



                /* SENDING FILE TYPE */

            if (Clipboard.ContainsFileDropList()) 
            {
                long size = ClipboardFiles.GetCBFilesSize();
                if (size > ClipboardFiles.MaxSize)
                {
                    if (MessageBox.Show("The size of clipboard's content is greater than MaxSize: " + ClipboardFiles.MaxSize  
                        + " \nConfirm the transfer?", "Closing Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) 
                    {
                        return;
                    }
                }

                MsgStream.Send(new InitFileCBP(), s);
                ClipboardFiles.SendClipboardFiles(s);
                    //Console.WriteLine("CBP : Sent Files.");
                MsgStream.Send(new StopFileCBP(), s);
            }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Ops...\nSomething goes wrong during Clipboard Transfer[on Sending].\nThe connection will be closed.\nTry again later.",
                    "Clipboard Transfer Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                this.DisconnectionReq();
                Console.WriteLine("Clipboard file transfer error: " + e.Message);
            }


            EnableCBTransfers();
            return;

        }


        private void EnableCBTransfers()
        {

            if (this.serverActive.InvokeRequired)
            {
                UsefulDelegate ud = new UsefulDelegate(EnableCBTransfers);
                this.Invoke(ud);
            }
            else
            {
                sendCBB.Enabled = true;
            }

        }



    }//end ServerPanel


}//end namespace
