namespace Yadi.Utilities
{
    partial class DeleteTransaction
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
            this.pnlSearch = new OMControls.OMBPanel();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.gvPayType = new System.Windows.Forms.DataGridView();
            this.PkPayTypeNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayTypeName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkSelectAllPayType = new System.Windows.Forms.CheckBox();
            this.chkSelectAllParty = new System.Windows.Forms.CheckBox();
            this.BtnShowParty = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgParty = new System.Windows.Forms.DataGridView();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShowPaytype = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPToDate = new System.Windows.Forms.DateTimePicker();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pnlRecords = new OMControls.OMBPanel();
            this.btnBack2 = new System.Windows.Forms.Button();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgBillDetails = new System.Windows.Forms.DataGridView();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherUserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BilledAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutStand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PayTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDate = new OMControls.OMBPanel();
            this.BtnShowDate = new System.Windows.Forms.Button();
            this.BtnExit1 = new System.Windows.Forms.Button();
            this.btnNext1 = new System.Windows.Forms.Button();
            this.lblFirmName = new System.Windows.Forms.Label();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParty)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillDetails)).BeginInit();
            this.pnlDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderColor = System.Drawing.Color.Gray;
            this.pnlSearch.BorderRadius = 3;
            this.pnlSearch.Controls.Add(this.btnBack1);
            this.pnlSearch.Controls.Add(this.btnNext2);
            this.pnlSearch.Controls.Add(this.gvPayType);
            this.pnlSearch.Controls.Add(this.chkSelectAllPayType);
            this.pnlSearch.Controls.Add(this.chkSelectAllParty);
            this.pnlSearch.Controls.Add(this.BtnShowParty);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Controls.Add(this.dgParty);
            this.pnlSearch.Controls.Add(this.BtnShowPaytype);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Location = new System.Drawing.Point(3, 23);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(415, 568);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnBack1
            // 
            this.btnBack1.Location = new System.Drawing.Point(7, 537);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(129, 27);
            this.btnBack1.TabIndex = 5;
            this.btnBack1.Text = "Back";
            this.btnBack1.UseVisualStyleBackColor = false;
            this.btnBack1.Click += new System.EventHandler(this.btnBack1_Click);
            // 
            // btnNext2
            // 
            this.btnNext2.Location = new System.Drawing.Point(7, 222);
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.Size = new System.Drawing.Size(129, 27);
            this.btnNext2.TabIndex = 1;
            this.btnNext2.Text = "Fill Party List";
            this.btnNext2.UseVisualStyleBackColor = false;
            this.btnNext2.Click += new System.EventHandler(this.btnNext2_Click);
            // 
            // gvPayType
            // 
            this.gvPayType.AllowUserToAddRows = false;
            this.gvPayType.AllowUserToDeleteRows = false;
            this.gvPayType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPayType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PkPayTypeNo1,
            this.PayTypeName1,
            this.Select11});
            this.gvPayType.Location = new System.Drawing.Point(7, 27);
            this.gvPayType.Name = "gvPayType";
            this.gvPayType.Size = new System.Drawing.Size(402, 194);
            this.gvPayType.TabIndex = 0;
            // 
            // PkPayTypeNo1
            // 
            this.PkPayTypeNo1.DataPropertyName = "PkPayTypeNo";
            this.PkPayTypeNo1.HeaderText = "PkPayTypeNo";
            this.PkPayTypeNo1.Name = "PkPayTypeNo1";
            this.PkPayTypeNo1.Visible = false;
            // 
            // PayTypeName1
            // 
            this.PayTypeName1.DataPropertyName = "PayTypeName";
            this.PayTypeName1.HeaderText = "Name";
            this.PayTypeName1.Name = "PayTypeName1";
            this.PayTypeName1.Width = 300;
            // 
            // Select11
            // 
            this.Select11.DataPropertyName = "Chk";
            this.Select11.HeaderText = "";
            this.Select11.Name = "Select11";
            this.Select11.Width = 50;
            // 
            // chkSelectAllPayType
            // 
            this.chkSelectAllPayType.AutoSize = true;
            this.chkSelectAllPayType.Location = new System.Drawing.Point(324, 228);
            this.chkSelectAllPayType.Name = "chkSelectAllPayType";
            this.chkSelectAllPayType.Size = new System.Drawing.Size(85, 17);
            this.chkSelectAllPayType.TabIndex = 3;
            this.chkSelectAllPayType.Text = "SelectAll(F2)";
            this.chkSelectAllPayType.UseVisualStyleBackColor = true;
            this.chkSelectAllPayType.CheckedChanged += new System.EventHandler(this.chkSelectAllPayType_CheckedChanged_1);
            // 
            // chkSelectAllParty
            // 
            this.chkSelectAllParty.AutoSize = true;
            this.chkSelectAllParty.Location = new System.Drawing.Point(321, 543);
            this.chkSelectAllParty.Name = "chkSelectAllParty";
            this.chkSelectAllParty.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAllParty.TabIndex = 7;
            this.chkSelectAllParty.Text = "SelectAll (F3)";
            this.chkSelectAllParty.UseVisualStyleBackColor = true;
            this.chkSelectAllParty.CheckedChanged += new System.EventHandler(this.chkSelectAllParty_CheckedChanged);
            // 
            // BtnShowParty
            // 
            this.BtnShowParty.Location = new System.Drawing.Point(159, 537);
            this.BtnShowParty.Name = "BtnShowParty";
            this.BtnShowParty.Size = new System.Drawing.Size(129, 27);
            this.BtnShowParty.TabIndex = 6;
            this.BtnShowParty.Text = "Show Record(s)";
            this.BtnShowParty.UseVisualStyleBackColor = false;
            this.BtnShowParty.Click += new System.EventHandler(this.BtnShowParty_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(0, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(415, 23);
            this.label5.TabIndex = 514;
            this.label5.Text = "Party Name";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgParty
            // 
            this.dgParty.AllowUserToAddRows = false;
            this.dgParty.AllowUserToDeleteRows = false;
            this.dgParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerNo,
            this.PartyName,
            this.Select1});
            this.dgParty.Location = new System.Drawing.Point(7, 288);
            this.dgParty.Name = "dgParty";
            this.dgParty.Size = new System.Drawing.Size(402, 243);
            this.dgParty.TabIndex = 4;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Visible = false;
            // 
            // PartyName
            // 
            this.PartyName.DataPropertyName = "LedgerName";
            this.PartyName.HeaderText = "Name";
            this.PartyName.Name = "PartyName";
            this.PartyName.Width = 320;
            // 
            // Select1
            // 
            this.Select1.DataPropertyName = "Chk";
            this.Select1.HeaderText = "";
            this.Select1.Name = "Select1";
            this.Select1.Width = 50;
            // 
            // BtnShowPaytype
            // 
            this.BtnShowPaytype.Location = new System.Drawing.Point(159, 222);
            this.BtnShowPaytype.Name = "BtnShowPaytype";
            this.BtnShowPaytype.Size = new System.Drawing.Size(129, 27);
            this.BtnShowPaytype.TabIndex = 2;
            this.BtnShowPaytype.Text = "Show Record(s)";
            this.BtnShowPaytype.UseVisualStyleBackColor = false;
            this.BtnShowPaytype.Click += new System.EventHandler(this.BtnShowPaytype_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(415, 23);
            this.label4.TabIndex = 511;
            this.label4.Text = "Payment Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-146, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 71;
            this.label1.Text = "From Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "From";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(52, 7);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(126, 23);
            this.DTPFromDate.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(180, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 75;
            this.label3.Text = "To";
            // 
            // DTPToDate
            // 
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(206, 7);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(126, 23);
            this.DTPToDate.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblFirmName);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.pnlRecords);
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Controls.Add(this.pnlDate);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1162, 596);
            this.pnlMain.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(556, 311);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 518;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // pnlRecords
            // 
            this.pnlRecords.BorderColor = System.Drawing.Color.Gray;
            this.pnlRecords.BorderRadius = 3;
            this.pnlRecords.Controls.Add(this.btnBack2);
            this.pnlRecords.Controls.Add(this.chkSelect);
            this.pnlRecords.Controls.Add(this.BtnExit);
            this.pnlRecords.Controls.Add(this.btnDelete);
            this.pnlRecords.Controls.Add(this.dgBillDetails);
            this.pnlRecords.Location = new System.Drawing.Point(420, 60);
            this.pnlRecords.Name = "pnlRecords";
            this.pnlRecords.Size = new System.Drawing.Size(736, 531);
            this.pnlRecords.TabIndex = 2;
            // 
            // btnBack2
            // 
            this.btnBack2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack2.Location = new System.Drawing.Point(141, 500);
            this.btnBack2.Name = "btnBack2";
            this.btnBack2.Size = new System.Drawing.Size(129, 27);
            this.btnBack2.TabIndex = 2;
            this.btnBack2.Text = "Back";
            this.btnBack2.UseVisualStyleBackColor = false;
            this.btnBack2.Click += new System.EventHandler(this.btnBack2_Click);
            // 
            // chkSelect
            // 
            this.chkSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(643, 506);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(88, 17);
            this.chkSelect.TabIndex = 4;
            this.chkSelect.Text = "SelectAll (F4)";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnExit.Location = new System.Drawing.Point(276, 500);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(129, 27);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(6, 500);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(129, 27);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgBillDetails
            // 
            this.dgBillDetails.AllowUserToAddRows = false;
            this.dgBillDetails.AllowUserToDeleteRows = false;
            this.dgBillDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBillDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VoucherDate,
            this.VoucherUserNo,
            this.LedgerName,
            this.BilledAmount,
            this.PayType,
            this.OutStand,
            this.PkVoucherNo,
            this.PLedgerNo,
            this.Select,
            this.PayTypeNo});
            this.dgBillDetails.Location = new System.Drawing.Point(7, 6);
            this.dgBillDetails.Name = "dgBillDetails";
            this.dgBillDetails.Size = new System.Drawing.Size(724, 488);
            this.dgBillDetails.TabIndex = 0;
            this.dgBillDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBillDetails_CellFormatting);
            // 
            // VoucherDate
            // 
            this.VoucherDate.DataPropertyName = "VoucherDate";
            this.VoucherDate.HeaderText = "Date";
            this.VoucherDate.Name = "VoucherDate";
            this.VoucherDate.ReadOnly = true;
            this.VoucherDate.Width = 80;
            // 
            // VoucherUserNo
            // 
            this.VoucherUserNo.DataPropertyName = "VoucherUserNo";
            this.VoucherUserNo.FillWeight = 50F;
            this.VoucherUserNo.HeaderText = "BillNo";
            this.VoucherUserNo.Name = "VoucherUserNo";
            this.VoucherUserNo.ReadOnly = true;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "PartyName";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Width = 250;
            // 
            // BilledAmount
            // 
            this.BilledAmount.DataPropertyName = "BilledAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BilledAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.BilledAmount.HeaderText = "Amount";
            this.BilledAmount.Name = "BilledAmount";
            this.BilledAmount.ReadOnly = true;
            // 
            // PayType
            // 
            this.PayType.DataPropertyName = "PayTypeName";
            this.PayType.HeaderText = "PayType";
            this.PayType.Name = "PayType";
            this.PayType.ReadOnly = true;
            this.PayType.Width = 80;
            // 
            // OutStand
            // 
            this.OutStand.DataPropertyName = "OutStand";
            this.OutStand.HeaderText = "OutStand";
            this.OutStand.Name = "OutStand";
            this.OutStand.ReadOnly = true;
            this.OutStand.Visible = false;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.DataPropertyName = "PkVoucherNo";
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.Visible = false;
            // 
            // PLedgerNo
            // 
            this.PLedgerNo.DataPropertyName = "LedgerNo";
            this.PLedgerNo.HeaderText = "PLedgerNo";
            this.PLedgerNo.Name = "PLedgerNo";
            this.PLedgerNo.Visible = false;
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 50;
            // 
            // PayTypeNo
            // 
            this.PayTypeNo.DataPropertyName = "PayTypeNo";
            this.PayTypeNo.HeaderText = "PayTypeNo";
            this.PayTypeNo.Name = "PayTypeNo";
            this.PayTypeNo.ReadOnly = true;
            this.PayTypeNo.Visible = false;
            // 
            // pnlDate
            // 
            this.pnlDate.BorderColor = System.Drawing.Color.Gray;
            this.pnlDate.BorderRadius = 3;
            this.pnlDate.Controls.Add(this.BtnShowDate);
            this.pnlDate.Controls.Add(this.BtnExit1);
            this.pnlDate.Controls.Add(this.btnNext1);
            this.pnlDate.Controls.Add(this.DTPFromDate);
            this.pnlDate.Controls.Add(this.label3);
            this.pnlDate.Controls.Add(this.label2);
            this.pnlDate.Controls.Add(this.DTPToDate);
            this.pnlDate.Location = new System.Drawing.Point(420, 23);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(736, 39);
            this.pnlDate.TabIndex = 0;
            // 
            // BtnShowDate
            // 
            this.BtnShowDate.Location = new System.Drawing.Point(469, 5);
            this.BtnShowDate.Name = "BtnShowDate";
            this.BtnShowDate.Size = new System.Drawing.Size(129, 27);
            this.BtnShowDate.TabIndex = 3;
            this.BtnShowDate.Text = "Show Record(s)";
            this.BtnShowDate.UseVisualStyleBackColor = false;
            this.BtnShowDate.Click += new System.EventHandler(this.BtnShowDate_Click);
            // 
            // BtnExit1
            // 
            this.BtnExit1.Location = new System.Drawing.Point(602, 5);
            this.BtnExit1.Name = "BtnExit1";
            this.BtnExit1.Size = new System.Drawing.Size(129, 27);
            this.BtnExit1.TabIndex = 4;
            this.BtnExit1.Text = "Exit";
            this.BtnExit1.UseVisualStyleBackColor = false;
            this.BtnExit1.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnNext1
            // 
            this.btnNext1.Location = new System.Drawing.Point(336, 5);
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.Size = new System.Drawing.Size(129, 27);
            this.btnNext1.TabIndex = 2;
            this.btnNext1.Text = "Next";
            this.btnNext1.UseVisualStyleBackColor = false;
            this.btnNext1.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // lblFirmName
            // 
            this.lblFirmName.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.lblFirmName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFirmName.ForeColor = System.Drawing.Color.White;
            this.lblFirmName.Location = new System.Drawing.Point(0, 0);
            this.lblFirmName.Name = "lblFirmName";
            this.lblFirmName.Size = new System.Drawing.Size(1162, 18);
            this.lblFirmName.TabIndex = 5574;
            this.lblFirmName.Text = "label16";
            this.lblFirmName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeleteTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 604);
            this.Controls.Add(this.pnlMain);
            this.Name = "DeleteTransaction";
            this.Text = "DeleteTransaction";
            this.Load += new System.EventHandler(this.DeleteTransaction_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParty)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlRecords.ResumeLayout(false);
            this.pnlRecords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillDetails)).EndInit();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTPToDate;
        private OMControls.OMBPanel pnlMain;
        private OMControls.OMBPanel pnlRecords;
        private OMControls.OMBPanel pnlDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgBillDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgParty;
        internal System.Windows.Forms.Button BtnShowPaytype;
        internal System.Windows.Forms.Button BtnShowParty;
        internal System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.CheckBox chkSelectAllPayType;
        private System.Windows.Forms.CheckBox chkSelectAllParty;
        private System.Windows.Forms.DataGridView gvPayType;
        internal System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkSelect;
        internal System.Windows.Forms.Button btnNext1;
        internal System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkPayTypeNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeName1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select11;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select1;
        internal System.Windows.Forms.Button btnBack1;
        internal System.Windows.Forms.Button btnBack2;
        internal System.Windows.Forms.Button BtnExit1;
        internal System.Windows.Forms.Button BtnShowDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherUserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BilledAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutStand;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLedgerNo;
        private new System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeNo;
        private System.Windows.Forms.Label lblFirmName;
    }
}