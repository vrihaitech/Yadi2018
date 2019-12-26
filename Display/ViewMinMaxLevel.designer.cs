namespace Yadi.Display
{
    partial class ViewMinMaxLevel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnShow = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblFirmName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdActiveDeActive = new System.Windows.Forms.RadioButton();
            this.rdDeActive = new System.Windows.Forms.RadioButton();
            this.rdActive = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(431, 10);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 3;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(616, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 99;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.ItemName,
            this.MinLevel,
            this.MaxQty,
            this.Quantity,
            this.MinOrder,
            this.MaxOrderQty});
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(21, 72);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(688, 461);
            this.DataGridView1.TabIndex = 104;
            this.DataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 105;
            this.label1.Text = "From Date :";
            // 
            // s
            // 
            this.s.CustomFormat = "dd-MMM-yyyy";
            this.s.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s.Location = new System.Drawing.Point(109, 12);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(123, 23);
            this.s.TabIndex = 1;
            this.s.ValueChanged += new System.EventHandler(this.s_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(231, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 120;
            this.label2.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.CustomFormat = "dd-MMM-yyyy";
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(305, 12);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(123, 23);
            this.DTToDate.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.lblFirmName);
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.BtnShow);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.s);
            this.pnlMain.Controls.Add(this.DataGridView1);
            this.pnlMain.Location = new System.Drawing.Point(3, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(728, 543);
            this.pnlMain.TabIndex = 121;
            // 
            // lblFirmName
            // 
            this.lblFirmName.AutoSize = true;
            this.lblFirmName.Location = new System.Drawing.Point(268, 46);
            this.lblFirmName.Name = "lblFirmName";
            this.lblFirmName.Size = new System.Drawing.Size(35, 13);
            this.lblFirmName.TabIndex = 10005;
            this.lblFirmName.Text = "label3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdActiveDeActive);
            this.groupBox1.Controls.Add(this.rdDeActive);
            this.groupBox1.Controls.Add(this.rdActive);
            this.groupBox1.Location = new System.Drawing.Point(24, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 35);
            this.groupBox1.TabIndex = 10004;
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
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(525, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 121;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "Item No";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 270;
            // 
            // MinLevel
            // 
            this.MinLevel.DataPropertyName = "MinLevel";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MinLevel.DefaultCellStyle = dataGridViewCellStyle1;
            this.MinLevel.HeaderText = "MinLevel";
            this.MinLevel.Name = "MinLevel";
            this.MinLevel.ReadOnly = true;
            this.MinLevel.Width = 80;
            // 
            // MaxQty
            // 
            this.MaxQty.DataPropertyName = "MaxLevel";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MaxQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.MaxQty.HeaderText = "MaxLevel";
            this.MaxQty.Name = "MaxQty";
            this.MaxQty.ReadOnly = true;
            this.MaxQty.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.Quantity.HeaderText = "Stock";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // MinOrder
            // 
            this.MinOrder.DataPropertyName = "OrdQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MinOrder.DefaultCellStyle = dataGridViewCellStyle4;
            this.MinOrder.HeaderText = "MinOrd";
            this.MinOrder.Name = "MinOrder";
            this.MinOrder.ReadOnly = true;
            this.MinOrder.Width = 80;
            // 
            // MaxOrderQty
            // 
            this.MaxOrderQty.DataPropertyName = "MaxQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MaxOrderQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.MaxOrderQty.HeaderText = "MaxOrd";
            this.MaxOrderQty.Name = "MaxOrderQty";
            this.MaxOrderQty.ReadOnly = true;
            this.MaxOrderQty.Width = 80;
            // 
            // ViewMinMaxLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 567);
            this.Controls.Add(this.pnlMain);
            this.Name = "ViewMinMaxLevel";
            this.Text = "Stock Below Min Level";
            this.Load += new System.EventHandler(this.ViewMinMaxLevel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker s;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdActiveDeActive;
        private System.Windows.Forms.RadioButton rdDeActive;
        private System.Windows.Forms.RadioButton rdActive;
        private System.Windows.Forms.Label lblFirmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxOrderQty;
    }
}