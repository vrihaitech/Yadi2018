namespace Yadi.Display
{
    partial class DuplicatBillPrint
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
            this.pnlMain = new OMControls.OMBPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.pnlBillNoWise = new System.Windows.Forms.Panel();
            this.txtToNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFromNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgBill = new System.Windows.Forms.DataGridView();
            this.pnlDateWise = new System.Windows.Forms.Panel();
            this.DtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPartName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFirm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRb = new System.Windows.Forms.Panel();
            this.rdBillNoWise = new System.Windows.Forms.RadioButton();
            this.rbDateWise = new System.Windows.Forms.RadioButton();
            this.rbPartyWise = new System.Windows.Forms.RadioButton();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DiscAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MixMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MfgCompNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MfgCompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlBillNoWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).BeginInit();
            this.pnlDateWise.SuspendLayout();
            this.pnlRb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.btnShow);
            this.pnlMain.Controls.Add(this.pnlBillNoWise);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.dgBill);
            this.pnlMain.Controls.Add(this.pnlDateWise);
            this.pnlMain.Controls.Add(this.cmbFirm);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.pnlRb);
            this.pnlMain.Location = new System.Drawing.Point(11, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(592, 393);
            this.pnlMain.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(26, 170);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 1564566;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(408, 352);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 1564565;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(500, 70);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(82, 33);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // pnlBillNoWise
            // 
            this.pnlBillNoWise.Controls.Add(this.txtToNo);
            this.pnlBillNoWise.Controls.Add(this.label6);
            this.pnlBillNoWise.Controls.Add(this.txtFromNo);
            this.pnlBillNoWise.Controls.Add(this.label5);
            this.pnlBillNoWise.Location = new System.Drawing.Point(88, 224);
            this.pnlBillNoWise.Name = "pnlBillNoWise";
            this.pnlBillNoWise.Size = new System.Drawing.Size(360, 37);
            this.pnlBillNoWise.TabIndex = 4544;
            this.pnlBillNoWise.Visible = false;
            // 
            // txtToNo
            // 
            this.txtToNo.Location = new System.Drawing.Point(247, 9);
            this.txtToNo.Name = "txtToNo";
            this.txtToNo.Size = new System.Drawing.Size(100, 20);
            this.txtToNo.TabIndex = 5;
            this.txtToNo.TextChanged += new System.EventHandler(this.txtSetMask_TextChanged);
            this.txtToNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToNo_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 7545646;
            this.label6.Text = "To :";
            // 
            // txtFromNo
            // 
            this.txtFromNo.Location = new System.Drawing.Point(93, 9);
            this.txtFromNo.Name = "txtFromNo";
            this.txtFromNo.Size = new System.Drawing.Size(100, 20);
            this.txtFromNo.TabIndex = 4;
            this.txtFromNo.TextChanged += new System.EventHandler(this.txtSetMask_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 55454545;
            this.label5.Text = "Bill No From :";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(119, 352);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 33);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(24, 352);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 33);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgBill
            // 
            this.dgBill.AllowUserToAddRows = false;
            this.dgBill.AllowUserToDeleteRows = false;
            this.dgBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.Date,
            this.BillNo,
            this.PartyName,
            this.Amount,
            this.PkVoucherNo,
            this.Select,
            this.DiscAmt,
            this.OrderType,
            this.MixMode,
            this.PayTypeName,
            this.MfgCompNo,
            this.MfgCompName});
            this.dgBill.Location = new System.Drawing.Point(24, 141);
            this.dgBill.Name = "dgBill";
            this.dgBill.Size = new System.Drawing.Size(486, 206);
            this.dgBill.TabIndex = 7;
            this.dgBill.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBill_CellFormatting);
            this.dgBill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBill_KeyDown);
            // 
            // pnlDateWise
            // 
            this.pnlDateWise.Controls.Add(this.DtpToDate);
            this.pnlDateWise.Controls.Add(this.label4);
            this.pnlDateWise.Controls.Add(this.DtpFromDate);
            this.pnlDateWise.Controls.Add(this.label3);
            this.pnlDateWise.Controls.Add(this.cmbPartName);
            this.pnlDateWise.Controls.Add(this.label2);
            this.pnlDateWise.Location = new System.Drawing.Point(18, 70);
            this.pnlDateWise.Name = "pnlDateWise";
            this.pnlDateWise.Size = new System.Drawing.Size(478, 65);
            this.pnlDateWise.TabIndex = 15151;
            this.pnlDateWise.Visible = false;
            // 
            // DtpToDate
            // 
            this.DtpToDate.Location = new System.Drawing.Point(319, 6);
            this.DtpToDate.Name = "DtpToDate";
            this.DtpToDate.Size = new System.Drawing.Size(138, 20);
            this.DtpToDate.TabIndex = 2;
            this.DtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpToDate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 765464;
            this.label4.Text = "To Date :";
            // 
            // DtpFromDate
            // 
            this.DtpFromDate.Location = new System.Drawing.Point(93, 6);
            this.DtpFromDate.Name = "DtpFromDate";
            this.DtpFromDate.Size = new System.Drawing.Size(120, 20);
            this.DtpFromDate.TabIndex = 1;
            this.DtpFromDate.ValueChanged += new System.EventHandler(this.DtpFromDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5145454;
            this.label3.Text = "From Date :";
            // 
            // cmbPartName
            // 
            this.cmbPartName.FormattingEnabled = true;
            this.cmbPartName.Location = new System.Drawing.Point(93, 32);
            this.cmbPartName.Name = "cmbPartName";
            this.cmbPartName.Size = new System.Drawing.Size(364, 21);
            this.cmbPartName.TabIndex = 3;
            this.cmbPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPartName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 45555;
            this.label2.Text = "Party Name :";
            // 
            // cmbFirm
            // 
            this.cmbFirm.FormattingEnabled = true;
            this.cmbFirm.Location = new System.Drawing.Point(110, 44);
            this.cmbFirm.Name = "cmbFirm";
            this.cmbFirm.Size = new System.Drawing.Size(364, 21);
            this.cmbFirm.TabIndex = 0;
            this.cmbFirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFirm_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1564564;
            this.label1.Text = "Firm :";
            // 
            // pnlRb
            // 
            this.pnlRb.Controls.Add(this.rdBillNoWise);
            this.pnlRb.Controls.Add(this.rbDateWise);
            this.pnlRb.Controls.Add(this.rbPartyWise);
            this.pnlRb.Location = new System.Drawing.Point(18, 9);
            this.pnlRb.Name = "pnlRb";
            this.pnlRb.Size = new System.Drawing.Size(478, 31);
            this.pnlRb.TabIndex = 100;
            // 
            // rdBillNoWise
            // 
            this.rdBillNoWise.AutoSize = true;
            this.rdBillNoWise.Location = new System.Drawing.Point(223, 7);
            this.rdBillNoWise.Name = "rdBillNoWise";
            this.rdBillNoWise.Size = new System.Drawing.Size(82, 17);
            this.rdBillNoWise.TabIndex = 100;
            this.rdBillNoWise.Text = "Bill No Wise";
            this.rdBillNoWise.UseVisualStyleBackColor = true;
            this.rdBillNoWise.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbDateWise
            // 
            this.rbDateWise.AutoSize = true;
            this.rbDateWise.Location = new System.Drawing.Point(114, 7);
            this.rbDateWise.Name = "rbDateWise";
            this.rbDateWise.Size = new System.Drawing.Size(75, 17);
            this.rbDateWise.TabIndex = 100;
            this.rbDateWise.Text = "Date Wise";
            this.rbDateWise.UseVisualStyleBackColor = true;
            this.rbDateWise.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbPartyWise
            // 
            this.rbPartyWise.AutoSize = true;
            this.rbPartyWise.Checked = true;
            this.rbPartyWise.Location = new System.Drawing.Point(6, 7);
            this.rbPartyWise.Name = "rbPartyWise";
            this.rbPartyWise.Size = new System.Drawing.Size(76, 17);
            this.rbPartyWise.TabIndex = 100;
            this.rbPartyWise.TabStop = true;
            this.rbPartyWise.Text = "Party Wise";
            this.rbPartyWise.UseVisualStyleBackColor = true;
            this.rbPartyWise.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 85;
            // 
            // BillNo
            // 
            this.BillNo.HeaderText = "Bill No";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            this.BillNo.Width = 70;
            // 
            // PartyName
            // 
            this.PartyName.HeaderText = "Party Name";
            this.PartyName.Name = "PartyName";
            this.PartyName.ReadOnly = true;
            this.PartyName.Visible = false;
            this.PartyName.Width = 130;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 80;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.ReadOnly = true;
            this.PkVoucherNo.Visible = false;
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.Width = 50;
            // 
            // DiscAmt
            // 
            this.DiscAmt.HeaderText = "DiscAmt";
            this.DiscAmt.Name = "DiscAmt";
            this.DiscAmt.Visible = false;
            // 
            // OrderType
            // 
            this.OrderType.HeaderText = "OrderType";
            this.OrderType.Name = "OrderType";
            this.OrderType.Visible = false;
            // 
            // MixMode
            // 
            this.MixMode.HeaderText = "MixMode";
            this.MixMode.Name = "MixMode";
            this.MixMode.Visible = false;
            // 
            // PayTypeName
            // 
            this.PayTypeName.HeaderText = "PayTypeName";
            this.PayTypeName.Name = "PayTypeName";
            this.PayTypeName.Visible = false;
            // 
            // MfgCompNo
            // 
            this.MfgCompNo.HeaderText = "MfgCompNo";
            this.MfgCompNo.Name = "MfgCompNo";
            this.MfgCompNo.Visible = false;
            // 
            // MfgCompName
            // 
            this.MfgCompName.HeaderText = "MfgCompName";
            this.MfgCompName.Name = "MfgCompName";
            this.MfgCompName.Visible = false;
            // 
            // DuplicatBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 402);
            this.Controls.Add(this.pnlMain);
            this.Name = "DuplicatBillPrint";
            this.Text = "Duplicat Bill Printing";
            this.Load += new System.EventHandler(this.DuplicatBillPrint_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlBillNoWise.ResumeLayout(false);
            this.pnlBillNoWise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).EndInit();
            this.pnlDateWise.ResumeLayout(false);
            this.pnlDateWise.PerformLayout();
            this.pnlRb.ResumeLayout(false);
            this.pnlRb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel pnlRb;
        private System.Windows.Forms.RadioButton rdBillNoWise;
        private System.Windows.Forms.RadioButton rbDateWise;
        private System.Windows.Forms.RadioButton rbPartyWise;
        private System.Windows.Forms.Panel pnlDateWise;
        private System.Windows.Forms.ComboBox cmbPartName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBillNoWise;
        private System.Windows.Forms.DateTimePicker DtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtToNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFromNo;
        private System.Windows.Forms.DataGridView dgBill;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private new System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MixMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MfgCompNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MfgCompName;
    }
}