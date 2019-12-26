namespace Yadi.Utilities
{
    partial class CashDenomination
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
            this.dgCashDenomination = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCashDenomination)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCashDenomination
            // 
            this.dgCashDenomination.AllowUserToAddRows = false;
            this.dgCashDenomination.AllowUserToDeleteRows = false;
            this.dgCashDenomination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCashDenomination.Location = new System.Drawing.Point(11, 8);
            this.dgCashDenomination.Name = "dgCashDenomination";
            this.dgCashDenomination.Size = new System.Drawing.Size(439, 311);
            this.dgCashDenomination.TabIndex = 0;
            this.dgCashDenomination.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCashDenomination_CellValueChanged);
            this.dgCashDenomination.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgCashDenomination_CellBeginEdit);
            this.dgCashDenomination.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgCashDenomination_CellValidating);
            this.dgCashDenomination.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCashDenomination_CellClick);
            this.dgCashDenomination.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgCashDenomination_KeyDown);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 330);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 60);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(96, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 60);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Clos&e";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblTotal);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.dgCashDenomination);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(460, 410);
            this.pnlMain.TabIndex = 5;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.lblTotal.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(230, 330);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(215, 61);
            this.lblTotal.TabIndex = 5554;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CashDenomination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 446);
            this.Controls.Add(this.pnlMain);
            this.Name = "CashDenomination";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cash Denomination";
            this.Load += new System.EventHandler(this.CashDenomination_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCashDenomination)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCashDenomination;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblTotal;
    }
}