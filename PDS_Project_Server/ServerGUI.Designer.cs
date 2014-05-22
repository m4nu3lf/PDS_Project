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
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventsPortUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // psswBox
            // 
            this.psswBox.Location = new System.Drawing.Point(126, 90);
            this.psswBox.Name = "psswBox";
            this.psswBox.Size = new System.Drawing.Size(100, 20);
            this.psswBox.TabIndex = 0;
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
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 2;
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
            this.eventsPortUpDown.Location = new System.Drawing.Point(126, 38);
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
            this.eventsPortUpDown.Size = new System.Drawing.Size(100, 20);
            this.eventsPortUpDown.TabIndex = 8;
            this.eventsPortUpDown.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // clipboardUpDown
            // 
            this.clipboardUpDown.Location = new System.Drawing.Point(126, 64);
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
            this.clipboardUpDown.Size = new System.Drawing.Size(100, 20);
            this.clipboardUpDown.TabIndex = 9;
            this.clipboardUpDown.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(151, 116);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 149);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.clipboardUpDown);
            this.Controls.Add(this.eventsPortUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.psswBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ServerGUI";
            this.Text = "Form1";
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
        private System.Windows.Forms.Button saveButton;
    }
}

