namespace Yadi.Utilities
{
    partial class SchemeActivation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.pnlMain = new OMControls.OMBPanel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeriodFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeriodTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.SchemeType,
            this.SchemeName,
            this.PeriodFrom,
            this.PeriodTo,
            this.SchemeNo,
            this.IsActive});
            this.GridView.Location = new System.Drawing.Point(15, 15);
            this.GridView.Name = "GridView";
            this.GridView.Size = new System.Drawing.Size(687, 150);
            this.GridView.TabIndex = 0;
            this.GridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridView_CellFormatting);
            this.GridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridView_KeyDown);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.GridView);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(718, 241);
            this.pnlMain.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(15, 171);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 19;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(101, 171);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 50;
            // 
            // SchemeType
            // 
            this.SchemeType.DataPropertyName = "SchemeTypeName";
            this.SchemeType.HeaderText = "Scheme Type";
            this.SchemeType.Name = "SchemeType";
            this.SchemeType.ReadOnly = true;
            this.SchemeType.Width = 200;
            // 
            // SchemeName
            // 
            this.SchemeName.DataPropertyName = "SchemeName";
            this.SchemeName.HeaderText = "Scheme Name";
            this.SchemeName.Name = "SchemeName";
            this.SchemeName.ReadOnly = true;
            this.SchemeName.Width = 175;
            // 
            // PeriodFrom
            // 
            this.PeriodFrom.DataPropertyName = "SchemePeriodFrom";
            this.PeriodFrom.HeaderText = "Period From";
            this.PeriodFrom.Name = "PeriodFrom";
            this.PeriodFrom.ReadOnly = true;
            this.PeriodFrom.Width = 90;
            // 
            // PeriodTo
            // 
            this.PeriodTo.DataPropertyName = "SchemePeriodTo";
            this.PeriodTo.HeaderText = "Period To";
            this.PeriodTo.Name = "PeriodTo";
            this.PeriodTo.ReadOnly = true;
            this.PeriodTo.Width = 85;
            // 
            // SchemeNo
            // 
            this.SchemeNo.DataPropertyName = "SchemeNo";
            this.SchemeNo.HeaderText = "SchemeNo";
            this.SchemeNo.Name = "SchemeNo";
            this.SchemeNo.Visible = false;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "Select";
            this.IsActive.Name = "IsActive";
            this.IsActive.Width = 70;
            // 
            // SchemeActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 275);
            this.Controls.Add(this.pnlMain);
            this.Name = "SchemeActivation";
            this.Text = "Scheme Activation";
            this.Load += new System.EventHandler(this.SchemeActivation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridView;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
    }
}