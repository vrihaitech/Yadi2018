namespace Yadi.Settings
{
    partial class BarcodePrintingPurchase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMainPur = new OMControls.OMBPanel();
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
            this.pnlPur = new OMControls.OMBPanel();
            this.btnExitPur = new System.Windows.Forms.Button();
            this.btnOKPur = new System.Windows.Forms.Button();
            this.GridViewPur = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MrpRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMainPur.SuspendLayout();
            this.pnlExpDays.SuspendLayout();
            this.pnlDateType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.pnlPur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewPur)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMainPur
            // 
            this.pnlMainPur.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlMainPur.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainPur.BorderRadius = 3;
            this.pnlMainPur.Controls.Add(this.pnlExpDays);
            this.pnlMainPur.Controls.Add(this.dtpDate);
            this.pnlMainPur.Controls.Add(this.lblChkHelp1);
            this.pnlMainPur.Controls.Add(this.lblChkHelp);
            this.pnlMainPur.Controls.Add(this.txtStartNo);
            this.pnlMainPur.Controls.Add(this.txtNoOfPrint);
            this.pnlMainPur.Controls.Add(this.label3);
            this.pnlMainPur.Controls.Add(this.label2);
            this.pnlMainPur.Controls.Add(this.pnlDateType);
            this.pnlMainPur.Controls.Add(this.btnCancel);
            this.pnlMainPur.Controls.Add(this.BtnExit);
            this.pnlMainPur.Controls.Add(this.btnOk);
            this.pnlMainPur.Controls.Add(this.GridView);
            this.pnlMainPur.Controls.Add(this.label1);
            this.pnlMainPur.Controls.Add(this.cmbBarcodeTemplate);
            this.pnlMainPur.Location = new System.Drawing.Point(12, 270);
            this.pnlMainPur.Name = "pnlMainPur";
            this.pnlMainPur.Size = new System.Drawing.Size(711, 406);
            this.pnlMainPur.TabIndex = 2;
            this.pnlMainPur.Visible = false;
            // 
            // pnlExpDays
            // 
            this.pnlExpDays.Controls.Add(this.label4);
            this.pnlExpDays.Controls.Add(this.txtExpDays);
            this.pnlExpDays.Controls.Add(this.btnCancelExpDays);
            this.pnlExpDays.Controls.Add(this.btnOkExpDays);
            this.pnlExpDays.Location = new System.Drawing.Point(26, 75);
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
            // 
            // btnCancelExpDays
            // 
            this.btnCancelExpDays.Location = new System.Drawing.Point(94, 65);
            this.btnCancelExpDays.Name = "btnCancelExpDays";
            this.btnCancelExpDays.Size = new System.Drawing.Size(75, 23);
            this.btnCancelExpDays.TabIndex = 25;
            this.btnCancelExpDays.Text = "Cancel";
            this.btnCancelExpDays.UseVisualStyleBackColor = true;
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
            this.dtpDate.Location = new System.Drawing.Point(273, 22);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.TabIndex = 1000712;
            this.dtpDate.Visible = false;
            this.dtpDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.dtpDate_DateSelected);
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // lblChkHelp1
            // 
            this.lblChkHelp1.AutoSize = true;
            this.lblChkHelp1.BackColor = System.Drawing.Color.Transparent;
            this.lblChkHelp1.Location = new System.Drawing.Point(23, 313);
            this.lblChkHelp1.Name = "lblChkHelp1";
            this.lblChkHelp1.Size = new System.Drawing.Size(141, 13);
            this.lblChkHelp1.TabIndex = 1000711;
            this.lblChkHelp1.Text = "Weight : (Eg. 250 gm, 2  Kg)";
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(282, 321);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(141, 78);
            this.lblChkHelp.TabIndex = 39;
            this.lblChkHelp.Text = "d-MMM-yy (1-Mar-99)\r\nd-MMM-yyyy (1-Mar-1999)\r\ndd-MMM-yyyy (01-Mar-1999)\r\nMM-yyyy " +
                "(09-1999)\r\nMM-yy (03 -99)\r\ndd-MM-yyyy (01-03-1999)";
            // 
            // txtStartNo
            // 
            this.txtStartNo.Location = new System.Drawing.Point(394, 366);
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
            this.txtNoOfPrint.Location = new System.Drawing.Point(377, 12);
            this.txtNoOfPrint.Name = "txtNoOfPrint";
            this.txtNoOfPrint.Size = new System.Drawing.Size(107, 20);
            this.txtNoOfPrint.TabIndex = 37;
            this.txtNoOfPrint.Text = "1";
            this.txtNoOfPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoOfPrint.Visible = false;
            this.txtNoOfPrint.TextChanged += new System.EventHandler(this.txtNoOfPrint_TextChanged);
            this.txtNoOfPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfPrint_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Start No.";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "No.Of Sticker";
            this.label2.Visible = false;
            // 
            // pnlDateType
            // 
            this.pnlDateType.Controls.Add(this.lstDateTpe);
            this.pnlDateType.Location = new System.Drawing.Point(204, 127);
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
            this.btnCancel.Location = new System.Drawing.Point(109, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(196, 335);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 31;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 335);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 60);
            this.btnOk.TabIndex = 30;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.PkSrNo,
            this.SettingNo,
            this.Options,
            this.Input,
            this.Print});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.GridView.Location = new System.Drawing.Point(23, 43);
            this.GridView.Name = "GridView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridView.Size = new System.Drawing.Size(550, 267);
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
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template";
            // 
            // cmbBarcodeTemplate
            // 
            this.cmbBarcodeTemplate.FormattingEnabled = true;
            this.cmbBarcodeTemplate.Location = new System.Drawing.Point(111, 11);
            this.cmbBarcodeTemplate.Name = "cmbBarcodeTemplate";
            this.cmbBarcodeTemplate.Size = new System.Drawing.Size(169, 21);
            this.cmbBarcodeTemplate.TabIndex = 1;
            this.cmbBarcodeTemplate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbBarcodeTemplate_MouseClick);
            this.cmbBarcodeTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbBarcodeTemplate_SelectedIndexChanged);
            this.cmbBarcodeTemplate.Leave += new System.EventHandler(this.cmbBarcodeTemplate_Leave);
            this.cmbBarcodeTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBarcodeTemplate_KeyDown);
            // 
            // pnlPur
            // 
            this.pnlPur.BorderColor = System.Drawing.Color.Gray;
            this.pnlPur.BorderRadius = 3;
            this.pnlPur.Controls.Add(this.btnExitPur);
            this.pnlPur.Controls.Add(this.btnOKPur);
            this.pnlPur.Controls.Add(this.GridViewPur);
            this.pnlPur.Location = new System.Drawing.Point(12, 9);
            this.pnlPur.Name = "pnlPur";
            this.pnlPur.Size = new System.Drawing.Size(711, 254);
            this.pnlPur.TabIndex = 1000714;
            this.pnlPur.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPur_Paint);
            // 
            // btnExitPur
            // 
            this.btnExitPur.Location = new System.Drawing.Point(407, 184);
            this.btnExitPur.Name = "btnExitPur";
            this.btnExitPur.Size = new System.Drawing.Size(80, 60);
            this.btnExitPur.TabIndex = 31;
            this.btnExitPur.Text = "Exit";
            this.btnExitPur.UseVisualStyleBackColor = false;
            this.btnExitPur.Click += new System.EventHandler(this.btnExitPur_Click);
            // 
            // btnOKPur
            // 
            this.btnOKPur.Location = new System.Drawing.Point(320, 184);
            this.btnOKPur.Name = "btnOKPur";
            this.btnOKPur.Size = new System.Drawing.Size(80, 60);
            this.btnOKPur.TabIndex = 30;
            this.btnOKPur.Text = "Ok";
            this.btnOKPur.UseVisualStyleBackColor = false;
            this.btnOKPur.Click += new System.EventHandler(this.btnOKPur_Click);
            // 
            // GridViewPur
            // 
            this.GridViewPur.AllowUserToAddRows = false;
            this.GridViewPur.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewPur.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.GridViewPur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewPur.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ItemName,
            this.MrpRate,
            this.SaleRate,
            this.PurRate,
            this.BillQty,
            this.PrintQty,
            this.ItemNo,
            this.Select,
            this.Code,
            this.SupplierCode});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridViewPur.DefaultCellStyle = dataGridViewCellStyle7;
            this.GridViewPur.Location = new System.Drawing.Point(23, 13);
            this.GridViewPur.Name = "GridViewPur";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewPur.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.GridViewPur.Size = new System.Drawing.Size(671, 160);
            this.GridViewPur.TabIndex = 2;
            this.GridViewPur.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewPur_CellFormatting);
            this.GridViewPur.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewPur_CellEndEdit);
            this.GridViewPur.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewPur_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SrNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "SrNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 300;
            // 
            // MrpRate
            // 
            this.MrpRate.HeaderText = "MRP";
            this.MrpRate.Name = "MrpRate";
            this.MrpRate.Visible = false;
            this.MrpRate.Width = 70;
            // 
            // SaleRate
            // 
            this.SaleRate.HeaderText = "SaleRate";
            this.SaleRate.Name = "SaleRate";
            this.SaleRate.Width = 70;
            // 
            // PurRate
            // 
            this.PurRate.HeaderText = "PurRate";
            this.PurRate.Name = "PurRate";
            this.PurRate.Width = 70;
            // 
            // BillQty
            // 
            this.BillQty.HeaderText = "BillQty";
            this.BillQty.MaxInputLength = 32789;
            this.BillQty.Name = "BillQty";
            this.BillQty.Width = 70;
            // 
            // PrintQty
            // 
            this.PrintQty.HeaderText = "PrintQty";
            this.PrintQty.Name = "PrintQty";
            this.PrintQty.Width = 70;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            this.ItemNo.Width = 70;
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            // 
            // SupplierCode
            // 
            this.SupplierCode.HeaderText = "SupplierCode";
            this.SupplierCode.Name = "SupplierCode";
            // 
            // BarcodePrintingPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 688);
            this.Controls.Add(this.pnlPur);
            this.Controls.Add(this.pnlMainPur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BarcodePrintingPurchase";
            this.Text = "Barcode Printing";
            this.Load += new System.EventHandler(this.BarcodePrinting_Load);
            this.pnlMainPur.ResumeLayout(false);
            this.pnlMainPur.PerformLayout();
            this.pnlExpDays.ResumeLayout(false);
            this.pnlExpDays.PerformLayout();
            this.pnlDateType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.pnlPur.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewPur)).EndInit();
            this.ResumeLayout(false);

        }

       
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBarcodeTemplate;
        private OMControls.OMBPanel pnlMainPur;
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
        private OMControls.OMBPanel pnlPur;
        private System.Windows.Forms.Button btnOKPur;
        private System.Windows.Forms.DataGridView GridViewPur;
        private System.Windows.Forms.Button btnExitPur;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MrpRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private new System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierCode;
    }
}