namespace Yadi.Master
{
    partial class StockOrderItemsAE
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
            this.pnlMain = new OMControls.OMBPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.chkGeneralSelect = new System.Windows.Forms.CheckBox();
            this.chkUploadSelect = new System.Windows.Forms.CheckBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.cmbBrandName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemShortDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualStockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LangBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LangItemDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Upload = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.General = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsUpload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsGeneral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockGroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockOrderItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempActualStockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempItemShortCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FKRateSettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.pnlBrand.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.dgItems);
            this.pnlMain.Controls.Add(this.pnlBrand);
            this.pnlMain.Location = new System.Drawing.Point(11, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1159, 492);
            this.pnlMain.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(229, 210);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 516;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.chkGeneralSelect);
            this.panel1.Controls.Add(this.chkUploadSelect);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(16, 404);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1128, 65);
            this.panel1.TabIndex = 2;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.Location = new System.Drawing.Point(390, 5);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(243, 23);
            this.lblChkHelp.TabIndex = 516;
            this.lblChkHelp.Text = "F6: Previous          F5: Next\r\n";
            this.lblChkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkGeneralSelect
            // 
            this.chkGeneralSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGeneralSelect.AutoSize = true;
            this.chkGeneralSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkGeneralSelect.Location = new System.Drawing.Point(767, 5);
            this.chkGeneralSelect.Name = "chkGeneralSelect";
            this.chkGeneralSelect.Size = new System.Drawing.Size(125, 17);
            this.chkGeneralSelect.TabIndex = 515;
            this.chkGeneralSelect.Text = "General SelectAll(F3)";
            this.chkGeneralSelect.UseVisualStyleBackColor = false;
            this.chkGeneralSelect.CheckedChanged += new System.EventHandler(this.chkGeneralSelect_CheckedChanged);
            // 
            // chkUploadSelect
            // 
            this.chkUploadSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUploadSelect.AutoSize = true;
            this.chkUploadSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkUploadSelect.Location = new System.Drawing.Point(639, 5);
            this.chkUploadSelect.Name = "chkUploadSelect";
            this.chkUploadSelect.Size = new System.Drawing.Size(122, 17);
            this.chkUploadSelect.TabIndex = 514;
            this.chkUploadSelect.Text = "Upload SelectAll(F2)";
            this.chkUploadSelect.UseVisualStyleBackColor = false;
            this.chkUploadSelect.CheckedChanged += new System.EventHandler(this.chkUploadSelect_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(209, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(95, 43);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(109, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 43);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 43);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.ItemShortDesc,
            this.ActualStockName,
            this.ItemDesc,
            this.LangBrand,
            this.LangItemDesc,
            this.Upload,
            this.General,
            this.IsUpload,
            this.IsGeneral,
            this.ItemNo,
            this.StockGroupNo,
            this.StockOrderItemNo,
            this.TempActualStockName,
            this.TempItemShortCode,
            this.TempItemName,
            this.UOM,
            this.MRP,
            this.Stock,
            this.FKRateSettingNo,
            this.SRate});
            this.dgItems.Location = new System.Drawing.Point(16, 68);
            this.dgItems.Name = "dgItems";
            this.dgItems.Size = new System.Drawing.Size(1128, 330);
            this.dgItems.TabIndex = 1;
            this.dgItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgItems_CellFormatting);
            this.dgItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellEndEdit);
            this.dgItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellContentClick);
            // 
            // pnlBrand
            // 
            this.pnlBrand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBrand.Controls.Add(this.cmbBrandName);
            this.pnlBrand.Controls.Add(this.label2);
            this.pnlBrand.Location = new System.Drawing.Point(16, 14);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new System.Drawing.Size(1128, 48);
            this.pnlBrand.TabIndex = 0;
            // 
            // cmbBrandName
            // 
            this.cmbBrandName.FormattingEnabled = true;
            this.cmbBrandName.Location = new System.Drawing.Point(93, 14);
            this.cmbBrandName.Name = "cmbBrandName";
            this.cmbBrandName.Size = new System.Drawing.Size(411, 21);
            this.cmbBrandName.TabIndex = 509;
            this.cmbBrandName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBrandName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 510;
            this.label2.Text = "Brand Name";
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // ItemShortDesc
            // 
            this.ItemShortDesc.DataPropertyName = "ItemShortCode";
            this.ItemShortDesc.HeaderText = "ItemShortDesc";
            this.ItemShortDesc.Name = "ItemShortDesc";
            this.ItemShortDesc.Width = 150;
            // 
            // ActualStockName
            // 
            this.ActualStockName.DataPropertyName = "ActualStockGroupName";
            this.ActualStockName.HeaderText = "ActualStockName";
            this.ActualStockName.Name = "ActualStockName";
            this.ActualStockName.Width = 150;
            // 
            // ItemDesc
            // 
            this.ItemDesc.DataPropertyName = "ItemName";
            this.ItemDesc.HeaderText = "ItemDesc";
            this.ItemDesc.Name = "ItemDesc";
            this.ItemDesc.Width = 150;
            // 
            // LangBrand
            // 
            this.LangBrand.DataPropertyName = "LanguageName";
            this.LangBrand.HeaderText = "LangBrand";
            this.LangBrand.Name = "LangBrand";
            this.LangBrand.ReadOnly = true;
            this.LangBrand.Width = 150;
            // 
            // LangItemDesc
            // 
            this.LangItemDesc.DataPropertyName = "LangFullDesc";
            this.LangItemDesc.HeaderText = "LangItemDesc";
            this.LangItemDesc.Name = "LangItemDesc";
            this.LangItemDesc.ReadOnly = true;
            this.LangItemDesc.Width = 150;
            // 
            // Upload
            // 
            this.Upload.DataPropertyName = "Upload";
            this.Upload.FalseValue = "False";
            this.Upload.HeaderText = "Upload";
            this.Upload.Name = "Upload";
            this.Upload.TrueValue = "True";
            this.Upload.Width = 55;
            // 
            // General
            // 
            this.General.DataPropertyName = "General";
            this.General.FalseValue = "False";
            this.General.HeaderText = "General";
            this.General.Name = "General";
            this.General.TrueValue = "True";
            this.General.Width = 55;
            // 
            // IsUpload
            // 
            this.IsUpload.DataPropertyName = "IsUpload";
            this.IsUpload.HeaderText = "IsUpload";
            this.IsUpload.Name = "IsUpload";
            this.IsUpload.Visible = false;
            // 
            // IsGeneral
            // 
            this.IsGeneral.DataPropertyName = "IsGeneral";
            this.IsGeneral.HeaderText = "IsGeneral";
            this.IsGeneral.Name = "IsGeneral";
            this.IsGeneral.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // StockGroupNo
            // 
            this.StockGroupNo.DataPropertyName = "StockGroupNo";
            this.StockGroupNo.HeaderText = "StockGroupNo";
            this.StockGroupNo.Name = "StockGroupNo";
            this.StockGroupNo.Visible = false;
            // 
            // StockOrderItemNo
            // 
            this.StockOrderItemNo.DataPropertyName = "StockOrderItemNo";
            this.StockOrderItemNo.HeaderText = "StockOrderItemNo";
            this.StockOrderItemNo.Name = "StockOrderItemNo";
            this.StockOrderItemNo.Visible = false;
            // 
            // TempActualStockName
            // 
            this.TempActualStockName.HeaderText = "TempActualStockName";
            this.TempActualStockName.Name = "TempActualStockName";
            this.TempActualStockName.Visible = false;
            // 
            // TempItemShortCode
            // 
            this.TempItemShortCode.HeaderText = "TempItemShortCode";
            this.TempItemShortCode.Name = "TempItemShortCode";
            this.TempItemShortCode.Visible = false;
            // 
            // TempItemName
            // 
            this.TempItemName.HeaderText = "TempItemName";
            this.TempItemName.Name = "TempItemName";
            this.TempItemName.Visible = false;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.Width = 40;
            // 
            // MRP
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MRP.DefaultCellStyle = dataGridViewCellStyle1;
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.Width = 65;
            // 
            // Stock
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Stock.DefaultCellStyle = dataGridViewCellStyle2;
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.Width = 55;
            // 
            // FKRateSettingNo
            // 
            this.FKRateSettingNo.HeaderText = "FKRateSettingNo";
            this.FKRateSettingNo.Name = "FKRateSettingNo";
            this.FKRateSettingNo.Visible = false;
            // 
            // SRate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.SRate.HeaderText = "Rate";
            this.SRate.Name = "SRate";
            this.SRate.Width = 55;
            // 
            // StockOrderItemsAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 507);
            this.Controls.Add(this.pnlMain);
            this.Name = "StockOrderItemsAE";
            this.Text = "Stock Order Items";
            this.Load += new System.EventHandler(this.StockOrderItemsAE_Load);
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.pnlBrand.ResumeLayout(false);
            this.pnlBrand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.ComboBox cmbBrandName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.CheckBox chkGeneralSelect;
        private System.Windows.Forms.CheckBox chkUploadSelect;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemShortDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualStockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn LangBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn LangItemDesc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Upload;
        private System.Windows.Forms.DataGridViewCheckBoxColumn General;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsGeneral;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockGroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockOrderItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempActualStockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempItemShortCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn FKRateSettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRate;
    }
}