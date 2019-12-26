namespace Yadi.Utilities
{
    partial class AdvanceSetting
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
            this.pnlmain = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAdelete = new System.Windows.Forms.Button();
            this.btnIdelete = new System.Windows.Forms.Button();
            this.btnLdelete = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPentryDelete = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSentrydelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEstock = new System.Windows.Forms.Button();
            this.btnSstock = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.pnlmain.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlmain
            // 
            this.pnlmain.Controls.Add(this.btnexit);
            this.pnlmain.Controls.Add(this.groupBox4);
            this.pnlmain.Controls.Add(this.groupBox3);
            this.pnlmain.Controls.Add(this.groupBox2);
            this.pnlmain.Controls.Add(this.groupBox1);
            this.pnlmain.Location = new System.Drawing.Point(12, 12);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(927, 491);
            this.pnlmain.TabIndex = 0;
            this.pnlmain.Visible = false;
            this.pnlmain.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.btnAdelete);
            this.groupBox4.Controls.Add(this.btnIdelete);
            this.groupBox4.Controls.Add(this.btnLdelete);
            this.groupBox4.Location = new System.Drawing.Point(21, 221);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 152);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Master";
            // 
            // btnAdelete
            // 
            this.btnAdelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdelete.Location = new System.Drawing.Point(23, 87);
            this.btnAdelete.Name = "btnAdelete";
            this.btnAdelete.Size = new System.Drawing.Size(112, 45);
            this.btnAdelete.TabIndex = 16;
            this.btnAdelete.Text = "All Master Delete";
            this.btnAdelete.UseVisualStyleBackColor = false;
            this.btnAdelete.Click += new System.EventHandler(this.btnAdelete_Click);
            // 
            // btnIdelete
            // 
            this.btnIdelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnIdelete.Location = new System.Drawing.Point(173, 19);
            this.btnIdelete.Name = "btnIdelete";
            this.btnIdelete.Size = new System.Drawing.Size(112, 45);
            this.btnIdelete.TabIndex = 15;
            this.btnIdelete.Text = "Only ItemMaster Delete";
            this.btnIdelete.UseVisualStyleBackColor = false;
            this.btnIdelete.Click += new System.EventHandler(this.btnIdelete_Click);
            // 
            // btnLdelete
            // 
            this.btnLdelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnLdelete.Location = new System.Drawing.Point(23, 19);
            this.btnLdelete.Name = "btnLdelete";
            this.btnLdelete.Size = new System.Drawing.Size(112, 45);
            this.btnLdelete.TabIndex = 14;
            this.btnLdelete.Text = "Only Ledger Delete";
            this.btnLdelete.UseVisualStyleBackColor = false;
            this.btnLdelete.Click += new System.EventHandler(this.btnLdelete_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btnPentryDelete);
            this.groupBox3.Location = new System.Drawing.Point(484, 221);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 152);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Purchase";
            // 
            // btnPentryDelete
            // 
            this.btnPentryDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnPentryDelete.Location = new System.Drawing.Point(36, 46);
            this.btnPentryDelete.Name = "btnPentryDelete";
            this.btnPentryDelete.Size = new System.Drawing.Size(112, 45);
            this.btnPentryDelete.TabIndex = 15;
            this.btnPentryDelete.Text = "Purchase Entry Delete";
            this.btnPentryDelete.UseVisualStyleBackColor = false;
            this.btnPentryDelete.Click += new System.EventHandler(this.btnPentryDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnSentrydelete);
            this.groupBox2.Location = new System.Drawing.Point(484, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 145);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "sales";
            // 
            // btnSentrydelete
            // 
            this.btnSentrydelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnSentrydelete.Location = new System.Drawing.Point(36, 30);
            this.btnSentrydelete.Name = "btnSentrydelete";
            this.btnSentrydelete.Size = new System.Drawing.Size(112, 45);
            this.btnSentrydelete.TabIndex = 15;
            this.btnSentrydelete.Text = "Sales Entry Delete";
            this.btnSentrydelete.UseVisualStyleBackColor = false;
            this.btnSentrydelete.Click += new System.EventHandler(this.btnSentrydelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnEstock);
            this.groupBox1.Controls.Add(this.btnSstock);
            this.groupBox1.Location = new System.Drawing.Point(21, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 145);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "stock";
            // 
            // btnEstock
            // 
            this.btnEstock.BackColor = System.Drawing.SystemColors.Control;
            this.btnEstock.Location = new System.Drawing.Point(173, 19);
            this.btnEstock.Name = "btnEstock";
            this.btnEstock.Size = new System.Drawing.Size(112, 45);
            this.btnEstock.TabIndex = 14;
            this.btnEstock.Text = "Estimation Stock Zero";
            this.btnEstock.UseVisualStyleBackColor = false;
            this.btnEstock.Click += new System.EventHandler(this.btnEstock_Click);
            // 
            // btnSstock
            // 
            this.btnSstock.BackColor = System.Drawing.SystemColors.Control;
            this.btnSstock.Location = new System.Drawing.Point(23, 19);
            this.btnSstock.Name = "btnSstock";
            this.btnSstock.Size = new System.Drawing.Size(112, 45);
            this.btnSstock.TabIndex = 13;
            this.btnSstock.Text = "Sales Stock Zero";
            this.btnSstock.UseVisualStyleBackColor = false;
            this.btnSstock.Click += new System.EventHandler(this.btnSstock_Click);
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.SystemColors.Control;
            this.btnexit.Location = new System.Drawing.Point(381, 406);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(112, 45);
            this.btnexit.TabIndex = 17;
            this.btnexit.Text = "Exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // AdvanceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(966, 527);
            this.Controls.Add(this.pnlmain);
            this.Name = "AdvanceSetting";
            this.Text = "AdvanceSetting";
            this.Click += new System.EventHandler(this.AdvanceSetting_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdvanceSetting_KeyDown);
            this.pnlmain.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Button btnLdelete;
        internal System.Windows.Forms.Button btnEstock;
        internal System.Windows.Forms.Button btnSstock;
        internal System.Windows.Forms.Button btnAdelete;
        internal System.Windows.Forms.Button btnIdelete;
        internal System.Windows.Forms.Button btnPentryDelete;
        internal System.Windows.Forms.Button btnSentrydelete;
        internal System.Windows.Forms.Button btnexit;
    }
}