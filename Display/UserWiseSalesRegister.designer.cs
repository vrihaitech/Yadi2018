 namespace Yadi.Display
{
    partial class UserWiseSalesRegister
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnShow = new System.Windows.Forms.Button();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.rdDetails = new System.Windows.Forms.RadioButton();
            this.pnlMain = new OMControls.OMBPanel();
            this.rdQuarterSummary = new System.Windows.Forms.RadioButton();
            this.rdMonthSummary = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnBigPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(343, 12);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(250, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(121, 12);
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
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(7, 94);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 2;
            this.BtnShow.Text = "Print";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // rdSummary
            // 
            this.rdSummary.AutoSize = true;
            this.rdSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSummary.Location = new System.Drawing.Point(81, 56);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(75, 17);
            this.rdSummary.TabIndex = 78;
            this.rdSummary.Text = "Summary";
            this.rdSummary.UseVisualStyleBackColor = true;
            // 
            // rdDetails
            // 
            this.rdDetails.AutoSize = true;
            this.rdDetails.Checked = true;
            this.rdDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDetails.Location = new System.Drawing.Point(10, 56);
            this.rdDetails.Name = "rdDetails";
            this.rdDetails.Size = new System.Drawing.Size(64, 17);
            this.rdDetails.TabIndex = 77;
            this.rdDetails.TabStop = true;
            this.rdDetails.Text = "Details";
            this.rdDetails.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnBigPrint);
            this.pnlMain.Controls.Add(this.rdQuarterSummary);
            this.pnlMain.Controls.Add(this.rdMonthSummary);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnExport);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.rdSummary);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.rdDetails);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.BtnShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(464, 137);
            this.pnlMain.TabIndex = 79;
            // 
            // rdQuarterSummary
            // 
            this.rdQuarterSummary.AutoSize = true;
            this.rdQuarterSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdQuarterSummary.Location = new System.Drawing.Point(314, 56);
            this.rdQuarterSummary.Name = "rdQuarterSummary";
            this.rdQuarterSummary.Size = new System.Drawing.Size(145, 17);
            this.rdQuarterSummary.TabIndex = 104;
            this.rdQuarterSummary.Text = "QuarterWiseSummary";
            this.rdQuarterSummary.UseVisualStyleBackColor = true;
            // 
            // rdMonthSummary
            // 
            this.rdMonthSummary.AutoSize = true;
            this.rdMonthSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdMonthSummary.Location = new System.Drawing.Point(166, 56);
            this.rdMonthSummary.Name = "rdMonthSummary";
            this.rdMonthSummary.Size = new System.Drawing.Size(138, 17);
            this.rdMonthSummary.TabIndex = 103;
            this.rdMonthSummary.Text = "MonthWiseSummary";
            this.rdMonthSummary.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(216, 94);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 80;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(112, 94);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(93, 27);
            this.btnExport.TabIndex = 79;
            this.btnExport.Text = "Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnBigPrint
            // 
            this.btnBigPrint.Location = new System.Drawing.Point(314, 94);
            this.btnBigPrint.Name = "btnBigPrint";
            this.btnBigPrint.Size = new System.Drawing.Size(93, 27);
            this.btnBigPrint.TabIndex = 105;
            this.btnBigPrint.Text = "Big Print";
            this.btnBigPrint.UseVisualStyleBackColor = false;
            this.btnBigPrint.Click += new System.EventHandler(this.btnBigPrint_Click);
            // 
            // UserWiseSalesRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 421);
            this.Controls.Add(this.pnlMain);
            this.Name = "UserWiseSalesRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserWiseItemSalesDetails";
            this.Load += new System.EventHandler(this.UserWiseSalesDetails_Load);
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
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.RadioButton rdSummary;
        private System.Windows.Forms.RadioButton rdDetails;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExport;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rdQuarterSummary;
        private System.Windows.Forms.RadioButton rdMonthSummary;
        internal System.Windows.Forms.Button btnBigPrint;
    }
}