namespace Yadi.Settings
{
    partial class GodownSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblChkHelpNew = new System.Windows.Forms.Label();
            this.pnlBrandItem = new OMControls.OMBPanel();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.lstLocation = new System.Windows.Forms.ListBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.btnItemCancel = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.dgBrand = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Uom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockGroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandDiscNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TUomNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgItemMaster = new System.Windows.Forms.DataGridView();
            this.ISrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDiscPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FkBcdNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.panel1 = new OMControls.OMBPanel();
            this.CmbBrand = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlBrandItem.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemMaster)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlBrandItem);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Location = new System.Drawing.Point(2, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(941, 453);
            this.pnlMain.TabIndex = 0;
            // 
            // lblChkHelpNew
            // 
            this.lblChkHelpNew.Location = new System.Drawing.Point(771, 217);
            this.lblChkHelpNew.Name = "lblChkHelpNew";
            this.lblChkHelpNew.Size = new System.Drawing.Size(138, 23);
            this.lblChkHelpNew.TabIndex = 60054;
            this.lblChkHelpNew.Text = "New Row -Press F3.";
            // 
            // pnlBrandItem
            // 
            this.pnlBrandItem.BorderColor = System.Drawing.Color.Gray;
            this.pnlBrandItem.BorderRadius = 3;
            this.pnlBrandItem.Controls.Add(this.lblChkHelpNew);
            this.pnlBrandItem.Controls.Add(this.pnlLocation);
            this.pnlBrandItem.Controls.Add(this.lblWait);
            this.pnlBrandItem.Controls.Add(this.lblChkHelp);
            this.pnlBrandItem.Controls.Add(this.btnItemCancel);
            this.pnlBrandItem.Controls.Add(this.lblMsg);
            this.pnlBrandItem.Controls.Add(this.dgBrand);
            this.pnlBrandItem.Controls.Add(this.dgItemMaster);
            this.pnlBrandItem.Controls.Add(this.btnApply);
            this.pnlBrandItem.Location = new System.Drawing.Point(14, 98);
            this.pnlBrandItem.Name = "pnlBrandItem";
            this.pnlBrandItem.Size = new System.Drawing.Size(916, 269);
            this.pnlBrandItem.TabIndex = 60046;
            // 
            // pnlLocation
            // 
            this.pnlLocation.BackColor = System.Drawing.Color.Bisque;
            this.pnlLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocation.Controls.Add(this.lstLocation);
            this.pnlLocation.Location = new System.Drawing.Point(767, 49);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(118, 117);
            this.pnlLocation.TabIndex = 60053;
            this.pnlLocation.Visible = false;
            // 
            // lstLocation
            // 
            this.lstLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLocation.FormattingEnabled = true;
            this.lstLocation.Location = new System.Drawing.Point(8, 8);
            this.lstLocation.Name = "lstLocation";
            this.lstLocation.Size = new System.Drawing.Size(101, 93);
            this.lstLocation.TabIndex = 516;
            this.lstLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLocation_KeyDown);
            // 
            // lblWait
            // 
            this.lblWait.Location = new System.Drawing.Point(11, 235);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(454, 28);
            this.lblWait.TabIndex = 60050;
            this.lblWait.Text = "label8";
            this.lblWait.Visible = false;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.Location = new System.Drawing.Point(9, 217);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(345, 23);
            this.lblChkHelp.TabIndex = 60049;
            this.lblChkHelp.Text = "Save - Press Esc.   Show Item Details-Press F4 / Double Click.";
            // 
            // btnItemCancel
            // 
            this.btnItemCancel.Enabled = false;
            this.btnItemCancel.Location = new System.Drawing.Point(674, 217);
            this.btnItemCancel.Name = "btnItemCancel";
            this.btnItemCancel.Size = new System.Drawing.Size(91, 40);
            this.btnItemCancel.TabIndex = 60048;
            this.btnItemCancel.Text = "Cancel (F6)";
            this.btnItemCancel.UseVisualStyleBackColor = true;
            this.btnItemCancel.Click += new System.EventHandler(this.btnItemCancel_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblMsg.Location = new System.Drawing.Point(252, 81);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(481, 52);
            this.lblMsg.TabIndex = 60047;
            this.lblMsg.Text = "label4";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Visible = false;
            // 
            // dgBrand
            // 
            this.dgBrand.AllowUserToAddRows = false;
            this.dgBrand.AllowUserToDeleteRows = false;
            this.dgBrand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBrand.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.BrandName,
            this.DiscPerc,
            this.Uom,
            this.StockGroupNo,
            this.BrandDiscNo,
            this.TUomNo});
            this.dgBrand.Location = new System.Drawing.Point(9, 13);
            this.dgBrand.Name = "dgBrand";
            this.dgBrand.Size = new System.Drawing.Size(497, 198);
            this.dgBrand.TabIndex = 4;
            this.dgBrand.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBrand_CellDoubleClick);
            this.dgBrand.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBrand_CellFormatting);
           
            this.dgBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBrand_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.DataPropertyName = "SrNo";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SrNo.DefaultCellStyle = dataGridViewCellStyle7;
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // BrandName
            // 
            this.BrandName.DataPropertyName = "ItemName";
            this.BrandName.HeaderText = "Item Name";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            this.BrandName.Width = 200;
            // 
            // DiscPerc
            // 
            this.DiscPerc.DataPropertyName = "Barcode";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DiscPerc.DefaultCellStyle = dataGridViewCellStyle8;
            this.DiscPerc.HeaderText = "Barcode";
            this.DiscPerc.Name = "DiscPerc";
            this.DiscPerc.Width = 130;
            // 
            // Uom
            // 
            this.Uom.DataPropertyName = "UOMName";
            this.Uom.HeaderText = "Uom";
            this.Uom.Name = "Uom";
            this.Uom.ReadOnly = true;
            this.Uom.Width = 70;
            // 
            // StockGroupNo
            // 
            this.StockGroupNo.DataPropertyName = "ItemNo";
            this.StockGroupNo.HeaderText = "ItemNo";
            this.StockGroupNo.Name = "StockGroupNo";
            this.StockGroupNo.ReadOnly = true;
            this.StockGroupNo.Visible = false;
            // 
            // BrandDiscNo
            // 
            this.BrandDiscNo.DataPropertyName = "PkStockBarcodeNo";
            this.BrandDiscNo.HeaderText = "BarcodeNo";
            this.BrandDiscNo.Name = "BrandDiscNo";
            this.BrandDiscNo.ReadOnly = true;
            this.BrandDiscNo.Visible = false;
            // 
            // TUomNo
            // 
            this.TUomNo.DataPropertyName = "UOMNo";
            this.TUomNo.HeaderText = "UomNo";
            this.TUomNo.Name = "TUomNo";
            this.TUomNo.Visible = false;
            // 
            // dgItemMaster
            // 
            this.dgItemMaster.AllowUserToAddRows = false;
            this.dgItemMaster.AllowUserToDeleteRows = false;
            this.dgItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ISrNo,
            this.ItemName,
            this.MRP,
            this.IDiscPerc,
            this.dataGridViewTextBoxColumn4,
            this.PkSrNo,
            this.ItemNo,
            this.FkBcdNo,
            this.UomNo});
            this.dgItemMaster.Location = new System.Drawing.Point(512, 13);
            this.dgItemMaster.Name = "dgItemMaster";
            this.dgItemMaster.Size = new System.Drawing.Size(394, 198);
            this.dgItemMaster.TabIndex = 5;
            this.dgItemMaster.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgItemMaster_CellFormatting);
            this.dgItemMaster.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItemMaster_CellEndEdit);
            this.dgItemMaster.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItemMaster_EditingControlShowing);
            this.dgItemMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItemMaster_KeyDown);
            // 
            // ISrNo
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ISrNo.DefaultCellStyle = dataGridViewCellStyle9;
            this.ISrNo.HeaderText = "SrNo";
            this.ISrNo.Name = "ISrNo";
            this.ISrNo.ReadOnly = true;
            this.ISrNo.Width = 50;
            // 
            // ItemName
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle10;
            this.ItemName.HeaderText = "FromSlab";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 80;
            // 
            // MRP
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MRP.DefaultCellStyle = dataGridViewCellStyle11;
            this.MRP.HeaderText = "ToSlab";
            this.MRP.Name = "MRP";
            this.MRP.Width = 80;
            // 
            // IDiscPerc
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDiscPerc.DefaultCellStyle = dataGridViewCellStyle12;
            this.IDiscPerc.HeaderText = "Location";
            this.IDiscPerc.Name = "IDiscPerc";
            this.IDiscPerc.ReadOnly = true;
            this.IDiscPerc.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "LocationNo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "settingNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.DataPropertyName = "ItemNo";
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // FkBcdNo
            // 
            this.FkBcdNo.DataPropertyName = "FkBcdNo";
            this.FkBcdNo.HeaderText = "FkBcdNo";
            this.FkBcdNo.Name = "FkBcdNo";
            this.FkBcdNo.Visible = false;
            // 
            // UomNo
            // 
            this.UomNo.DataPropertyName = "UomNo";
            this.UomNo.HeaderText = "UomNo";
            this.UomNo.Name = "UomNo";
            this.UomNo.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(514, 217);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(145, 40);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply Changes  (F5)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(277, 384);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(97, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(181, 384);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 13;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.BorderRadius = 3;
            this.panel1.Controls.Add(this.CmbBrand);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(14, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 44);
            this.panel1.TabIndex = 0;
            // 
            // CmbBrand
            // 
            this.CmbBrand.FormattingEnabled = true;
            this.CmbBrand.Location = new System.Drawing.Point(130, 11);
            this.CmbBrand.Name = "CmbBrand";
            this.CmbBrand.Size = new System.Drawing.Size(321, 21);
            this.CmbBrand.TabIndex = 0;
            this.CmbBrand.Leave += new System.EventHandler(this.CmbBrand_Leave);
            this.CmbBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbBrand_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 10017;
            this.label2.Text = "Brand Name :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1002, 26);
            this.label1.TabIndex = 60029;
            this.label1.Text = "Godown Setting";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(15, 384);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // GodownSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 462);
            this.Controls.Add(this.pnlMain);
            this.Name = "GodownSetting";
            this.Text = "Godown Setting";
            this.Load += new System.EventHandler(this.GodownSetting_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlBrandItem.ResumeLayout(false);
            this.pnlLocation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemMaster)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        private OMControls.OMBPanel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgBrand;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgItemMaster;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button BtnExit;
        private OMControls.OMBPanel pnlBrandItem;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnItemCancel;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.ComboBox CmbBrand;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.ListBox lstLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDiscPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkBcdNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomNo;
        private System.Windows.Forms.Label lblChkHelpNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Uom;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockGroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandDiscNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TUomNo;
        private System.Windows.Forms.Button btnNew;
    }
}