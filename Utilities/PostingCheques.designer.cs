namespace Yadi.Utilities
{
    partial class PostingCheques
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.gvChqDtls = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Party = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostBank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlBank = new System.Windows.Forms.Panel();
            this.lstBank = new System.Windows.Forms.ListBox();
            this.pnlCompany = new System.Windows.Forms.Panel();
            this.lstCompany = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.rdPending = new System.Windows.Forms.RadioButton();
            this.rdPosted = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvChqDtls)).BeginInit();
            this.pnlBank.SuspendLayout();
            this.pnlCompany.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(85, 12);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(95, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(308, 12);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(106, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(697, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // gvChqDtls
            // 
            this.gvChqDtls.AllowUserToAddRows = false;
            this.gvChqDtls.AllowUserToDeleteRows = false;
            this.gvChqDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvChqDtls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.Party,
            this.ChequeNo,
            this.ChequeDate,
            this.Amount,
            this.Bank,
            this.Branch,
            this.PostBank,
            this.Company,
            this.Pk,
            this.BankCode,
            this.CompanyCode,
            this.LedgNo});
            this.gvChqDtls.Location = new System.Drawing.Point(0, 46);
            this.gvChqDtls.Name = "gvChqDtls";
            this.gvChqDtls.Size = new System.Drawing.Size(782, 147);
            this.gvChqDtls.TabIndex = 5;
            this.gvChqDtls.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvChqDtls_CellFormatting);
            this.gvChqDtls.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvChqDtls_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            this.SrNo.HeaderText = "Sr";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 30;
            // 
            // Party
            // 
            this.Party.DataPropertyName = "LedgerName";
            this.Party.HeaderText = "Party";
            this.Party.Name = "Party";
            this.Party.ReadOnly = true;
            this.Party.Width = 168;
            // 
            // ChequeNo
            // 
            this.ChequeNo.DataPropertyName = "ChequeNo";
            this.ChequeNo.HeaderText = "ChqNo";
            this.ChequeNo.Name = "ChequeNo";
            this.ChequeNo.ReadOnly = true;
            this.ChequeNo.Width = 80;
            // 
            // ChequeDate
            // 
            this.ChequeDate.DataPropertyName = "ChequeDate";
            this.ChequeDate.HeaderText = "ChqDate";
            this.ChequeDate.Name = "ChequeDate";
            this.ChequeDate.ReadOnly = true;
            this.ChequeDate.Width = 70;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 70;
            // 
            // Bank
            // 
            this.Bank.DataPropertyName = "BankName";
            this.Bank.HeaderText = "Bank";
            this.Bank.Name = "Bank";
            this.Bank.ReadOnly = true;
            this.Bank.Width = 70;
            // 
            // Branch
            // 
            this.Branch.DataPropertyName = "BranchName";
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            this.Branch.Width = 70;
            // 
            // PostBank
            // 
            this.PostBank.DataPropertyName = "PostLedger";
            this.PostBank.HeaderText = "Post Bank";
            this.PostBank.Name = "PostBank";
            this.PostBank.ReadOnly = true;
            this.PostBank.Width = 120;
            // 
            // Company
            // 
            this.Company.DataPropertyName = "PostCompany";
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            // 
            // Pk
            // 
            this.Pk.DataPropertyName = "PkSrNo";
            this.Pk.HeaderText = "Pk";
            this.Pk.Name = "Pk";
            this.Pk.ReadOnly = true;
            this.Pk.Visible = false;
            // 
            // BankCode
            // 
            this.BankCode.DataPropertyName = "BankCode";
            this.BankCode.HeaderText = "BankCode";
            this.BankCode.Name = "BankCode";
            this.BankCode.ReadOnly = true;
            this.BankCode.Visible = false;
            // 
            // CompanyCode
            // 
            this.CompanyCode.DataPropertyName = "CompanyCode";
            this.CompanyCode.HeaderText = "CompanyCode";
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.ReadOnly = true;
            this.CompanyCode.Visible = false;
            // 
            // LedgNo
            // 
            this.LedgNo.DataPropertyName = "LedgerNo";
            this.LedgNo.HeaderText = "LedgNo";
            this.LedgNo.Name = "LedgNo";
            this.LedgNo.ReadOnly = true;
            this.LedgNo.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(0, 215);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 60);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Post Voucher";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlBank
            // 
            this.pnlBank.BackColor = System.Drawing.Color.Bisque;
            this.pnlBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBank.Controls.Add(this.lstBank);
            this.pnlBank.Location = new System.Drawing.Point(539, 82);
            this.pnlBank.Name = "pnlBank";
            this.pnlBank.Size = new System.Drawing.Size(200, 100);
            this.pnlBank.TabIndex = 7;
            this.pnlBank.Visible = false;
            // 
            // lstBank
            // 
            this.lstBank.FormattingEnabled = true;
            this.lstBank.Location = new System.Drawing.Point(10, 13);
            this.lstBank.Name = "lstBank";
            this.lstBank.Size = new System.Drawing.Size(181, 69);
            this.lstBank.TabIndex = 0;
            this.lstBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBank_KeyDown);
            // 
            // pnlCompany
            // 
            this.pnlCompany.BackColor = System.Drawing.Color.Bisque;
            this.pnlCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCompany.Controls.Add(this.lstCompany);
            this.pnlCompany.Location = new System.Drawing.Point(582, 78);
            this.pnlCompany.Name = "pnlCompany";
            this.pnlCompany.Size = new System.Drawing.Size(200, 100);
            this.pnlCompany.TabIndex = 1;
            this.pnlCompany.Visible = false;
            // 
            // lstCompany
            // 
            this.lstCompany.FormattingEnabled = true;
            this.lstCompany.Location = new System.Drawing.Point(12, 13);
            this.lstCompany.Name = "lstCompany";
            this.lstCompany.Size = new System.Drawing.Size(169, 69);
            this.lstCompany.TabIndex = 0;
            this.lstCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCompany_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(125, 215);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 60);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rdPending
            // 
            this.rdPending.AutoSize = true;
            this.rdPending.Location = new System.Drawing.Point(445, 11);
            this.rdPending.Name = "rdPending";
            this.rdPending.Size = new System.Drawing.Size(64, 17);
            this.rdPending.TabIndex = 9;
            this.rdPending.TabStop = true;
            this.rdPending.Text = "Pending";
            this.rdPending.UseVisualStyleBackColor = true;
            // 
            // rdPosted
            // 
            this.rdPosted.AutoSize = true;
            this.rdPosted.Location = new System.Drawing.Point(527, 11);
            this.rdPosted.Name = "rdPosted";
            this.rdPosted.Size = new System.Drawing.Size(58, 17);
            this.rdPosted.TabIndex = 10;
            this.rdPosted.TabStop = true;
            this.rdPosted.Text = "Posted";
            this.rdPosted.UseVisualStyleBackColor = true;
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Location = new System.Drawing.Point(602, 11);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(36, 17);
            this.rdAll.TabIndex = 11;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All";
            this.rdAll.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(209, 215);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 60);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // PostingCheques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 330);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.rdAll);
            this.Controls.Add(this.rdPosted);
            this.Controls.Add(this.rdPending);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlCompany);
            this.Controls.Add(this.pnlBank);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gvChqDtls);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label1);
            this.Name = "PostingCheques";
            this.Text = "PostingCheques";
            this.Load += new System.EventHandler(this.PostingCheques_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvChqDtls)).EndInit();
            this.pnlBank.ResumeLayout(false);
            this.pnlCompany.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.DataGridView gvChqDtls;
        private System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Panel pnlBank;
        private System.Windows.Forms.ListBox lstBank;
        private System.Windows.Forms.Panel pnlCompany;
        private System.Windows.Forms.ListBox lstCompany;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rdPending;
        private System.Windows.Forms.RadioButton rdPosted;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Party;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pk;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgNo;
        private System.Windows.Forms.Button btnPrint;
    }
}