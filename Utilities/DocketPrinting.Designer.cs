namespace Yadi.Utilities
{
    partial class DocketPrinting
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
            this.pnlMain = new OMControls.OMBPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnReamainig = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PkVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbDepostiteto = new System.Windows.Forms.ComboBox();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.txtDocketNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.btnReamainig);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.lblChkHelp);
            this.pnlMain.Controls.Add(this.dgDetails);
            this.pnlMain.Controls.Add(this.cmbDepostiteto);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.txtDocketNo);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(775, 442);
            this.pnlMain.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(442, 357);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(158, 32);
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReamainig
            // 
            this.btnReamainig.Location = new System.Drawing.Point(585, 79);
            this.btnReamainig.Name = "btnReamainig";
            this.btnReamainig.Size = new System.Drawing.Size(153, 36);
            this.btnReamainig.TabIndex = 4564565;
            this.btnReamainig.Text = "Show Remaining";
            this.btnReamainig.UseVisualStyleBackColor = true;
            this.btnReamainig.Visible = false;
            this.btnReamainig.Click += new System.EventHandler(this.btnReamainig_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(147, 195);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 506;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(188, 358);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(105, 358);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(522, 392);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(22, 357);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(482, 392);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(22, 358);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(563, 392);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 15;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(105, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(442, 392);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 12;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(354, 358);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 10;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(271, 358);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(119, 113);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(35, 13);
            this.lblChkHelp.TabIndex = 114546;
            this.lblChkHelp.Text = "label5";
            this.lblChkHelp.Visible = false;
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.LedgerName,
            this.ChqNo,
            this.ChDate,
            this.BranchName,
            this.Amount,
            this.Remark,
            this.ChkSelect,
            this.PkVoucherNo,
            this.PkSrNo});
            this.dgDetails.Enabled = false;
            this.dgDetails.Location = new System.Drawing.Point(21, 130);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.Size = new System.Drawing.Size(718, 222);
            this.dgDetails.TabIndex = 0;
            this.dgDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDetails_CellFormatting);
            this.dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgDetails_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "Party Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Width = 150;
            // 
            // ChqNo
            // 
            this.ChqNo.HeaderText = "Chq No";
            this.ChqNo.Name = "ChqNo";
            this.ChqNo.ReadOnly = true;
            this.ChqNo.Width = 70;
            // 
            // ChDate
            // 
            this.ChDate.HeaderText = "Ch Date";
            this.ChDate.Name = "ChDate";
            this.ChDate.ReadOnly = true;
            this.ChDate.Width = 80;
            // 
            // BranchName
            // 
            this.BranchName.HeaderText = "BranchName";
            this.BranchName.Name = "BranchName";
            this.BranchName.ReadOnly = true;
            this.BranchName.Width = 110;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 70;
            // 
            // Remark
            // 
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            // 
            // ChkSelect
            // 
            this.ChkSelect.HeaderText = "Select";
            this.ChkSelect.Name = "ChkSelect";
            this.ChkSelect.Width = 50;
            // 
            // PkVoucherNo
            // 
            this.PkVoucherNo.HeaderText = "PkVoucherNo";
            this.PkVoucherNo.Name = "PkVoucherNo";
            this.PkVoucherNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PkVoucherNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PkVoucherNo.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // cmbDepostiteto
            // 
            this.cmbDepostiteto.FormattingEnabled = true;
            this.cmbDepostiteto.Location = new System.Drawing.Point(117, 84);
            this.cmbDepostiteto.Name = "cmbDepostiteto";
            this.cmbDepostiteto.Size = new System.Drawing.Size(388, 21);
            this.cmbDepostiteto.TabIndex = 3;
            this.cmbDepostiteto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepostiteto_KeyDown);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(117, 51);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(135, 23);
            this.DTToDate.TabIndex = 2;
            this.DTToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTToDate_KeyDown);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(482, 16);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(135, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.Visible = false;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            // 
            // txtDocketNo
            // 
            this.txtDocketNo.Location = new System.Drawing.Point(117, 20);
            this.txtDocketNo.Name = "txtDocketNo";
            this.txtDocketNo.ReadOnly = true;
            this.txtDocketNo.Size = new System.Drawing.Size(133, 20);
            this.txtDocketNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 364564;
            this.label4.Text = "Deposite To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 454542;
            this.label3.Text = "To Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1564564;
            this.label2.Text = "From Date :";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4564564;
            this.label1.Text = "Docket No :";
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // DocketPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 485);
            this.Controls.Add(this.pnlMain);
            this.Name = "DocketPrinting";
            this.Text = "Docket Printing";
            this.Load += new System.EventHandler(this.DocketPrinting_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDocketNo;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.DataGridView dgDetails;
        private System.Windows.Forms.ComboBox cmbDepostiteto;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnReamainig;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
    }
}