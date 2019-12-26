namespace Yadi.Settings
{
    partial class BarcodePrinting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlExpDays = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExpDays = new System.Windows.Forms.TextBox();
            this.btnCancelExpDays = new System.Windows.Forms.Button();
            this.btnOkExpDays = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.MonthCalendar();
            this.lblChkHelp1 = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.txtNoOfPrint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDateType = new System.Windows.Forms.Panel();
            this.lstDateTpe = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Options = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBarcodeTemplate = new System.Windows.Forms.ComboBox();
            this.pnlMain.SuspendLayout();
            this.pnlExpDays.SuspendLayout();
            this.pnlDateType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlExpDays);
            this.pnlMain.Controls.Add(this.dtpDate);
            this.pnlMain.Controls.Add(this.lblChkHelp1);
            this.pnlMain.Controls.Add(this.lblChkHelp);
            this.pnlMain.Controls.Add(this.txtStartNo);
            this.pnlMain.Controls.Add(this.txtNoOfPrint);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.pnlDateType);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnOk);
            this.pnlMain.Controls.Add(this.GridView);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.cmbBarcodeTemplate);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(524, 448);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlExpDays
            // 
            this.pnlExpDays.Controls.Add(this.label4);
            this.pnlExpDays.Controls.Add(this.txtExpDays);
            this.pnlExpDays.Controls.Add(this.btnCancelExpDays);
            this.pnlExpDays.Controls.Add(this.btnOkExpDays);
            this.pnlExpDays.Location = new System.Drawing.Point(26, 118);
            this.pnlExpDays.Name = "pnlExpDays";
            this.pnlExpDays.Size = new System.Drawing.Size(235, 98);
            this.pnlExpDays.TabIndex = 1000713;
            this.pnlExpDays.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Exp Days";
            // 
            // txtExpDays
            // 
            this.txtExpDays.Location = new System.Drawing.Point(76, 23);
            this.txtExpDays.Name = "txtExpDays";
            this.txtExpDays.Size = new System.Drawing.Size(121, 20);
            this.txtExpDays.TabIndex = 0;
            this.txtExpDays.TextChanged += new System.EventHandler(this.txtExpDays_TextChanged);
            this.txtExpDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpDays_KeyPress);
            // 
            // btnCancelExpDays
            // 
            this.btnCancelExpDays.Location = new System.Drawing.Point(94, 65);
            this.btnCancelExpDays.Name = "btnCancelExpDays";
            this.btnCancelExpDays.Size = new System.Drawing.Size(75, 23);
            this.btnCancelExpDays.TabIndex = 25;
            this.btnCancelExpDays.Text = "Cancel";
            this.btnCancelExpDays.UseVisualStyleBackColor = true;
            this.btnCancelExpDays.Click += new System.EventHandler(this.btnCancelExpDays_Click);
            // 
            // btnOkExpDays
            // 
            this.btnOkExpDays.Location = new System.Drawing.Point(13, 65);
            this.btnOkExpDays.Name = "btnOkExpDays";
            this.btnOkExpDays.Size = new System.Drawing.Size(75, 23);
            this.btnOkExpDays.TabIndex = 1;
            this.btnOkExpDays.Text = "Ok";
            this.btnOkExpDays.UseVisualStyleBackColor = true;
            this.btnOkExpDays.Click += new System.EventHandler(this.btnOkExpDays_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(273, 65);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.TabIndex = 1000712;
            this.dtpDate.Visible = false;
            this.dtpDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.dtpDate_DateSelected);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            // 
            // lblChkHelp1
            // 
            this.lblChkHelp1.AutoSize = true;
            this.lblChkHelp1.BackColor = System.Drawing.Color.Transparent;
            this.lblChkHelp1.Location = new System.Drawing.Point(23, 356);
            this.lblChkHelp1.Name = "lblChkHelp1";
            this.lblChkHelp1.Size = new System.Drawing.Size(141, 13);
            this.lblChkHelp1.TabIndex = 1000711;
            this.lblChkHelp1.Text = "Weight : (Eg. 250 gm, 2  Kg)";
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(282, 364);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(141, 78);
            this.lblChkHelp.TabIndex = 39;
            this.lblChkHelp.Text = "d-MMM-yy (1-Mar-99)\r\nd-MMM-yyyy (1-Mar-1999)\r\ndd-MMM-yyyy (01-Mar-1999)\r\nMM-yyyy " +
    "(09-1999)\r\nMM-yy (03 -99)\r\ndd-MM-yyyy (01-03-1999)";
            // 
            // txtStartNo
            // 
            this.txtStartNo.Location = new System.Drawing.Point(273, 44);
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(111, 20);
            this.txtStartNo.TabIndex = 38;
            this.txtStartNo.Text = "0";
            this.txtStartNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStartNo.Visible = false;
            this.txtStartNo.TextChanged += new System.EventHandler(this.txtStartNo_TextChanged);
            this.txtStartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartNo_KeyDown);
            // 
            // txtNoOfPrint
            // 
            this.txtNoOfPrint.Location = new System.Drawing.Point(111, 44);
            this.txtNoOfPrint.Name = "txtNoOfPrint";
            this.txtNoOfPrint.Size = new System.Drawing.Size(107, 20);
            this.txtNoOfPrint.TabIndex = 37;
            this.txtNoOfPrint.Text = "1";
            this.txtNoOfPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfPrint.TextChanged += new System.EventHandler(this.txtNoOfPrint_TextChanged);
            this.txtNoOfPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfPrint_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Start No.";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "No.Of Sticker";
            // 
            // pnlDateType
            // 
            this.pnlDateType.Controls.Add(this.lstDateTpe);
            this.pnlDateType.Location = new System.Drawing.Point(204, 170);
            this.pnlDateType.Name = "pnlDateType";
            this.pnlDateType.Size = new System.Drawing.Size(117, 88);
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
            this.lstDateTpe.Size = new System.Drawing.Size(111, 82);
            this.lstDateTpe.TabIndex = 0;
            this.lstDateTpe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstDateTpe_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(109, 378);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(196, 378);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 31;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 378);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 60);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.GridView.Location = new System.Drawing.Point(23, 86);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(462, 267);
            this.GridView.TabIndex = 2;
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            this.GridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridView_EditingControlShowing);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle2;
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
            this.Input.MaxInputLength = 32789;
            this.Input.Name = "Input";
            this.Input.Width = 140;
            // 
            // Print
            // 
            this.Print.DataPropertyName = "IsActive";
            this.Print.HeaderText = "Print";
            this.Print.Name = "Print";
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
            this.cmbBarcodeTemplate.Location = new System.Drawing.Point(111, 12);
            this.cmbBarcodeTemplate.Name = "cmbBarcodeTemplate";
            this.cmbBarcodeTemplate.Size = new System.Drawing.Size(169, 21);
            this.cmbBarcodeTemplate.TabIndex = 1;
            this.cmbBarcodeTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbBarcodeTemplate_SelectedIndexChanged);
            this.cmbBarcodeTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBarcodeTemplate_KeyDown);
            this.cmbBarcodeTemplate.Leave += new System.EventHandler(this.cmbBarcodeTemplate_Leave);
            this.cmbBarcodeTemplate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbBarcodeTemplate_MouseClick);
            // 
            // BarcodePrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 472);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BarcodePrinting";
            this.Text = "Barcode Printing";
            this.Load += new System.EventHandler(this.BarcodePrinting_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlExpDays.ResumeLayout(false);
            this.pnlExpDays.PerformLayout();
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lstDateTpe;
        private System.Windows.Forms.Panel pnlDateType;
        private System.Windows.Forms.TextBox txtStartNo;
        private System.Windows.Forms.TextBox txtNoOfPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblChkHelp1;
        private System.Windows.Forms.MonthCalendar dtpDate;
        private System.Windows.Forms.Panel pnlExpDays;
        private System.Windows.Forms.Button btnCancelExpDays;
        private System.Windows.Forms.Button btnOkExpDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExpDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Options;
        private System.Windows.Forms.DataGridViewTextBoxColumn Input;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Print;
    }
}