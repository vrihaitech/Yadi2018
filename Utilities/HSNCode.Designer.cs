namespace Yadi.Utilities
{
    partial class HSNCode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelShow = new System.Windows.Forms.Panel();
            this.pnlItemName = new System.Windows.Forms.Panel();
            this.lstItemName = new System.Windows.Forms.ListBox();
            this.panelBtn = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.GvInfo = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HSNCode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnOkGetdata = new System.Windows.Forms.Button();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.txtHSN_Code = new System.Windows.Forms.TextBox();
            this.rdHSNCode = new System.Windows.Forms.RadioButton();
            this.txtpkItemNo = new System.Windows.Forms.TextBox();
            this.rdNoHSNCode = new System.Windows.Forms.RadioButton();
            this.cachedItemWiseVatRegister1 = new Yadi.Reports.CachedItemWiseVatRegister();
            this.panelShow.SuspendLayout();
            this.pnlItemName.SuspendLayout();
            this.panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvInfo)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelShow
            // 
            this.panelShow.BackColor = System.Drawing.SystemColors.Control;
            this.panelShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelShow.Controls.Add(this.pnlItemName);
            this.panelShow.Controls.Add(this.panelBtn);
            this.panelShow.Controls.Add(this.GvInfo);
            this.panelShow.Location = new System.Drawing.Point(127, 115);
            this.panelShow.Name = "panelShow";
            this.panelShow.Size = new System.Drawing.Size(921, 449);
            this.panelShow.TabIndex = 0;
            this.panelShow.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnlItemName
            // 
            this.pnlItemName.BackColor = System.Drawing.Color.Bisque;
            this.pnlItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItemName.Controls.Add(this.lstItemName);
            this.pnlItemName.Location = new System.Drawing.Point(368, -1);
            this.pnlItemName.Name = "pnlItemName";
            this.pnlItemName.Size = new System.Drawing.Size(407, 270);
            this.pnlItemName.TabIndex = 123522;
            this.pnlItemName.Visible = false;
            // 
            // lstItemName
            // 
            this.lstItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstItemName.FormattingEnabled = true;
            this.lstItemName.Location = new System.Drawing.Point(8, 7);
            this.lstItemName.Name = "lstItemName";
            this.lstItemName.Size = new System.Drawing.Size(394, 249);
            this.lstItemName.TabIndex = 516;
            this.lstItemName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstItemName_MouseClick);
            this.lstItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItemName_KeyDown);
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnUpdate);
            this.panelBtn.Controls.Add(this.btnCancel);
            this.panelBtn.Controls.Add(this.btnClose);
            this.panelBtn.Controls.Add(this.btnOK);
            this.panelBtn.Location = new System.Drawing.Point(3, 361);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(907, 81);
            this.panelBtn.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(15, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(187, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 524;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(273, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 60);
            this.btnClose.TabIndex = 525;
            this.btnClose.Text = "E&xit";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(101, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 60);
            this.btnOK.TabIndex = 522;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // GvInfo
            // 
            this.GvInfo.AllowUserToAddRows = false;
            this.GvInfo.AllowUserToDeleteRows = false;
            this.GvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.GroupName,
            this.ItemName,
            this.HSNCode1,
            this.GroupNo,
            this.ItemNo});
            this.GvInfo.Location = new System.Drawing.Point(18, 10);
            this.GvInfo.Name = "GvInfo";
            this.GvInfo.Size = new System.Drawing.Size(892, 345);
            this.GvInfo.TabIndex = 518;
            this.GvInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvInfo_CellEndEdit);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // GroupName
            // 
            this.GroupName.HeaderText = "GroupName";
            this.GroupName.Name = "GroupName";
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 360;
            // 
            // HSNCode1
            // 
            this.HSNCode1.HeaderText = "HSNCode";
            this.HSNCode1.Name = "HSNCode1";
            this.HSNCode1.Width = 150;
            // 
            // GroupNo
            // 
            this.GroupNo.HeaderText = "GroupNo";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.BtnOkGetdata);
            this.panel4.Controls.Add(this.txtItemName);
            this.panel4.Controls.Add(this.rdAll);
            this.panel4.Controls.Add(this.txtHSN_Code);
            this.panel4.Controls.Add(this.rdHSNCode);
            this.panel4.Controls.Add(this.txtpkItemNo);
            this.panel4.Controls.Add(this.rdNoHSNCode);
            this.panel4.Location = new System.Drawing.Point(127, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(930, 50);
            this.panel4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(273, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 519;
            this.label2.Text = "Item Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(603, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 521;
            this.label4.Text = "HSN Code :";
            // 
            // BtnOkGetdata
            // 
            this.BtnOkGetdata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnOkGetdata.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOkGetdata.Location = new System.Drawing.Point(874, 3);
            this.BtnOkGetdata.Name = "BtnOkGetdata";
            this.BtnOkGetdata.Size = new System.Drawing.Size(51, 38);
            this.BtnOkGetdata.TabIndex = 526;
            this.BtnOkGetdata.Text = "Ok";
            this.BtnOkGetdata.UseVisualStyleBackColor = true;
            this.BtnOkGetdata.Click += new System.EventHandler(this.BtnOkGetdata_Click);
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.White;
            this.txtItemName.Location = new System.Drawing.Point(368, 13);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(158, 20);
            this.txtItemName.TabIndex = 123520;
            this.txtItemName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstItemName_MouseClick);
            this.txtItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemName_KeyPress);
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Checked = true;
            this.rdAll.Enabled = false;
            this.rdAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAll.Location = new System.Drawing.Point(210, 14);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(39, 17);
            this.rdAll.TabIndex = 121;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.rdAll_CheckedChanged);
            this.rdAll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdAll_KeyPress);
            // 
            // txtHSN_Code
            // 
            this.txtHSN_Code.Location = new System.Drawing.Point(693, 18);
            this.txtHSN_Code.Name = "txtHSN_Code";
            this.txtHSN_Code.Size = new System.Drawing.Size(161, 20);
            this.txtHSN_Code.TabIndex = 123523;
            this.txtHSN_Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHSN_Code_KeyPress);
            // 
            // rdHSNCode
            // 
            this.rdHSNCode.AutoSize = true;
            this.rdHSNCode.Enabled = false;
            this.rdHSNCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdHSNCode.Location = new System.Drawing.Point(125, 14);
            this.rdHSNCode.Name = "rdHSNCode";
            this.rdHSNCode.Size = new System.Drawing.Size(80, 17);
            this.rdHSNCode.TabIndex = 120;
            this.rdHSNCode.Text = "HSNCode";
            this.rdHSNCode.UseVisualStyleBackColor = true;
            this.rdHSNCode.CheckedChanged += new System.EventHandler(this.rdHSNCode_CheckedChanged);
            this.rdHSNCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdHSNCode_KeyPress);
            // 
            // txtpkItemNo
            // 
            this.txtpkItemNo.Location = new System.Drawing.Point(532, 15);
            this.txtpkItemNo.Name = "txtpkItemNo";
            this.txtpkItemNo.Size = new System.Drawing.Size(49, 20);
            this.txtpkItemNo.TabIndex = 123522;
            // 
            // rdNoHSNCode
            // 
            this.rdNoHSNCode.AutoSize = true;
            this.rdNoHSNCode.Enabled = false;
            this.rdNoHSNCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNoHSNCode.Location = new System.Drawing.Point(11, 14);
            this.rdNoHSNCode.Name = "rdNoHSNCode";
            this.rdNoHSNCode.Size = new System.Drawing.Size(107, 17);
            this.rdNoHSNCode.TabIndex = 119;
            this.rdNoHSNCode.Text = "Non HSNCode";
            this.rdNoHSNCode.UseVisualStyleBackColor = true;
            this.rdNoHSNCode.CheckedChanged += new System.EventHandler(this.rdNoHSNCode_CheckedChanged);
            this.rdNoHSNCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdNoHSNCode_KeyPress);
            // 
            // HSNCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 750);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelShow);
            this.Name = "HSNCode";
            this.Text = "HSNCode";
            this.Load += new System.EventHandler(this.HSNCode_Load);
            this.panelShow.ResumeLayout(false);
            this.pnlItemName.ResumeLayout(false);
            this.panelBtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvInfo)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelShow;
        private Reports.CachedItemWiseVatRegister cachedItemWiseVatRegister1;
        private System.Windows.Forms.Panel pnlItemName;
        private System.Windows.Forms.ListBox lstItemName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdHSNCode;
        private System.Windows.Forms.RadioButton rdNoHSNCode;
        private System.Windows.Forms.Button BtnOkGetdata;
        private System.Windows.Forms.Panel panelBtn;
        private System.Windows.Forms.DataGridView GvInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtHSN_Code;
        private System.Windows.Forms.TextBox txtpkItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HSNCode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
    }
}