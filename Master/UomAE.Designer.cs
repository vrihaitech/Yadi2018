namespace Yadi.Master
{
    partial class UomAE
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
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlUomType = new System.Windows.Forms.Panel();
            this.lstUomType = new System.Windows.Forms.ListBox();
            this.txtUomType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.lblStar1 = new System.Windows.Forms.Label();
            this.lblStar2 = new System.Windows.Forms.Label();
            this.txtUOMName = new System.Windows.Forms.TextBox();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlUomType.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(99, 267);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(59, 267);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 11;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(139, 267);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 13;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(19, 267);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 10;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlUomType);
            this.panel1.Controls.Add(this.txtUomType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblChkHelp);
            this.panel1.Controls.Add(this.lblStar1);
            this.panel1.Controls.Add(this.lblStar2);
            this.panel1.Controls.Add(this.txtUOMName);
            this.panel1.Controls.Add(this.txtShortName);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(19, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 168);
            this.panel1.TabIndex = 0;
            // 
            // pnlUomType
            // 
            this.pnlUomType.BackColor = System.Drawing.Color.Bisque;
            this.pnlUomType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUomType.Controls.Add(this.lstUomType);
            this.pnlUomType.Location = new System.Drawing.Point(152, 92);
            this.pnlUomType.Name = "pnlUomType";
            this.pnlUomType.Size = new System.Drawing.Size(103, 37);
            this.pnlUomType.TabIndex = 190055;
            this.pnlUomType.Visible = false;
            // 
            // lstUomType
            // 
            this.lstUomType.FormattingEnabled = true;
            this.lstUomType.Location = new System.Drawing.Point(3, 2);
            this.lstUomType.Name = "lstUomType";
            this.lstUomType.Size = new System.Drawing.Size(95, 30);
            this.lstUomType.TabIndex = 3;
            this.lstUomType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstUomType_KeyPress);
            // 
            // txtUomType
            // 
            this.txtUomType.Location = new System.Drawing.Point(151, 71);
            this.txtUomType.Name = "txtUomType";
            this.txtUomType.Size = new System.Drawing.Size(106, 20);
            this.txtUomType.TabIndex = 2;
            this.txtUomType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtuomtype_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 190043;
            this.label4.Text = "UOM Type :";
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(196, 100);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(215, 13);
            this.lblChkHelp.TabIndex = 190042;
            this.lblChkHelp.Text = "( Press SPACE BAR or cilck using MOUSE )";
            // 
            // lblStar1
            // 
            this.lblStar1.AutoSize = true;
            this.lblStar1.Location = new System.Drawing.Point(381, 15);
            this.lblStar1.Name = "lblStar1";
            this.lblStar1.Size = new System.Drawing.Size(11, 13);
            this.lblStar1.TabIndex = 912;
            this.lblStar1.Text = "*";
            // 
            // lblStar2
            // 
            this.lblStar2.AutoSize = true;
            this.lblStar2.Location = new System.Drawing.Point(272, 48);
            this.lblStar2.Name = "lblStar2";
            this.lblStar2.Size = new System.Drawing.Size(11, 13);
            this.lblStar2.TabIndex = 911;
            this.lblStar2.Text = "*";
            // 
            // txtUOMName
            // 
            this.txtUOMName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUOMName.Location = new System.Drawing.Point(151, 11);
            this.txtUOMName.Name = "txtUOMName";
            this.txtUOMName.Size = new System.Drawing.Size(218, 20);
            this.txtUOMName.TabIndex = 0;
            this.txtUOMName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUOMName_KeyPress);
            // 
            // txtShortName
            // 
            this.txtShortName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShortName.Location = new System.Drawing.Point(151, 42);
            this.txtShortName.MaxLength = 5;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(105, 20);
            this.txtShortName.TabIndex = 1;
            this.txtShortName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtshortName_KeyDown);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(151, 100);
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
            this.label3.Location = new System.Drawing.Point(23, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 503;
            this.label3.Text = "Active Status :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 502;
            this.label2.Text = "Short Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 501;
            this.label1.Text = "UOM :";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(19, 201);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(185, 201);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(268, 201);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(351, 201);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 9;
            this.BtnExit.Text = "E&xit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(19, 201);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(102, 201);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Location = new System.Drawing.Point(21, 36);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(450, 307);
            this.pnlMain.TabIndex = 14;
            // 
            // UomAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 368);
            this.Controls.Add(this.pnlMain);
            this.Name = "UomAE";
            this.Text = "Uom Master";
            this.Load += new System.EventHandler(this.UomAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlUomType.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUOMName;
        private System.Windows.Forms.TextBox txtShortName;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblStar1;
        private System.Windows.Forms.Label lblStar2;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Panel pnlUomType;
        private System.Windows.Forms.ListBox lstUomType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUomType;
    }
}