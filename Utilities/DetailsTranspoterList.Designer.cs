namespace Yadi.Utilities
{
    partial class DetailsTranspoterList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdPending = new System.Windows.Forms.RadioButton();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdCleared = new System.Windows.Forms.RadioButton();
            this.GvTran = new System.Windows.Forms.DataGridView();
            this.SRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeTransport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LrDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranspId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pkVoucherEntryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FKEwayDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvTran)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.pnlDate);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.GvTran);
            this.panel1.Location = new System.Drawing.Point(21, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1258, 532);
            this.panel1.TabIndex = 0;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(181, 486);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 34);
            this.btnReport.TabIndex = 12;
            this.btnReport.Text = "Print";
            this.btnReport.UseVisualStyleBackColor = true;
          //  this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(925, 39);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 32);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlDate
            // 
            this.pnlDate.BackColor = System.Drawing.Color.White;
            this.pnlDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDate.Controls.Add(this.dtpToDate);
            this.pnlDate.Controls.Add(this.dtpFromDate);
            this.pnlDate.Controls.Add(this.label2);
            this.pnlDate.Controls.Add(this.label1);
            this.pnlDate.Location = new System.Drawing.Point(318, 10);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(587, 53);
            this.pnlDate.TabIndex = 10;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(366, 14);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(184, 20);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpToDate_KeyPress);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(96, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(177, 20);
            this.dtpFromDate.TabIndex = 2;
            this.dtpFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpFromDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(97, 486);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 34);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(13, 485);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(78, 34);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rdPending);
            this.panel2.Controls.Add(this.rdAll);
            this.panel2.Controls.Add(this.rdCleared);
            this.panel2.Location = new System.Drawing.Point(13, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 52);
            this.panel2.TabIndex = 7;
            // 
            // rdPending
            // 
            this.rdPending.AutoSize = true;
            this.rdPending.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPending.Location = new System.Drawing.Point(20, 18);
            this.rdPending.Name = "rdPending";
            this.rdPending.Size = new System.Drawing.Size(79, 22);
            this.rdPending.TabIndex = 4;
            this.rdPending.TabStop = true;
            this.rdPending.Text = "Pending";
            this.rdPending.UseVisualStyleBackColor = true;
            this.rdPending.CheckedChanged += new System.EventHandler(this.rdPending_CheckedChanged);
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAll.Location = new System.Drawing.Point(220, 18);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(41, 22);
            this.rdAll.TabIndex = 6;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.rdAll_CheckedChanged);
            // 
            // rdCleared
            // 
            this.rdCleared.AutoSize = true;
            this.rdCleared.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdCleared.Location = new System.Drawing.Point(120, 18);
            this.rdCleared.Name = "rdCleared";
            this.rdCleared.Size = new System.Drawing.Size(77, 22);
            this.rdCleared.TabIndex = 5;
            this.rdCleared.TabStop = true;
            this.rdCleared.Text = "Cleared";
            this.rdCleared.UseVisualStyleBackColor = true;
            this.rdCleared.CheckedChanged += new System.EventHandler(this.rdCleared_CheckedChanged);
            // 
            // GvTran
            // 
            this.GvTran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvTran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SRNo,
            this.DocNo,
            this.TypeTransport,
            this.LrNo,
            this.LrDate,
            this.LedgerName,
            this.BillNo,
            this.Qty,
            this.RecQty,
            this.BalQty,
            this.TaxPer,
            this.TotalAmount,
            this.Remarks,
            this.TranspId,
            this.LedgerId,
            this.TaxId,
            this.pkVoucherEntryId,
            this.FKEwayDetails});
            this.GvTran.Location = new System.Drawing.Point(11, 77);
            this.GvTran.Name = "GvTran";
            this.GvTran.Size = new System.Drawing.Size(1235, 400);
            this.GvTran.TabIndex = 3;
            this.GvTran.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GvTran_CellFormatting);
            // 
            // SRNo
            // 
            this.SRNo.HeaderText = "SRNo";
            this.SRNo.Name = "SRNo";
            this.SRNo.Width = 50;
            // 
            // DocNo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DocNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.DocNo.HeaderText = "Doc.No";
            this.DocNo.Name = "DocNo";
            this.DocNo.Width = 50;
            // 
            // TypeTransport
            // 
            this.TypeTransport.HeaderText = "Transport Name";
            this.TypeTransport.Name = "TypeTransport";
            this.TypeTransport.Width = 160;
            // 
            // LrNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.LrNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.LrNo.HeaderText = "LR No.";
            this.LrNo.Name = "LrNo";
            this.LrNo.Width = 75;
            // 
            // LrDate
            // 
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            this.LrDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.LrDate.HeaderText = "LR Date";
            this.LrDate.Name = "LrDate";
            this.LrDate.Width = 90;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "Supplier Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.Width = 220;
            // 
            // BillNo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.BillNo.DefaultCellStyle = dataGridViewCellStyle4;
            this.BillNo.HeaderText = "Bill No";
            this.BillNo.Name = "BillNo";
            this.BillNo.Width = 70;
            // 
            // Qty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.NullValue = null;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.Qty.HeaderText = "NoOfQty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 60;
            // 
            // RecQty
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C0";
            dataGridViewCellStyle6.NullValue = null;
            this.RecQty.DefaultCellStyle = dataGridViewCellStyle6;
            this.RecQty.HeaderText = "Reci. Qty";
            this.RecQty.Name = "RecQty";
            this.RecQty.Width = 80;
            // 
            // BalQty
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "C0";
            dataGridViewCellStyle7.NullValue = null;
            this.BalQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.BalQty.HeaderText = "Bal. Qty";
            this.BalQty.Name = "BalQty";
            this.BalQty.Width = 70;
            // 
            // TaxPer
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "C0";
            dataGridViewCellStyle8.NullValue = null;
            this.TaxPer.DefaultCellStyle = dataGridViewCellStyle8;
            this.TaxPer.HeaderText = "Tax %";
            this.TaxPer.Name = "TaxPer";
            this.TaxPer.Width = 70;
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle9;
            this.TotalAmount.HeaderText = "Total Amount";
            this.TotalAmount.Name = "TotalAmount";
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remark";
            this.Remarks.Name = "Remarks";
            // 
            // TranspId
            // 
            this.TranspId.HeaderText = "TranspId";
            this.TranspId.Name = "TranspId";
            this.TranspId.Visible = false;
            // 
            // LedgerId
            // 
            this.LedgerId.HeaderText = "LedgerId";
            this.LedgerId.Name = "LedgerId";
            this.LedgerId.Visible = false;
            // 
            // TaxId
            // 
            this.TaxId.HeaderText = "TaxId";
            this.TaxId.Name = "TaxId";
            this.TaxId.Visible = false;
            // 
            // pkVoucherEntryId
            // 
            this.pkVoucherEntryId.HeaderText = "pkVoucherEntryId";
            this.pkVoucherEntryId.Name = "pkVoucherEntryId";
            this.pkVoucherEntryId.Visible = false;
            // 
            // FKEwayDetails
            // 
            this.FKEwayDetails.HeaderText = "FKEwayDetails";
            this.FKEwayDetails.Name = "FKEwayDetails";
            this.FKEwayDetails.Visible = false;
            // 
            // DetailsTranspoterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 593);
            this.Controls.Add(this.panel1);
            this.Name = "DetailsTranspoterList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetailsTranspoterList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DetailsExpenseList_Load);
            this.panel1.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvTran)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView GvTran;
        private System.Windows.Forms.RadioButton rdCleared;
        private System.Windows.Forms.RadioButton rdPending;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeTransport;
        private System.Windows.Forms.DataGridViewTextBoxColumn LrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LrDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranspId;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn pkVoucherEntryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FKEwayDetails;
    }
}