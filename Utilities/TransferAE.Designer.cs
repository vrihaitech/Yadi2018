namespace Yadi.Utilities
{
    partial class TransferAE
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
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnExit = new System.Windows.Forms.Button();
            this.PB = new OMControls.OMProgressBar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.plnExport = new System.Windows.Forms.Panel();
            this.BtnExport = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlImportExport = new System.Windows.Forms.Panel();
            this.btnStopTransfer = new System.Windows.Forms.Button();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.rbExport = new System.Windows.Forms.RadioButton();
            this.rbImport = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.plnExport.SuspendLayout();
            this.pnlImportExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(510, 4);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 9;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // PB
            // 
            this.PB.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.EndColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.ForeColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.PB.Location = new System.Drawing.Point(10, 300);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(594, 26);
            this.PB.StartColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.Step = 1000;
            this.PB.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.PB.TabIndex = 11;
            this.PB.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.plnExport);
            this.pnlMain.Controls.Add(this.pnlImportExport);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(627, 506);
            this.pnlMain.TabIndex = 13;
            // 
            // plnExport
            // 
            this.plnExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plnExport.Controls.Add(this.BtnExport);
            this.plnExport.Controls.Add(this.btnShow);
            this.plnExport.Controls.Add(this.dtpToDate);
            this.plnExport.Controls.Add(this.label2);
            this.plnExport.Controls.Add(this.dtpFromDate);
            this.plnExport.Controls.Add(this.label1);
            this.plnExport.Location = new System.Drawing.Point(13, 82);
            this.plnExport.Name = "plnExport";
            this.plnExport.Size = new System.Drawing.Size(602, 64);
            this.plnExport.TabIndex = 1;
            this.plnExport.Visible = false;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(496, 2);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(94, 60);
            this.BtnExport.TabIndex = 4;
            this.BtnExport.Text = "Transfer";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Visible = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(400, 2);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(80, 60);
            this.btnShow.TabIndex = 3;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(277, 10);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(119, 20);
            this.dtpToDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(84, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(119, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Date :";
            // 
            // pnlImportExport
            // 
            this.pnlImportExport.Controls.Add(this.btnStopTransfer);
            this.pnlImportExport.Controls.Add(this.dgDetails);
            this.pnlImportExport.Controls.Add(this.PB);
            this.pnlImportExport.Location = new System.Drawing.Point(3, 149);
            this.pnlImportExport.Name = "pnlImportExport";
            this.pnlImportExport.Size = new System.Drawing.Size(612, 352);
            this.pnlImportExport.TabIndex = 16;
            this.pnlImportExport.Visible = false;
            // 
            // btnStopTransfer
            // 
            this.btnStopTransfer.Location = new System.Drawing.Point(503, 325);
            this.btnStopTransfer.Name = "btnStopTransfer";
            this.btnStopTransfer.Size = new System.Drawing.Size(99, 25);
            this.btnStopTransfer.TabIndex = 16;
            this.btnStopTransfer.Text = "Stop Transfer";
            this.btnStopTransfer.UseVisualStyleBackColor = false;
            this.btnStopTransfer.Click += new System.EventHandler(this.btnStopTransfer_Click);
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Location = new System.Drawing.Point(9, 6);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.Size = new System.Drawing.Size(594, 285);
            this.dgDetails.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTransfer);
            this.panel1.Controls.Add(this.lblText);
            this.panel1.Controls.Add(this.rbExport);
            this.panel1.Controls.Add(this.rbImport);
            this.panel1.Controls.Add(this.BtnExit);
            this.panel1.Location = new System.Drawing.Point(13, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 67);
            this.panel1.TabIndex = 15;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(382, 3);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(99, 60);
            this.btnTransfer.TabIndex = 15;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.ForeColor = System.Drawing.Color.Red;
            this.lblText.Location = new System.Drawing.Point(13, 43);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(0, 13);
            this.lblText.TabIndex = 14;
            // 
            // rbExport
            // 
            this.rbExport.AutoSize = true;
            this.rbExport.Checked = true;
            this.rbExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExport.Location = new System.Drawing.Point(96, 8);
            this.rbExport.Name = "rbExport";
            this.rbExport.Size = new System.Drawing.Size(61, 17);
            this.rbExport.TabIndex = 1;
            this.rbExport.TabStop = true;
            this.rbExport.Text = "Export";
            this.rbExport.UseVisualStyleBackColor = true;
            this.rbExport.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbImport
            // 
            this.rbImport.AutoSize = true;
            this.rbImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImport.Location = new System.Drawing.Point(5, 9);
            this.rbImport.Name = "rbImport";
            this.rbImport.Size = new System.Drawing.Size(60, 17);
            this.rbImport.TabIndex = 0;
            this.rbImport.Text = "Import";
            this.rbImport.UseVisualStyleBackColor = true;
            this.rbImport.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // TransferAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 536);
            this.Controls.Add(this.pnlMain);
            this.Name = "TransferAE";
            this.Text = "Transfer";
            this.Load += new System.EventHandler(this.TransferAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.plnExport.ResumeLayout(false);
            this.plnExport.PerformLayout();
            this.pnlImportExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button BtnExit;
        private OMControls.OMProgressBar PB;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbExport;
        private System.Windows.Forms.RadioButton rbImport;
        private System.Windows.Forms.Panel pnlImportExport;
        private System.Windows.Forms.DataGridView dgDetails;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Panel plnExport;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button btnStopTransfer;
    }
}