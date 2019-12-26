namespace Yadi.Master
{
    partial class FooterDiscount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgPayType = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PayTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dgBillAmount = new System.Windows.Forms.DataGridView();
            this.LoyaltyPk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeDetailsNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlActive = new OMControls.OMBPanel();
            this.lblSchemeStatus = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbDiscType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpDiscountDateTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.DtpDiscountDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtSchemeUserNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillAmount)).BeginInit();
            this.pnlActive.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.dgPayType);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.dgBillAmount);
            this.pnlMain.Controls.Add(this.pnlActive);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(732, 539);
            this.pnlMain.TabIndex = 0;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(619, 442);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 524;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgPayType
            // 
            this.dgPayType.AllowUserToAddRows = false;
            this.dgPayType.AllowUserToDeleteRows = false;
            this.dgPayType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPayType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.PayType,
            this.ChkSelect,
            this.PayTypeNo,
            this.PkSrNo});
            this.dgPayType.Location = new System.Drawing.Point(492, 128);
            this.dgPayType.Name = "dgPayType";
            this.dgPayType.Size = new System.Drawing.Size(215, 307);
            this.dgPayType.TabIndex = 60029;
            this.dgPayType.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPayType_CellFormatting);
            this.dgPayType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPayType_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // PayType
            // 
            this.PayType.HeaderText = "Pay Type";
            this.PayType.Name = "PayType";
            this.PayType.ReadOnly = true;
            this.PayType.Width = 120;
            // 
            // ChkSelect
            // 
            this.ChkSelect.HeaderText = "Select";
            this.ChkSelect.Name = "ChkSelect";
            this.ChkSelect.Width = 50;
            // 
            // PayTypeNo
            // 
            this.PayTypeNo.HeaderText = "PayTypeNo";
            this.PayTypeNo.Name = "PayTypeNo";
            this.PayTypeNo.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(730, 26);
            this.label1.TabIndex = 60028;
            this.label1.Text = "Bill Footer Discount";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgBillAmount
            // 
            this.dgBillAmount.AllowUserToAddRows = false;
            this.dgBillAmount.AllowUserToDeleteRows = false;
            this.dgBillAmount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBillAmount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LoyaltyPk,
            this.BillAmount,
            this.DiscPercentage,
            this.DiscAmount,
            this.Scheme,
            this.SchemeDetailsNo});
            this.dgBillAmount.Location = new System.Drawing.Point(16, 128);
            this.dgBillAmount.Name = "dgBillAmount";
            this.dgBillAmount.RowHeadersVisible = false;
            this.dgBillAmount.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgBillAmount.Size = new System.Drawing.Size(470, 307);
            this.dgBillAmount.TabIndex = 3;
            this.dgBillAmount.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBillAmount_CellFormatting);
            this.dgBillAmount.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBillAmount_CellEndEdit);
            this.dgBillAmount.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgBillAmount_EditingControlShowing);
            this.dgBillAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBillAmount_KeyDown);
            // 
            // LoyaltyPk
            // 
            this.LoyaltyPk.HeaderText = "SrNo";
            this.LoyaltyPk.Name = "LoyaltyPk";
            this.LoyaltyPk.ReadOnly = true;
            this.LoyaltyPk.Width = 60;
            // 
            // BillAmount
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BillAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.BillAmount.HeaderText = "BillAmount";
            this.BillAmount.Name = "BillAmount";
            this.BillAmount.Width = 150;
            // 
            // DiscPercentage
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscPercentage.DefaultCellStyle = dataGridViewCellStyle5;
            this.DiscPercentage.HeaderText = "Disc.%";
            this.DiscPercentage.Name = "DiscPercentage";
            // 
            // DiscAmount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.DiscAmount.HeaderText = "Disc. RS";
            this.DiscAmount.Name = "DiscAmount";
            // 
            // Scheme
            // 
            this.Scheme.HeaderText = "DtlsPkSrNo";
            this.Scheme.Name = "Scheme";
            this.Scheme.Visible = false;
            // 
            // SchemeDetailsNo
            // 
            this.SchemeDetailsNo.HeaderText = "PkSrNo";
            this.SchemeDetailsNo.Name = "SchemeDetailsNo";
            this.SchemeDetailsNo.Visible = false;
            // 
            // pnlActive
            // 
            this.pnlActive.BorderColor = System.Drawing.Color.Gray;
            this.pnlActive.BorderRadius = 3;
            this.pnlActive.Controls.Add(this.lblSchemeStatus);
            this.pnlActive.Location = new System.Drawing.Point(354, 442);
            this.pnlActive.Name = "pnlActive";
            this.pnlActive.Size = new System.Drawing.Size(135, 59);
            this.pnlActive.TabIndex = 60026;
            // 
            // lblSchemeStatus
            // 
            this.lblSchemeStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSchemeStatus.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchemeStatus.Location = new System.Drawing.Point(3, 12);
            this.lblSchemeStatus.Name = "lblSchemeStatus";
            this.lblSchemeStatus.Size = new System.Drawing.Size(129, 34);
            this.lblSchemeStatus.TabIndex = 10031;
            this.lblSchemeStatus.Text = "Active";
            this.lblSchemeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSchemeStatus.Click += new System.EventHandler(this.lblSchemeStatus_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(409, 506);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(366, 506);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(452, 506);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(323, 506);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(495, 442);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(16, 440);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(16, 440);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(265, 440);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 10;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(182, 440);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(100, 440);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDiscType);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.DtpDiscountDateTo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.DtpDiscountDateFrom);
            this.panel1.Controls.Add(this.txtSchemeUserNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.DtpDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(16, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 82);
            this.panel1.TabIndex = 10017;
            // 
            // cmbDiscType
            // 
            this.cmbDiscType.FormattingEnabled = true;
            this.cmbDiscType.Location = new System.Drawing.Point(573, 52);
            this.cmbDiscType.Name = "cmbDiscType";
            this.cmbDiscType.Size = new System.Drawing.Size(113, 21);
            this.cmbDiscType.TabIndex = 10018;
            this.cmbDiscType.Leave += new System.EventHandler(this.cmbDiscType_Leave);
            this.cmbDiscType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDiscType_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(469, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 10017;
            this.label12.Text = "Discount Type :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 10010;
            this.label4.Text = "Doc No.";
            // 
            // DtpDiscountDateTo
            // 
            this.DtpDiscountDateTo.Location = new System.Drawing.Point(334, 52);
            this.DtpDiscountDateTo.Name = "DtpDiscountDateTo";
            this.DtpDiscountDateTo.Size = new System.Drawing.Size(115, 20);
            this.DtpDiscountDateTo.TabIndex = 2;
            this.DtpDiscountDateTo.Leave += new System.EventHandler(this.DtpDiscountDateTo_Leave);
            this.DtpDiscountDateTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDiscountDateTo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10011;
            this.label5.Text = "Date :";
            // 
            // DtpDiscountDateFrom
            // 
            this.DtpDiscountDateFrom.Location = new System.Drawing.Point(128, 52);
            this.DtpDiscountDateFrom.Name = "DtpDiscountDateFrom";
            this.DtpDiscountDateFrom.Size = new System.Drawing.Size(115, 20);
            this.DtpDiscountDateFrom.TabIndex = 1;
            this.DtpDiscountDateFrom.ValueChanged += new System.EventHandler(this.DtpDiscountDateFrom_ValueChanged);
            this.DtpDiscountDateFrom.Leave += new System.EventHandler(this.DtpDiscountDateFrom_Leave);
            this.DtpDiscountDateFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDiscountDateFrom_KeyDown);
            // 
            // txtSchemeUserNo
            // 
            this.txtSchemeUserNo.Enabled = false;
            this.txtSchemeUserNo.Location = new System.Drawing.Point(128, 7);
            this.txtSchemeUserNo.Name = "txtSchemeUserNo";
            this.txtSchemeUserNo.ReadOnly = true;
            this.txtSchemeUserNo.Size = new System.Drawing.Size(113, 20);
            this.txtSchemeUserNo.TabIndex = 10012;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 10016;
            this.label6.Text = "To";
            // 
            // DtpDate
            // 
            this.DtpDate.Enabled = false;
            this.DtpDate.Location = new System.Drawing.Point(334, 7);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(115, 20);
            this.DtpDate.TabIndex = 0;
            this.DtpDate.ValueChanged += new System.EventHandler(this.DtpDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 10015;
            this.label3.Text = "Period From :";
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // FooterDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 563);
            this.Controls.Add(this.pnlMain);
            this.Name = "FooterDiscount";
            this.Text = "Bill Footer Discount";
            this.Load += new System.EventHandler(this.MFooterDiscount_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillAmount)).EndInit();
            this.pnlActive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.TextBox txtSchemeUserNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker DtpDiscountDateTo;
        private System.Windows.Forms.DateTimePicker DtpDiscountDateFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private OMControls.OMBPanel pnlActive;
        private System.Windows.Forms.Label lblSchemeStatus;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgBillAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoyaltyPk;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scheme;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeDetailsNo;
        private System.Windows.Forms.DataGridView dgPayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ComboBox cmbDiscType;
        private System.Windows.Forms.Label label12;
    }
}