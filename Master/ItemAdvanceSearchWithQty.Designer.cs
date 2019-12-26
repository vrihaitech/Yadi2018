namespace Yadi.Master
{
    partial class ItemAdvanceSearchWithQty
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
            this.pnlMain = new OMControls.OMBPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.iItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescrLang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iMRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iUOMStk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iSaleTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPurTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iRateSettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartyDiscPerce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.iItemNo,
            this.iItemName,
            this.ItemDescrLang,
            this.iRate,
            this.iUOM,
            this.iMRP,
            this.iStock,
            this.iUOMStk,
            this.iSaleTax,
            this.iPurTax,
            this.iCompany,
            this.iBarcode,
            this.iRateSettingNo,
            this.UOMDefault,
            this.PurRate,
            this.PartyDiscPerce,
            this.ShortCode,
            this.Qty});
            this.dgItemList.Location = new System.Drawing.Point(3, 3);
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.Size = new System.Drawing.Size(1021, 416);
            this.dgItemList.TabIndex = 516;
            // 
            // iItemNo
            // 
            this.iItemNo.DataPropertyName = "ItemNo";
            this.iItemNo.HeaderText = "ItemNo";
            this.iItemNo.Name = "iItemNo";
            this.iItemNo.ReadOnly = true;
            this.iItemNo.ToolTipText = "ItemNo";
            this.iItemNo.Visible = false;
            // 
            // iItemName
            // 
            this.iItemName.DataPropertyName = "ItemName";
            this.iItemName.HeaderText = "Item Description";
            this.iItemName.Name = "iItemName";
            this.iItemName.ReadOnly = true;
            this.iItemName.ToolTipText = "Product Name";
            this.iItemName.Width = 200;
            // 
            // ItemDescrLang
            // 
            this.ItemDescrLang.DataPropertyName = "ItemNameLang";
            this.ItemDescrLang.HeaderText = "Item Name";
            this.ItemDescrLang.Name = "ItemDescrLang";
            this.ItemDescrLang.ReadOnly = true;
            this.ItemDescrLang.Width = 150;
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
            // iUOM
            // 
            this.iUOM.DataPropertyName = "UOMName";
            this.iUOM.HeaderText = "UOM";
            this.iUOM.Name = "iUOM";
            this.iUOM.ReadOnly = true;
            this.iUOM.ToolTipText = "UOM";
            this.iUOM.Width = 60;
            // 
            // iMRP
            // 
            this.iMRP.DataPropertyName = "MRP";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.iMRP.DefaultCellStyle = dataGridViewCellStyle2;
            this.iMRP.HeaderText = "MRP";
            this.iMRP.Name = "iMRP";
            this.iMRP.ReadOnly = true;
            this.iMRP.ToolTipText = "MRP";
            this.iMRP.Width = 60;
            // 
            // iStock
            // 
            this.iStock.DataPropertyName = "Stock";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.iStock.DefaultCellStyle = dataGridViewCellStyle3;
            this.iStock.HeaderText = "Stock";
            this.iStock.Name = "iStock";
            this.iStock.ReadOnly = true;
            this.iStock.ToolTipText = "Stock QTY";
            this.iStock.Width = 60;
            // 
            // iUOMStk
            // 
            this.iUOMStk.DataPropertyName = "stkUOM";
            this.iUOMStk.HeaderText = "UOM";
            this.iUOMStk.Name = "iUOMStk";
            this.iUOMStk.ReadOnly = true;
            this.iUOMStk.ToolTipText = "Stock UOM";
            this.iUOMStk.Visible = false;
            this.iUOMStk.Width = 50;
            // 
            // iSaleTax
            // 
            this.iSaleTax.DataPropertyName = "SaleTax";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.iSaleTax.DefaultCellStyle = dataGridViewCellStyle4;
            this.iSaleTax.HeaderText = "S.Tax";
            this.iSaleTax.Name = "iSaleTax";
            this.iSaleTax.ReadOnly = true;
            this.iSaleTax.ToolTipText = "Sales Tax Percent";
            this.iSaleTax.Visible = false;
            this.iSaleTax.Width = 50;
            // 
            // iPurTax
            // 
            this.iPurTax.DataPropertyName = "PurTax";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.iPurTax.DefaultCellStyle = dataGridViewCellStyle5;
            this.iPurTax.HeaderText = "P.Tax";
            this.iPurTax.Name = "iPurTax";
            this.iPurTax.ReadOnly = true;
            this.iPurTax.ToolTipText = "Purchase Tax Percent";
            this.iPurTax.Visible = false;
            this.iPurTax.Width = 50;
            // 
            // iCompany
            // 
            this.iCompany.DataPropertyName = "CompanyNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.iCompany.DefaultCellStyle = dataGridViewCellStyle6;
            this.iCompany.HeaderText = "CNo";
            this.iCompany.Name = "iCompany";
            this.iCompany.ReadOnly = true;
            this.iCompany.ToolTipText = "Company No";
            this.iCompany.Visible = false;
            this.iCompany.Width = 30;
            // 
            // iBarcode
            // 
            this.iBarcode.DataPropertyName = "Barcode";
            this.iBarcode.HeaderText = "Barcode";
            this.iBarcode.Name = "iBarcode";
            this.iBarcode.ToolTipText = "Barcode";
            this.iBarcode.Visible = false;
            this.iBarcode.Width = 125;
            // 
            // iRateSettingNo
            // 
            this.iRateSettingNo.DataPropertyName = "RateSettingNo";
            this.iRateSettingNo.HeaderText = "RateSettingNo";
            this.iRateSettingNo.Name = "iRateSettingNo";
            this.iRateSettingNo.ReadOnly = true;
            this.iRateSettingNo.Visible = false;
            // 
            // UOMDefault
            // 
            this.UOMDefault.DataPropertyName = "UOMDefault";
            this.UOMDefault.HeaderText = "UOMDefault";
            this.UOMDefault.Name = "UOMDefault";
            this.UOMDefault.ReadOnly = true;
            this.UOMDefault.Visible = false;
            // 
            // PurRate
            // 
            this.PurRate.DataPropertyName = "PurRate";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PurRate.DefaultCellStyle = dataGridViewCellStyle7;
            this.PurRate.HeaderText = "PurRate";
            this.PurRate.Name = "PurRate";
            this.PurRate.ReadOnly = true;
            this.PurRate.Visible = false;
            // 
            // PartyDiscPerce
            // 
            this.PartyDiscPerce.DataPropertyName = "DiscPerce";
            this.PartyDiscPerce.HeaderText = "PartyDiscPerce";
            this.PartyDiscPerce.Name = "PartyDiscPerce";
            this.PartyDiscPerce.Visible = false;
            // 
            // ShortCode
            // 
            this.ShortCode.DataPropertyName = "ShortCode";
            this.ShortCode.HeaderText = "ShortCode";
            this.ShortCode.Name = "ShortCode";
            this.ShortCode.ReadOnly = true;
            this.ShortCode.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // ItemAdvanceSearchWithQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1039, 461);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ItemAdvanceSearchWithQty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Search";
            this.Load += new System.EventHandler(this.ItemAdvanceSearchWithQty_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridView dgItemList;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn iItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn iItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescrLang;
        private System.Windows.Forms.DataGridViewTextBoxColumn iRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn iUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn iMRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn iStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn iUOMStk;
        private System.Windows.Forms.DataGridViewTextBoxColumn iSaleTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPurTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn iCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn iBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn iRateSettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartyDiscPerce;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
    }
}