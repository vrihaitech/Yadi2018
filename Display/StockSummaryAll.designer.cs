namespace Yadi.Display
{
    partial class StockSummaryAll
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
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.chkbarcode = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkItem = new System.Windows.Forms.CheckBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblbarand = new System.Windows.Forms.Label();
            this.cmbsubcategory = new System.Windows.Forms.ComboBox();
            this.cmbcategory = new System.Windows.Forms.ComboBox();
            this.cmbbrand = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.chKsubcategory = new System.Windows.Forms.CheckBox();
            this.chkgroup = new System.Windows.Forms.CheckBox();
            this.chkbrand = new System.Windows.Forms.CheckBox();
            this.chkCmpNm = new System.Windows.Forms.CheckBox();
            this.chkall = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.GridViewDaily = new System.Windows.Forms.DataGridView();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Particulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InwardQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutwardQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDatewise = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(422, 19);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(136, 23);
            this.DTToDate.TabIndex = 109;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(335, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 108;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(109, 19);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(145, 23);
            this.DTPFromDate.TabIndex = 107;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 106;
            this.label1.Text = "From Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 110;
            this.label3.Text = "Type :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.chkbarcode);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbItem);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkItem);
            this.panel1.Controls.Add(this.lblSubCategory);
            this.panel1.Controls.Add(this.lblCategory);
            this.panel1.Controls.Add(this.lblbarand);
            this.panel1.Controls.Add(this.cmbsubcategory);
            this.panel1.Controls.Add(this.cmbcategory);
            this.panel1.Controls.Add(this.cmbbrand);
            this.panel1.Controls.Add(this.cmbCompany);
            this.panel1.Controls.Add(this.lblCompany);
            this.panel1.Controls.Add(this.chKsubcategory);
            this.panel1.Controls.Add(this.chkgroup);
            this.panel1.Controls.Add(this.chkbrand);
            this.panel1.Controls.Add(this.chkCmpNm);
            this.panel1.Controls.Add(this.chkall);
            this.panel1.Location = new System.Drawing.Point(74, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 244);
            this.panel1.TabIndex = 111;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBarCode);
            this.panel2.Location = new System.Drawing.Point(600, 188);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 43);
            this.panel2.TabIndex = 107;
            // 
            // txtBarCode
            // 
            this.txtBarCode.AcceptsReturn = true;
            this.txtBarCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Location = new System.Drawing.Point(9, 8);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(165, 26);
            this.txtBarCode.TabIndex = 139;
            // 
            // chkbarcode
            // 
            this.chkbarcode.AutoSize = true;
            this.chkbarcode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbarcode.Location = new System.Drawing.Point(520, 196);
            this.chkbarcode.Name = "chkbarcode";
            this.chkbarcode.Size = new System.Drawing.Size(82, 18);
            this.chkbarcode.TabIndex = 60;
            this.chkbarcode.Text = "Barcode:";
            this.chkbarcode.UseVisualStyleBackColor = true;
            this.chkbarcode.CheckedChanged += new System.EventHandler(this.chkbarcode_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(573, 196);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 14);
            this.label9.TabIndex = 59;
            // 
            // cmbItem
            // 
            this.cmbItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItem.ForeColor = System.Drawing.Color.Black;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(196, 180);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(312, 28);
            this.cmbItem.TabIndex = 58;
            this.cmbItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbItem_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(137, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 57;
            this.label4.Text = "Item:";
            // 
            // chkItem
            // 
            this.chkItem.AutoSize = true;
            this.chkItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkItem.Location = new System.Drawing.Point(28, 180);
            this.chkItem.Name = "chkItem";
            this.chkItem.Size = new System.Drawing.Size(55, 18);
            this.chkItem.TabIndex = 56;
            this.chkItem.Text = "Item";
            this.chkItem.UseVisualStyleBackColor = true;
            this.chkItem.CheckedChanged += new System.EventHandler(this.chkItem_CheckedChanged);
            // 
            // lblSubCategory
            // 
            this.lblSubCategory.AutoSize = true;
            this.lblSubCategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubCategory.Location = new System.Drawing.Point(193, 142);
            this.lblSubCategory.Name = "lblSubCategory";
            this.lblSubCategory.Size = new System.Drawing.Size(97, 14);
            this.lblSubCategory.TabIndex = 55;
            this.lblSubCategory.Text = "SubCategory:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(193, 106);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(72, 14);
            this.lblCategory.TabIndex = 54;
            this.lblCategory.Text = "Category:";
            // 
            // lblbarand
            // 
            this.lblbarand.AutoSize = true;
            this.lblbarand.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbarand.Location = new System.Drawing.Point(193, 71);
            this.lblbarand.Name = "lblbarand";
            this.lblbarand.Size = new System.Drawing.Size(93, 14);
            this.lblbarand.TabIndex = 53;
            this.lblbarand.Text = "Brand Name:";
            // 
            // cmbsubcategory
            // 
            this.cmbsubcategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbsubcategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsubcategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsubcategory.ForeColor = System.Drawing.Color.Black;
            this.cmbsubcategory.FormattingEnabled = true;
            this.cmbsubcategory.Location = new System.Drawing.Point(290, 142);
            this.cmbsubcategory.Name = "cmbsubcategory";
            this.cmbsubcategory.Size = new System.Drawing.Size(312, 28);
            this.cmbsubcategory.TabIndex = 52;
            this.cmbsubcategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbsubcategory_KeyPress);
            // 
            // cmbcategory
            // 
            this.cmbcategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbcategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcategory.ForeColor = System.Drawing.Color.Black;
            this.cmbcategory.FormattingEnabled = true;
            this.cmbcategory.Location = new System.Drawing.Point(290, 106);
            this.cmbcategory.Name = "cmbcategory";
            this.cmbcategory.Size = new System.Drawing.Size(312, 28);
            this.cmbcategory.TabIndex = 51;
            this.cmbcategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbcategory_KeyPress);
            // 
            // cmbbrand
            // 
            this.cmbbrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbbrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbbrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbrand.ForeColor = System.Drawing.Color.Black;
            this.cmbbrand.FormattingEnabled = true;
            this.cmbbrand.Location = new System.Drawing.Point(290, 66);
            this.cmbbrand.Name = "cmbbrand";
            this.cmbbrand.Size = new System.Drawing.Size(312, 28);
            this.cmbbrand.TabIndex = 50;
            this.cmbbrand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbbrand_KeyPress);
            // 
            // cmbCompany
            // 
            this.cmbCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.ForeColor = System.Drawing.Color.Black;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(290, 30);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(312, 28);
            this.cmbCompany.TabIndex = 49;
            this.cmbCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCompany_KeyPress);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(193, 37);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(91, 13);
            this.lblCompany.TabIndex = 5;
            this.lblCompany.Text = "Mfg.Company :";
            // 
            // chKsubcategory
            // 
            this.chKsubcategory.AutoSize = true;
            this.chKsubcategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chKsubcategory.Location = new System.Drawing.Point(28, 142);
            this.chKsubcategory.Name = "chKsubcategory";
            this.chKsubcategory.Size = new System.Drawing.Size(112, 18);
            this.chKsubcategory.TabIndex = 4;
            this.chKsubcategory.Text = "Sub Category";
            this.chKsubcategory.UseVisualStyleBackColor = true;
            this.chKsubcategory.CheckedChanged += new System.EventHandler(this.chKsubcategory_CheckedChanged);
            // 
            // chkgroup
            // 
            this.chkgroup.AutoSize = true;
            this.chkgroup.Location = new System.Drawing.Point(28, 106);
            this.chkgroup.Name = "chkgroup";
            this.chkgroup.Size = new System.Drawing.Size(68, 17);
            this.chkgroup.TabIndex = 3;
            this.chkgroup.Text = "Category";
            this.chkgroup.UseVisualStyleBackColor = true;
            this.chkgroup.CheckedChanged += new System.EventHandler(this.chkgroup_CheckedChanged);
            // 
            // chkbrand
            // 
            this.chkbrand.AutoSize = true;
            this.chkbrand.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbrand.Location = new System.Drawing.Point(28, 71);
            this.chkbrand.Name = "chkbrand";
            this.chkbrand.Size = new System.Drawing.Size(103, 18);
            this.chkbrand.TabIndex = 2;
            this.chkbrand.Text = "Brand Name";
            this.chkbrand.UseVisualStyleBackColor = true;
            this.chkbrand.CheckedChanged += new System.EventHandler(this.chkbrand_CheckedChanged);
            // 
            // chkCmpNm
            // 
            this.chkCmpNm.AutoSize = true;
            this.chkCmpNm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCmpNm.Location = new System.Drawing.Point(28, 37);
            this.chkCmpNm.Name = "chkCmpNm";
            this.chkCmpNm.Size = new System.Drawing.Size(111, 18);
            this.chkCmpNm.TabIndex = 1;
            this.chkCmpNm.Text = "Mfg.Company";
            this.chkCmpNm.UseVisualStyleBackColor = true;
            this.chkCmpNm.CheckedChanged += new System.EventHandler(this.chkCmpNm_CheckedChanged);
            // 
            // chkall
            // 
            this.chkall.AutoSize = true;
            this.chkall.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkall.Location = new System.Drawing.Point(28, 12);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(40, 18);
            this.chkall.TabIndex = 0;
            this.chkall.Text = "All";
            this.chkall.UseVisualStyleBackColor = true;
            this.chkall.CheckedChanged += new System.EventHandler(this.chkall_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(792, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 113;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(583, 15);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 112;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 321);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(880, 369);
            this.tabControl1.TabIndex = 115;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGridView1);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(872, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stock Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNo,
            this.ItemNumber,
            this.ItemName,
            this.Quantity,
            this.Rate,
            this.Amount});
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(12, 34);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(848, 301);
            this.DataGridView1.TabIndex = 68;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "Item No";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // ItemNumber
            // 
            this.ItemNumber.DataPropertyName = "ItemUserNo";
            this.ItemNumber.HeaderText = "ItemNumber";
            this.ItemNumber.Name = "ItemNumber";
            this.ItemNumber.ReadOnly = true;
            this.ItemNumber.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 350;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 125;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(866, 23);
            this.label6.TabIndex = 67;
            this.label6.Text = "Stock Summary All";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DataGridView2);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(872, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Stock Summary Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DataGridView2
            // 
            this.DataGridView2.AllowUserToAddRows = false;
            this.DataGridView2.AllowUserToDeleteRows = false;
            this.DataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView2.Location = new System.Drawing.Point(13, 52);
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.ReadOnly = true;
            this.DataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowTemplate.Height = 27;
            this.DataGridView2.Size = new System.Drawing.Size(847, 285);
            this.DataGridView2.TabIndex = 70;
            this.DataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView2_CellFormatting);
            this.DataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellClick);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(866, 23);
            this.label7.TabIndex = 68;
            this.label7.Text = "Stock Summary Detail";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.GridViewDaily);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.lblDatewise);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(872, 343);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Voucher Entry Details";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // GridViewDaily
            // 
            this.GridViewDaily.AllowUserToAddRows = false;
            this.GridViewDaily.AllowUserToDeleteRows = false;
            this.GridViewDaily.AllowUserToResizeColumns = false;
            this.GridViewDaily.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridViewDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VoucherDate,
            this.VoucherSrNo,
            this.Particulars,
            this.VoucherTypeName,
            this.VoucherNo,
            this.InwardQuantity,
            this.OutwardQuantity});
            this.GridViewDaily.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewDaily.Location = new System.Drawing.Point(14, 33);
            this.GridViewDaily.Name = "GridViewDaily";
            this.GridViewDaily.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GridViewDaily.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowTemplate.Height = 27;
            this.GridViewDaily.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewDaily.Size = new System.Drawing.Size(845, 293);
            this.GridViewDaily.TabIndex = 59;
            this.GridViewDaily.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewDaily_CellFormatting);
            // 
            // VoucherDate
            // 
            this.VoucherDate.DataPropertyName = "VoucherDate";
            this.VoucherDate.HeaderText = "VoucherDate";
            this.VoucherDate.Name = "VoucherDate";
            this.VoucherDate.ReadOnly = true;
            this.VoucherDate.Width = 120;
            // 
            // VoucherSrNo
            // 
            this.VoucherSrNo.DataPropertyName = "VoucherSrNo";
            this.VoucherSrNo.HeaderText = "Vch No";
            this.VoucherSrNo.Name = "VoucherSrNo";
            this.VoucherSrNo.ReadOnly = true;
            this.VoucherSrNo.Width = 50;
            // 
            // Particulars
            // 
            this.Particulars.DataPropertyName = "Particulars";
            this.Particulars.HeaderText = "Particulars";
            this.Particulars.Name = "Particulars";
            this.Particulars.ReadOnly = true;
            this.Particulars.Width = 200;
            // 
            // VoucherTypeName
            // 
            this.VoucherTypeName.DataPropertyName = "VoucherTypeName";
            this.VoucherTypeName.HeaderText = "VoucherType Name";
            this.VoucherTypeName.Name = "VoucherTypeName";
            this.VoucherTypeName.ReadOnly = true;
            this.VoucherTypeName.Width = 130;
            // 
            // VoucherNo
            // 
            this.VoucherNo.DataPropertyName = "VoucherNo";
            this.VoucherNo.HeaderText = "VoucherNo";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.ReadOnly = true;
            this.VoucherNo.Visible = false;
            // 
            // InwardQuantity
            // 
            this.InwardQuantity.DataPropertyName = "Inward Quantity";
            this.InwardQuantity.HeaderText = "Inward Qty";
            this.InwardQuantity.Name = "InwardQuantity";
            this.InwardQuantity.ReadOnly = true;
            // 
            // OutwardQuantity
            // 
            this.OutwardQuantity.DataPropertyName = "Outward Quantity";
            this.OutwardQuantity.HeaderText = "Outward Qty";
            this.OutwardQuantity.Name = "OutwardQuantity";
            this.OutwardQuantity.ReadOnly = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(730, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 58;
            this.label8.Text = "label8";
            // 
            // lblDatewise
            // 
            this.lblDatewise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblDatewise.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDatewise.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatewise.ForeColor = System.Drawing.Color.White;
            this.lblDatewise.Location = new System.Drawing.Point(3, 3);
            this.lblDatewise.Name = "lblDatewise";
            this.lblDatewise.Size = new System.Drawing.Size(866, 27);
            this.lblDatewise.TabIndex = 57;
            this.lblDatewise.Text = " Voucher Entry Details";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(690, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 108;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StockSummaryAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 702);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.BtnShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DTToDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DTPFromDate);
            this.Controls.Add(this.label1);
            this.Name = "StockSummaryAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Summary All";
            this.Load += new System.EventHandler(this.StockSummuryAll_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkall;
        private System.Windows.Forms.CheckBox chkCmpNm;
        private System.Windows.Forms.CheckBox chkbrand;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.CheckBox chKsubcategory;
        private System.Windows.Forms.CheckBox chkgroup;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblbarand;
        private System.Windows.Forms.ComboBox cmbsubcategory;
        private System.Windows.Forms.ComboBox cmbcategory;
        private System.Windows.Forms.ComboBox cmbbrand;
        private System.Windows.Forms.ComboBox cmbCompany;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DataGridView2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.DataGridView GridViewDaily;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Particulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InwardQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutwardQuantity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDatewise;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.CheckBox chkbarcode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBarCode;
        internal System.Windows.Forms.Button btnExit;
    }
}