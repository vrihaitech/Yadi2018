namespace Yadi.Utilities
{
    partial class QuotationRateChange
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvRateSetting = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.cmbItemsName = new System.Windows.Forms.ComboBox();
            this.cmbMainGroup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkSelect = new System.Windows.Forms.CheckBox();
            this.txtTotProducts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSaleRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApplySame = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApplytoInDe = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbInDeType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPerRs = new System.Windows.Forms.ComboBox();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuotationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FkRateSettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsClose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvRateSetting)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvRateSetting
            // 
            this.gvRateSetting.AllowUserToAddRows = false;
            this.gvRateSetting.AllowUserToDeleteRows = false;
            this.gvRateSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRateSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.ItemNo,
            this.ItemName,
            this.LedgerNo,
            this.LedgerName,
            this.QuotationNo,
            this.PkSrNo,
            this.FromDate,
            this.ToDate,
            this.MRP,
            this.Rate,
            this.Chk,
            this.FkRateSettingNo,
            this.IsClose,
            this.ChkChange});
            this.gvRateSetting.Location = new System.Drawing.Point(12, 55);
            this.gvRateSetting.Name = "gvRateSetting";
            this.gvRateSetting.Size = new System.Drawing.Size(1179, 301);
            this.gvRateSetting.TabIndex = 9;
            this.gvRateSetting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRateSetting_CellValueChanged);
            this.gvRateSetting.Leave += new System.EventHandler(this.gvRateSetting_Leave);
            this.gvRateSetting.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvRateSetting_CellFormatting);
            this.gvRateSetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvRateSetting_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(286, 362);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 60);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "E&xit";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(116, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyChanges.Location = new System.Drawing.Point(12, 362);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(100, 60);
            this.btnApplyChanges.TabIndex = 10;
            this.btnApplyChanges.Text = "Apply Changes";
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // cmbItemName
            // 
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.Location = new System.Drawing.Point(1052, 314);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(139, 21);
            this.cmbItemName.TabIndex = 590;
            this.cmbItemName.Visible = false;
            this.cmbItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbItemName_KeyDown);
            // 
            // cmbItemsName
            // 
            this.cmbItemsName.FormattingEnabled = true;
            this.cmbItemsName.Location = new System.Drawing.Point(107, 16);
            this.cmbItemsName.Name = "cmbItemsName";
            this.cmbItemsName.Size = new System.Drawing.Size(273, 21);
            this.cmbItemsName.TabIndex = 0;
            this.cmbItemsName.Leave += new System.EventHandler(this.cmbItemsName_Leave);
            this.cmbItemsName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbItemsName_KeyDown);
            // 
            // cmbMainGroup
            // 
            this.cmbMainGroup.FormattingEnabled = true;
            this.cmbMainGroup.Location = new System.Drawing.Point(1029, 314);
            this.cmbMainGroup.Name = "cmbMainGroup";
            this.cmbMainGroup.Size = new System.Drawing.Size(141, 21);
            this.cmbMainGroup.TabIndex = 560;
            this.cmbMainGroup.Visible = false;
            this.cmbMainGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMainGroup_KeyPress);
            this.cmbMainGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMainGroup_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(1070, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 509;
            this.label3.Text = "Item Name";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 508;
            this.label2.Text = "Item Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1049, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 507;
            this.label1.Text = "Main Group";
            this.label1.Visible = false;
            // 
            // ChkSelect
            // 
            this.ChkSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkSelect.AutoSize = true;
            this.ChkSelect.BackColor = System.Drawing.Color.Transparent;
            this.ChkSelect.Location = new System.Drawing.Point(935, 366);
            this.ChkSelect.Name = "ChkSelect";
            this.ChkSelect.Size = new System.Drawing.Size(85, 17);
            this.ChkSelect.TabIndex = 513;
            this.ChkSelect.Text = "SelectAll(F2)";
            this.ChkSelect.UseVisualStyleBackColor = false;
            this.ChkSelect.CheckedChanged += new System.EventHandler(this.ChkSelect_CheckedChanged);
            // 
            // txtTotProducts
            // 
            this.txtTotProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotProducts.Location = new System.Drawing.Point(840, 364);
            this.txtTotProducts.Name = "txtTotProducts";
            this.txtTotProducts.ReadOnly = true;
            this.txtTotProducts.Size = new System.Drawing.Size(61, 20);
            this.txtTotProducts.TabIndex = 511;
            this.txtTotProducts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(737, 367);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 512;
            this.label5.Text = "Total Products :";
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(357, 166);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 514;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(201, 362);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(565, 16);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(273, 21);
            this.cmbCustomer.TabIndex = 2;
            this.cmbCustomer.Leave += new System.EventHandler(this.cmbCustomer_Leave);
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(443, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 5009;
            this.label4.Text = "Customer Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(401, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 5010;
            this.label8.Text = "OR";
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSaleRate.Location = new System.Drawing.Point(89, 3);
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.Size = new System.Drawing.Size(100, 20);
            this.txtSaleRate.TabIndex = 0;
            this.txtSaleRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSaleRate.TextChanged += new System.EventHandler(this.txtSaleRate_TextChanged);
            this.txtSaleRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleRate_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 5012;
            this.label6.Text = "Sale Rate :";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnApplySame);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSaleRate);
            this.panel1.Location = new System.Drawing.Point(370, 362);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 28);
            this.panel1.TabIndex = 5013;
            // 
            // btnApplySame
            // 
            this.btnApplySame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplySame.Location = new System.Drawing.Point(202, 2);
            this.btnApplySame.Name = "btnApplySame";
            this.btnApplySame.Size = new System.Drawing.Size(135, 26);
            this.btnApplySame.TabIndex = 1;
            this.btnApplySame.Text = "Apply To Selected";
            this.btnApplySame.UseVisualStyleBackColor = true;
            this.btnApplySame.Click += new System.EventHandler(this.btnApplySame_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.cmbPerRs);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cmbInDeType);
            this.panel2.Controls.Add(this.btnApplytoInDe);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Location = new System.Drawing.Point(371, 396);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(448, 28);
            this.panel2.TabIndex = 5015;
            // 
            // btnApplytoInDe
            // 
            this.btnApplytoInDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplytoInDe.Location = new System.Drawing.Point(296, 1);
            this.btnApplytoInDe.Name = "btnApplytoInDe";
            this.btnApplytoInDe.Size = new System.Drawing.Size(135, 26);
            this.btnApplytoInDe.TabIndex = 3;
            this.btnApplytoInDe.Text = "Apply To Selected";
            this.btnApplytoInDe.UseVisualStyleBackColor = true;
            this.btnApplytoInDe.Click += new System.EventHandler(this.btnApplytoInDe_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAmount.Location = new System.Drawing.Point(158, 3);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(51, 20);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // cmbInDeType
            // 
            this.cmbInDeType.FormattingEnabled = true;
            this.cmbInDeType.Location = new System.Drawing.Point(6, 2);
            this.cmbInDeType.Name = "cmbInDeType";
            this.cmbInDeType.Size = new System.Drawing.Size(121, 21);
            this.cmbInDeType.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(133, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 5015;
            this.label7.Text = "By";
            // 
            // cmbPerRs
            // 
            this.cmbPerRs.FormattingEnabled = true;
            this.cmbPerRs.Location = new System.Drawing.Point(218, 4);
            this.cmbPerRs.Name = "cmbPerRs";
            this.cmbPerRs.Size = new System.Drawing.Size(72, 21);
            this.cmbPerRs.TabIndex = 2;
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle11;
            this.SrNo.HeaderText = "Sr";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 260;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.ReadOnly = true;
            this.LedgerNo.Visible = false;
            this.LedgerNo.Width = 250;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Customer Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Width = 250;
            // 
            // QuotationNo
            // 
            this.QuotationNo.DataPropertyName = "QuotationNo";
            this.QuotationNo.HeaderText = "QuotationNo";
            this.QuotationNo.Name = "QuotationNo";
            this.QuotationNo.ReadOnly = true;
            this.QuotationNo.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.DataPropertyName = "PkSrNo";
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.ReadOnly = true;
            this.PkSrNo.Visible = false;
            // 
            // FromDate
            // 
            this.FromDate.DataPropertyName = "FromDate";
            this.FromDate.HeaderText = "From Date";
            this.FromDate.Name = "FromDate";
            this.FromDate.ReadOnly = true;
            this.FromDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FromDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ToDate
            // 
            this.ToDate.DataPropertyName = "ToDate";
            this.ToDate.HeaderText = "To Date";
            this.ToDate.Name = "ToDate";
            this.ToDate.ReadOnly = true;
            this.ToDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ToDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MRP
            // 
            this.MRP.DataPropertyName = "MRP";
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            this.MRP.Visible = false;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Sale Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // Chk
            // 
            this.Chk.DataPropertyName = "Chk";
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Width = 50;
            // 
            // FkRateSettingNo
            // 
            this.FkRateSettingNo.DataPropertyName = "FkRateSettingNo";
            this.FkRateSettingNo.HeaderText = "FkRateSettingNo";
            this.FkRateSettingNo.Name = "FkRateSettingNo";
            this.FkRateSettingNo.ReadOnly = true;
            this.FkRateSettingNo.Visible = false;
            // 
            // IsClose
            // 
            this.IsClose.DataPropertyName = "IsClose";
            this.IsClose.HeaderText = "IsClose";
            this.IsClose.Name = "IsClose";
            this.IsClose.ReadOnly = true;
            this.IsClose.Visible = false;
            // 
            // ChkChange
            // 
            this.ChkChange.HeaderText = "ChkChange";
            this.ChkChange.Name = "ChkChange";
            this.ChkChange.Visible = false;
            // 
            // QuotationRateChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 434);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.ChkSelect);
            this.Controls.Add(this.txtTotProducts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbItemName);
            this.Controls.Add(this.cmbItemsName);
            this.Controls.Add(this.cmbMainGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApplyChanges);
            this.Controls.Add(this.gvRateSetting);
            this.Name = "QuotationRateChange";
            this.Text = "Quotation Rate  Change";
            this.Load += new System.EventHandler(this.QuotationRateChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvRateSetting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvRateSetting;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.ComboBox cmbItemsName;
        private System.Windows.Forms.ComboBox cmbMainGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkSelect;
        private System.Windows.Forms.TextBox txtTotProducts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSaleRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnApplySame;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbPerRs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbInDeType;
        private System.Windows.Forms.Button btnApplytoInDe;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuotationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkRateSettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChkChange;
    }
}