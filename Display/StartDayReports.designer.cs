namespace Yadi.Display
{
    partial class StartDayReports
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
            this.btnStocksummary = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DTStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDayReports = new System.Windows.Forms.Button();
            this.DTEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnOutstanding = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnOutstanding);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnStocksummary);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTStartDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnDayReports);
            this.pnlMain.Controls.Add(this.DTEndDate);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(461, 214);
            this.pnlMain.TabIndex = 111;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(205, 142);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 60);
            this.btnExit.TabIndex = 101;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStocksummary
            // 
            this.btnStocksummary.Location = new System.Drawing.Point(23, 78);
            this.btnStocksummary.Name = "btnStocksummary";
            this.btnStocksummary.Size = new System.Drawing.Size(157, 60);
            this.btnStocksummary.TabIndex = 2;
            this.btnStocksummary.Text = "Stock Summary";
            this.btnStocksummary.UseVisualStyleBackColor = false;
            this.btnStocksummary.Click += new System.EventHandler(this.btnStocksummary_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(216, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "Start Date :";
            // 
            // DTStartDate
            // 
            this.DTStartDate.Enabled = false;
            this.DTStartDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTStartDate.Location = new System.Drawing.Point(315, 31);
            this.DTStartDate.Name = "DTStartDate";
            this.DTStartDate.Size = new System.Drawing.Size(131, 23);
            this.DTStartDate.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "End Date :";
            // 
            // btnDayReports
            // 
            this.btnDayReports.Location = new System.Drawing.Point(23, 12);
            this.btnDayReports.Name = "btnDayReports";
            this.btnDayReports.Size = new System.Drawing.Size(157, 60);
            this.btnDayReports.TabIndex = 1;
            this.btnDayReports.Text = "Day Reports";
            this.btnDayReports.UseVisualStyleBackColor = false;
            this.btnDayReports.Click += new System.EventHandler(this.btnDayReports_Click);
            // 
            // DTEndDate
            // 
            this.DTEndDate.Enabled = false;
            this.DTEndDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTEndDate.Location = new System.Drawing.Point(315, 90);
            this.DTEndDate.Name = "DTEndDate";
            this.DTEndDate.Size = new System.Drawing.Size(130, 23);
            this.DTEndDate.TabIndex = 1;
            // 
            // btnOutstanding
            // 
            this.btnOutstanding.Location = new System.Drawing.Point(23, 142);
            this.btnOutstanding.Name = "btnOutstanding";
            this.btnOutstanding.Size = new System.Drawing.Size(157, 60);
            this.btnOutstanding.TabIndex = 102;
            this.btnOutstanding.Text = "Outstanding";
            this.btnOutstanding.UseVisualStyleBackColor = false;
            this.btnOutstanding.Click += new System.EventHandler(this.btnOutstanding_Click);
            // 
            // StartDayReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 282);
            this.Controls.Add(this.pnlMain);
            this.Name = "StartDayReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Day Reports";
            this.Load += new System.EventHandler(this.MSIDailyBusiness_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnStocksummary;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker DTStartDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnDayReports;
        internal System.Windows.Forms.DateTimePicker DTEndDate;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnOutstanding;

    }
}