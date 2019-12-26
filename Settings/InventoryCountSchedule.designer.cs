namespace Yadi.Settings
{
    partial class InventoryCountSchedule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblMsg = new OMControls.OMLabel();
            this.pnlType = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.DtpDefaultValue = new System.Windows.Forms.DateTimePicker();
            this.cmbStockCountType = new System.Windows.Forms.ComboBox();
            this.cmbDefault = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvDetails = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ChkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.rbDeActive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.cmbGroupNo2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDepartmentName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategoryName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.pnlType);
            this.pnlMain.Controls.Add(this.gvDetails);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(980, 534);
            this.pnlMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(243, 488);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 37);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(128, 488);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 37);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(15, 488);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 37);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Apply Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.lblMsg.Location = new System.Drawing.Point(243, 199);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 522;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.btnApply);
            this.pnlType.Controls.Add(this.DtpDefaultValue);
            this.pnlType.Controls.Add(this.cmbStockCountType);
            this.pnlType.Controls.Add(this.cmbDefault);
            this.pnlType.Controls.Add(this.label1);
            this.pnlType.Location = new System.Drawing.Point(15, 447);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(945, 39);
            this.pnlType.TabIndex = 11;
            this.pnlType.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(489, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 27);
            this.btnApply.TabIndex = 9;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // DtpDefaultValue
            // 
            this.DtpDefaultValue.Location = new System.Drawing.Point(312, 11);
            this.DtpDefaultValue.Name = "DtpDefaultValue";
            this.DtpDefaultValue.Size = new System.Drawing.Size(126, 20);
            this.DtpDefaultValue.TabIndex = 8;
            this.DtpDefaultValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDefaultValue_KeyDown);
            // 
            // cmbStockCountType
            // 
            this.cmbStockCountType.FormattingEnabled = true;
            this.cmbStockCountType.Location = new System.Drawing.Point(143, 12);
            this.cmbStockCountType.Name = "cmbStockCountType";
            this.cmbStockCountType.Size = new System.Drawing.Size(129, 21);
            this.cmbStockCountType.TabIndex = 7;
            this.cmbStockCountType.SelectedIndexChanged += new System.EventHandler(this.cmbStockCountType_SelectedIndexChanged);
            this.cmbStockCountType.Leave += new System.EventHandler(this.cmbStockCountType_Leave);
            this.cmbStockCountType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbStockCountType_KeyDown);
            // 
            // cmbDefault
            // 
            this.cmbDefault.FormattingEnabled = true;
            this.cmbDefault.Location = new System.Drawing.Point(310, 11);
            this.cmbDefault.Name = "cmbDefault";
            this.cmbDefault.Size = new System.Drawing.Size(129, 21);
            this.cmbDefault.TabIndex = 8;
            this.cmbDefault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDefault_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stock Count Type :";
            // 
            // gvDetails
            // 
            this.gvDetails.AllowUserToAddRows = false;
            this.gvDetails.AllowUserToDeleteRows = false;
            this.gvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.DepartmentName,
            this.CategoryName,
            this.ItemName,
            this.UOM,
            this.Type,
            this.Value,
            this.DefaultValue,
            this.CountTypeNo,
            this.DepartmentNo,
            this.CategoryNo,
            this.ItemNo,
            this.PkSrNo,
            this.Chk,
            this.ChkSelect});
            this.gvDetails.Location = new System.Drawing.Point(15, 119);
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.Size = new System.Drawing.Size(945, 324);
            this.gvDetails.TabIndex = 6;
            this.gvDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvDetails_CellFormatting);
            this.gvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvDetails_KeyDown);
            this.gvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDetails_CellContentClick);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle4;
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
            this.DepartmentName.Width = 190;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "CategoryName";
            this.CategoryName.HeaderText = "Category Name";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.Width = 190;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 180;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 60;
            // 
            // Type
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Type.DefaultCellStyle = dataGridViewCellStyle5;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Value
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Value.DefaultCellStyle = dataGridViewCellStyle6;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // DefaultValue
            // 
            this.DefaultValue.HeaderText = "DefaultValue";
            this.DefaultValue.Name = "DefaultValue";
            this.DefaultValue.ReadOnly = true;
            this.DefaultValue.Visible = false;
            this.DefaultValue.Width = 60;
            // 
            // CountTypeNo
            // 
            this.CountTypeNo.HeaderText = "CountTypeNo";
            this.CountTypeNo.Name = "CountTypeNo";
            this.CountTypeNo.ReadOnly = true;
            this.CountTypeNo.Visible = false;
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
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.ReadOnly = true;
            this.PkSrNo.Visible = false;
            // 
            // Chk
            // 
            this.Chk.DataPropertyName = "chk";
            this.Chk.FalseValue = "False";
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.TrueValue = "True";
            this.Chk.Width = 50;
            // 
            // ChkSelect
            // 
            this.ChkSelect.FalseValue = "False";
            this.ChkSelect.HeaderText = "ChkSelect";
            this.ChkSelect.Name = "ChkSelect";
            this.ChkSelect.ReadOnly = true;
            this.ChkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ChkSelect.TrueValue = "True";
            this.ChkSelect.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSelectAll);
            this.panel1.Controls.Add(this.rbDeActive);
            this.panel1.Controls.Add(this.rbActive);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtBarcode);
            this.panel1.Controls.Add(this.cmbGroupNo2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbDepartmentName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbCategoryName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(15, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 87);
            this.panel1.TabIndex = 0;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(815, 64);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 523;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // rbDeActive
            // 
            this.rbDeActive.AutoSize = true;
            this.rbDeActive.Location = new System.Drawing.Point(226, 68);
            this.rbDeActive.Name = "rbDeActive";
            this.rbDeActive.Size = new System.Drawing.Size(87, 17);
            this.rbDeActive.TabIndex = 5;
            this.rbDeActive.Text = "DeActive(F6)";
            this.rbDeActive.UseVisualStyleBackColor = true;
            this.rbDeActive.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Location = new System.Drawing.Point(111, 68);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(73, 17);
            this.rbActive.TabIndex = 4;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active(F5)";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(369, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 519;
            this.label7.Text = "Barcode :";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(474, 12);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(200, 20);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // cmbGroupNo2
            // 
            this.cmbGroupNo2.FormattingEnabled = true;
            this.cmbGroupNo2.Location = new System.Drawing.Point(113, 14);
            this.cmbGroupNo2.Name = "cmbGroupNo2";
            this.cmbGroupNo2.Size = new System.Drawing.Size(200, 21);
            this.cmbGroupNo2.TabIndex = 0;
            this.cmbGroupNo2.Leave += new System.EventHandler(this.cmbGroupNo2_Leave);
            this.cmbGroupNo2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGroupNo2_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 518;
            this.label5.Text = "Department :";
            // 
            // cmbDepartmentName
            // 
            this.cmbDepartmentName.FormattingEnabled = true;
            this.cmbDepartmentName.Location = new System.Drawing.Point(113, 41);
            this.cmbDepartmentName.Name = "cmbDepartmentName";
            this.cmbDepartmentName.Size = new System.Drawing.Size(200, 21);
            this.cmbDepartmentName.TabIndex = 2;
            this.cmbDepartmentName.Leave += new System.EventHandler(this.cmbDepartmentName_Leave);
            this.cmbDepartmentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartmentName_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 517;
            this.label4.Text = "Category :";
            // 
            // cmbCategoryName
            // 
            this.cmbCategoryName.FormattingEnabled = true;
            this.cmbCategoryName.Location = new System.Drawing.Point(474, 41);
            this.cmbCategoryName.Name = "cmbCategoryName";
            this.cmbCategoryName.Size = new System.Drawing.Size(200, 21);
            this.cmbCategoryName.TabIndex = 3;
            this.cmbCategoryName.Leave += new System.EventHandler(this.cmbCategoryName_Leave);
            this.cmbCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategoryName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 516;
            this.label3.Text = "Brand Name :";
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // InventoryCountSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 569);
            this.Controls.Add(this.pnlMain);
            this.Name = "InventoryCountSchedule";
            this.Text = "Inventory Count Schedule";
            this.Load += new System.EventHandler(this.InventoryCountSchedule_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.ComboBox cmbGroupNo2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDepartmentName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategoryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvDetails;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.ComboBox cmbStockCountType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDeActive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.ErrorProvider EP;
        private OMControls.OMLabel lblMsg;
        private System.Windows.Forms.DateTimePicker DtpDefaultValue;
        private System.Windows.Forms.ComboBox cmbDefault;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkSelect;
        private System.Windows.Forms.CheckBox chkSelectAll;
    }
}