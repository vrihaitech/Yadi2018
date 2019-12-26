namespace Yadi.Display
{
    partial class AllItemTransaction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrint = new System.Windows.Forms.Button();
            this.BtnShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InwQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InwAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutwQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutwAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(824, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 96;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(598, 10);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 92;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(419, 12);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(134, 23);
            this.DTToDate.TabIndex = 91;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(332, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 90;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(119, 12);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(142, 23);
            this.DTPFromDate.TabIndex = 89;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 88;
            this.label1.Text = "From Date :";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView1.ColumnHeadersHeight = 21;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.ItemName,
            this.OpQty,
            this.OpAmt,
            this.InwQty,
            this.InwAmt,
            this.OutwQty,
            this.OutwAmt,
            this.ClosingQty,
            this.ClosingAmt});
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(18, 54);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(902, 437);
            this.DataGridView1.TabIndex = 97;
            this.DataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Width = 60;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 200;
            // 
            // OpQty
            // 
            this.OpQty.DataPropertyName = "OpQty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OpQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.OpQty.HeaderText = "OpQty";
            this.OpQty.Name = "OpQty";
            this.OpQty.ReadOnly = true;
            this.OpQty.Width = 60;
            // 
            // OpAmt
            // 
            this.OpAmt.DataPropertyName = "OpAmt";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OpAmt.DefaultCellStyle = dataGridViewCellStyle2;
            this.OpAmt.HeaderText = "OpAmt";
            this.OpAmt.Name = "OpAmt";
            this.OpAmt.ReadOnly = true;
            this.OpAmt.Width = 60;
            // 
            // InwQty
            // 
            this.InwQty.DataPropertyName = "InwQty";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InwQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.InwQty.HeaderText = "InwQty";
            this.InwQty.Name = "InwQty";
            this.InwQty.ReadOnly = true;
            this.InwQty.Width = 60;
            // 
            // InwAmt
            // 
            this.InwAmt.DataPropertyName = "InwAmt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InwAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.InwAmt.HeaderText = "InwAmt";
            this.InwAmt.Name = "InwAmt";
            this.InwAmt.ReadOnly = true;
            this.InwAmt.Width = 60;
            // 
            // OutwQty
            // 
            this.OutwQty.DataPropertyName = "OutwQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OutwQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.OutwQty.HeaderText = "OutwQty";
            this.OutwQty.Name = "OutwQty";
            this.OutwQty.ReadOnly = true;
            this.OutwQty.Width = 70;
            // 
            // OutwAmt
            // 
            this.OutwAmt.DataPropertyName = "OutwAmt";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OutwAmt.DefaultCellStyle = dataGridViewCellStyle6;
            this.OutwAmt.HeaderText = "OutwAmt";
            this.OutwAmt.Name = "OutwAmt";
            this.OutwAmt.ReadOnly = true;
            this.OutwAmt.Width = 70;
            // 
            // ClosingQty
            // 
            this.ClosingQty.DataPropertyName = "ClosingQty";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ClosingQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.ClosingQty.HeaderText = "ClosingQty";
            this.ClosingQty.Name = "ClosingQty";
            this.ClosingQty.ReadOnly = true;
            this.ClosingQty.Width = 70;
            // 
            // ClosingAmt
            // 
            this.ClosingAmt.DataPropertyName = "ClosingAmt";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ClosingAmt.DefaultCellStyle = dataGridViewCellStyle8;
            this.ClosingAmt.HeaderText = "ClosingAmt";
            this.ClosingAmt.Name = "ClosingAmt";
            this.ClosingAmt.ReadOnly = true;
            this.ClosingAmt.Width = 70;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DataGridView1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.BtnShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(6, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(953, 505);
            this.pnlMain.TabIndex = 98;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(713, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 98;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AllItemTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 578);
            this.Controls.Add(this.pnlMain);
            this.Name = "AllItemTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "All Item Transaction";
            this.Load += new System.EventHandler(this.AllItemTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn InwQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InwAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutwQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutwAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingAmt;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
    }
}