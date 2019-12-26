namespace Yadi.Utilities
{
    partial class LedgerOpeningBalance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rdAllLedger = new System.Windows.Forms.RadioButton();
            this.rdLedgerwise = new System.Windows.Forms.RadioButton();
            this.GridLedger = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnderGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OpBal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherCompTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApplyChange = new System.Windows.Forms.Button();
            this.TotalLedger = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtBoxSearch = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblNoteOpeningBal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.GridLedger)).BeginInit();
            this.SuspendLayout();
            // 
            // rdAllLedger
            // 
            this.rdAllLedger.AutoSize = true;
            this.rdAllLedger.BackColor = System.Drawing.Color.Transparent;
            this.rdAllLedger.Location = new System.Drawing.Point(29, 27);
            this.rdAllLedger.Name = "rdAllLedger";
            this.rdAllLedger.Size = new System.Drawing.Size(72, 17);
            this.rdAllLedger.TabIndex = 0;
            this.rdAllLedger.TabStop = true;
            this.rdAllLedger.Text = "All Ledger";
            this.rdAllLedger.UseVisualStyleBackColor = false;
            this.rdAllLedger.CheckedChanged += new System.EventHandler(this.rdAllLedger_CheckedChanged);
            // 
            // rdLedgerwise
            // 
            this.rdLedgerwise.AutoSize = true;
            this.rdLedgerwise.BackColor = System.Drawing.Color.Transparent;
            this.rdLedgerwise.Location = new System.Drawing.Point(122, 27);
            this.rdLedgerwise.Name = "rdLedgerwise";
            this.rdLedgerwise.Size = new System.Drawing.Size(85, 17);
            this.rdLedgerwise.TabIndex = 1;
            this.rdLedgerwise.TabStop = true;
            this.rdLedgerwise.Text = "Ledger Wise";
            this.rdLedgerwise.UseVisualStyleBackColor = false;
            this.rdLedgerwise.CheckedChanged += new System.EventHandler(this.rdLedgerwise_CheckedChanged);
            // 
            // GridLedger
            // 
            this.GridLedger.AllowUserToAddRows = false;
            this.GridLedger.AllowUserToDeleteRows = false;
            this.GridLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLedger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.LedgerName,
            this.UnderGroup,
            this.OpeningBalance,
            this.SignCode,
            this.LedgerNo,
            this.Check,
            this.OpBal,
            this.PkVoucherTrnNo,
            this.PkVoucherCompTrnNo});
            this.GridLedger.Location = new System.Drawing.Point(29, 50);
            this.GridLedger.Name = "GridLedger";
            this.GridLedger.Size = new System.Drawing.Size(815, 328);
            this.GridLedger.TabIndex = 4;
            this.GridLedger.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridLedger_CellEndEdit);
            this.GridLedger.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridLedger_CellFormatting);
            this.GridLedger.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridLedger_EditingControlShowing);
            this.GridLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridLedger_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle8;
            this.SrNo.HeaderText = "Sr";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 35;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.LedgerName.DefaultCellStyle = dataGridViewCellStyle9;
            this.LedgerName.HeaderText = "Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Width = 400;
            // 
            // UnderGroup
            // 
            this.UnderGroup.DataPropertyName = "GroupName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.UnderGroup.DefaultCellStyle = dataGridViewCellStyle10;
            this.UnderGroup.HeaderText = "Under Group";
            this.UnderGroup.Name = "UnderGroup";
            this.UnderGroup.ReadOnly = true;
            this.UnderGroup.Width = 135;
            // 
            // OpeningBalance
            // 
            this.OpeningBalance.DataPropertyName = "OpeningBalance";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.NullValue = null;
            this.OpeningBalance.DefaultCellStyle = dataGridViewCellStyle11;
            this.OpeningBalance.HeaderText = "Op Bal";
            this.OpeningBalance.Name = "OpeningBalance";
            this.OpeningBalance.Width = 88;
            // 
            // SignCode
            // 
            this.SignCode.DataPropertyName = "SignCode";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SignCode.DefaultCellStyle = dataGridViewCellStyle12;
            this.SignCode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.SignCode.HeaderText = "Sign";
            this.SignCode.Name = "SignCode";
            this.SignCode.ReadOnly = true;
            this.SignCode.Width = 77;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LedgerNo.DefaultCellStyle = dataGridViewCellStyle13;
            this.LedgerNo.HeaderText = "Ledger No.";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Visible = false;
            this.LedgerNo.Width = 20;
            // 
            // Check
            // 
            this.Check.DataPropertyName = "chk";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.NullValue = "False";
            this.Check.DefaultCellStyle = dataGridViewCellStyle14;
            this.Check.HeaderText = "Chk";
            this.Check.Name = "Check";
            this.Check.Visible = false;
            this.Check.Width = 35;
            // 
            // OpBal
            // 
            this.OpBal.DataPropertyName = "OpBal";
            this.OpBal.HeaderText = "OpBal";
            this.OpBal.Name = "OpBal";
            this.OpBal.Visible = false;
            this.OpBal.Width = 88;
            // 
            // PkVoucherTrnNo
            // 
            this.PkVoucherTrnNo.DataPropertyName = "PkVoucherTrnNo";
            this.PkVoucherTrnNo.HeaderText = "PkVoucherTrnNo";
            this.PkVoucherTrnNo.Name = "PkVoucherTrnNo";
            this.PkVoucherTrnNo.ReadOnly = true;
            this.PkVoucherTrnNo.Visible = false;
            // 
            // PkVoucherCompTrnNo
            // 
            this.PkVoucherCompTrnNo.DataPropertyName = "PkVoucherCompTrnNo";
            this.PkVoucherCompTrnNo.HeaderText = "PkVoucherCompTrnNo";
            this.PkVoucherCompTrnNo.Name = "PkVoucherCompTrnNo";
            this.PkVoucherCompTrnNo.ReadOnly = true;
            this.PkVoucherCompTrnNo.Visible = false;
            // 
            // btnApplyChange
            // 
            this.btnApplyChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyChange.Location = new System.Drawing.Point(28, 395);
            this.btnApplyChange.Name = "btnApplyChange";
            this.btnApplyChange.Size = new System.Drawing.Size(128, 60);
            this.btnApplyChange.TabIndex = 5;
            this.btnApplyChange.Text = "Apply Changes";
            this.btnApplyChange.UseVisualStyleBackColor = true;
            this.btnApplyChange.Click += new System.EventHandler(this.btnApplyChange_Click);
            // 
            // TotalLedger
            // 
            this.TotalLedger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TotalLedger.AutoSize = true;
            this.TotalLedger.BackColor = System.Drawing.Color.Transparent;
            this.TotalLedger.Location = new System.Drawing.Point(668, 396);
            this.TotalLedger.Name = "TotalLedger";
            this.TotalLedger.Size = new System.Drawing.Size(73, 13);
            this.TotalLedger.TabIndex = 6;
            this.TotalLedger.Text = "Total Ledger :";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Location = new System.Drawing.Point(773, 392);
            this.txtTotal.MaxLength = 9999;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(69, 20);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBoxSearch
            // 
            this.txtBoxSearch.Location = new System.Drawing.Point(226, 27);
            this.txtBoxSearch.Name = "txtBoxSearch";
            this.txtBoxSearch.Size = new System.Drawing.Size(100, 20);
            this.txtBoxSearch.TabIndex = 3;
            this.txtBoxSearch.TextChanged += new System.EventHandler(this.txtBoxSearch_TextChanged);
            this.txtBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxSearch_KeyDown);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(102, 148);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 510;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(175, 396);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 60);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChkHelp.BackColor = System.Drawing.Color.Transparent;
            this.lblChkHelp.Location = new System.Drawing.Point(256, 396);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(185, 35);
            this.lblChkHelp.TabIndex = 10029;
            this.lblChkHelp.Text = "To Pay enter data in format -200\r\nTo Receive enter data in format 200\r\n\r\n";
            // 
            // lblNoteOpeningBal
            // 
            this.lblNoteOpeningBal.AutoSize = true;
            this.lblNoteOpeningBal.BackColor = System.Drawing.Color.Transparent;
            this.lblNoteOpeningBal.Location = new System.Drawing.Point(411, 29);
            this.lblNoteOpeningBal.Name = "lblNoteOpeningBal";
            this.lblNoteOpeningBal.Size = new System.Drawing.Size(219, 13);
            this.lblNoteOpeningBal.TabIndex = 10031;
            this.lblNoteOpeningBal.Text = " = Collection Done Against Opening balance.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.label2.Location = new System.Drawing.Point(369, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 10032;
            this.label2.Text = "Color";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBillDate.Location = new System.Drawing.Point(720, 23);
            this.dtpBillDate.MinDate = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(101, 20);
            this.dtpBillDate.TabIndex = 10033;
            // 
            // LedgerOpeningBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 457);
            this.Controls.Add(this.dtpBillDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNoteOpeningBal);
            this.Controls.Add(this.lblChkHelp);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtBoxSearch);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.TotalLedger);
            this.Controls.Add(this.btnApplyChange);
            this.Controls.Add(this.GridLedger);
            this.Controls.Add(this.rdLedgerwise);
            this.Controls.Add(this.rdAllLedger);
            this.Name = "LedgerOpeningBalance";
            this.Text = "Ledger Opening Balance";
            this.Load += new System.EventHandler(this.LedgerOpeningBalance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridLedger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdAllLedger;
        private System.Windows.Forms.RadioButton rdLedgerwise;
        private System.Windows.Forms.DataGridView GridLedger;
        private System.Windows.Forms.Button btnApplyChange;
        private System.Windows.Forms.Label TotalLedger;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtBoxSearch;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblNoteOpeningBal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnderGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningBalance;
        private System.Windows.Forms.DataGridViewComboBoxColumn SignCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpBal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherCompTrnNo;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
    }
}