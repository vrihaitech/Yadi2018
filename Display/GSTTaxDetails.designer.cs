namespace Yadi.Display
{
    partial class GSTTaxDetails
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
            this.pnlMain = new OMControls.OMBPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlVatNo = new System.Windows.Forms.Panel();
            this.chkShowVatNo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdSales = new System.Windows.Forms.RadioButton();
            this.rdPurchase = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rdGSTR1 = new System.Windows.Forms.RadioButton();
            this.rdForm3B = new System.Windows.Forms.RadioButton();
            this.pnlMain.SuspendLayout();
            this.pnlVatNo.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.radioButton1);
            this.pnlMain.Controls.Add(this.rdGSTR1);
            this.pnlMain.Controls.Add(this.rdForm3B);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.pnlVatNo);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(519, 174);
            this.pnlMain.TabIndex = 109;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(204, 136);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 112;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pnlVatNo
            // 
            this.pnlVatNo.Controls.Add(this.chkShowVatNo);
            this.pnlVatNo.Controls.Add(this.label3);
            this.pnlVatNo.Enabled = false;
            this.pnlVatNo.Location = new System.Drawing.Point(2, 40);
            this.pnlVatNo.Name = "pnlVatNo";
            this.pnlVatNo.Size = new System.Drawing.Size(172, 23);
            this.pnlVatNo.TabIndex = 111;
            // 
            // chkShowVatNo
            // 
            this.chkShowVatNo.AutoSize = true;
            this.chkShowVatNo.Location = new System.Drawing.Point(98, 6);
            this.chkShowVatNo.Name = "chkShowVatNo";
            this.chkShowVatNo.Size = new System.Drawing.Size(15, 14);
            this.chkShowVatNo.TabIndex = 1;
            this.chkShowVatNo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Show VatNo :";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(108, 136);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(6, 136);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 27);
            this.BtnExport.TabIndex = 103;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdSales);
            this.panel1.Controls.Add(this.rdPurchase);
            this.panel1.Location = new System.Drawing.Point(6, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 32);
            this.panel1.TabIndex = 121;
            // 
            // rdSales
            // 
            this.rdSales.AutoSize = true;
            this.rdSales.Checked = true;
            this.rdSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSales.Location = new System.Drawing.Point(10, 8);
            this.rdSales.Name = "rdSales";
            this.rdSales.Size = new System.Drawing.Size(56, 17);
            this.rdSales.TabIndex = 118;
            this.rdSales.TabStop = true;
            this.rdSales.Text = "Sales";
            this.rdSales.UseVisualStyleBackColor = true;
            // 
            // rdPurchase
            // 
            this.rdPurchase.AutoSize = true;
            this.rdPurchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPurchase.Location = new System.Drawing.Point(87, 8);
            this.rdPurchase.Name = "rdPurchase";
            this.rdPurchase.Size = new System.Drawing.Size(78, 17);
            this.rdPurchase.TabIndex = 117;
            this.rdPurchase.Text = "Purchase";
            this.rdPurchase.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(178, 108);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(109, 17);
            this.radioButton1.TabIndex = 120;
            this.radioButton1.Text = "GSTR1 Details";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rdGSTR1
            // 
            this.rdGSTR1.AutoSize = true;
            this.rdGSTR1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdGSTR1.Location = new System.Drawing.Point(99, 108);
            this.rdGSTR1.Name = "rdGSTR1";
            this.rdGSTR1.Size = new System.Drawing.Size(66, 17);
            this.rdGSTR1.TabIndex = 119;
            this.rdGSTR1.Text = "GSTR1";
            this.rdGSTR1.UseVisualStyleBackColor = true;
            // 
            // rdForm3B
            // 
            this.rdForm3B.AutoSize = true;
            this.rdForm3B.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdForm3B.Location = new System.Drawing.Point(8, 108);
            this.rdForm3B.Name = "rdForm3B";
            this.rdForm3B.Size = new System.Drawing.Size(71, 17);
            this.rdForm3B.TabIndex = 118;
            this.rdForm3B.Text = "Form 3B";
            this.rdForm3B.UseVisualStyleBackColor = true;
            // 
            // GSTTaxDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 528);
            this.Controls.Add(this.pnlMain);
            this.Name = "GSTTaxDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaxDetails";
            this.Load += new System.EventHandler(this.TaxDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlVatNo.ResumeLayout(false);
            this.pnlVatNo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlVatNo;
        private System.Windows.Forms.CheckBox chkShowVatNo;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdSales;
        private System.Windows.Forms.RadioButton rdPurchase;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdGSTR1;
        private System.Windows.Forms.RadioButton rdForm3B;
    }
}