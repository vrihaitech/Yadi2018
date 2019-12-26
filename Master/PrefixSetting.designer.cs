namespace Yadi.Master
{
    partial class PrefixSetting
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
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlSearch = new OMControls.OMPanel();
            this.btnSearchCancel = new System.Windows.Forms.Button();
            this.btnSearchOk = new System.Windows.Forms.Button();
            this.txtBrPrefix = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1 = new OMControls.OMBPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbVatPurchase = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cmbVatSales = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCategoryName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDepartmentName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGroupNo2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGroupNo1 = new System.Windows.Forms.ComboBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblChkHelp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.pnlMain.BorderRadius = 2;
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1030, 262);
            this.pnlMain.TabIndex = 10004;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderRadius = 3;
            this.pnlSearch.Controls.Add(this.btnSearchCancel);
            this.pnlSearch.Controls.Add(this.btnSearchOk);
            this.pnlSearch.Controls.Add(this.txtBrPrefix);
            this.pnlSearch.Controls.Add(this.label21);
            this.pnlSearch.CornerRadius = 3;
            this.pnlSearch.GradientBottom = System.Drawing.Color.LightGray;
            this.pnlSearch.GradientMiddle = System.Drawing.Color.White;
            this.pnlSearch.GradientShow = true;
            this.pnlSearch.GradientTop = System.Drawing.Color.Silver;
            this.pnlSearch.Location = new System.Drawing.Point(395, 66);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(278, 101);
            this.pnlSearch.TabIndex = 3101;
            this.pnlSearch.Visible = false;
            // 
            // btnSearchCancel
            // 
            this.btnSearchCancel.Location = new System.Drawing.Point(127, 59);
            this.btnSearchCancel.Name = "btnSearchCancel";
            this.btnSearchCancel.Size = new System.Drawing.Size(73, 24);
            this.btnSearchCancel.TabIndex = 10008;
            this.btnSearchCancel.Text = "Cancel";
            this.btnSearchCancel.UseVisualStyleBackColor = true;
            this.btnSearchCancel.Click += new System.EventHandler(this.btnSearchCancel_Click);
            // 
            // btnSearchOk
            // 
            this.btnSearchOk.Location = new System.Drawing.Point(49, 60);
            this.btnSearchOk.Name = "btnSearchOk";
            this.btnSearchOk.Size = new System.Drawing.Size(62, 24);
            this.btnSearchOk.TabIndex = 10007;
            this.btnSearchOk.Text = "OK";
            this.btnSearchOk.UseVisualStyleBackColor = true;
            this.btnSearchOk.Click += new System.EventHandler(this.btnSearchOk_Click);
            // 
            // txtBrPrefix
            // 
            this.txtBrPrefix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBrPrefix.Location = new System.Drawing.Point(118, 16);
            this.txtBrPrefix.MaxLength = 5;
            this.txtBrPrefix.Name = "txtBrPrefix";
            this.txtBrPrefix.Size = new System.Drawing.Size(100, 20);
            this.txtBrPrefix.TabIndex = 10003;
            this.txtBrPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBrPrefix_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(7, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 13);
            this.label21.TabIndex = 1000201;
            this.label21.Text = "Barcode Prefix :";
            // 
            // panel1
            // 
            this.panel1.BorderColor = System.Drawing.Color.Silver;
            this.panel1.BorderRadius = 3;
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbCategoryName);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbDepartmentName);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbGroupNo2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbCompanyName);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbGroupNo1);
            this.panel1.Controls.Add(this.txtItemName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(15, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 198);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(680, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Active Status :";
            // 
            // chkActive
            // 
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(805, 136);
            this.chkActive.Margin = new System.Windows.Forms.Padding(0);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 9;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbVatPurchase);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cmbVatSales);
            this.groupBox1.Location = new System.Drawing.Point(9, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 61);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tax Details";
            // 
            // cmbVatPurchase
            // 
            this.cmbVatPurchase.FormattingEnabled = true;
            this.cmbVatPurchase.Location = new System.Drawing.Point(353, 28);
            this.cmbVatPurchase.MaxLength = 25;
            this.cmbVatPurchase.Name = "cmbVatPurchase";
            this.cmbVatPurchase.Size = new System.Drawing.Size(230, 21);
            this.cmbVatPurchase.TabIndex = 8;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(417, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(52, 13);
            this.label26.TabIndex = 10009;
            this.label26.Text = "Purchase";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(175, 9);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(33, 13);
            this.label25.TabIndex = 10008;
            this.label25.Text = "Sales";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(39, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(28, 13);
            this.label24.TabIndex = 10007;
            this.label24.Text = "VAT";
            // 
            // cmbVatSales
            // 
            this.cmbVatSales.FormattingEnabled = true;
            this.cmbVatSales.Location = new System.Drawing.Point(104, 26);
            this.cmbVatSales.Name = "cmbVatSales";
            this.cmbVatSales.Size = new System.Drawing.Size(230, 21);
            this.cmbVatSales.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Barcode Prefix :";
            // 
            // cmbCategoryName
            // 
            this.cmbCategoryName.FormattingEnabled = true;
            this.cmbCategoryName.Location = new System.Drawing.Point(631, 63);
            this.cmbCategoryName.MaxLength = 25;
            this.cmbCategoryName.Name = "cmbCategoryName";
            this.cmbCategoryName.Size = new System.Drawing.Size(300, 21);
            this.cmbCategoryName.TabIndex = 5;
            this.cmbCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategoryName_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(495, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Category Name  :";
            // 
            // cmbDepartmentName
            // 
            this.cmbDepartmentName.FormattingEnabled = true;
            this.cmbDepartmentName.Location = new System.Drawing.Point(152, 63);
            this.cmbDepartmentName.MaxLength = 25;
            this.cmbDepartmentName.Name = "cmbDepartmentName";
            this.cmbDepartmentName.Size = new System.Drawing.Size(300, 21);
            this.cmbDepartmentName.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 61;
            this.label8.Text = "Department Name :";
            // 
            // cmbGroupNo2
            // 
            this.cmbGroupNo2.FormattingEnabled = true;
            this.cmbGroupNo2.Location = new System.Drawing.Point(631, 9);
            this.cmbGroupNo2.MaxLength = 25;
            this.cmbGroupNo2.Name = "cmbGroupNo2";
            this.cmbGroupNo2.Size = new System.Drawing.Size(300, 21);
            this.cmbGroupNo2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(495, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Main Group :";
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(631, 36);
            this.cmbCompanyName.MaxLength = 25;
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(300, 21);
            this.cmbCompanyName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(495, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Company Name :";
            // 
            // cmbGroupNo1
            // 
            this.cmbGroupNo1.FormattingEnabled = true;
            this.cmbGroupNo1.Location = new System.Drawing.Point(152, 35);
            this.cmbGroupNo1.MaxLength = 25;
            this.cmbGroupNo1.Name = "cmbGroupNo1";
            this.cmbGroupNo1.Size = new System.Drawing.Size(300, 21);
            this.cmbGroupNo1.TabIndex = 2;
            // 
            // txtItemName
            // 
            this.txtItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemName.Location = new System.Drawing.Point(151, 6);
            this.txtItemName.MaxLength = 50;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(300, 20);
            this.txtItemName.TabIndex = 0;
            this.txtItemName.Leave += new System.EventHandler(this.txtItemName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Brand Name :";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(513, 223);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 20);
            this.btnNext.TabIndex = 19;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(480, 223);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 20);
            this.btnPrev.TabIndex = 18;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(447, 223);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 20);
            this.btnFirst.TabIndex = 17;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(546, 223);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 20);
            this.btnLast.TabIndex = 20;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(345, 221);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 24);
            this.BtnExit.TabIndex = 16;
            this.BtnExit.Text = "&Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(179, 221);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 24);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(262, 221);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 24);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(13, 221);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 24);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(13, 221);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 24);
            this.btnNew.TabIndex = 11;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(96, 221);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 24);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(96, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(695, 158);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // PrefixSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 310);
            this.Controls.Add(this.pnlMain);
            this.Name = "PrefixSetting";
            this.Text = "Barcode Prefix";
            this.Load += new System.EventHandler(this.PrefixSetting_Load);
            this.Activated += new System.EventHandler(this.PrefixSetting_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider EP;
        private OMControls.OMBPanel panel1;
        private System.Windows.Forms.ComboBox cmbGroupNo1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDepartmentName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbGroupNo2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCategoryName;
        private System.Windows.Forms.Label label9;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbVatSales;
        private System.Windows.Forms.ComboBox cmbVatPurchase;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkActive;
        private OMControls.OMPanel pnlSearch;
        private System.Windows.Forms.Button btnSearchCancel;
        private System.Windows.Forms.Button btnSearchOk;
        private System.Windows.Forms.TextBox txtBrPrefix;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblChkHelp;
    }
}