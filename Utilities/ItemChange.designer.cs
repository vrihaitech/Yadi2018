namespace Yadi.Utilities
{
    partial class ItemChange
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvRateSetting = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HidChk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.cmbBrandName = new System.Windows.Forms.ComboBox();
            this.cmbMainGroup = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkSelect = new System.Windows.Forms.CheckBox();
            this.txtTotProducts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlChange = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gvRateSetting)).BeginInit();
            this.pnlChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvRateSetting
            // 
            this.gvRateSetting.AllowUserToAddRows = false;
            this.gvRateSetting.AllowUserToDeleteRows = false;
            this.gvRateSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRateSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.DepartmentName,
            this.CategoryName,
            this.ItemName,
            this.ItemNo,
            this.DepartmentNo,
            this.CategoryNo,
            this.IsActive,
            this.Chk,
            this.HidChk});
            this.gvRateSetting.Location = new System.Drawing.Point(12, 66);
            this.gvRateSetting.Name = "gvRateSetting";
            this.gvRateSetting.Size = new System.Drawing.Size(1179, 190);
            this.gvRateSetting.TabIndex = 9;
            this.gvRateSetting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRateSetting_CellValueChanged);
            this.gvRateSetting.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvRateSetting_CellFormatting);
            this.gvRateSetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvRateSetting_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.SrNo.HeaderText = "Sr";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // DepartmentName
            // 
            this.DepartmentName.DataPropertyName = "DepartmentName";
            this.DepartmentName.HeaderText = "Department Name";
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.ReadOnly = true;
            this.DepartmentName.Width = 250;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "Category Name";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.Width = 250;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 260;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // DepartmentNo
            // 
            this.DepartmentNo.DataPropertyName = "DepartmentNo";
            this.DepartmentNo.HeaderText = "DepartmentNo";
            this.DepartmentNo.Name = "DepartmentNo";
            this.DepartmentNo.ReadOnly = true;
            this.DepartmentNo.Visible = false;
            // 
            // CategoryNo
            // 
            this.CategoryNo.DataPropertyName = "CategoryNo";
            this.CategoryNo.HeaderText = "CategoryNo";
            this.CategoryNo.Name = "CategoryNo";
            this.CategoryNo.ReadOnly = true;
            this.CategoryNo.Visible = false;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.Width = 70;
            // 
            // Chk
            // 
            this.Chk.DataPropertyName = "chk";
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Width = 50;
            // 
            // HidChk
            // 
            this.HidChk.DataPropertyName = "HidChk";
            this.HidChk.HeaderText = "Hidden";
            this.HidChk.Name = "HidChk";
            this.HidChk.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(341, 325);
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
            this.btnCancel.Location = new System.Drawing.Point(244, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyChanges.Location = new System.Drawing.Point(100, 325);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(130, 60);
            this.btnApplyChanges.TabIndex = 11;
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
            // cmbBrandName
            // 
            this.cmbBrandName.FormattingEnabled = true;
            this.cmbBrandName.Location = new System.Drawing.Point(107, 16);
            this.cmbBrandName.Name = "cmbBrandName";
            this.cmbBrandName.Size = new System.Drawing.Size(411, 21);
            this.cmbBrandName.TabIndex = 0;
            this.cmbBrandName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBrandName_KeyDown);
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
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 508;
            this.label2.Text = "Brand Name";
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
            this.ChkSelect.Location = new System.Drawing.Point(973, 270);
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
            this.txtTotProducts.Location = new System.Drawing.Point(878, 268);
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
            this.label5.Location = new System.Drawing.Point(775, 271);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(547, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 515;
            this.label4.Text = "Barcode";
            this.label4.Visible = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(608, 16);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(141, 20);
            this.txtBarcode.TabIndex = 3;
            this.txtBarcode.Visible = false;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(122, 10);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(229, 21);
            this.cmbDepartment.TabIndex = 5001;
            this.cmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartment_KeyDown);
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(513, 11);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(217, 21);
            this.cmbCategory.TabIndex = 5002;
            this.cmbCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 5003;
            this.label6.Text = "Department :";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(423, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 5004;
            this.label7.Text = "Category :";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(12, 325);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 60);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlChange
            // 
            this.pnlChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlChange.BorderColor = System.Drawing.Color.Silver;
            this.pnlChange.BorderRadius = 3;
            this.pnlChange.Controls.Add(this.label6);
            this.pnlChange.Controls.Add(this.cmbDepartment);
            this.pnlChange.Controls.Add(this.cmbCategory);
            this.pnlChange.Controls.Add(this.label7);
            this.pnlChange.Location = new System.Drawing.Point(12, 268);
            this.pnlChange.Name = "pnlChange";
            this.pnlChange.Size = new System.Drawing.Size(744, 42);
            this.pnlChange.TabIndex = 5007;
            this.pnlChange.Visible = false;
            // 
            // ItemChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 434);
            this.Controls.Add(this.pnlChange);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.ChkSelect);
            this.Controls.Add(this.txtTotProducts);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbItemName);
            this.Controls.Add(this.cmbBrandName);
            this.Controls.Add(this.cmbMainGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApplyChanges);
            this.Controls.Add(this.gvRateSetting);
            this.Name = "ItemChange";
            this.Text = "Item  Change";
            this.Load += new System.EventHandler(this.ItemRateChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvRateSetting)).EndInit();
            this.pnlChange.ResumeLayout(false);
            this.pnlChange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvRateSetting;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.ComboBox cmbBrandName;
        private System.Windows.Forms.ComboBox cmbMainGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ChkSelect;
        private System.Windows.Forms.TextBox txtTotProducts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private OMControls.OMBPanel pnlChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HidChk;
    }
}