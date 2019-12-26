namespace Yadi.Utilities
{
    partial class RateVariationChange
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtHigher = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLower = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnApplyChange = new System.Windows.Forms.Button();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LowerVariation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HigherVariation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbBrand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnApply);
            this.pnlMain.Controls.Add(this.txtHigher);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtLower);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.btnApplyChange);
            this.pnlMain.Controls.Add(this.dgDetails);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(574, 445);
            this.pnlMain.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(479, 359);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(86, 42);
            this.btnApply.TabIndex = 520;
            this.btnApply.Text = "Apply (F5)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtHigher
            // 
            this.txtHigher.Location = new System.Drawing.Point(408, 363);
            this.txtHigher.Name = "txtHigher";
            this.txtHigher.Size = new System.Drawing.Size(63, 20);
            this.txtHigher.TabIndex = 519;
            this.txtHigher.TextChanged += new System.EventHandler(this.txtPer_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 518;
            this.label4.Text = "Higher (F4) :";
            // 
            // txtLower
            // 
            this.txtLower.Location = new System.Drawing.Point(255, 363);
            this.txtLower.Name = "txtLower";
            this.txtLower.Size = new System.Drawing.Size(63, 20);
            this.txtLower.TabIndex = 517;
            this.txtLower.TextChanged += new System.EventHandler(this.txtPer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 516;
            this.label1.Text = "Lower (F3) :";
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(41, 200);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 515;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(574, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rate Veriation Change";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(274, 400);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(104, 38);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(164, 400);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 38);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear (F6)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApplyChange
            // 
            this.btnApplyChange.Location = new System.Drawing.Point(20, 400);
            this.btnApplyChange.Name = "btnApplyChange";
            this.btnApplyChange.Size = new System.Drawing.Size(138, 38);
            this.btnApplyChange.TabIndex = 2;
            this.btnApplyChange.Text = "Apply Change (F2)";
            this.btnApplyChange.UseVisualStyleBackColor = true;
            this.btnApplyChange.Click += new System.EventHandler(this.btnApplyChange_Click);
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.ItemName,
            this.LowerVariation,
            this.HigherVariation,
            this.LV,
            this.HV,
            this.ItemNo,
            this.IsChange});
            this.dgDetails.Location = new System.Drawing.Point(15, 80);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.Size = new System.Drawing.Size(549, 272);
            this.dgDetails.TabIndex = 1;
            this.dgDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDetails_CellFormatting);
            this.dgDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellEndEdit);
            this.dgDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgDetails_EditingControlShowing);
            this.dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgDetails_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 60;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 230;
            // 
            // LowerVariation
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LowerVariation.DefaultCellStyle = dataGridViewCellStyle5;
            this.LowerVariation.HeaderText = "Lower";
            this.LowerVariation.Name = "LowerVariation";
            // 
            // HigherVariation
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.HigherVariation.DefaultCellStyle = dataGridViewCellStyle6;
            this.HigherVariation.HeaderText = "Higher";
            this.HigherVariation.Name = "HigherVariation";
            // 
            // LV
            // 
            this.LV.HeaderText = "LV";
            this.LV.Name = "LV";
            this.LV.Visible = false;
            // 
            // HV
            // 
            this.HV.HeaderText = "HV";
            this.HV.Name = "HV";
            this.HV.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // IsChange
            // 
            this.IsChange.HeaderText = "IsChange";
            this.IsChange.Name = "IsChange";
            this.IsChange.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbBrand);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(8, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 35);
            this.panel1.TabIndex = 0;
            // 
            // cmbBrand
            // 
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.Location = new System.Drawing.Point(96, 8);
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(460, 21);
            this.cmbBrand.TabIndex = 3;
            this.cmbBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBrand_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Brand :";
            // 
            // RateVariationChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 465);
            this.Controls.Add(this.pnlMain);
            this.Name = "RateVariationChange";
            this.Text = "PartyWiseRateChange";
            this.Load += new System.EventHandler(this.RateVariationChange_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbBrand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgDetails;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnApplyChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtHigher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LowerVariation;
        private System.Windows.Forms.DataGridViewTextBoxColumn HigherVariation;
        private System.Windows.Forms.DataGridViewTextBoxColumn LV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsChange;
    }
}