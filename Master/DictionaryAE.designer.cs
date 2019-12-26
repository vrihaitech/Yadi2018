namespace Yadi.Master
{
    partial class DictionaryAE
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
            this.Panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pnlMain = new OMControls.OMBPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.chkSupplier = new System.Windows.Forms.CheckBox();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.chkItemName = new System.Windows.Forms.CheckBox();
            this.chkItemGroup = new System.Windows.Forms.CheckBox();
            this.btnAutoCheck = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.panel3);
            this.Panel2.Controls.Add(this.DataGridView1);
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(15, 10);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(653, 415);
            this.Panel2.TabIndex = 23;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 370);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(653, 45);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "New Entry (Ctrl + N/ F5 )";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(570, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 40);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 55);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView1.Size = new System.Drawing.Size(653, 314);
            this.DataGridView1.TabIndex = 2;
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentDoubleClick);
            this.DataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView1_KeyDown);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.TxtSearch);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(653, 55);
            this.Panel1.TabIndex = 0;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.Location = new System.Drawing.Point(4, 9);
            this.TxtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSearch.MaxLength = 20;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(645, 27);
            this.TxtSearch.TabIndex = 1;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label32);
            this.pnlMain.Controls.Add(this.chkSupplier);
            this.pnlMain.Controls.Add(this.chkCustomer);
            this.pnlMain.Controls.Add(this.chkItemName);
            this.pnlMain.Controls.Add(this.chkItemGroup);
            this.pnlMain.Controls.Add(this.btnAutoCheck);
            this.pnlMain.Controls.Add(this.Panel2);
            this.pnlMain.Location = new System.Drawing.Point(12, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(885, 436);
            this.pnlMain.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(692, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 12);
            this.label4.TabIndex = 88;
            this.label4.Text = "Supplier";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(692, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 87;
            this.label3.Text = "Customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(692, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 86;
            this.label2.Text = "Item Name";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(692, 64);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(51, 12);
            this.label32.TabIndex = 85;
            this.label32.Text = "Item Group";
            // 
            // chkSupplier
            // 
            this.chkSupplier.AutoSize = true;
            this.chkSupplier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSupplier.Location = new System.Drawing.Point(805, 140);
            this.chkSupplier.Name = "chkSupplier";
            this.chkSupplier.Size = new System.Drawing.Size(39, 16);
            this.chkSupplier.TabIndex = 84;
            this.chkSupplier.Text = "Yes";
            this.chkSupplier.UseVisualStyleBackColor = true;
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkCustomer.Location = new System.Drawing.Point(805, 118);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(39, 16);
            this.chkCustomer.TabIndex = 83;
            this.chkCustomer.Text = "Yes";
            this.chkCustomer.UseVisualStyleBackColor = true;
            // 
            // chkItemName
            // 
            this.chkItemName.AutoSize = true;
            this.chkItemName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkItemName.Location = new System.Drawing.Point(805, 86);
            this.chkItemName.Name = "chkItemName";
            this.chkItemName.Size = new System.Drawing.Size(39, 16);
            this.chkItemName.TabIndex = 82;
            this.chkItemName.Text = "Yes";
            this.chkItemName.UseVisualStyleBackColor = true;
            // 
            // chkItemGroup
            // 
            this.chkItemGroup.AutoSize = true;
            this.chkItemGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkItemGroup.Location = new System.Drawing.Point(805, 64);
            this.chkItemGroup.Name = "chkItemGroup";
            this.chkItemGroup.Size = new System.Drawing.Size(39, 16);
            this.chkItemGroup.TabIndex = 81;
            this.chkItemGroup.Text = "Yes";
            this.chkItemGroup.UseVisualStyleBackColor = true;
            // 
            // btnAutoCheck
            // 
            this.btnAutoCheck.Location = new System.Drawing.Point(679, 174);
            this.btnAutoCheck.Name = "btnAutoCheck";
            this.btnAutoCheck.Size = new System.Drawing.Size(80, 40);
            this.btnAutoCheck.TabIndex = 80;
            this.btnAutoCheck.Text = "Auto Check";
            this.btnAutoCheck.UseVisualStyleBackColor = true;
            this.btnAutoCheck.Click += new System.EventHandler(this.btnAutoCheck_Click_1);
            // 
            // DictionaryAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 9F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 460);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DictionaryAE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dictionary";
            this.Panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.BindingSource bindingSource1;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox chkSupplier;
        private System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.CheckBox chkItemName;
        private System.Windows.Forms.CheckBox chkItemGroup;
        private System.Windows.Forms.Button btnAutoCheck;
    }
}