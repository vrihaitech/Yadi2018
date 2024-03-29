﻿namespace Yadi.Vouchers
{
    partial class ExpensesEntry
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
            this.Panel2 = new System.Windows.Forms.Panel();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.CmbSearch = new System.Windows.Forms.ComboBox();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pnlMain = new OMControls.OMBPanel();
            this.dtpSearch = new System.Windows.Forms.DateTimePicker();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.DataGridView1);
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(15, 10);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(653, 415);
            this.Panel2.TabIndex = 23;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.DataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView1.Location = new System.Drawing.Point(0, 57);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 35;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(653, 358);
            this.DataGridView1.TabIndex = 4;
            this.DataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.dtpSearch);
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Controls.Add(this.btnShowAll);
            this.Panel1.Controls.Add(this.CmbSearch);
            this.Panel1.Controls.Add(this.TxtSearch);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(653, 57);
            this.Panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(570, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.Location = new System.Drawing.Point(459, 3);
            this.btnShowAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(80, 50);
            this.btnShowAll.TabIndex = 2;
            this.btnShowAll.Text = "Show &All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // CmbSearch
            // 
            this.CmbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbSearch.FormattingEnabled = true;
            this.CmbSearch.Location = new System.Drawing.Point(91, 2);
            this.CmbSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CmbSearch.MaxLength = 20;
            this.CmbSearch.Name = "CmbSearch";
            this.CmbSearch.Size = new System.Drawing.Size(194, 26);
            this.CmbSearch.TabIndex = 0;
            this.CmbSearch.SelectedIndexChanged += new System.EventHandler(this.CmbSearch_SelectedIndexChanged);
            this.CmbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbSearch_KeyDown);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(290, 2);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TxtSearch.MaxLength = 20;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(158, 27);
            this.TxtSearch.TabIndex = 1;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(8, 7);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(81, 17);
            this.Label1.TabIndex = 51;
            this.Label1.Text = "Search By :";
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.Panel2);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(683, 435);
            this.pnlMain.TabIndex = 24;
            // 
            // dtpSearch
            // 
            this.dtpSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSearch.Location = new System.Drawing.Point(290, 2);
            this.dtpSearch.Name = "dtpSearch";
            this.dtpSearch.Size = new System.Drawing.Size(118, 20);
            this.dtpSearch.TabIndex = 25;
            this.dtpSearch.Visible = false;
            this.dtpSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpSearch_KeyDown);
            // 
            // ExpensesEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 9F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 462);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ExpensesEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expense Entry (Search)";
            this.Load += new System.EventHandler(this.VoucherEntry_Load);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnShowAll;
        internal System.Windows.Forms.ComboBox CmbSearch;
        internal System.Windows.Forms.TextBox TxtSearch;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.BindingSource bindingSource1;
        internal System.Windows.Forms.Button btnCancel;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DateTimePicker dtpSearch;
    }
}