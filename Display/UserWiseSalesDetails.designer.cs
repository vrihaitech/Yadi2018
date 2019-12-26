 namespace Yadi.Display
{
    partial class UserWiseSalesDetails
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
            this.BtnShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlItemDetails = new System.Windows.Forms.Panel();
            this.BtnExport = new System.Windows.Forms.Button();
            this.rdDayWiseSummary = new System.Windows.Forms.RadioButton();
            this.rdSummary = new System.Windows.Forms.RadioButton();
            this.rdDetails = new System.Windows.Forms.RadioButton();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.gvItem = new System.Windows.Forms.DataGridView();
            this.Iteno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.BtnItmShow = new System.Windows.Forms.Button();
            this.pnlRateType = new System.Windows.Forms.Panel();
            this.btnRateTypeCancel = new System.Windows.Forms.Button();
            this.btnRateTypeOK = new System.Windows.Forms.Button();
            this.txtRateTypePassword = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlItemDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlRateType.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnShow
            // 
            this.BtnShow.Location = new System.Drawing.Point(3, 402);
            this.BtnShow.Name = "BtnShow";
            this.BtnShow.Size = new System.Drawing.Size(93, 27);
            this.BtnShow.TabIndex = 5;
            this.BtnShow.Text = "Show";
            this.BtnShow.UseVisualStyleBackColor = false;
            this.BtnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(292, 7);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(221, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(95, 7);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(120, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // pnlItemDetails
            // 
            this.pnlItemDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlItemDetails.Controls.Add(this.BtnExport);
            this.pnlItemDetails.Controls.Add(this.rdDayWiseSummary);
            this.pnlItemDetails.Controls.Add(this.rdSummary);
            this.pnlItemDetails.Controls.Add(this.rdDetails);
            this.pnlItemDetails.Controls.Add(this.chkSelectAll);
            this.pnlItemDetails.Controls.Add(this.gvItem);
            this.pnlItemDetails.Controls.Add(this.BtnShow);
            this.pnlItemDetails.Location = new System.Drawing.Point(96, 46);
            this.pnlItemDetails.Name = "pnlItemDetails";
            this.pnlItemDetails.Size = new System.Drawing.Size(383, 438);
            this.pnlItemDetails.TabIndex = 74;
            this.pnlItemDetails.Visible = false;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(115, 402);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(93, 27);
            this.BtnExport.TabIndex = 78;
            this.BtnExport.Text = "Excel";
            this.BtnExport.UseVisualStyleBackColor = false;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // rdDayWiseSummary
            // 
            this.rdDayWiseSummary.AutoSize = true;
            this.rdDayWiseSummary.Location = new System.Drawing.Point(182, 11);
            this.rdDayWiseSummary.Name = "rdDayWiseSummary";
            this.rdDayWiseSummary.Size = new System.Drawing.Size(68, 17);
            this.rdDayWiseSummary.TabIndex = 77;
            this.rdDayWiseSummary.TabStop = true;
            this.rdDayWiseSummary.Text = "DayWise";
            this.rdDayWiseSummary.UseVisualStyleBackColor = true;
            // 
            // rdSummary
            // 
            this.rdSummary.AutoSize = true;
            this.rdSummary.Location = new System.Drawing.Point(98, 11);
            this.rdSummary.Name = "rdSummary";
            this.rdSummary.Size = new System.Drawing.Size(68, 17);
            this.rdSummary.TabIndex = 76;
            this.rdSummary.Text = "Summary";
            this.rdSummary.UseVisualStyleBackColor = true;
            // 
            // rdDetails
            // 
            this.rdDetails.AutoSize = true;
            this.rdDetails.Checked = true;
            this.rdDetails.Location = new System.Drawing.Point(22, 11);
            this.rdDetails.Name = "rdDetails";
            this.rdDetails.Size = new System.Drawing.Size(57, 17);
            this.rdDetails.TabIndex = 75;
            this.rdDetails.TabStop = true;
            this.rdDetails.Text = "Details";
            this.rdDetails.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(242, 402);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 3;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // gvItem
            // 
            this.gvItem.AllowUserToAddRows = false;
            this.gvItem.AllowUserToDeleteRows = false;
            this.gvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Iteno,
            this.Item,
            this.Chk});
            this.gvItem.Location = new System.Drawing.Point(3, 34);
            this.gvItem.Name = "gvItem";
            this.gvItem.Size = new System.Drawing.Size(362, 352);
            this.gvItem.TabIndex = 4;
            this.gvItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvItem_KeyDown);
            // 
            // Iteno
            // 
            this.Iteno.DataPropertyName = "ItemName";
            this.Iteno.HeaderText = "ItemNo";
            this.Iteno.Name = "Iteno";
            this.Iteno.Visible = false;
            // 
            // Item
            // 
            this.Item.DataPropertyName = "ItemName";
            this.Item.HeaderText = "ItemName";
            this.Item.Name = "Item";
            this.Item.ReadOnly = true;
            this.Item.Width = 250;
            // 
            // Chk
            // 
            this.Chk.HeaderText = "Select";
            this.Chk.Name = "Chk";
            this.Chk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk.Width = 80;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // BtnItmShow
            // 
            this.BtnItmShow.Location = new System.Drawing.Point(407, 3);
            this.BtnItmShow.Name = "BtnItmShow";
            this.BtnItmShow.Size = new System.Drawing.Size(93, 27);
            this.BtnItmShow.TabIndex = 2;
            this.BtnItmShow.Text = "ShowItem";
            this.BtnItmShow.UseVisualStyleBackColor = false;
            this.BtnItmShow.Click += new System.EventHandler(this.BtnItmShow_Click);
            // 
            // pnlRateType
            // 
            this.pnlRateType.BackColor = System.Drawing.Color.Transparent;
            this.pnlRateType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRateType.Controls.Add(this.btnRateTypeCancel);
            this.pnlRateType.Controls.Add(this.btnRateTypeOK);
            this.pnlRateType.Controls.Add(this.txtRateTypePassword);
            this.pnlRateType.Controls.Add(this.label40);
            this.pnlRateType.Controls.Add(this.label44);
            this.pnlRateType.Location = new System.Drawing.Point(154, 275);
            this.pnlRateType.Name = "pnlRateType";
            this.pnlRateType.Size = new System.Drawing.Size(409, 104);
            this.pnlRateType.TabIndex = 5554;
            this.pnlRateType.Visible = false;
            // 
            // btnRateTypeCancel
            // 
            this.btnRateTypeCancel.Location = new System.Drawing.Point(209, 64);
            this.btnRateTypeCancel.Name = "btnRateTypeCancel";
            this.btnRateTypeCancel.Size = new System.Drawing.Size(75, 23);
            this.btnRateTypeCancel.TabIndex = 709;
            this.btnRateTypeCancel.Text = "Cancel";
            this.btnRateTypeCancel.UseVisualStyleBackColor = true;
            this.btnRateTypeCancel.Click += new System.EventHandler(this.btnRateTypeCancel_Click);
            // 
            // btnRateTypeOK
            // 
            this.btnRateTypeOK.Location = new System.Drawing.Point(110, 64);
            this.btnRateTypeOK.Name = "btnRateTypeOK";
            this.btnRateTypeOK.Size = new System.Drawing.Size(75, 23);
            this.btnRateTypeOK.TabIndex = 706;
            this.btnRateTypeOK.Text = "OK";
            this.btnRateTypeOK.UseVisualStyleBackColor = true;
            this.btnRateTypeOK.Click += new System.EventHandler(this.btnRateTypeOK_Click);
            // 
            // txtRateTypePassword
            // 
            this.txtRateTypePassword.Location = new System.Drawing.Point(137, 30);
            this.txtRateTypePassword.MaxLength = 6;
            this.txtRateTypePassword.Name = "txtRateTypePassword";
            this.txtRateTypePassword.PasswordChar = '*';
            this.txtRateTypePassword.Size = new System.Drawing.Size(187, 20);
            this.txtRateTypePassword.TabIndex = 703;
            this.txtRateTypePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRateTypePassword_KeyDown);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(34, 31);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(53, 13);
            this.label40.TabIndex = 3;
            this.label40.Text = "Password";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Bisque;
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(407, 13);
            this.label44.TabIndex = 0;
            this.label44.Text = "Rate Type Password Details";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.pnlRateType);
            this.pnlMain.Controls.Add(this.pnlItemDetails);
            this.pnlMain.Controls.Add(this.BtnItmShow);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(600, 499);
            this.pnlMain.TabIndex = 79;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(506, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 5555;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // UserWiseSalesDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 676);
            this.Controls.Add(this.pnlMain);
            this.Name = "UserWiseSalesDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserWiseItemSalesDetails";
            this.Load += new System.EventHandler(this.UserWiseSalesDetails_Load);
            this.pnlItemDetails.ResumeLayout(false);
            this.pnlItemDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlRateType.ResumeLayout(false);
            this.pnlRateType.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnShow;
        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlItemDetails;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.DataGridView gvItem;
        internal System.Windows.Forms.Button BtnItmShow;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iteno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Item;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk;
        private System.Windows.Forms.RadioButton rdSummary;
        private System.Windows.Forms.RadioButton rdDetails;
        private System.Windows.Forms.Panel pnlRateType;
        private System.Windows.Forms.Button btnRateTypeCancel;
        private System.Windows.Forms.Button btnRateTypeOK;
        private System.Windows.Forms.TextBox txtRateTypePassword;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.RadioButton rdDayWiseSummary;
        internal System.Windows.Forms.Button BtnExport;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
    }
}