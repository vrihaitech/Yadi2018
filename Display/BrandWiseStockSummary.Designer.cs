namespace Yadi.Display
{
    partial class BrandWiseStockSummary
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
            this.pnlMain = new OMControls.OMBPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdActiveDeActive = new System.Windows.Forms.RadioButton();
            this.rdDeActive = new System.Windows.Forms.RadioButton();
            this.rdActive = new System.Windows.Forms.RadioButton();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgItem = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShow = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnItmShow = new System.Windows.Forms.Button();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InwardQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutwardQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClsQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFirmName = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlItem.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblFirmName);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.pnlItem);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnItmShow);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.dgDetails);
            this.pnlMain.Location = new System.Drawing.Point(8, 6);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(841, 573);
            this.pnlMain.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(840, 26);
            this.label3.TabIndex = 10011;
            this.label3.Text = "Bulk Break Stock";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlItem
            // 
            this.pnlItem.Controls.Add(this.groupBox1);
            this.pnlItem.Controls.Add(this.chkSelectAll);
            this.pnlItem.Controls.Add(this.dgItem);
            this.pnlItem.Controls.Add(this.BtnShow);
            this.pnlItem.Location = new System.Drawing.Point(9, 79);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(452, 474);
            this.pnlItem.TabIndex = 75;
            this.pnlItem.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdActiveDeActive);
            this.groupBox1.Controls.Add(this.rdDeActive);
            this.groupBox1.Controls.Add(this.rdActive);
            this.groupBox1.Location = new System.Drawing.Point(42, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 35);
            this.groupBox1.TabIndex = 10005;
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
            this.rdActiveDeActive.CheckedChanged += new System.EventHandler(this.rdActive_CheckedChanged);
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
            this.rdDeActive.CheckedChanged += new System.EventHandler(this.rdActive_CheckedChanged);
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
            this.rdActive.CheckedChanged += new System.EventHandler(this.rdActive_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(316, 6);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(85, 17);
            this.chkSelectAll.TabIndex = 72;
            this.chkSelectAll.Text = "SelectAll(F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgItem
            // 
            this.dgItem.AllowUserToAddRows = false;
            this.dgItem.AllowUserToDeleteRows = false;
            this.dgItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteno,
            this.Item,
            this.chkSelect});
            this.dgItem.Location = new System.Drawing.Point(25, 39);
            this.dgItem.Name = "dgItem";
            this.dgItem.Size = new System.Drawing.Size(401, 399);
            this.dgItem.TabIndex = 7;
            // 
            // Iteno
            // 
            this.Iteno.DataPropertyName = "ItemNo";
            this.Iteno.HeaderText = "ItemNo";
            this.Iteno.Name = "Iteno";
            this.Iteno.Visible = false;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "ItemName";
            this.Item.HeaderText = "ItemName";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 250;
            // 
            // chkSelect
            // 
            this.chkSelect.DataPropertyName = "chkSelect";
            this.chkSelect.FalseValue = "false";
            this.chkSelect.HeaderText = "Select";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkSelect.TrueValue = "true";
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(25, 442);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 4;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(627, 39);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 10008;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 10006;
            this.label1.Text = "From Date :";
            // 
            // BtnItmShow
            // 
            this.BtnItmShow.Location = new System.Drawing.Point(520, 39);
            this.BtnItmShow.Name = "BtnItmShow";
            this.BtnItmShow.Size = new System.Drawing.Size(93, 27);
            this.BtnItmShow.TabIndex = 10005;
            this.BtnItmShow.Text = "ShowItem";
            this.BtnItmShow.UseVisualStyleBackColor = false;
            this.BtnItmShow.Click += new System.EventHandler(this.BtnItmShow_Click);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(105, 44);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(145, 23);
            this.DTPFromDate.TabIndex = 10003;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(270, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 10007;
            this.label2.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(359, 43);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(136, 23);
            this.DTToDate.TabIndex = 10004;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(731, 38);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 10009;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.OpQty,
            this.InwardQty,
            this.OutwardQty,
            this.ClsQty});
            this.dgDetails.Location = new System.Drawing.Point(9, 79);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.Size = new System.Drawing.Size(815, 461);
            this.dgDetails.TabIndex = 10010;
            this.dgDetails.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 400;
            // 
            // OpQty
            // 
            this.OpQty.DataPropertyName = "OpQty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OpQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.OpQty.HeaderText = "Op Qty";
            this.OpQty.Name = "OpQty";
            this.OpQty.ReadOnly = true;
            // 
            // InwardQty
            // 
            this.InwardQty.DataPropertyName = "InvQty";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InwardQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.InwardQty.HeaderText = "Inward Qty";
            this.InwardQty.Name = "InwardQty";
            this.InwardQty.ReadOnly = true;
            // 
            // OutwardQty
            // 
            this.OutwardQty.DataPropertyName = "OutQty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OutwardQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.OutwardQty.HeaderText = "Outward Qty";
            this.OutwardQty.Name = "OutwardQty";
            this.OutwardQty.ReadOnly = true;
            // 
            // ClsQty
            // 
            this.ClsQty.DataPropertyName = "ActualStockQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ClsQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.ClsQty.HeaderText = "Cls Qty";
            this.ClsQty.Name = "ClsQty";
            this.ClsQty.ReadOnly = true;
            // 
            // lblFirmName
            // 
            this.lblFirmName.AutoSize = true;
            this.lblFirmName.Location = new System.Drawing.Point(10, 556);
            this.lblFirmName.Name = "lblFirmName";
            this.lblFirmName.Size = new System.Drawing.Size(35, 13);
            this.lblFirmName.TabIndex = 1;
            this.lblFirmName.Text = "label4";
            // 
            // BrandWiseStockSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 586);
            this.Controls.Add(this.pnlMain);
            this.Name = "BrandWiseStockSummary";
            this.Text = "Bulk Break Stock";
            this.Load += new System.EventHandler(this.BrandWiseStockSummary_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button BtnItmShow;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView dgItem;
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.DataGridView dgDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InwardQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutwardQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClsQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdActiveDeActive;
        private System.Windows.Forms.RadioButton rdDeActive;
        private System.Windows.Forms.RadioButton rdActive;
        private System.Windows.Forms.Label lblFirmName;
    }
}