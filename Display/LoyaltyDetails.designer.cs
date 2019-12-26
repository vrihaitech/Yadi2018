namespace Yadi.Display
{
    partial class LoyaltyDetails
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
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdInstant = new System.Windows.Forms.RadioButton();
            this.pnlMain = new OMControls.OMBPanel();
            this.chkIsIwScheme = new System.Windows.Forms.CheckBox();
            this.pnlInstant = new OMControls.OMBPanel();
            this.rdPartyDetails = new System.Windows.Forms.RadioButton();
            this.rdDayDetails = new System.Windows.Forms.RadioButton();
            this.rbDetails = new System.Windows.Forms.RadioButton();
            this.rbSummary = new System.Windows.Forms.RadioButton();
            this.dgInstant = new System.Windows.Forms.DataGridView();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LSelec = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SchemeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlInstant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInstant)).BeginInit();
            this.SuspendLayout();
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(106, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "From Date :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(12, 351);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdInstant
            // 
            this.rdInstant.AutoSize = true;
            this.rdInstant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdInstant.Location = new System.Drawing.Point(12, 48);
            this.rdInstant.Name = "rdInstant";
            this.rdInstant.Size = new System.Drawing.Size(64, 17);
            this.rdInstant.TabIndex = 2;
            this.rdInstant.Text = "Instant";
            this.rdInstant.UseVisualStyleBackColor = true;
            this.rdInstant.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.chkIsIwScheme);
            this.pnlMain.Controls.Add(this.pnlInstant);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.rbMonthly);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.rdInstant);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(582, 398);
            this.pnlMain.TabIndex = 109;
            // 
            // chkIsIwScheme
            // 
            this.chkIsIwScheme.AutoSize = true;
            this.chkIsIwScheme.Location = new System.Drawing.Point(483, 17);
            this.chkIsIwScheme.Name = "chkIsIwScheme";
            this.chkIsIwScheme.Size = new System.Drawing.Size(84, 17);
            this.chkIsIwScheme.TabIndex = 115;
            this.chkIsIwScheme.Text = "IsIwScheme";
            this.chkIsIwScheme.UseVisualStyleBackColor = true;
            // 
            // pnlInstant
            // 
            this.pnlInstant.BorderColor = System.Drawing.Color.Gray;
            this.pnlInstant.BorderRadius = 3;
            this.pnlInstant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInstant.Controls.Add(this.rdPartyDetails);
            this.pnlInstant.Controls.Add(this.rdDayDetails);
            this.pnlInstant.Controls.Add(this.rbDetails);
            this.pnlInstant.Controls.Add(this.rbSummary);
            this.pnlInstant.Controls.Add(this.dgInstant);
            this.pnlInstant.Location = new System.Drawing.Point(8, 70);
            this.pnlInstant.Name = "pnlInstant";
            this.pnlInstant.Size = new System.Drawing.Size(563, 263);
            this.pnlInstant.TabIndex = 114;
            this.pnlInstant.Visible = false;
            // 
            // rdPartyDetails
            // 
            this.rdPartyDetails.AutoSize = true;
            this.rdPartyDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPartyDetails.Location = new System.Drawing.Point(351, 222);
            this.rdPartyDetails.Name = "rdPartyDetails";
            this.rdPartyDetails.Size = new System.Drawing.Size(93, 17);
            this.rdPartyDetails.TabIndex = 118;
            this.rdPartyDetails.TabStop = true;
            this.rdPartyDetails.Text = "PartyDetails";
            this.rdPartyDetails.UseVisualStyleBackColor = true;
            // 
            // rdDayDetails
            // 
            this.rdDayDetails.AutoSize = true;
            this.rdDayDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDayDetails.Location = new System.Drawing.Point(225, 222);
            this.rdDayDetails.Name = "rdDayDetails";
            this.rdDayDetails.Size = new System.Drawing.Size(86, 17);
            this.rdDayDetails.TabIndex = 117;
            this.rdDayDetails.TabStop = true;
            this.rdDayDetails.Text = "DayDetails";
            this.rdDayDetails.UseVisualStyleBackColor = true;
            // 
            // rbDetails
            // 
            this.rbDetails.AutoSize = true;
            this.rbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDetails.Location = new System.Drawing.Point(119, 222);
            this.rbDetails.Name = "rbDetails";
            this.rbDetails.Size = new System.Drawing.Size(81, 17);
            this.rbDetails.TabIndex = 116;
            this.rbDetails.Text = "BillDetails";
            this.rbDetails.UseVisualStyleBackColor = true;
            // 
            // rbSummary
            // 
            this.rbSummary.AutoSize = true;
            this.rbSummary.Checked = true;
            this.rbSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSummary.Location = new System.Drawing.Point(14, 221);
            this.rbSummary.Name = "rbSummary";
            this.rbSummary.Size = new System.Drawing.Size(75, 17);
            this.rbSummary.TabIndex = 115;
            this.rbSummary.TabStop = true;
            this.rbSummary.Text = "Summary";
            this.rbSummary.UseVisualStyleBackColor = true;
            // 
            // dgInstant
            // 
            this.dgInstant.AllowUserToAddRows = false;
            this.dgInstant.AllowUserToDeleteRows = false;
            this.dgInstant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInstant.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.SchemeTypeNo,
            this.SchemeType,
            this.SchemeFrom,
            this.SchemeTo,
            this.SchemeDate,
            this.LSelec,
            this.SchemeNo});
            this.dgInstant.Location = new System.Drawing.Point(11, 21);
            this.dgInstant.Name = "dgInstant";
            this.dgInstant.Size = new System.Drawing.Size(537, 194);
            this.dgInstant.TabIndex = 0;
            this.dgInstant.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgInstant_CellFormatting);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(334, 13);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(129, 23);
            this.DTToDate.TabIndex = 112;
            this.DTToDate.ValueChanged += new System.EventHandler(this.DTToDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 113;
            this.label2.Text = "To Date :";
            // 
            // rbMonthly
            // 
            this.rbMonthly.AutoSize = true;
            this.rbMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMonthly.Location = new System.Drawing.Point(114, 47);
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.Size = new System.Drawing.Size(69, 17);
            this.rbMonthly.TabIndex = 111;
            this.rbMonthly.Text = "Monthly";
            this.rbMonthly.UseVisualStyleBackColor = true;
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(111, 351);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(312, 351);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 27);
            this.BtnExport.TabIndex = 103;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Visible = false;
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "srno";
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // SchemeTypeNo
            // 
            this.SchemeTypeNo.DataPropertyName = "SchemeTypeNo";
            this.SchemeTypeNo.HeaderText = "SchemeTypeNo";
            this.SchemeTypeNo.Name = "SchemeTypeNo";
            this.SchemeTypeNo.ReadOnly = true;
            this.SchemeTypeNo.Visible = false;
            // 
            // SchemeType
            // 
            this.SchemeType.DataPropertyName = "SchemeType";
            this.SchemeType.HeaderText = "SchemeType";
            this.SchemeType.Name = "SchemeType";
            this.SchemeType.ReadOnly = true;
            this.SchemeType.Width = 200;
            // 
            // SchemeFrom
            // 
            this.SchemeFrom.DataPropertyName = "SchemePeriodFrom";
            this.SchemeFrom.HeaderText = "SchemeFrom";
            this.SchemeFrom.Name = "SchemeFrom";
            this.SchemeFrom.ReadOnly = true;
            // 
            // SchemeTo
            // 
            this.SchemeTo.DataPropertyName = "SchemePeriodTo";
            this.SchemeTo.HeaderText = "SchemeTo";
            this.SchemeTo.Name = "SchemeTo";
            this.SchemeTo.ReadOnly = true;
            // 
            // SchemeDate
            // 
            this.SchemeDate.DataPropertyName = "SchemeDate";
            this.SchemeDate.HeaderText = "SchemeDate";
            this.SchemeDate.Name = "SchemeDate";
            this.SchemeDate.ReadOnly = true;
            this.SchemeDate.Visible = false;
            // 
            // LSelec
            // 
            this.LSelec.DataPropertyName = "Select";
            this.LSelec.FalseValue = "False";
            this.LSelec.HeaderText = "Select";
            this.LSelec.Name = "LSelec";
            this.LSelec.TrueValue = "True";
            this.LSelec.Width = 70;
            // 
            // SchemeNo
            // 
            this.SchemeNo.DataPropertyName = "SchemeNo";
            this.SchemeNo.HeaderText = "SchemeNo";
            this.SchemeNo.Name = "SchemeNo";
            this.SchemeNo.ReadOnly = true;
            this.SchemeNo.Visible = false;
            // 
            // LoyaltyDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 451);
            this.Controls.Add(this.pnlMain);
            this.Name = "LoyaltyDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loyalty Details";
            this.Load += new System.EventHandler(this.LoyaltyDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlInstant.ResumeLayout(false);
            this.pnlInstant.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInstant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdInstant;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbMonthly;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        private OMControls.OMBPanel pnlInstant;
        private System.Windows.Forms.DataGridView dgInstant;
        private System.Windows.Forms.RadioButton rbDetails;
        private System.Windows.Forms.RadioButton rbSummary;
        private System.Windows.Forms.RadioButton rdDayDetails;
        private System.Windows.Forms.RadioButton rdPartyDetails;
        private System.Windows.Forms.CheckBox chkIsIwScheme;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LSelec;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeNo;
    }
}