namespace Yadi.Master
{
    partial class StartDay
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNo = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTodaysDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpLastTransDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpLastTransDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpTodaysDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Controls.Add(this.BtnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(305, 169);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Are you sure you want to set this Start Date?";
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(163, 123);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(80, 28);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "&No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(50, 123);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(80, 28);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "&Yes";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Date";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(121, 65);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(139, 20);
            this.dtpDate.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Today\'s date";
            // 
            // dtpTodaysDate
            // 
            this.dtpTodaysDate.Enabled = false;
            this.dtpTodaysDate.Location = new System.Drawing.Point(121, 9);
            this.dtpTodaysDate.Name = "dtpTodaysDate";
            this.dtpTodaysDate.Size = new System.Drawing.Size(139, 20);
            this.dtpTodaysDate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Last Trans.Date";
            // 
            // dtpLastTransDate
            // 
            this.dtpLastTransDate.Enabled = false;
            this.dtpLastTransDate.Location = new System.Drawing.Point(121, 38);
            this.dtpLastTransDate.Name = "dtpLastTransDate";
            this.dtpLastTransDate.Size = new System.Drawing.Size(139, 20);
            this.dtpLastTransDate.TabIndex = 6;
            // 
            // StartDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 171);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartDay";
            this.Text = "StartDay";
            this.Load += new System.EventHandler(this.StartDay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpLastTransDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTodaysDate;
    }
}