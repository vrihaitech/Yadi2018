namespace Yadi.Settings
{
    partial class PayTypeSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.txtChrg4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtChrg3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtChrg2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtChrg1 = new System.Windows.Forms.TextBox();
            this.pnlPayType = new System.Windows.Forms.Panel();
            this.lstPayType = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.pnlLedger = new System.Windows.Forms.Panel();
            this.lstLedger = new System.Windows.Forms.ListBox();
            this.cmbFoodVoucher = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlUnder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PayTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ControlUnderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChrgPerce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCreditCard = new System.Windows.Forms.ComboBox();
            this.cmbCheque = new System.Windows.Forms.ComboBox();
            this.cmbCash = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain = new OMControls.OMBPanel();
            this.panel3.SuspendLayout();
            this.pnlPayType.SuspendLayout();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 60);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(103, 365);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(69, 60);
            this.BtnClose.TabIndex = 5;
            this.BtnClose.Text = "E&xit";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.txtChrg4);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtChrg3);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtChrg2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtChrg1);
            this.panel3.Controls.Add(this.pnlPayType);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnAddRow);
            this.panel3.Controls.Add(this.pnlLedger);
            this.panel3.Controls.Add(this.cmbFoodVoucher);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.GridView);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbCreditCard);
            this.panel3.Controls.Add(this.cmbCheque);
            this.panel3.Controls.Add(this.cmbCash);
            this.panel3.Controls.Add(this.cmbCompany);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.lblChkHelp);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(14, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(753, 346);
            this.panel3.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(723, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 60058;
            this.label13.Text = "%";
            this.label13.Visible = false;
            // 
            // txtChrg4
            // 
            this.txtChrg4.Location = new System.Drawing.Point(686, 147);
            this.txtChrg4.Name = "txtChrg4";
            this.txtChrg4.Size = new System.Drawing.Size(35, 20);
            this.txtChrg4.TabIndex = 8;
            this.txtChrg4.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(722, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 60056;
            this.label9.Text = "%";
            this.label9.Visible = false;
            // 
            // txtChrg3
            // 
            this.txtChrg3.Location = new System.Drawing.Point(685, 121);
            this.txtChrg3.Name = "txtChrg3";
            this.txtChrg3.Size = new System.Drawing.Size(35, 20);
            this.txtChrg3.TabIndex = 6;
            this.txtChrg3.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(723, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 60054;
            this.label8.Text = "%";
            this.label8.Visible = false;
            // 
            // txtChrg2
            // 
            this.txtChrg2.Location = new System.Drawing.Point(686, 94);
            this.txtChrg2.Name = "txtChrg2";
            this.txtChrg2.Size = new System.Drawing.Size(35, 20);
            this.txtChrg2.TabIndex = 4;
            this.txtChrg2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(722, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 60052;
            this.label5.Text = "%";
            this.label5.Visible = false;
            // 
            // txtChrg1
            // 
            this.txtChrg1.Location = new System.Drawing.Point(685, 68);
            this.txtChrg1.Name = "txtChrg1";
            this.txtChrg1.Size = new System.Drawing.Size(35, 20);
            this.txtChrg1.TabIndex = 2;
            this.txtChrg1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // pnlPayType
            // 
            this.pnlPayType.BackColor = System.Drawing.Color.Bisque;
            this.pnlPayType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPayType.Controls.Add(this.lstPayType);
            this.pnlPayType.Location = new System.Drawing.Point(185, 232);
            this.pnlPayType.Name = "pnlPayType";
            this.pnlPayType.Size = new System.Drawing.Size(147, 99);
            this.pnlPayType.TabIndex = 5546;
            this.pnlPayType.Visible = false;
            // 
            // lstPayType
            // 
            this.lstPayType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPayType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPayType.FormattingEnabled = true;
            this.lstPayType.Location = new System.Drawing.Point(8, 8);
            this.lstPayType.Name = "lstPayType";
            this.lstPayType.Size = new System.Drawing.Size(130, 80);
            this.lstPayType.TabIndex = 516;
            this.lstPayType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPayType_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(484, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 29);
            this.btnCancel.TabIndex = 5545;
            this.btnCancel.Text = "Cancel (F6)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(584, 175);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(147, 29);
            this.btnAddRow.TabIndex = 6;
            this.btnAddRow.Text = "New PayType (F2)";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // pnlLedger
            // 
            this.pnlLedger.BackColor = System.Drawing.Color.Bisque;
            this.pnlLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLedger.Controls.Add(this.lstLedger);
            this.pnlLedger.Location = new System.Drawing.Point(278, 232);
            this.pnlLedger.Name = "pnlLedger";
            this.pnlLedger.Size = new System.Drawing.Size(365, 99);
            this.pnlLedger.TabIndex = 5544;
            this.pnlLedger.Visible = false;
            // 
            // lstLedger
            // 
            this.lstLedger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLedger.FormattingEnabled = true;
            this.lstLedger.Location = new System.Drawing.Point(8, 8);
            this.lstLedger.Name = "lstLedger";
            this.lstLedger.Size = new System.Drawing.Size(348, 80);
            this.lstLedger.TabIndex = 516;
            this.lstLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLedger_KeyDown);
            // 
            // cmbFoodVoucher
            // 
            this.cmbFoodVoucher.FormattingEnabled = true;
            this.cmbFoodVoucher.Location = new System.Drawing.Point(247, 148);
            this.cmbFoodVoucher.Name = "cmbFoodVoucher";
            this.cmbFoodVoucher.Size = new System.Drawing.Size(433, 21);
            this.cmbFoodVoucher.TabIndex = 7;
            this.cmbFoodVoucher.SelectedIndexChanged += new System.EventHandler(this.cmbFoodVoucher_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 567;
            this.label2.Text = "Food Voucher :";
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PkSrNo,
            this.PayTypeName,
            this.ControlUnder,
            this.LedgerName,
            this.ShortName,
            this.IsActive,
            this.PayTypeNo,
            this.LedgerNo,
            this.ControlUnderNo,
            this.ChrgPerce});
            this.GridView.Location = new System.Drawing.Point(14, 206);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(717, 115);
            this.GridView.TabIndex = 565;
            this.GridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridView_CellEndEdit);
            this.GridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridView_EditingControlShowing);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // PkSrNo
            // 
            this.PkSrNo.DataPropertyName = "PkPayTypeLedgerNo";
            this.PkSrNo.HeaderText = "PkSRNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // PayTypeName
            // 
            this.PayTypeName.DataPropertyName = "PayTypeName";
            this.PayTypeName.HeaderText = "Type Name";
            this.PayTypeName.Name = "PayTypeName";
            this.PayTypeName.Width = 130;
            // 
            // ControlUnder
            // 
            this.ControlUnder.DataPropertyName = "ControlUnderName";
            this.ControlUnder.HeaderText = "Under Pay Type";
            this.ControlUnder.Name = "ControlUnder";
            this.ControlUnder.ReadOnly = true;
            this.ControlUnder.Width = 140;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "LedgerName";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Visible = false;
            this.LedgerName.Width = 300;
            // 
            // ShortName
            // 
            this.ShortName.DataPropertyName = "ShortName";
            this.ShortName.HeaderText = "ShortName";
            this.ShortName.Name = "ShortName";
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "IsActive";
            this.IsActive.Name = "IsActive";
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsActive.Visible = false;
            this.IsActive.Width = 70;
            // 
            // PayTypeNo
            // 
            this.PayTypeNo.DataPropertyName = "PKPayTypeNo";
            this.PayTypeNo.HeaderText = "PayTypeNo";
            this.PayTypeNo.Name = "PayTypeNo";
            this.PayTypeNo.Visible = false;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Visible = false;
            // 
            // ControlUnderNo
            // 
            this.ControlUnderNo.DataPropertyName = "ControlUnder";
            this.ControlUnderNo.HeaderText = "ControlUnderNo";
            this.ControlUnderNo.Name = "ControlUnderNo";
            this.ControlUnderNo.Visible = false;
            // 
            // ChrgPerce
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ChrgPerce.DefaultCellStyle = dataGridViewCellStyle2;
            this.ChrgPerce.HeaderText = "Pecentage";
            this.ChrgPerce.Name = "ChrgPerce";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 13);
            this.label1.TabIndex = 564;
            this.label1.Text = "Owner Pay Type Name (For Auto Posting)";
            this.label1.Visible = false;
            // 
            // cmbCreditCard
            // 
            this.cmbCreditCard.FormattingEnabled = true;
            this.cmbCreditCard.Location = new System.Drawing.Point(247, 121);
            this.cmbCreditCard.Name = "cmbCreditCard";
            this.cmbCreditCard.Size = new System.Drawing.Size(433, 21);
            this.cmbCreditCard.TabIndex = 5;
            this.cmbCreditCard.SelectedValueChanged += new System.EventHandler(this.cmbPayTypeLedger_SelectedValueChanged);
            // 
            // cmbCheque
            // 
            this.cmbCheque.FormattingEnabled = true;
            this.cmbCheque.Location = new System.Drawing.Point(247, 94);
            this.cmbCheque.Name = "cmbCheque";
            this.cmbCheque.Size = new System.Drawing.Size(433, 21);
            this.cmbCheque.TabIndex = 3;
            this.cmbCheque.SelectedValueChanged += new System.EventHandler(this.cmbPayTypeLedger_SelectedValueChanged);
            // 
            // cmbCash
            // 
            this.cmbCash.FormattingEnabled = true;
            this.cmbCash.Location = new System.Drawing.Point(247, 67);
            this.cmbCash.Name = "cmbCash";
            this.cmbCash.Size = new System.Drawing.Size(433, 21);
            this.cmbCash.TabIndex = 1;
            this.cmbCash.SelectedValueChanged += new System.EventHandler(this.cmbPayTypeLedger_SelectedValueChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(155, 6);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(576, 21);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(247, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(244, 13);
            this.label12.TabIndex = 563;
            this.label12.Text = "Account Name (For Auto Receipt Posting)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 507;
            this.label3.Text = "Credit Card : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(155, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 503;
            this.label6.Text = "Pay Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 504;
            this.label7.Text = "Company / Firm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 505;
            this.label10.Text = "Cash : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(155, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 506;
            this.label11.Text = "Cheque : ";
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(13, 325);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(243, 13);
            this.lblChkHelp.TabIndex = 60050;
            this.lblChkHelp.Text = "Select Under Pay Type Press Enter Key or F4 Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(557, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 5547;
            this.label4.Text = "F4 : Change Ledger Account";
            this.label4.Visible = false;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.BtnClose);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Location = new System.Drawing.Point(14, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(786, 431);
            this.pnlMain.TabIndex = 571;
            // 
            // PayTypeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 455);
            this.Controls.Add(this.pnlMain);
            this.Name = "PayTypeSetting";
            this.Text = "Pay Type Settings";
            this.Load += new System.EventHandler(this.PayTypeSetting_Load);
            this.Activated += new System.EventHandler(this.PayTypeSetting_Activated);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPayType.ResumeLayout(false);
            this.pnlLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbCreditCard;
        private System.Windows.Forms.ComboBox cmbCheque;
        private System.Windows.Forms.ComboBox cmbCash;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.ComboBox cmbFoodVoucher;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlLedger;
        private System.Windows.Forms.ListBox lstLedger;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlPayType;
        private System.Windows.Forms.ListBox lstPayType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtChrg2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtChrg1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtChrg4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtChrg3;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlUnder;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ControlUnderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChrgPerce;
    }
}