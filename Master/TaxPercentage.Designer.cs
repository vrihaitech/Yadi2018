namespace Yadi.Master
{
    partial class TaxPercentage
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
            this.PnlPercentage = new System.Windows.Forms.Panel();
            this.BtnPerCancel = new System.Windows.Forms.Button();
            this.btnPecOk = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.PnlPercentage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlPercentage
            // 
            this.PnlPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlPercentage.Controls.Add(this.BtnPerCancel);
            this.PnlPercentage.Controls.Add(this.btnPecOk);
            this.PnlPercentage.Controls.Add(this.label31);
            this.PnlPercentage.Controls.Add(this.label32);
            this.PnlPercentage.Controls.Add(this.txtPercentage);
            this.PnlPercentage.Location = new System.Drawing.Point(14, 13);
            this.PnlPercentage.Name = "PnlPercentage";
            this.PnlPercentage.Size = new System.Drawing.Size(241, 108);
            this.PnlPercentage.TabIndex = 0;
            // 
            // BtnPerCancel
            // 
            this.BtnPerCancel.Location = new System.Drawing.Point(130, 54);
            this.BtnPerCancel.Name = "BtnPerCancel";
            this.BtnPerCancel.Size = new System.Drawing.Size(73, 24);
            this.BtnPerCancel.TabIndex = 2;
            this.BtnPerCancel.Text = "Cancel";
            this.BtnPerCancel.UseVisualStyleBackColor = true;
            this.BtnPerCancel.Click += new System.EventHandler(this.BtnPerCancel_Click);
            // 
            // btnPecOk
            // 
            this.btnPecOk.Location = new System.Drawing.Point(29, 54);
            this.btnPecOk.Name = "btnPecOk";
            this.btnPecOk.Size = new System.Drawing.Size(62, 24);
            this.btnPecOk.TabIndex = 1;
            this.btnPecOk.Text = "OK";
            this.btnPecOk.UseVisualStyleBackColor = true;
            this.btnPecOk.Click += new System.EventHandler(this.btnPecOk_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(191, 17);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 13);
            this.label31.TabIndex = 59;
            this.label31.Text = "%";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(27, 17);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(68, 13);
            this.label32.TabIndex = 58;
            this.label32.Text = "Percentage :";
            // 
            // txtPercentage
            // 
            this.txtPercentage.Location = new System.Drawing.Point(130, 14);
            this.txtPercentage.MaxLength = 5;
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(55, 20);
            this.txtPercentage.TabIndex = 0;
            this.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPercentage.TextChanged += new System.EventHandler(this.txtPercentage_TextChanged);
            this.txtPercentage.Leave += new System.EventHandler(this.txtPercentage_Leave);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // TaxPercentage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 144);
            this.Controls.Add(this.PnlPercentage);
            this.Name = "TaxPercentage";
            this.Text = "TaxPercentage";
            this.Load += new System.EventHandler(this.TaxPercentage_Load);
            this.PnlPercentage.ResumeLayout(false);
            this.PnlPercentage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlPercentage;
        private System.Windows.Forms.Button BtnPerCancel;
        private System.Windows.Forms.Button btnPecOk;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtPercentage;
        private System.Windows.Forms.ErrorProvider EP;
    }
}