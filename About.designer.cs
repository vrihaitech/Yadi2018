namespace Yadi
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.lblMainInfo = new System.Windows.Forms.Label();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.panel1 = new OMControls.OMPanel();
            this.txtAboutInfo = new System.Windows.Forms.TextBox();
            this.omPanel1 = new OMControls.OMPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.omPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMainInfo
            // 
            this.lblMainInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMainInfo.Location = new System.Drawing.Point(250, 66);
            this.lblMainInfo.Name = "lblMainInfo";
            this.lblMainInfo.Size = new System.Drawing.Size(308, 53);
            this.lblMainInfo.TabIndex = 15;
            this.lblMainInfo.Text = "Main Info";
            this.lblMainInfo.Visible = false;
            this.lblMainInfo.Click += new System.EventHandler(this.lblMainInfo_Click);
            // 
            // lblSystemName
            // 
            this.lblSystemName.AutoSize = true;
            this.lblSystemName.BackColor = System.Drawing.Color.Transparent;
            this.lblSystemName.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSystemName.Location = new System.Drawing.Point(3, 18);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(211, 29);
            this.lblSystemName.TabIndex = 0;
            this.lblSystemName.Text = "Yadi Plus System";
            // 
            // panel1
            // 
            this.panel1.BorderRadius = 3;
            this.panel1.Controls.Add(this.lblSystemName);
            this.panel1.CornerRadius = 3;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.GradientBottom = System.Drawing.Color.LightGray;
            this.panel1.GradientMiddle = System.Drawing.Color.White;
            this.panel1.GradientShow = true;
            this.panel1.GradientTop = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 65);
            this.panel1.TabIndex = 16;
            // 
            // txtAboutInfo
            // 
            this.txtAboutInfo.BackColor = System.Drawing.Color.White;
            this.txtAboutInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAboutInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAboutInfo.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAboutInfo.Location = new System.Drawing.Point(0, 66);
            this.txtAboutInfo.Multiline = true;
            this.txtAboutInfo.Name = "txtAboutInfo";
            this.txtAboutInfo.ReadOnly = true;
            this.txtAboutInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAboutInfo.Size = new System.Drawing.Size(570, 255);
            this.txtAboutInfo.TabIndex = 17;
            this.txtAboutInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAboutInfo_KeyPress);
            // 
            // omPanel1
            // 
            this.omPanel1.BorderRadius = 3;
            this.omPanel1.Controls.Add(this.btnClose);
            this.omPanel1.CornerRadius = 3;
            this.omPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.omPanel1.GradientBottom = System.Drawing.Color.LightGray;
            this.omPanel1.GradientMiddle = System.Drawing.Color.White;
            this.omPanel1.GradientShow = true;
            this.omPanel1.GradientTop = System.Drawing.Color.Silver;
            this.omPanel1.Location = new System.Drawing.Point(0, 321);
            this.omPanel1.Name = "omPanel1";
            this.omPanel1.Size = new System.Drawing.Size(570, 76);
            this.omPanel1.TabIndex = 18;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(478, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 60);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 397);
            this.Controls.Add(this.omPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMainInfo);
            this.Controls.Add(this.txtAboutInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.omPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMainInfo;
        private System.Windows.Forms.Label lblSystemName;
        private OMControls.OMPanel panel1;
        private System.Windows.Forms.TextBox txtAboutInfo;
        private OMControls.OMPanel omPanel1;
        private System.Windows.Forms.Button btnClose;

    }
}
