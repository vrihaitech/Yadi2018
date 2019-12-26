namespace Yadi.Settings.Loyalty
{
    partial class LoyaltyAssign
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
            this.ombSearchPanel = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.chkDeAssign = new System.Windows.Forms.CheckBox();
            this.btnList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlLedger = new System.Windows.Forms.Panel();
            this.lstLedger = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgBill = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.ombSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.ombSearchPanel);
            this.pnlMain.Controls.Add(this.chkDeAssign);
            this.pnlMain.Controls.Add(this.btnList);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.pnlLedger);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.dgBill);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(12, 11);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(494, 434);
            this.pnlMain.TabIndex = 0;
            // 
            // ombSearchPanel
            // 
            this.ombSearchPanel.Controls.Add(this.dataGridView1);
            this.ombSearchPanel.Controls.Add(this.panel1);
            this.ombSearchPanel.Controls.Add(this.panel2);
            this.ombSearchPanel.Controls.Add(this.panel3);
            this.ombSearchPanel.Location = new System.Drawing.Point(6, 29);
            this.ombSearchPanel.Name = "ombSearchPanel";
            this.ombSearchPanel.Size = new System.Drawing.Size(472, 333);
            this.ombSearchPanel.TabIndex = 10036;
            this.ombSearchPanel.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkStatus,
            this.dataGridViewTextBoxColumn1,
            this.LedgerName1,
            this.MobileNo1,
            this.MobileNo2,
            this.PhNo1,
            this.PhNo2,
            this.Address,
            this.ContactPerson});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(472, 223);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 40);
            this.panel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Search :";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(76, 10);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(305, 20);
            this.TxtSearch.TabIndex = 1;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(388, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkSelectAll);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 293);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 40);
            this.panel2.TabIndex = 5;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(352, 6);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(91, 17);
            this.chkSelectAll.TabIndex = 10039;
            this.chkSelectAll.Text = "Select All (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ctrl + A : Select All";
            this.label4.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(9, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 33);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(87, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbFemale);
            this.panel3.Controls.Add(this.rbMale);
            this.panel3.Controls.Add(this.rbAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(472, 30);
            this.panel3.TabIndex = 7;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(194, 7);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbFemale.TabIndex = 2;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            this.rbFemale.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Location = new System.Drawing.Point(100, 7);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(48, 17);
            this.rbMale.TabIndex = 1;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            this.rbMale.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(9, 7);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // chkDeAssign
            // 
            this.chkDeAssign.AutoSize = true;
            this.chkDeAssign.Location = new System.Drawing.Point(387, 367);
            this.chkDeAssign.Name = "chkDeAssign";
            this.chkDeAssign.Size = new System.Drawing.Size(92, 17);
            this.chkDeAssign.TabIndex = 10038;
            this.chkDeAssign.Text = "DeAssign (F8)";
            this.chkDeAssign.UseVisualStyleBackColor = true;
            this.chkDeAssign.CheckedChanged += new System.EventHandler(this.chkDeAssign_CheckedChanged);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(188, 389);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(80, 30);
            this.btnList.TabIndex = 10037;
            this.btnList.Text = "List (F6)";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 10035;
            this.label2.Text = "F3 : Go To Last Line";
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(6, 163);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 10033;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 60);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save (F4)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlLedger
            // 
            this.pnlLedger.BackColor = System.Drawing.Color.Bisque;
            this.pnlLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLedger.Controls.Add(this.lstLedger);
            this.pnlLedger.Location = new System.Drawing.Point(34, 90);
            this.pnlLedger.Name = "pnlLedger";
            this.pnlLedger.Size = new System.Drawing.Size(108, 30);
            this.pnlLedger.TabIndex = 10034;
            this.pnlLedger.Visible = false;
            // 
            // lstLedger
            // 
            this.lstLedger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLedger.FormattingEnabled = true;
            this.lstLedger.Location = new System.Drawing.Point(8, 7);
            this.lstLedger.Name = "lstLedger";
            this.lstLedger.Size = new System.Drawing.Size(279, 197);
            this.lstLedger.TabIndex = 1;
            this.lstLedger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstLedger_KeyPress);
            this.lstLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLedger_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(93, 367);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit (F12)";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgBill
            // 
            this.dgBill.AllowUserToAddRows = false;
            this.dgBill.AllowUserToDeleteRows = false;
            this.dgBill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.LedgerName,
            this.IsActive,
            this.LedgerNo,
            this.PkSrNo});
            this.dgBill.Location = new System.Drawing.Point(10, 33);
            this.dgBill.Name = "dgBill";
            this.dgBill.Size = new System.Drawing.Size(472, 328);
            this.dgBill.TabIndex = 0;
            this.dgBill.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBill_CellFormatting);
            this.dgBill.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBill_CellEndEdit);
            this.dgBill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBill_KeyDown);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SrNo";
            this.Column1.HeaderText = "Sr";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // LedgerName
            // 
            this.LedgerName.DataPropertyName = "LedgerName";
            this.LedgerName.HeaderText = "Customer Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            this.LedgerName.Width = 340;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsActive.Width = 50;
            // 
            // LedgerNo
            // 
            this.LedgerNo.DataPropertyName = "LedgerNo";
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.DataPropertyName = "PkSrNo";
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 26);
            this.label1.TabIndex = 10000;
            this.label1.Text = "Loyalty Assigns";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // chkStatus
            // 
            this.chkStatus.HeaderText = "Status";
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkStatus.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LedgerNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "LedgerNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // LedgerName1
            // 
            this.LedgerName1.DataPropertyName = "LedgerName";
            this.LedgerName1.HeaderText = "Name";
            this.LedgerName1.Name = "LedgerName1";
            this.LedgerName1.Width = 150;
            // 
            // MobileNo1
            // 
            this.MobileNo1.DataPropertyName = "MobileNo1";
            this.MobileNo1.HeaderText = "Mobile No1";
            this.MobileNo1.Name = "MobileNo1";
            // 
            // MobileNo2
            // 
            this.MobileNo2.DataPropertyName = "MobileNo2";
            this.MobileNo2.HeaderText = "Mobile No2";
            this.MobileNo2.Name = "MobileNo2";
            // 
            // PhNo1
            // 
            this.PhNo1.DataPropertyName = "PhNo1";
            this.PhNo1.HeaderText = "Ph No1";
            this.PhNo1.Name = "PhNo1";
            // 
            // PhNo2
            // 
            this.PhNo2.DataPropertyName = "PhNo2";
            this.PhNo2.HeaderText = "Ph No2";
            this.PhNo2.Name = "PhNo2";
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // ContactPerson
            // 
            this.ContactPerson.DataPropertyName = "ContactPerson";
            this.ContactPerson.HeaderText = "ContactPerson";
            this.ContactPerson.Name = "ContactPerson";
            // 
            // LoyaltyAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 459);
            this.Controls.Add(this.pnlMain);
            this.Name = "LoyaltyAssign";
            this.Text = "LoyaltyMTD";
            this.Load += new System.EventHandler(this.LoyaltyMTD_Load);
            this.Activated += new System.EventHandler(this.LoyaltyAssign_Activated);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ombSearchPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgBill;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Panel pnlLedger;
        private System.Windows.Forms.ListBox lstLedger;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Panel ombSearchPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkDeAssign;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactPerson;
    }
}