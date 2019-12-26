namespace Yadi.Display
{
    partial class OutStanding
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
            this.DTPToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.BtnPartyShow = new System.Windows.Forms.Button();
            this.pnlParty = new System.Windows.Forms.Panel();
            this.btnBillWiseDetail = new System.Windows.Forms.Button();
            this.btnBigPrint = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.rbDetails = new System.Windows.Forms.RadioButton();
            this.gvParty = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlParty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).BeginInit();
            this.SuspendLayout();
            // 
            // DTPToDate
            // 
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(81, 7);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(130, 23);
            this.DTPToDate.TabIndex = 0;
            this.DTPToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPToDate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "Date :";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(25, 457);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(80, 29);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.BtnPartyShow);
            this.pnlMain.Controls.Add(this.pnlParty);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTPToDate);
            this.pnlMain.Location = new System.Drawing.Point(21, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(508, 536);
            this.pnlMain.TabIndex = 0;
            // 
            // BtnPartyShow
            // 
            this.BtnPartyShow.Location = new System.Drawing.Point(237, 7);
            this.BtnPartyShow.Name = "BtnPartyShow";
            this.BtnPartyShow.Size = new System.Drawing.Size(165, 27);
            this.BtnPartyShow.TabIndex = 1;
            this.BtnPartyShow.Text = "Show Party ";
            this.BtnPartyShow.UseVisualStyleBackColor = false;
            this.BtnPartyShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // pnlParty
            // 
            this.pnlParty.Controls.Add(this.btnBillWiseDetail);
            this.pnlParty.Controls.Add(this.btnBigPrint);
            this.pnlParty.Controls.Add(this.chkSelectAll);
            this.pnlParty.Controls.Add(this.rbDetails);
            this.pnlParty.Controls.Add(this.gvParty);
            this.pnlParty.Controls.Add(this.rbSummary);
            this.pnlParty.Controls.Add(this.btnShow);
            this.pnlParty.Controls.Add(this.btnExit);
            this.pnlParty.Location = new System.Drawing.Point(6, 36);
            this.pnlParty.Name = "pnlParty";
            this.pnlParty.Size = new System.Drawing.Size(438, 489);
            this.pnlParty.TabIndex = 110;
            this.pnlParty.Visible = false;
            // 
            // btnBillWiseDetail
            // 
            this.btnBillWiseDetail.Location = new System.Drawing.Point(306, 456);
            this.btnBillWiseDetail.Name = "btnBillWiseDetail";
            this.btnBillWiseDetail.Size = new System.Drawing.Size(102, 29);
            this.btnBillWiseDetail.TabIndex = 113;
            this.btnBillWiseDetail.Text = "&Detail Print";
            this.btnBillWiseDetail.UseVisualStyleBackColor = false;
            this.btnBillWiseDetail.Visible = false;
            this.btnBillWiseDetail.Click += new System.EventHandler(this.btnBillWiseDetail_Click);
            // 
            // btnBigPrint
            // 
            this.btnBigPrint.Location = new System.Drawing.Point(218, 457);
            this.btnBigPrint.Name = "btnBigPrint";
            this.btnBigPrint.Size = new System.Drawing.Size(80, 29);
            this.btnBigPrint.TabIndex = 5;
            this.btnBigPrint.Text = "&Big Print";
            this.btnBigPrint.UseVisualStyleBackColor = false;
            this.btnBigPrint.Click += new System.EventHandler(this.btnBigPrint_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(311, 4);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(85, 17);
            this.chkSelectAll.TabIndex = 2;
            this.chkSelectAll.Text = "SelectAll(F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // rbDetails
            // 
            this.rbDetails.AutoSize = true;
            this.rbDetails.Location = new System.Drawing.Point(112, 12);
            this.rbDetails.Name = "rbDetails";
            this.rbDetails.Size = new System.Drawing.Size(57, 17);
            this.rbDetails.TabIndex = 112;
            this.rbDetails.Text = "Details";
            this.rbDetails.UseVisualStyleBackColor = true;
            // 
            // gvParty
            // 
            this.gvParty.AllowUserToAddRows = false;
            this.gvParty.AllowUserToDeleteRows = false;
            this.gvParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvParty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteno,
            this.Item,
            this.Chk});
            this.gvParty.Location = new System.Drawing.Point(25, 35);
            this.gvParty.Name = "gvParty";
            this.gvParty.Size = new System.Drawing.Size(401, 416);
            this.gvParty.TabIndex = 3;
            // 
            // Iteno
            // 
            this.Iteno.DataPropertyName = "LedgerNo";
            this.Iteno.HeaderText = "LedgerNo";
            this.Iteno.Name = "Iteno";
            this.Iteno.Visible = false;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "LedgerName";
            this.Item.HeaderText = "Party";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 250;
            // 
            // Chk
            // 
            this.Chk.DataPropertyName = "Chk";
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Checked = true;
            this.rbSummary.Location = new System.Drawing.Point(25, 12);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(68, 17);
            this.rbSummary.TabIndex = 111;
            this.rbSummary.TabStop = true;
            this.rbSummary.Text = "Summary";
            this.rbSummary.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(122, 457);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 29);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // OutStanding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 545);
            this.Controls.Add(this.pnlMain);
            this.Name = "OutStanding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OutStanding";
            this.Load += new System.EventHandler(this.OutStanding_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlParty.ResumeLayout(false);
            this.pnlParty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnShow;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbDetails;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.Panel pnlParty;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView gvParty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        internal System.Windows.Forms.Button BtnPartyShow;
        internal System.Windows.Forms.Button btnBigPrint;
        internal System.Windows.Forms.Button btnBillWiseDetail;
    }
}