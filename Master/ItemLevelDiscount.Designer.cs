namespace Yadi.Master
{
    partial class ItemLevelDiscount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new OMControls.OMBPanel();
            this.pnlBrandItem = new OMControls.OMBPanel();
            this.chkItemLevel = new System.Windows.Forms.CheckBox();
            this.chkBrand = new System.Windows.Forms.CheckBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblChkHelp = new System.Windows.Forms.Label();
            this.btnItemCancel = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.dgBrand = new System.Windows.Forms.DataGridView();
            this.dgItemMaster = new System.Windows.Forms.DataGridView();
            this.ISrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDiscPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FkRateSetting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApply = new System.Windows.Forms.Button();
            this.pnlActive = new OMControls.OMBPanel();
            this.lblSchemeStatus = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1 = new OMControls.OMBPanel();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMfgCompany = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpDiscountDateTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.DtpDiscountDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtDiscUserNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StockGroupNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandDiscNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.pnlBrandItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemMaster)).BeginInit();
            this.pnlActive.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.pnlBrandItem);
            this.pnlMain.Controls.Add(this.pnlActive);
            this.pnlMain.Controls.Add(this.btnNext);
            this.pnlMain.Controls.Add(this.btnPrev);
            this.pnlMain.Controls.Add(this.btnLast);
            this.pnlMain.Controls.Add(this.btnFirst);
            this.pnlMain.Controls.Add(this.btnSearch);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.btnNew);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnView);
            this.pnlMain.Location = new System.Drawing.Point(2, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1004, 531);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlBrandItem
            // 
            this.pnlBrandItem.BorderColor = System.Drawing.Color.Gray;
            this.pnlBrandItem.BorderRadius = 3;
            this.pnlBrandItem.Controls.Add(this.chkItemLevel);
            this.pnlBrandItem.Controls.Add(this.chkBrand);
            this.pnlBrandItem.Controls.Add(this.lblWait);
            this.pnlBrandItem.Controls.Add(this.lblChkHelp);
            this.pnlBrandItem.Controls.Add(this.btnItemCancel);
            this.pnlBrandItem.Controls.Add(this.lblMsg);
            this.pnlBrandItem.Controls.Add(this.dgBrand);
            this.pnlBrandItem.Controls.Add(this.dgItemMaster);
            this.pnlBrandItem.Controls.Add(this.btnApply);
            this.pnlBrandItem.Location = new System.Drawing.Point(14, 169);
            this.pnlBrandItem.Name = "pnlBrandItem";
            this.pnlBrandItem.Size = new System.Drawing.Size(964, 268);
            this.pnlBrandItem.TabIndex = 60046;
            // 
            // chkItemLevel
            // 
            this.chkItemLevel.AutoSize = true;
            this.chkItemLevel.Location = new System.Drawing.Point(859, 217);
            this.chkItemLevel.Name = "chkItemLevel";
            this.chkItemLevel.Size = new System.Drawing.Size(91, 17);
            this.chkItemLevel.TabIndex = 60052;
            this.chkItemLevel.Text = "(F8) Select All";
            this.chkItemLevel.UseVisualStyleBackColor = true;
            this.chkItemLevel.CheckedChanged += new System.EventHandler(this.chkItemLevel_CheckedChanged);
            // 
            // chkBrand
            // 
            this.chkBrand.AutoSize = true;
            this.chkBrand.Location = new System.Drawing.Point(360, 217);
            this.chkBrand.Name = "chkBrand";
            this.chkBrand.Size = new System.Drawing.Size(91, 17);
            this.chkBrand.TabIndex = 60051;
            this.chkBrand.Text = "Select All (F7)";
            this.chkBrand.UseVisualStyleBackColor = true;
            this.chkBrand.CheckedChanged += new System.EventHandler(this.chkBrand_CheckedChanged);
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
            this.btnItemCancel.Location = new System.Drawing.Point(632, 217);
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
            this.BSelect,
            this.StockGroupNo,
            this.BrandDiscNo});
            this.dgBrand.Location = new System.Drawing.Point(9, 13);
            this.dgBrand.Name = "dgBrand";
            this.dgBrand.Size = new System.Drawing.Size(441, 198);
            this.dgBrand.TabIndex = 4;
            this.dgBrand.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBrand_CellDoubleClick);
            this.dgBrand.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgBrand_CellFormatting);
            this.dgBrand.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBrand_CellEndEdit);
            this.dgBrand.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgBrand_EditingControlShowing);
            this.dgBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBrand_KeyDown);
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
            this.ItemSelect,
            this.dataGridViewTextBoxColumn4,
            this.PkSrNo,
            this.ItemNo,
            this.FkRateSetting});
            this.dgItemMaster.Location = new System.Drawing.Point(472, 13);
            this.dgItemMaster.Name = "dgItemMaster";
            this.dgItemMaster.Size = new System.Drawing.Size(479, 198);
            this.dgItemMaster.TabIndex = 5;
            this.dgItemMaster.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgItemMaster_CellFormatting);
            this.dgItemMaster.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItemMaster_CellEndEdit);
            this.dgItemMaster.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItemMaster_EditingControlShowing);
            this.dgItemMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItemMaster_KeyDown);
            // 
            // ISrNo
            // 
            this.ISrNo.HeaderText = "SrNo";
            this.ISrNo.Name = "ISrNo";
            this.ISrNo.ReadOnly = true;
            this.ISrNo.Width = 50;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 200;
            // 
            // MRP
            // 
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            this.MRP.Width = 60;
            // 
            // IDiscPerc
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IDiscPerc.DefaultCellStyle = dataGridViewCellStyle2;
            this.IDiscPerc.HeaderText = "Disc %";
            this.IDiscPerc.Name = "IDiscPerc";
            this.IDiscPerc.Width = 70;
            // 
            // ItemSelect
            // 
            this.ItemSelect.HeaderText = "Select";
            this.ItemSelect.Name = "ItemSelect";
            this.ItemSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ItemSelect.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "StockGroupNo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // ItemNo
            // 
            this.ItemNo.HeaderText = "ItemNo";
            this.ItemNo.Name = "ItemNo";
            this.ItemNo.Visible = false;
            // 
            // FkRateSetting
            // 
            this.FkRateSetting.HeaderText = "FkRateSetting";
            this.FkRateSetting.Name = "FkRateSetting";
            this.FkRateSetting.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(472, 217);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(145, 40);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply Changes  (F5)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pnlActive
            // 
            this.pnlActive.BorderColor = System.Drawing.Color.Gray;
            this.pnlActive.BorderRadius = 3;
            this.pnlActive.Controls.Add(this.lblSchemeStatus);
            this.pnlActive.Location = new System.Drawing.Point(358, 444);
            this.pnlActive.Name = "pnlActive";
            this.pnlActive.Size = new System.Drawing.Size(164, 59);
            this.pnlActive.TabIndex = 60045;
            // 
            // lblSchemeStatus
            // 
            this.lblSchemeStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSchemeStatus.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchemeStatus.Location = new System.Drawing.Point(3, 12);
            this.lblSchemeStatus.Name = "lblSchemeStatus";
            this.lblSchemeStatus.Size = new System.Drawing.Size(158, 34);
            this.lblSchemeStatus.TabIndex = 10031;
            this.lblSchemeStatus.Text = "Active";
            this.lblSchemeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSchemeStatus.Click += new System.EventHandler(this.lblSchemeStatus_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(614, 477);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 27);
            this.btnNext.TabIndex = 16;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(571, 477);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 27);
            this.btnPrev.TabIndex = 15;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(657, 477);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(37, 27);
            this.btnLast.TabIndex = 17;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(528, 477);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(37, 27);
            this.btnFirst.TabIndex = 14;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(700, 444);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 60);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(17, 443);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(17, 444);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 60);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(100, 443);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(266, 444);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 13;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(183, 444);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 60);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(100, 444);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 60);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.VisibleChanged += new System.EventHandler(this.btnUpdate_VisibleChanged);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.BorderRadius = 3;
            this.panel1.Controls.Add(this.txtDiscount);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbMfgCompany);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.DtpDiscountDateTo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.DtpDiscountDateFrom);
            this.panel1.Controls.Add(this.txtDiscUserNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.DtpDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(14, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 115);
            this.panel1.TabIndex = 60030;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDiscount.Location = new System.Drawing.Point(588, 76);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(115, 20);
            this.txtDiscount.TabIndex = 3;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscount_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(480, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 10019;
            this.label7.Text = "Discount % :";
            // 
            // cmbMfgCompany
            // 
            this.cmbMfgCompany.FormattingEnabled = true;
            this.cmbMfgCompany.Location = new System.Drawing.Point(129, 39);
            this.cmbMfgCompany.Name = "cmbMfgCompany";
            this.cmbMfgCompany.Size = new System.Drawing.Size(321, 21);
            this.cmbMfgCompany.TabIndex = 0;
            this.cmbMfgCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMfgCompany_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 10017;
            this.label2.Text = "Company Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 10010;
            this.label4.Text = "Doc No.";
            // 
            // DtpDiscountDateTo
            // 
            this.DtpDiscountDateTo.Location = new System.Drawing.Point(335, 70);
            this.DtpDiscountDateTo.Name = "DtpDiscountDateTo";
            this.DtpDiscountDateTo.Size = new System.Drawing.Size(115, 20);
            this.DtpDiscountDateTo.TabIndex = 2;
            this.DtpDiscountDateTo.ValueChanged += new System.EventHandler(this.DtpDiscountDateFrom_ValueChanged);
            this.DtpDiscountDateTo.Leave += new System.EventHandler(this.DtpDiscountDateTo_Leave);
            this.DtpDiscountDateTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDiscountDateTo_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(480, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10011;
            this.label5.Text = "Date :";
            // 
            // DtpDiscountDateFrom
            // 
            this.DtpDiscountDateFrom.Location = new System.Drawing.Point(129, 70);
            this.DtpDiscountDateFrom.Name = "DtpDiscountDateFrom";
            this.DtpDiscountDateFrom.Size = new System.Drawing.Size(115, 20);
            this.DtpDiscountDateFrom.TabIndex = 1;
            this.DtpDiscountDateFrom.ValueChanged += new System.EventHandler(this.DtpDiscountDateFrom_ValueChanged);
            this.DtpDiscountDateFrom.Leave += new System.EventHandler(this.DtpDiscountDateFrom_Leave);
            this.DtpDiscountDateFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DtpDiscountDateFrom_KeyDown);
            // 
            // txtDiscUserNo
            // 
            this.txtDiscUserNo.Enabled = false;
            this.txtDiscUserNo.Location = new System.Drawing.Point(128, 7);
            this.txtDiscUserNo.Name = "txtDiscUserNo";
            this.txtDiscUserNo.ReadOnly = true;
            this.txtDiscUserNo.Size = new System.Drawing.Size(113, 20);
            this.txtDiscUserNo.TabIndex = 10012;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 10016;
            this.label6.Text = "To";
            // 
            // DtpDate
            // 
            this.DtpDate.Location = new System.Drawing.Point(588, 36);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(115, 20);
            this.DtpDate.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 10015;
            this.label3.Text = "Period From :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1002, 26);
            this.label1.TabIndex = 60029;
            this.label1.Text = "Item Level Discount";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(100, 443);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(80, 60);
            this.btnView.TabIndex = 60047;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 60;
            // 
            // BrandName
            // 
            this.BrandName.HeaderText = "Brand Name";
            this.BrandName.Name = "BrandName";
            this.BrandName.ReadOnly = true;
            this.BrandName.Width = 200;
            // 
            // DiscPerc
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscPerc.DefaultCellStyle = dataGridViewCellStyle1;
            this.DiscPerc.HeaderText = "Disc %";
            this.DiscPerc.Name = "DiscPerc";
            this.DiscPerc.Width = 80;
            // 
            // BSelect
            // 
            this.BSelect.FalseValue = "false";
            this.BSelect.HeaderText = "Select";
            this.BSelect.Name = "BSelect";
            this.BSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.BSelect.TrueValue = "true";
            this.BSelect.Width = 60;
            // 
            // StockGroupNo
            // 
            this.StockGroupNo.HeaderText = "StockGroupNo";
            this.StockGroupNo.Name = "StockGroupNo";
            this.StockGroupNo.ReadOnly = true;
            this.StockGroupNo.Visible = false;
            // 
            // BrandDiscNo
            // 
            this.BrandDiscNo.HeaderText = "BrandDiscNo";
            this.BrandDiscNo.Name = "BrandDiscNo";
            this.BrandDiscNo.Visible = false;
            // 
            // ItemLevelDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 544);
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemLevelDiscount";
            this.Text = "Item Level Discount";
            this.Load += new System.EventHandler(this.ItemLevelDiscount_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlBrandItem.ResumeLayout(false);
            this.pnlBrandItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemMaster)).EndInit();
            this.pnlActive.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label1;
        private OMControls.OMBPanel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpDiscountDateTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker DtpDiscountDateFrom;
        private System.Windows.Forms.TextBox txtDiscUserNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbMfgCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.DataGridView dgBrand;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgItemMaster;
        private OMControls.OMBPanel pnlActive;
        private System.Windows.Forms.Label lblSchemeStatus;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private OMControls.OMBPanel pnlBrandItem;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnItemCancel;
        private System.Windows.Forms.Label lblChkHelp;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.CheckBox chkBrand;
        private System.Windows.Forms.CheckBox chkItemLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDiscPerc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ItemSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FkRateSetting;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPerc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockGroupNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandDiscNo;
    }
}