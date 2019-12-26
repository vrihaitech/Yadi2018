namespace Yadi.Display
{
    partial class MultipleBillPrint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.plnMain1 = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.BtnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbCustDetails = new System.Windows.Forms.RadioButton();
            this.rbDetails = new System.Windows.Forms.RadioButton();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotRec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetBal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.plnMain1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plnMain1
            // 
            this.plnMain1.Controls.Add(this.chkSelectAll);
            this.plnMain1.Controls.Add(this.btnPrint);
            this.plnMain1.Controls.Add(this.GridView);
            this.plnMain1.Controls.Add(this.BtnExit);
            this.plnMain1.Controls.Add(this.panel1);
            this.plnMain1.Location = new System.Drawing.Point(3, 12);
            this.plnMain1.Name = "plnMain1";
            this.plnMain1.Size = new System.Drawing.Size(545, 347);
            this.plnMain1.TabIndex = 0;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(440, 281);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 4;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(11, 281);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 60);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.BillDate,
            this.BillAmt,
            this.TotRec,
            this.NetBal,
            this.Amount,
            this.PayType,
            this.LedgerNo,
            this.ChqNo,
            this.ChqDate,
            this.BankNo,
            this.BranchNo,
            this.PayTypeNo,
            this.RefNo,
            this.PkVoucherNo,
            this.Chk});
            this.GridView.Location = new System.Drawing.Point(11, 64);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(527, 211);
            this.GridView.TabIndex = 2;
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(97, 281);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbCustDetails);
            this.panel1.Controls.Add(this.rbDetails);
            this.panel1.Controls.Add(this.rbSummary);
            this.panel1.Location = new System.Drawing.Point(11, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 49);
            this.panel1.TabIndex = 0;
            // 
            // rbCustDetails
            // 
            this.rbCustDetails.AutoSize = true;
            this.rbCustDetails.Location = new System.Drawing.Point(275, 15);
            this.rbCustDetails.Name = "rbCustDetails";
            this.rbCustDetails.Size = new System.Drawing.Size(78, 17);
            this.rbCustDetails.TabIndex = 3;
            this.rbCustDetails.TabStop = true;
            this.rbCustDetails.Text = "Details (F5)";
            this.rbCustDetails.UseVisualStyleBackColor = true;
            this.rbCustDetails.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbDetails
            // 
            this.rbDetails.AutoSize = true;
            this.rbDetails.Location = new System.Drawing.Point(135, 16);
            this.rbDetails.Name = "rbDetails";
            this.rbDetails.Size = new System.Drawing.Size(117, 17);
            this.rbDetails.TabIndex = 2;
            this.rbDetails.TabStop = true;
            this.rbDetails.Text = "Bill Item Details (F4)";
            this.rbDetails.UseVisualStyleBackColor = true;
            this.rbDetails.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Location = new System.Drawing.Point(21, 16);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(89, 17);
            this.rbSummary.TabIndex = 1;
            this.rbSummary.TabStop = true;
            this.rbSummary.Text = "Summary (F3)";
            this.rbSummary.UseVisualStyleBackColor = true;
            this.rbSummary.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "VoucherUserNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "BillNo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // BillDate
            // 
            this.BillDate.DataPropertyName = "VoucherDate";
            this.BillDate.HeaderText = "BillDate";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
            this.BillDate.Width = 80;
            // 
            // BillAmt
            // 
            this.BillAmt.DataPropertyName = "Debit";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BillAmt.DefaultCellStyle = dataGridViewCellStyle2;
            this.BillAmt.HeaderText = "BillAmt";
            this.BillAmt.Name = "BillAmt";
            this.BillAmt.ReadOnly = true;
            this.BillAmt.Width = 95;
            // 
            // TotRec
            // 
            this.TotRec.DataPropertyName = "TotRec";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotRec.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotRec.HeaderText = "TotRec";
            this.TotRec.Name = "TotRec";
            this.TotRec.ReadOnly = true;
            this.TotRec.Width = 95;
            // 
            // NetBal
            // 
            this.NetBal.DataPropertyName = "NetBal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NetBal.DefaultCellStyle = dataGridViewCellStyle4;
            this.NetBal.HeaderText = "NetBal";
            this.NetBal.Name = "NetBal";
            this.NetBal.ReadOnly = true;
            this.NetBal.Width = 95;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle5;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Visible = false;
            this.Amount.Width = 95;
            // 
            // PayType
            // 
            this.PayType.DataPropertyName = "PayType";
            this.PayType.HeaderText = "PayType";
            this.PayType.Name = "PayType";
            this.PayType.ReadOnly = true;
            this.PayType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PayType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PayType.Visible = false;
            this.PayType.Width = 80;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.ReadOnly = true;
            this.LedgerNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LedgerNo.Visible = false;
            // 
            // ChqNo
            // 
            this.ChqNo.DataPropertyName = "ChqNo";
            this.ChqNo.HeaderText = "ChqNo";
            this.ChqNo.Name = "ChqNo";
            this.ChqNo.Visible = false;
            // 
            // ChqDate
            // 
            this.ChqDate.DataPropertyName = "ChqDate";
            this.ChqDate.HeaderText = "ChqDate";
            this.ChqDate.Name = "ChqDate";
            this.ChqDate.Visible = false;
            // 
            // BankNo
            // 
            this.BankNo.DataPropertyName = "BankNo";
            this.BankNo.HeaderText = "BankNo";
            this.BankNo.Name = "BankNo";
            this.BankNo.Visible = false;
            // 
            // BranchNo
            // 
            this.BranchNo.DataPropertyName = "BranchNo";
            this.BranchNo.HeaderText = "BranchNo";
            this.BranchNo.Name = "BranchNo";
            this.BranchNo.Visible = false;
            // 
            // PayTypeNo
            // 
            this.PayTypeNo.DataPropertyName = "PayTypeNo";
            this.PayTypeNo.HeaderText = "PayTypeNo";
            this.PayTypeNo.Name = "PayTypeNo";
            this.PayTypeNo.Visible = false;
            // 
            // RefNo
            // 
            this.RefNo.DataPropertyName = "RefNo";
            this.RefNo.HeaderText = "RefNo";
            this.RefNo.Name = "RefNo";
            this.RefNo.Visible = false;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.Visible = false;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Width = 60;
            // 
            // MultipleBillPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 367);
            this.Controls.Add(this.plnMain1);
            this.Name = "MultipleBillPrint";
            this.Text = "Multiple Bill Print";
            this.Load += new System.EventHandler(this.MultipleBillPrint_Load);
            this.plnMain1.ResumeLayout(false);
            this.plnMain1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel  plnMain1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbDetails;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.RadioButton rbCustDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotRec;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetBal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
    }
}