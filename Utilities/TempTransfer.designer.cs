namespace Yadi.Utilities
{
    partial class TempTransfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.plnLedger = new System.Windows.Forms.Panel();
            this.dgTransfer = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOML = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MktQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoOfUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSNCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstimateSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.pnlMainForm = new OMControls.OMBPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlGroupName = new System.Windows.Forms.Panel();
            this.lstGroupName = new System.Windows.Forms.ListBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.plnLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransfer)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.pnlGroupName.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // plnLedger
            // 
            this.plnLedger.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.plnLedger.Controls.Add(this.dgTransfer);
            this.plnLedger.Location = new System.Drawing.Point(10, 76);
            this.plnLedger.Name = "plnLedger";
            this.plnLedger.Size = new System.Drawing.Size(1136, 512);
            this.plnLedger.TabIndex = 3;
            this.plnLedger.Visible = false;
            // 
            // dgTransfer
            // 
            this.dgTransfer.AllowUserToAddRows = false;
            this.dgTransfer.AllowUserToDeleteRows = false;
            this.dgTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransfer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.GroupName,
            this.ItemName,
            this.MRP,
            this.UOMH,
            this.UOML,
            this.UOMD,
            this.WRate,
            this.RRate,
            this.MktQty,
            this.NoOfUnit,
            this.PurRate,
            this.ItemNo,
            this.GroupNo,
            this.HSNCode,
            this.EstimateSales,
            this.Check,
            this.BNO});
            this.dgTransfer.Location = new System.Drawing.Point(0, 4);
            this.dgTransfer.Name = "dgTransfer";
            this.dgTransfer.Size = new System.Drawing.Size(1135, 507);
            this.dgTransfer.TabIndex = 3;
            this.dgTransfer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgTransfer_CellFormatting);
            this.dgTransfer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgLedger_KeyDown);
            // 
            // SrNo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // GroupName
            // 
            this.GroupName.HeaderText = "GroupName";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 250;
            // 
            // MRP
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MRP.DefaultCellStyle = dataGridViewCellStyle2;
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.Width = 70;
            // 
            // UOMH
            // 
            this.UOMH.HeaderText = "UOMH";
            this.UOMH.Name = "UOMH";
            this.UOMH.ReadOnly = true;
            this.UOMH.Width = 65;
            // 
            // UOML
            // 
            this.UOML.HeaderText = "UOML";
            this.UOML.Name = "UOML";
            this.UOML.ReadOnly = true;
            this.UOML.Width = 65;
            // 
            // UOMD
            // 
            this.UOMD.HeaderText = "UOMD";
            this.UOMD.Name = "UOMD";
            this.UOMD.ReadOnly = true;
            this.UOMD.Visible = false;
            this.UOMD.Width = 65;
            // 
            // WRate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.WRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.WRate.HeaderText = "W Rate";
            this.WRate.Name = "WRate";
            this.WRate.Width = 65;
            // 
            // RRate
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RRate.DefaultCellStyle = dataGridViewCellStyle4;
            this.RRate.HeaderText = "R Rate";
            this.RRate.Name = "RRate";
            this.RRate.Width = 65;
            // 
            // MktQty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MktQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.MktQty.HeaderText = "MktQty";
            this.MktQty.Name = "MktQty";
            this.MktQty.ReadOnly = true;
            this.MktQty.Width = 65;
            // 
            // NoOfUnit
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NoOfUnit.DefaultCellStyle = dataGridViewCellStyle6;
            this.NoOfUnit.HeaderText = "NoOfUnit";
            this.NoOfUnit.Name = "NoOfUnit";
            this.NoOfUnit.Width = 70;
            // 
            // PurRate
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PurRate.DefaultCellStyle = dataGridViewCellStyle7;
            this.PurRate.HeaderText = "PurRate";
            this.PurRate.Name = "PurRate";
            this.PurRate.Width = 70;
            // 
            // ItemNo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ItemNo.DefaultCellStyle = dataGridViewCellStyle8;
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            this.ItemNo.Width = 70;
            // 
            // GroupNo
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GroupNo.DefaultCellStyle = dataGridViewCellStyle9;
            this.GroupNo.HeaderText = "GroupNo";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.Visible = false;
            this.GroupNo.Width = 70;
            // 
            // HSNCode
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.HSNCode.DefaultCellStyle = dataGridViewCellStyle10;
            this.HSNCode.HeaderText = "HSNCode";
            this.HSNCode.Name = "HSNCode";
            this.HSNCode.Width = 75;
            // 
            // EstimateSales
            // 
            this.EstimateSales.HeaderText = "EstSales";
            this.EstimateSales.Name = "EstimateSales";
            this.EstimateSales.Width = 70;
            // 
            // Check
            // 
            this.Check.HeaderText = "Select";
            this.Check.Name = "Check";
            this.Check.Width = 72;
            // 
            // BNO
            // 
            this.BNO.HeaderText = "BNO";
            this.BNO.Name = "BNO";
            this.BNO.ReadOnly = true;
            this.BNO.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(1039, 601);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "Select All(F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(300, 595);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 61;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(19, 595);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(90, 27);
            this.btnTransfer.TabIndex = 74;
            this.btnTransfer.Text = "&Update Items";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainForm.Controls.Add(this.button1);
            this.pnlMainForm.Controls.Add(this.chkSelectAll);
            this.pnlMainForm.Controls.Add(this.pnlGroupName);
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.Label1);
            this.pnlMainForm.Controls.Add(this.btnTransfer);
            this.pnlMainForm.Controls.Add(this.txtGroupName);
            this.pnlMainForm.Controls.Add(this.plnLedger);
            this.pnlMainForm.Location = new System.Drawing.Point(3, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(1163, 673);
            this.pnlMainForm.TabIndex = 75;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 595);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 27);
            this.button1.TabIndex = 16000043;
            this.button1.Text = "&Transfer Items";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlGroupName
            // 
            this.pnlGroupName.BackColor = System.Drawing.Color.Bisque;
            this.pnlGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGroupName.Controls.Add(this.lstGroupName);
            this.pnlGroupName.Location = new System.Drawing.Point(120, 48);
            this.pnlGroupName.Name = "pnlGroupName";
            this.pnlGroupName.Size = new System.Drawing.Size(259, 196);
            this.pnlGroupName.TabIndex = 16000042;
            this.pnlGroupName.Visible = false;
            // 
            // lstGroupName
            // 
            this.lstGroupName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGroupName.FormattingEnabled = true;
            this.lstGroupName.Location = new System.Drawing.Point(3, 3);
            this.lstGroupName.Name = "lstGroupName";
            this.lstGroupName.Size = new System.Drawing.Size(250, 186);
            this.lstGroupName.Sorted = true;
            this.lstGroupName.TabIndex = 516;
            this.lstGroupName.SelectedIndexChanged += new System.EventHandler(this.lstGroupName_SelectedIndexChanged);
            this.lstGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGroupName_KeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(18, 21);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(73, 13);
            this.Label1.TabIndex = 54;
            this.Label1.Text = "Group Name :";
            // 
            // txtGroupName
            // 
            this.txtGroupName.BackColor = System.Drawing.SystemColors.Window;
            this.txtGroupName.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroupName.Location = new System.Drawing.Point(120, 21);
            this.txtGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.txtGroupName.MaxLength = 20;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(259, 27);
            this.txtGroupName.TabIndex = 0;
            this.txtGroupName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGroupName_KeyPress);
            // 
            // TempTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1178, 699);
            this.Controls.Add(this.pnlMainForm);
            this.Name = "TempTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temp Transfer";
            this.Load += new System.EventHandler(this.TempTransfer_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TempTransfer_KeyPress);
            this.plnLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTransfer)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.pnlGroupName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel plnLedger;
        private System.Windows.Forms.DataGridView dgTransfer;
        private System.Windows.Forms.CheckBox chkSelectAll;
        internal System.Windows.Forms.Button btnExit;
        private OMControls.OMBPanel pnlMainForm;
        internal System.Windows.Forms.Button btnTransfer;
        internal System.Windows.Forms.TextBox txtGroupName;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel pnlGroupName;
        private System.Windows.Forms.ListBox lstGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOML;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMD;
        private System.Windows.Forms.DataGridViewTextBoxColumn WRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn RRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MktQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoOfUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSNCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstimateSales;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn BNO;
        internal System.Windows.Forms.Button button1;
    }
}