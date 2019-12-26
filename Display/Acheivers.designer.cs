namespace Yadi.Display
{
    partial class Acheivers
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
            this.btnShow = new System.Windows.Forms.Button();
            this.pnlDataInfo = new OMControls.OMBPanel();
            this.lblSchemeNumber = new System.Windows.Forms.Label();
            this.lblRedemPeriod = new System.Windows.Forms.Label();
            this.btnApplyAchievers = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblSchemePeriod = new System.Windows.Forms.Label();
            this.dgAchiever = new System.Windows.Forms.DataGridView();
            this.cmbScheme = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.rdTAcheivers = new System.Windows.Forms.RadioButton();
            this.rdAcheivers = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlDataInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAchiever)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnShow);
            this.pnlMain.Controls.Add(this.pnlDataInfo);
            this.pnlMain.Controls.Add(this.cmbScheme);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.rdTAcheivers);
            this.pnlMain.Controls.Add(this.rdAcheivers);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(24, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(606, 465);
            this.pnlMain.TabIndex = 111;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(402, 3);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 60);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Show (F3)";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // pnlDataInfo
            // 
            this.pnlDataInfo.BorderColor = System.Drawing.Color.Gray;
            this.pnlDataInfo.BorderRadius = 3;
            this.pnlDataInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDataInfo.Controls.Add(this.lblSchemeNumber);
            this.pnlDataInfo.Controls.Add(this.lblRedemPeriod);
            this.pnlDataInfo.Controls.Add(this.btnApplyAchievers);
            this.pnlDataInfo.Controls.Add(this.btnPrint);
            this.pnlDataInfo.Controls.Add(this.lblSchemePeriod);
            this.pnlDataInfo.Controls.Add(this.dgAchiever);
            this.pnlDataInfo.Location = new System.Drawing.Point(6, 69);
            this.pnlDataInfo.Name = "pnlDataInfo";
            this.pnlDataInfo.Size = new System.Drawing.Size(590, 387);
            this.pnlDataInfo.TabIndex = 112;
            this.pnlDataInfo.Visible = false;
            // 
            // lblSchemeNumber
            // 
            this.lblSchemeNumber.AutoSize = true;
            this.lblSchemeNumber.Location = new System.Drawing.Point(4, 6);
            this.lblSchemeNumber.Name = "lblSchemeNumber";
            this.lblSchemeNumber.Size = new System.Drawing.Size(92, 13);
            this.lblSchemeNumber.TabIndex = 115;
            this.lblSchemeNumber.Text = "Scheme Number :";
            // 
            // lblRedemPeriod
            // 
            this.lblRedemPeriod.AutoSize = true;
            this.lblRedemPeriod.Location = new System.Drawing.Point(3, 57);
            this.lblRedemPeriod.Name = "lblRedemPeriod";
            this.lblRedemPeriod.Size = new System.Drawing.Size(129, 13);
            this.lblRedemPeriod.TabIndex = 114;
            this.lblRedemPeriod.Text = "Redemption Period From :";
            // 
            // btnApplyAchievers
            // 
            this.btnApplyAchievers.Location = new System.Drawing.Point(4, 318);
            this.btnApplyAchievers.Name = "btnApplyAchievers";
            this.btnApplyAchievers.Size = new System.Drawing.Size(90, 60);
            this.btnApplyAchievers.TabIndex = 113;
            this.btnApplyAchievers.Text = "Apply Achievers (F4)";
            this.btnApplyAchievers.UseVisualStyleBackColor = false;
            this.btnApplyAchievers.Click += new System.EventHandler(this.btnApplyAchievers_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(120, 318);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 60);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblSchemePeriod
            // 
            this.lblSchemePeriod.AutoSize = true;
            this.lblSchemePeriod.Location = new System.Drawing.Point(2, 30);
            this.lblSchemePeriod.Name = "lblSchemePeriod";
            this.lblSchemePeriod.Size = new System.Drawing.Size(111, 13);
            this.lblSchemePeriod.TabIndex = 113;
            this.lblSchemePeriod.Text = "Scheme Period From :";
            // 
            // dgAchiever
            // 
            this.dgAchiever.AllowUserToAddRows = false;
            this.dgAchiever.AllowUserToDeleteRows = false;
            this.dgAchiever.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAchiever.Location = new System.Drawing.Point(4, 85);
            this.dgAchiever.Name = "dgAchiever";
            this.dgAchiever.Size = new System.Drawing.Size(501, 227);
            this.dgAchiever.TabIndex = 104;
            this.dgAchiever.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAchiever_CellValueChanged);
            this.dgAchiever.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgAchiever_CellFormatting);
            // 
            // cmbScheme
            // 
            this.cmbScheme.FormattingEnabled = true;
            this.cmbScheme.Location = new System.Drawing.Point(152, 37);
            this.cmbScheme.Name = "cmbScheme";
            this.cmbScheme.Size = new System.Drawing.Size(167, 21);
            this.cmbScheme.TabIndex = 0;
            this.cmbScheme.SelectedIndexChanged += new System.EventHandler(this.cmbScheme_SelectedIndexChanged);
            this.cmbScheme.Leave += new System.EventHandler(this.cmbScheme_Leave);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(501, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 60);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rdTAcheivers
            // 
            this.rdTAcheivers.AutoSize = true;
            this.rdTAcheivers.Location = new System.Drawing.Point(154, 9);
            this.rdTAcheivers.Name = "rdTAcheivers";
            this.rdTAcheivers.Size = new System.Drawing.Size(141, 17);
            this.rdTAcheivers.TabIndex = 112;
            this.rdTAcheivers.TabStop = true;
            this.rdTAcheivers.Text = "Tentative Achievers (F2)";
            this.rdTAcheivers.UseVisualStyleBackColor = true;
            this.rdTAcheivers.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rdAcheivers
            // 
            this.rdAcheivers.AutoSize = true;
            this.rdAcheivers.Checked = true;
            this.rdAcheivers.Location = new System.Drawing.Point(15, 9);
            this.rdAcheivers.Name = "rdAcheivers";
            this.rdAcheivers.Size = new System.Drawing.Size(93, 17);
            this.rdAcheivers.TabIndex = 111;
            this.rdAcheivers.TabStop = true;
            this.rdAcheivers.Text = "Achievers (F1)";
            this.rdAcheivers.UseVisualStyleBackColor = true;
            this.rdAcheivers.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "Select Scheme :";
            // 
            // Acheivers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 498);
            this.Controls.Add(this.pnlMain);
            this.Name = "Acheivers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Achievers";
            this.Load += new System.EventHandler(this.Acheivers_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlDataInfo.ResumeLayout(false);
            this.pnlDataInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAchiever)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdTAcheivers;
        private System.Windows.Forms.RadioButton rdAcheivers;
        private System.Windows.Forms.ComboBox cmbScheme;
        private OMControls.OMBPanel pnlDataInfo;
        private System.Windows.Forms.DataGridView dgAchiever;
        internal System.Windows.Forms.Button btnShow;
        internal System.Windows.Forms.Button btnApplyAchievers;
        private System.Windows.Forms.Label lblSchemePeriod;
        private System.Windows.Forms.Label lblRedemPeriod;
        private System.Windows.Forms.Label lblSchemeNumber;

    }
}