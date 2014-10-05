using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PDS_Project_Client
{
    partial class ClientGUI
    {



        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button exitB;
        private System.Windows.Forms.Button continueB;

        private System.Windows.Forms.TableLayoutPanel tlp_OUTER;
        private System.Windows.Forms.TableLayoutPanel tlp_INNER;
        private System.Windows.Forms.TableLayoutPanel tlp_PANEL;

        private System.Windows.Forms.Panel panel;


        private System.Windows.Forms.Button hotkeyBH;
        private System.Windows.Forms.Button hotkeyBS0;
        private System.Windows.Forms.Button hotkeyBS1;
        private System.Windows.Forms.Button hotkeyBS2;
        private System.Windows.Forms.Button hotkeyBS3;

        private System.Windows.Forms.Label hkLabel;
        private System.Windows.Forms.Label hotkey;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }





        private void InitializeComponent()
        {
            

            this.exitB = new System.Windows.Forms.Button();
            this.continueB = new System.Windows.Forms.Button();

            this.tlp_OUTER = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_INNER = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_PANEL = new System.Windows.Forms.TableLayoutPanel();
            this.panel = new System.Windows.Forms.Panel();



            this.hotkeyBH = new System.Windows.Forms.Button();

            this.hotkeyBS0 = new System.Windows.Forms.Button();
            this.hotkeyBS1 = new System.Windows.Forms.Button();
            this.hotkeyBS2 = new System.Windows.Forms.Button();
            this.hotkeyBS3 = new System.Windows.Forms.Button();

            this.hotkey = new System.Windows.Forms.Label();
            this.hkLabel = new System.Windows.Forms.Label();



            this.tlp_OUTER.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();


            // 
            // exitB
            // 
            this.exitB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(90, 30);
            this.exitB.TabIndex = 0;
            this.exitB.Text = "Exit";
            this.exitB.UseVisualStyleBackColor = true;

            this.exitB.Click += new EventHandler(this.exitB_click);


            // 
            // ContinueB
            // 
            this.continueB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continueB.Name = "continueB";
            this.continueB.Size = new System.Drawing.Size(90, 30);
            this.continueB.TabIndex = 0;
            this.continueB.Text = "Continue";
            this.continueB.UseVisualStyleBackColor = true;

            this.continueB.Click += new EventHandler(this.continueB_click);


            // 
            // hotkeyBH
            // 
            this.hotkeyBH.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyBH.Name = "Host HotKey";
            this.hotkeyBH.Size = new System.Drawing.Size(90, 30);
            this.hotkeyBH.TabIndex = 0;
            this.hotkeyBH.Text = "C.HK";
            this.hotkeyBH.UseVisualStyleBackColor = true;

            this.hotkeyBH.Click += new EventHandler(this.hotkeyB_click);

            // 
            // hotkeyBS0
            // 
            this.hotkeyBS0.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyBS0.Name = "Server1 HotKey";
            this.hotkeyBS0.Size = new System.Drawing.Size(90, 30);
            this.hotkeyBS0.TabIndex = 0;
            this.hotkeyBS0.Text = "S1.HK";
            this.hotkeyBS0.UseVisualStyleBackColor = true;

            this.hotkeyBS0.Click += new EventHandler(this.hotkeyB_click);

            // 
            // hotkeyBS1
            // 
            this.hotkeyBS1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyBS1.Name = "Server2 HotKey";
            this.hotkeyBS1.Size = new System.Drawing.Size(90, 30);
            this.hotkeyBS1.TabIndex = 0;
            this.hotkeyBS1.Text = "S2.HK";
            this.hotkeyBS1.UseVisualStyleBackColor = true;

            this.hotkeyBS1.Click += new EventHandler(this.hotkeyB_click);

            // 
            // hotkeyBS2
            // 
            this.hotkeyBS2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyBS2.Name = "Server3 HotKey";
            this.hotkeyBS2.Size = new System.Drawing.Size(90, 30);
            this.hotkeyBS2.TabIndex = 0;
            this.hotkeyBS2.Text = "S3.HK";
            this.hotkeyBS2.UseVisualStyleBackColor = true;

            this.hotkeyBS2.Click += new EventHandler(this.hotkeyB_click);

            // 
            // hotkeyBS3
            // 
            this.hotkeyBS3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyBS3.Name = "Server4 HotKey";
            this.hotkeyBS3.Size = new System.Drawing.Size(90, 30);
            this.hotkeyBS3.TabIndex = 0;
            this.hotkeyBS3.Text = "S4.HK";
            this.hotkeyBS3.UseVisualStyleBackColor = true;

            this.hotkeyBS3.Click += new EventHandler(this.hotkeyB_click);


            // 
            // hkLabel
            // 
            this.hkLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hkLabel.Name = "hkLabel";
            this.hkLabel.Size = new System.Drawing.Size(130, 30);
            this.hkLabel.TabIndex = 0;
            this.hkLabel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.hkLabel.Text = "Client Switch HK: ";

            // 
            // hotkey
            // 
            this.hotkey.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hotkey.Name = "hotkey";
            this.hotkey.Size = new System.Drawing.Size(50, 30);
            this.hotkey.TabIndex = 0;
            this.hotkey.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.hotkey.Text = "KEY_Q";
            this.hotkey.ForeColor = System.Drawing.Color.Blue;


            // 
            // tlpPANEL
            // 
            this.tlp_PANEL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.tlp_PANEL.AutoSize = true;

            this.tlp_PANEL.ColumnCount = 10;

            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));

            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));

            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));

            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));

            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));

            this.tlp_PANEL.Location = new System.Drawing.Point(0, 0); ;
            this.tlp_PANEL.Name = "tlp_PANEL";
            this.tlp_PANEL.RowCount = 1;
            this.tlp_PANEL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_PANEL.TabIndex = 0;

            this.tlp_PANEL.ResumeLayout(false);
            this.tlp_PANEL.PerformLayout();


            this.tlp_PANEL.Controls.Add(this.hotkeyBH, 0, 0);

            this.tlp_PANEL.Controls.Add(this.hkLabel, 1, 0);
            this.tlp_PANEL.Controls.Add(this.hotkey, 2, 0);

            this.tlp_PANEL.Controls.Add(this.hotkeyBS0, 3, 0);
            this.tlp_PANEL.Controls.Add(this.hotkeyBS1, 4, 0);
            this.tlp_PANEL.Controls.Add(this.hotkeyBS2, 5, 0);
            this.tlp_PANEL.Controls.Add(this.hotkeyBS3, 6, 0);

            this.tlp_PANEL.Controls.Add(this.continueB, 8, 0);
            this.tlp_PANEL.Controls.Add(this.exitB, 9, 0);


            // 
            // panel
            // 

            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "Panel";
            this.panel.TabIndex = 0;

            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.tlp_PANEL);
            this.panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            

            // 
            // tlpOUTER
            // 
            this.tlp_OUTER.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp_OUTER.ColumnCount = 1;
            this.tlp_OUTER.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_OUTER.Controls.Add(this.tlp_INNER, 0, 0);
            this.tlp_OUTER.Controls.Add(this.panel, 0, 1);
            this.tlp_OUTER.Location = new System.Drawing.Point(12, 12);
            this.tlp_OUTER.Name = "tlp_OUTER";
            this.tlp_OUTER.RowCount = 2;
            this.tlp_OUTER.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlp_OUTER.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_OUTER.Size = new System.Drawing.Size(786, 375);
            this.tlp_OUTER.TabIndex = 2;


            // 
            // tlpINNER
            // 
            this.tlp_INNER.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp_INNER.ColumnCount = 4;
            this.tlp_INNER.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_INNER.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_INNER.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_INNER.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_INNER.Location = new System.Drawing.Point(3, 3);
            this.tlp_INNER.Name = "tlp_INNER";
            this.tlp_INNER.RowCount = 1;
            this.tlp_INNER.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_INNER.Size = new System.Drawing.Size(780, 312);
            this.tlp_INNER.TabIndex = 3;


            /* adding ServerPanels */


            this.tlp_INNER.Controls.Add(sp[0], 0, 0);
            this.tlp_INNER.Controls.Add(sp[1], 1, 0);
            this.tlp_INNER.Controls.Add(sp[2], 2, 0);
            this.tlp_INNER.Controls.Add(sp[3], 3, 0);


            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.tlp_OUTER);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ClientGUI";
            this.Text = "Remote Control";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tlp_OUTER.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);


        }


    }
}

