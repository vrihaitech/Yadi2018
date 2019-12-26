namespace Yadi.Utilities
{
    partial class BulkLRNOEntry
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
            this.pnlMain = new OMControls.OMBPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lstTransName = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnApplyChange = new System.Windows.Forms.Button();
            this.dgBill = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Party = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransporterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LRNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransporterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlhedder = new OMControls.OMBPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.rbFeeded = new System.Windows.Forms.RadioButton();
            this.rbEmpty = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).BeginInit();
            this.pnlhedder.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.lstTransName);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnApplyChange);
            this.pnlMain.Controls.Add(this.dgBill);
            this.pnlMain.Controls.Add(this.pnlhedder);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(754, 489);
            this.pnlMain.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(137, 218);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 506;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // lstTransName
            // 
            this.lstTransName.FormattingEnabled = true;
            this.lstTransName.Location = new System.Drawing.Point(497, 146);
            this.lstTransName.Name = "lstTransName";
            this.lstTransName.Size = new System.Drawing.Size(239, 186);
            this.lstTransName.TabIndex = 112;
            this.lstTransName.Visible = false;
            this.lstTransName.VisibleChanged += new System.EventHandler(this.lstTransName_VisibleChanged);
            this.lstTransName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstTransName_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(165, 444);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 37);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnApplyChange
            // 
            this.btnApplyChange.Location = new System.Drawing.Point(15, 444);
            this.btnApplyChange.Name = "btnApplyChange";
            this.btnApplyChange.Size = new System.Drawing.Size(147, 37);
            this.btnApplyChange.TabIndex = 8;
            this.btnApplyChange.Text = "Apply Changes";
            this.btnApplyChange.UseVisualStyleBackColor = true;
            this.btnApplyChange.Click += new System.EventHandler(this.btnApplyChange_Click);
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
            this.Party,
            this.Amount,
            this.TransporterName,
            this.LRNO,
            this.TransporterNo,
            this.PkVoucherNo,
            this.IsChange});
            this.dgBill.Location = new System.Drawing.Point(15, 120);
            this.dgBill.Name = "dgBill";
            this.dgBill.Size = new System.Drawing.Size(721, 318);
            this.dgBill.TabIndex = 7;
            this.dgBill.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBill_CellFormatting);
            this.dgBill.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBill_CellEndEdit);
            this.dgBill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBill_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 80;
            // 
            // BillNo
            // 
            this.BillNo.HeaderText = "BillNo";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            this.BillNo.Width = 60;
            // 
            // Party
            // 
            this.Party.HeaderText = "Party";
            this.Party.Name = "Party";
            this.Party.ReadOnly = true;
            this.Party.Width = 180;
            // 
            // Amount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 80;
            // 
            // TransporterName
            // 
            this.TransporterName.HeaderText = "Tran.Name";
            this.TransporterName.Name = "TransporterName";
            this.TransporterName.ReadOnly = true;
            this.TransporterName.Width = 150;
            // 
            // LRNO
            // 
            this.LRNO.HeaderText = "LRNO";
            this.LRNO.Name = "LRNO";
            this.LRNO.Width = 80;
            // 
            // TransporterNo
            // 
            this.TransporterNo.HeaderText = "TransporterNo";
            this.TransporterNo.Name = "TransporterNo";
            this.TransporterNo.Visible = false;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.Visible = false;
            // 
            // IsChange
            // 
            this.IsChange.HeaderText = "IsChange";
            this.IsChange.Name = "IsChange";
            this.IsChange.Visible = false;
            // 
            // pnlhedder
            // 
            this.pnlhedder.BorderColor = System.Drawing.Color.Gray;
            this.pnlhedder.BorderRadius = 3;
            this.pnlhedder.Controls.Add(this.btnCancel);
            this.pnlhedder.Controls.Add(this.btnShow);
            this.pnlhedder.Controls.Add(this.rbFeeded);
            this.pnlhedder.Controls.Add(this.rbEmpty);
            this.pnlhedder.Controls.Add(this.rbAll);
            this.pnlhedder.Controls.Add(this.label2);
            this.pnlhedder.Controls.Add(this.DTPFromDate);
            this.pnlhedder.Controls.Add(this.label3);
            this.pnlhedder.Controls.Add(this.DTToDate);
            this.pnlhedder.Controls.Add(this.cmbCompanyName);
            this.pnlhedder.Controls.Add(this.label1);
            this.pnlhedder.Location = new System.Drawing.Point(15, 13);
            this.pnlhedder.Name = "pnlhedder";
            this.pnlhedder.Size = new System.Drawing.Size(721, 101);
            this.pnlhedder.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(630, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 47);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(541, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(83, 47);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // rbFeeded
            // 
            this.rbFeeded.AutoSize = true;
            this.rbFeeded.Location = new System.Drawing.Point(308, 80);
            this.rbFeeded.Name = "rbFeeded";
            this.rbFeeded.Size = new System.Drawing.Size(113, 17);
            this.rbFeeded.TabIndex = 5;
            this.rbFeeded.Text = "LRNo Feeded (F3)";
            this.rbFeeded.UseVisualStyleBackColor = true;
            this.rbFeeded.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbEmpty
            // 
            this.rbEmpty.AutoSize = true;
            this.rbEmpty.Location = new System.Drawing.Point(196, 80);
            this.rbEmpty.Name = "rbEmpty";
            this.rbEmpty.Size = new System.Drawing.Size(106, 17);
            this.rbEmpty.TabIndex = 4;
            this.rbEmpty.Text = "LRNo Empty (F2)";
            this.rbEmpty.UseVisualStyleBackColor = true;
            this.rbEmpty.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(129, 80);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(57, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All (F1)";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 101546;
            this.label2.Text = "From Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(129, 41);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(134, 23);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(292, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 10456464;
            this.label3.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(393, 41);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(140, 23);
            this.DTToDate.TabIndex = 2;
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(129, 10);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(404, 21);
            this.cmbCompanyName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 65464;
            this.label1.Text = "Company Name :";
            // 
            // BulkLRNOEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 507);
            this.Controls.Add(this.pnlMain);
            this.Name = "BulkLRNOEntry";
            this.Text = "Bulk LRNo Entry";
            this.Load += new System.EventHandler(this.BulkLRNOEntry_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).EndInit();
            this.pnlhedder.ResumeLayout(false);
            this.pnlhedder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private OMControls.OMBPanel pnlhedder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.RadioButton rbEmpty;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.RadioButton rbFeeded;
        private System.Windows.Forms.DataGridView dgBill;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnApplyChange;
        private System.Windows.Forms.ListBox lstTransName;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Party;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransporterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LRNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransporterNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsChange;
    }
}