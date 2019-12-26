namespace Yadi.Master
{
    partial class CityAE
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtshortName = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCityName = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlstate = new System.Windows.Forms.Panel();
            this.lststate = new System.Windows.Forms.ListBox();
            this.txtstate = new System.Windows.Forms.TextBox();
            this.btnLangLongDesc = new System.Windows.Forms.Button();
            this.lblCityLN = new System.Windows.Forms.Label();
            this.txtCityLangName = new System.Windows.Forms.TextBox();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.lblStar4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlstate.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "State Name :";
            // 
            // txtshortName
            // 
            this.txtshortName.Location = new System.Drawing.Point(174, 43);
            this.txtshortName.MaxLength = 100;
            this.txtshortName.Name = "txtshortName";
            this.txtshortName.Size = new System.Drawing.Size(83, 20);
            this.txtshortName.TabIndex = 2;
            this.txtshortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtshortName_KeyDown);
            this.txtshortName.Leave += new System.EventHandler(this.txtshortName_Leave);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(98, 317);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(58, 317);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(138, 317);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 15;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(18, 317);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 12;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(350, 251);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(18, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 60);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(175, 119);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            this.chkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkActive_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Active Status :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Short Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Name :";
            // 
            // txtCityName
            // 
            this.txtCityName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCityName.Location = new System.Drawing.Point(174, 11);
            this.txtCityName.MaxLength = 25;
            this.txtCityName.Name = "txtCityName";
            this.txtCityName.Size = new System.Drawing.Size(197, 20);
            this.txtCityName.TabIndex = 1;
            this.txtCityName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCityName_KeyDown);
            this.txtCityName.Leave += new System.EventHandler(this.txtCityName_Leave);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(267, 251);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlstate);
            this.panel1.Controls.Add(this.txtstate);
            this.panel1.Controls.Add(this.btnLangLongDesc);
            this.panel1.Controls.Add(this.lblCityLN);
            this.panel1.Controls.Add(this.txtCityLangName);
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.lblStar4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCityName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.txtshortName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(18, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 215);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // pnlstate
            // 
            this.pnlstate.BackColor = System.Drawing.Color.Bisque;
            this.pnlstate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlstate.Controls.Add(this.lststate);
            this.pnlstate.Location = new System.Drawing.Point(175, 96);
            this.pnlstate.Name = "pnlstate";
            this.pnlstate.Size = new System.Drawing.Size(198, 40);
            this.pnlstate.TabIndex = 16000034;
            this.pnlstate.Visible = false;
            // 
            // lststate
            // 
            this.lststate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lststate.FormattingEnabled = true;
            this.lststate.Location = new System.Drawing.Point(6, 6);
            this.lststate.Name = "lststate";
            this.lststate.Size = new System.Drawing.Size(185, 17);
            this.lststate.Sorted = true;
            this.lststate.TabIndex = 516;
            this.lststate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lststate_KeyDown);
            // 
            // txtstate
            // 
            this.txtstate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtstate.Location = new System.Drawing.Point(174, 78);
            this.txtstate.MaxLength = 25;
            this.txtstate.Name = "txtstate";
            this.txtstate.Size = new System.Drawing.Size(197, 20);
            this.txtstate.TabIndex = 3;
            this.txtstate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtstate_KeyPress);
            // 
            // btnLangLongDesc
            // 
            this.btnLangLongDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLangLongDesc.Location = new System.Drawing.Point(378, 155);
            this.btnLangLongDesc.Name = "btnLangLongDesc";
            this.btnLangLongDesc.Size = new System.Drawing.Size(21, 21);
            this.btnLangLongDesc.TabIndex = 190070;
            this.btnLangLongDesc.Text = "..";
            this.btnLangLongDesc.UseVisualStyleBackColor = true;
            this.btnLangLongDesc.Click += new System.EventHandler(this.btnLangLongDesc_Click);
            // 
            // lblCityLN
            // 
            this.lblCityLN.AutoSize = true;
            this.lblCityLN.Location = new System.Drawing.Point(21, 158);
            this.lblCityLN.Name = "lblCityLN";
            this.lblCityLN.Size = new System.Drawing.Size(71, 13);
            this.lblCityLN.TabIndex = 190044;
            this.lblCityLN.Text = " Lang Name :";
            // 
            // txtCityLangName
            // 
            this.txtCityLangName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCityLangName.Location = new System.Drawing.Point(174, 155);
            this.txtCityLangName.MaxLength = 25;
            this.txtCityLangName.Name = "txtCityLangName";
            this.txtCityLangName.Size = new System.Drawing.Size(197, 21);
            this.txtCityLangName.TabIndex = 3;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.Location = new System.Drawing.Point(219, 115);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(177, 30);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.Location = new System.Drawing.Point(377, 14);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 910;
            this.lblStar1.Text = "*";
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.Location = new System.Drawing.Point(263, 50);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 909;
            this.lblStar2.Text = "*";
            this.lblStar2.Visible = false;
            // 
            // lblStar4
            // 
            this.lblStar4.AutoSize = true;
            this.lblStar4.Location = new System.Drawing.Point(378, 123);
            this.lblStar4.Name = "lblStar4";
            this.lblStar4.Size = new System.Drawing.Size(11, 13);
            this.lblStar4.TabIndex = 907;
            this.lblStar4.Text = "*";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(184, 251);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(101, 251);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(101, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(18, 251);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(450, 362);
            this.pnlMain.TabIndex = 17;
            // 
            // CityAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 391);
            this.Controls.Add(this.pnlMain);
            this.Name = "CityAE";
            this.Text = "City  Master";
            this.Deactivate += new System.EventHandler(this.CityAE_Deactivate);
            this.Load += new System.EventHandler(this.CityAE_Load);
            this.Activated += new System.EventHandler(this.CityAE_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlstate.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtshortName;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCityName;
        private System.Windows.Forms.ErrorProvider EP;
        public System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblStar4;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblCityLN;
        private System.Windows.Forms.TextBox txtCityLangName;
        private System.Windows.Forms.Button btnLangLongDesc;
        private System.Windows.Forms.TextBox txtstate;
        private System.Windows.Forms.Panel pnlstate;
        private System.Windows.Forms.ListBox lststate;
    }
}