namespace Yadi.Display
{
    partial class SalesRegisterSummary
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
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.rdDaySummary = new System.Windows.Forms.RadioButton();
            this.pnlMain = new OMControls.OMBPanel();
            this.rdDetailed = new System.Windows.Forms.RadioButton();
            this.rdDisc = new System.Windows.Forms.RadioButton();
            this.btnBigPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.rdQuarterSummary = new System.Windows.Forms.RadioButton();
            this.rdMonthSummary = new System.Windows.Forms.RadioButton();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(326, 10);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(130, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(248, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(102, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "From Date :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(53, 88);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 60);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdSummary
            // 
            this.rdSummary.AutoSize = true;
            this.rdSummary.Checked = true;
            this.rdSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSummary.Location = new System.Drawing.Point(4, 51);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(75, 17);
            this.rdSummary.TabIndex = 2;
            this.rdSummary.TabStop = true;
            this.rdSummary.Text = "Summary";
            this.rdSummary.UseVisualStyleBackColor = true;
            // 
            // rdDaySummary
            // 
            this.rdDaySummary.AutoSize = true;
            this.rdDaySummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDaySummary.Location = new System.Drawing.Point(168, 51);
            this.rdDaySummary.Name = "rdDaySummary";
            this.rdDaySummary.Size = new System.Drawing.Size(125, 17);
            this.rdDaySummary.TabIndex = 4;
            this.rdDaySummary.Text = "DayWiseSummary";
            this.rdDaySummary.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.rdDetailed);
            this.pnlMain.Controls.Add(this.rdDisc);
            this.pnlMain.Controls.Add(this.btnBigPrint);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.rdQuarterSummary);
            this.pnlMain.Controls.Add(this.rdMonthSummary);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.rdDaySummary);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.rdSummary);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(697, 157);
            this.pnlMain.TabIndex = 109;
            // 
            // rdDetailed
            // 
            this.rdDetailed.AutoSize = true;
            this.rdDetailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDetailed.Location = new System.Drawing.Point(85, 51);
            this.rdDetailed.Name = "rdDetailed";
            this.rdDetailed.Size = new System.Drawing.Size(72, 17);
            this.rdDetailed.TabIndex = 3;
            this.rdDetailed.Text = "Detailed";
            this.rdDetailed.UseVisualStyleBackColor = true;
            // 
            // rdDisc
            // 
            this.rdDisc.AutoSize = true;
            this.rdDisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDisc.ForeColor = System.Drawing.Color.Black;
            this.rdDisc.Location = new System.Drawing.Point(583, 51);
            this.rdDisc.Name = "rdDisc";
            this.rdDisc.Size = new System.Drawing.Size(100, 17);
            this.rdDisc.TabIndex = 7;
            this.rdDisc.Text = "DiscSummary";
            this.rdDisc.UseVisualStyleBackColor = true;
            this.rdDisc.Visible = false;
            // 
            // btnBigPrint
            // 
            this.btnBigPrint.Location = new System.Drawing.Point(257, 88);
            this.btnBigPrint.Name = "btnBigPrint";
            this.btnBigPrint.Size = new System.Drawing.Size(93, 60);
            this.btnBigPrint.TabIndex = 10;
            this.btnBigPrint.Text = "&Big Print";
            this.btnBigPrint.UseVisualStyleBackColor = false;
            this.btnBigPrint.Click += new System.EventHandler(this.btnBigPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(359, 88);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 60);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(155, 88);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 60);
            this.BtnExport.TabIndex = 9;
            this.BtnExport.Text = "&Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // rdQuarterSummary
            // 
            this.rdQuarterSummary.AutoSize = true;
            this.rdQuarterSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdQuarterSummary.Location = new System.Drawing.Point(437, 51);
            this.rdQuarterSummary.Name = "rdQuarterSummary";
            this.rdQuarterSummary.Size = new System.Drawing.Size(145, 17);
            this.rdQuarterSummary.TabIndex = 6;
            this.rdQuarterSummary.Text = "QuarterWiseSummary";
            this.rdQuarterSummary.UseVisualStyleBackColor = true;
            // 
            // rdMonthSummary
            // 
            this.rdMonthSummary.AutoSize = true;
            this.rdMonthSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMonthSummary.Location = new System.Drawing.Point(295, 51);
            this.rdMonthSummary.Name = "rdMonthSummary";
            this.rdMonthSummary.Size = new System.Drawing.Size(138, 17);
            this.rdMonthSummary.TabIndex = 5;
            this.rdMonthSummary.Text = "MonthWiseSummary";
            this.rdMonthSummary.UseVisualStyleBackColor = true;
            // 
            // SalesRegisterSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 279);
            this.Controls.Add(this.pnlMain);
            this.Name = "SalesRegisterSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Return Register";
            this.Load += new System.EventHandler(this.SalesRegisterSummary_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdSummary;
        private System.Windows.Forms.RadioButton rdDaySummary;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.RadioButton rdMonthSummary;
        private System.Windows.Forms.RadioButton rdQuarterSummary;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnBigPrint;
        private System.Windows.Forms.RadioButton rdDisc;
        private System.Windows.Forms.RadioButton rdDetailed;
    }
}