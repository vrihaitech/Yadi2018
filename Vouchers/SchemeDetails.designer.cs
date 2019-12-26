namespace Yadi.Vouchers
{
    partial class SchemeDetails
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
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pnlPerc = new OMControls.OMBPanel();
            this.dgSchemeCollect = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRs = new System.Windows.Forms.TextBox();
            this.dgBill = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPerce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchAchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchAchAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlPerc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchemeCollect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.pnlPerc);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(570, 282);
            this.pnlMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(224, 228);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(123, 48);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblMsg.Location = new System.Drawing.Point(2, 2);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(567, 23);
            this.lblMsg.TabIndex = 70002;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPerc
            // 
            this.pnlPerc.BorderColor = System.Drawing.Color.Gray;
            this.pnlPerc.BorderRadius = 3;
            this.pnlPerc.Controls.Add(this.label11);
            this.pnlPerc.Controls.Add(this.txtRs);
            this.pnlPerc.Controls.Add(this.dgBill);
            this.pnlPerc.Controls.Add(this.label10);
            this.pnlPerc.Controls.Add(this.txtPercentage);
            this.pnlPerc.Controls.Add(this.dgSchemeCollect);
            this.pnlPerc.Enabled = false;
            this.pnlPerc.Location = new System.Drawing.Point(4, 28);
            this.pnlPerc.Name = "pnlPerc";
            this.pnlPerc.Size = new System.Drawing.Size(563, 251);
            this.pnlPerc.TabIndex = 70001;
            // 
            // dgSchemeCollect
            // 
            this.dgSchemeCollect.AllowUserToAddRows = false;
            this.dgSchemeCollect.AllowUserToDeleteRows = false;
            this.dgSchemeCollect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgSchemeCollect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSchemeCollect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.SchAchDate,
            this.SchAchAmt});
            this.dgSchemeCollect.Location = new System.Drawing.Point(7, 7);
            this.dgSchemeCollect.Name = "dgSchemeCollect";
            this.dgSchemeCollect.Size = new System.Drawing.Size(549, 188);
            this.dgSchemeCollect.TabIndex = 10019;
            this.dgSchemeCollect.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(293, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 13);
            this.label11.TabIndex = 10018;
            this.label11.Text = "Rs.";
            // 
            // txtRs
            // 
            this.txtRs.Location = new System.Drawing.Point(339, 6);
            this.txtRs.Name = "txtRs";
            this.txtRs.Size = new System.Drawing.Size(76, 20);
            this.txtRs.TabIndex = 10;
            this.txtRs.TabStop = false;
            this.txtRs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgBill
            // 
            this.dgBill.AllowUserToAddRows = false;
            this.dgBill.AllowUserToDeleteRows = false;
            this.dgBill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ItemName,
            this.Quantity,
            this.UOM,
            this.Rate,
            this.DiscPerce});
            this.dgBill.Location = new System.Drawing.Point(6, 41);
            this.dgBill.Name = "dgBill";
            this.dgBill.Size = new System.Drawing.Size(549, 155);
            this.dgBill.TabIndex = 12;
            this.dgBill.TabStop = false;
            this.dgBill.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBill_CellFormatting);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Sr";
            this.Column1.HeaderText = "Sr";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Description";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 240;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle1;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 80;
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UOMName";
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 50;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "MRP";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle2;
            this.Rate.HeaderText = "MRP";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 60;
            // 
            // DiscPerce
            // 
            this.DiscPerce.DataPropertyName = "DiscPercentage";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscPerce.DefaultCellStyle = dataGridViewCellStyle3;
            this.DiscPerce.HeaderText = "Disc%";
            this.DiscPerce.Name = "DiscPerce";
            this.DiscPerce.Width = 55;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(143, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 10017;
            this.label10.Text = "%";
            // 
            // txtPercentage
            // 
            this.txtPercentage.Location = new System.Drawing.Point(182, 7);
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(76, 20);
            this.txtPercentage.TabIndex = 9;
            this.txtPercentage.TabStop = false;
            this.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.Width = 50;
            // 
            // SchAchDate
            // 
            this.SchAchDate.HeaderText = "Date";
            this.SchAchDate.Name = "SchAchDate";
            // 
            // SchAchAmt
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SchAchAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.SchAchAmt.HeaderText = "Amount";
            this.SchAchAmt.Name = "SchAchAmt";
            // 
            // SchemeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 286);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SchemeDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SchemeDetails";
            this.Load += new System.EventHandler(this.SchemeDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlPerc.ResumeLayout(false);
            this.pnlPerc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchemeCollect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView dgBill;
        private System.Windows.Forms.Label lblMsg;
        private OMControls.OMBPanel pnlPerc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPercentage;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPerce;
        private System.Windows.Forms.DataGridView dgSchemeCollect;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchAchDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchAchAmt;
    }
}