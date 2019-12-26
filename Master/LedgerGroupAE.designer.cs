namespace Yadi.Master
{
    partial class LedgerGroupAE
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGroupName = new System.Windows.Forms.Panel();
            this.lstGroupName = new System.Windows.Forms.ListBox();
            this.btnLangLongDesc = new System.Windows.Forms.Button();
            this.lblCityLN = new System.Windows.Forms.Label();
            this.txtLedgerLangName = new System.Windows.Forms.TextBox();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.lblStar6 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.txtLedgerName = new System.Windows.Forms.TextBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain = new OMControls.OMBPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.PnlParty = new System.Windows.Forms.Panel();
            this.BtnPartyAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.GvParty = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectParty = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlGroupName.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel4.SuspendLayout();
            this.PnlParty.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvParty)).BeginInit();
            this.SuspendLayout();
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlGroupName);
            this.panel1.Controls.Add(this.btnLangLongDesc);
            this.panel1.Controls.Add(this.lblCityLN);
            this.panel1.Controls.Add(this.txtLedgerLangName);
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.lblStar6);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.txtLedgerName);
            this.panel1.Controls.Add(this.txtGroupName);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(22, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 197);
            this.panel1.TabIndex = 0;
            // 
            // pnlGroupName
            // 
            this.pnlGroupName.BackColor = System.Drawing.Color.Bisque;
            this.pnlGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGroupName.Controls.Add(this.lstGroupName);
            this.pnlGroupName.Location = new System.Drawing.Point(123, 76);
            this.pnlGroupName.Name = "pnlGroupName";
            this.pnlGroupName.Size = new System.Drawing.Size(222, 76);
            this.pnlGroupName.TabIndex = 16000035;
            this.pnlGroupName.Visible = false;
            // 
            // lstGroupName
            // 
            this.lstGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGroupName.FormattingEnabled = true;
            this.lstGroupName.Location = new System.Drawing.Point(6, 3);
            this.lstGroupName.Name = "lstGroupName";
            this.lstGroupName.Size = new System.Drawing.Size(209, 69);
            this.lstGroupName.Sorted = true;
            this.lstGroupName.TabIndex = 516;
            this.lstGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGroupName_KeyDown);
            // 
            // btnLangLongDesc
            // 
            this.btnLangLongDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLangLongDesc.Location = new System.Drawing.Point(360, 113);
            this.btnLangLongDesc.Name = "btnLangLongDesc";
            this.btnLangLongDesc.Size = new System.Drawing.Size(21, 21);
            this.btnLangLongDesc.TabIndex = 190073;
            this.btnLangLongDesc.Text = "..";
            this.btnLangLongDesc.UseVisualStyleBackColor = true;
            this.btnLangLongDesc.Click += new System.EventHandler(this.btnLangLongDesc_Click);
            // 
            // lblCityLN
            // 
            this.lblCityLN.AutoSize = true;
            this.lblCityLN.Location = new System.Drawing.Point(15, 117);
            this.lblCityLN.Name = "lblCityLN";
            this.lblCityLN.Size = new System.Drawing.Size(68, 13);
            this.lblCityLN.TabIndex = 190072;
            this.lblCityLN.Text = "Lang Name :";
            // 
            // txtLedgerLangName
            // 
            this.txtLedgerLangName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtLedgerLangName.Location = new System.Drawing.Point(122, 115);
            this.txtLedgerLangName.MaxLength = 25;
            this.txtLedgerLangName.Name = "txtLedgerLangName";
            this.txtLedgerLangName.Size = new System.Drawing.Size(223, 21);
            this.txtLedgerLangName.TabIndex = 3;
            this.txtLedgerLangName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLedgerLangName_KeyPress);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(174, 88);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.Location = new System.Drawing.Point(357, 28);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 916;
            this.lblStar1.Text = "*";
            // 
            // lblStar6
            // 
            this.lblStar6.AutoSize = true;
            this.lblStar6.Location = new System.Drawing.Point(357, 197);
            this.lblStar6.Name = "lblStar6";
            this.lblStar6.Size = new System.Drawing.Size(11, 13);
            this.lblStar6.TabIndex = 912;
            this.lblStar6.Text = "*";
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.Location = new System.Drawing.Point(358, 56);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 911;
            this.lblStar2.Text = "*";
            this.lblStar2.Visible = false;
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLedgerName.Location = new System.Drawing.Point(122, 24);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Size = new System.Drawing.Size(223, 20);
            this.txtLedgerName.TabIndex = 1;
            this.txtLedgerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLedgerName_KeyDown);
            this.txtLedgerName.Leave += new System.EventHandler(this.txtLedgerName_Leave);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(122, 54);
            this.txtGroupName.MaxLength = 5;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(223, 20);
            this.txtGroupName.TabIndex = 2;
            this.txtGroupName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGroupName_KeyPress);
            this.txtGroupName.Leave += new System.EventHandler(this.txtGroupName_Leave);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(123, 87);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 3;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            this.chkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkActive_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 507;
            this.label3.Text = "Active Status :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 502;
            this.label2.Text = "Group  :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 501;
            this.label1.Text = "Name :";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.panel4);
            this.pnlMain.Controls.Add(this.PnlParty);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(17, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(874, 509);
            this.pnlMain.TabIndex = 18;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnNext);
            this.panel4.Controls.Add(this.btnPrev);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.btnLast);
            this.panel4.Controls.Add(this.btnNew);
            this.panel4.Controls.Add(this.btnFirst);
            this.panel4.Controls.Add(this.BtnSave);
            this.panel4.Controls.Add(this.btnUpdate);
            this.panel4.Controls.Add(this.btnDelete);
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.btnSearch);
            this.panel4.Location = new System.Drawing.Point(22, 210);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(425, 103);
            this.panel4.TabIndex = 21;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(85, 70);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 24;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(45, 70);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 23;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(337, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(125, 70);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 25;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(5, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(5, 70);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 22;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(5, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(88, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(254, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(88, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(171, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // PnlParty
            // 
            this.PnlParty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PnlParty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PnlParty.Controls.Add(this.BtnPartyAdd);
            this.PnlParty.Controls.Add(this.panel3);
            this.PnlParty.Controls.Add(this.GvParty);
            this.PnlParty.Location = new System.Drawing.Point(454, 8);
            this.PnlParty.Name = "PnlParty";
            this.PnlParty.Size = new System.Drawing.Size(400, 490);
            this.PnlParty.TabIndex = 20;
            // 
            // BtnPartyAdd
            // 
            this.BtnPartyAdd.Location = new System.Drawing.Point(7, 446);
            this.BtnPartyAdd.Name = "BtnPartyAdd";
            this.BtnPartyAdd.Size = new System.Drawing.Size(43, 35);
            this.BtnPartyAdd.TabIndex = 26;
            this.BtnPartyAdd.Text = "OK";
            this.BtnPartyAdd.UseVisualStyleBackColor = true;
            this.BtnPartyAdd.Click += new System.EventHandler(this.BtnPartyAdd_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(7, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 32);
            this.panel3.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SandyBrown;
            this.label4.Location = new System.Drawing.Point(136, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Party Name";
            // 
            // GvParty
            // 
            this.GvParty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GvParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvParty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.LedgerName,
            this.LedgerNo,
            this.SelectParty});
            this.GvParty.Location = new System.Drawing.Point(7, 37);
            this.GvParty.Name = "GvParty";
            this.GvParty.Size = new System.Drawing.Size(382, 405);
            this.GvParty.TabIndex = 0;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.Visible = false;
            this.SrNo.Width = 40;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "Ledger Name";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.Width = 260;
            // 
            // LedgerNo
            // 
            this.LedgerNo.HeaderText = "LedgerNo";
            this.LedgerNo.Name = "LedgerNo";
            this.LedgerNo.Visible = false;
            this.LedgerNo.Width = 60;
            // 
            // SelectParty
            // 
            this.SelectParty.HeaderText = "Select";
            this.SelectParty.Name = "SelectParty";
            // 
            // LedgerGroupAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 545);
            this.Controls.Add(this.pnlMain);
            this.Name = "LedgerGroupAE";
            this.Text = "Ledger Group Master";
            this.Activated += new System.EventHandler(this.LedgerGroupAE_Activated);
            this.Deactivate += new System.EventHandler(this.LedgerGroupAE_Deactivate);
            this.Load += new System.EventHandler(this.LedgerGroupAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlGroupName.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.PnlParty.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvParty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLedgerName;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar6;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Button btnLangLongDesc;
        private System.Windows.Forms.Label lblCityLN;
        private System.Windows.Forms.TextBox txtLedgerLangName;
        private System.Windows.Forms.Panel pnlGroupName;
        private System.Windows.Forms.ListBox lstGroupName;
        private System.Windows.Forms.Panel PnlParty;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView GvParty;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button BtnPartyAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectParty;
    }
}