using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_Project_Client
{
    class ServerPanel : System.Windows.Forms.Panel
    {

        private System.Windows.Forms.Button connectB;
        private System.Windows.Forms.Button disconnectB;

        private System.Windows.Forms.Label serverActive;
        private System.Windows.Forms.Label connectionStatus;

        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label serverIndex;

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
        
        /* variables */

        private Host _host;
        private Int16 _index;



        public ServerPanel(Int16 i)
        {
            _index = i;


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
            this.connectionStatus = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.serverActive = new System.Windows.Forms.Label();

            this.connectB = new System.Windows.Forms.Button();
            this.disconnectB = new System.Windows.Forms.Button();

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
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();


            // 
            // tb_IP
            // 
            this.tb_IP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_IP.Location = new System.Drawing.Point(103, 3);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(94, 20);
            this.tb_IP.TabIndex = 0;


            // 
            // tb_PSW
            // 
            this.tb_PSW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_PSW.Location = new System.Drawing.Point(103, 28);
            this.tb_PSW.Name = "tb_PSW";
            this.tb_PSW.Size = new System.Drawing.Size(94, 20);
            this.tb_PSW.TabIndex = 0;


            // 
            // tb_DP
            // 
            this.tb_DP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_DP.Location = new System.Drawing.Point(103, 53);
            this.tb_DP.Name = "tp_DP";
            this.tb_DP.Size = new System.Drawing.Size(94, 20);
            this.tb_DP.TabIndex = 0;


            // 
            // tb_EP
            // 
            this.tb_EP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_EP.Location = new System.Drawing.Point(103, 78);
            this.tb_EP.Name = "tb_EP";
            this.tb_EP.Size = new System.Drawing.Size(94, 20);
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
            this.serverIndex.Text = "Server " + _index.ToString() + " :";


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
            this.serverActive.Size = new System.Drawing.Size(94, 25);
            this.serverActive.TabIndex = 0;
            this.serverActive.Text = "Not Active";


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
            this.tlp.Name = "tableLayoutPanel1";
            this.tlp.RowCount = 10;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.Size = new System.Drawing.Size(200, 100);
            this.tlp.TabIndex = 0;


            //
            // Adding elements to Layout
            //

            this.tlp.Controls.Add(this.serverIndex, 0, 0);
            this.tlp.Controls.Add(this.connectionStatus, 1, 0);
            this.tlp.Controls.Add(this.status, 0, 1);
            this.tlp.Controls.Add(this.serverActive, 1, 1);


            this.tlp.Controls.Add(this.tb_IP, 1, 2);
            this.tlp.Controls.Add(this.tb_PSW, 1, 3);
            this.tlp.Controls.Add(this.tb_DP, 1, 4);
            this.tlp.Controls.Add(this.tb_EP, 1, 5);

            this.tlp.Controls.Add(this.ipLabel, 0, 2);
            this.tlp.Controls.Add(this.pswLabel, 0, 3);
            this.tlp.Controls.Add(this.eportLabel, 0, 4);
            this.tlp.Controls.Add(this.dportLabel, 0, 5);

            this.tlp.Controls.Add(this.connectB, 0, 6);
            this.tlp.Controls.Add(this.disconnectB, 1, 6);


        }


    }
}
