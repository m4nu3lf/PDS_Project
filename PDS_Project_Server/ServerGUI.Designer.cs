namespace PDS_Project_Server
{
    partial class ServerGUI
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            if (_server != null)
                _server.Terminate();
            _blinking.Terminate();
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
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.eventsPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.clipboardUpDown = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventsPortUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // psswBox
            // 
            this.psswBox.Location = new System.Drawing.Point(126, 90);
            this.psswBox.Name = "psswBox";
            this.psswBox.Size = new System.Drawing.Size(162, 20);
            this.psswBox.TabIndex = 0;
            this.psswBox.Text = "12345";
            this.psswBox.TextChanged += new System.EventHandler(this.psswBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Password:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(126, 12);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(162, 20);
            this.ipBox.TabIndex = 2;
            this.ipBox.TextChanged += new System.EventHandler(this.ipBox_TextChanged);
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
            this.label3.Location = new System.Drawing.Point(15, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Events Port Number:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Clipboard Port Number:";
            // 
            // eventsPortUpDown
            // 
            this.eventsPortUpDown.Location = new System.Drawing.Point(213, 38);
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
            this.clipboardUpDown.Location = new System.Drawing.Point(213, 64);
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
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(132, 116);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 10;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(213, 116);
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
            this.label5.Location = new System.Drawing.Point(80, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Status:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 144);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(300, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "Status:";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(126, 149);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(73, 13);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "Disconnected";
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 166);
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
            this.Controls.Add(this.ipBox);
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
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown eventsPortUpDown;
        private System.Windows.Forms.NumericUpDown clipboardUpDown;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label statusLabel;
    }
}

