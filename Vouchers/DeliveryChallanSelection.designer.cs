namespace Yadi.Vouchers
{
    partial class DeliveryChallanSelection
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
            this.dgSO = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GDCNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GDCDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GParty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GPkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgSO)).BeginInit();
            this.SuspendLayout();
            // 
            // dgSO
            // 
            this.dgSO.AllowUserToAddRows = false;
            this.dgSO.AllowUserToDeleteRows = false;
            this.dgSO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.GDCNo,
            this.GDCDate,
            this.GAmount,
            this.GParty,
            this.GPkVoucherNo,
            this.chk});
            this.dgSO.Location = new System.Drawing.Point(12, 40);
            this.dgSO.Name = "dgSO";
            this.dgSO.Size = new System.Drawing.Size(676, 262);
            this.dgSO.TabIndex = 7;
            this.dgSO.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPO_CellFormatting);
            this.dgSO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPO_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(95, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 49);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(705, 26);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Delivery Challan Selection";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 309);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 49);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 45;
            // 
            // GDCNo
            // 
            this.GDCNo.HeaderText = "DC No";
            this.GDCNo.Name = "GDCNo";
            this.GDCNo.ReadOnly = true;
            this.GDCNo.Width = 65;
            // 
            // GDCDate
            // 
            this.GDCDate.HeaderText = "DC Date";
            this.GDCDate.Name = "GDCDate";
            this.GDCDate.ReadOnly = true;
            this.GDCDate.Width = 90;
            // 
            // GAmount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.GAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.GAmount.HeaderText = "Amount";
            this.GAmount.Name = "GAmount";
            this.GAmount.ReadOnly = true;
            // 
            // GParty
            // 
            this.GParty.HeaderText = "Party";
            this.GParty.Name = "GParty";
            this.GParty.ReadOnly = true;
            this.GParty.Width = 250;
            // 
            // GPkVoucherNo
            // 
            this.GPkVoucherNo.HeaderText = "PkVoucherNo";
            this.GPkVoucherNo.Name = "GPkVoucherNo";
            this.GPkVoucherNo.Visible = false;
            // 
            // chk
            // 
            this.chk.HeaderText = "";
            this.chk.Name = "chk";
            this.chk.Width = 45;
            // 
            // DeliveryChallanSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 362);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgSO);
            this.Name = "DeliveryChallanSelection";
            this.Text = "DC Selection";
            this.Load += new System.EventHandler(this.SOSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSO;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GDCNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GDCDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GParty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GPkVoucherNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;

    }
}