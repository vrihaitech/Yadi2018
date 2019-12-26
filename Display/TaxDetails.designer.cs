namespace Yadi.Display
{
    partial class TaxDetails
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
            this.rdBillSummary = new System.Windows.Forms.RadioButton();
            this.rdBillWiseDetails = new System.Windows.Forms.RadioButton();
            this.pnlMain = new OMControls.OMBPanel();
            this.rdFormB2BPayType = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNewExcel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.rdForm3B = new System.Windows.Forms.RadioButton();
            this.rdHSNCodeSummary = new System.Windows.Forms.RadioButton();
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
            // rdBillSummary
            // 
            this.rdBillSummary.AutoSize = true;
            this.rdBillSummary.Checked = true;
            this.rdBillSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBillSummary.Location = new System.Drawing.Point(23, 53);
            this.rdBillSummary.Name = "rdBillSummary";
            this.rdBillSummary.Size = new System.Drawing.Size(92, 17);
            this.rdBillSummary.TabIndex = 2;
            this.rdBillSummary.TabStop = true;
            this.rdBillSummary.Text = "BillSummary";
            this.rdBillSummary.UseVisualStyleBackColor = true;
            this.rdBillSummary.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // rdBillWiseDetails
            // 
            this.rdBillWiseDetails.AutoSize = true;
            this.rdBillWiseDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBillWiseDetails.Location = new System.Drawing.Point(119, 53);
            this.rdBillWiseDetails.Name = "rdBillWiseDetails";
            this.rdBillWiseDetails.Size = new System.Drawing.Size(109, 17);
            this.rdBillWiseDetails.TabIndex = 3;
            this.rdBillWiseDetails.Text = "BillWiseDetails";
            this.rdBillWiseDetails.UseVisualStyleBackColor = true;
            this.rdBillWiseDetails.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.rdFormB2BPayType);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.btnNewExcel);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.rdForm3B);
            this.pnlMain.Controls.Add(this.rdHSNCodeSummary);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.rdBillWiseDetails);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.rdBillSummary);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(688, 136);
            this.pnlMain.TabIndex = 109;
            // 
            // rdFormB2BPayType
            // 
            this.rdFormB2BPayType.AutoSize = true;
            this.rdFormB2BPayType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdFormB2BPayType.Location = new System.Drawing.Point(470, 53);
            this.rdFormB2BPayType.Name = "rdFormB2BPayType";
            this.rdFormB2BPayType.Size = new System.Drawing.Size(124, 17);
            this.rdFormB2BPayType.TabIndex = 6;
            this.rdFormB2BPayType.Text = "FormB2BPayType";
            this.rdFormB2BPayType.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(432, 92);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 27);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnNewExcel
            // 
            this.btnNewExcel.Location = new System.Drawing.Point(221, 92);
            this.btnNewExcel.Name = "btnNewExcel";
            this.btnNewExcel.Size = new System.Drawing.Size(116, 27);
            this.btnNewExcel.TabIndex = 112;
            this.btnNewExcel.Text = "Excel BillWise";
            this.btnNewExcel.UseVisualStyleBackColor = false;
            this.btnNewExcel.Visible = false;
            this.btnNewExcel.Click += new System.EventHandler(this.btnNewExcel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(125, 92);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(23, 92);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 27);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // rdForm3B
            // 
            this.rdForm3B.AutoSize = true;
            this.rdForm3B.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdForm3B.Location = new System.Drawing.Point(397, 54);
            this.rdForm3B.Name = "rdForm3B";
            this.rdForm3B.Size = new System.Drawing.Size(67, 17);
            this.rdForm3B.TabIndex = 5;
            this.rdForm3B.Text = "Form3B";
            this.rdForm3B.UseVisualStyleBackColor = true;
            this.rdForm3B.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // rdHSNCodeSummary
            // 
            this.rdHSNCodeSummary.AutoSize = true;
            this.rdHSNCodeSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdHSNCodeSummary.Location = new System.Drawing.Point(234, 53);
            this.rdHSNCodeSummary.Name = "rdHSNCodeSummary";
            this.rdHSNCodeSummary.Size = new System.Drawing.Size(158, 17);
            this.rdHSNCodeSummary.TabIndex = 4;
            this.rdHSNCodeSummary.Text = "HSNCodeWiseSummary";
            this.rdHSNCodeSummary.UseVisualStyleBackColor = true;
            this.rdHSNCodeSummary.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // TaxDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 383);
            this.Controls.Add(this.pnlMain);
            this.Name = "TaxDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaxDetails";
            this.Load += new System.EventHandler(this.TaxDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdBillSummary;
        private System.Windows.Forms.RadioButton rdBillWiseDetails;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.RadioButton rdHSNCodeSummary;
        private System.Windows.Forms.RadioButton rdForm3B;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnNewExcel;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdFormB2BPayType;
    }
}