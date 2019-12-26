namespace Yadi.Display
{
    partial class DailySalesReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnShow = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.DTPDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.GridViewDaily = new System.Windows.Forms.DataGridView();
            this.pnlMain = new OMControls.OMBPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(295, 13);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 2;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(406, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(93, 27);
            this.btnPrint.TabIndex = 113;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // DTPDate
            // 
            this.DTPDate.CustomFormat = "dd-MMM-yyyy";
            this.DTPDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPDate.Location = new System.Drawing.Point(139, 17);
            this.DTPDate.Name = "DTPDate";
            this.DTPDate.Size = new System.Drawing.Size(134, 23);
            this.DTPDate.TabIndex = 1;
            this.DTPDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 107;
            this.label1.Text = "Today\'s Date :";
            // 
            // GridViewDaily
            // 
            this.GridViewDaily.AllowUserToAddRows = false;
            this.GridViewDaily.AllowUserToDeleteRows = false;
            this.GridViewDaily.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridViewDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewDaily.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.GridViewDaily.Location = new System.Drawing.Point(13, 58);
            this.GridViewDaily.Name = "GridViewDaily";
            this.GridViewDaily.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewDaily.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridViewDaily.RowTemplate.Height = 27;
            this.GridViewDaily.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridViewDaily.Size = new System.Drawing.Size(815, 395);
            this.GridViewDaily.TabIndex = 117;
            this.GridViewDaily.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridViewDaily_CellFormatting_2);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.GridViewDaily);
            this.pnlMain.Controls.Add(this.DTPDate);
            this.pnlMain.Controls.Add(this.BtnShow);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Location = new System.Drawing.Point(6, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(855, 474);
            this.pnlMain.TabIndex = 118;
            // 
            // DailySalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 505);
            this.Controls.Add(this.pnlMain);
            this.Name = "DailySalesReport";
            this.Text = "Daily Sales Report";
            this.Load += new System.EventHandler(this.DailySalesRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewDaily)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.Button btnPrint;
        internal System.Windows.Forms.DateTimePicker DTPDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DataGridView GridViewDaily;
        private OMControls.OMBPanel pnlMain;
    }
}