namespace Yadi.Master
{
    partial class ItemGroupAE
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
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStockGroupName = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.btnLangDesc = new System.Windows.Forms.Button();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.labellan = new System.Windows.Forms.Label();
            this.lblStar5 = new System.Windows.Forms.Label();
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
            this.pnlMain = new OMControls.OMBPanel();
            this.btnNewManufacturer = new System.Windows.Forms.Button();
            this.lblMfgComp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(128, 53);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 3;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkActive_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Active Status :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = " Name :";
            // 
            // txtStockGroupName
            // 
            this.txtStockGroupName.Location = new System.Drawing.Point(128, 18);
            this.txtStockGroupName.MaxLength = 50;
            this.txtStockGroupName.Name = "txtStockGroupName";
            this.txtStockGroupName.Size = new System.Drawing.Size(243, 20);
            this.txtStockGroupName.TabIndex = 1;
            this.txtStockGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockGroupName_KeyDown);
            this.txtStockGroupName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockGroupName_KeyPress);
            this.txtStockGroupName.Leave += new System.EventHandler(this.txtStockGroupName_Leave);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.btnLangDesc);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.txtLanguage);
            this.panel1.Controls.Add(this.labellan);
            this.panel1.Controls.Add(this.txtStockGroupName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Location = new System.Drawing.Point(22, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 136);
            this.panel1.TabIndex = 0;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(176, 54);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // btnLangDesc
            // 
            this.btnLangDesc.Location = new System.Drawing.Point(377, 87);
            this.btnLangDesc.Name = "btnLangDesc";
            this.btnLangDesc.Size = new System.Drawing.Size(21, 21);
            this.btnLangDesc.TabIndex = 190041;
            this.btnLangDesc.Text = "..";
            this.btnLangDesc.UseVisualStyleBackColor = true;
            this.btnLangDesc.Click += new System.EventHandler(this.btnLangDesc_Click);
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStar2.Location = new System.Drawing.Point(396, 137);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 190022;
            this.lblStar2.Text = "*";
            this.lblStar2.Visible = false;
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStar1.Location = new System.Drawing.Point(396, 21);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 190018;
            this.lblStar1.Text = "*";
            // 
            // txtLanguage
            // 
            this.txtLanguage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLanguage.Location = new System.Drawing.Point(128, 86);
            this.txtLanguage.MaxLength = 25;
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(243, 26);
            this.txtLanguage.TabIndex = 25;
            this.txtLanguage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLanguage_KeyPress);
            // 
            // labellan
            // 
            this.labellan.AutoSize = true;
            this.labellan.Location = new System.Drawing.Point(7, 90);
            this.labellan.Name = "labellan";
            this.labellan.Size = new System.Drawing.Size(61, 13);
            this.labellan.TabIndex = 54;
            this.labellan.Text = "Language :";
            // 
            // lblStar5
            // 
            this.lblStar5.AutoSize = true;
            this.lblStar5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStar5.Location = new System.Drawing.Point(598, 207);
            this.lblStar5.Name = "lblStar5";
            this.lblStar5.Size = new System.Drawing.Size(11, 13);
            this.lblStar5.TabIndex = 190046;
            this.lblStar5.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(106, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(23, 177);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(106, 177);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(189, 177);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(272, 177);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(103, 244);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(63, 244);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(143, 244);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(23, 244);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(355, 177);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 10;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(23, 177);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderColor = System.Drawing.Color.White;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnNewManufacturer);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.lblStar5);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.lblMfgComp);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(456, 286);
            this.pnlMain.TabIndex = 15;
            // 
            // btnNewManufacturer
            // 
            this.btnNewManufacturer.Location = new System.Drawing.Point(571, 202);
            this.btnNewManufacturer.Name = "btnNewManufacturer";
            this.btnNewManufacturer.Size = new System.Drawing.Size(21, 21);
            this.btnNewManufacturer.TabIndex = 190047;
            this.btnNewManufacturer.Text = "..";
            this.btnNewManufacturer.UseVisualStyleBackColor = true;
            this.btnNewManufacturer.Visible = false;
            // 
            // lblMfgComp
            // 
            this.lblMfgComp.AutoSize = true;
            this.lblMfgComp.Location = new System.Drawing.Point(497, 207);
            this.lblMfgComp.Name = "lblMfgComp";
            this.lblMfgComp.Size = new System.Drawing.Size(68, 13);
            this.lblMfgComp.TabIndex = 190044;
            this.lblMfgComp.Text = "Mfg.  Name :";
            this.lblMfgComp.Visible = false;
            // 
            // ItemGroupAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 328);
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemGroupAE";
            this.Text = "Item Group";
            this.Load += new System.EventHandler(this.ItemGroupAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStockGroupName;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.Label labellan;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Button btnLangDesc;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblStar5;
        private System.Windows.Forms.Button btnNewManufacturer;
        private System.Windows.Forms.Label lblMfgComp;
    }
}