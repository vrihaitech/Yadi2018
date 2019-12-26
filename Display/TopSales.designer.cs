namespace Yadi.Display
{
    partial class TopSales
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
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdItem = new System.Windows.Forms.RadioButton();
            this.pnlMain = new OMControls.OMBPanel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTopQty = new System.Windows.Forms.TextBox();
            this.txtBottomValue = new System.Windows.Forms.TextBox();
            this.txtBottomQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DTPToDate = new System.Windows.Forms.DateTimePicker();
            this.txtTopValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdBrand = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(102, 10);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(131, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "From Date :";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(6, 217);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdItem
            // 
            this.rdItem.AutoSize = true;
            this.rdItem.Checked = true;
            this.rdItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdItem.Location = new System.Drawing.Point(6, 49);
            this.rdItem.Name = "rdItem";
            this.rdItem.Size = new System.Drawing.Size(74, 17);
            this.rdItem.TabIndex = 2;
            this.rdItem.TabStop = true;
            this.rdItem.Text = "Itemwise";
            this.rdItem.UseVisualStyleBackColor = true;
            this.rdItem.CheckedChanged += new System.EventHandler(this.rdItem_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.txtTopQty);
            this.pnlMain.Controls.Add(this.txtBottomValue);
            this.pnlMain.Controls.Add(this.txtBottomQty);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.DTPToDate);
            this.pnlMain.Controls.Add(this.txtTopValue);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.rdBrand);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.BtnExport);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.rdItem);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(498, 268);
            this.pnlMain.TabIndex = 109;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(224, 13);
            this.label12.TabIndex = 126;
            this.label12.Text = "Enter No. Of Records To Be Shown In Report";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(204, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 125;
            this.label11.Text = "**";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(326, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 124;
            this.label10.Text = "**";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(326, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 123;
            this.label9.Text = "**";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "**";
            // 
            // txtTopQty
            // 
            this.txtTopQty.Location = new System.Drawing.Point(247, 99);
            this.txtTopQty.Name = "txtTopQty";
            this.txtTopQty.Size = new System.Drawing.Size(73, 20);
            this.txtTopQty.TabIndex = 5;
            this.txtTopQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTopQty.TextChanged += new System.EventHandler(this.txtTopQty_TextChanged);
            this.txtTopQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTopQty_KeyDown);
            this.txtTopQty.Leave += new System.EventHandler(this.txtTopQty_Leave);
            // 
            // txtBottomValue
            // 
            this.txtBottomValue.Location = new System.Drawing.Point(125, 137);
            this.txtBottomValue.Name = "txtBottomValue";
            this.txtBottomValue.Size = new System.Drawing.Size(73, 20);
            this.txtBottomValue.TabIndex = 6;
            this.txtBottomValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBottomValue.TextChanged += new System.EventHandler(this.txtBottomValue_TextChanged);
            this.txtBottomValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBottomValue_KeyDown);
            this.txtBottomValue.Leave += new System.EventHandler(this.txtBottomValue_Leave);
            // 
            // txtBottomQty
            // 
            this.txtBottomQty.Location = new System.Drawing.Point(247, 137);
            this.txtBottomQty.Name = "txtBottomQty";
            this.txtBottomQty.Size = new System.Drawing.Size(73, 20);
            this.txtBottomQty.TabIndex = 7;
            this.txtBottomQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBottomQty.TextChanged += new System.EventHandler(this.txtBottomQty_TextChanged);
            this.txtBottomQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBottomQty_KeyDown);
            this.txtBottomQty.Leave += new System.EventHandler(this.txtBottomQty_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(122, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 16);
            this.label7.TabIndex = 118;
            this.label7.Text = "Valuewise";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(253, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 117;
            this.label6.Text = "Qtywise";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 116;
            this.label5.Text = "Bottom  :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 16);
            this.label4.TabIndex = 115;
            this.label4.Text = "** ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(252, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 114;
            this.label3.Text = "To Date :";
            // 
            // DTPToDate
            // 
            this.DTPToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPToDate.Location = new System.Drawing.Point(351, 10);
            this.DTPToDate.Name = "DTPToDate";
            this.DTPToDate.Size = new System.Drawing.Size(131, 23);
            this.DTPToDate.TabIndex = 1;
            // 
            // txtTopValue
            // 
            this.txtTopValue.Location = new System.Drawing.Point(125, 100);
            this.txtTopValue.Name = "txtTopValue";
            this.txtTopValue.Size = new System.Drawing.Size(73, 20);
            this.txtTopValue.TabIndex = 4;
            this.txtTopValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTopValue.TextChanged += new System.EventHandler(this.txtTopValue_TextChanged);
            this.txtTopValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTopValue_KeyDown);
            this.txtTopValue.Leave += new System.EventHandler(this.txtTopValue_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 112;
            this.label2.Text = "Top  :";
            // 
            // rdBrand
            // 
            this.rdBrand.AutoSize = true;
            this.rdBrand.Checked = true;
            this.rdBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBrand.Location = new System.Drawing.Point(104, 49);
            this.rdBrand.Name = "rdBrand";
            this.rdBrand.Size = new System.Drawing.Size(83, 17);
            this.rdBrand.TabIndex = 3;
            this.rdBrand.TabStop = true;
            this.rdBrand.Text = "Brandwise";
            this.rdBrand.UseVisualStyleBackColor = true;
            this.rdBrand.CheckedChanged += new System.EventHandler(this.rdBrand_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(210, 217);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 110;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(108, 217);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 27);
            this.BtnExport.TabIndex = 103;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // TopSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 528);
            this.Controls.Add(this.pnlMain);
            this.Name = "TopSales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Top Item/Brand Sales";
            this.Load += new System.EventHandler(this.TopItemSales_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdItem;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button BtnExport;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rdBrand;
        private System.Windows.Forms.TextBox txtTopValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.DateTimePicker DTPToDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTopQty;
        private System.Windows.Forms.TextBox txtBottomValue;
        private System.Windows.Forms.TextBox txtBottomQty;
    }
}