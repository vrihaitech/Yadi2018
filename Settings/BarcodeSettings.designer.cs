namespace Yadi.Settings
{
    partial class BarcodeSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBarcodeTemplate = new System.Windows.Forms.ComboBox();
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlDateType = new System.Windows.Forms.Panel();
            this.lstDateTpe = new System.Windows.Forms.ListBox();
            this.lblChkHelp1 = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Options = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlDateType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template";
            // 
            // cmbBarcodeTemplate
            // 
            this.cmbBarcodeTemplate.FormattingEnabled = true;
            this.cmbBarcodeTemplate.Location = new System.Drawing.Point(91, 12);
            this.cmbBarcodeTemplate.Name = "cmbBarcodeTemplate";
            this.cmbBarcodeTemplate.Size = new System.Drawing.Size(176, 21);
            this.cmbBarcodeTemplate.TabIndex = 1;
            this.cmbBarcodeTemplate.Leave += new System.EventHandler(this.cmbBarcodeTemplate_Leave);
            this.cmbBarcodeTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBarcodeTemplate_KeyDown);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlDateType);
            this.pnlMain.Controls.Add(this.lblChkHelp1);
            this.pnlMain.Controls.Add(this.lblChkHelp);
            this.pnlMain.Controls.Add(this.dtpDate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.GridView);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.cmbBarcodeTemplate);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(510, 453);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlDateType
            // 
            this.pnlDateType.Controls.Add(this.lstDateTpe);
            this.pnlDateType.Location = new System.Drawing.Point(204, 170);
            this.pnlDateType.Name = "pnlDateType";
            this.pnlDateType.Size = new System.Drawing.Size(117, 118);
            this.pnlDateType.TabIndex = 34;
            this.pnlDateType.Visible = false;
            // 
            // lstDateTpe
            // 
            this.lstDateTpe.FormattingEnabled = true;
            this.lstDateTpe.Items.AddRange(new object[] {
            "d-MMM-yy",
            "d-MMM-yyyy",
            "dd-MMM-yyy",
            "MM-yyyy",
            "MM-yy",
            "dd-MM-yyyy"});
            this.lstDateTpe.Location = new System.Drawing.Point(2, 2);
            this.lstDateTpe.Name = "lstDateTpe";
            this.lstDateTpe.Size = new System.Drawing.Size(111, 108);
            this.lstDateTpe.TabIndex = 0;
            this.lstDateTpe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstDateTpe_KeyDown);
            // 
            // lblChkHelp1
            // 
            this.lblChkHelp1.AutoSize = true;
            this.lblChkHelp1.BackColor = System.Drawing.Color.Transparent;
            this.lblChkHelp1.Location = new System.Drawing.Point(21, 331);
            this.lblChkHelp1.Name = "lblChkHelp1";
            this.lblChkHelp1.Size = new System.Drawing.Size(141, 13);
            this.lblChkHelp1.TabIndex = 1000710;
            this.lblChkHelp1.Text = "Weight : (Eg. 250 gm, 2  Kg)";
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(282, 337);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(141, 78);
            this.lblChkHelp.TabIndex = 40;
            this.lblChkHelp.Text = "d-MMM-yy (1-Mar-99)\r\nd-MMM-yyyy (1-Mar-1999)\r\ndd-MMM-yyyy (01-Mar-1999)\r\nMM-yyyy " +
                "(09-1999)\r\nMM-yy (03 -99)\r\ndd-MM-yyyy (01-03-1999)";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(273, 16);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(111, 20);
            this.dtpDate.TabIndex = 33;
            this.dtpDate.Visible = false;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(109, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(196, 351);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 31;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(23, 351);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 60);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.PkSrNo,
            this.SettingNo,
            this.Options,
            this.Input,
            this.Print});
            this.GridView.Location = new System.Drawing.Point(23, 42);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(458, 286);
            this.GridView.TabIndex = 2;
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            this.GridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridView_EditingControlShowing);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // PkSrNo
            // 
            this.PkSrNo.DataPropertyName = "PkSrNo";
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // SettingNo
            // 
            this.SettingNo.DataPropertyName = "SettingNo";
            this.SettingNo.HeaderText = "SettingNo";
            this.SettingNo.Name = "SettingNo";
            this.SettingNo.Visible = false;
            // 
            // Options
            // 
            this.Options.DataPropertyName = "SettingsKeyCode";
            this.Options.HeaderText = "Options";
            this.Options.Name = "Options";
            this.Options.ReadOnly = true;
            this.Options.Width = 120;
            // 
            // Input
            // 
            this.Input.DataPropertyName = "SettingValue";
            this.Input.HeaderText = "Input";
            this.Input.Name = "Input";
            this.Input.Width = 140;
            // 
            // Print
            // 
            this.Print.DataPropertyName = "IsActive";
            this.Print.HeaderText = "Print";
            this.Print.Name = "Print";
            // 
            // BarcodeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 477);
            this.Controls.Add(this.pnlMain);
            this.Name = "BarcodeSettings";
            this.Text = "Barcode Settings";
            this.Load += new System.EventHandler(this.BarcodeSettings_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlDateType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBarcodeTemplate;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ListBox lstDateTpe;
        private System.Windows.Forms.Panel pnlDateType;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblChkHelp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Options;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Print;
    }
}