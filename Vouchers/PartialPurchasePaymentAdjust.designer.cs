namespace Yadi.Vouchers
{
    partial class PartialPurchasePaymentAdjust
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlPartial = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCollectionAmt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdjustAmt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBillAmt = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNMsg = new System.Windows.Forms.Label();
            this.dgPayType = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkRefTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PKVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlPartial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayType)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPartial
            // 
            this.pnlPartial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPartial.Controls.Add(this.label3);
            this.pnlPartial.Controls.Add(this.lblCollectionAmt);
            this.pnlPartial.Controls.Add(this.label2);
            this.pnlPartial.Controls.Add(this.lblAdjustAmt);
            this.pnlPartial.Controls.Add(this.label5);
            this.pnlPartial.Controls.Add(this.label1);
            this.pnlPartial.Controls.Add(this.lblBillAmt);
            this.pnlPartial.Controls.Add(this.btnCancel);
            this.pnlPartial.Controls.Add(this.lblNMsg);
            this.pnlPartial.Controls.Add(this.dgPayType);
            this.pnlPartial.Controls.Add(this.btnOk);
            this.pnlPartial.Controls.Add(this.txtTotalAmt);
            this.pnlPartial.Controls.Add(this.label26);
            this.pnlPartial.Location = new System.Drawing.Point(12, 12);
            this.pnlPartial.Name = "pnlPartial";
            this.pnlPartial.Size = new System.Drawing.Size(866, 321);
            this.pnlPartial.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 5562;
            this.label3.Text = "Payment Amt.";
            // 
            // lblCollectionAmt
            // 
            this.lblCollectionAmt.Location = new System.Drawing.Point(84, 245);
            this.lblCollectionAmt.Name = "lblCollectionAmt";
            this.lblCollectionAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCollectionAmt.Size = new System.Drawing.Size(86, 13);
            this.lblCollectionAmt.TabIndex = 5561;
            this.lblCollectionAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5560;
            this.label2.Text = "Adjust Amt :";
            // 
            // lblAdjustAmt
            // 
            this.lblAdjustAmt.Location = new System.Drawing.Point(84, 264);
            this.lblAdjustAmt.Name = "lblAdjustAmt";
            this.lblAdjustAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAdjustAmt.Size = new System.Drawing.Size(86, 13);
            this.lblAdjustAmt.TabIndex = 5559;
            this.lblAdjustAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5558;
            this.label5.Text = "Adjust Payment";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5555;
            this.label1.Text = "Bill Amt :";
            // 
            // lblBillAmt
            // 
            this.lblBillAmt.Location = new System.Drawing.Point(84, 224);
            this.lblBillAmt.Name = "lblBillAmt";
            this.lblBillAmt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblBillAmt.Size = new System.Drawing.Size(86, 13);
            this.lblBillAmt.TabIndex = 5552;
            this.lblBillAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(95, 291);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNMsg
            // 
            this.lblNMsg.Location = new System.Drawing.Point(3, 208);
            this.lblNMsg.Name = "lblNMsg";
            this.lblNMsg.Size = new System.Drawing.Size(288, 13);
            this.lblNMsg.TabIndex = 5550;
            this.lblNMsg.Text = ".";
            // 
            // dgPayType
            // 
            this.dgPayType.AllowUserToAddRows = false;
            this.dgPayType.AllowUserToDeleteRows = false;
            this.dgPayType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPayType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.VoucherDate,
            this.Amount,
            this.DiscAmt,
            this.PayTypeName,
            this.ChequeNo,
            this.ChequeDate,
            this.CreditCardNo,
            this.PkRefTrnNo,
            this.chk,
            this.PKVoucherNo});
            this.dgPayType.Location = new System.Drawing.Point(2, 27);
            this.dgPayType.Name = "dgPayType";
            this.dgPayType.Size = new System.Drawing.Size(845, 169);
            this.dgPayType.TabIndex = 0;
            this.dgPayType.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPayType_CellFormatting);
            this.dgPayType.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPayType_CellEndEdit);
            this.dgPayType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPayType_CellClick);
            this.dgPayType.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgPayType_EditingControlShowing);
            this.dgPayType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPayType_KeyDown);
            this.dgPayType.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPayType_CellContentClick);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            // 
            // VoucherDate
            // 
            this.VoucherDate.DataPropertyName = "VoucherDate";
            this.VoucherDate.HeaderText = "Date";
            this.VoucherDate.Name = "VoucherDate";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // DiscAmt
            // 
            this.DiscAmt.DataPropertyName = "DiscAmt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.DiscAmt.HeaderText = "DiscAmt";
            this.DiscAmt.Name = "DiscAmt";
            this.DiscAmt.Width = 50;
            // 
            // PayTypeName
            // 
            this.PayTypeName.DataPropertyName = "PayTypeName";
            this.PayTypeName.HeaderText = "Pay Type";
            this.PayTypeName.Name = "PayTypeName";
            // 
            // ChequeNo
            // 
            this.ChequeNo.DataPropertyName = "ChequeNo";
            this.ChequeNo.HeaderText = "ChequeNo";
            this.ChequeNo.Name = "ChequeNo";
            // 
            // ChequeDate
            // 
            this.ChequeDate.DataPropertyName = "ChequeDate";
            this.ChequeDate.HeaderText = "ChequeDate";
            this.ChequeDate.Name = "ChequeDate";
            // 
            // CreditCardNo
            // 
            this.CreditCardNo.DataPropertyName = "CreditCardNo";
            this.CreditCardNo.HeaderText = "CreditCardNo";
            this.CreditCardNo.Name = "CreditCardNo";
            // 
            // PkRefTrnNo
            // 
            this.PkRefTrnNo.DataPropertyName = "PkRefTrnNo";
            this.PkRefTrnNo.HeaderText = "PkRefTrnNo";
            this.PkRefTrnNo.Name = "PkRefTrnNo";
            this.PkRefTrnNo.Visible = false;
            // 
            // chk
            // 
            this.chk.DataPropertyName = "chk";
            this.chk.HeaderText = "";
            this.chk.Name = "chk";
            this.chk.Width = 25;
            // 
            // PKVoucherNo
            // 
            this.PKVoucherNo.DataPropertyName = "PKVoucherNo";
            this.PKVoucherNo.HeaderText = "PKVoucherNo";
            this.PKVoucherNo.Name = "PKVoucherNo";
            this.PKVoucherNo.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(7, 290);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.BackColor = System.Drawing.Color.White;
            this.txtTotalAmt.Enabled = false;
            this.txtTotalAmt.Location = new System.Drawing.Point(257, 224);
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(72, 20);
            this.txtTotalAmt.TabIndex = 656;
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmt.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(211, 229);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 13);
            this.label26.TabIndex = 502;
            this.label26.Text = "Total :";
            this.label26.Visible = false;
            // 
            // PartialPurchasePaymentAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 343);
            this.Controls.Add(this.pnlPartial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartialPurchasePaymentAdjust";
            this.Text = "PartialPayment";
            this.Load += new System.EventHandler(this.PartialPayment_Load);
            this.pnlPartial.ResumeLayout(false);
            this.pnlPartial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPartial;
        private System.Windows.Forms.Label lblNMsg;
        private System.Windows.Forms.DataGridView dgPayType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtTotalAmt;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblBillAmt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdjustAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCollectionAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkRefTrnNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn PKVoucherNo;
    }
}