namespace Yadi.Display
{
    partial class VoucherViewAE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpVoucherDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel2 = new OMControls.OMPanel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignCode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkVoucherTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VchCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "No :";
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(52, 10);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(116, 20);
            this.txtNo.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "ReferenceNo";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Location = new System.Drawing.Point(266, 10);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(100, 20);
            this.txtRefNo.TabIndex = 111;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Date :";
            // 
            // dtpVoucherDate
            // 
            this.dtpVoucherDate.Location = new System.Drawing.Point(418, 10);
            this.dtpVoucherDate.Name = "dtpVoucherDate";
            this.dtpVoucherDate.Size = new System.Drawing.Size(149, 20);
            this.dtpVoucherDate.TabIndex = 0;
            this.dtpVoucherDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpVoucherDate_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpVoucherDate);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtRefNo);
            this.panel1.Location = new System.Drawing.Point(6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(673, 45);
            this.panel1.TabIndex = 0;
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.SignCode,
            this.LedgerNo,
            this.Debit,
            this.Credit,
            this.PkVoucherTrnNo,
            this.CompanyNo,
            this.VchCompanyName});
            this.GridView.Location = new System.Drawing.Point(7, 59);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(673, 410);
            this.GridView.TabIndex = 1;
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            this.GridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridView_EditingControlShowing);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 476);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Narration :";
            // 
            // txtNarration
            // 
            this.txtNarration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNarration.Location = new System.Drawing.Point(81, 475);
            this.txtNarration.Multiline = true;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNarration.Size = new System.Drawing.Size(598, 35);
            this.txtNarration.TabIndex = 3;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnExit.Location = new System.Drawing.Point(6, 519);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 23);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSave.Location = new System.Drawing.Point(265, 518);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 23);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Visible = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(179, 518);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel2
            // 
            this.panel2.BorderRadius = 3;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.GridView);
            this.panel2.Controls.Add(this.BtnExit);
            this.panel2.Controls.Add(this.txtNarration);
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Controls.Add(this.label4);
            this.panel2.CornerRadius = 3;
            this.panel2.GradientBottom = System.Drawing.Color.LightGray;
            this.panel2.GradientMiddle = System.Drawing.Color.White;
            this.panel2.GradientShow = true;
            this.panel2.GradientTop = System.Drawing.Color.Silver;
            this.panel2.Location = new System.Drawing.Point(1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(693, 548);
            this.panel2.TabIndex = 54;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "SRNO";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // SignCode
            // 
            this.SignCode.DataPropertyName = "SignCode";
            this.SignCode.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.SignCode.HeaderText = "Cr/Dr";
            this.SignCode.Name = "SignCode";
            this.SignCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SignCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SignCode.Width = 70;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.LedgerNo.HeaderText = "Particulars";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LedgerNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.LedgerNo.Width = 210;
            // 
            // Debit
            // 
            this.Debit.DataPropertyName = "Debit";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Debit.DefaultCellStyle = dataGridViewCellStyle1;
            this.Debit.HeaderText = "Debit";
            this.Debit.Name = "Debit";
            this.Debit.Width = 80;
            // 
            // Credit
            // 
            this.Credit.DataPropertyName = "Credit";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Credit.DefaultCellStyle = dataGridViewCellStyle2;
            this.Credit.HeaderText = "Credit";
            this.Credit.Name = "Credit";
            this.Credit.Width = 80;
            // 
            // PkVoucherTrnNo
            // 
            this.PkVoucherTrnNo.DataPropertyName = "PkVoucherTrnNo";
            this.PkVoucherTrnNo.HeaderText = "VoucherNo";
            this.PkVoucherTrnNo.Name = "PkVoucherTrnNo";
            this.PkVoucherTrnNo.Visible = false;
            // 
            // CompanyNo
            // 
            this.CompanyNo.DataPropertyName = "CompanyNo";
            this.CompanyNo.HeaderText = "CompanyNo";
            this.CompanyNo.Name = "CompanyNo";
            this.CompanyNo.Visible = false;
            // 
            // VchCompanyName
            // 
            this.VchCompanyName.DataPropertyName = "CompanyName";
            this.VchCompanyName.HeaderText = "Company Name";
            this.VchCompanyName.Name = "VchCompanyName";
            this.VchCompanyName.Width = 150;
            // 
            // VoucherViewAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 550);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VoucherViewAE";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Voucher (Alter)";
            this.Load += new System.EventHandler(this.VoucherViewAE_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VoucherViewAE_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpVoucherDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSave;
        private OMControls.OMPanel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn SignCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkVoucherTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VchCompanyName;
    }
}