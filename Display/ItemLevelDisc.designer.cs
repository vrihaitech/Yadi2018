namespace Yadi.Display
{
    partial class ItemLevelDisc
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
            this.pnlMain = new OMControls.OMBPanel();
            this.rdBrandwise = new System.Windows.Forms.RadioButton();
            this.rdItemwise = new System.Windows.Forms.RadioButton();
            this.rdBillwise = new System.Windows.Forms.RadioButton();
            this.BtnExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(102, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTPFromDate_KeyDown);
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
            this.btnPrint.Location = new System.Drawing.Point(78, 74);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 60);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.rdBrandwise);
            this.pnlMain.Controls.Add(this.rdItemwise);
            this.pnlMain.Controls.Add(this.rdBillwise);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(498, 160);
            this.pnlMain.TabIndex = 109;
            // 
            // rdBrandwise
            // 
            this.rdBrandwise.AutoSize = true;
            this.rdBrandwise.Location = new System.Drawing.Point(305, 45);
            this.rdBrandwise.Name = "rdBrandwise";
            this.rdBrandwise.Size = new System.Drawing.Size(77, 17);
            this.rdBrandwise.TabIndex = 117;
            this.rdBrandwise.Text = "BrandWise";
            this.rdBrandwise.UseVisualStyleBackColor = true;
            // 
            // rdItemwise
            // 
            this.rdItemwise.AutoSize = true;
            this.rdItemwise.Location = new System.Drawing.Point(198, 45);
            this.rdItemwise.Name = "rdItemwise";
            this.rdItemwise.Size = new System.Drawing.Size(69, 17);
            this.rdItemwise.TabIndex = 116;
            this.rdItemwise.Text = "ItemWise";
            this.rdItemwise.UseVisualStyleBackColor = true;
            // 
            // rdBillwise
            // 
            this.rdBillwise.AutoSize = true;
            this.rdBillwise.Checked = true;
            this.rdBillwise.Location = new System.Drawing.Point(95, 45);
            this.rdBillwise.Name = "rdBillwise";
            this.rdBillwise.Size = new System.Drawing.Size(62, 17);
            this.rdBillwise.TabIndex = 115;
            this.rdBillwise.TabStop = true;
            this.rdBillwise.Text = "BillWise";
            this.rdBillwise.UseVisualStyleBackColor = true;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(192, 73);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 60);
            this.BtnExport.TabIndex = 114;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 113;
            this.label2.Text = "To Date :";
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(333, 10);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(126, 23);
            this.DTToDate.TabIndex = 112;
            this.DTToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTToDate_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(305, 73);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 60);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ItemLevelDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 528);
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemLevelDisc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Level Discount";
            this.Load += new System.EventHandler(this.ItemLevelDiscount1_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnPrint;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.RadioButton rdItemwise;
        private System.Windows.Forms.RadioButton rdBillwise;
        private System.Windows.Forms.RadioButton rdBrandwise;
    }
}