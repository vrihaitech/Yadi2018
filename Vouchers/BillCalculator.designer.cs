namespace Yadi.Vouchers
{
    partial class BillCalculator
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRecAmt = new System.Windows.Forms.TextBox();
            this.txtBillAmt = new System.Windows.Forms.TextBox();
            this.txtBalAmt = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnFullAmt = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Receive Amount : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bill Amount : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Refund Rs.";
            // 
            // txtRecAmt
            // 
            this.txtRecAmt.BackColor = System.Drawing.Color.White;
            this.txtRecAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecAmt.Location = new System.Drawing.Point(203, 93);
            this.txtRecAmt.Name = "txtRecAmt";
            this.txtRecAmt.Size = new System.Drawing.Size(136, 26);
            this.txtRecAmt.TabIndex = 0;
            this.txtRecAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRecAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecAmt_KeyDown);
            // 
            // txtBillAmt
            // 
            this.txtBillAmt.BackColor = System.Drawing.Color.White;
            this.txtBillAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillAmt.Location = new System.Drawing.Point(203, 137);
            this.txtBillAmt.Name = "txtBillAmt";
            this.txtBillAmt.ReadOnly = true;
            this.txtBillAmt.Size = new System.Drawing.Size(136, 26);
            this.txtBillAmt.TabIndex = 10;
            this.txtBillAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBalAmt
            // 
            this.txtBalAmt.BackColor = System.Drawing.Color.White;
            this.txtBalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalAmt.Location = new System.Drawing.Point(203, 187);
            this.txtBalAmt.Name = "txtBalAmt";
            this.txtBalAmt.ReadOnly = true;
            this.txtBalAmt.Size = new System.Drawing.Size(136, 26);
            this.txtBalAmt.TabIndex = 1;
            this.txtBalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(345, 153);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 60);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(541, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(38, 9);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 50);
            this.button1.TabIndex = 12;
            this.button1.Text = "2000";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Common_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 50);
            this.button2.TabIndex = 13;
            this.button2.Text = "500";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Common_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(247, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 50);
            this.button3.TabIndex = 14;
            this.button3.Text = "100";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Common_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(323, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 50);
            this.button4.TabIndex = 15;
            this.button4.Text = "50";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Common_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(399, 26);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(70, 50);
            this.button5.TabIndex = 16;
            this.button5.Text = "20";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Common_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(475, 26);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(70, 50);
            this.button6.TabIndex = 17;
            this.button6.Text = "10";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Common_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(551, 26);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(70, 50);
            this.button7.TabIndex = 18;
            this.button7.Text = "5";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Common_Click);
            // 
            // btnFullAmt
            // 
            this.btnFullAmt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFullAmt.Location = new System.Drawing.Point(541, 86);
            this.btnFullAmt.Name = "btnFullAmt";
            this.btnFullAmt.Size = new System.Drawing.Size(80, 60);
            this.btnFullAmt.TabIndex = 19;
            this.btnFullAmt.Text = "Full Amount";
            this.btnFullAmt.UseVisualStyleBackColor = true;
            this.btnFullAmt.Click += new System.EventHandler(this.btnFullAmt_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(161, 26);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(70, 50);
            this.button8.TabIndex = 20;
            this.button8.Text = "200";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Common_Click);
            // 
            // BillCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 222);
            this.ControlBox = false;
            this.Controls.Add(this.button8);
            this.Controls.Add(this.btnFullAmt);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBalAmt);
            this.Controls.Add(this.txtBillAmt);
            this.Controls.Add(this.txtRecAmt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "BillCalculator";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BillCalculator";
            this.Load += new System.EventHandler(this.BillCalculator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRecAmt;
        private System.Windows.Forms.TextBox txtBillAmt;
        private System.Windows.Forms.TextBox txtBalAmt;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnFullAmt;
        private System.Windows.Forms.Button button8;
    }
}