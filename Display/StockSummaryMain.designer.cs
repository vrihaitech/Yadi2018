namespace Yadi.Display
{
    partial class StockSummaryMain
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
            this.BtnShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnItmShow = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMainForm = new System.Windows.Forms.Panel();
            this.chkViewStock = new System.Windows.Forms.CheckBox();
            this.chkIncludeTax = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rdAllColumns = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbRateType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdQtyAmt = new System.Windows.Forms.RadioButton();
            this.rdAmount = new System.Windows.Forms.RadioButton();
            this.rdQty = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdActiveDeActive = new System.Windows.Forms.RadioButton();
            this.rdDeActive = new System.Windows.Forms.RadioButton();
            this.rdActive = new System.Windows.Forms.RadioButton();
            this.pnlPB = new System.Windows.Forms.Panel();
            this.PBBar = new OMControls.OMProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlPB.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(7, 463);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(146, 27);
            this.BtnShow.TabIndex = 4;
            this.BtnShow.Text = "Show Report";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(372, 9);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(136, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(283, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(118, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(145, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSelectAll);
            this.panel1.Controls.Add(this.gvItem);
            this.panel1.Controls.Add(this.BtnShow);
            this.panel1.Location = new System.Drawing.Point(12, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 506);
            this.panel1.TabIndex = 74;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(609, 464);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(85, 17);
            this.chkSelectAll.TabIndex = 72;
            this.chkSelectAll.Text = "SelectAll(F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // gvItem
            // 
            this.gvItem.AllowUserToAddRows = false;
            this.gvItem.AllowUserToDeleteRows = false;
            this.gvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteno,
            this.Item,
            this.Chk});
            this.gvItem.Location = new System.Drawing.Point(4, 5);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(706, 450);
            this.gvItem.TabIndex = 7;
            // 
            // Iteno
            // 
            this.Iteno.DataPropertyName = "ItemName";
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
            this.Item.Width = 550;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnItmShow
            // 
            this.BtnItmShow.Location = new System.Drawing.Point(533, 5);
            this.BtnItmShow.Name = "BtnItmShow";
            this.BtnItmShow.Size = new System.Drawing.Size(93, 27);
            this.BtnItmShow.TabIndex = 6;
            this.BtnItmShow.Text = "ShowItem";
            this.BtnItmShow.UseVisualStyleBackColor = false;
            this.BtnItmShow.Click += new System.EventHandler(this.BtnItmShow_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(1001, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 245;
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainForm.Controls.Add(this.pnlPB);
            this.pnlMainForm.Controls.Add(this.groupBox1);
            this.pnlMainForm.Controls.Add(this.chkViewStock);
            this.pnlMainForm.Controls.Add(this.chkIncludeTax);
            this.pnlMainForm.Controls.Add(this.panel3);
            this.pnlMainForm.Controls.Add(this.panel2);
            this.pnlMainForm.Controls.Add(this.panel1);
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Controls.Add(this.BtnItmShow);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Location = new System.Drawing.Point(3, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(869, 621);
            this.pnlMainForm.TabIndex = 246;
            // 
            // chkViewStock
            // 
            this.chkViewStock.AutoSize = true;
            this.chkViewStock.Location = new System.Drawing.Point(164, 80);
            this.chkViewStock.Name = "chkViewStock";
            this.chkViewStock.Size = new System.Drawing.Size(98, 17);
            this.chkViewStock.TabIndex = 10007;
            this.chkViewStock.Text = "View Stock = 0";
            this.chkViewStock.UseVisualStyleBackColor = true;
            // 
            // chkIncludeTax
            // 
            this.chkIncludeTax.AutoSize = true;
            this.chkIncludeTax.Location = new System.Drawing.Point(12, 78);
            this.chkIncludeTax.Name = "chkIncludeTax";
            this.chkIncludeTax.Size = new System.Drawing.Size(120, 17);
            this.chkIncludeTax.TabIndex = 10006;
            this.chkIncludeTax.Text = "Include Tax In Rate";
            this.chkIncludeTax.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Controls.Add(this.rdAllColumns);
            this.panel3.Location = new System.Drawing.Point(268, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(358, 34);
            this.panel3.TabIndex = 10005;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(120, 7);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(149, 20);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Only Closing Stock";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rdAllColumns
            // 
            this.rdAllColumns.AutoSize = true;
            this.rdAllColumns.Checked = true;
            this.rdAllColumns.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAllColumns.Location = new System.Drawing.Point(5, 6);
            this.rdAllColumns.Name = "rdAllColumns";
            this.rdAllColumns.Size = new System.Drawing.Size(100, 20);
            this.rdAllColumns.TabIndex = 2;
            this.rdAllColumns.TabStop = true;
            this.rdAllColumns.Text = "All Columns";
            this.rdAllColumns.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbRateType);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.rdQtyAmt);
            this.panel2.Controls.Add(this.rdAmount);
            this.panel2.Controls.Add(this.rdQty);
            this.panel2.Location = new System.Drawing.Point(12, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(614, 34);
            this.panel2.TabIndex = 112;
            // 
            // cmbRateType
            // 
            this.cmbRateType.FormattingEnabled = true;
            this.cmbRateType.Location = new System.Drawing.Point(453, 7);
            this.cmbRateType.Name = "cmbRateType";
            this.cmbRateType.Size = new System.Drawing.Size(154, 21);
            this.cmbRateType.TabIndex = 10004;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(356, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 10003;
            this.label3.Text = "Rate Type :";
            // 
            // rdQtyAmt
            // 
            this.rdQtyAmt.AutoSize = true;
            this.rdQtyAmt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdQtyAmt.Location = new System.Drawing.Point(184, 6);
            this.rdQtyAmt.Name = "rdQtyAmt";
            this.rdQtyAmt.Size = new System.Drawing.Size(152, 20);
            this.rdQtyAmt.TabIndex = 4;
            this.rdQtyAmt.TabStop = true;
            this.rdQtyAmt.Text = "Quantity && Amount";
            this.rdQtyAmt.UseVisualStyleBackColor = true;
            // 
            // rdAmount
            // 
            this.rdAmount.AutoSize = true;
            this.rdAmount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAmount.Location = new System.Drawing.Point(95, 7);
            this.rdAmount.Name = "rdAmount";
            this.rdAmount.Size = new System.Drawing.Size(76, 20);
            this.rdAmount.TabIndex = 3;
            this.rdAmount.TabStop = true;
            this.rdAmount.Text = "Amount";
            this.rdAmount.UseVisualStyleBackColor = true;
            // 
            // rdQty
            // 
            this.rdQty.AutoSize = true;
            this.rdQty.Checked = true;
            this.rdQty.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdQty.Location = new System.Drawing.Point(5, 6);
            this.rdQty.Name = "rdQty";
            this.rdQty.Size = new System.Drawing.Size(83, 20);
            this.rdQty.TabIndex = 2;
            this.rdQty.TabStop = true;
            this.rdQty.Text = "Quantity";
            this.rdQty.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(640, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdActiveDeActive);
            this.groupBox1.Controls.Add(this.rdDeActive);
            this.groupBox1.Controls.Add(this.rdActive);
            this.groupBox1.Location = new System.Drawing.Point(640, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 35);
            this.groupBox1.TabIndex = 10008;
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
            // 
            // pnlPB
            // 
            this.pnlPB.Controls.Add(this.PBBar);
            this.pnlPB.Location = new System.Drawing.Point(640, 73);
            this.pnlPB.Name = "pnlPB";
            this.pnlPB.Size = new System.Drawing.Size(219, 30);
            this.pnlPB.TabIndex = 10009;
            this.pnlPB.Visible = false;
            // 
            // PBBar
            // 
            this.PBBar.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PBBar.EndColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PBBar.ForeColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PBBar.Location = new System.Drawing.Point(4, 3);
            this.PBBar.Name = "PBBar";
            this.PBBar.Size = new System.Drawing.Size(209, 23);
            this.PBBar.TabIndex = 0;
            // 
            // StockSummaryMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 700);
            this.Controls.Add(this.pnlMainForm);
            this.Controls.Add(this.lblStatus);
            this.Name = "StockSummaryMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Summary";
            this.Load += new System.EventHandler(this.StockSummary_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlPB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.DataGridView gvItem;
        internal System.Windows.Forms.Button BtnItmShow;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMainForm;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbRateType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdQtyAmt;
        private System.Windows.Forms.RadioButton rdAmount;
        private System.Windows.Forms.RadioButton rdQty;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rdAllColumns;
        private System.Windows.Forms.CheckBox chkIncludeTax;
        private System.Windows.Forms.CheckBox chkViewStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdActiveDeActive;
        private System.Windows.Forms.RadioButton rdDeActive;
        private System.Windows.Forms.RadioButton rdActive;
        private System.Windows.Forms.Panel pnlPB;
        private OMControls.OMProgressBar PBBar;
    }
}