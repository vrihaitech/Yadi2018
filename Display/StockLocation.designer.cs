namespace Yadi.Display
{
    partial class StockLocation
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkSelectAllItem = new System.Windows.Forms.CheckBox();
            this.btnItemNameCancel = new System.Windows.Forms.Button();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgItemName = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selectItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLocationOk = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMonthDtls = new System.Windows.Forms.Label();
            this.dgLocation = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GodownNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMainForm = new OMControls.OMBPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdActiveDeActive = new System.Windows.Forms.RadioButton();
            this.rdDeActive = new System.Windows.Forms.RadioButton();
            this.rdActive = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnShow = new System.Windows.Forms.Button();
            this.rbProductWise = new System.Windows.Forms.RadioButton();
            this.rbLocationWise = new System.Windows.Forms.RadioButton();
            this.txtToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlLocation = new OMControls.OMBPanel();
            this.pnlProduct = new OMControls.OMBPanel();
            this.lblFirmName = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemName)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocation)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkSelectAllItem);
            this.panel2.Controls.Add(this.btnItemNameCancel);
            this.panel2.Controls.Add(this.btnShowReport);
            this.panel2.Controls.Add(this.btnExcel);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 52);
            this.panel2.TabIndex = 513;
            // 
            // chkSelectAllItem
            // 
            this.chkSelectAllItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectAllItem.AutoSize = true;
            this.chkSelectAllItem.Location = new System.Drawing.Point(517, 7);
            this.chkSelectAllItem.Name = "chkSelectAllItem";
            this.chkSelectAllItem.Size = new System.Drawing.Size(91, 17);
            this.chkSelectAllItem.TabIndex = 505;
            this.chkSelectAllItem.Text = "Select All (F2)";
            this.chkSelectAllItem.UseVisualStyleBackColor = true;
            this.chkSelectAllItem.CheckedChanged += new System.EventHandler(this.chkItemNameAll_CheckedChanged);
            // 
            // btnItemNameCancel
            // 
            this.btnItemNameCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemNameCancel.Location = new System.Drawing.Point(1, 13);
            this.btnItemNameCancel.Name = "btnItemNameCancel";
            this.btnItemNameCancel.Size = new System.Drawing.Size(80, 24);
            this.btnItemNameCancel.TabIndex = 2;
            this.btnItemNameCancel.Text = "Back";
            this.btnItemNameCancel.UseVisualStyleBackColor = true;
            this.btnItemNameCancel.Click += new System.EventHandler(this.btnItemNameCancel_Click);
            // 
            // btnShowReport
            // 
            this.btnShowReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowReport.Location = new System.Drawing.Point(87, 13);
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.Size = new System.Drawing.Size(125, 24);
            this.btnShowReport.TabIndex = 506;
            this.btnShowReport.Text = "&Show Report";
            this.btnShowReport.UseVisualStyleBackColor = true;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExcel.Location = new System.Drawing.Point(304, 11);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(76, 28);
            this.btnExcel.TabIndex = 510;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(218, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 24);
            this.btnExit.TabIndex = 508;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtItemName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(4, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 28);
            this.panel1.TabIndex = 512;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(124, 5);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(382, 20);
            this.txtItemName.TabIndex = 509;
            this.txtItemName.TextChanged += new System.EventHandler(this.txtItemName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 501;
            this.label4.Text = "Select Item Name :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(616, 26);
            this.label1.TabIndex = 511;
            this.label1.Text = "Select Item";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgItemName
            // 
            this.dgItemName.AllowUserToAddRows = false;
            this.dgItemName.AllowUserToDeleteRows = false;
            this.dgItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgItemName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.selectItem,
            this.ItemNo});
            this.dgItemName.Location = new System.Drawing.Point(3, 62);
            this.dgItemName.Name = "dgItemName";
            this.dgItemName.Size = new System.Drawing.Size(613, 389);
            this.dgItemName.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 500;
            // 
            // selectItem
            // 
            this.selectItem.DataPropertyName = "chkCheckItem";
            this.selectItem.HeaderText = "Select";
            this.selectItem.Name = "selectItem";
            this.selectItem.Width = 80;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLocationOk);
            this.panel3.Controls.Add(this.chkSelectAll);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 432);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(616, 34);
            this.panel3.TabIndex = 511;
            // 
            // btnLocationOk
            // 
            this.btnLocationOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLocationOk.Location = new System.Drawing.Point(3, 7);
            this.btnLocationOk.Name = "btnLocationOk";
            this.btnLocationOk.Size = new System.Drawing.Size(80, 24);
            this.btnLocationOk.TabIndex = 6;
            this.btnLocationOk.Text = "Ok";
            this.btnLocationOk.UseVisualStyleBackColor = true;
            this.btnLocationOk.Click += new System.EventHandler(this.btnLocationOk_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(522, 2);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(91, 17);
            this.chkSelectAll.TabIndex = 502;
            this.chkSelectAll.Text = "Select All (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(93, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 24);
            this.button1.TabIndex = 509;
            this.button1.Text = "E&xit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblMonthDtls
            // 
            this.lblMonthDtls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblMonthDtls.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthDtls.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthDtls.ForeColor = System.Drawing.Color.White;
            this.lblMonthDtls.Location = new System.Drawing.Point(0, 0);
            this.lblMonthDtls.Name = "lblMonthDtls";
            this.lblMonthDtls.Size = new System.Drawing.Size(616, 26);
            this.lblMonthDtls.TabIndex = 510;
            this.lblMonthDtls.Text = "Select Location";
            this.lblMonthDtls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgLocation
            // 
            this.dgLocation.AllowUserToAddRows = false;
            this.dgLocation.AllowUserToDeleteRows = false;
            this.dgLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLocation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.LocSelect,
            this.GodownNo});
            this.dgLocation.Location = new System.Drawing.Point(2, 32);
            this.dgLocation.Name = "dgLocation";
            this.dgLocation.Size = new System.Drawing.Size(611, 394);
            this.dgLocation.TabIndex = 4;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "LocationName";
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 400;
            // 
            // LocSelect
            // 
            this.LocSelect.DataPropertyName = "chkCheck";
            this.LocSelect.HeaderText = "Select";
            this.LocSelect.Name = "LocSelect";
            this.LocSelect.Width = 130;
            // 
            // GodownNo
            // 
            this.GodownNo.DataPropertyName = "GodownNo";
            this.GodownNo.HeaderText = "GodownNo";
            this.GodownNo.Name = "GodownNo";
            this.GodownNo.Visible = false;
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.Controls.Add(this.lblFirmName);
            this.pnlMainForm.Controls.Add(this.groupBox1);
            this.pnlMainForm.Controls.Add(this.panel4);
            this.pnlMainForm.Controls.Add(this.txtToDate);
            this.pnlMainForm.Controls.Add(this.label3);
            this.pnlMainForm.Controls.Add(this.txtFromDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.pnlLocation);
            this.pnlMainForm.Controls.Add(this.pnlProduct);
            this.pnlMainForm.Location = new System.Drawing.Point(0, 1);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(636, 598);
            this.pnlMainForm.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdActiveDeActive);
            this.groupBox1.Controls.Add(this.rdDeActive);
            this.groupBox1.Controls.Add(this.rdActive);
            this.groupBox1.Location = new System.Drawing.Point(425, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 35);
            this.groupBox1.TabIndex = 10006;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items List";
            // 
            // rdActiveDeActive
            // 
            this.rdActiveDeActive.AutoSize = true;
            this.rdActiveDeActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdActiveDeActive.Location = new System.Drawing.Point(153, 14);
            this.rdActiveDeActive.Name = "rdActiveDeActive";
            this.rdActiveDeActive.Size = new System.Drawing.Size(41, 20);
            this.rdActiveDeActive.TabIndex = 6;
            this.rdActiveDeActive.Text = "All";
            this.rdActiveDeActive.UseVisualStyleBackColor = true;
            this.rdActiveDeActive.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rdDeActive
            // 
            this.rdDeActive.AutoSize = true;
            this.rdDeActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDeActive.Location = new System.Drawing.Point(71, 13);
            this.rdDeActive.Name = "rdDeActive";
            this.rdDeActive.Size = new System.Drawing.Size(85, 20);
            this.rdDeActive.TabIndex = 5;
            this.rdDeActive.Text = "DeActive";
            this.rdDeActive.UseVisualStyleBackColor = true;
            this.rdDeActive.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rdActive
            // 
            this.rdActive.AutoSize = true;
            this.rdActive.Checked = true;
            this.rdActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdActive.Location = new System.Drawing.Point(5, 12);
            this.rdActive.Name = "rdActive";
            this.rdActive.Size = new System.Drawing.Size(68, 20);
            this.rdActive.TabIndex = 4;
            this.rdActive.TabStop = true;
            this.rdActive.Text = "Active";
            this.rdActive.UseVisualStyleBackColor = true;
            this.rdActive.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnShow);
            this.panel4.Controls.Add(this.rbProductWise);
            this.panel4.Controls.Add(this.rbLocationWise);
            this.panel4.Location = new System.Drawing.Point(11, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(620, 34);
            this.panel4.TabIndex = 5;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShow.Location = new System.Drawing.Point(538, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(80, 24);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // rbProductWise
            // 
            this.rbProductWise.AutoSize = true;
            this.rbProductWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProductWise.Location = new System.Drawing.Point(274, 10);
            this.rbProductWise.Name = "rbProductWise";
            this.rbProductWise.Size = new System.Drawing.Size(265, 17);
            this.rbProductWise.TabIndex = 5;
            this.rbProductWise.Text = "Product Wise Location Wise Stock Report";
            this.rbProductWise.UseVisualStyleBackColor = true;
            this.rbProductWise.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbLocationWise
            // 
            this.rbLocationWise.AutoSize = true;
            this.rbLocationWise.Checked = true;
            this.rbLocationWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLocationWise.Location = new System.Drawing.Point(8, 10);
            this.rbLocationWise.Name = "rbLocationWise";
            this.rbLocationWise.Size = new System.Drawing.Size(265, 17);
            this.rbLocationWise.TabIndex = 5;
            this.rbLocationWise.TabStop = true;
            this.rbLocationWise.Text = "Location Wise Product Wise Stock Report";
            this.rbLocationWise.UseVisualStyleBackColor = true;
            this.rbLocationWise.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // txtToDate
            // 
            this.txtToDate.Location = new System.Drawing.Point(290, 18);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(126, 20);
            this.txtToDate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "To Date :";
            // 
            // txtFromDate
            // 
            this.txtFromDate.Location = new System.Drawing.Point(91, 18);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(122, 20);
            this.txtFromDate.TabIndex = 0;
            this.txtFromDate.ValueChanged += new System.EventHandler(this.txtFromDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "From Date :";
            // 
            // pnlLocation
            // 
            this.pnlLocation.BorderColor = System.Drawing.Color.Gray;
            this.pnlLocation.BorderRadius = 3;
            this.pnlLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocation.Controls.Add(this.lblMonthDtls);
            this.pnlLocation.Controls.Add(this.panel3);
            this.pnlLocation.Controls.Add(this.dgLocation);
            this.pnlLocation.Location = new System.Drawing.Point(11, 82);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(618, 468);
            this.pnlLocation.TabIndex = 512;
            this.pnlLocation.Visible = false;
            // 
            // pnlProduct
            // 
            this.pnlProduct.BorderColor = System.Drawing.Color.Gray;
            this.pnlProduct.BorderRadius = 3;
            this.pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProduct.Controls.Add(this.panel2);
            this.pnlProduct.Controls.Add(this.label1);
            this.pnlProduct.Controls.Add(this.panel1);
            this.pnlProduct.Controls.Add(this.dgItemName);
            this.pnlProduct.Location = new System.Drawing.Point(7, 82);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(618, 511);
            this.pnlProduct.TabIndex = 514;
            this.pnlProduct.Visible = false;
            // 
            // lblFirmName
            // 
            this.lblFirmName.AutoSize = true;
            this.lblFirmName.Location = new System.Drawing.Point(16, 6);
            this.lblFirmName.Name = "lblFirmName";
            this.lblFirmName.Size = new System.Drawing.Size(35, 13);
            this.lblFirmName.TabIndex = 6;
            this.lblFirmName.Text = "label5";
            this.lblFirmName.Visible = false;
            // 
            // StockLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 611);
            this.Controls.Add(this.pnlMainForm);
            this.Name = "StockLocation";
            this.Text = "Locationwise Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemName)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocation)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLocationOk;
        private System.Windows.Forms.DataGridView dgLocation;
        private System.Windows.Forms.Button btnItemNameCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgItemName;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckBox chkSelectAllItem;
        private System.Windows.Forms.Button btnShowReport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Button btnExcel;
        private OMControls.OMBPanel pnlMainForm;
        private System.Windows.Forms.DateTimePicker txtToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txtFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMonthDtls;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbProductWise;
        private System.Windows.Forms.RadioButton rbLocationWise;
        private OMControls.OMBPanel pnlLocation;
        private OMControls.OMBPanel pnlProduct;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selectItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdActiveDeActive;
        private System.Windows.Forms.RadioButton rdDeActive;
        private System.Windows.Forms.RadioButton rdActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LocSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn GodownNo;
        private System.Windows.Forms.Label lblFirmName;


    }
}