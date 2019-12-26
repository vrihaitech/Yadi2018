namespace Yadi.Utilities
{
    partial class SeasonalBarcodePrint
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
            this.rdItemwise = new System.Windows.Forms.RadioButton();
            this.rdPurchaseItemwise = new System.Windows.Forms.RadioButton();
            this.dgvSeasonalBarcode = new System.Windows.Forms.DataGridView();
            this.btnBarcodePrint = new System.Windows.Forms.Button();
            this.rdSmall = new System.Windows.Forms.RadioButton();
            this.rdBig = new System.Windows.Forms.RadioButton();
            this.pnlItemwise = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnImportItems = new System.Windows.Forms.Button();
            this.OF = new System.Windows.Forms.OpenFileDialog();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.PKSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasonalBarcode)).BeginInit();
            this.pnlItemwise.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdItemwise
            // 
            this.rdItemwise.AutoSize = true;
            this.rdItemwise.Checked = true;
            this.rdItemwise.Location = new System.Drawing.Point(13, 8);
            this.rdItemwise.Name = "rdItemwise";
            this.rdItemwise.Size = new System.Drawing.Size(87, 17);
            this.rdItemwise.TabIndex = 0;
            this.rdItemwise.TabStop = true;
            this.rdItemwise.Text = "Itemwise (F3)";
            this.rdItemwise.UseVisualStyleBackColor = true;
            this.rdItemwise.CheckedChanged += new System.EventHandler(this.rdItemwise_CheckedChanged);
            // 
            // rdPurchaseItemwise
            // 
            this.rdPurchaseItemwise.AutoSize = true;
            this.rdPurchaseItemwise.Location = new System.Drawing.Point(148, 8);
            this.rdPurchaseItemwise.Name = "rdPurchaseItemwise";
            this.rdPurchaseItemwise.Size = new System.Drawing.Size(135, 17);
            this.rdPurchaseItemwise.TabIndex = 1;
            this.rdPurchaseItemwise.Text = "Purchase Itemwise (F4)";
            this.rdPurchaseItemwise.UseVisualStyleBackColor = true;
            this.rdPurchaseItemwise.CheckedChanged += new System.EventHandler(this.rdPurchaseItemwise_CheckedChanged);
            // 
            // dgvSeasonalBarcode
            // 
            this.dgvSeasonalBarcode.AllowUserToAddRows = false;
            this.dgvSeasonalBarcode.AllowUserToDeleteRows = false;
            this.dgvSeasonalBarcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeasonalBarcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PKSrNo,
            this.BillNo,
            this.ItemName,
            this.MRP,
            this.Qty,
            this.ActualQty,
            this.Barcode,
            this.IsPrint});
            this.dgvSeasonalBarcode.Location = new System.Drawing.Point(13, 61);
            this.dgvSeasonalBarcode.Name = "dgvSeasonalBarcode";
            this.dgvSeasonalBarcode.Size = new System.Drawing.Size(795, 248);
            this.dgvSeasonalBarcode.TabIndex = 2;
            this.dgvSeasonalBarcode.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeasonalBarcode_CellEndEdit);
            this.dgvSeasonalBarcode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeasonalBarcode_CellClick);
            this.dgvSeasonalBarcode.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSeasonalBarcode_EditingControlShowing);
            this.dgvSeasonalBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSeasonalBarcode_KeyDown);
            this.dgvSeasonalBarcode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeasonalBarcode_CellContentClick);
            // 
            // btnBarcodePrint
            // 
            this.btnBarcodePrint.Location = new System.Drawing.Point(13, 343);
            this.btnBarcodePrint.Name = "btnBarcodePrint";
            this.btnBarcodePrint.Size = new System.Drawing.Size(152, 46);
            this.btnBarcodePrint.TabIndex = 3;
            this.btnBarcodePrint.Text = "Barcode Print (F5)";
            this.btnBarcodePrint.UseVisualStyleBackColor = true;
            this.btnBarcodePrint.Click += new System.EventHandler(this.btnBarcodePrint_Click);
            // 
            // rdSmall
            // 
            this.rdSmall.AutoSize = true;
            this.rdSmall.Checked = true;
            this.rdSmall.Location = new System.Drawing.Point(224, 314);
            this.rdSmall.Name = "rdSmall";
            this.rdSmall.Size = new System.Drawing.Size(50, 17);
            this.rdSmall.TabIndex = 4;
            this.rdSmall.TabStop = true;
            this.rdSmall.Text = "Small";
            this.rdSmall.UseVisualStyleBackColor = true;
            this.rdSmall.Visible = false;
            // 
            // rdBig
            // 
            this.rdBig.AutoSize = true;
            this.rdBig.Location = new System.Drawing.Point(304, 314);
            this.rdBig.Name = "rdBig";
            this.rdBig.Size = new System.Drawing.Size(40, 17);
            this.rdBig.TabIndex = 5;
            this.rdBig.Text = "Big";
            this.rdBig.UseVisualStyleBackColor = true;
            this.rdBig.Visible = false;
            // 
            // pnlItemwise
            // 
            this.pnlItemwise.Controls.Add(this.rdItemwise);
            this.pnlItemwise.Controls.Add(this.rdPurchaseItemwise);
            this.pnlItemwise.Location = new System.Drawing.Point(12, 15);
            this.pnlItemwise.Name = "pnlItemwise";
            this.pnlItemwise.Size = new System.Drawing.Size(314, 38);
            this.pnlItemwise.TabIndex = 1001;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(720, 316);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(88, 17);
            this.chkSelectAll.TabIndex = 6;
            this.chkSelectAll.Text = "SelectAll (F2)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(174, 343);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 46);
            this.btnExit.TabIndex = 1002;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnImportItems
            // 
            this.btnImportItems.Location = new System.Drawing.Point(274, 341);
            this.btnImportItems.Name = "btnImportItems";
            this.btnImportItems.Size = new System.Drawing.Size(128, 46);
            this.btnImportItems.TabIndex = 1003;
            this.btnImportItems.Text = "Import Items (F6)";
            this.btnImportItems.UseVisualStyleBackColor = true;
            this.btnImportItems.Click += new System.EventHandler(this.btnImportItems_Click);
            // 
            // OF
            // 
            this.OF.FileName = "openFileDialog1";
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblMsg);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.txtStartNo);
            this.pnlMain.Controls.Add(this.pnlItemwise);
            this.pnlMain.Controls.Add(this.btnImportItems);
            this.pnlMain.Controls.Add(this.dgvSeasonalBarcode);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnBarcodePrint);
            this.pnlMain.Controls.Add(this.chkSelectAll);
            this.pnlMain.Controls.Add(this.rdSmall);
            this.pnlMain.Controls.Add(this.rdBig);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(812, 397);
            this.pnlMain.TabIndex = 1004;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(543, 317);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(74, 13);
            this.lblMsg.TabIndex = 1005;
            this.lblMsg.Text = "Printed Qty : 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1004;
            this.label1.Text = "Start No :";
            // 
            // txtStartNo
            // 
            this.txtStartNo.Location = new System.Drawing.Point(84, 314);
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(100, 20);
            this.txtStartNo.TabIndex = 2;
            this.txtStartNo.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PKSrNo
            // 
            this.PKSrNo.DataPropertyName = "PKSrNo";
            this.PKSrNo.HeaderText = "PKSrNo";
            this.PKSrNo.Name = "PKSrNo";
            this.PKSrNo.ReadOnly = true;
            this.PKSrNo.Visible = false;
            // 
            // BillNo
            // 
            this.BillNo.DataPropertyName = "BillNo";
            this.BillNo.HeaderText = "BillNo";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            this.BillNo.Width = 60;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 300;
            // 
            // MRP
            // 
            this.MRP.DataPropertyName = "MRP";
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            this.MRP.Width = 60;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 60;
            // 
            // ActualQty
            // 
            this.ActualQty.DataPropertyName = "ActQty";
            this.ActualQty.HeaderText = "Act Qty";
            this.ActualQty.Name = "ActualQty";
            this.ActualQty.Width = 70;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Barcode";
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Width = 130;
            // 
            // IsPrint
            // 
            this.IsPrint.DataPropertyName = "IsPrint";
            this.IsPrint.HeaderText = "IsPrint";
            this.IsPrint.Name = "IsPrint";
            this.IsPrint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsPrint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsPrint.Width = 60;
            // 
            // SeasonalBarcodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 421);
            this.Controls.Add(this.pnlMain);
            this.Name = "SeasonalBarcodePrint";
            this.Text = "Seasonal Barcode Print";
            this.Load += new System.EventHandler(this.SeasonalBarcodePrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeasonalBarcode)).EndInit();
            this.pnlItemwise.ResumeLayout(false);
            this.pnlItemwise.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdItemwise;
        private System.Windows.Forms.RadioButton rdPurchaseItemwise;
        private System.Windows.Forms.DataGridView dgvSeasonalBarcode;
        private System.Windows.Forms.Button btnBarcodePrint;
        private System.Windows.Forms.RadioButton rdSmall;
        private System.Windows.Forms.RadioButton rdBig;
        private System.Windows.Forms.Panel pnlItemwise;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnImportItems;
        private System.Windows.Forms.OpenFileDialog OF;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartNo;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn PKSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrint;

    }
}