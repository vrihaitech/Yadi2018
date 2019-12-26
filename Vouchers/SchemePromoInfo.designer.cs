namespace Yadi.Vouchers
{
    partial class SchemePromoInfo
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new OMControls.OMBPanel();
            this.dgInsTSKU = new System.Windows.Forms.DataGridView();
            this.rType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchemeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchUserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchTypeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchperFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchPerTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDiscAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SchDtlspKSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOmNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BUomName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnDtails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSKUCLoyaltyFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsIWScheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValPromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnContSave = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgPromo = new System.Windows.Forms.DataGridView();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValSchemeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInsTSKU)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPromo)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(707, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "TSKU and TSKUC Schemes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderColor = System.Drawing.Color.Gray;
            this.panel2.BorderRadius = 3;
            this.panel2.Controls.Add(this.dgInsTSKU);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(251, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(706, 312);
            this.panel2.TabIndex = 3;
            // 
            // dgInsTSKU
            // 
            this.dgInsTSKU.AllowUserToAddRows = false;
            this.dgInsTSKU.AllowUserToDeleteRows = false;
            this.dgInsTSKU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInsTSKU.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rType,
            this.SchemeNo,
            this.SchUserNo,
            this.SchName,
            this.ItemName,
            this.SchTypeNo,
            this.SchDate,
            this.SchperFrom,
            this.SchPerTo,
            this.DiscAmt,
            this.PDiscAmt,
            this.SchDtlspKSrNo,
            this.ItemNo,
            this.Qty,
            this.UomName,
            this.UOmNo,
            this.BillQty,
            this.BUomName,
            this.BtnDtails,
            this.select,
            this.chk,
            this.MRP,
            this.TSKUCLoyaltyFactor,
            this.MainPromoCode,
            this.IsIWScheme,
            this.ValPromoCode});
            this.dgInsTSKU.Location = new System.Drawing.Point(3, 24);
            this.dgInsTSKU.Name = "dgInsTSKU";
            this.dgInsTSKU.Size = new System.Drawing.Size(700, 285);
            this.dgInsTSKU.TabIndex = 3;
            this.dgInsTSKU.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgInsTSKU_CellPainting);
            this.dgInsTSKU.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInsTSKU_CellClick);
            this.dgInsTSKU.Click += new System.EventHandler(this.dgInsTSKU_Click);
            // 
            // rType
            // 
            this.rType.HeaderText = "rType";
            this.rType.Name = "rType";
            this.rType.Visible = false;
            // 
            // SchemeNo
            // 
            this.SchemeNo.HeaderText = "SchemeNo";
            this.SchemeNo.Name = "SchemeNo";
            this.SchemeNo.Visible = false;
            // 
            // SchUserNo
            // 
            this.SchUserNo.HeaderText = "Sch. No.";
            this.SchUserNo.Name = "SchUserNo";
            // 
            // SchName
            // 
            this.SchName.HeaderText = "SchemeName";
            this.SchName.Name = "SchName";
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.Name = "ItemName";
            // 
            // SchTypeNo
            // 
            this.SchTypeNo.HeaderText = "SchTypeNo";
            this.SchTypeNo.Name = "SchTypeNo";
            this.SchTypeNo.Visible = false;
            // 
            // SchDate
            // 
            this.SchDate.HeaderText = "SchDate";
            this.SchDate.Name = "SchDate";
            this.SchDate.Visible = false;
            // 
            // SchperFrom
            // 
            this.SchperFrom.HeaderText = "SchperFrom";
            this.SchperFrom.Name = "SchperFrom";
            this.SchperFrom.Visible = false;
            // 
            // SchPerTo
            // 
            this.SchPerTo.HeaderText = "SchPerTo";
            this.SchPerTo.Name = "SchPerTo";
            this.SchPerTo.Visible = false;
            // 
            // DiscAmt
            // 
            this.DiscAmt.HeaderText = "DiscAmt";
            this.DiscAmt.Name = "DiscAmt";
            this.DiscAmt.Visible = false;
            // 
            // PDiscAmt
            // 
            this.PDiscAmt.HeaderText = "PDiscAmt";
            this.PDiscAmt.Name = "PDiscAmt";
            this.PDiscAmt.Visible = false;
            // 
            // SchDtlspKSrNo
            // 
            this.SchDtlspKSrNo.HeaderText = "SchDtlspKSrNo";
            this.SchDtlspKSrNo.Name = "SchDtlspKSrNo";
            this.SchDtlspKSrNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // Qty
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 70;
            // 
            // UomName
            // 
            this.UomName.HeaderText = "Uom";
            this.UomName.Name = "UomName";
            this.UomName.Width = 70;
            // 
            // UOmNo
            // 
            this.UOmNo.HeaderText = "UOmNo";
            this.UOmNo.Name = "UOmNo";
            this.UOmNo.Visible = false;
            // 
            // BillQty
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BillQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.BillQty.HeaderText = "BillQty";
            this.BillQty.Name = "BillQty";
            this.BillQty.ReadOnly = true;
            this.BillQty.Width = 70;
            // 
            // BUomName
            // 
            this.BUomName.HeaderText = "Uom";
            this.BUomName.Name = "BUomName";
            this.BUomName.ReadOnly = true;
            this.BUomName.Visible = false;
            this.BUomName.Width = 70;
            // 
            // BtnDtails
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnDtails.DefaultCellStyle = dataGridViewCellStyle3;
            this.BtnDtails.HeaderText = ".";
            this.BtnDtails.Name = "BtnDtails";
            this.BtnDtails.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BtnDtails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BtnDtails.Visible = false;
            this.BtnDtails.Width = 50;
            // 
            // select
            // 
            this.select.HeaderText = " ";
            this.select.Name = "select";
            this.select.ReadOnly = true;
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.select.Visible = false;
            this.select.Width = 40;
            // 
            // chk
            // 
            this.chk.HeaderText = "chk";
            this.chk.Name = "chk";
            this.chk.Visible = false;
            // 
            // MRP
            // 
            this.MRP.DataPropertyName = "MRP";
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            this.MRP.Visible = false;
            // 
            // TSKUCLoyaltyFactor
            // 
            this.TSKUCLoyaltyFactor.HeaderText = "LoyaltyFactor";
            this.TSKUCLoyaltyFactor.Name = "TSKUCLoyaltyFactor";
            this.TSKUCLoyaltyFactor.Visible = false;
            // 
            // MainPromoCode
            // 
            this.MainPromoCode.HeaderText = "Main Promo";
            this.MainPromoCode.Name = "MainPromoCode";
            this.MainPromoCode.Visible = false;
            // 
            // IsIWScheme
            // 
            this.IsIWScheme.HeaderText = "IsIWScheme";
            this.IsIWScheme.Name = "IsIWScheme";
            this.IsIWScheme.Visible = false;
            // 
            // ValPromoCode
            // 
            this.ValPromoCode.HeaderText = "Promo Code";
            this.ValPromoCode.Name = "ValPromoCode";
            this.ValPromoCode.ReadOnly = true;
            // 
            // btnContSave
            // 
            this.btnContSave.Location = new System.Drawing.Point(838, 327);
            this.btnContSave.Name = "btnContSave";
            this.btnContSave.Size = new System.Drawing.Size(116, 37);
            this.btnContSave.TabIndex = 1;
            this.btnContSave.Text = "Continue (F6)";
            this.btnContSave.UseVisualStyleBackColor = true;
            this.btnContSave.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(98, 327);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 37);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel (F5)";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.lblChkHelp);
            this.pnlMain.Controls.Add(this.txtPromoCode);
            this.pnlMain.Controls.Add(this.btnContSave);
            this.pnlMain.Controls.Add(this.btnApply);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.BtnCancel);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Location = new System.Drawing.Point(7, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(967, 375);
            this.pnlMain.TabIndex = 101;
            // 
            // lblChkHelp
            // 
            this.lblChkHelp.AutoSize = true;
            this.lblChkHelp.Location = new System.Drawing.Point(251, 327);
            this.lblChkHelp.Name = "lblChkHelp";
            this.lblChkHelp.Size = new System.Drawing.Size(147, 13);
            this.lblChkHelp.TabIndex = 5;
            this.lblChkHelp.Text = "Promo Code input compulsary";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Promo :";
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Location = new System.Drawing.Point(68, 22);
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(147, 20);
            this.txtPromoCode.TabIndex = 0;
            this.txtPromoCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPromoCode_KeyDown);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(10, 327);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 37);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply (F4)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgPromo);
            this.panel1.Location = new System.Drawing.Point(10, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 275);
            this.panel1.TabIndex = 1;
            // 
            // dgPromo
            // 
            this.dgPromo.AllowUserToAddRows = false;
            this.dgPromo.AllowUserToDeleteRows = false;
            this.dgPromo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPromo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.PromoCode,
            this.ValSchemeNo});
            this.dgPromo.Location = new System.Drawing.Point(7, 5);
            this.dgPromo.Name = "dgPromo";
            this.dgPromo.Size = new System.Drawing.Size(214, 261);
            this.dgPromo.TabIndex = 2;
            this.dgPromo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPromo_CellDoubleClick);
            this.dgPromo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPromo_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.Width = 40;
            // 
            // PromoCode
            // 
            this.PromoCode.HeaderText = "Promo Code";
            this.PromoCode.Name = "PromoCode";
            this.PromoCode.ReadOnly = true;
            // 
            // ValSchemeNo
            // 
            this.ValSchemeNo.HeaderText = "SchemeNo";
            this.ValSchemeNo.Name = "ValSchemeNo";
            this.ValSchemeNo.Visible = false;
            // 
            // SchemePromoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 386);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SchemePromoInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Scheme";
            this.Load += new System.EventHandler(this.Scheme_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInsTSKU)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPromo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private OMControls.OMBPanel panel2;
        private System.Windows.Forms.Button btnContSave;
        private System.Windows.Forms.Button BtnCancel;
        private OMControls.OMBPanel pnlMain;
        public System.Windows.Forms.DataGridView dgInsTSKU;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dgPromo;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromoCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValSchemeNo;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn rType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchemeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchUserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchTypeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchperFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchPerTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDiscAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchDtlspKSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UomName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOmNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BUomName;
        private System.Windows.Forms.DataGridViewButtonColumn BtnDtails;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSKUCLoyaltyFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn MainPromoCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsIWScheme;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValPromoCode;
        private System.Windows.Forms.Label lblChkHelp;
    }
}