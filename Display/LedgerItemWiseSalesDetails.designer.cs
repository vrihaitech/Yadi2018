 namespace Yadi.Display
{
     partial class LedgerItemWiseSalesDetails
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
            this.pnlPartyDetails = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.BtnShowItem = new System.Windows.Forms.Button();
            this.gvParty = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnPartyShow = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlItemDetails = new System.Windows.Forms.Panel();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnItemCancel = new System.Windows.Forms.Button();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.rdDetails = new System.Windows.Forms.RadioButton();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.BtnShow = new System.Windows.Forms.Button();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rbpartywsie = new System.Windows.Forms.RadioButton();
            this.rbpartyitem = new System.Windows.Forms.RadioButton();
            this.pnlPartyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlItemDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(312, 14);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(235, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(101, 14);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(120, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // pnlPartyDetails
            // 
            this.pnlPartyDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPartyDetails.Controls.Add(this.BtnCancel);
            this.pnlPartyDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPartyDetails.Controls.Add(this.BtnShowItem);
            this.pnlPartyDetails.Controls.Add(this.gvParty);
            this.pnlPartyDetails.Location = new System.Drawing.Point(8, 63);
            this.pnlPartyDetails.Name = "pnlPartyDetails";
            this.pnlPartyDetails.Size = new System.Drawing.Size(614, 563);
            this.pnlPartyDetails.TabIndex = 75;
            this.pnlPartyDetails.Visible = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(121, 532);
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
            this.chkPartySelectAll.Location = new System.Drawing.Point(513, 538);
            this.chkPartySelectAll.Name = "chkPartySelectAll";
            this.chkPartySelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkPartySelectAll.TabIndex = 3;
            this.chkPartySelectAll.Text = "SelectAll (F2)";
            this.chkPartySelectAll.UseVisualStyleBackColor = true;
            this.chkPartySelectAll.CheckedChanged += new System.EventHandler(this.chkPartySelectAll_CheckedChanged);
            // 
            // BtnShowItem
            // 
            this.BtnShowItem.Location = new System.Drawing.Point(22, 532);
            this.BtnShowItem.Name = "BtnShowItem";
            this.BtnShowItem.Size = new System.Drawing.Size(93, 27);
            this.BtnShowItem.TabIndex = 5;
            this.BtnShowItem.Text = "Show";
            this.BtnShowItem.UseVisualStyleBackColor = false;
            this.BtnShowItem.Click += new System.EventHandler(this.BtnShowItem_Click);
            // 
            // gvParty
            // 
            this.gvParty.AllowUserToAddRows = false;
            this.gvParty.AllowUserToDeleteRows = false;
            this.gvParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvParty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Chk1});
            this.gvParty.Location = new System.Drawing.Point(22, 32);
            this.gvParty.Name = "gvParty";
            this.gvParty.Size = new System.Drawing.Size(579, 437);
            this.gvParty.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LedgerNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "LedgerNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "LedgerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "PartyName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 450;
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
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnPartyShow
            // 
            this.BtnPartyShow.Location = new System.Drawing.Point(430, 12);
            this.BtnPartyShow.Name = "BtnPartyShow";
            this.BtnPartyShow.Size = new System.Drawing.Size(137, 27);
            this.BtnPartyShow.TabIndex = 2;
            this.BtnPartyShow.Text = "Show Party";
            this.BtnPartyShow.UseVisualStyleBackColor = false;
            this.BtnPartyShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.rbpartyitem);
            this.pnlMain.Controls.Add(this.rbpartywsie);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnPartyShow);
            this.pnlMain.Controls.Add(this.pnlItemDetails);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.pnlPartyDetails);
            this.pnlMain.Location = new System.Drawing.Point(57, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(695, 631);
            this.pnlMain.TabIndex = 75;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(577, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlItemDetails
            // 
            this.pnlItemDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlItemDetails.Controls.Add(this.BtnExport);
            this.pnlItemDetails.Controls.Add(this.BtnItemCancel);
            this.pnlItemDetails.Controls.Add(this.rdSummary);
            this.pnlItemDetails.Controls.Add(this.rdDetails);
            this.pnlItemDetails.Controls.Add(this.chkSelectAll);
            this.pnlItemDetails.Controls.Add(this.BtnShow);
            this.pnlItemDetails.Controls.Add(this.gvItem);
            this.pnlItemDetails.Location = new System.Drawing.Point(10, 65);
            this.pnlItemDetails.Name = "pnlItemDetails";
            this.pnlItemDetails.Size = new System.Drawing.Size(612, 558);
            this.pnlItemDetails.TabIndex = 74;
            this.pnlItemDetails.Visible = false;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(200, 519);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(91, 27);
            this.BtnExport.TabIndex = 79;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnItemCancel
            // 
            this.BtnItemCancel.Location = new System.Drawing.Point(111, 519);
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
            this.rdSummary.Location = new System.Drawing.Point(513, 6);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(68, 17);
            this.rdSummary.TabIndex = 76;
            this.rdSummary.Text = "Summary";
            this.rdSummary.UseVisualStyleBackColor = true;
            // 
            // rdDetails
            // 
            this.rdDetails.AutoSize = true;
            this.rdDetails.Checked = true;
            this.rdDetails.Location = new System.Drawing.Point(425, 5);
            this.rdDetails.Name = "rdDetails";
            this.rdDetails.Size = new System.Drawing.Size(57, 17);
            this.rdDetails.TabIndex = 75;
            this.rdDetails.TabStop = true;
            this.rdDetails.Text = "Details";
            this.rdDetails.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(513, 525);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 3;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(22, 519);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(83, 27);
            this.BtnShow.TabIndex = 5;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
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
            this.gvItem.Location = new System.Drawing.Point(22, 25);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(579, 468);
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
            this.Item.Width = 450;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk.Width = 80;
            // 
            // rbpartywsie
            // 
            this.rbpartywsie.AutoSize = true;
            this.rbpartywsie.Location = new System.Drawing.Point(19, 40);
            this.rbpartywsie.Name = "rbpartywsie";
            this.rbpartywsie.Size = new System.Drawing.Size(73, 17);
            this.rbpartywsie.TabIndex = 77;
            this.rbpartywsie.Text = "Party wsie";
            this.rbpartywsie.UseVisualStyleBackColor = true;
            this.rbpartywsie.CheckedChanged += new System.EventHandler(this.rbpartywsie_CheckedChanged);
            // 
            // rbpartyitem
            // 
            this.rbpartyitem.AutoSize = true;
            this.rbpartyitem.Location = new System.Drawing.Point(131, 40);
            this.rbpartyitem.Name = "rbpartyitem";
            this.rbpartyitem.Size = new System.Drawing.Size(126, 17);
            this.rbpartyitem.TabIndex = 78;
            this.rbpartyitem.Text = "Party wsie - Item wise";
            this.rbpartyitem.UseVisualStyleBackColor = true;
            this.rbpartyitem.CheckedChanged += new System.EventHandler(this.rbpartyitem_CheckedChanged);
            // 
            // LedgerItemWiseSalesDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 651);
            this.Controls.Add(this.pnlMain);
            this.Name = "LedgerItemWiseSalesDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Party ItemWise Details";
            this.Load += new System.EventHandler(this.LedgerItemWiseSalesDetails_Load);
            this.pnlPartyDetails.ResumeLayout(false);
            this.pnlPartyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlItemDetails.ResumeLayout(false);
            this.pnlItemDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        internal System.Windows.Forms.Button BtnPartyShow;
        private System.Windows.Forms.Panel pnlPartyDetails;
        private System.Windows.Forms.CheckBox chkPartySelectAll;
        private System.Windows.Forms.DataGridView gvParty;
        internal System.Windows.Forms.Button BtnShowItem;
        internal System.Windows.Forms.Button BtnCancel;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
        private System.Windows.Forms.Panel pnlItemDetails;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button BtnItemCancel;
        private System.Windows.Forms.RadioButton rdSummary;
        private System.Windows.Forms.RadioButton rdDetails;
        private System.Windows.Forms.CheckBox chkSelectAll;
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.DataGridView gvItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.RadioButton rbpartyitem;
        private System.Windows.Forms.RadioButton rbpartywsie;
    }
}