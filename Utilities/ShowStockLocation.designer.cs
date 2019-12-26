namespace OM
{
    partial class ShowStockLocation
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
            this.pnlStockGodown = new System.Windows.Forms.Panel();
            this.txtStockGodwnQty = new System.Windows.Forms.TextBox();
            this.dgStockGodown = new System.Windows.Forms.DataGridView();
            this.GodownNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GodownName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GActualQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkStockGodownNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStkGodownOk = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.pnlStockGodown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStockGodown)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlStockGodown
            // 
            this.pnlStockGodown.BackColor = System.Drawing.Color.Transparent;
            this.pnlStockGodown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStockGodown.Controls.Add(this.txtStockGodwnQty);
            this.pnlStockGodown.Controls.Add(this.dgStockGodown);
            this.pnlStockGodown.Controls.Add(this.btnStkGodownOk);
            this.pnlStockGodown.Controls.Add(this.label41);
            this.pnlStockGodown.Location = new System.Drawing.Point(12, 12);
            this.pnlStockGodown.Name = "pnlStockGodown";
            this.pnlStockGodown.Size = new System.Drawing.Size(356, 265);
            this.pnlStockGodown.TabIndex = 5551;
            // 
            // txtStockGodwnQty
            // 
            this.txtStockGodwnQty.Enabled = false;
            this.txtStockGodwnQty.Location = new System.Drawing.Point(239, 189);
            this.txtStockGodwnQty.Name = "txtStockGodwnQty";
            this.txtStockGodwnQty.Size = new System.Drawing.Size(100, 20);
            this.txtStockGodwnQty.TabIndex = 708;
            this.txtStockGodwnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgStockGodown
            // 
            this.dgStockGodown.AllowUserToAddRows = false;
            this.dgStockGodown.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStockGodown.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GodownNo,
            this.GodownName,
            this.Qty,
            this.GActualQty,
            this.PkStockGodownNo});
            this.dgStockGodown.Location = new System.Drawing.Point(3, 16);
            this.dgStockGodown.Name = "dgStockGodown";
            this.dgStockGodown.Size = new System.Drawing.Size(336, 167);
            this.dgStockGodown.TabIndex = 706;
            this.dgStockGodown.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStockGodown_CellEndEdit);
            this.dgStockGodown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgStockGodown_KeyDown);
            // 
            // GodownNo
            // 
            this.GodownNo.DataPropertyName = "GodownNo";
            this.GodownNo.HeaderText = "GodownNo";
            this.GodownNo.Name = "GodownNo";
            // 
            // GodownName
            // 
            this.GodownName.DataPropertyName = "GodownName";
            this.GodownName.HeaderText = "GodownName";
            this.GodownName.Name = "GodownName";
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // GActualQty
            // 
            this.GActualQty.DataPropertyName = "ActualQty";
            this.GActualQty.HeaderText = "Actual Qty";
            this.GActualQty.Name = "GActualQty";
            // 
            // PkStockGodownNo
            // 
            this.PkStockGodownNo.DataPropertyName = "PkStockGodownNo";
            this.PkStockGodownNo.HeaderText = "PkStockGodownNo";
            this.PkStockGodownNo.Name = "PkStockGodownNo";
            // 
            // btnStkGodownOk
            // 
            this.btnStkGodownOk.Location = new System.Drawing.Point(3, 190);
            this.btnStkGodownOk.Name = "btnStkGodownOk";
            this.btnStkGodownOk.Size = new System.Drawing.Size(75, 60);
            this.btnStkGodownOk.TabIndex = 707;
            this.btnStkGodownOk.Text = "OK";
            this.btnStkGodownOk.UseVisualStyleBackColor = true;
            this.btnStkGodownOk.Click += new System.EventHandler(this.btnStkGodownOk_Click);
            // 
            // label41
            // 
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(354, 13);
            this.label41.TabIndex = 0;
            this.label41.Text = "Godown Details";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowStockLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 289);
            this.Controls.Add(this.pnlStockGodown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowStockLocation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowStockLocation";
            this.pnlStockGodown.ResumeLayout(false);
            this.pnlStockGodown.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStockGodown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlStockGodown;
        private System.Windows.Forms.TextBox txtStockGodwnQty;
        private System.Windows.Forms.DataGridView dgStockGodown;
        private System.Windows.Forms.DataGridViewTextBoxColumn GodownNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GodownName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GActualQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkStockGodownNo;
        private System.Windows.Forms.Button btnStkGodownOk;
        private System.Windows.Forms.Label label41;
    }
}