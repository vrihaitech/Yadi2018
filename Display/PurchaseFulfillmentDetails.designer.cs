 namespace Yadi.Display
{
     partial class PurchaseFulfillmentDetails
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
            this.pnlItemDetails = new System.Windows.Forms.Panel();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnItemCancel = new System.Windows.Forms.Button();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.rdDetails = new System.Windows.Forms.RadioButton();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShow = new System.Windows.Forms.Button();
            this.pnlPurchaseOrderDetails = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.gvPurchase = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShowItem = new System.Windows.Forms.Button();
            this.pnlLedger = new System.Windows.Forms.Panel();
            this.btnLedgerCancel = new System.Windows.Forms.Button();
            this.chckLedgerAll = new System.Windows.Forms.CheckBox();
            this.dgLedger = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnLedgerShow = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPartyShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlItemDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.pnlPurchaseOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchase)).BeginInit();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLedger)).BeginInit();
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
            this.pnlMain.Controls.Add(this.pnlItemDetails);
            this.pnlMain.Controls.Add(this.pnlPurchaseOrderDetails);
            this.pnlMain.Controls.Add(this.pnlLedger);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnPartyShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(6, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(695, 555);
            this.pnlMain.TabIndex = 75;
            // 
            // pnlItemDetails
            // 
            this.pnlItemDetails.Controls.Add(this.BtnExport);
            this.pnlItemDetails.Controls.Add(this.BtnItemCancel);
            this.pnlItemDetails.Controls.Add(this.rdSummary);
            this.pnlItemDetails.Controls.Add(this.rdDetails);
            this.pnlItemDetails.Controls.Add(this.chkSelectAll);
            this.pnlItemDetails.Controls.Add(this.gvItem);
            this.pnlItemDetails.Controls.Add(this.BtnShow);
            this.pnlItemDetails.Location = new System.Drawing.Point(8, 48);
            this.pnlItemDetails.Name = "pnlItemDetails";
            this.pnlItemDetails.Size = new System.Drawing.Size(449, 480);
            this.pnlItemDetails.TabIndex = 74;
            this.pnlItemDetails.Visible = false;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(198, 432);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(91, 27);
            this.BtnExport.TabIndex = 79;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Visible = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnItemCancel
            // 
            this.BtnItemCancel.Location = new System.Drawing.Point(109, 432);
            this.BtnItemCancel.Name = "BtnItemCancel";
            this.BtnItemCancel.Size = new System.Drawing.Size(83, 27);
            this.BtnItemCancel.TabIndex = 77;
            this.BtnItemCancel.Text = "Cancel";
            this.BtnItemCancel.UseVisualStyleBackColor = false;
            this.BtnItemCancel.Click += new System.EventHandler(this.BtnItemCancel_Click);
            // 
            // rdSummary
            // 
            this.rdSummary.AutoSize = true;
            this.rdSummary.Location = new System.Drawing.Point(98, 11);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(68, 17);
            this.rdSummary.TabIndex = 76;
            this.rdSummary.Text = "Summary";
            this.rdSummary.UseVisualStyleBackColor = true;
            this.rdSummary.Visible = false;
            // 
            // rdDetails
            // 
            this.rdDetails.AutoSize = true;
            this.rdDetails.Checked = true;
            this.rdDetails.Location = new System.Drawing.Point(22, 11);
            this.rdDetails.Name = "rdDetails";
            this.rdDetails.Size = new System.Drawing.Size(57, 17);
            this.rdDetails.TabIndex = 75;
            this.rdDetails.TabStop = true;
            this.rdDetails.Text = "Details";
            this.rdDetails.UseVisualStyleBackColor = true;
            this.rdDetails.Visible = false;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(297, 416);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 3;
            this.chkSelectAll.Text = "SelectAll (F2)";
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
            this.gvItem.Location = new System.Drawing.Point(22, 34);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(391, 376);
            this.gvItem.TabIndex = 4;
            this.gvItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvItem_KeyDown);
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
            this.Chk.Width = 80;
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(20, 432);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(83, 27);
            this.BtnShow.TabIndex = 5;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // pnlPurchaseOrderDetails
            // 
            this.pnlPurchaseOrderDetails.Controls.Add(this.BtnCancel);
            this.pnlPurchaseOrderDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPurchaseOrderDetails.Controls.Add(this.gvPurchase);
            this.pnlPurchaseOrderDetails.Controls.Add(this.BtnShowItem);
            this.pnlPurchaseOrderDetails.Location = new System.Drawing.Point(14, 33);
            this.pnlPurchaseOrderDetails.Name = "pnlPurchaseOrderDetails";
            this.pnlPurchaseOrderDetails.Size = new System.Drawing.Size(513, 474);
            this.pnlPurchaseOrderDetails.TabIndex = 75;
            this.pnlPurchaseOrderDetails.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(112, 424);
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
            this.chkPartySelectAll.Location = new System.Drawing.Point(239, 424);
            this.chkPartySelectAll.Name = "chkPartySelectAll";
            this.chkPartySelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkPartySelectAll.TabIndex = 3;
            this.chkPartySelectAll.Text = "SelectAll (F2)";
            this.chkPartySelectAll.UseVisualStyleBackColor = true;
            this.chkPartySelectAll.CheckedChanged += new System.EventHandler(this.chkPartySelectAll_CheckedChanged);
            // 
            // gvPurchase
            // 
            this.gvPurchase.AllowUserToAddRows = false;
            this.gvPurchase.AllowUserToDeleteRows = false;
            this.gvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPurchase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.LedgerName,
            this.Chk1});
            this.gvPurchase.Location = new System.Drawing.Point(13, 11);
            this.gvPurchase.Name = "gvPurchase";
            this.gvPurchase.Size = new System.Drawing.Size(477, 395);
            this.gvPurchase.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PkOtherVoucherNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "PkOtherVoucherNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VoucherUserNo";
            this.dataGridViewTextBoxColumn2.HeaderText = "Purchase Order No";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "LedgerName";
            this.LedgerName.Name = "LedgerName";
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
            this.BtnShowItem.Location = new System.Drawing.Point(13, 424);
            this.BtnShowItem.Name = "BtnShowItem";
            this.BtnShowItem.Size = new System.Drawing.Size(93, 27);
            this.BtnShowItem.TabIndex = 5;
            this.BtnShowItem.Text = "Show";
            this.BtnShowItem.UseVisualStyleBackColor = false;
            this.BtnShowItem.Click += new System.EventHandler(this.BtnShowItem_Click);
            // 
            // pnlLedger
            // 
            this.pnlLedger.Controls.Add(this.btnLedgerCancel);
            this.pnlLedger.Controls.Add(this.chckLedgerAll);
            this.pnlLedger.Controls.Add(this.dgLedger);
            this.pnlLedger.Controls.Add(this.btnLedgerShow);
            this.pnlLedger.Location = new System.Drawing.Point(5, 36);
            this.pnlLedger.Name = "pnlLedger";
            this.pnlLedger.Size = new System.Drawing.Size(418, 474);
            this.pnlLedger.TabIndex = 76;
            this.pnlLedger.Visible = false;
            // 
            // btnLedgerCancel
            // 
            this.btnLedgerCancel.Location = new System.Drawing.Point(112, 424);
            this.btnLedgerCancel.Name = "btnLedgerCancel";
            this.btnLedgerCancel.Size = new System.Drawing.Size(93, 27);
            this.btnLedgerCancel.TabIndex = 6;
            this.btnLedgerCancel.Text = "Cancel";
            this.btnLedgerCancel.UseVisualStyleBackColor = false;
            this.btnLedgerCancel.Click += new System.EventHandler(this.btnLedgerCancel_Click);
            // 
            // chckLedgerAll
            // 
            this.chckLedgerAll.AutoSize = true;
            this.chckLedgerAll.Location = new System.Drawing.Point(239, 424);
            this.chckLedgerAll.Name = "chckLedgerAll";
            this.chckLedgerAll.Size = new System.Drawing.Size(88, 17);
            this.chckLedgerAll.TabIndex = 3;
            this.chckLedgerAll.Text = "SelectAll (F2)";
            this.chckLedgerAll.UseVisualStyleBackColor = true;
            this.chckLedgerAll.CheckedChanged += new System.EventHandler(this.chckLedgerAll_CheckedChanged);
            // 
            // dgLedger
            // 
            this.dgLedger.AllowUserToAddRows = false;
            this.dgLedger.AllowUserToDeleteRows = false;
            this.dgLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLedger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn1});
            this.dgLedger.Location = new System.Drawing.Point(13, 11);
            this.dgLedger.Name = "dgLedger";
            this.dgLedger.Size = new System.Drawing.Size(380, 395);
            this.dgLedger.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "LedgerNo";
            this.dataGridViewTextBoxColumn3.HeaderText = "LedgerNo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "LedgerName";
            this.dataGridViewTextBoxColumn5.HeaderText = "LedgerName";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "chk";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 80;
            // 
            // btnLedgerShow
            // 
            this.btnLedgerShow.Location = new System.Drawing.Point(13, 424);
            this.btnLedgerShow.Name = "btnLedgerShow";
            this.btnLedgerShow.Size = new System.Drawing.Size(93, 27);
            this.btnLedgerShow.TabIndex = 5;
            this.btnLedgerShow.Text = "Show";
            this.btnLedgerShow.UseVisualStyleBackColor = false;
            this.btnLedgerShow.Click += new System.EventHandler(this.btnLedgerShow_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(579, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // BtnPartyShow
            // 
            this.BtnPartyShow.Location = new System.Drawing.Point(407, 2);
            this.BtnPartyShow.Name = "BtnPartyShow";
            this.BtnPartyShow.Size = new System.Drawing.Size(163, 27);
            this.BtnPartyShow.TabIndex = 2;
            this.BtnPartyShow.Text = "Show Party";
            this.BtnPartyShow.UseVisualStyleBackColor = false;
            this.BtnPartyShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(288, 4);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(93, 4);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(120, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // PurchaseFulfillmentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 676);
            this.Controls.Add(this.pnlMain);
            this.Name = "PurchaseFulfillmentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Fulfillment Report";
            this.Load += new System.EventHandler(this.StockSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlItemDetails.ResumeLayout(false);
            this.pnlItemDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.pnlPurchaseOrderDetails.ResumeLayout(false);
            this.pnlPurchaseOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchase)).EndInit();
            this.pnlLedger.ResumeLayout(false);
            this.pnlLedger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLedger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlItemDetails;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.DataGridView gvItem;
        internal System.Windows.Forms.Button BtnPartyShow;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.RadioButton rdSummary;
        private System.Windows.Forms.RadioButton rdDetails;
        private System.Windows.Forms.Panel pnlPurchaseOrderDetails;
        private System.Windows.Forms.CheckBox chkPartySelectAll;
        private System.Windows.Forms.DataGridView gvPurchase;
        internal System.Windows.Forms.Button BtnShowItem;
        internal System.Windows.Forms.Button BtnItemCancel;
        internal System.Windows.Forms.Button BtnCancel;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
        private System.Windows.Forms.Panel pnlLedger;
        internal System.Windows.Forms.Button btnLedgerCancel;
        private System.Windows.Forms.CheckBox chckLedgerAll;
        private System.Windows.Forms.DataGridView dgLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        internal System.Windows.Forms.Button btnLedgerShow;
    }
}