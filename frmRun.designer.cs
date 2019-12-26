namespace Yadi
{
    partial class frmRun
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRun));
            this.PB = new OMControls.OMProgressBar();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.pnlMain = new OMControls.OMPanel();
            this.lblTradeMark = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PB
            // 
            this.PB.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PB.EndColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.ForeColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.HighlightColor = System.Drawing.Color.NavajoWhite;
            this.PB.Location = new System.Drawing.Point(0, 125);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(562, 22);
            this.PB.StartColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.PB.TabIndex = 49;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.ForeColor = System.Drawing.Color.Black;
            this.labelProductName.Location = new System.Drawing.Point(8, 52);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(35, 13);
            this.labelProductName.TabIndex = 44;
            this.labelProductName.Text = "label2";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompanyName.ForeColor = System.Drawing.Color.Black;
            this.labelCompanyName.Location = new System.Drawing.Point(9, 97);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(35, 13);
            this.labelCompanyName.TabIndex = 47;
            this.labelCompanyName.Text = "label2";
            // 
            // pnlMain
            // 
            this.pnlMain.BorderRadius = 1;
            this.pnlMain.Controls.Add(this.PB);
            this.pnlMain.Controls.Add(this.labelProductName);
            this.pnlMain.Controls.Add(this.lblTradeMark);
            this.pnlMain.Controls.Add(this.labelCompanyName);
            this.pnlMain.Controls.Add(this.labelVersion);
            this.pnlMain.Controls.Add(this.labelCopyright);
            this.pnlMain.CornerRadius = 1;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.GradientBottom = System.Drawing.Color.FromArgb(255,93,173,226 );
            this.pnlMain.GradientMiddle = System.Drawing.Color.PapayaWhip;
            this.pnlMain.GradientShow = true;
            this.pnlMain.GradientTop = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(562, 147);
            this.pnlMain.TabIndex = 50;
            // 
            // lblTradeMark
            // 
            this.lblTradeMark.AutoSize = true;
            this.lblTradeMark.BackColor = System.Drawing.Color.Transparent;
            this.lblTradeMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTradeMark.ForeColor = System.Drawing.Color.Black;
            this.lblTradeMark.Location = new System.Drawing.Point(401, 83);
            this.lblTradeMark.Name = "lblTradeMark";
            this.lblTradeMark.Size = new System.Drawing.Size(35, 13);
            this.lblTradeMark.TabIndex = 48;
            this.lblTradeMark.Text = "label2";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Black;
            this.labelVersion.Location = new System.Drawing.Point(8, 68);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(35, 13);
            this.labelVersion.TabIndex = 45;
            this.labelVersion.Text = "label2";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.Black;
            this.labelCopyright.Location = new System.Drawing.Point(8, 83);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(35, 13);
            this.labelCopyright.TabIndex = 46;
            this.labelCopyright.Text = "label2";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(562, 147);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRun";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmRun_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal OMControls.OMProgressBar PB;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCompanyName;
        private OMControls.OMPanel pnlMain;
        private System.Windows.Forms.Label lblTradeMark;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Timer timer1;



    }
}
