namespace Yadi.Display
{
    partial class DailyStockSummary
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
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnShow = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemUserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbAllItem = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbItemWise = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMainForm = new OMControls.OMBPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.CustomFormat = "dd-MMM-yyyy";
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(117, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(140, 23);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.Value = new System.DateTime(2011, 6, 28, 16, 2, 0, 0);
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 141;
            this.label1.Text = "From Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.CustomFormat = "dd-MMM-yyyy";
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(370, 9);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(139, 23);
            this.DTToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(280, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 140;
            this.label2.Text = "To Date :";
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(532, 5);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 4;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(631, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 138;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(7, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 476);
            this.panel1.TabIndex = 143;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(792, 26);
            this.label5.TabIndex = 128;
            this.label5.Text = "Daily Stock Summary Details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemNo,
            this.ItemUserNo,
            this.ItemName,
            this.Quantity,
            this.Rate,
            this.SalesRate});
            this.dataGridView1.Location = new System.Drawing.Point(0, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(789, 410);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.ReadOnly = true;
            this.ItemNo.Visible = false;
            // 
            // ItemUserNo
            // 
            this.ItemUserNo.DataPropertyName = "Code";
            this.ItemUserNo.HeaderText = "Code";
            this.ItemUserNo.Name = "ItemUserNo";
            this.ItemUserNo.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Cur Stk Bal";
            this.Quantity.HeaderText = "Cur Stk Bal";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Cur Stk Cos";
            this.Rate.HeaderText = "Cur Stk Cos";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // SalesRate
            // 
            this.SalesRate.DataPropertyName = "Cur Stk Sale";
            this.SalesRate.HeaderText = "Cur Stk Sale";
            this.SalesRate.Name = "SalesRate";
            this.SalesRate.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.txtBarCode);
            this.panel2.Location = new System.Drawing.Point(487, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 47);
            this.panel2.TabIndex = 150;
            this.panel2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(9, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(129, 21);
            this.checkBox1.TabIndex = 152;
            this.checkBox1.Text = "BarCode Wise";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // txtBarCode
            // 
            this.txtBarCode.AcceptsReturn = true;
            this.txtBarCode.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Location = new System.Drawing.Point(144, 14);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(165, 26);
            this.txtBarCode.TabIndex = 139;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbItemName);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(9, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(461, 47);
            this.panel3.TabIndex = 148;
            this.panel3.Visible = false;
            // 
            // cmbItemName
            // 
            this.cmbItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemName.ForeColor = System.Drawing.Color.Black;
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.Location = new System.Drawing.Point(109, 6);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(329, 28);
            this.cmbItemName.TabIndex = 3;
            this.cmbItemName.SelectedIndexChanged += new System.EventHandler(this.cmbItemName_SelectedIndexChanged);
            this.cmbItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbItemName_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "Item Name:";
            // 
            // rbAllItem
            // 
            this.rbAllItem.AutoSize = true;
            this.rbAllItem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllItem.Location = new System.Drawing.Point(195, 49);
            this.rbAllItem.Name = "rbAllItem";
            this.rbAllItem.Size = new System.Drawing.Size(83, 20);
            this.rbAllItem.TabIndex = 147;
            this.rbAllItem.TabStop = true;
            this.rbAllItem.Text = "All Item";
            this.rbAllItem.UseVisualStyleBackColor = true;
            this.rbAllItem.CheckedChanged += new System.EventHandler(this.rbAllItem_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 146;
            this.label3.Text = "Type :";
            // 
            // rbItemWise
            // 
            this.rbItemWise.AutoSize = true;
            this.rbItemWise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbItemWise.Location = new System.Drawing.Point(67, 49);
            this.rbItemWise.Name = "rbItemWise";
            this.rbItemWise.Size = new System.Drawing.Size(100, 20);
            this.rbItemWise.TabIndex = 145;
            this.rbItemWise.TabStop = true;
            this.rbItemWise.Text = "Item Wise";
            this.rbItemWise.UseVisualStyleBackColor = true;
            this.rbItemWise.CheckedChanged += new System.EventHandler(this.rbItemWise_CheckedChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 689);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1028, 23);
            this.label9.TabIndex = 152;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Controls.Add(this.btnPrint);
            this.pnlMainForm.Controls.Add(this.panel2);
            this.pnlMainForm.Controls.Add(this.BtnShow);
            this.pnlMainForm.Controls.Add(this.panel3);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.rbAllItem);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Controls.Add(this.label3);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.rbItemWise);
            this.pnlMainForm.Controls.Add(this.panel1);
            this.pnlMainForm.Location = new System.Drawing.Point(6, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(832, 680);
            this.pnlMainForm.TabIndex = 153;
            // 
            // DailyStockSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 712);
            this.Controls.Add(this.pnlMainForm);
            this.Controls.Add(this.label9);
            this.Name = "DailyStockSummary";
            this.Text = "DailyStockSummary";
            this.Load += new System.EventHandler(this.DailyStockSummary_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbAllItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbItemWise;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemUserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesRate;
        private OMControls.OMBPanel pnlMainForm;
    }
}