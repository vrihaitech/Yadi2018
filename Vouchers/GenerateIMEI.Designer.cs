namespace Yadi.Vouchers
{
    partial class GenerateIMEI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.dgGenerateIMEI = new System.Windows.Forms.DataGridView();
            this.SrNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMEINo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FkStockTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesStockTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesFkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkGenerateIMEIID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectIMEI = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GvItem = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkStockTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGenerateIMEI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(256, 544);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 42);
            this.BtnExit.TabIndex = 14;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(10, 544);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 42);
            this.BtnSave.TabIndex = 8;
            this.BtnSave.Text = "Add";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(174, 544);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 42);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.btnSales);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnRelease);
            this.pnlMain.Controls.Add(this.dgGenerateIMEI);
            this.pnlMain.Controls.Add(this.GvItem);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Location = new System.Drawing.Point(16, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(788, 594);
            this.pnlMain.TabIndex = 16;
            // 
            // btnSales
            // 
            this.btnSales.Location = new System.Drawing.Point(10, 544);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(80, 42);
            this.btnSales.TabIndex = 19;
            this.btnSales.Text = "Sales";
            this.btnSales.UseVisualStyleBackColor = true;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(338, 544);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 42);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(92, 544);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(80, 42);
            this.btnRelease.TabIndex = 18;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // dgGenerateIMEI
            // 
            this.dgGenerateIMEI.AllowUserToAddRows = false;
            this.dgGenerateIMEI.AllowUserToDeleteRows = false;
            this.dgGenerateIMEI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGenerateIMEI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo1,
            this.IMEINo,
            this.FkVoucherNo,
            this.ItemNo1,
            this.FkStockTrnNo,
            this.IsSales,
            this.SalesStockTrnNo,
            this.SalesFkVoucherNo,
            this.PkGenerateIMEIID,
            this.SelectIMEI});
            this.dgGenerateIMEI.Location = new System.Drawing.Point(439, 6);
            this.dgGenerateIMEI.Name = "dgGenerateIMEI";
            this.dgGenerateIMEI.Size = new System.Drawing.Size(341, 531);
            this.dgGenerateIMEI.TabIndex = 16;
            this.dgGenerateIMEI.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgGenerateIMEI_CellEndEdit);
            this.dgGenerateIMEI.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgGenerateIMEI_CellFormatting);
            this.dgGenerateIMEI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgGenerateIMEI_KeyDown);
            // 
            // SrNo1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo1.DefaultCellStyle = dataGridViewCellStyle1;
            this.SrNo1.HeaderText = "SrNo";
            this.SrNo1.Name = "SrNo1";
            this.SrNo1.ReadOnly = true;
            this.SrNo1.Width = 45;
            // 
            // IMEINo
            // 
            this.IMEINo.HeaderText = "IMEI No";
            this.IMEINo.Name = "IMEINo";
            this.IMEINo.Width = 170;
            // 
            // FkVoucherNo
            // 
            this.FkVoucherNo.HeaderText = "FkVoucherNo";
            this.FkVoucherNo.Name = "FkVoucherNo";
            this.FkVoucherNo.Visible = false;
            // 
            // ItemNo1
            // 
            this.ItemNo1.HeaderText = "ItemNo";
            this.ItemNo1.Name = "ItemNo1";
            this.ItemNo1.Visible = false;
            // 
            // FkStockTrnNo
            // 
            this.FkStockTrnNo.HeaderText = "FkStockTrnNo";
            this.FkStockTrnNo.Name = "FkStockTrnNo";
            this.FkStockTrnNo.Visible = false;
            // 
            // IsSales
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IsSales.DefaultCellStyle = dataGridViewCellStyle2;
            this.IsSales.HeaderText = "IsSales";
            this.IsSales.Name = "IsSales";
            this.IsSales.ReadOnly = true;
            this.IsSales.Width = 62;
            // 
            // SalesStockTrnNo
            // 
            this.SalesStockTrnNo.HeaderText = "SalesStockTrnNo";
            this.SalesStockTrnNo.Name = "SalesStockTrnNo";
            this.SalesStockTrnNo.Visible = false;
            // 
            // SalesFkVoucherNo
            // 
            this.SalesFkVoucherNo.HeaderText = "SalesFkVoucherNo";
            this.SalesFkVoucherNo.Name = "SalesFkVoucherNo";
            this.SalesFkVoucherNo.Visible = false;
            // 
            // PkGenerateIMEIID
            // 
            this.PkGenerateIMEIID.HeaderText = "PkGenerateIMEIID";
            this.PkGenerateIMEIID.Name = "PkGenerateIMEIID";
            this.PkGenerateIMEIID.Visible = false;
            // 
            // SelectIMEI
            // 
            this.SelectIMEI.HeaderText = "Select";
            this.SelectIMEI.Name = "SelectIMEI";
            this.SelectIMEI.Width = 60;
            // 
            // GvItem
            // 
            this.GvItem.AllowUserToAddRows = false;
            this.GvItem.AllowUserToDeleteRows = false;
            this.GvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.ItemName,
            this.Quantity,
            this.tItemNo,
            this.PkStockTrnNo,
            this.PkVoucherNo,
            this.SelectItem});
            this.GvItem.Location = new System.Drawing.Point(6, 6);
            this.GvItem.Name = "GvItem";
            this.GvItem.Size = new System.Drawing.Size(429, 531);
            this.GvItem.TabIndex = 15;
            this.GvItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvItem_CellContentClick);
            this.GvItem.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GvItem_CellFormatting);
            // 
            // SrNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 45;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 260;
            // 
            // Quantity
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 60;
            // 
            // tItemNo
            // 
            this.tItemNo.HeaderText = "ItemNo";
            this.tItemNo.Name = "tItemNo";
            this.tItemNo.Visible = false;
            // 
            // PkStockTrnNo
            // 
            this.PkStockTrnNo.HeaderText = "PkStockTrnNo";
            this.PkStockTrnNo.Name = "PkStockTrnNo";
            this.PkStockTrnNo.Visible = false;
            this.PkStockTrnNo.Width = 45;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.Visible = false;
            // 
            // SelectItem
            // 
            this.SelectItem.HeaderText = "Select";
            this.SelectItem.Name = "SelectItem";
            this.SelectItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectItem.Width = 60;
            // 
            // GenerateIMEI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 630);
            this.Controls.Add(this.pnlMain);
            this.Name = "GenerateIMEI";
            this.Text = "Generate IMEI";
            this.Load += new System.EventHandler(this.GenerateIMEI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGenerateIMEI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider EP;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView GvItem;
        private System.Windows.Forms.DataGridView dgGenerateIMEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn tItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkStockTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectItem;
        private System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnRelease;
        public System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMEINo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkStockTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesStockTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesFkVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkGenerateIMEIID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectIMEI;
    }
}