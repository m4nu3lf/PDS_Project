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


        private System.Windows.Forms.Button hotkeyB;
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



            this.hotkeyB = new System.Windows.Forms.Button();
            this.hotkey = new System.Windows.Forms.Label();



            this.tlp_OUTER.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();


            // 
            // exitB
            // 
            this.exitB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(100, 30);
            this.exitB.TabIndex = 0;
            this.exitB.Text = "Exit";
            this.exitB.UseVisualStyleBackColor = true;


            // 
            // ContinueB
            // 
            this.continueB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continueB.Name = "continueB";
            this.continueB.Size = new System.Drawing.Size(100, 30);
            this.continueB.TabIndex = 0;
            this.continueB.Text = "Continue";
            this.continueB.UseVisualStyleBackColor = true;

            this.continueB.Click += new EventHandler(this.continueB_click);

            // 
            // hotkeyB
            // 
            this.hotkeyB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkeyB.Name = "hotkeyB";
            this.hotkeyB.Size = new System.Drawing.Size(150, 30);
            this.hotkeyB.TabIndex = 0;
            this.hotkeyB.Text = "Change Client HK";
            this.hotkeyB.UseVisualStyleBackColor = true;

            this.hotkeyB.Click += new EventHandler(this.hotkeyB_click);



            // 
            // hotkey
            // 
            this.hotkey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hotkey.Name = "hotkey";
            this.hotkey.Size = new System.Drawing.Size(300, 30);
            this.hotkey.TabIndex = 0;
            this.hotkey.Text = "Actual Client Switch Hotkey: ctrl + alt + 0";


            // 
            // tlpPANEL
            // 
            this.tlp_PANEL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.tlp_PANEL.AutoSize = true;

            this.tlp_PANEL.ColumnCount = 5;
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_PANEL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));

            this.tlp_PANEL.Controls.Add(this.hotkeyB, 0, 0);
            this.tlp_PANEL.Controls.Add(this.hotkey, 1, 0);
            this.tlp_PANEL.Controls.Add(this.continueB, 3, 0);
            this.tlp_PANEL.Controls.Add(this.exitB, 4, 0);

            this.tlp_PANEL.Location = new System.Drawing.Point(0, 0); ;
            this.tlp_PANEL.Name = "tlp_PANEL";
            this.tlp_PANEL.RowCount = 1;
            this.tlp_PANEL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_PANEL.TabIndex = 0;

            this.tlp_PANEL.ResumeLayout(false);
            this.tlp_PANEL.PerformLayout();


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


            this.tlp_INNER.Controls.Add(this.sp1, 0, 0);
            this.tlp_INNER.Controls.Add(this.sp2, 1, 0);
            this.tlp_INNER.Controls.Add(this.sp3, 2, 0);
            this.tlp_INNER.Controls.Add(this.sp4, 3, 0);


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

