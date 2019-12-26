namespace Yadi.Display
{
    partial class LedgerBook
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabPage3 = new OMControls.OMTabPage();
            this.lblTotal = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.GridViewDaily = new System.Windows.Forms.DataGridView();
            this.btnCancelt2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDatewise = new System.Windows.Forms.Label();
            this.tabPage2 = new OMControls.OMTabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnCancelt1 = new System.Windows.Forms.Button();
            this.lblMonthDtls = new System.Windows.Forms.Label();
            this.tabPage1 = new OMControls.OMTabPage();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.tabLedgerBook = new OMControls.OMTabControl();
            this.plnLedger = new System.Windows.Forms.Panel();
            this.dgLedger = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortLedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnSLedger = new System.Windows.Forms.Button();
            this.pnlMainForm = new OMControls.OMBPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbMultibillPrint = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbBillwise = new System.Windows.Forms.RadioButton();
            this.rbDetailed = new System.Windows.Forms.RadioButton();
            this.rbMonthwise = new System.Windows.Forms.RadioButton();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbSupplier = new System.Windows.Forms.RadioButton();
            this.rbCustomer = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.tabLedgerBook.SuspendLayout();
            this.plnLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLedger)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "From Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(101, 6);
            this.DTPFromDate.MaxDate = new System.DateTime(2029, 1, 1, 0, 0, 0, 0);
            this.DTPFromDate.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(241, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(319, 6);
            this.DTToDate.MaxDate = new System.DateTime(2029, 1, 1, 0, 0, 0, 0);
            this.DTToDate.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(129, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTToDate_KeyDown);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblTotal);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.GridViewDaily);
            this.tabPage3.Controls.Add(this.btnCancelt2);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.lblDatewise);
            this.tabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage3.ImageIndex = -1;
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(820, 533);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ledger  Book Voucher Entry Details";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(545, 510);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(35, 13);
            this.lblTotal.TabIndex = 79;
            this.lblTotal.Text = "label3";
            this.lblTotal.Visible = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(734, 510);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 78;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GridViewDaily
            // 
            this.GridViewDaily.AllowUserToAddRows = false;
            this.GridViewDaily.AllowUserToDeleteRows = false;
            this.GridViewDaily.AllowUserToResizeRows = false;
            this.GridViewDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridViewDaily.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDaily.Dock = System.Windows.Forms.DockStyle.Top;
            this.GridViewDaily.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewDaily.Location = new System.Drawing.Point(0, 27);
            this.GridViewDaily.Name = "GridViewDaily";
            this.GridViewDaily.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewDaily.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowTemplate.Height = 27;
            this.GridViewDaily.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewDaily.Size = new System.Drawing.Size(820, 483);
            this.GridViewDaily.TabIndex = 60;
            this.GridViewDaily.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewDaily_CellClick);
            this.GridViewDaily.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewDaily_CellFormatting);
            this.GridViewDaily.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewDaily_KeyDown);
            // 
            // btnCancelt2
            // 
            this.btnCancelt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelt2.Location = new System.Drawing.Point(724, 548);
            this.btnCancelt2.Name = "btnCancelt2";
            this.btnCancelt2.Size = new System.Drawing.Size(75, 23);
            this.btnCancelt2.TabIndex = 59;
            this.btnCancelt2.Text = "Cancel";
            this.btnCancelt2.UseVisualStyleBackColor = true;
            this.btnCancelt2.Click += new System.EventHandler(this.btnCancelt2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(690, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 18);
            this.label7.TabIndex = 57;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDatewise
            // 
            this.lblDatewise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblDatewise.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDatewise.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatewise.ForeColor = System.Drawing.Color.White;
            this.lblDatewise.Location = new System.Drawing.Point(0, 0);
            this.lblDatewise.Name = "lblDatewise";
            this.lblDatewise.Size = new System.Drawing.Size(820, 27);
            this.lblDatewise.TabIndex = 56;
            this.lblDatewise.Text = "Voucher Entry Details";
            this.lblDatewise.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.DataGridView2);
            this.tabPage2.Controls.Add(this.btnCancelt1);
            this.tabPage2.Controls.Add(this.lblMonthDtls);
            this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage2.ImageIndex = -1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(820, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Ledger Book Details";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(736, 507);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 61;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataGridView2
            // 
            this.DataGridView2.AllowUserToAddRows = false;
            this.DataGridView2.AllowUserToDeleteRows = false;
            this.DataGridView2.AllowUserToResizeColumns = false;
            this.DataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView2.Location = new System.Drawing.Point(0, 39);
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowTemplate.Height = 27;
            this.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView2.Size = new System.Drawing.Size(820, 462);
            this.DataGridView2.TabIndex = 60;
            this.DataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellClick);
            this.DataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView2_CellFormatting);
            // 
            // btnCancelt1
            // 
            this.btnCancelt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelt1.Location = new System.Drawing.Point(724, 633);
            this.btnCancelt1.Name = "btnCancelt1";
            this.btnCancelt1.Size = new System.Drawing.Size(75, 23);
            this.btnCancelt1.TabIndex = 59;
            this.btnCancelt1.Text = "Cancel";
            this.btnCancelt1.UseVisualStyleBackColor = true;
            this.btnCancelt1.Click += new System.EventHandler(this.btnCancelt1_Click);
            // 
            // lblMonthDtls
            // 
            this.lblMonthDtls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblMonthDtls.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthDtls.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthDtls.ForeColor = System.Drawing.Color.White;
            this.lblMonthDtls.Location = new System.Drawing.Point(0, 0);
            this.lblMonthDtls.Name = "lblMonthDtls";
            this.lblMonthDtls.Size = new System.Drawing.Size(820, 39);
            this.lblMonthDtls.TabIndex = 58;
            this.lblMonthDtls.Text = "Ledger Book Details";
            this.lblMonthDtls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGridView1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnBack);
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = -1;
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(820, 533);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ledger Book";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(0, 23);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(820, 356);
            this.DataGridView1.TabIndex = 60;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(820, 23);
            this.label5.TabIndex = 59;
            this.label5.Text = "Ledger Book";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(713, 506);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 58;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tabLedgerBook
            // 
            this.tabLedgerBook.ActiveColor = System.Drawing.SystemColors.Control;
            this.tabLedgerBook.BackColor = System.Drawing.SystemColors.Control;
            this.tabLedgerBook.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tabLedgerBook.Controls.Add(this.tabPage1);
            this.tabLedgerBook.Controls.Add(this.tabPage3);
            this.tabLedgerBook.Controls.Add(this.tabPage2);
            this.tabLedgerBook.ImageIndex = -1;
            this.tabLedgerBook.ImageList = null;
            this.tabLedgerBook.InactiveColor = System.Drawing.SystemColors.Window;
            this.tabLedgerBook.Location = new System.Drawing.Point(14, 70);
            this.tabLedgerBook.Name = "tabLedgerBook";
            this.tabLedgerBook.ScrollButtonStyle = OMControls.OMScrollButtonStyle.Always;
            this.tabLedgerBook.SelectedIndex = 2;
            this.tabLedgerBook.SelectedTab = this.tabPage2;
            this.tabLedgerBook.Size = new System.Drawing.Size(828, 567);
            this.tabLedgerBook.TabDock = System.Windows.Forms.DockStyle.Top;
            this.tabLedgerBook.TabDrawer = null;
            this.tabLedgerBook.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabLedgerBook.TabIndex = 48;
            this.tabLedgerBook.TabChanged += new System.EventHandler(this.tabLedgerBook_TabChanged);
            // 
            // plnLedger
            // 
            this.plnLedger.Controls.Add(this.dgLedger);
            this.plnLedger.Controls.Add(this.chkSelectAll);
            this.plnLedger.Location = new System.Drawing.Point(14, 67);
            this.plnLedger.Name = "plnLedger";
            this.plnLedger.Size = new System.Drawing.Size(520, 565);
            this.plnLedger.TabIndex = 3;
            this.plnLedger.Visible = false;
            // 
            // dgLedger
            // 
            this.dgLedger.AllowUserToAddRows = false;
            this.dgLedger.AllowUserToDeleteRows = false;
            this.dgLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLedger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.LedgerNum,
            this.SortLedgerName,
            this.Check});
            this.dgLedger.Location = new System.Drawing.Point(11, 14);
            this.dgLedger.Name = "dgLedger";
            this.dgLedger.Size = new System.Drawing.Size(495, 506);
            this.dgLedger.TabIndex = 3;
            this.dgLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgLedger_KeyDown);
            // 
            // SrNo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.SrNo.HeaderText = "SrNO";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // LedgerNum
            // 
            this.LedgerNum.HeaderText = "LedgerNum";
            this.LedgerNum.Name = "LedgerNum";
            this.LedgerNum.Visible = false;
            // 
            // SortLedgerName
            // 
            this.SortLedgerName.HeaderText = "Ledger Name";
            this.SortLedgerName.Name = "SortLedgerName";
            this.SortLedgerName.ReadOnly = true;
            this.SortLedgerName.Width = 350;
            // 
            // Check
            // 
            this.Check.HeaderText = "Select";
            this.Check.Name = "Check";
            this.Check.Width = 75;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(416, 531);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "Select All(F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnSLedger
            // 
            this.btnSLedger.Location = new System.Drawing.Point(761, 6);
            this.btnSLedger.Name = "btnSLedger";
            this.btnSLedger.Size = new System.Drawing.Size(100, 27);
            this.btnSLedger.TabIndex = 1;
            this.btnSLedger.Text = "Sho&w Report (F4)";
            this.btnSLedger.UseVisualStyleBackColor = true;
            this.btnSLedger.Click += new System.EventHandler(this.btnSLedger_Click);
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainForm.Controls.Add(this.btnSLedger);
            this.pnlMainForm.Controls.Add(this.tabLedgerBook);
            this.pnlMainForm.Controls.Add(this.panel1);
            this.pnlMainForm.Controls.Add(this.label3);
            this.pnlMainForm.Controls.Add(this.rbOther);
            this.pnlMainForm.Controls.Add(this.rbSupplier);
            this.pnlMainForm.Controls.Add(this.rbCustomer);
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Controls.Add(this.btnExport);
            this.pnlMainForm.Controls.Add(this.btnPrint);
            this.pnlMainForm.Controls.Add(this.BtnShow);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.plnLedger);
            this.pnlMainForm.Location = new System.Drawing.Point(3, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(1022, 673);
            this.pnlMainForm.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbMultibillPrint);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rbBillwise);
            this.panel1.Controls.Add(this.rbDetailed);
            this.panel1.Controls.Add(this.rbMonthwise);
            this.panel1.Controls.Add(this.rbSummary);
            this.panel1.Location = new System.Drawing.Point(378, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 30);
            this.panel1.TabIndex = 82;
            // 
            // rbMultibillPrint
            // 
            this.rbMultibillPrint.AutoSize = true;
            this.rbMultibillPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.rbMultibillPrint.Location = new System.Drawing.Point(439, 6);
            this.rbMultibillPrint.Name = "rbMultibillPrint";
            this.rbMultibillPrint.Size = new System.Drawing.Size(81, 17);
            this.rbMultibillPrint.TabIndex = 83;
            this.rbMultibillPrint.TabStop = true;
            this.rbMultibillPrint.Text = "MultiBillPrint";
            this.rbMultibillPrint.UseVisualStyleBackColor = true;
            this.rbMultibillPrint.CheckedChanged += new System.EventHandler(this.RdMultibillPrint_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 83;
            this.label4.Text = "Type :";
            // 
            // rbBillwise
            // 
            this.rbBillwise.AutoSize = true;
            this.rbBillwise.Location = new System.Drawing.Point(363, 6);
            this.rbBillwise.Name = "rbBillwise";
            this.rbBillwise.Size = new System.Drawing.Size(59, 17);
            this.rbBillwise.TabIndex = 80;
            this.rbBillwise.TabStop = true;
            this.rbBillwise.Text = "Billwise";
            this.rbBillwise.UseVisualStyleBackColor = true;
            // 
            // rbDetailed
            // 
            this.rbDetailed.AutoSize = true;
            this.rbDetailed.Location = new System.Drawing.Point(275, 6);
            this.rbDetailed.Name = "rbDetailed";
            this.rbDetailed.Size = new System.Drawing.Size(64, 17);
            this.rbDetailed.TabIndex = 79;
            this.rbDetailed.TabStop = true;
            this.rbDetailed.Text = "Detailed";
            this.rbDetailed.UseVisualStyleBackColor = true;
            // 
            // rbMonthwise
            // 
            this.rbMonthwise.AutoSize = true;
            this.rbMonthwise.Location = new System.Drawing.Point(175, 6);
            this.rbMonthwise.Name = "rbMonthwise";
            this.rbMonthwise.Size = new System.Drawing.Size(76, 17);
            this.rbMonthwise.TabIndex = 78;
            this.rbMonthwise.TabStop = true;
            this.rbMonthwise.Text = "Monthwise";
            this.rbMonthwise.UseVisualStyleBackColor = true;
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Location = new System.Drawing.Point(83, 6);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(68, 17);
            this.rbSummary.TabIndex = 77;
            this.rbSummary.TabStop = true;
            this.rbSummary.Text = "Summary";
            this.rbSummary.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 653);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Print Ctrl + P on Row";
            this.label3.Visible = false;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOther.Location = new System.Drawing.Point(240, 39);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(63, 20);
            this.rbOther.TabIndex = 77;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // rbSupplier
            // 
            this.rbSupplier.AutoSize = true;
            this.rbSupplier.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSupplier.Location = new System.Drawing.Point(133, 39);
            this.rbSupplier.Name = "rbSupplier";
            this.rbSupplier.Size = new System.Drawing.Size(78, 20);
            this.rbSupplier.TabIndex = 76;
            this.rbSupplier.TabStop = true;
            this.rbSupplier.Text = "Supplier";
            this.rbSupplier.UseVisualStyleBackColor = true;
            this.rbSupplier.CheckedChanged += new System.EventHandler(this.rbSupplier_CheckedChanged);
            // 
            // rbCustomer
            // 
            this.rbCustomer.AutoSize = true;
            this.rbCustomer.Checked = true;
            this.rbCustomer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCustomer.Location = new System.Drawing.Point(12, 40);
            this.rbCustomer.Name = "rbCustomer";
            this.rbCustomer.Size = new System.Drawing.Size(88, 20);
            this.rbCustomer.TabIndex = 75;
            this.rbCustomer.TabStop = true;
            this.rbCustomer.Text = "Customer";
            this.rbCustomer.UseVisualStyleBackColor = true;
            this.rbCustomer.CheckedChanged += new System.EventHandler(this.rbCustomer_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(528, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(71, 27);
            this.btnExit.TabIndex = 61;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(684, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 27);
            this.btnExport.TabIndex = 74;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(605, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(71, 27);
            this.btnPrint.TabIndex = 73;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(454, 6);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(71, 27);
            this.BtnShow.TabIndex = 2;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // LedgerBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1113, 699);
            this.Controls.Add(this.pnlMainForm);
            this.Name = "LedgerBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ledger Book";
            this.Load += new System.EventHandler(this.LedgerBook_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.tabLedgerBook.ResumeLayout(false);
            this.plnLedger.ResumeLayout(false);
            this.plnLedger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLedger)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        internal System.Windows.Forms.Button btnPrint;
        private OMControls.OMTabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDatewise;
        private OMControls.OMTabPage tabPage2;
        private System.Windows.Forms.Label lblMonthDtls;
        private OMControls.OMTabPage tabPage1;
        private OMControls.OMTabControl tabLedgerBook;
        private System.Windows.Forms.Button btnCancelt2;
        private System.Windows.Forms.Button btnCancelt1;
        private System.Windows.Forms.Panel plnLedger;
        private System.Windows.Forms.DataGridView dgLedger;
        System.Windows.Forms.Button btnSLedger;
        private System.Windows.Forms.CheckBox chkSelectAll;
        System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortLedgerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        internal System.Windows.Forms.Button btnExport;
        private OMControls.OMBPanel pnlMainForm;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.DataGridView DataGridView2;
        internal System.Windows.Forms.DataGridView GridViewDaily;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbSupplier;
        private System.Windows.Forms.RadioButton rbCustomer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbBillwise;
        private System.Windows.Forms.RadioButton rbDetailed;
        private System.Windows.Forms.RadioButton rbMonthwise;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbMultibillPrint;
    }
}