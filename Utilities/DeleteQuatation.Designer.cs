namespace Yadi.Utilities
{
    partial class DeleteQuatation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblhelpQuotation = new System.Windows.Forms.Label();
            this.chkDeleteAll = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.dgQuotation = new System.Windows.Forms.DataGridView();
            this.srno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quserno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.todate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchaseno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.qno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgDisplay = new System.Windows.Forms.DataGridView();
            this.sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOMNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkRateSettingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuotationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsClosed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblList = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbParty = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblhelpQuotation);
            this.pnlMain.Controls.Add(this.chkDeleteAll);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.dgQuotation);
            this.pnlMain.Controls.Add(this.dgDisplay);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.lblList);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(572, 529);
            this.pnlMain.TabIndex = 0;
            // 
            // lblhelpQuotation
            // 
            this.lblhelpQuotation.AutoSize = true;
            this.lblhelpQuotation.Location = new System.Drawing.Point(14, 282);
            this.lblhelpQuotation.Name = "lblhelpQuotation";
            this.lblhelpQuotation.Size = new System.Drawing.Size(97, 13);
            this.lblhelpQuotation.TabIndex = 5571;
            this.lblhelpQuotation.Text = "F5 : Display Details";
            // 
            // chkDeleteAll
            // 
            this.chkDeleteAll.AutoSize = true;
            this.chkDeleteAll.Location = new System.Drawing.Point(468, 285);
            this.chkDeleteAll.Name = "chkDeleteAll";
            this.chkDeleteAll.Size = new System.Drawing.Size(88, 17);
            this.chkDeleteAll.TabIndex = 5570;
            this.chkDeleteAll.Text = "SelectAll (F2)";
            this.chkDeleteAll.UseVisualStyleBackColor = true;
            this.chkDeleteAll.CheckedChanged += new System.EventHandler(this.chkDeleteAll_CheckedChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(74, 186);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(371, 52);
            this.lblMsg.TabIndex = 5569;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // dgQuotation
            // 
            this.dgQuotation.AllowUserToAddRows = false;
            this.dgQuotation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQuotation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srno,
            this.quserno,
            this.date,
            this.fromdate,
            this.todate,
            this.purchaseno,
            this.delete,
            this.qno});
            this.dgQuotation.Location = new System.Drawing.Point(11, 125);
            this.dgQuotation.Name = "dgQuotation";
            this.dgQuotation.Size = new System.Drawing.Size(548, 150);
            this.dgQuotation.TabIndex = 1;
            this.dgQuotation.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgQuotation_CellMouseClick);
            this.dgQuotation.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgQuotation_CellFormatting);
            this.dgQuotation.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgQuotation_CellMouseDoubleClick);
            this.dgQuotation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgQuotation_KeyDown);
            this.dgQuotation.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgQuotation_CellContentClick);
            // 
            // srno
            // 
            this.srno.HeaderText = "SrNo";
            this.srno.MinimumWidth = 2;
            this.srno.Name = "srno";
            this.srno.ReadOnly = true;
            this.srno.Width = 50;
            // 
            // quserno
            // 
            this.quserno.HeaderText = "QNo";
            this.quserno.Name = "quserno";
            this.quserno.ReadOnly = true;
            this.quserno.Width = 50;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 95;
            // 
            // fromdate
            // 
            this.fromdate.HeaderText = "FromDate";
            this.fromdate.Name = "fromdate";
            this.fromdate.ReadOnly = true;
            this.fromdate.Width = 95;
            // 
            // todate
            // 
            this.todate.HeaderText = "ToDate";
            this.todate.Name = "todate";
            this.todate.ReadOnly = true;
            this.todate.Width = 95;
            // 
            // purchaseno
            // 
            this.purchaseno.HeaderText = "PNo";
            this.purchaseno.MinimumWidth = 2;
            this.purchaseno.Name = "purchaseno";
            this.purchaseno.ReadOnly = true;
            this.purchaseno.Width = 50;
            // 
            // delete
            // 
            this.delete.HeaderText = "Delete";
            this.delete.MinimumWidth = 2;
            this.delete.Name = "delete";
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delete.Width = 50;
            // 
            // qno
            // 
            this.qno.HeaderText = "QuotationNo";
            this.qno.Name = "qno";
            this.qno.Visible = false;
            // 
            // dgDisplay
            // 
            this.dgDisplay.AllowUserToAddRows = false;
            this.dgDisplay.AllowUserToDeleteRows = false;
            this.dgDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sno,
            this.ItemName,
            this.UOM,
            this.SaleRate,
            this.Rate,
            this.Barcode,
            this.PkSrNo,
            this.ItemNo,
            this.UOMNo,
            this.PkRateSettingNo,
            this.QuotationNo,
            this.IsClosed});
            this.dgDisplay.Location = new System.Drawing.Point(11, 333);
            this.dgDisplay.Name = "dgDisplay";
            this.dgDisplay.Size = new System.Drawing.Size(545, 114);
            this.dgDisplay.TabIndex = 7;
            this.dgDisplay.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDisplay_CellFormatting);
            // 
            // sno
            // 
            this.sno.DataPropertyName = "Sr";
            this.sno.HeaderText = "Sr";
            this.sno.Name = "sno";
            this.sno.ReadOnly = true;
            this.sno.Width = 50;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Description";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 200;
            // 
            // UOM
            // 
            this.UOM.DataPropertyName = "UOMName";
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 60;
            // 
            // SaleRate
            // 
            this.SaleRate.DataPropertyName = "MRP";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.SaleRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.SaleRate.HeaderText = "MRP";
            this.SaleRate.Name = "SaleRate";
            this.SaleRate.ReadOnly = true;
            this.SaleRate.Width = 90;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle4;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 90;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Barcode";
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.DataPropertyName = "PkStockBarcodeNo";
            this.PkSrNo.HeaderText = "PkBarcodeNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // UOMNo
            // 
            this.UOMNo.DataPropertyName = "UOMNo";
            this.UOMNo.HeaderText = "UOMNo";
            this.UOMNo.Name = "UOMNo";
            this.UOMNo.Visible = false;
            // 
            // PkRateSettingNo
            // 
            this.PkRateSettingNo.HeaderText = "RateSettingNo";
            this.PkRateSettingNo.Name = "PkRateSettingNo";
            this.PkRateSettingNo.Visible = false;
            // 
            // QuotationNo
            // 
            this.QuotationNo.HeaderText = "QuotationNo";
            this.QuotationNo.Name = "QuotationNo";
            this.QuotationNo.Visible = false;
            // 
            // IsClosed
            // 
            this.IsClosed.HeaderText = "IsClosed";
            this.IsClosed.Name = "IsClosed";
            this.IsClosed.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(176, 456);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 64);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(95, 456);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 64);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(14, 456);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 64);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(14, 317);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(60, 13);
            this.lblList.TabIndex = 3;
            this.lblList.Text = "Display List";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbParty);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.lblToDate);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.lblFromDate);
            this.panel1.Location = new System.Drawing.Point(11, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 102);
            this.panel1.TabIndex = 0;
            // 
            // cmbParty
            // 
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.Location = new System.Drawing.Point(84, 60);
            this.cmbParty.Name = "cmbParty";
            this.cmbParty.Size = new System.Drawing.Size(383, 21);
            this.cmbParty.TabIndex = 5577;
            this.cmbParty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbParty_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5576;
            this.label1.Text = "Party:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(310, 20);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(157, 20);
            this.dtpToDate.TabIndex = 5575;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(247, 23);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(49, 13);
            this.lblToDate.TabIndex = 5573;
            this.lblToDate.Text = "To Date:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(84, 20);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(149, 20);
            this.dtpFromDate.TabIndex = 5574;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromDate_KeyDown);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(6, 23);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(59, 13);
            this.lblFromDate.TabIndex = 5572;
            this.lblFromDate.Text = "From Date:\r\n";
            // 
            // DeleteQuatation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 534);
            this.Controls.Add(this.pnlMain);
            this.Name = "DeleteQuatation";
            this.Text = "Delete Quatation";
            this.Load += new System.EventHandler(this.DeleteQuatation_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgQuotation;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgDisplay;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkDeleteAll;
        private System.Windows.Forms.Label lblhelpQuotation;
        private System.Windows.Forms.DataGridViewTextBoxColumn sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkRateSettingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuotationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsClosed;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn srno;
        private System.Windows.Forms.DataGridViewTextBoxColumn quserno;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn todate;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn qno;
        private System.Windows.Forms.ComboBox cmbParty;
        private System.Windows.Forms.Label label1;
    }
}