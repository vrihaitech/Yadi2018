namespace Yadi.Settings
{
    partial class StockCountTypeAE
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
            this.btnSave = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlDefault = new OMControls.OMBPanel();
            this.DtpDefaultValue = new System.Windows.Forms.DateTimePicker();
            this.cmbDefault = new System.Windows.Forms.ComboBox();
            this.lblDefault = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CountTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlDefault.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(187, 210);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 60);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(18, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 60);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlDefault
            // 
            this.pnlDefault.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDefault.BorderColor = System.Drawing.Color.Gray;
            this.pnlDefault.BorderRadius = 3;
            this.pnlDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDefault.Controls.Add(this.DtpDefaultValue);
            this.pnlDefault.Controls.Add(this.cmbDefault);
            this.pnlDefault.Controls.Add(this.lblDefault);
            this.pnlDefault.Location = new System.Drawing.Point(139, 84);
            this.pnlDefault.Name = "pnlDefault";
            this.pnlDefault.Size = new System.Drawing.Size(266, 39);
            this.pnlDefault.TabIndex = 190044;
            // 
            // DtpDefaultValue
            // 
            this.DtpDefaultValue.Location = new System.Drawing.Point(122, 6);
            this.DtpDefaultValue.Name = "DtpDefaultValue";
            this.DtpDefaultValue.Size = new System.Drawing.Size(123, 20);
            this.DtpDefaultValue.TabIndex = 1;
            this.DtpDefaultValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDefaultValue_KeyDown);
            // 
            // cmbDefault
            // 
            this.cmbDefault.FormattingEnabled = true;
            this.cmbDefault.Location = new System.Drawing.Point(122, 6);
            this.cmbDefault.Name = "cmbDefault";
            this.cmbDefault.Size = new System.Drawing.Size(123, 21);
            this.cmbDefault.TabIndex = 1;
            this.cmbDefault.SelectedIndexChanged += new System.EventHandler(this.cmbDefault_SelectedIndexChanged);
            this.cmbDefault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDefault_KeyDown);
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(9, 9);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(77, 13);
            this.lblDefault.TabIndex = 190043;
            this.lblDefault.Text = "Default Value :";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(18, 210);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(101, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlDefault);
            this.pnlMain.Controls.Add(this.dgDetails);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(542, 282);
            this.pnlMain.TabIndex = 17;
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.CountType,
            this.Value,
            this.IsActive,
            this.CountTypeNo,
            this.DefaultValue,
            this.Chk});
            this.dgDetails.Location = new System.Drawing.Point(18, 19);
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.Size = new System.Drawing.Size(518, 176);
            this.dgDetails.TabIndex = 10;
            this.dgDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDetails_CellFormatting);
            this.dgDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgDetails_KeyDown);
            this.dgDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellClick);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 60;
            // 
            // CountType
            // 
            this.CountType.HeaderText = "CountType";
            this.CountType.Name = "CountType";
            this.CountType.ReadOnly = true;
            this.CountType.Width = 180;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 180;
            // 
            // IsActive
            // 
            this.IsActive.HeaderText = "IsActive";
            this.IsActive.Name = "IsActive";
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsActive.Width = 60;
            // 
            // CountTypeNo
            // 
            this.CountTypeNo.HeaderText = "CountTypeNo";
            this.CountTypeNo.Name = "CountTypeNo";
            this.CountTypeNo.ReadOnly = true;
            this.CountTypeNo.Visible = false;
            // 
            // DefaultValue
            // 
            this.DefaultValue.HeaderText = "DefaultValue";
            this.DefaultValue.Name = "DefaultValue";
            this.DefaultValue.ReadOnly = true;
            this.DefaultValue.Visible = false;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Chk";
            this.Chk.Name = "Chk";
            this.Chk.ReadOnly = true;
            this.Chk.Visible = false;
            // 
            // StockCountTypeAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 318);
            this.Controls.Add(this.pnlMain);
            this.Name = "StockCountTypeAE";
            this.Text = "Stock Count Type";
            this.Load += new System.EventHandler(this.StockCountTypeAE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlDefault.ResumeLayout(false);
            this.pnlDefault.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblDefault;
        private OMControls.OMBPanel pnlDefault;
        private System.Windows.Forms.ComboBox cmbDefault;
        private System.Windows.Forms.DateTimePicker DtpDefaultValue;
        private System.Windows.Forms.DataGridView dgDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chk;
    }
}