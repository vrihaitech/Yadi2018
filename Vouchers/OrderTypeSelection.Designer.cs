namespace Yadi.Vouchers
{
    partial class OrderTypeSelection
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
            this.btnOrderFirst = new System.Windows.Forms.Button();
            this.btnOrderSecond = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOrderFirst
            // 
            this.btnOrderFirst.Location = new System.Drawing.Point(20, 21);
            this.btnOrderFirst.Name = "btnOrderFirst";
            this.btnOrderFirst.Size = new System.Drawing.Size(106, 68);
            this.btnOrderFirst.TabIndex = 9;
            this.btnOrderFirst.Text = "Counter Sales (F1)";
            this.btnOrderFirst.UseVisualStyleBackColor = true;
            this.btnOrderFirst.Click += new System.EventHandler(this.btnOrderFirst_Click);
            // 
            // btnOrderSecond
            // 
            this.btnOrderSecond.Location = new System.Drawing.Point(145, 21);
            this.btnOrderSecond.Name = "btnOrderSecond";
            this.btnOrderSecond.Size = new System.Drawing.Size(106, 68);
            this.btnOrderSecond.TabIndex = 10;
            this.btnOrderSecond.Text = "Home";
            this.btnOrderSecond.UseVisualStyleBackColor = true;
            this.btnOrderSecond.Click += new System.EventHandler(this.btnOrderSecond_Click);
            // 
            // OrderTypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 109);
            this.Controls.Add(this.btnOrderSecond);
            this.Controls.Add(this.btnOrderFirst);
            this.Name = "OrderTypeSelection";
            this.Text = "Order Type Selection";
            this.Load += new System.EventHandler(this.POSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOrderFirst;
        private System.Windows.Forms.Button btnOrderSecond;

    }
}