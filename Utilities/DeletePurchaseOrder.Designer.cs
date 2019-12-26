namespace Yadi.Utilities
{
    partial class DeletePurchaseOrder
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
            this.pnlMain = new OMControls.OMBPanel();
            this.lblhelpQuotation = new System.Windows.Forms.Label();
            this.chkDeleteAll = new System.Windows.Forms.CheckBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.dgOrder = new System.Windows.Forms.DataGridView();
            this.srno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BilledAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PkOtherVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgdisplay = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblList = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblparty = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.cmbParty = new System.Windows.Forms.ComboBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdisplay)).BeginInit();
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
            this.pnlMain.Controls.Add(this.dgOrder);
            this.pnlMain.Controls.Add(this.dgdisplay);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.lblList);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(572, 494);
            this.pnlMain.TabIndex = 0;
            // 
            // lblhelpQuotation
            // 
            this.lblhelpQuotation.AutoSize = true;
            this.lblhelpQuotation.Location = new System.Drawing.Point(14, 248);
            this.lblhelpQuotation.Name = "lblhelpQuotation";
            this.lblhelpQuotation.Size = new System.Drawing.Size(97, 13);
            this.lblhelpQuotation.TabIndex = 5571;
            this.lblhelpQuotation.Text = "F5 : Display Details";
            // 
            // chkDeleteAll
            // 
            this.chkDeleteAll.AutoSize = true;
            this.chkDeleteAll.Location = new System.Drawing.Point(467, 247);
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
            this.lblMsg.Location = new System.Drawing.Point(98, 151);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(371, 52);
            this.lblMsg.TabIndex = 5569;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // dgOrder
            // 
            this.dgOrder.AllowUserToAddRows = false;
            this.dgOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srno,
            this.DocNo,
            this.date,
            this.pono,
            this.BilledAmt,
            this.delete,
            this.PkOtherVoucherNo});
            this.dgOrder.Location = new System.Drawing.Point(11, 95);
            this.dgOrder.Name = "dgOrder";
            this.dgOrder.Size = new System.Drawing.Size(545, 150);
            this.dgOrder.TabIndex = 3;
            this.dgOrder.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgquotation_CellMouseClick);
            this.dgOrder.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgquotation_CellFormatting);
            this.dgOrder.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgquotation_CellMouseDoubleClick);
            this.dgOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgquotation_KeyDown);
            // 
            // srno
            // 
            this.srno.HeaderText = "SrNo";
            this.srno.MinimumWidth = 2;
            this.srno.Name = "srno";
            this.srno.ReadOnly = true;
            this.srno.Width = 50;
            // 
            // DocNo
            // 
            this.DocNo.HeaderText = "Doc No";
            this.DocNo.Name = "DocNo";
            this.DocNo.ReadOnly = true;
            this.DocNo.Width = 80;
            // 
            // date
            // 
            this.date.HeaderText = "Order Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // pono
            // 
            this.pono.HeaderText = "Po.No";
            this.pono.Name = "pono";
            this.pono.ReadOnly = true;
            // 
            // BilledAmt
            // 
            this.BilledAmt.HeaderText = "Billed Amt";
            this.BilledAmt.Name = "BilledAmt";
            this.BilledAmt.ReadOnly = true;
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
            // PkOtherVoucherNo
            // 
            this.PkOtherVoucherNo.HeaderText = "PkOtherVoucherNo";
            this.PkOtherVoucherNo.Name = "PkOtherVoucherNo";
            this.PkOtherVoucherNo.Visible = false;
            // 
            // dgdisplay
            // 
            this.dgdisplay.AllowUserToAddRows = false;
            this.dgdisplay.AllowUserToDeleteRows = false;
            this.dgdisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sno,
            this.ItemName,
            this.UOM,
            this.Qty,
            this.SaleRate,
            this.Rate});
            this.dgdisplay.Location = new System.Drawing.Point(11, 299);
            this.dgdisplay.Name = "dgdisplay";
            this.dgdisplay.Size = new System.Drawing.Size(545, 114);
            this.dgdisplay.TabIndex = 4;
            this.dgdisplay.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgdisplay_CellFormatting);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(176, 422);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 64);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(95, 422);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 64);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(14, 422);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 64);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(14, 283);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(60, 13);
            this.lblList.TabIndex = 355554;
            this.lblList.Text = "Display List";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.lblparty);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.lblToDate);
            this.panel1.Controls.Add(this.cmbParty);
            this.panel1.Controls.Add(this.lblFromDate);
            this.panel1.Location = new System.Drawing.Point(7, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 80);
            this.panel1.TabIndex = 0;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(352, 8);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(149, 20);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // lblparty
            // 
            this.lblparty.AutoSize = true;
            this.lblparty.Location = new System.Drawing.Point(4, 43);
            this.lblparty.Name = "lblparty";
            this.lblparty.Size = new System.Drawing.Size(37, 13);
            this.lblparty.TabIndex = 11556;
            this.lblparty.Text = "Party :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(91, 8);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(149, 20);
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromDate_KeyDown);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(284, 12);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(52, 13);
            this.lblToDate.TabIndex = 5573;
            this.lblToDate.Text = "To Date :";
            // 
            // cmbParty
            // 
            this.cmbParty.FormattingEnabled = true;
            this.cmbParty.Location = new System.Drawing.Point(91, 39);
            this.cmbParty.Name = "cmbParty";
            this.cmbParty.Size = new System.Drawing.Size(410, 21);
            this.cmbParty.TabIndex = 2;
            //this.cmbParty.SelectedIndexChanged += new System.EventHandler(this.cmbParty_SelectedIndexChanged);
            this.cmbParty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbParty_KeyDown);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(4, 12);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(59, 13);
            this.lblFromDate.TabIndex = 5572;
            this.lblFromDate.Text = "From Date:\r\n";
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
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 60;
            // 
            // SaleRate
            // 
            this.SaleRate.DataPropertyName = "MRP";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.SaleRate.DefaultCellStyle = dataGridViewCellStyle1;
            this.SaleRate.HeaderText = "MRP";
            this.SaleRate.Name = "SaleRate";
            this.SaleRate.ReadOnly = true;
            this.SaleRate.Width = 60;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle2;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 60;
            // 
            // DeletePurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 498);
            this.Controls.Add(this.pnlMain);
            this.Name = "DeletePurchaseOrder";
            this.Text = "Delete Purchase Order";
            this.Load += new System.EventHandler(this.DeletePurchaseOrder_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblparty;
        private System.Windows.Forms.ComboBox cmbParty;
        private System.Windows.Forms.DataGridView dgOrder;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgdisplay;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkDeleteAll;
        private System.Windows.Forms.Label lblhelpQuotation;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn srno;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn pono;
        private System.Windows.Forms.DataGridViewTextBoxColumn BilledAmt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkOtherVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
    }
}