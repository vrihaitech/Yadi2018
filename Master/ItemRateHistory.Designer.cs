namespace Yadi.Master
{
    partial class ItemRateHistory
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
            this.btnOk = new System.Windows.Forms.Button();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.VoucherUserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BilledQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnOk);
            this.pnlMain.Controls.Add(this.dgItemList);
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1028, 452);
            this.pnlMain.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(935, 425);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 517;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgItemList
            // 
            this.dgItemList.AllowUserToAddRows = false;
            this.dgItemList.AllowUserToDeleteRows = false;
            this.dgItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgItemList.BackgroundColor = System.Drawing.Color.Bisque;
            this.dgItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VoucherUserNo,
            this.VoucherDate,
            this.BilledQuantity,
            this.DiscPercentage,
            this.iRate});
            this.dgItemList.Location = new System.Drawing.Point(3, 3);
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.Size = new System.Drawing.Size(1021, 416);
            this.dgItemList.TabIndex = 516;
            this.dgItemList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItemList_KeyDown);
            // 
            // VoucherUserNo
            // 
            this.VoucherUserNo.HeaderText = "Bill No";
            this.VoucherUserNo.Name = "VoucherUserNo";
            this.VoucherUserNo.ReadOnly = true;
            // 
            // VoucherDate
            // 
            this.VoucherDate.HeaderText = "Voucher Date";
            this.VoucherDate.Name = "VoucherDate";
            this.VoucherDate.ReadOnly = true;
            // 
            // BilledQuantity
            // 
            this.BilledQuantity.HeaderText = "Qty";
            this.BilledQuantity.Name = "BilledQuantity";
            this.BilledQuantity.ReadOnly = true;
            // 
            // DiscPercentage
            // 
            this.DiscPercentage.HeaderText = "DiscPercentage";
            this.DiscPercentage.Name = "DiscPercentage";
            // 
            // iRate
            // 
            this.iRate.DataPropertyName = "SaleRate";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.iRate.DefaultCellStyle = dataGridViewCellStyle1;
            this.iRate.HeaderText = "Rate";
            this.iRate.Name = "iRate";
            this.iRate.ReadOnly = true;
            this.iRate.ToolTipText = "Sales Rate";
            this.iRate.Width = 60;
            // 
            // ItemRateHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1039, 461);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ItemRateHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Search";
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridView dgItemList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherUserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BilledQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn iRate;
    }
}