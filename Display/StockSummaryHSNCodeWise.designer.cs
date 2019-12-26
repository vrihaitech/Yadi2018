namespace Yadi.Display
{
    partial class StockSummaryHSNCodeWise
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnItmShow = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMainForm = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdActiveDeActive = new System.Windows.Forms.RadioButton();
            this.rdDeActive = new System.Windows.Forms.RadioButton();
            this.rdActive = new System.Windows.Forms.RadioButton();
            this.pnlrb = new System.Windows.Forms.Panel();
            this.rbMonthlySummary = new System.Windows.Forms.RadioButton();
            this.rbItemWise = new System.Windows.Forms.RadioButton();
            this.rbHSNCodeWise = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlrb.SuspendLayout();
            this.SuspendLayout();
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
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkSelectAll);
            this.panel1.Controls.Add(this.gvItem);
            this.panel1.Location = new System.Drawing.Point(16, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 467);
            this.panel1.TabIndex = 74;
            this.panel1.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(311, 4);
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
            this.gvItem.Location = new System.Drawing.Point(16, 27);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(395, 426);
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
            this.Item.Width = 250;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(460, 54);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 10002;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnItmShow
            // 
            this.BtnItmShow.Location = new System.Drawing.Point(249, 54);
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
            this.pnlMainForm.Controls.Add(this.groupBox1);
            this.pnlMainForm.Controls.Add(this.panel1);
            this.pnlMainForm.Controls.Add(this.pnlrb);
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Controls.Add(this.BtnItmShow);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Controls.Add(this.btnPrint);
            this.pnlMainForm.Location = new System.Drawing.Point(18, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(588, 615);
            this.pnlMainForm.TabIndex = 246;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdActiveDeActive);
            this.groupBox1.Controls.Add(this.rdDeActive);
            this.groupBox1.Controls.Add(this.rdActive);
            this.groupBox1.Location = new System.Drawing.Point(16, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 35);
            this.groupBox1.TabIndex = 10003;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Items List";
            // 
            // rdActiveDeActive
            // 
            this.rdActiveDeActive.AutoSize = true;
            this.rdActiveDeActive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdActiveDeActive.Location = new System.Drawing.Point(155, 13);
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
            // pnlrb
            // 
            this.pnlrb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlrb.Controls.Add(this.rbMonthlySummary);
            this.pnlrb.Controls.Add(this.rbItemWise);
            this.pnlrb.Controls.Add(this.rbHSNCodeWise);
            this.pnlrb.Location = new System.Drawing.Point(16, 91);
            this.pnlrb.Name = "pnlrb";
            this.pnlrb.Size = new System.Drawing.Size(554, 38);
            this.pnlrb.TabIndex = 111;
            // 
            // rbMonthlySummary
            // 
            this.rbMonthlySummary.AutoSize = true;
            this.rbMonthlySummary.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMonthlySummary.Location = new System.Drawing.Point(359, 7);
            this.rbMonthlySummary.Name = "rbMonthlySummary";
            this.rbMonthlySummary.Size = new System.Drawing.Size(143, 20);
            this.rbMonthlySummary.TabIndex = 4;
            this.rbMonthlySummary.TabStop = true;
            this.rbMonthlySummary.Text = "Monthly Summary";
            this.rbMonthlySummary.UseVisualStyleBackColor = true;
            // 
            // rbItemWise
            // 
            this.rbItemWise.AutoSize = true;
            this.rbItemWise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbItemWise.Location = new System.Drawing.Point(202, 7);
            this.rbItemWise.Name = "rbItemWise";
            this.rbItemWise.Size = new System.Drawing.Size(150, 20);
            this.rbItemWise.TabIndex = 3;
            this.rbItemWise.TabStop = true;
            this.rbItemWise.Text = "Itemwise Summary";
            this.rbItemWise.UseVisualStyleBackColor = true;
            // 
            // rbHSNCodeWise
            // 
            this.rbHSNCodeWise.AutoSize = true;
            this.rbHSNCodeWise.Checked = true;
            this.rbHSNCodeWise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHSNCodeWise.Location = new System.Drawing.Point(5, 7);
            this.rbHSNCodeWise.Name = "rbHSNCodeWise";
            this.rbHSNCodeWise.Size = new System.Drawing.Size(187, 20);
            this.rbHSNCodeWise.TabIndex = 2;
            this.rbHSNCodeWise.TabStop = true;
            this.rbHSNCodeWise.Text = "HSNCode Wise Summary";
            this.rbHSNCodeWise.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(356, 54);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // StockSummaryHSNCodeWise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 633);
            this.Controls.Add(this.pnlMainForm);
            this.Controls.Add(this.lblStatus);
            this.Name = "StockSummaryHSNCodeWise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Summary HSNCode Wise";
            this.Load += new System.EventHandler(this.StockSummaryHSNCodeWise_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlrb.ResumeLayout(false);
            this.pnlrb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.DataGridView gvItem;
        internal System.Windows.Forms.Button BtnItmShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMainForm;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlrb;
        private System.Windows.Forms.RadioButton rbMonthlySummary;
        private System.Windows.Forms.RadioButton rbItemWise;
        private System.Windows.Forms.RadioButton rbHSNCodeWise;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdActiveDeActive;
        private System.Windows.Forms.RadioButton rdDeActive;
        private System.Windows.Forms.RadioButton rdActive;
    }
}