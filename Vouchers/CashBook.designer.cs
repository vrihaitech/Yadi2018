namespace Yadi.Display
{
    partial class CashBook
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
            this.BtnShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabcashBook = new OMControls.OMTabControl();
            this.tabPage1 = new OMControls.OMTabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new OMControls.OMTabPage();
            this.btnCancelt1 = new System.Windows.Forms.Button();
            this.lblMonthly = new System.Windows.Forms.Label();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new OMControls.OMTabPage();
            this.btnCancelt2 = new System.Windows.Forms.Button();
            this.lblM = new System.Windows.Forms.Label();
            this.lblDatewise = new System.Windows.Forms.Label();
            this.GridViewDaily = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlMainForm = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabcashBook.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).BeginInit();
            this.pnlMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(459, 8);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(90, 27);
            this.BtnShow.TabIndex = 51;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(322, 10);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(130, 23);
            this.DTToDate.TabIndex = 50;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(242, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 100;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(101, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 49;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "From Date :";
            // 
            // tabcashBook
            // 
            this.tabcashBook.ActiveColor = System.Drawing.SystemColors.Control;
            this.tabcashBook.BackColor = System.Drawing.SystemColors.Control;
            this.tabcashBook.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tabcashBook.Controls.Add(this.tabPage1);
            this.tabcashBook.Controls.Add(this.tabPage2);
            this.tabcashBook.Controls.Add(this.tabPage3);
            this.tabcashBook.ImageIndex = -1;
            this.tabcashBook.ImageList = null;
            this.tabcashBook.InactiveColor = System.Drawing.SystemColors.Window;
            this.tabcashBook.Location = new System.Drawing.Point(9, 45);
            this.tabcashBook.Name = "tabcashBook";
            this.tabcashBook.ScrollButtonStyle = OMControls.OMScrollButtonStyle.Always;
            this.tabcashBook.SelectedIndex = 1;
            this.tabcashBook.SelectedTab = this.tabPage2;
            this.tabcashBook.Size = new System.Drawing.Size(820, 592);
            this.tabcashBook.TabDock = System.Windows.Forms.DockStyle.Top;
            this.tabcashBook.TabDrawer = null;
            this.tabcashBook.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabcashBook.TabIndex = 67;
            this.tabcashBook.TabChanged += new System.EventHandler(this.tabcashBook_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.DataGridView1);
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = -1;
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(812, 558);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cash Book";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(812, 23);
            this.label3.TabIndex = 54;
            this.label3.Text = "Cash Book";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.DataGridView1.Location = new System.Drawing.Point(0, 23);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(812, 529);
            this.DataGridView1.TabIndex = 53;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCancelt1);
            this.tabPage2.Controls.Add(this.lblMonthly);
            this.tabPage2.Controls.Add(this.DataGridView2);
            this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage2.ImageIndex = -1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(812, 558);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cash Book Details";
            // 
            // btnCancelt1
            // 
            this.btnCancelt1.Location = new System.Drawing.Point(717, 532);
            this.btnCancelt1.Name = "btnCancelt1";
            this.btnCancelt1.Size = new System.Drawing.Size(75, 23);
            this.btnCancelt1.TabIndex = 56;
            this.btnCancelt1.Text = "Cancel";
            this.btnCancelt1.UseVisualStyleBackColor = true;
            this.btnCancelt1.Click += new System.EventHandler(this.btnCancelt1_Click);
            // 
            // lblMonthly
            // 
            this.lblMonthly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblMonthly.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMonthly.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthly.ForeColor = System.Drawing.Color.White;
            this.lblMonthly.Location = new System.Drawing.Point(0, 0);
            this.lblMonthly.Name = "lblMonthly";
            this.lblMonthly.Size = new System.Drawing.Size(812, 23);
            this.lblMonthly.TabIndex = 55;
            this.lblMonthly.Text = "Cash  Book Details";
            this.lblMonthly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.DataGridView2.Location = new System.Drawing.Point(0, 23);
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView2.RowTemplate.Height = 27;
            this.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView2.Size = new System.Drawing.Size(812, 505);
            this.DataGridView2.TabIndex = 45;
            this.DataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView2_CellFormatting);
            this.DataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnCancelt2);
            this.tabPage3.Controls.Add(this.lblM);
            this.tabPage3.Controls.Add(this.lblDatewise);
            this.tabPage3.Controls.Add(this.GridViewDaily);
            this.tabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage3.ImageIndex = -1;
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(812, 558);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cash Book Voucher Entry Details";
            // 
            // btnCancelt2
            // 
            this.btnCancelt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelt2.Location = new System.Drawing.Point(727, 535);
            this.btnCancelt2.Name = "btnCancelt2";
            this.btnCancelt2.Size = new System.Drawing.Size(75, 23);
            this.btnCancelt2.TabIndex = 58;
            this.btnCancelt2.Text = "Cancel";
            this.btnCancelt2.UseVisualStyleBackColor = true;
            this.btnCancelt2.Click += new System.EventHandler(this.btnCancelt2_Click);
            // 
            // lblM
            // 
            this.lblM.AutoSize = true;
            this.lblM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblM.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblM.ForeColor = System.Drawing.Color.White;
            this.lblM.Location = new System.Drawing.Point(489, 7);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(0, 20);
            this.lblM.TabIndex = 57;
            // 
            // lblDatewise
            // 
            this.lblDatewise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.lblDatewise.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDatewise.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatewise.ForeColor = System.Drawing.Color.White;
            this.lblDatewise.Location = new System.Drawing.Point(0, 0);
            this.lblDatewise.Name = "lblDatewise";
            this.lblDatewise.Size = new System.Drawing.Size(812, 23);
            this.lblDatewise.TabIndex = 55;
            this.lblDatewise.Text = " Voucher Entry Details";
            this.lblDatewise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GridViewDaily
            // 
            this.GridViewDaily.AllowUserToAddRows = false;
            this.GridViewDaily.AllowUserToDeleteRows = false;
            this.GridViewDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridViewDaily.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDaily.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewDaily.Location = new System.Drawing.Point(0, 23);
            this.GridViewDaily.Name = "GridViewDaily";
            this.GridViewDaily.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridViewDaily.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowTemplate.Height = 27;
            this.GridViewDaily.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewDaily.Size = new System.Drawing.Size(812, 508);
            this.GridViewDaily.TabIndex = 46;
            this.GridViewDaily.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewDaily_CellFormatting);
            this.GridViewDaily.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridViewDaily_CellClick);
            this.GridViewDaily.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridViewDaily_KeyDown);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(645, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 27);
            this.btnPrint.TabIndex = 72;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(738, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 27);
            this.btnExport.TabIndex = 101;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlMainForm
            // 
            this.pnlMainForm.BorderColor = System.Drawing.Color.Gray;
            this.pnlMainForm.BorderRadius = 3;
            this.pnlMainForm.Controls.Add(this.btnExit);
            this.pnlMainForm.Controls.Add(this.tabcashBook);
            this.pnlMainForm.Controls.Add(this.label1);
            this.pnlMainForm.Controls.Add(this.btnExport);
            this.pnlMainForm.Controls.Add(this.btnPrint);
            this.pnlMainForm.Controls.Add(this.BtnShow);
            this.pnlMainForm.Controls.Add(this.DTPFromDate);
            this.pnlMainForm.Controls.Add(this.DTToDate);
            this.pnlMainForm.Controls.Add(this.label2);
            this.pnlMainForm.Location = new System.Drawing.Point(3, 12);
            this.pnlMainForm.Name = "pnlMainForm";
            this.pnlMainForm.Size = new System.Drawing.Size(839, 648);
            this.pnlMainForm.TabIndex = 102;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(552, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 62;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CashBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 694);
            this.Controls.Add(this.pnlMainForm);
            this.Name = "CashBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash Book";
            this.Load += new System.EventHandler(this.CashBook_Load);
            this.tabcashBook.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).EndInit();
            this.pnlMainForm.ResumeLayout(false);
            this.pnlMainForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private OMControls.OMTabControl tabcashBook;
        private OMControls.OMTabPage tabPage1;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private OMControls.OMTabPage tabPage2;
        private System.Windows.Forms.Label lblMonthly;
        internal System.Windows.Forms.DataGridView DataGridView2;
        private OMControls.OMTabPage tabPage3;
        private System.Windows.Forms.Label lblDatewise;
        internal System.Windows.Forms.DataGridView GridViewDaily;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.Button btnCancelt1;
        private System.Windows.Forms.Button btnCancelt2;
        internal System.Windows.Forms.Button btnExport;
        private OMControls.OMBPanel pnlMainForm;
        internal System.Windows.Forms.Button btnExit;
    }
}