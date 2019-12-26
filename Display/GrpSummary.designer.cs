namespace Yadi.Display
{
    partial class GrpSummary
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabGrpSumm = new OMControls.OMTabControl();
            this.tabPage1 = new OMControls.OMTabPage();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new OMControls.OMTabPage();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.lblMonthDtls = new System.Windows.Forms.Label();
            this.tabPage3 = new OMControls.OMTabPage();
            this.GridViewDaily = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSummDtls = new System.Windows.Forms.Label();
            this.BtnShow = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbGroupName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbAllGroup = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbGrpwise = new System.Windows.Forms.RadioButton();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlMainForm = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabGrpSumm.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabGrpSumm
            // 
            this.tabGrpSumm.ActiveColor = System.Drawing.SystemColors.Control;
            this.tabGrpSumm.BackColor = System.Drawing.SystemColors.Control;
            this.tabGrpSumm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tabGrpSumm.Controls.Add(this.tabPage1);
            this.tabGrpSumm.Controls.Add(this.tabPage2);
            this.tabGrpSumm.Controls.Add(this.tabPage3);
            this.tabGrpSumm.ImageIndex = -1;
            this.tabGrpSumm.ImageList = null;
            this.tabGrpSumm.InactiveColor = System.Drawing.SystemColors.Window;
            this.tabGrpSumm.Location = new System.Drawing.Point(14, 147);
            this.tabGrpSumm.Name = "tabGrpSumm";
            this.tabGrpSumm.ScrollButtonStyle = OMControls.OMScrollButtonStyle.Always;
            this.tabGrpSumm.SelectedIndex = 2;
            this.tabGrpSumm.SelectedTab = this.tabPage3;
            this.tabGrpSumm.Size = new System.Drawing.Size(820, 542);
            this.tabGrpSumm.TabDock = System.Windows.Forms.DockStyle.Top;
            this.tabGrpSumm.TabDrawer = null;
            this.tabGrpSumm.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabGrpSumm.TabIndex = 58;
            this.tabGrpSumm.TabChanged += new System.EventHandler(this.tabLedgerBook_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGridView1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = -1;
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(812, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Group Summary";
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(3, 23);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(813, 469);
            this.DataGridView1.TabIndex = 43;
            this.DataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.DataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridView1_DataBindingComplete);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(812, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "Group Summary";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DataGridView2);
            this.tabPage2.Controls.Add(this.lblMonthDtls);
            this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage2.ImageIndex = -1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(812, 508);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Group Summary Details";
            // 
            // DataGridView2
            // 
            this.DataGridView2.AllowUserToAddRows = false;
            this.DataGridView2.AllowUserToDeleteRows = false;
            this.DataGridView2.AllowUserToResizeColumns = false;
            this.DataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView2.Location = new System.Drawing.Point(3, 26);
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowTemplate.Height = 27;
            this.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView2.Size = new System.Drawing.Size(816, 479);
            this.DataGridView2.TabIndex = 44;
            this.DataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellClick);
            // 
            // lblMonthDtls
            // 
            this.lblMonthDtls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblMonthDtls.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthDtls.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthDtls.ForeColor = System.Drawing.Color.White;
            this.lblMonthDtls.Location = new System.Drawing.Point(0, 0);
            this.lblMonthDtls.Name = "lblMonthDtls";
            this.lblMonthDtls.Size = new System.Drawing.Size(812, 26);
            this.lblMonthDtls.TabIndex = 57;
            this.lblMonthDtls.Text = "Group Summary Details";
            this.lblMonthDtls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.GridViewDaily);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.lblSummDtls);
            this.tabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage3.ImageIndex = -1;
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(812, 508);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Group Summary Voucher Entry Details";
            // 
            // GridViewDaily
            // 
            this.GridViewDaily.AllowUserToAddRows = false;
            this.GridViewDaily.AllowUserToDeleteRows = false;
            this.GridViewDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridViewDaily.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDaily.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewDaily.Location = new System.Drawing.Point(0, 26);
            this.GridViewDaily.Name = "GridViewDaily";
            this.GridViewDaily.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridViewDaily.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowTemplate.Height = 27;
            this.GridViewDaily.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewDaily.Size = new System.Drawing.Size(816, 469);
            this.GridViewDaily.TabIndex = 45;
            this.GridViewDaily.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewDaily_CellFormatting);
            this.GridViewDaily.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewDaily_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(704, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 18);
            this.label6.TabIndex = 59;
            // 
            // lblSummDtls
            // 
            this.lblSummDtls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblSummDtls.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummDtls.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummDtls.ForeColor = System.Drawing.Color.White;
            this.lblSummDtls.Location = new System.Drawing.Point(0, 0);
            this.lblSummDtls.Name = "lblSummDtls";
            this.lblSummDtls.Size = new System.Drawing.Size(812, 26);
            this.lblSummDtls.TabIndex = 58;
            this.lblSummDtls.Text = "Voucher Entry  Details";
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(414, 53);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 3;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbGroupName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(85, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(577, 44);
            this.panel1.TabIndex = 56;
            this.panel1.Visible = false;
            // 
            // cmbGroupName
            // 
            this.cmbGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroupName.FormattingEnabled = true;
            this.cmbGroupName.Location = new System.Drawing.Point(166, 11);
            this.cmbGroupName.Name = "cmbGroupName";
            this.cmbGroupName.Size = new System.Drawing.Size(332, 28);
            this.cmbGroupName.TabIndex = 46;
            this.cmbGroupName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbGroupName_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "Category Name :";
            // 
            // rbAllGroup
            // 
            this.rbAllGroup.AutoSize = true;
            this.rbAllGroup.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllGroup.Location = new System.Drawing.Point(271, 59);
            this.rbAllGroup.Name = "rbAllGroup";
            this.rbAllGroup.Size = new System.Drawing.Size(91, 20);
            this.rbAllGroup.TabIndex = 55;
            this.rbAllGroup.TabStop = true;
            this.rbAllGroup.Text = "All Group";
            this.rbAllGroup.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 54;
            this.label3.Text = "Type :";
            // 
            // rbGrpwise
            // 
            this.rbGrpwise.AutoSize = true;
            this.rbGrpwise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGrpwise.Location = new System.Drawing.Point(143, 59);
            this.rbGrpwise.Name = "rbGrpwise";
            this.rbGrpwise.Size = new System.Drawing.Size(108, 20);
            this.rbGrpwise.TabIndex = 53;
            this.rbGrpwise.TabStop = true;
            this.rbGrpwise.Text = "Group Wise";
            this.rbGrpwise.UseVisualStyleBackColor = true;
            this.rbGrpwise.CheckedChanged += new System.EventHandler(this.rbGrpwise_CheckedChanged);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(455, 9);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(128, 26);
            this.DTToDate.TabIndex = 2;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(377, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 70;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(143, 9);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(128, 26);
            this.DTPFromDate.TabIndex = 1;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 49;
            this.label1.Text = "From Date :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(627, 53);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 74;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(735, 52);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(93, 27);
            this.btnExport.TabIndex = 75;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.tabGrpSumm);
            this.pnlMainForm.Controls.Add(this.btnExport);
            this.pnlMainForm.Controls.Add(this.btnPrint);
            this.pnlMainForm.Controls.Add(this.BtnShow);
            this.pnlMainForm.Controls.Add(this.panel1);
            this.pnlMainForm.Controls.Add(this.rbAllGroup);
            this.pnlMainForm.Controls.Add(this.label3);
            this.pnlMainForm.Controls.Add(this.rbGrpwise);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Location = new System.Drawing.Point(3, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(839, 694);
            this.pnlMainForm.TabIndex = 76;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(522, 54);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 62;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // GrpSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 744);
            this.Controls.Add(this.pnlMainForm);
            this.Name = "GrpSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Group Summary";
            this.Load += new System.EventHandler(this.GrpSummary_Load);
            this.tabGrpSumm.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal OMControls.OMTabControl tabGrpSumm;
        internal OMControls.OMTabPage tabPage1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal OMControls.OMTabPage tabPage2;
        internal System.Windows.Forms.DataGridView DataGridView2;
        internal OMControls.OMTabPage tabPage3;
        internal System.Windows.Forms.DataGridView GridViewDaily;
        internal System.Windows.Forms.Button BtnShow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbGroupName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbAllGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbGrpwise;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMonthDtls;
        private System.Windows.Forms.Label lblSummDtls;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Button btnExport;
        private OMControls.OMBPanel pnlMainForm;
        internal System.Windows.Forms.Button btnExit;
    }
}