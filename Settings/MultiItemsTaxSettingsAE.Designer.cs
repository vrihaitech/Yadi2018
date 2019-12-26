namespace Yadi.Settings
{
    partial class MultiItemsTaxSettingsAE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new OMControls.OMBPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.cmbGroupNo2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDepartmentName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategoryName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdBoth = new System.Windows.Forms.RadioButton();
            this.rdNotAssigned = new System.Windows.Forms.RadioButton();
            this.rdAssigned = new System.Windows.Forms.RadioButton();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvTaxItem = new System.Windows.Forms.DataGridView();
            this.Chechk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesLedgNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurLedgNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSetTax = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMsg = new OMControls.OMLabel();
            this.pnlrb = new System.Windows.Forms.Panel();
            this.lblStar10 = new System.Windows.Forms.Label();
            this.lblStar9 = new System.Windows.Forms.Label();
            this.cmbVatPurchase = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbVatSales = new System.Windows.Forms.ComboBox();
            this.pnlTAx = new OMControls.OMBPanel();
            this.lblWait = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlrb.SuspendLayout();
            this.pnlTAx.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.BorderRadius = 3;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtBarcode);
            this.panel1.Controls.Add(this.cmbGroupNo2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbDepartmentName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbCategoryName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(9, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 64);
            this.panel1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 511;
            this.label7.Text = "Barcode :";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(512, 5);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(200, 20);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // cmbGroupNo2
            // 
            this.cmbGroupNo2.FormattingEnabled = true;
            this.cmbGroupNo2.Location = new System.Drawing.Point(108, 5);
            this.cmbGroupNo2.Name = "cmbGroupNo2";
            this.cmbGroupNo2.Size = new System.Drawing.Size(200, 21);
            this.cmbGroupNo2.TabIndex = 0;
            this.cmbGroupNo2.Leave += new System.EventHandler(this.cmbGroupNo2_Leave);
            this.cmbGroupNo2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGroupNo2_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 505;
            this.label5.Text = "Department :";
            // 
            // cmbDepartmentName
            // 
            this.cmbDepartmentName.FormattingEnabled = true;
            this.cmbDepartmentName.Location = new System.Drawing.Point(108, 32);
            this.cmbDepartmentName.Name = "cmbDepartmentName";
            this.cmbDepartmentName.Size = new System.Drawing.Size(200, 21);
            this.cmbDepartmentName.TabIndex = 2;
            this.cmbDepartmentName.Leave += new System.EventHandler(this.cmbDepartmentName_Leave);
            this.cmbDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartmentName_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(408, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 504;
            this.label4.Text = "Category :";
            // 
            // cmbCategoryName
            // 
            this.cmbCategoryName.FormattingEnabled = true;
            this.cmbCategoryName.Location = new System.Drawing.Point(512, 34);
            this.cmbCategoryName.Name = "cmbCategoryName";
            this.cmbCategoryName.Size = new System.Drawing.Size(200, 21);
            this.cmbCategoryName.TabIndex = 3;
            this.cmbCategoryName.Leave += new System.EventHandler(this.cmbCategoryName_Leave);
            this.cmbCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategoryName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 503;
            this.label3.Text = "Brand Name :";
            // 
            // rdBoth
            // 
            this.rdBoth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdBoth.AutoSize = true;
            this.rdBoth.BackColor = System.Drawing.Color.Transparent;
            this.rdBoth.Location = new System.Drawing.Point(185, 7);
            this.rdBoth.Name = "rdBoth";
            this.rdBoth.Size = new System.Drawing.Size(47, 17);
            this.rdBoth.TabIndex = 509;
            this.rdBoth.TabStop = true;
            this.rdBoth.Text = "Both";
            this.rdBoth.UseVisualStyleBackColor = false;
            this.rdBoth.Click += new System.EventHandler(this.rd_Click);
            // 
            // rdNotAssigned
            // 
            this.rdNotAssigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdNotAssigned.AutoSize = true;
            this.rdNotAssigned.BackColor = System.Drawing.Color.Transparent;
            this.rdNotAssigned.Location = new System.Drawing.Point(91, 7);
            this.rdNotAssigned.Name = "rdNotAssigned";
            this.rdNotAssigned.Size = new System.Drawing.Size(88, 17);
            this.rdNotAssigned.TabIndex = 508;
            this.rdNotAssigned.TabStop = true;
            this.rdNotAssigned.Text = "Not Assigned";
            this.rdNotAssigned.UseVisualStyleBackColor = false;
            this.rdNotAssigned.Click += new System.EventHandler(this.rd_Click);
            // 
            // rdAssigned
            // 
            this.rdAssigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdAssigned.AutoSize = true;
            this.rdAssigned.BackColor = System.Drawing.Color.Transparent;
            this.rdAssigned.Location = new System.Drawing.Point(15, 7);
            this.rdAssigned.Name = "rdAssigned";
            this.rdAssigned.Size = new System.Drawing.Size(68, 17);
            this.rdAssigned.TabIndex = 502;
            this.rdAssigned.TabStop = true;
            this.rdAssigned.Text = "Assigned";
            this.rdAssigned.UseVisualStyleBackColor = false;
            this.rdAssigned.Click += new System.EventHandler(this.rd_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.chkSelectAll.Location = new System.Drawing.Point(26, 83);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(91, 17);
            this.chkSelectAll.TabIndex = 11;
            this.chkSelectAll.Text = "Select All (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgvTaxItem
            // 
            this.dgvTaxItem.AllowUserToAddRows = false;
            this.dgvTaxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTaxItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Chechk,
            this.dataGridViewTextBoxColumn1,
            this.BarCode,
            this.dataGridViewTextBoxColumn3,
            this.SVAT,
            this.PVAT,
            this.SalesLedgNo,
            this.PurLedgNo,
            this.ItemNo});
            this.dgvTaxItem.Location = new System.Drawing.Point(9, 105);
            this.dgvTaxItem.Name = "dgvTaxItem";
            this.dgvTaxItem.Size = new System.Drawing.Size(825, 294);
            this.dgvTaxItem.TabIndex = 10;
            this.dgvTaxItem.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTaxItem_CellFormatting);
            this.dgvTaxItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTaxItem_KeyDown);
            // 
            // Chechk
            // 
            this.Chechk.HeaderText = "";
            this.Chechk.Name = "Chechk";
            this.Chechk.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "SrNo";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn1.HeaderText = "SrNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // BarCode
            // 
            this.BarCode.DataPropertyName = "Barcode";
            this.BarCode.HeaderText = "BarCode";
            this.BarCode.Name = "BarCode";
            this.BarCode.ReadOnly = true;
            this.BarCode.Width = 180;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ItemName";
            this.dataGridViewTextBoxColumn3.HeaderText = "Item Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // SVAT
            // 
            this.SVAT.DataPropertyName = "SVat";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SVAT.DefaultCellStyle = dataGridViewCellStyle23;
            this.SVAT.HeaderText = "Sales %";
            this.SVAT.Name = "SVAT";
            this.SVAT.ReadOnly = true;
            // 
            // PVAT
            // 
            this.PVAT.DataPropertyName = "PVat";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PVAT.DefaultCellStyle = dataGridViewCellStyle24;
            this.PVAT.HeaderText = "Purchase %";
            this.PVAT.Name = "PVAT";
            this.PVAT.ReadOnly = true;
            // 
            // SalesLedgNo
            // 
            this.SalesLedgNo.DataPropertyName = "SalesLedgerNo";
            this.SalesLedgNo.HeaderText = "SalesLedgNo";
            this.SalesLedgNo.Name = "SalesLedgNo";
            this.SalesLedgNo.ReadOnly = true;
            this.SalesLedgNo.Visible = false;
            // 
            // PurLedgNo
            // 
            this.PurLedgNo.DataPropertyName = "PurLedgerNo";
            this.PurLedgNo.HeaderText = "PurLedgNo";
            this.PurLedgNo.Name = "PurLedgNo";
            this.PurLedgNo.ReadOnly = true;
            this.PurLedgNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnSetTax
            // 
            this.btnSetTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetTax.Location = new System.Drawing.Point(9, 465);
            this.btnSetTax.Name = "btnSetTax";
            this.btnSetTax.Size = new System.Drawing.Size(80, 60);
            this.btnSetTax.TabIndex = 12;
            this.btnSetTax.Text = "Save";
            this.btnSetTax.UseVisualStyleBackColor = true;
            this.btnSetTax.Click += new System.EventHandler(this.btnSetTax_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(107, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 54;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(203, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 60);
            this.btnClose.TabIndex = 55;
            this.btnClose.Text = "E&xit";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.CornerRadius = 3;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.GradientBottom = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.GradientMiddle = System.Drawing.Color.White;
            this.lblMsg.GradientShow = true;
            this.lblMsg.GradientTop = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.Location = new System.Drawing.Point(270, 249);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 506;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // pnlrb
            // 
            this.pnlrb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlrb.BackColor = System.Drawing.Color.Transparent;
            this.pnlrb.Controls.Add(this.rdAssigned);
            this.pnlrb.Controls.Add(this.rdNotAssigned);
            this.pnlrb.Controls.Add(this.rdBoth);
            this.pnlrb.Location = new System.Drawing.Point(293, 468);
            this.pnlrb.Name = "pnlrb";
            this.pnlrb.Size = new System.Drawing.Size(242, 31);
            this.pnlrb.TabIndex = 510;
            // 
            // lblStar10
            // 
            this.lblStar10.AutoSize = true;
            this.lblStar10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStar10.Location = new System.Drawing.Point(407, 28);
            this.lblStar10.Name = "lblStar10";
            this.lblStar10.Size = new System.Drawing.Size(11, 13);
            this.lblStar10.TabIndex = 190018;
            this.lblStar10.Text = "*";
            // 
            // lblStar9
            // 
            this.lblStar9.AutoSize = true;
            this.lblStar9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStar9.Location = new System.Drawing.Point(189, 26);
            this.lblStar9.Name = "lblStar9";
            this.lblStar9.Size = new System.Drawing.Size(11, 13);
            this.lblStar9.TabIndex = 190017;
            this.lblStar9.Text = "*";
            // 
            // cmbVatPurchase
            // 
            this.cmbVatPurchase.FormattingEnabled = true;
            this.cmbVatPurchase.Location = new System.Drawing.Point(283, 22);
            this.cmbVatPurchase.MaxLength = 25;
            this.cmbVatPurchase.Name = "cmbVatPurchase";
            this.cmbVatPurchase.Size = new System.Drawing.Size(121, 21);
            this.cmbVatPurchase.TabIndex = 19;
            this.cmbVatPurchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbVatPurchase_KeyDown);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(310, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(52, 13);
            this.label26.TabIndex = 10009;
            this.label26.Text = "Purchase";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(109, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 10008;
            this.label25.Text = "Sales";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(11, 28);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(28, 13);
            this.label24.TabIndex = 10007;
            this.label24.Text = "VAT";
            // 
            // cmbVatSales
            // 
            this.cmbVatSales.FormattingEnabled = true;
            this.cmbVatSales.Location = new System.Drawing.Point(65, 21);
            this.cmbVatSales.Name = "cmbVatSales";
            this.cmbVatSales.Size = new System.Drawing.Size(121, 21);
            this.cmbVatSales.TabIndex = 18;
            this.cmbVatSales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbVatSales_KeyDown);
            // 
            // pnlTAx
            // 
            this.pnlTAx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTAx.BackColor = System.Drawing.Color.Transparent;
            this.pnlTAx.BorderColor = System.Drawing.Color.Gray;
            this.pnlTAx.BorderRadius = 3;
            this.pnlTAx.Controls.Add(this.label25);
            this.pnlTAx.Controls.Add(this.lblStar10);
            this.pnlTAx.Controls.Add(this.cmbVatSales);
            this.pnlTAx.Controls.Add(this.lblStar9);
            this.pnlTAx.Controls.Add(this.label24);
            this.pnlTAx.Controls.Add(this.label26);
            this.pnlTAx.Controls.Add(this.cmbVatPurchase);
            this.pnlTAx.Location = new System.Drawing.Point(9, 405);
            this.pnlTAx.Name = "pnlTAx";
            this.pnlTAx.Size = new System.Drawing.Size(446, 54);
            this.pnlTAx.TabIndex = 190019;
            this.pnlTAx.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWait.Location = new System.Drawing.Point(465, 425);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(350, 31);
            this.lblWait.TabIndex = 60052;
            this.lblWait.Text = "label8";
            this.lblWait.Visible = false;
            // 
            // MultiItemsTaxSettingsAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 539);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.pnlTAx);
            this.Controls.Add(this.pnlrb);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSetTax);
            this.Controls.Add(this.dgvTaxItem);
            this.Controls.Add(this.panel1);
            this.Name = "MultiItemsTaxSettingsAE";
            this.Text = "MultiItemsTaxSettings";
            this.Load += new System.EventHandler(this.MultiItemsTaxSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlrb.ResumeLayout(false);
            this.pnlrb.PerformLayout();
            this.pnlTAx.ResumeLayout(false);
            this.pnlTAx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OMControls.OMBPanel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDepartmentName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategoryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvTaxItem;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnSetTax;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbGroupNo2;
        private System.Windows.Forms.RadioButton rdBoth;
        private System.Windows.Forms.RadioButton rdNotAssigned;
        private System.Windows.Forms.RadioButton rdAssigned;
        private OMControls.OMLabel lblMsg;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlrb;
        private System.Windows.Forms.Label lblStar10;
        private System.Windows.Forms.Label lblStar9;
        private System.Windows.Forms.ComboBox cmbVatPurchase;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbVatSales;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chechk;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn SVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesLedgNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurLedgNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private OMControls.OMBPanel pnlTAx;
        private System.Windows.Forms.Label lblWait;
    }
}