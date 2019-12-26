namespace Yadi.Display
{
     partial class PartywiseQuotationDetails
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
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.gvParty = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnShow = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnPartyShow = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPartyDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
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
            // pnlPartyDetails
            // 
            this.pnlPartyDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPartyDetails.Controls.Add(this.gvParty);
            this.pnlPartyDetails.Controls.Add(this.btnShow);
            this.pnlPartyDetails.Location = new System.Drawing.Point(5, 35);
            this.pnlPartyDetails.Name = "pnlPartyDetails";
            this.pnlPartyDetails.Size = new System.Drawing.Size(533, 474);
            this.pnlPartyDetails.TabIndex = 75;
            this.pnlPartyDetails.Visible = false;
            // 
            // chkPartySelectAll
            // 
            this.chkPartySelectAll.AutoSize = true;
            this.chkPartySelectAll.Location = new System.Drawing.Point(433, 430);
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
            this.gvParty.Size = new System.Drawing.Size(508, 395);
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
            this.dataGridViewTextBoxColumn2.Width = 350;
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
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(13, 424);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 27);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.BtnShow_Click);
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
            this.BtnPartyShow.Location = new System.Drawing.Point(401, 2);
            this.BtnPartyShow.Name = "BtnPartyShow";
            this.BtnPartyShow.Size = new System.Drawing.Size(137, 27);
            this.BtnPartyShow.TabIndex = 2;
            this.BtnPartyShow.Text = "Show Party";
            this.BtnPartyShow.UseVisualStyleBackColor = false;
            this.BtnPartyShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.pnlPartyDetails);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnPartyShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(6, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(553, 555);
            this.pnlMain.TabIndex = 75;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(8, 515);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // PartywiseGRNDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 676);
            this.Controls.Add(this.pnlMain);
            this.Name = "PartywiseQuotationDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partywise Quotation Details";
            this.Load += new System.EventHandler(this.PartywiseQuotationDetails_Load);
            this.pnlPartyDetails.ResumeLayout(false);
            this.pnlPartyDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
        internal System.Windows.Forms.Button btnShow;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
    }
}