using System;
using System.IO;
using Microsoft.Win32;
using PDS_Project_Common;

namespace PDS_Project_Server
{
    partial class ServerGUI
    {
        private const string ProgramKeyName = "PDS_Project_Server";

        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            // Save settings
            if (ipComboBox.SelectedItem != null)
                Properties.Settings.Default["IpAddress"] = ipComboBox.SelectedItem.ToString();
            else
                Properties.Settings.Default["IpAddress"] = "";
            Properties.Settings.Default["EventsPort"] = (ushort)eventsPortUpDown.Value;
            Properties.Settings.Default["ClipboardPort"] = (ushort)clipboardUpDown.Value;
            Properties.Settings.Default["Password"] = psswBox.Text;
            Properties.Settings.Default["Autorun"] = autorunCheckBox.Checked;
            Properties.Settings.Default.Save();
            RegistryKey key;
            key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
            if (autorunCheckBox.Checked)
            {
                key.SetValue(ProgramKeyName, AppDomain.CurrentDomain.BaseDirectory +
                    AppDomain.CurrentDomain.FriendlyName);
            }
            else
            {
                if (key.GetValue(ProgramKeyName, null) != null)
                    key.DeleteValue("PDS_Project_Server");
            }
            key.Close();


            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (_evtServer != null)
                _evtServer.Terminate();
            if (_clpbServer != null)
                _clpbServer.Terminate();
            _blinking.Terminate();
            ClipboardFiles.FreeTmpResources();
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.psswBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.Label();
            this.autorunCheckBox = new System.Windows.Forms.CheckBox();
            this.eventsPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.clipboardUpDown = new System.Windows.Forms.NumericUpDown();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.ipComboBox = new System.Windows.Forms.ComboBox();
            this.updateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventsPortUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // psswBox
            // 
            this.psswBox.Location = new System.Drawing.Point(132, 116);
            this.psswBox.Name = "psswBox";
            this.psswBox.Size = new System.Drawing.Size(162, 20);
            this.psswBox.TabIndex = 0;
            this.psswBox.Text = "12345";
            this.psswBox.TextChanged += new System.EventHandler(this.psswBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Local IP:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Events Port Number:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Clipboard Port Number:";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(132, 141);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(213, 142);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 12;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Status:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 190);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(297, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "Status:";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(123, 193);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(73, 13);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "Disconnected";
            // 
            // autorunCheckBox
            // 
            this.autorunCheckBox.AutoSize = true;
            this.autorunCheckBox.Location = new System.Drawing.Point(132, 170);
            this.autorunCheckBox.Name = "autorunCheckBox";
            this.autorunCheckBox.Size = new System.Drawing.Size(134, 17);
            this.autorunCheckBox.TabIndex = 15;
            this.autorunCheckBox.Text = "Autorun at system boot";
            this.autorunCheckBox.UseVisualStyleBackColor = true;
            this.autorunCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // eventsPortUpDown
            // 
            this.eventsPortUpDown.Location = new System.Drawing.Point(132, 64);
            this.eventsPortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.eventsPortUpDown.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.eventsPortUpDown.Name = "eventsPortUpDown";
            this.eventsPortUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.eventsPortUpDown.Size = new System.Drawing.Size(75, 20);
            this.eventsPortUpDown.TabIndex = 8;
            this.eventsPortUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eventsPortUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.eventsPortUpDown.ValueChanged += new System.EventHandler(this.eventsPortUpDown_ValueChanged);
            // 
            // clipboardUpDown
            // 
            this.clipboardUpDown.Location = new System.Drawing.Point(132, 90);
            this.clipboardUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.clipboardUpDown.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.clipboardUpDown.Name = "clipboardUpDown";
            this.clipboardUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.clipboardUpDown.Size = new System.Drawing.Size(75, 20);
            this.clipboardUpDown.TabIndex = 9;
            this.clipboardUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.clipboardUpDown.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 125);
            // 
            // ipComboBox
            // 
            this.ipComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ipComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ipComboBox.FormattingEnabled = true;
            this.ipComboBox.Location = new System.Drawing.Point(132, 7);
            this.ipComboBox.Name = "ipComboBox";
            this.ipComboBox.Size = new System.Drawing.Size(153, 21);
            this.ipComboBox.TabIndex = 16;
            this.ipComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(132, 34);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 17;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 212);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.ipComboBox);
            this.Controls.Add(this.autorunCheckBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.clipboardUpDown);
            this.Controls.Add(this.eventsPortUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.psswBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServerGUI";
            this.Text = "PDS Project Server";
            this.Load += new System.EventHandler(this.ServerGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventsPortUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox psswBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox autorunCheckBox;
        private System.Windows.Forms.NumericUpDown eventsPortUpDown;
        private System.Windows.Forms.NumericUpDown clipboardUpDown;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ComboBox ipComboBox;
        private System.Windows.Forms.Button updateButton;
    }
}

