namespace Yadi.Master
{
    partial class RackMasterAE
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
            this.btnExit = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.lblStar6 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.txtRackName = new System.Windows.Forms.TextBox();
            this.txtRackNo = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRackNo = new System.Windows.Forms.Label();
            this.lblRackName = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(352, 186);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(186, 186);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(269, 186);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(103, 186);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(103, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(20, 186);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(20, 186);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.lblStar6);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.txtRackName);
            this.panel1.Controls.Add(this.txtRackNo);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblRackNo);
            this.panel1.Controls.Add(this.lblRackName);
            this.panel1.Location = new System.Drawing.Point(20, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 156);
            this.panel1.TabIndex = 0;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(166, 88);
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
            this.lblStar2.Location = new System.Drawing.Point(223, 56);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 911;
            this.lblStar2.Text = "*";
            this.lblStar2.Visible = false;
            // 
            // txtRackName
            // 
            this.txtRackName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRackName.Location = new System.Drawing.Point(122, 24);
            this.txtRackName.Name = "txtRackName";
            this.txtRackName.Size = new System.Drawing.Size(223, 20);
            this.txtRackName.TabIndex = 1;
            this.txtRackName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRackName_KeyDown);
            this.txtRackName.Leave += new System.EventHandler(this.txtRackName_Leave);
            // 
            // txtRackNo
            // 
            this.txtRackNo.Location = new System.Drawing.Point(122, 54);
            this.txtRackNo.MaxLength = 5;
            this.txtRackNo.Name = "txtRackNo";
            this.txtRackNo.Size = new System.Drawing.Size(89, 20);
            this.txtRackNo.TabIndex = 2;
            this.txtRackNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRackNo_KeyDown);
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
            // lblRackNo
            // 
            this.lblRackNo.AutoSize = true;
            this.lblRackNo.Location = new System.Drawing.Point(15, 56);
            this.lblRackNo.Name = "lblRackNo";
            this.lblRackNo.Size = new System.Drawing.Size(56, 13);
            this.lblRackNo.TabIndex = 502;
            this.lblRackNo.Text = "Rack No :";
            // 
            // lblRackName
            // 
            this.lblRackName.AutoSize = true;
            this.lblRackName.Location = new System.Drawing.Point(15, 28);
            this.lblRackName.Name = "lblRackName";
            this.lblRackName.Size = new System.Drawing.Size(41, 13);
            this.lblRackName.TabIndex = 501;
            this.lblRackName.Text = "Name :";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(100, 252);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(60, 252);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(140, 252);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(20, 252);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Location = new System.Drawing.Point(17, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(452, 292);
            this.pnlMain.TabIndex = 18;
            // 
            // RackMasterAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 335);
            this.Controls.Add(this.pnlMain);
            this.Name = "RackMasterAE";
            this.Text = "Rack Master";
            this.Activated += new System.EventHandler(this.RackMasterAE_Activated);
            this.Deactivate += new System.EventHandler(this.RackMasterAE_Deactivate);
            this.Load += new System.EventHandler(this.RackMasterAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRackName;
        private System.Windows.Forms.TextBox txtRackNo;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRackNo;
        private System.Windows.Forms.Label lblRackName;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar6;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblChkHelp;
    }
}