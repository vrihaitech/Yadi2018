namespace Yadi.Settings
{
    partial class DecimalRoundOffSettings
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
            this.dgDecimalRoundOffSettings = new System.Windows.Forms.DataGridView();
            this.srNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decimalDigits = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.roundOffDecimalDigits = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.roundOffType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgDecimalRoundOffSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDecimalRoundOffSettings
            // 
            this.dgDecimalRoundOffSettings.AllowUserToAddRows = false;
            this.dgDecimalRoundOffSettings.AllowUserToDeleteRows = false;
            this.dgDecimalRoundOffSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDecimalRoundOffSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srNo,
            this.decimalDigits,
            this.roundOffDecimalDigits,
            this.roundOffType});
            this.dgDecimalRoundOffSettings.Location = new System.Drawing.Point(14, 12);
            this.dgDecimalRoundOffSettings.Name = "dgDecimalRoundOffSettings";
            this.dgDecimalRoundOffSettings.Size = new System.Drawing.Size(533, 399);
            this.dgDecimalRoundOffSettings.TabIndex = 0;
            // 
            // srNo
            // 
            this.srNo.HeaderText = "Sr. No.";
            this.srNo.Name = "srNo";
            this.srNo.ReadOnly = true;
            this.srNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.srNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // decimalDigits
            // 
            this.decimalDigits.DataPropertyName = "DecimalDigits";
            this.decimalDigits.HeaderText = "Decimal Digits to be Displayed";
            this.decimalDigits.Name = "decimalDigits";
            // 
            // roundOffDecimalDigits
            // 
            this.roundOffDecimalDigits.DataPropertyName = "RoundOffDecimalDigits";
            this.roundOffDecimalDigits.HeaderText = "RoundOff Decimal Digits";
            this.roundOffDecimalDigits.Name = "roundOffDecimalDigits";
            // 
            // roundOffType
            // 
            this.roundOffType.DataPropertyName = "RoundOffType";
            this.roundOffType.HeaderText = "Round Off Type";
            this.roundOffType.Name = "roundOffType";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(355, 418);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(442, 418);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Decimal_RoundOff_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 459);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgDecimalRoundOffSettings);
            this.Name = "Decimal_RoundOff_Settings";
            this.Text = "Decimal_RoundOff_Settings";
            this.Load += new System.EventHandler(this.Decimal_RoundOff_Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDecimalRoundOffSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDecimalRoundOffSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn srNo;
        private System.Windows.Forms.DataGridViewComboBoxColumn decimalDigits;
        private System.Windows.Forms.DataGridViewComboBoxColumn roundOffDecimalDigits;
        private System.Windows.Forms.DataGridViewComboBoxColumn roundOffType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}