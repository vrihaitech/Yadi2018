namespace Yadi.Master
{
    partial class ItemMasterSearchAuto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain1 = new OMControls.OMBPanel();
            this.pnlVehicle = new System.Windows.Forms.Panel();
            this.lstVehicle = new System.Windows.Forms.ListBox();
            this.pnlProduct = new System.Windows.Forms.Panel();
            this.lstProduct = new System.Windows.Forms.ListBox();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lstBrand = new System.Windows.Forms.ListBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.chkClearAll = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkVehicle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkProduct = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkBrand = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblS3 = new System.Windows.Forms.Label();
            this.lblS2 = new System.Windows.Forms.Label();
            this.lblS1 = new System.Windows.Forms.Label();
            this.txtS3 = new System.Windows.Forms.TextBox();
            this.txtS2 = new System.Windows.Forms.TextBox();
            this.txtS1 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnlMain = new OMControls.OMBPanel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pnlMain1.SuspendLayout();
            this.pnlVehicle.SuspendLayout();
            this.pnlProduct.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain1
            // 
            this.pnlMain1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(173)))), ((int)(((byte)(226)))));
            this.pnlMain1.BorderRadius = 2;
            this.pnlMain1.Controls.Add(this.pnlVehicle);
            this.pnlMain1.Controls.Add(this.pnlProduct);
            this.pnlMain1.Controls.Add(this.pnlBrand);
            this.pnlMain1.Controls.Add(this.DataGridView1);
            this.pnlMain1.Controls.Add(this.Panel1);
            this.pnlMain1.Location = new System.Drawing.Point(31, 7);
            this.pnlMain1.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain1.Name = "pnlMain1";
            this.pnlMain1.Size = new System.Drawing.Size(1170, 509);
            this.pnlMain1.TabIndex = 23;
            // 
            // pnlVehicle
            // 
            this.pnlVehicle.BackColor = System.Drawing.Color.Bisque;
            this.pnlVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVehicle.Controls.Add(this.lstVehicle);
            this.pnlVehicle.Location = new System.Drawing.Point(232, 100);
            this.pnlVehicle.Name = "pnlVehicle";
            this.pnlVehicle.Size = new System.Drawing.Size(295, 210);
            this.pnlVehicle.TabIndex = 16000039;
            this.pnlVehicle.Visible = false;
            // 
            // lstVehicle
            // 
            this.lstVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVehicle.FormattingEnabled = true;
            this.lstVehicle.ItemHeight = 16;
            this.lstVehicle.Location = new System.Drawing.Point(6, 8);
            this.lstVehicle.Name = "lstVehicle";
            this.lstVehicle.Size = new System.Drawing.Size(284, 196);
            this.lstVehicle.Sorted = true;
            this.lstVehicle.TabIndex = 516;
            this.lstVehicle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstVehicle_KeyPress);
            // 
            // pnlProduct
            // 
            this.pnlProduct.BackColor = System.Drawing.Color.Bisque;
            this.pnlProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProduct.Controls.Add(this.lstProduct);
            this.pnlProduct.Location = new System.Drawing.Point(162, 99);
            this.pnlProduct.Name = "pnlProduct";
            this.pnlProduct.Size = new System.Drawing.Size(295, 211);
            this.pnlProduct.TabIndex = 16000038;
            this.pnlProduct.Visible = false;
            // 
            // lstProduct
            // 
            this.lstProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstProduct.FormattingEnabled = true;
            this.lstProduct.ItemHeight = 16;
            this.lstProduct.Location = new System.Drawing.Point(6, 8);
            this.lstProduct.Name = "lstProduct";
            this.lstProduct.Size = new System.Drawing.Size(279, 196);
            this.lstProduct.Sorted = true;
            this.lstProduct.TabIndex = 516;
            this.lstProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstProduct_KeyPress);
            // 
            // pnlBrand
            // 
            this.pnlBrand.BackColor = System.Drawing.Color.Bisque;
            this.pnlBrand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBrand.Controls.Add(this.lstBrand);
            this.pnlBrand.Location = new System.Drawing.Point(140, 99);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Size = new System.Drawing.Size(295, 211);
            this.pnlBrand.TabIndex = 16000036;
            this.pnlBrand.Visible = false;
            // 
            // lstBrand
            // 
            this.lstBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBrand.FormattingEnabled = true;
            this.lstBrand.ItemHeight = 16;
            this.lstBrand.Location = new System.Drawing.Point(6, 8);
            this.lstBrand.Name = "lstBrand";
            this.lstBrand.Size = new System.Drawing.Size(279, 196);
            this.lstBrand.Sorted = true;
            this.lstBrand.TabIndex = 516;
            this.lstBrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBrand_KeyDown);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.DataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(4, 95);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 50;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(1162, 411);
            this.DataGridView1.TabIndex = 4;
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentDoubleClick);
            this.DataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1_CellFormatting);
            this.DataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.chkClearAll);
            this.Panel1.Controls.Add(this.label7);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.btnExit);
            this.Panel1.Controls.Add(this.chkVehicle);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.chkProduct);
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.chkBrand);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.lblS3);
            this.Panel1.Controls.Add(this.lblS2);
            this.Panel1.Controls.Add(this.lblS1);
            this.Panel1.Controls.Add(this.txtS3);
            this.Panel1.Controls.Add(this.txtS2);
            this.Panel1.Controls.Add(this.txtS1);
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Controls.Add(this.btnShowAll);
            this.Panel1.Controls.Add(this.txtSearch);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Location = new System.Drawing.Point(4, 4);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1162, 89);
            this.Panel1.TabIndex = 0;
            // 
            // chkClearAll
            // 
            this.chkClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClearAll.Location = new System.Drawing.Point(586, 16);
            this.chkClearAll.Margin = new System.Windows.Forms.Padding(0);
            this.chkClearAll.Name = "chkClearAll";
            this.chkClearAll.Size = new System.Drawing.Size(42, 17);
            this.chkClearAll.TabIndex = 66;
            this.chkClearAll.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkClearAll.UseVisualStyleBackColor = true;
            this.chkClearAll.CheckedChanged += new System.EventHandler(this.chkClearAll_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(504, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 67;
            this.label7.Text = "Clear All";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(727, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 509;
            this.label4.Text = "(Press Esc Key For Search Item Selection)";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1062, 9);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 508;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkVehicle
            // 
            this.chkVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVehicle.Location = new System.Drawing.Point(447, 16);
            this.chkVehicle.Margin = new System.Windows.Forms.Padding(0);
            this.chkVehicle.Name = "chkVehicle";
            this.chkVehicle.Size = new System.Drawing.Size(42, 17);
            this.chkVehicle.TabIndex = 58;
            this.chkVehicle.Text = "F3";
            this.chkVehicle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkVehicle.UseVisualStyleBackColor = true;
            this.chkVehicle.CheckedChanged += new System.EventHandler(this.chkVehicle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Vehicle Name";
            // 
            // chkProduct
            // 
            this.chkProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProduct.Location = new System.Drawing.Point(285, 16);
            this.chkProduct.Margin = new System.Windows.Forms.Padding(0);
            this.chkProduct.Name = "chkProduct";
            this.chkProduct.Size = new System.Drawing.Size(42, 17);
            this.chkProduct.TabIndex = 60;
            this.chkProduct.Text = "F2";
            this.chkProduct.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkProduct.UseVisualStyleBackColor = true;
            this.chkProduct.CheckedChanged += new System.EventHandler(this.chkProduct_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(186, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "Product Name";
            // 
            // chkBrand
            // 
            this.chkBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBrand.Location = new System.Drawing.Point(119, 16);
            this.chkBrand.Margin = new System.Windows.Forms.Padding(0);
            this.chkBrand.Name = "chkBrand";
            this.chkBrand.Size = new System.Drawing.Size(42, 17);
            this.chkBrand.TabIndex = 62;
            this.chkBrand.Text = "F1";
            this.chkBrand.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkBrand.UseVisualStyleBackColor = true;
            this.chkBrand.CheckedChanged += new System.EventHandler(this.chkBrand_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "Brand Name";
            // 
            // lblS3
            // 
            this.lblS3.AutoSize = true;
            this.lblS3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblS3.ForeColor = System.Drawing.Color.Black;
            this.lblS3.Location = new System.Drawing.Point(629, 61);
            this.lblS3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS3.Name = "lblS3";
            this.lblS3.Size = new System.Drawing.Size(0, 17);
            this.lblS3.TabIndex = 507;
            // 
            // lblS2
            // 
            this.lblS2.AutoSize = true;
            this.lblS2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblS2.ForeColor = System.Drawing.Color.Black;
            this.lblS2.Location = new System.Drawing.Point(282, 61);
            this.lblS2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS2.Name = "lblS2";
            this.lblS2.Size = new System.Drawing.Size(0, 17);
            this.lblS2.TabIndex = 506;
            // 
            // lblS1
            // 
            this.lblS1.AutoSize = true;
            this.lblS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblS1.ForeColor = System.Drawing.Color.Black;
            this.lblS1.Location = new System.Drawing.Point(13, 61);
            this.lblS1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS1.Name = "lblS1";
            this.lblS1.Size = new System.Drawing.Size(0, 17);
            this.lblS1.TabIndex = 505;
            // 
            // txtS3
            // 
            this.txtS3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3.Location = new System.Drawing.Point(733, 56);
            this.txtS3.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3.MaxLength = 30;
            this.txtS3.Name = "txtS3";
            this.txtS3.Size = new System.Drawing.Size(138, 27);
            this.txtS3.TabIndex = 504;
            // 
            // txtS2
            // 
            this.txtS2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS2.Location = new System.Drawing.Point(406, 56);
            this.txtS2.Margin = new System.Windows.Forms.Padding(2);
            this.txtS2.MaxLength = 30;
            this.txtS2.Name = "txtS2";
            this.txtS2.Size = new System.Drawing.Size(196, 27);
            this.txtS2.TabIndex = 503;
            // 
            // txtS1
            // 
            this.txtS1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS1.Location = new System.Drawing.Point(103, 56);
            this.txtS1.Margin = new System.Windows.Forms.Padding(2);
            this.txtS1.MaxLength = 30;
            this.txtS1.Name = "txtS1";
            this.txtS1.Size = new System.Drawing.Size(158, 27);
            this.txtS1.TabIndex = 502;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(957, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(857, 9);
            this.btnShowAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(80, 30);
            this.btnShowAll.TabIndex = 2;
            this.btnShowAll.Text = "Show";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(103, 56);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.MaxLength = 30;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(64, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Visible = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(13, 61);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 17);
            this.Label1.TabIndex = 501;
            this.Label1.Text = "Search By :";
            this.Label1.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(173)))), ((int)(((byte)(226)))));
            this.pnlMain.BorderRadius = 2;
            this.pnlMain.Controls.Add(this.pnlMain1);
            this.pnlMain.Location = new System.Drawing.Point(5, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1203, 518);
            this.pnlMain.TabIndex = 24;
            // 
            // ItemMasterSearchAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 9F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 542);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ItemMasterSearchAuto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Auto (Search)";
            this.Load += new System.EventHandler(this.ItemMasterSearchAuto_Load);
            this.pnlMain1.ResumeLayout(false);
            this.pnlVehicle.ResumeLayout(false);
            this.pnlProduct.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal OMControls.OMBPanel pnlMain1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnShowAll;
        internal System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.BindingSource bindingSource1;
        internal System.Windows.Forms.Button btnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.CheckBox chkClearAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkBrand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkVehicle;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblS3;
        internal System.Windows.Forms.Label lblS2;
        internal System.Windows.Forms.Label lblS1;
        internal System.Windows.Forms.TextBox txtS3;
        internal System.Windows.Forms.TextBox txtS2;
        internal System.Windows.Forms.TextBox txtS1;
        private System.Windows.Forms.Panel pnlVehicle;
        private System.Windows.Forms.ListBox lstVehicle;
        private System.Windows.Forms.Panel pnlProduct;
        private System.Windows.Forms.ListBox lstProduct;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.ListBox lstBrand;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Label label4;
    }
}