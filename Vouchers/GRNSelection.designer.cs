namespace Yadi.Vouchers
{
    partial class GRNSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgSelection = new System.Windows.Forms.DataGridView();
            this.DcNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblChkHelp);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnOk);
            this.pnlMain.Controls.Add(this.dgSelection);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(385, 259);
            this.pnlMain.TabIndex = 0;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(210, 193);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(110, 13);
            this.lblChkHelp.TabIndex = 19;
            this.lblChkHelp.Text = "Press Escape For Exit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "GRN Details";
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(89, 193);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 60);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(14, 193);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(69, 60);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgSelection
            // 
            this.dgSelection.AllowUserToAddRows = false;
            this.dgSelection.AllowUserToDeleteRows = false;
            this.dgSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DcNo,
            this.DocNo,
            this.RefNo,
            this.ChDate,
            this.Chk});
            this.dgSelection.Location = new System.Drawing.Point(14, 36);
            this.dgSelection.Name = "dgSelection";
            this.dgSelection.Size = new System.Drawing.Size(345, 150);
            this.dgSelection.TabIndex = 0;
            this.dgSelection.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSelection_CellFormatting);
            this.dgSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgSelection_KeyDown);
            // 
            // DcNo
            // 
            this.DcNo.DataPropertyName = "ChallanNo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DcNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.DcNo.HeaderText = "DCNo";
            this.DcNo.Name = "DcNo";
            this.DcNo.ReadOnly = true;
            this.DcNo.Visible = false;
            // 
            // DocNo
            // 
            this.DocNo.DataPropertyName = "ChallanUserNo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DocNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.DocNo.HeaderText = "DocNo";
            this.DocNo.Name = "DocNo";
            this.DocNo.ReadOnly = true;
            this.DocNo.Width = 70;
            // 
            // RefNo
            // 
            this.RefNo.DataPropertyName = "RefNo";
            this.RefNo.HeaderText = "RefNo";
            this.RefNo.Name = "RefNo";
            this.RefNo.ReadOnly = true;
            // 
            // ChDate
            // 
            this.ChDate.DataPropertyName = "ChallanDate";
            this.ChDate.HeaderText = "Date";
            this.ChDate.Name = "ChDate";
            this.ChDate.ReadOnly = true;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk.Width = 50;
            // 
            // GRNSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 284);
            this.Controls.Add(this.pnlMain);
            this.Name = "GRNSelection";
            this.Text = "GRN Selection";
            this.Load += new System.EventHandler(this.DCSelection_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView dgSelection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
    }
}