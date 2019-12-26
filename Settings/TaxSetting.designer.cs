namespace Yadi.Settings
{
    partial class TaxSetting
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
            this.txtTotSalePercentage = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSaleTaxSettingNo = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlTotalSale = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpSaleFrmDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSaleCalcMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSaleTax = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btndelete = new System.Windows.Forms.Button();
            this.pnlItemRate = new System.Windows.Forms.Panel();
            this.cmbSalesAccount = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbTextType = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpItemFrmDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbItemCalcMethod = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbItemTax = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtItemPercentage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlOtherTaxes = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbLedgerTax = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpOtherTaxFrmDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbOtherTaxCalcMethod = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbOtherTax = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOtherTaxPercentage = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlGrandTotal = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpGrandTotFrmDate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbGrandTotCalcMethod = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbGrandTotTax = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtGrandTotPercentage = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.rdTotalSale = new System.Windows.Forms.RadioButton();
            this.rdItemRate = new System.Windows.Forms.RadioButton();
            this.rdOtherTaxes = new System.Windows.Forms.RadioButton();
            this.rdGrandTotal = new System.Windows.Forms.RadioButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.DGPanel = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlTotalSale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnlItemRate.SuspendLayout();
            this.pnlOtherTaxes.SuspendLayout();
            this.pnlGrandTotal.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.DGPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTotSalePercentage
            // 
            this.txtTotSalePercentage.Location = new System.Drawing.Point(143, 137);
            this.txtTotSalePercentage.MaxLength = 10;
            this.txtTotSalePercentage.Name = "txtTotSalePercentage";
            this.txtTotSalePercentage.Size = new System.Drawing.Size(169, 20);
            this.txtTotSalePercentage.TabIndex = 12;
            this.txtTotSalePercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotSalePercentage_KeyPress);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(96, 656);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 23);
            this.btnExit.TabIndex = 202;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(17, 656);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 201;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 604;
            this.label2.Text = "Percentage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 605;
            this.label1.Text = "Tax Setting No";
            // 
            // txtSaleTaxSettingNo
            // 
            this.txtSaleTaxSettingNo.Location = new System.Drawing.Point(191, 172);
            this.txtSaleTaxSettingNo.Name = "txtSaleTaxSettingNo";
            this.txtSaleTaxSettingNo.Size = new System.Drawing.Size(25, 20);
            this.txtSaleTaxSettingNo.TabIndex = 13;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // pnlTotalSale
            // 
            this.pnlTotalSale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTotalSale.Controls.Add(this.label20);
            this.pnlTotalSale.Controls.Add(this.dtpSaleFrmDate);
            this.pnlTotalSale.Controls.Add(this.label3);
            this.pnlTotalSale.Controls.Add(this.cmbSaleCalcMethod);
            this.pnlTotalSale.Controls.Add(this.label5);
            this.pnlTotalSale.Controls.Add(this.cmbSaleTax);
            this.pnlTotalSale.Controls.Add(this.label4);
            this.pnlTotalSale.Controls.Add(this.txtSaleTaxSettingNo);
            this.pnlTotalSale.Controls.Add(this.label1);
            this.pnlTotalSale.Controls.Add(this.txtTotSalePercentage);
            this.pnlTotalSale.Controls.Add(this.label2);
            this.pnlTotalSale.Location = new System.Drawing.Point(370, 68);
            this.pnlTotalSale.Name = "pnlTotalSale";
            this.pnlTotalSale.Size = new System.Drawing.Size(329, 197);
            this.pnlTotalSale.TabIndex = 9;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Lavender;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(325, 20);
            this.label20.TabIndex = 601;
            this.label20.Text = "Tax Based On Total Sale";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpSaleFrmDate
            // 
            this.dtpSaleFrmDate.Location = new System.Drawing.Point(143, 102);
            this.dtpSaleFrmDate.Name = "dtpSaleFrmDate";
            this.dtpSaleFrmDate.Size = new System.Drawing.Size(169, 20);
            this.dtpSaleFrmDate.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 603;
            this.label3.Text = "From Date";
            // 
            // cmbSaleCalcMethod
            // 
            this.cmbSaleCalcMethod.FormattingEnabled = true;
            this.cmbSaleCalcMethod.Location = new System.Drawing.Point(143, 67);
            this.cmbSaleCalcMethod.MaxLength = 25;
            this.cmbSaleCalcMethod.Name = "cmbSaleCalcMethod";
            this.cmbSaleCalcMethod.Size = new System.Drawing.Size(169, 21);
            this.cmbSaleCalcMethod.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 602;
            this.label5.Text = "Calculation Method";
            // 
            // cmbSaleTax
            // 
            this.cmbSaleTax.FormattingEnabled = true;
            this.cmbSaleTax.Location = new System.Drawing.Point(143, 33);
            this.cmbSaleTax.MaxLength = 20;
            this.cmbSaleTax.Name = "cmbSaleTax";
            this.cmbSaleTax.Size = new System.Drawing.Size(169, 21);
            this.cmbSaleTax.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 601;
            this.label4.Text = "Select Tax";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 1);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(600, 200);
            this.dgv.TabIndex = 1000;
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_RowsAdded);
            // 
            // btndelete
            // 
            this.btndelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btndelete.Location = new System.Drawing.Point(256, 657);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(80, 23);
            this.btndelete.TabIndex = 203;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // pnlItemRate
            // 
            this.pnlItemRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlItemRate.Controls.Add(this.cmbSalesAccount);
            this.pnlItemRate.Controls.Add(this.label26);
            this.pnlItemRate.Controls.Add(this.cmbTextType);
            this.pnlItemRate.Controls.Add(this.label25);
            this.pnlItemRate.Controls.Add(this.cmbItemName);
            this.pnlItemRate.Controls.Add(this.label22);
            this.pnlItemRate.Controls.Add(this.label11);
            this.pnlItemRate.Controls.Add(this.dtpItemFrmDate);
            this.pnlItemRate.Controls.Add(this.label6);
            this.pnlItemRate.Controls.Add(this.cmbItemCalcMethod);
            this.pnlItemRate.Controls.Add(this.label7);
            this.pnlItemRate.Controls.Add(this.cmbItemTax);
            this.pnlItemRate.Controls.Add(this.label8);
            this.pnlItemRate.Controls.Add(this.txtItemPercentage);
            this.pnlItemRate.Controls.Add(this.label10);
            this.pnlItemRate.Location = new System.Drawing.Point(371, 275);
            this.pnlItemRate.Name = "pnlItemRate";
            this.pnlItemRate.Size = new System.Drawing.Size(365, 232);
            this.pnlItemRate.TabIndex = 19;
            // 
            // cmbSalesAccount
            // 
            this.cmbSalesAccount.FormattingEnabled = true;
            this.cmbSalesAccount.Location = new System.Drawing.Point(141, 78);
            this.cmbSalesAccount.MaxLength = 20;
            this.cmbSalesAccount.Name = "cmbSalesAccount";
            this.cmbSalesAccount.Size = new System.Drawing.Size(169, 21);
            this.cmbSalesAccount.TabIndex = 21;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 82);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(109, 13);
            this.label26.TabIndex = 803;
            this.label26.Text = "Select Sales Account";
            // 
            // cmbTextType
            // 
            this.cmbTextType.FormattingEnabled = true;
            this.cmbTextType.Location = new System.Drawing.Point(141, 22);
            this.cmbTextType.MaxLength = 20;
            this.cmbTextType.Name = "cmbTextType";
            this.cmbTextType.Size = new System.Drawing.Size(169, 21);
            this.cmbTextType.TabIndex = 19;
            this.cmbTextType.SelectedIndexChanged += new System.EventHandler(this.cmbTextType_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(10, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 13);
            this.label25.TabIndex = 801;
            this.label25.Text = "Select Tax Type";
            // 
            // cmbItemName
            // 
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.Location = new System.Drawing.Point(141, 136);
            this.cmbItemName.MaxLength = 30;
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(169, 21);
            this.cmbItemName.TabIndex = 23;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Lavender;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(361, 20);
            this.label22.TabIndex = 801;
            this.label22.Text = "Tax Based On Item Rate";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 139);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 805;
            this.label11.Text = "Item Name";
            // 
            // dtpItemFrmDate
            // 
            this.dtpItemFrmDate.Location = new System.Drawing.Point(141, 165);
            this.dtpItemFrmDate.Name = "dtpItemFrmDate";
            this.dtpItemFrmDate.Size = new System.Drawing.Size(169, 20);
            this.dtpItemFrmDate.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 806;
            this.label6.Text = "From Date";
            // 
            // cmbItemCalcMethod
            // 
            this.cmbItemCalcMethod.FormattingEnabled = true;
            this.cmbItemCalcMethod.Location = new System.Drawing.Point(141, 106);
            this.cmbItemCalcMethod.MaxLength = 25;
            this.cmbItemCalcMethod.Name = "cmbItemCalcMethod";
            this.cmbItemCalcMethod.Size = new System.Drawing.Size(169, 21);
            this.cmbItemCalcMethod.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 804;
            this.label7.Text = "Calculation Method";
            // 
            // cmbItemTax
            // 
            this.cmbItemTax.FormattingEnabled = true;
            this.cmbItemTax.Location = new System.Drawing.Point(142, 49);
            this.cmbItemTax.MaxLength = 20;
            this.cmbItemTax.Name = "cmbItemTax";
            this.cmbItemTax.Size = new System.Drawing.Size(169, 21);
            this.cmbItemTax.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 802;
            this.label8.Text = "Select Tax";
            // 
            // txtItemPercentage
            // 
            this.txtItemPercentage.Location = new System.Drawing.Point(141, 193);
            this.txtItemPercentage.MaxLength = 10;
            this.txtItemPercentage.Name = "txtItemPercentage";
            this.txtItemPercentage.Size = new System.Drawing.Size(169, 20);
            this.txtItemPercentage.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 807;
            this.label10.Text = "Percentage";
            // 
            // pnlOtherTaxes
            // 
            this.pnlOtherTaxes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOtherTaxes.Controls.Add(this.label23);
            this.pnlOtherTaxes.Controls.Add(this.cmbLedgerTax);
            this.pnlOtherTaxes.Controls.Add(this.label15);
            this.pnlOtherTaxes.Controls.Add(this.dtpOtherTaxFrmDate);
            this.pnlOtherTaxes.Controls.Add(this.label12);
            this.pnlOtherTaxes.Controls.Add(this.cmbOtherTaxCalcMethod);
            this.pnlOtherTaxes.Controls.Add(this.label13);
            this.pnlOtherTaxes.Controls.Add(this.cmbOtherTax);
            this.pnlOtherTaxes.Controls.Add(this.label14);
            this.pnlOtherTaxes.Controls.Add(this.txtOtherTaxPercentage);
            this.pnlOtherTaxes.Controls.Add(this.label16);
            this.pnlOtherTaxes.Location = new System.Drawing.Point(16, 274);
            this.pnlOtherTaxes.Name = "pnlOtherTaxes";
            this.pnlOtherTaxes.Size = new System.Drawing.Size(329, 219);
            this.pnlOtherTaxes.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Lavender;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(325, 20);
            this.label23.TabIndex = 701;
            this.label23.Text = "Tax Based On Other Taxes";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbLedgerTax
            // 
            this.cmbLedgerTax.FormattingEnabled = true;
            this.cmbLedgerTax.Location = new System.Drawing.Point(146, 65);
            this.cmbLedgerTax.MaxLength = 10;
            this.cmbLedgerTax.Name = "cmbLedgerTax";
            this.cmbLedgerTax.Size = new System.Drawing.Size(169, 21);
            this.cmbLedgerTax.TabIndex = 15;
             // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 702;
            this.label15.Text = "Tax On Ledger";
            // 
            // dtpOtherTaxFrmDate
            // 
            this.dtpOtherTaxFrmDate.Location = new System.Drawing.Point(146, 136);
            this.dtpOtherTaxFrmDate.Name = "dtpOtherTaxFrmDate";
            this.dtpOtherTaxFrmDate.Size = new System.Drawing.Size(169, 20);
            this.dtpOtherTaxFrmDate.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 704;
            this.label12.Text = "From Date";
            // 
            // cmbOtherTaxCalcMethod
            // 
            this.cmbOtherTaxCalcMethod.FormattingEnabled = true;
            this.cmbOtherTaxCalcMethod.Location = new System.Drawing.Point(146, 101);
            this.cmbOtherTaxCalcMethod.MaxLength = 25;
            this.cmbOtherTaxCalcMethod.Name = "cmbOtherTaxCalcMethod";
            this.cmbOtherTaxCalcMethod.Size = new System.Drawing.Size(169, 21);
            this.cmbOtherTaxCalcMethod.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 13);
            this.label13.TabIndex = 703;
            this.label13.Text = "Calculation Method";
            // 
            // cmbOtherTax
            // 
            this.cmbOtherTax.FormattingEnabled = true;
            this.cmbOtherTax.Location = new System.Drawing.Point(146, 33);
            this.cmbOtherTax.Name = "cmbOtherTax";
            this.cmbOtherTax.Size = new System.Drawing.Size(169, 21);
            this.cmbOtherTax.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 13);
            this.label14.TabIndex = 701;
            this.label14.Text = "Select Tax";
            // 
            // txtOtherTaxPercentage
            // 
            this.txtOtherTaxPercentage.Location = new System.Drawing.Point(146, 171);
            this.txtOtherTaxPercentage.MaxLength = 10;
            this.txtOtherTaxPercentage.Name = "txtOtherTaxPercentage";
            this.txtOtherTaxPercentage.Size = new System.Drawing.Size(169, 20);
            this.txtOtherTaxPercentage.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 174);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 705;
            this.label16.Text = "Percentage";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(175, 657);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 603;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlGrandTotal
            // 
            this.pnlGrandTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGrandTotal.Controls.Add(this.label24);
            this.pnlGrandTotal.Controls.Add(this.dtpGrandTotFrmDate);
            this.pnlGrandTotal.Controls.Add(this.label17);
            this.pnlGrandTotal.Controls.Add(this.cmbGrandTotCalcMethod);
            this.pnlGrandTotal.Controls.Add(this.label18);
            this.pnlGrandTotal.Controls.Add(this.cmbGrandTotTax);
            this.pnlGrandTotal.Controls.Add(this.label19);
            this.pnlGrandTotal.Controls.Add(this.txtGrandTotPercentage);
            this.pnlGrandTotal.Controls.Add(this.label21);
            this.pnlGrandTotal.Location = new System.Drawing.Point(14, 68);
            this.pnlGrandTotal.Name = "pnlGrandTotal";
            this.pnlGrandTotal.Size = new System.Drawing.Size(320, 185);
            this.pnlGrandTotal.TabIndex = 5;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Lavender;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(316, 20);
            this.label24.TabIndex = 501;
            this.label24.Text = "Tax Based On Grand Total";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpGrandTotFrmDate
            // 
            this.dtpGrandTotFrmDate.Location = new System.Drawing.Point(137, 106);
            this.dtpGrandTotFrmDate.Name = "dtpGrandTotFrmDate";
            this.dtpGrandTotFrmDate.Size = new System.Drawing.Size(169, 20);
            this.dtpGrandTotFrmDate.TabIndex = 7;
             // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 503;
            this.label17.Text = "From Date";
            // 
            // cmbGrandTotCalcMethod
            // 
            this.cmbGrandTotCalcMethod.FormattingEnabled = true;
            this.cmbGrandTotCalcMethod.Location = new System.Drawing.Point(137, 71);
            this.cmbGrandTotCalcMethod.MaxLength = 25;
            this.cmbGrandTotCalcMethod.Name = "cmbGrandTotCalcMethod";
            this.cmbGrandTotCalcMethod.Size = new System.Drawing.Size(169, 21);
            this.cmbGrandTotCalcMethod.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 74);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 13);
            this.label18.TabIndex = 502;
            this.label18.Text = "Calculation Method";
            // 
            // cmbGrandTotTax
            // 
            this.cmbGrandTotTax.FormattingEnabled = true;
            this.cmbGrandTotTax.Location = new System.Drawing.Point(137, 37);
            this.cmbGrandTotTax.MaxLength = 20;
            this.cmbGrandTotTax.Name = "cmbGrandTotTax";
            this.cmbGrandTotTax.Size = new System.Drawing.Size(169, 21);
            this.cmbGrandTotTax.TabIndex = 5;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 13);
            this.label19.TabIndex = 501;
            this.label19.Text = "Select Tax";
            // 
            // txtGrandTotPercentage
            // 
            this.txtGrandTotPercentage.Location = new System.Drawing.Point(137, 141);
            this.txtGrandTotPercentage.MaxLength = 10;
            this.txtGrandTotPercentage.Name = "txtGrandTotPercentage";
            this.txtGrandTotPercentage.Size = new System.Drawing.Size(169, 20);
            this.txtGrandTotPercentage.TabIndex = 8;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 144);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 13);
            this.label21.TabIndex = 504;
            this.label21.Text = "Percentage";
            // 
            // rdTotalSale
            // 
            this.rdTotalSale.AutoSize = true;
            this.rdTotalSale.Location = new System.Drawing.Point(22, 14);
            this.rdTotalSale.Name = "rdTotalSale";
            this.rdTotalSale.Size = new System.Drawing.Size(144, 17);
            this.rdTotalSale.TabIndex = 1;
            this.rdTotalSale.TabStop = true;
            this.rdTotalSale.Text = "Tax Based On Total Sale";
            this.rdTotalSale.UseVisualStyleBackColor = true;
            this.rdTotalSale.CheckedChanged += new System.EventHandler(this.rdTotalSale_CheckedChanged);
            // 
            // rdItemRate
            // 
            this.rdItemRate.AutoSize = true;
            this.rdItemRate.Location = new System.Drawing.Point(200, 14);
            this.rdItemRate.Name = "rdItemRate";
            this.rdItemRate.Size = new System.Drawing.Size(142, 17);
            this.rdItemRate.TabIndex = 2;
            this.rdItemRate.TabStop = true;
            this.rdItemRate.Text = "Tax Based On Item Rate";
            this.rdItemRate.UseVisualStyleBackColor = true;
            this.rdItemRate.CheckedChanged += new System.EventHandler(this.rdItemRate_CheckedChanged);
            // 
            // rdOtherTaxes
            // 
            this.rdOtherTaxes.AutoSize = true;
            this.rdOtherTaxes.Location = new System.Drawing.Point(22, 46);
            this.rdOtherTaxes.Name = "rdOtherTaxes";
            this.rdOtherTaxes.Size = new System.Drawing.Size(154, 17);
            this.rdOtherTaxes.TabIndex = 3;
            this.rdOtherTaxes.TabStop = true;
            this.rdOtherTaxes.Text = "Tax Based On Other Taxes";
            this.rdOtherTaxes.UseVisualStyleBackColor = true;
            this.rdOtherTaxes.CheckedChanged += new System.EventHandler(this.rdOtherTaxes_CheckedChanged);
            // 
            // rdGrandTotal
            // 
            this.rdGrandTotal.AutoSize = true;
            this.rdGrandTotal.Location = new System.Drawing.Point(200, 46);
            this.rdGrandTotal.Name = "rdGrandTotal";
            this.rdGrandTotal.Size = new System.Drawing.Size(152, 17);
            this.rdGrandTotal.TabIndex = 4;
            this.rdGrandTotal.TabStop = true;
            this.rdGrandTotal.Text = "Tax Based On Grand Total";
            this.rdGrandTotal.UseVisualStyleBackColor = true;
            this.rdGrandTotal.CheckedChanged += new System.EventHandler(this.rdGrandTotal_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlGrandTotal);
            this.pnlMain.Controls.Add(this.pnlTotalSale);
            this.pnlMain.Controls.Add(this.pnlOtherTaxes);
            this.pnlMain.Controls.Add(this.pnlItemRate);
            this.pnlMain.Location = new System.Drawing.Point(13, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(786, 512);
            this.pnlMain.TabIndex = 0;
            // 
            // DGPanel
            // 
            this.DGPanel.Controls.Add(this.dgv);
            this.DGPanel.Location = new System.Drawing.Point(489, 530);
            this.DGPanel.Name = "DGPanel";
            this.DGPanel.Size = new System.Drawing.Size(527, 204);
            this.DGPanel.TabIndex = 1001;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(371, 657);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 20);
            this.btnPrev.TabIndex = 205;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(429, 656);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 20);
            this.btnLast.TabIndex = 207;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(399, 658);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 20);
            this.btnNext.TabIndex = 206;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(342, 657);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 20);
            this.btnFirst.TabIndex = 204;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // TaxSettingsAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 746);
            this.Controls.Add(this.DGPanel);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.rdGrandTotal);
            this.Controls.Add(this.rdOtherTaxes);
            this.Controls.Add(this.rdItemRate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.rdTotalSale);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.pnlMain);
            this.Name = "TaxSettingsAE";
            this.Text = "Tax Settings";
            this.Load += new System.EventHandler(this.TaxSettingsAE_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaxSettingsAE_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlTotalSale.ResumeLayout(false);
            this.pnlTotalSale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnlItemRate.ResumeLayout(false);
            this.pnlItemRate.PerformLayout();
            this.pnlOtherTaxes.ResumeLayout(false);
            this.pnlOtherTaxes.PerformLayout();
            this.pnlGrandTotal.ResumeLayout(false);
            this.pnlGrandTotal.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.DGPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTotSalePercentage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSaleTaxSettingNo;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Panel pnlTotalSale;
        public System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSaleCalcMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSaleTax;
        private System.Windows.Forms.DateTimePicker dtpSaleFrmDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlItemRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpItemFrmDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbItemCalcMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbItemTax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtItemPercentage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlOtherTaxes;
        private System.Windows.Forms.DateTimePicker dtpOtherTaxFrmDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbOtherTaxCalcMethod;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbOtherTax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOtherTaxPercentage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlGrandTotal;
        private System.Windows.Forms.DateTimePicker dtpGrandTotFrmDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbGrandTotCalcMethod;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbGrandTotTax;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtGrandTotPercentage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbLedgerTax;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.RadioButton rdGrandTotal;
        private System.Windows.Forms.RadioButton rdOtherTaxes;
        private System.Windows.Forms.RadioButton rdItemRate;
        private System.Windows.Forms.RadioButton rdTotalSale;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel DGPanel;
        public System.Windows.Forms.Button btnFirst;
        public System.Windows.Forms.Button btnNext;
        public System.Windows.Forms.Button btnLast;
        public System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.ComboBox cmbTextType;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbSalesAccount;
        private System.Windows.Forms.Label label26;
    }
}