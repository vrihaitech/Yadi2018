 namespace Yadi.Display
{
     partial class ItemRateDiffDetails
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
            this.btnShow = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlRateType = new System.Windows.Forms.Panel();
            this.btnRateTypeCancel = new System.Windows.Forms.Button();
            this.btnRateTypeOK = new System.Windows.Forms.Button();
            this.txtRateTypePassword = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlRateType.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(328, 25);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(106, 25);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(120, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 31);
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
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(85, 54);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 27);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "SHOW";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnExport);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.btnShow);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(463, 100);
            this.pnlMain.TabIndex = 69;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(301, 54);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(93, 27);
            this.btnExport.TabIndex = 75;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(192, 54);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 27);
            this.btnExit.TabIndex = 69;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlRateType
            // 
            this.pnlRateType.BackColor = System.Drawing.Color.Transparent;
            this.pnlRateType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRateType.Controls.Add(this.btnRateTypeCancel);
            this.pnlRateType.Controls.Add(this.btnRateTypeOK);
            this.pnlRateType.Controls.Add(this.txtRateTypePassword);
            this.pnlRateType.Controls.Add(this.label40);
            this.pnlRateType.Controls.Add(this.label44);
            this.pnlRateType.Location = new System.Drawing.Point(248, 286);
            this.pnlRateType.Name = "pnlRateType";
            this.pnlRateType.Size = new System.Drawing.Size(409, 104);
            this.pnlRateType.TabIndex = 5552;
            this.pnlRateType.Visible = false;
            // 
            // btnRateTypeCancel
            // 
            this.btnRateTypeCancel.Location = new System.Drawing.Point(209, 64);
            this.btnRateTypeCancel.Name = "btnRateTypeCancel";
            this.btnRateTypeCancel.Size = new System.Drawing.Size(75, 23);
            this.btnRateTypeCancel.TabIndex = 709;
            this.btnRateTypeCancel.Text = "Cancel";
            this.btnRateTypeCancel.UseVisualStyleBackColor = true;
            this.btnRateTypeCancel.Click += new System.EventHandler(this.btnRateTypeCancel_Click);
            // 
            // btnRateTypeOK
            // 
            this.btnRateTypeOK.Location = new System.Drawing.Point(110, 64);
            this.btnRateTypeOK.Name = "btnRateTypeOK";
            this.btnRateTypeOK.Size = new System.Drawing.Size(75, 23);
            this.btnRateTypeOK.TabIndex = 706;
            this.btnRateTypeOK.Text = "OK";
            this.btnRateTypeOK.UseVisualStyleBackColor = true;
            this.btnRateTypeOK.Click += new System.EventHandler(this.btnRateTypeOK_Click);
            // 
            // txtRateTypePassword
            // 
            this.txtRateTypePassword.Location = new System.Drawing.Point(137, 30);
            this.txtRateTypePassword.MaxLength = 6;
            this.txtRateTypePassword.Name = "txtRateTypePassword";
            this.txtRateTypePassword.PasswordChar = '*';
            this.txtRateTypePassword.Size = new System.Drawing.Size(187, 20);
            this.txtRateTypePassword.TabIndex = 703;
            this.txtRateTypePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRateTypePassword_KeyDown);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(34, 31);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 13);
            this.label40.TabIndex = 3;
            this.label40.Text = "Password";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Bisque;
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(407, 13);
            this.label44.TabIndex = 0;
            this.label44.Text = "Rate Type Password Details";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemRateDiffDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 676);
            this.Controls.Add(this.pnlRateType);
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemRateDiffDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Rate Difference Details";
            this.Load += new System.EventHandler(this.StockSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlRateType.ResumeLayout(false);
            this.pnlRateType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        internal System.Windows.Forms.Button btnShow;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlRateType;
        private System.Windows.Forms.Button btnRateTypeCancel;
        private System.Windows.Forms.Button btnRateTypeOK;
        private System.Windows.Forms.TextBox txtRateTypePassword;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label44;
        internal System.Windows.Forms.Button btnExport;
    }
}