namespace Yadi.Master
{
    partial class GodownAE
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtGodownName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnllocation = new System.Windows.Forms.Panel();
            this.lstlocation = new System.Windows.Forms.ListBox();
            this.pnlcontrolgroup = new System.Windows.Forms.Panel();
            this.lstcontrolgroup = new System.Windows.Forms.ListBox();
            this.txtcontrolgroup = new System.Windows.Forms.TextBox();
            this.txtlocation = new System.Windows.Forms.TextBox();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblStar3 = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnllocation.SuspendLayout();
            this.pnlcontrolgroup.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Name :";
            // 
            // txtGodownName
            // 
            this.txtGodownName.Location = new System.Drawing.Point(161, 9);
            this.txtGodownName.MaxLength = 25;
            this.txtGodownName.Name = "txtGodownName";
            this.txtGodownName.Size = new System.Drawing.Size(217, 20);
            this.txtGodownName.TabIndex = 0;
            this.txtGodownName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGodownName_KeyDown);
            this.txtGodownName.Leave += new System.EventHandler(this.txtGodownName_Leave_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Active Status :";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(160, 118);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(44, 17);
            this.chkActive.TabIndex = 3;
            this.chkActive.Text = "Yes";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            this.chkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkActive_KeyDown);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(99, 324);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(59, 324);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(139, 324);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(19, 324);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(351, 258);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 10;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(19, 258);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "Location :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnllocation);
            this.panel1.Controls.Add(this.pnlcontrolgroup);
            this.panel1.Controls.Add(this.txtcontrolgroup);
            this.panel1.Controls.Add(this.txtlocation);
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.lblStar3);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtGodownName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Location = new System.Drawing.Point(20, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 226);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // pnllocation
            // 
            this.pnllocation.BackColor = System.Drawing.Color.Bisque;
            this.pnllocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnllocation.Controls.Add(this.lstlocation);
            this.pnllocation.Location = new System.Drawing.Point(161, 53);
            this.pnllocation.Name = "pnllocation";
            this.pnllocation.Size = new System.Drawing.Size(217, 25);
            this.pnllocation.TabIndex = 16000034;
            this.pnllocation.Visible = false;
            // 
            // lstlocation
            // 
            this.lstlocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstlocation.FormattingEnabled = true;
            this.lstlocation.Location = new System.Drawing.Point(4, 3);
            this.lstlocation.Name = "lstlocation";
            this.lstlocation.Size = new System.Drawing.Size(208, 17);
            this.lstlocation.Sorted = true;
            this.lstlocation.TabIndex = 516;
            this.lstlocation.Leave += new System.EventHandler(this.lstlocation_Leave);
            this.lstlocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstlocation_KeyDown);
            // 
            // pnlcontrolgroup
            // 
            this.pnlcontrolgroup.BackColor = System.Drawing.Color.Bisque;
            this.pnlcontrolgroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlcontrolgroup.Controls.Add(this.lstcontrolgroup);
            this.pnlcontrolgroup.Location = new System.Drawing.Point(161, 92);
            this.pnlcontrolgroup.Name = "pnlcontrolgroup";
            this.pnlcontrolgroup.Size = new System.Drawing.Size(218, 24);
            this.pnlcontrolgroup.TabIndex = 16000035;
            this.pnlcontrolgroup.Visible = false;
            // 
            // lstcontrolgroup
            // 
            this.lstcontrolgroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstcontrolgroup.FormattingEnabled = true;
            this.lstcontrolgroup.Location = new System.Drawing.Point(6, 3);
            this.lstcontrolgroup.Name = "lstcontrolgroup";
            this.lstcontrolgroup.Size = new System.Drawing.Size(206, 17);
            this.lstcontrolgroup.Sorted = true;
            this.lstcontrolgroup.TabIndex = 516;
            this.lstcontrolgroup.Leave += new System.EventHandler(this.lstcontrolgroup_Leave);
            this.lstcontrolgroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstcontolgroup_KeyDown);
            // 
            // txtcontrolgroup
            // 
            this.txtcontrolgroup.Location = new System.Drawing.Point(160, 83);
            this.txtcontrolgroup.MaxLength = 25;
            this.txtcontrolgroup.Name = "txtcontrolgroup";
            this.txtcontrolgroup.Size = new System.Drawing.Size(217, 20);
            this.txtcontrolgroup.TabIndex = 2;
            this.txtcontrolgroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcontrolgroup_KeyPress);
            // 
            // txtlocation
            // 
            this.txtlocation.Location = new System.Drawing.Point(161, 42);
            this.txtlocation.MaxLength = 25;
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.Size = new System.Drawing.Size(217, 20);
            this.txtlocation.TabIndex = 1;
            this.txtlocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlocation_KeyPress);
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(199, 119);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // lblStar3
            // 
            this.lblStar3.AutoSize = true;
            this.lblStar3.Location = new System.Drawing.Point(384, 53);
            this.lblStar3.Name = "lblStar3";
            this.lblStar3.Size = new System.Drawing.Size(11, 13);
            this.lblStar3.TabIndex = 58;
            this.lblStar3.Text = "*";
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.Location = new System.Drawing.Point(383, 13);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 56;
            this.lblStar1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "ControlGroup :";
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.Location = new System.Drawing.Point(310, 326);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 57;
            this.lblStar2.Text = "*";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(268, 258);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(185, 258);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(102, 258);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 258);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(19, 258);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 5;
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
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(455, 372);
            this.pnlMain.TabIndex = 15;
            // 
            // GodownAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 421);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblStar2);
            this.Name = "GodownAE";
            this.Text = "Godown Master";
            this.Load += new System.EventHandler(this.GodownAE_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GodownAE_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnllocation.ResumeLayout(false);
            this.pnlcontrolgroup.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGodownName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label2;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar3;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.TextBox txtlocation;
        private System.Windows.Forms.Panel pnllocation;
        private System.Windows.Forms.ListBox lstlocation;
        private System.Windows.Forms.Panel pnlcontrolgroup;
        private System.Windows.Forms.ListBox lstcontrolgroup;
        private System.Windows.Forms.TextBox txtcontrolgroup;
    }
}