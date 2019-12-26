namespace Yadi.Vouchers
{
    partial class PackingListDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgPacking = new System.Windows.Forms.DataGridView();
            this.Srno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pkstocktrnno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain = new OMControls.OMBPanel();
            this.lstItemName = new System.Windows.Forms.DataGridView();
            this.lst_ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lst_ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FkStockTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LstUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgPackDetails = new System.Windows.Forms.DataGridView();
            this.Srno1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemname1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bagno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Itemno1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackingListNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.P_FkStockTrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMsg = new System.Windows.Forms.Label();
            this.plnPrinting = new System.Windows.Forms.Panel();
            this.rbMarathi = new System.Windows.Forms.RadioButton();
            this.rbEnglish = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgPacking)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPackDetails)).BeginInit();
            this.plnPrinting.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPacking
            // 
            this.dgPacking.AllowUserToAddRows = false;
            this.dgPacking.AllowUserToDeleteRows = false;
            this.dgPacking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPacking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Srno,
            this.Itemname,
            this.Qty,
            this.UOM,
            this.PackedQty,
            this.BalQty,
            this.Itemno,
            this.Pkstocktrnno});
            this.dgPacking.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgPacking.Location = new System.Drawing.Point(6, 19);
            this.dgPacking.Name = "dgPacking";
            this.dgPacking.ReadOnly = true;
            this.dgPacking.RowHeadersVisible = false;
            this.dgPacking.Size = new System.Drawing.Size(567, 387);
            this.dgPacking.TabIndex = 45454;
            this.dgPacking.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPacking_CellFormatting);
            // 
            // Srno
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.Srno.DefaultCellStyle = dataGridViewCellStyle1;
            this.Srno.HeaderText = "SrNo";
            this.Srno.Name = "Srno";
            this.Srno.ReadOnly = true;
            this.Srno.Width = 40;
            // 
            // Itemname
            // 
            this.Itemname.HeaderText = "Item Name";
            this.Itemname.Name = "Itemname";
            this.Itemname.ReadOnly = true;
            this.Itemname.Width = 250;
            // 
            // Qty
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 60;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.ReadOnly = true;
            this.UOM.Width = 60;
            // 
            // PackedQty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PackedQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.PackedQty.HeaderText = "Pkg Qty";
            this.PackedQty.Name = "PackedQty";
            this.PackedQty.ReadOnly = true;
            this.PackedQty.Width = 70;
            // 
            // BalQty
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BalQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.BalQty.HeaderText = "Bal Qty";
            this.BalQty.Name = "BalQty";
            this.BalQty.ReadOnly = true;
            this.BalQty.Width = 70;
            // 
            // Itemno
            // 
            this.Itemno.HeaderText = "ItemNo";
            this.Itemno.Name = "Itemno";
            this.Itemno.ReadOnly = true;
            this.Itemno.Visible = false;
            this.Itemno.Width = 50;
            // 
            // Pkstocktrnno
            // 
            this.Pkstocktrnno.HeaderText = "PkStocktrnNo";
            this.Pkstocktrnno.Name = "Pkstocktrnno";
            this.Pkstocktrnno.ReadOnly = true;
            this.Pkstocktrnno.Visible = false;
            this.Pkstocktrnno.Width = 50;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.plnPrinting);
            this.pnlMain.Controls.Add(this.lstItemName);
            this.pnlMain.Controls.Add(this.btnTPrint);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnPrint);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.dgPackDetails);
            this.pnlMain.Controls.Add(this.dgPacking);
            this.pnlMain.Location = new System.Drawing.Point(6, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1142, 481);
            this.pnlMain.TabIndex = 1;
            // 
            // lstItemName
            // 
            this.lstItemName.AllowUserToAddRows = false;
            this.lstItemName.AllowUserToDeleteRows = false;
            this.lstItemName.AllowUserToResizeColumns = false;
            this.lstItemName.AllowUserToResizeRows = false;
            this.lstItemName.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.lstItemName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstItemName.ColumnHeadersVisible = false;
            this.lstItemName.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lst_ItemName,
            this.lst_ItemNo,
            this.FkStockTrnNo,
            this.LstUOM});
            this.lstItemName.Location = new System.Drawing.Point(646, 72);
            this.lstItemName.Name = "lstItemName";
            this.lstItemName.ReadOnly = true;
            this.lstItemName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lstItemName.Size = new System.Drawing.Size(460, 242);
            this.lstItemName.TabIndex = 45456;
            this.lstItemName.Visible = false;
            this.lstItemName.CurrentCellChanged += new System.EventHandler(this.lstItemName_CurrentCellChanged);
            this.lstItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItemName_KeyDown);
            this.lstItemName.VisibleChanged += new System.EventHandler(this.lstItemName_VisibleChanged);
            // 
            // lst_ItemName
            // 
            this.lst_ItemName.HeaderText = "ItemName";
            this.lst_ItemName.Name = "lst_ItemName";
            this.lst_ItemName.ReadOnly = true;
            this.lst_ItemName.Width = 450;
            // 
            // lst_ItemNo
            // 
            this.lst_ItemNo.HeaderText = "ItemNo";
            this.lst_ItemNo.Name = "lst_ItemNo";
            this.lst_ItemNo.ReadOnly = true;
            this.lst_ItemNo.Visible = false;
            // 
            // FkStockTrnNo
            // 
            this.FkStockTrnNo.HeaderText = "FkStockTrnNo";
            this.FkStockTrnNo.Name = "FkStockTrnNo";
            this.FkStockTrnNo.ReadOnly = true;
            this.FkStockTrnNo.Visible = false;
            // 
            // LstUOM
            // 
            this.LstUOM.HeaderText = "LstUOM";
            this.LstUOM.Name = "LstUOM";
            this.LstUOM.ReadOnly = true;
            this.LstUOM.Visible = false;
            // 
            // btnTPrint
            // 
            this.btnTPrint.Location = new System.Drawing.Point(842, 412);
            this.btnTPrint.Name = "btnTPrint";
            this.btnTPrint.Size = new System.Drawing.Size(75, 53);
            this.btnTPrint.TabIndex = 45455;
            this.btnTPrint.Text = "TPrint";
            this.btnTPrint.UseVisualStyleBackColor = true;
            this.btnTPrint.Click += new System.EventHandler(this.btnTPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(761, 412);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 53);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(684, 412);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 53);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(607, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 53);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgPackDetails
            // 
            this.dgPackDetails.AllowUserToAddRows = false;
            this.dgPackDetails.AllowUserToDeleteRows = false;
            this.dgPackDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPackDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Srno1,
            this.Itemname1,
            this.UOM1,
            this.Qty1,
            this.Bagno,
            this.Itemno1,
            this.PackingListNo,
            this.GroupNo,
            this.P_FkStockTrnNo});
            this.dgPackDetails.Location = new System.Drawing.Point(579, 19);
            this.dgPackDetails.Name = "dgPackDetails";
            this.dgPackDetails.RowHeadersVisible = false;
            this.dgPackDetails.Size = new System.Drawing.Size(555, 387);
            this.dgPackDetails.TabIndex = 0;
            this.dgPackDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPackDetails_CellFormatting);
            this.dgPackDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPackDetails_CellEndEdit);
            this.dgPackDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgPackDetails_EditingControlShowing);
            this.dgPackDetails.CurrentCellChanged += new System.EventHandler(this.dgPackDetails_CurrentCellChanged);
            this.dgPackDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPackDetails_KeyDown);
            this.dgPackDetails.VisibleChanged += new System.EventHandler(this.lstItemName_VisibleChanged);
            // 
            // Srno1
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Srno1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Srno1.HeaderText = "SrNo";
            this.Srno1.Name = "Srno1";
            this.Srno1.ReadOnly = true;
            this.Srno1.Width = 50;
            // 
            // Itemname1
            // 
            this.Itemname1.HeaderText = "Item Name";
            this.Itemname1.Name = "Itemname1";
            this.Itemname1.ReadOnly = true;
            this.Itemname1.Width = 290;
            // 
            // UOM1
            // 
            this.UOM1.HeaderText = "UOM";
            this.UOM1.Name = "UOM1";
            this.UOM1.ReadOnly = true;
            this.UOM1.Width = 60;
            // 
            // Qty1
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Qty1.HeaderText = "Qty";
            this.Qty1.Name = "Qty1";
            this.Qty1.Width = 60;
            // 
            // Bagno
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Bagno.DefaultCellStyle = dataGridViewCellStyle7;
            this.Bagno.HeaderText = "Bag No";
            this.Bagno.Name = "Bagno";
            this.Bagno.Width = 80;
            // 
            // Itemno1
            // 
            this.Itemno1.HeaderText = "ItemNo";
            this.Itemno1.Name = "Itemno1";
            this.Itemno1.Visible = false;
            // 
            // PackingListNo
            // 
            this.PackingListNo.HeaderText = "PackingListNo";
            this.PackingListNo.Name = "PackingListNo";
            this.PackingListNo.Visible = false;
            // 
            // GroupNo
            // 
            this.GroupNo.HeaderText = "GroupNo";
            this.GroupNo.Name = "GroupNo";
            this.GroupNo.Visible = false;
            // 
            // P_FkStockTrnNo
            // 
            this.P_FkStockTrnNo.HeaderText = "FkStockTrnNo";
            this.P_FkStockTrnNo.Name = "P_FkStockTrnNo";
            this.P_FkStockTrnNo.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(384, 141);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 506;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // plnPrinting
            // 
            this.plnPrinting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.plnPrinting.BackColor = System.Drawing.Color.Transparent;
            this.plnPrinting.Controls.Add(this.rbMarathi);
            this.plnPrinting.Controls.Add(this.rbEnglish);
            this.plnPrinting.Location = new System.Drawing.Point(923, 412);
            this.plnPrinting.Name = "plnPrinting";
            this.plnPrinting.Size = new System.Drawing.Size(145, 26);
            this.plnPrinting.TabIndex = 123473;
            // 
            // rbMarathi
            // 
            this.rbMarathi.AutoSize = true;
            this.rbMarathi.Font = new System.Drawing.Font("Shivaji05", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMarathi.Location = new System.Drawing.Point(69, 5);
            this.rbMarathi.Name = "rbMarathi";
            this.rbMarathi.Size = new System.Drawing.Size(62, 18);
            this.rbMarathi.TabIndex = 1;
            this.rbMarathi.TabStop = true;
            this.rbMarathi.Text = "marazI";
            this.rbMarathi.UseVisualStyleBackColor = true;
            // 
            // rbEnglish
            // 
            this.rbEnglish.AutoSize = true;
            this.rbEnglish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEnglish.Location = new System.Drawing.Point(3, 6);
            this.rbEnglish.Name = "rbEnglish";
            this.rbEnglish.Size = new System.Drawing.Size(51, 17);
            this.rbEnglish.TabIndex = 0;
            this.rbEnglish.TabStop = true;
            this.rbEnglish.Text = "Eng.";
            this.rbEnglish.UseVisualStyleBackColor = true;
            // 
            // PackingListDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 496);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.pnlMain);
            this.Name = "PackingListDetails";
            this.Text = "PackingListDetails";
            this.Load += new System.EventHandler(this.PackingListDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPacking)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPackDetails)).EndInit();
            this.plnPrinting.ResumeLayout(false);
            this.plnPrinting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView dgPackDetails;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgPacking;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnTPrint;
        private System.Windows.Forms.DataGridView lstItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Srno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pkstocktrnno;
        private System.Windows.Forms.DataGridViewTextBoxColumn lst_ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn lst_ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkStockTrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LstUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Srno1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemname1;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bagno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemno1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackingListNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn P_FkStockTrnNo;
        private System.Windows.Forms.Panel plnPrinting;
        private System.Windows.Forms.RadioButton rbMarathi;
        private System.Windows.Forms.RadioButton rbEnglish;
    }
}