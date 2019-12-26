namespace Yadi.Utilities
{
    partial class PrintCount
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
            this.pnlMain = new OMControls.OMBPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrintCount = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.txtPrintCount);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(12, 11);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(225, 79);
            this.pnlMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Print Count :";
            // 
            // txtPrintCount
            // 
            this.txtPrintCount.Location = new System.Drawing.Point(99, 17);
            this.txtPrintCount.Name = "txtPrintCount";
            this.txtPrintCount.Size = new System.Drawing.Size(100, 20);
            this.txtPrintCount.TabIndex = 1;
            this.txtPrintCount.TextChanged += new System.EventHandler(this.txtPrintCount_TextChanged);
            this.txtPrintCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrintCount_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(99, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PrintCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 102);
            this.Controls.Add(this.pnlMain);
            this.Name = "PrintCount";
            this.Text = "PrintCount";
            this.Load += new System.EventHandler(this.PrintCount_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPrintCount;
        private System.Windows.Forms.Label label1;
    }
}