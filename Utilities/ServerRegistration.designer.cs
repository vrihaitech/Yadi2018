namespace Yadi.Utilities
{
    partial class ServerRegistration
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
            this.pnlMain = new OMControls.OMBPanel();
            this.chkBackupUploadCsv = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHrsChkHelp = new System.Windows.Forms.Label();
            this.txtZipMins = new System.Windows.Forms.TextBox();
            this.txtZipHrs = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlDBToCSV = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPortID = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.rbDbToCSV = new System.Windows.Forms.RadioButton();
            this.rbDBToDB = new System.Windows.Forms.RadioButton();
            this.pnlDBToDB = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblMinsChkHelp = new System.Windows.Forms.Label();
            this.txtUploadMins = new System.Windows.Forms.TextBox();
            this.txtUploadHrs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAutoDataUpload = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlDBToCSV.SuspendLayout();
            this.pnlDBToDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.chkBackupUploadCsv);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.lblHrsChkHelp);
            this.pnlMain.Controls.Add(this.txtZipMins);
            this.pnlMain.Controls.Add(this.txtZipHrs);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.pnlDBToCSV);
            this.pnlMain.Controls.Add(this.rbDbToCSV);
            this.pnlMain.Controls.Add(this.rbDBToDB);
            this.pnlMain.Controls.Add(this.pnlDBToDB);
            this.pnlMain.Controls.Add(this.lblMinsChkHelp);
            this.pnlMain.Controls.Add(this.txtUploadMins);
            this.pnlMain.Controls.Add(this.txtUploadHrs);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.chkAutoDataUpload);
            this.pnlMain.Controls.Add(this.label16);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Location = new System.Drawing.Point(31, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(388, 388);
            this.pnlMain.TabIndex = 17;
            // 
            // chkBackupUploadCsv
            // 
            this.chkBackupUploadCsv.AutoSize = true;
            this.chkBackupUploadCsv.Location = new System.Drawing.Point(188, 288);
            this.chkBackupUploadCsv.Name = "chkBackupUploadCsv";
            this.chkBackupUploadCsv.Size = new System.Drawing.Size(40, 17);
            this.chkBackupUploadCsv.TabIndex = 588;
            this.chkBackupUploadCsv.Text = "No";
            this.chkBackupUploadCsv.UseVisualStyleBackColor = true;
            this.chkBackupUploadCsv.CheckedChanged += new System.EventHandler(this.chkSodUploadCsv_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 587;
            this.label6.Text = "Backup Uploaded";
            // 
            // lblHrsChkHelp
            // 
            this.lblHrsChkHelp.AutoSize = true;
            this.lblHrsChkHelp.Location = new System.Drawing.Point(161, 220);
            this.lblHrsChkHelp.Name = "lblHrsChkHelp";
            this.lblHrsChkHelp.Size = new System.Drawing.Size(26, 13);
            this.lblHrsChkHelp.TabIndex = 586;
            this.lblHrsChkHelp.Text = "Hrs.";
            // 
            // txtZipMins
            // 
            this.txtZipMins.Location = new System.Drawing.Point(220, 261);
            this.txtZipMins.MaxLength = 30;
            this.txtZipMins.Name = "txtZipMins";
            this.txtZipMins.Size = new System.Drawing.Size(41, 20);
            this.txtZipMins.TabIndex = 585;
            this.txtZipMins.TextChanged += new System.EventHandler(this.txtZipMins_TextChanged);
            this.txtZipMins.Leave += new System.EventHandler(this.txtZipMins_Leave);
            // 
            // txtZipHrs
            // 
            this.txtZipHrs.Location = new System.Drawing.Point(155, 262);
            this.txtZipHrs.MaxLength = 30;
            this.txtZipHrs.Name = "txtZipHrs";
            this.txtZipHrs.Size = new System.Drawing.Size(41, 20);
            this.txtZipHrs.TabIndex = 584;
            this.txtZipHrs.TextChanged += new System.EventHandler(this.txtZipHrs_TextChanged);
            this.txtZipHrs.Leave += new System.EventHandler(this.txtZipHrs_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 583;
            this.label12.Text = "BackUP Zip After";
            // 
            // pnlDBToCSV
            // 
            this.pnlDBToCSV.Controls.Add(this.label7);
            this.pnlDBToCSV.Controls.Add(this.txtPortID);
            this.pnlDBToCSV.Controls.Add(this.txtUser);
            this.pnlDBToCSV.Controls.Add(this.label8);
            this.pnlDBToCSV.Controls.Add(this.label9);
            this.pnlDBToCSV.Controls.Add(this.txtPwd);
            this.pnlDBToCSV.Controls.Add(this.label10);
            this.pnlDBToCSV.Controls.Add(this.txtHostName);
            this.pnlDBToCSV.Location = new System.Drawing.Point(24, 41);
            this.pnlDBToCSV.Name = "pnlDBToCSV";
            this.pnlDBToCSV.Size = new System.Drawing.Size(346, 152);
            this.pnlDBToCSV.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Port  :";
            // 
            // txtPortID
            // 
            this.txtPortID.Location = new System.Drawing.Point(123, 117);
            this.txtPortID.MaxLength = 50;
            this.txtPortID.Name = "txtPortID";
            this.txtPortID.Size = new System.Drawing.Size(210, 20);
            this.txtPortID.TabIndex = 504;
            this.txtPortID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPortID_KeyDown);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(123, 12);
            this.txtUser.MaxLength = 30;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(210, 20);
            this.txtUser.TabIndex = 501;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "User ID :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "Password :";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(123, 48);
            this.txtPwd.MaxLength = 16;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(210, 20);
            this.txtPwd.TabIndex = 502;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "Host :";
            // 
            // txtHostName
            // 
            this.txtHostName.Location = new System.Drawing.Point(123, 84);
            this.txtHostName.MaxLength = 30;
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(210, 20);
            this.txtHostName.TabIndex = 503;
            // 
            // rbDbToCSV
            // 
            this.rbDbToCSV.AutoSize = true;
            this.rbDbToCSV.Location = new System.Drawing.Point(153, 13);
            this.rbDbToCSV.Name = "rbDbToCSV";
            this.rbDbToCSV.Size = new System.Drawing.Size(74, 17);
            this.rbDbToCSV.TabIndex = 582;
            this.rbDbToCSV.Text = "DBToCSV";
            this.rbDbToCSV.UseVisualStyleBackColor = true;
            this.rbDbToCSV.CheckedChanged += new System.EventHandler(this.rbDbToCSV_CheckedChanged);
            // 
            // rbDBToDB
            // 
            this.rbDBToDB.AutoSize = true;
            this.rbDBToDB.Checked = true;
            this.rbDBToDB.Location = new System.Drawing.Point(42, 13);
            this.rbDBToDB.Name = "rbDBToDB";
            this.rbDBToDB.Size = new System.Drawing.Size(68, 17);
            this.rbDBToDB.TabIndex = 581;
            this.rbDBToDB.TabStop = true;
            this.rbDBToDB.Text = "DBToDB";
            this.rbDBToDB.UseVisualStyleBackColor = true;
            this.rbDBToDB.CheckedChanged += new System.EventHandler(this.rbDBToDB_CheckedChanged);
            // 
            // pnlDBToDB
            // 
            this.pnlDBToDB.Controls.Add(this.label1);
            this.pnlDBToDB.Controls.Add(this.txtServerName);
            this.pnlDBToDB.Controls.Add(this.txtUserID);
            this.pnlDBToDB.Controls.Add(this.label2);
            this.pnlDBToDB.Controls.Add(this.label4);
            this.pnlDBToDB.Controls.Add(this.txtPassword);
            this.pnlDBToDB.Controls.Add(this.label3);
            this.pnlDBToDB.Controls.Add(this.txtDatabase);
            this.pnlDBToDB.Location = new System.Drawing.Point(21, 46);
            this.pnlDBToDB.Name = "pnlDBToDB";
            this.pnlDBToDB.Size = new System.Drawing.Size(346, 152);
            this.pnlDBToDB.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Server Name :";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(123, 14);
            this.txtServerName.MaxLength = 50;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(210, 20);
            this.txtServerName.TabIndex = 0;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(123, 88);
            this.txtUserID.MaxLength = 30;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(210, 20);
            this.txtUserID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "User ID :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(123, 124);
            this.txtPassword.MaxLength = 16;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(210, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Database :";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(123, 51);
            this.txtDatabase.MaxLength = 30;
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(210, 20);
            this.txtDatabase.TabIndex = 1;
            // 
            // lblMinsChkHelp
            // 
            this.lblMinsChkHelp.AutoSize = true;
            this.lblMinsChkHelp.Location = new System.Drawing.Point(221, 220);
            this.lblMinsChkHelp.Name = "lblMinsChkHelp";
            this.lblMinsChkHelp.Size = new System.Drawing.Size(32, 13);
            this.lblMinsChkHelp.TabIndex = 580;
            this.lblMinsChkHelp.Text = "Mins.";
            // 
            // txtUploadMins
            // 
            this.txtUploadMins.Location = new System.Drawing.Point(220, 236);
            this.txtUploadMins.MaxLength = 30;
            this.txtUploadMins.Name = "txtUploadMins";
            this.txtUploadMins.Size = new System.Drawing.Size(41, 20);
            this.txtUploadMins.TabIndex = 579;
            this.txtUploadMins.TextChanged += new System.EventHandler(this.txtUploadMins_TextChanged);
            this.txtUploadMins.Leave += new System.EventHandler(this.txtUploadMins_Leave);
            // 
            // txtUploadHrs
            // 
            this.txtUploadHrs.Location = new System.Drawing.Point(155, 237);
            this.txtUploadHrs.MaxLength = 30;
            this.txtUploadHrs.Name = "txtUploadHrs";
            this.txtUploadHrs.Size = new System.Drawing.Size(41, 20);
            this.txtUploadHrs.TabIndex = 578;
            this.txtUploadHrs.TextChanged += new System.EventHandler(this.txtUploadHrs_TextChanged);
            this.txtUploadHrs.Leave += new System.EventHandler(this.txtUploadHrs_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 577;
            this.label5.Text = "Ping After";
            // 
            // chkAutoDataUpload
            // 
            this.chkAutoDataUpload.AutoSize = true;
            this.chkAutoDataUpload.Location = new System.Drawing.Point(188, 204);
            this.chkAutoDataUpload.Name = "chkAutoDataUpload";
            this.chkAutoDataUpload.Size = new System.Drawing.Size(40, 17);
            this.chkAutoDataUpload.TabIndex = 576;
            this.chkAutoDataUpload.Text = "No";
            this.chkAutoDataUpload.UseVisualStyleBackColor = true;
            this.chkAutoDataUpload.CheckedChanged += new System.EventHandler(this.chkAutoDataUpload_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(39, 204);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 13);
            this.label16.TabIndex = 575;
            this.label16.Text = "Auto Uploaded Data";
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(113, 314);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(208, 314);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // ServerRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 417);
            this.Controls.Add(this.pnlMain);
            this.Name = "ServerRegistration";
            this.Text = "Server Registration";
            this.Load += new System.EventHandler(this.ServerRegistration_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlDBToCSV.ResumeLayout(false);
            this.pnlDBToCSV.PerformLayout();
            this.pnlDBToDB.ResumeLayout(false);
            this.pnlDBToDB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblMinsChkHelp;
        private System.Windows.Forms.TextBox txtUploadMins;
        private System.Windows.Forms.TextBox txtUploadHrs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAutoDataUpload;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlDBToDB;
        private System.Windows.Forms.RadioButton rbDbToCSV;
        private System.Windows.Forms.RadioButton rbDBToDB;
        private System.Windows.Forms.Panel pnlDBToCSV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPortID;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Label lblHrsChkHelp;
        private System.Windows.Forms.TextBox txtZipMins;
        private System.Windows.Forms.TextBox txtZipHrs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkBackupUploadCsv;
        private System.Windows.Forms.Label label6;
    }
}