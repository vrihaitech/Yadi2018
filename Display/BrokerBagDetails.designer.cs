namespace Yadi.Display
{
    partial class BrokerBagDetails
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
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlPartyDetails = new System.Windows.Forms.Panel();
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.gvParty = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnShow = new System.Windows.Forms.Button();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlPartyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlPartyDetails);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnShow);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(9, 14);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(666, 511);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlPartyDetails
            // 
            this.pnlPartyDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPartyDetails.Controls.Add(this.gvParty);
            this.pnlPartyDetails.Controls.Add(this.btnPrint);
            this.pnlPartyDetails.Location = new System.Drawing.Point(16, 45);
            this.pnlPartyDetails.Name = "pnlPartyDetails";
            this.pnlPartyDetails.Size = new System.Drawing.Size(628, 448);
            this.pnlPartyDetails.TabIndex = 76;
            this.pnlPartyDetails.Visible = false;
            // 
            // chkPartySelectAll
            // 
            this.chkPartySelectAll.AutoSize = true;
            this.chkPartySelectAll.Location = new System.Drawing.Point(348, 412);
            this.chkPartySelectAll.Name = "chkPartySelectAll";
            this.chkPartySelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkPartySelectAll.TabIndex = 3;
            this.chkPartySelectAll.Text = "SelectAll (F2)";
            this.chkPartySelectAll.UseVisualStyleBackColor = true;
            this.chkPartySelectAll.CheckedChanged += new System.EventHandler(this.chkPartySelectAll_CheckedChanged);
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
            this.gvParty.Location = new System.Drawing.Point(13, 11);
            this.gvParty.Name = "gvParty";
            this.gvParty.Size = new System.Drawing.Size(423, 395);
            this.gvParty.TabIndex = 4;
            this.gvParty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvParty_KeyDown);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(13, 412);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(554, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 67;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "From Date :";
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(459, 11);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(90, 27);
            this.BtnShow.TabIndex = 64;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(106, 13);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 62;
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(323, 13);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(129, 23);
            this.DTToDate.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 66;
            this.label2.Text = "To Date :";
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
            this.dataGridViewTextBoxColumn2.HeaderText = "Broker Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
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
            // BrokerBagDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 534);
            this.Controls.Add(this.pnlMain);
            this.Name = "BrokerBagDetails";
            this.Text = "Broker Bag Details";
            this.Load += new System.EventHandler(this.BrokerBagDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlPartyDetails.ResumeLayout(false);
            this.pnlPartyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlPartyDetails;
        private System.Windows.Forms.CheckBox chkPartySelectAll;
        private System.Windows.Forms.DataGridView gvParty;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
    }
}