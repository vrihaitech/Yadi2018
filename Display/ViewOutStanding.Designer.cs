namespace Yadi.Display
{
    partial class ViewOutStanding
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTot = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTot);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.dgData);
            this.pnlMain.Location = new System.Drawing.Point(13, 13);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(594, 179);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTot
            // 
            this.lblTot.AutoSize = true;
            this.lblTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTot.Location = new System.Drawing.Point(173, 143);
            this.lblTot.Name = "lblTot";
            this.lblTot.Size = new System.Drawing.Size(0, 17);
            this.lblTot.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Net Balance Total :";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(491, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 60);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BillNo,
            this.BillAmount,
            this.RecAmount,
            this.NetAmount});
            this.dgData.Location = new System.Drawing.Point(22, 18);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.Size = new System.Drawing.Size(446, 105);
            this.dgData.TabIndex = 0;
            // 
            // BillNo
            // 
            this.BillNo.HeaderText = "Bill No.";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            // 
            // BillAmount
            // 
            this.BillAmount.HeaderText = "Bill Amount";
            this.BillAmount.Name = "BillAmount";
            this.BillAmount.ReadOnly = true;
            // 
            // RecAmount
            // 
            this.RecAmount.HeaderText = "Received Amount";
            this.RecAmount.Name = "RecAmount";
            this.RecAmount.ReadOnly = true;
            // 
            // NetAmount
            // 
            this.NetAmount.HeaderText = "Net Amount";
            this.NetAmount.Name = "NetAmount";
            this.NetAmount.ReadOnly = true;
            // 
            // ViewOutStanding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 208);
            this.Controls.Add(this.pnlMain);
            this.Name = "ViewOutStanding";
            this.Text = "View OutStanding";
            this.Load += new System.EventHandler(this.VOutStandingToday_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetAmount;
        private System.Windows.Forms.Label lblTot;
        private System.Windows.Forms.Label label1;
    }
}