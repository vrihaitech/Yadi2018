namespace Yadi.Display
{
    partial class PayTypeWiseChargesDetails
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdDaySummary = new System.Windows.Forms.RadioButton();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.rdDaySummary);
            this.pnlMain.Controls.Add(this.rdSummary);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(478, 149);
            this.pnlMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(145, 75);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 60);
            this.btnExit.TabIndex = 115;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(46, 75);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 60);
            this.btnPrint.TabIndex = 114;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdDaySummary
            // 
            this.rdDaySummary.AutoSize = true;
            this.rdDaySummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDaySummary.Location = new System.Drawing.Point(92, 52);
            this.rdDaySummary.Name = "rdDaySummary";
            this.rdDaySummary.Size = new System.Drawing.Size(125, 17);
            this.rdDaySummary.TabIndex = 106;
            this.rdDaySummary.Text = "DayWiseSummary";
            this.rdDaySummary.UseVisualStyleBackColor = true;
            // 
            // rdSummary
            // 
            this.rdSummary.AutoSize = true;
            this.rdSummary.Checked = true;
            this.rdSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSummary.Location = new System.Drawing.Point(13, 52);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(72, 17);
            this.rdSummary.TabIndex = 105;
            this.rdSummary.TabStop = true;
            this.rdSummary.Text = "Detailed";
            this.rdSummary.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 103;
            this.label1.Text = "From Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(107, 14);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(253, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 104;
            this.label2.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(331, 14);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(130, 23);
            this.DTToDate.TabIndex = 102;
            // 
            // PayTypeWiseChargesDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 178);
            this.Controls.Add(this.pnlMain);
            this.Name = "PayTypeWiseChargesDetails";
            this.Text = "Pay Type Wise Charges Details";
            this.Load += new System.EventHandler(this.PayTypeWiseChargesDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.RadioButton rdDaySummary;
        private System.Windows.Forms.RadioButton rdSummary;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnPrint;
    }
}