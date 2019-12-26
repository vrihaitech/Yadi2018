namespace Yadi.Master
{
    partial class StateAE
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtshortName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtStateName = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlcountry = new System.Windows.Forms.Panel();
            this.lstcountry = new System.Windows.Forms.ListBox();
            this.txtcountry = new System.Windows.Forms.TextBox();
            this.lblStateCode = new System.Windows.Forms.Label();
            this.txtStateCode = new System.Windows.Forms.TextBox();
            this.btnLangLongDesc = new System.Windows.Forms.Button();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.txtStateLangName = new System.Windows.Forms.TextBox();
            this.lblStateLangName = new System.Windows.Forms.Label();
            this.lblStar3 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlcountry.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(406, 77);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 13);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "New";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Country Name :";
            // 
            // txtshortName
            // 
            this.txtshortName.Location = new System.Drawing.Point(138, 41);
            this.txtshortName.MaxLength = 100;
            this.txtshortName.Name = "txtshortName";
            this.txtshortName.Size = new System.Drawing.Size(89, 20);
            this.txtshortName.TabIndex = 1;
            this.txtshortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtshortName_KeyDown);
            this.txtshortName.Leave += new System.EventHandler(this.txtshortName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Active Status :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Short Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Name :";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(138, 117);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            this.chkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkActive_KeyDown);
            // 
            // txtStateName
            // 
            this.txtStateName.Location = new System.Drawing.Point(138, 10);
            this.txtStateName.MaxLength = 25;
            this.txtStateName.Name = "txtStateName";
            this.txtStateName.Size = new System.Drawing.Size(243, 20);
            this.txtStateName.TabIndex = 0;
            this.txtStateName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStateName_KeyDown);
            this.txtStateName.Leave += new System.EventHandler(this.txtStateName_Leave);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(104, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(20, 224);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(103, 224);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(186, 224);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(269, 224);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(100, 290);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(60, 290);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(140, 290);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 15;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(20, 290);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 12;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(352, 224);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 11;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(21, 224);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlcountry);
            this.panel1.Controls.Add(this.txtcountry);
            this.panel1.Controls.Add(this.lblStateCode);
            this.panel1.Controls.Add(this.txtStateCode);
            this.panel1.Controls.Add(this.btnLangLongDesc);
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.txtStateLangName);
            this.panel1.Controls.Add(this.lblStateLangName);
            this.panel1.Controls.Add(this.lblStar3);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtStateName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtshortName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Location = new System.Drawing.Point(20, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 200);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // pnlcountry
            // 
            this.pnlcountry.BackColor = System.Drawing.Color.Bisque;
            this.pnlcountry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlcountry.Controls.Add(this.lstcountry);
            this.pnlcountry.Location = new System.Drawing.Point(139, 93);
            this.pnlcountry.Name = "pnlcountry";
            this.pnlcountry.Size = new System.Drawing.Size(242, 23);
            this.pnlcountry.TabIndex = 16000037;
            this.pnlcountry.Visible = false;
            // 
            // lstcountry
            // 
            this.lstcountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstcountry.FormattingEnabled = true;
            this.lstcountry.Location = new System.Drawing.Point(5, 3);
            this.lstcountry.Name = "lstcountry";
            this.lstcountry.Size = new System.Drawing.Size(232, 17);
            this.lstcountry.Sorted = true;
            this.lstcountry.TabIndex = 516;
            this.lstcountry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstcountry_KeyDown);
            // 
            // txtcountry
            // 
            this.txtcountry.Location = new System.Drawing.Point(138, 77);
            this.txtcountry.MaxLength = 25;
            this.txtcountry.Name = "txtcountry";
            this.txtcountry.Size = new System.Drawing.Size(243, 20);
            this.txtcountry.TabIndex = 3;
            this.txtcountry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcountry_KeyPress);
            // 
            // lblStateCode
            // 
            this.lblStateCode.AutoSize = true;
            this.lblStateCode.Location = new System.Drawing.Point(272, 44);
            this.lblStateCode.Name = "lblStateCode";
            this.lblStateCode.Size = new System.Drawing.Size(38, 13);
            this.lblStateCode.TabIndex = 190072;
            this.lblStateCode.Text = "Code :";
            // 
            // txtStateCode
            // 
            this.txtStateCode.Location = new System.Drawing.Point(333, 41);
            this.txtStateCode.MaxLength = 5;
            this.txtStateCode.Name = "txtStateCode";
            this.txtStateCode.Size = new System.Drawing.Size(48, 20);
            this.txtStateCode.TabIndex = 2;
            this.txtStateCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStateCode_KeyDown);
            // 
            // btnLangLongDesc
            // 
            this.btnLangLongDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLangLongDesc.Location = new System.Drawing.Point(390, 158);
            this.btnLangLongDesc.Name = "btnLangLongDesc";
            this.btnLangLongDesc.Size = new System.Drawing.Size(21, 21);
            this.btnLangLongDesc.TabIndex = 190070;
            this.btnLangLongDesc.Text = "..";
            this.btnLangLongDesc.UseVisualStyleBackColor = true;
            this.btnLangLongDesc.Click += new System.EventHandler(this.btnLangLongDesc_Click);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(184, 119);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // txtStateLangName
            // 
            this.txtStateLangName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateLangName.Location = new System.Drawing.Point(139, 158);
            this.txtStateLangName.MaxLength = 25;
            this.txtStateLangName.Name = "txtStateLangName";
            this.txtStateLangName.Size = new System.Drawing.Size(243, 21);
            this.txtStateLangName.TabIndex = 3;
            // 
            // lblStateLangName
            // 
            this.lblStateLangName.AutoSize = true;
            this.lblStateLangName.Location = new System.Drawing.Point(24, 160);
            this.lblStateLangName.Name = "lblStateLangName";
            this.lblStateLangName.Size = new System.Drawing.Size(68, 13);
            this.lblStateLangName.TabIndex = 190044;
            this.lblStateLangName.Text = "Lang Name :";
            // 
            // lblStar3
            // 
            this.lblStar3.AutoSize = true;
            this.lblStar3.Location = new System.Drawing.Point(387, 77);
            this.lblStar3.Name = "lblStar3";
            this.lblStar3.Size = new System.Drawing.Size(11, 13);
            this.lblStar3.TabIndex = 907;
            this.lblStar3.Text = "*";
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.Location = new System.Drawing.Point(235, 44);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 906;
            this.lblStar2.Text = "*";
            this.lblStar2.Visible = false;
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.Location = new System.Drawing.Point(387, 17);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 905;
            this.lblStar1.Text = "*";
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(21, 23);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(502, 325);
            this.pnlMain.TabIndex = 29;
            // 
            // StateAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 360);
            this.Controls.Add(this.pnlMain);
            this.Name = "StateAE";
            this.Text = "State Master";
            this.Deactivate += new System.EventHandler(this.StateAE_Deactivate);
            this.Load += new System.EventHandler(this.StateAE_Load);
            this.Activated += new System.EventHandler(this.StateAE_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlcountry.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtshortName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtStateName;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Panel panel1;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar3;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.TextBox txtStateLangName;
        private System.Windows.Forms.Label lblStateLangName;
        private System.Windows.Forms.Button btnLangLongDesc;
        private System.Windows.Forms.Label lblStateCode;
        private System.Windows.Forms.TextBox txtStateCode;
        private System.Windows.Forms.TextBox txtcountry;
        private System.Windows.Forms.Panel pnlcountry;
        private System.Windows.Forms.ListBox lstcountry;
    }
}