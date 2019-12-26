 namespace Yadi.Display
{
     partial class PurchaseOrderDetails
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.rbOpen = new System.Windows.Forms.RadioButton();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbClose = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlPartyDetails = new System.Windows.Forms.Panel();
            this.pnlSelectType = new System.Windows.Forms.Panel();
            this.btnBoth = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.chkPartySelectAll = new System.Windows.Forms.CheckBox();
            this.gvParty = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chk1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BtnShowItem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPartyShow = new System.Windows.Forms.Button();
            this.DTToDate = new System.Windows.Forms.DateTimePicker();
            this.DTPFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlSelect.SuspendLayout();
            this.pnlPartyDetails.SuspendLayout();
            this.pnlSelectType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlSelect);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.pnlPartyDetails);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnPartyShow);
            this.pnlMain.Controls.Add(this.DTToDate);
            this.pnlMain.Controls.Add(this.DTPFromDate);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Location = new System.Drawing.Point(6, 22);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(637, 552);
            this.pnlMain.TabIndex = 75;
            // 
            // pnlSelect
            // 
            this.pnlSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelect.Controls.Add(this.rbOpen);
            this.pnlSelect.Controls.Add(this.rbBoth);
            this.pnlSelect.Controls.Add(this.rbClose);
            this.pnlSelect.Location = new System.Drawing.Point(7, 31);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(388, 27);
            this.pnlSelect.TabIndex = 83;
            // 
            // rbOpen
            // 
            this.rbOpen.AutoSize = true;
            this.rbOpen.Location = new System.Drawing.Point(9, 4);
            this.rbOpen.Name = "rbOpen";
            this.rbOpen.Size = new System.Drawing.Size(51, 17);
            this.rbOpen.TabIndex = 80;
            this.rbOpen.TabStop = true;
            this.rbOpen.Text = "Open";
            this.rbOpen.UseVisualStyleBackColor = true;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Location = new System.Drawing.Point(163, 4);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(47, 17);
            this.rbBoth.TabIndex = 82;
            this.rbBoth.TabStop = true;
            this.rbBoth.Text = "Both";
            this.rbBoth.UseVisualStyleBackColor = true;
            // 
            // rbClose
            // 
            this.rbClose.AutoSize = true;
            this.rbClose.Location = new System.Drawing.Point(85, 4);
            this.rbClose.Name = "rbClose";
            this.rbClose.Size = new System.Drawing.Size(51, 17);
            this.rbClose.TabIndex = 81;
            this.rbClose.TabStop = true;
            this.rbClose.Text = "Close";
            this.rbClose.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(541, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 27);
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlPartyDetails
            // 
            this.pnlPartyDetails.Controls.Add(this.pnlSelectType);
            this.pnlPartyDetails.Controls.Add(this.BtnCancel);
            this.pnlPartyDetails.Controls.Add(this.chkPartySelectAll);
            this.pnlPartyDetails.Controls.Add(this.gvParty);
            this.pnlPartyDetails.Controls.Add(this.BtnShowItem);
            this.pnlPartyDetails.Location = new System.Drawing.Point(19, 64);
            this.pnlPartyDetails.Name = "pnlPartyDetails";
            this.pnlPartyDetails.Size = new System.Drawing.Size(462, 478);
            this.pnlPartyDetails.TabIndex = 75;
            this.pnlPartyDetails.Visible = false;
            // 
            // pnlSelectType
            // 
            this.pnlSelectType.Controls.Add(this.btnBoth);
            this.pnlSelectType.Controls.Add(this.btnClose);
            this.pnlSelectType.Controls.Add(this.btnOpen);
            this.pnlSelectType.Location = new System.Drawing.Point(13, 477);
            this.pnlSelectType.Name = "pnlSelectType";
            this.pnlSelectType.Size = new System.Drawing.Size(391, 36);
            this.pnlSelectType.TabIndex = 79;
            this.pnlSelectType.Visible = false;
            // 
            // btnBoth
            // 
            this.btnBoth.Location = new System.Drawing.Point(197, 3);
            this.btnBoth.Name = "btnBoth";
            this.btnBoth.Size = new System.Drawing.Size(93, 27);
            this.btnBoth.TabIndex = 78;
            this.btnBoth.Text = "Both";
            this.btnBoth.UseVisualStyleBackColor = true;
            this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(98, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 27);
            this.btnClose.TabIndex = 77;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(3, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(93, 27);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(114, 445);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(93, 27);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // chkPartySelectAll
            // 
            this.chkPartySelectAll.AutoSize = true;
            this.chkPartySelectAll.Location = new System.Drawing.Point(254, 445);
            this.chkPartySelectAll.Name = "chkPartySelectAll";
            this.chkPartySelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkPartySelectAll.TabIndex = 3;
            this.chkPartySelectAll.Text = "SelectAll (F2)";
            this.chkPartySelectAll.UseVisualStyleBackColor = true;
            this.chkPartySelectAll.CheckedChanged += new System.EventHandler(this.chkPartySelectAll_CheckedChanged);
            // 
            // gvParty
            // 
            this.gvParty.AllowUserToAddRows = false;
            this.gvParty.AllowUserToDeleteRows = false;
            this.gvParty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvParty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Chk1});
            this.gvParty.Location = new System.Drawing.Point(13, 13);
            this.gvParty.Name = "gvParty";
            this.gvParty.Size = new System.Drawing.Size(394, 423);
            this.gvParty.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LedgerNo";
            this.dataGridViewTextBoxColumn1.HeaderText = "LedgerNo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "LedgerName";
            this.dataGridViewTextBoxColumn2.HeaderText = "PartyName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // Chk1
            // 
            this.Chk1.DataPropertyName = "chk";
            this.Chk1.HeaderText = "Select";
            this.Chk1.Name = "Chk1";
            this.Chk1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Chk1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Chk1.Width = 80;
            // 
            // BtnShowItem
            // 
            this.BtnShowItem.Location = new System.Drawing.Point(15, 445);
            this.BtnShowItem.Name = "BtnShowItem";
            this.BtnShowItem.Size = new System.Drawing.Size(93, 27);
            this.BtnShowItem.TabIndex = 5;
            this.BtnShowItem.Text = "Show";
            this.BtnShowItem.UseVisualStyleBackColor = false;
            this.BtnShowItem.Click += new System.EventHandler(this.BtnShowItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "From Date :";
            // 
            // BtnPartyShow
            // 
            this.BtnPartyShow.Location = new System.Drawing.Point(401, 2);
            this.BtnPartyShow.Name = "BtnPartyShow";
            this.BtnPartyShow.Size = new System.Drawing.Size(137, 27);
            this.BtnPartyShow.TabIndex = 2;
            this.BtnPartyShow.Text = "Show Party";
            this.BtnPartyShow.UseVisualStyleBackColor = false;
            this.BtnPartyShow.Click += new System.EventHandler(this.BtnPartyShow_Click);
            // 
            // DTToDate
            // 
            this.DTToDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTToDate.Location = new System.Drawing.Point(288, 4);
            this.DTToDate.Name = "DTToDate";
            this.DTToDate.Size = new System.Drawing.Size(110, 23);
            this.DTToDate.TabIndex = 1;
            this.DTToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTToDate_KeyPress);
            // 
            // DTPFromDate
            // 
            this.DTPFromDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPFromDate.Location = new System.Drawing.Point(93, 4);
            this.DTPFromDate.Name = "DTPFromDate";
            this.DTPFromDate.Size = new System.Drawing.Size(120, 23);
            this.DTPFromDate.TabIndex = 0;
            this.DTPFromDate.ValueChanged += new System.EventHandler(this.DTPFromDate_ValueChanged);
            this.DTPFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DTPFromDate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "To Date :";
            // 
            // PurchaseOrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 634);
            this.Controls.Add(this.pnlMain);
            this.Name = "PurchaseOrderDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Report";
            this.Load += new System.EventHandler(this.StockSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlSelect.ResumeLayout(false);
            this.pnlSelect.PerformLayout();
            this.pnlPartyDetails.ResumeLayout(false);
            this.pnlPartyDetails.PerformLayout();
            this.pnlSelectType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvParty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker DTToDate;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker DTPFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider EP;
        internal System.Windows.Forms.Button BtnPartyShow;
        private System.Windows.Forms.Panel pnlPartyDetails;
        private System.Windows.Forms.CheckBox chkPartySelectAll;
        private System.Windows.Forms.DataGridView gvParty;
        internal System.Windows.Forms.Button BtnShowItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chk1;
        internal System.Windows.Forms.Button BtnCancel;
        private OMControls.OMBPanel pnlMain;
        internal System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlSelectType;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnBoth;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbClose;
        private System.Windows.Forms.RadioButton rbOpen;
        private System.Windows.Forms.Panel pnlSelect;
    }
}