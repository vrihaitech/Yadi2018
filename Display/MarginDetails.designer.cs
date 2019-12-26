 namespace Yadi.Display
{
     partial class MarginDetails
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbManu = new System.Windows.Forms.ComboBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbBrand = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbBrandName = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbDepart = new System.Windows.Forms.ComboBox();
            this.pnlPartyDetails = new System.Windows.Forms.Panel();
            this.pnlSelectType = new System.Windows.Forms.Panel();
            this.btnBoth = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.SrNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShowItem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlPartyDetails.SuspendLayout();
            this.pnlSelectType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.lbBrand);
            this.pnlMain.Controls.Add(this.cmbCategory);
            this.pnlMain.Controls.Add(this.cmbBrandName);
            this.pnlMain.Controls.Add(this.lblCategory);
            this.pnlMain.Controls.Add(this.cmbDepart);
            this.pnlMain.Controls.Add(this.pnlPartyDetails);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(6, 13);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(951, 609);
            this.pnlMain.TabIndex = 75;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(867, 29);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 27);
            this.btnExit.TabIndex = 5023;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbManu);
            this.panel1.Controls.Add(this.btnAll);
            this.panel1.Location = new System.Drawing.Point(513, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 49);
            this.panel1.TabIndex = 5022;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 5024;
            this.label4.Text = "Manufacturer";
            // 
            // cmbManu
            // 
            this.cmbManu.FormattingEnabled = true;
            this.cmbManu.Location = new System.Drawing.Point(98, 13);
            this.cmbManu.Name = "cmbManu";
            this.cmbManu.Size = new System.Drawing.Size(170, 21);
            this.cmbManu.TabIndex = 5025;
            this.cmbManu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbManu_KeyDown);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(281, 9);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(123, 27);
            this.btnAll.TabIndex = 5023;
            this.btnAll.Text = "All Items";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5014;
            this.label3.Text = "Department";
            // 
            // lbBrand
            // 
            this.lbBrand.AutoSize = true;
            this.lbBrand.BackColor = System.Drawing.Color.Transparent;
            this.lbBrand.Location = new System.Drawing.Point(522, 36);
            this.lbBrand.Name = "lbBrand";
            this.lbBrand.Size = new System.Drawing.Size(66, 13);
            this.lbBrand.TabIndex = 5017;
            this.lbBrand.Text = "Brand Name";
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(338, 36);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(164, 21);
            this.cmbCategory.TabIndex = 5016;
            this.cmbCategory.Leave += new System.EventHandler(this.cmbCategory_Leave);
            this.cmbCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyDown);
            // 
            // cmbBrandName
            // 
            this.cmbBrandName.FormattingEnabled = true;
            this.cmbBrandName.Location = new System.Drawing.Point(609, 32);
            this.cmbBrandName.Name = "cmbBrandName";
            this.cmbBrandName.Size = new System.Drawing.Size(159, 21);
            this.cmbBrandName.TabIndex = 5018;
            this.cmbBrandName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBrandName_KeyDown);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblCategory.Location = new System.Drawing.Point(268, 40);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 5015;
            this.lblCategory.Text = "Category";
            // 
            // cmbDepart
            // 
            this.cmbDepart.FormattingEnabled = true;
            this.cmbDepart.Location = new System.Drawing.Point(88, 37);
            this.cmbDepart.Name = "cmbDepart";
            this.cmbDepart.Size = new System.Drawing.Size(163, 21);
            this.cmbDepart.TabIndex = 5013;
            this.cmbDepart.Leave += new System.EventHandler(this.cmbDepart_Leave);
            this.cmbDepart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepart_KeyDown);
            // 
            // pnlPartyDetails
            // 
            this.pnlPartyDetails.Controls.Add(this.pnlSelectType);
            this.pnlPartyDetails.Controls.Add(this.BtnCancel);
            this.pnlPartyDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPartyDetails.Controls.Add(this.gvItem);
            this.pnlPartyDetails.Controls.Add(this.BtnShowItem);
            this.pnlPartyDetails.Location = new System.Drawing.Point(19, 125);
            this.pnlPartyDetails.Name = "pnlPartyDetails";
            this.pnlPartyDetails.Size = new System.Drawing.Size(503, 455);
            this.pnlPartyDetails.TabIndex = 75;
            this.pnlPartyDetails.Visible = false;
            // 
            // pnlSelectType
            // 
            this.pnlSelectType.Controls.Add(this.btnBoth);
            this.pnlSelectType.Controls.Add(this.btnClose);
            this.pnlSelectType.Controls.Add(this.btnOpen);
            this.pnlSelectType.Location = new System.Drawing.Point(13, 477);
            this.pnlSelectType.Name = "pnlSelectType";
            this.pnlSelectType.Size = new System.Drawing.Size(391, 36);
            this.pnlSelectType.TabIndex = 79;
            this.pnlSelectType.Visible = false;
            // 
            // btnBoth
            // 
            this.btnBoth.Location = new System.Drawing.Point(197, 3);
            this.btnBoth.Name = "btnBoth";
            this.btnBoth.Size = new System.Drawing.Size(93, 27);
            this.btnBoth.TabIndex = 78;
            this.btnBoth.Text = "Both";
            this.btnBoth.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(98, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 27);
            this.btnClose.TabIndex = 77;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(93, 27);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(114, 420);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(93, 27);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // chkPartySelectAll
            // 
            this.chkPartySelectAll.AutoSize = true;
            this.chkPartySelectAll.Location = new System.Drawing.Point(254, 420);
            this.chkPartySelectAll.Name = "chkPartySelectAll";
            this.chkPartySelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkPartySelectAll.TabIndex = 3;
            this.chkPartySelectAll.Text = "SelectAll (F2)";
            this.chkPartySelectAll.UseVisualStyleBackColor = true;
            this.chkPartySelectAll.CheckedChanged += new System.EventHandler(this.chkPartySelectAll_CheckedChanged);
            // 
            // gvItem
            // 
            this.gvItem.AllowUserToAddRows = false;
            this.gvItem.AllowUserToDeleteRows = false;
            this.gvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNO,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.Chk1});
            this.gvItem.Location = new System.Drawing.Point(13, 12);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(472, 395);
            this.gvItem.TabIndex = 4;
            // 
            // SrNO
            // 
            this.SrNO.DataPropertyName = "SrNo";
            this.SrNO.HeaderText = "SrNO";
            this.SrNO.Name = "SrNO";
            this.SrNO.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ItemName";
            this.dataGridViewTextBoxColumn2.HeaderText = "ItemName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ItemNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "ItemNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // Chk1
            // 
            this.Chk1.DataPropertyName = "chk";
            this.Chk1.HeaderText = "Select";
            this.Chk1.Name = "Chk1";
            this.Chk1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk1.Width = 80;
            // 
            // BtnShowItem
            // 
            this.BtnShowItem.Location = new System.Drawing.Point(15, 420);
            this.BtnShowItem.Name = "BtnShowItem";
            this.BtnShowItem.Size = new System.Drawing.Size(93, 27);
            this.BtnShowItem.TabIndex = 5;
            this.BtnShowItem.Text = "Show";
            this.BtnShowItem.UseVisualStyleBackColor = false;
            this.BtnShowItem.Click += new System.EventHandler(this.BtnShowItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(791, 29);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(66, 27);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(339, 8);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(94, 20);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            this.DTToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTToDate_KeyDown);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(97, 8);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(93, 20);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(250, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // MarginDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 634);
            this.Controls.Add(this.pnlMain);
            this.Name = "MarginDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Margin Details";
            this.Load += new System.EventHandler(this.StockSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlPartyDetails.ResumeLayout(false);
            this.pnlPartyDetails.PerformLayout();
            this.pnlSelectType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        internal System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Panel pnlPartyDetails;
        private System.Windows.Forms.CheckBox chkPartySelectAll;
        private System.Windows.Forms.DataGridView gvItem;
        internal System.Windows.Forms.Button BtnShowItem;
        internal System.Windows.Forms.Button BtnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel pnlSelectType;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBoth;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbBrand;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbBrandName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbDepart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbManu;
        internal System.Windows.Forms.Button btnAll;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
    }
}