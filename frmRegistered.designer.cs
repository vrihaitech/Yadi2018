namespace Yadi
{
    partial class frmRegistered
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.MaskedTextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegistered = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtRegID = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdServer = new System.Windows.Forms.RadioButton();
            this.rdClient = new System.Windows.Forms.RadioButton();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Gray;
            this.Label3.Location = new System.Drawing.Point(6, 38);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(354, 15);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "Tip : Key send to Software Team. They are taken Registered Key.";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Location = new System.Drawing.Point(95, 12);
            this.txtKey.Mask = "####:####:####:####:####:####";
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(235, 23);
            this.txtKey.TabIndex = 13;
            this.txtKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            this.txtKey.GotFocus += new System.EventHandler(this.txtKey_GotFocus);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(27, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 15);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Key :";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(198, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(46, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegistered
            // 
            this.btnRegistered.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnRegistered.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistered.ForeColor = System.Drawing.Color.White;
            this.btnRegistered.Location = new System.Drawing.Point(104, 92);
            this.btnRegistered.Name = "btnRegistered";
            this.btnRegistered.Size = new System.Drawing.Size(88, 25);
            this.btnRegistered.TabIndex = 10;
            this.btnRegistered.Text = "Registered";
            this.btnRegistered.UseVisualStyleBackColor = false;
            this.btnRegistered.Click += new System.EventHandler(this.btnRegistered_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(27, 66);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 15);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Serial No :";
            // 
            // txtRegID
            // 
            this.txtRegID.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegID.Location = new System.Drawing.Point(95, 63);
            this.txtRegID.Mask = "####:####:####:####:####:####";
            this.txtRegID.Name = "txtRegID";
            this.txtRegID.Size = new System.Drawing.Size(235, 23);
            this.txtRegID.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "For";
            // 
            // rdServer
            // 
            this.rdServer.AutoSize = true;
            this.rdServer.Checked = true;
            this.rdServer.Location = new System.Drawing.Point(58, 125);
            this.rdServer.Name = "rdServer";
            this.rdServer.Size = new System.Drawing.Size(56, 17);
            this.rdServer.TabIndex = 16;
            this.rdServer.TabStop = true;
            this.rdServer.Text = "Server";
            this.rdServer.UseVisualStyleBackColor = true;
            this.rdServer.CheckedChanged += new System.EventHandler(this.rdServer_CheckedChanged);
            // 
            // rdClient
            // 
            this.rdClient.AutoSize = true;
            this.rdClient.Location = new System.Drawing.Point(120, 125);
            this.rdClient.Name = "rdClient";
            this.rdClient.Size = new System.Drawing.Size(51, 17);
            this.rdClient.TabIndex = 17;
            this.rdClient.Text = "Client";
            this.rdClient.UseVisualStyleBackColor = true;
            this.rdClient.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(177, 125);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(118, 20);
            this.txtServerName.TabIndex = 18;
            this.txtServerName.Visible = false;
            // 
            // frmRegistered
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(366, 147);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.rdClient);
            this.Controls.Add(this.rdServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegistered);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtRegID);
            this.Name = "frmRegistered";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registered System";
            this.Load += new System.EventHandler(this.frmRegistered_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.MaskedTextBox txtKey;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnRegistered;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.MaskedTextBox txtRegID;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdServer;
        private System.Windows.Forms.RadioButton rdClient;
        private System.Windows.Forms.TextBox txtServerName;
    }
}