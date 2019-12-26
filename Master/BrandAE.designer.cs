namespace Yadi.Master
{
    partial class BrandAE
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtStockGroupName = new System.Windows.Forms.TextBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.pnlMain = new OMControls.OMBPanel();
            this.btnNewManufacturer = new System.Windows.Forms.Button();
            this.lstBrandName = new System.Windows.Forms.ListBox();
            this.cmbManufacturerCompanyName = new System.Windows.Forms.ComboBox();
            this.btnLangDesc = new System.Windows.Forms.Button();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblMfgComp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = " Name :";
            // 
            // txtStockGroupName
            // 
            this.txtStockGroupName.Location = new System.Drawing.Point(176, 18);
            this.txtStockGroupName.MaxLength = 25;
            this.txtStockGroupName.Name = "txtStockGroupName";
            this.txtStockGroupName.Size = new System.Drawing.Size(243, 20);
            this.txtStockGroupName.TabIndex = 0;
            this.txtStockGroupName.TextChanged += new System.EventHandler(this.txtStockGroupName_TextChanged);
            this.txtStockGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockGroupName_KeyDown);
            this.txtStockGroupName.Leave += new System.EventHandler(this.txtStockGroupName_Leave);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(229, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 60);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(127, 126);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnNewManufacturer);
            this.pnlMain.Controls.Add(this.lstBrandName);
            this.pnlMain.Controls.Add(this.cmbManufacturerCompanyName);
            this.pnlMain.Controls.Add(this.btnLangDesc);
            this.pnlMain.Controls.Add(this.txtLanguage);
            this.pnlMain.Controls.Add(this.lblLanguage);
            this.pnlMain.Controls.Add(this.txtStockGroupName);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Location = new System.Drawing.Point(21, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(456, 200);
            this.pnlMain.TabIndex = 15;
            // 
            // btnNewManufacturer
            // 
            this.btnNewManufacturer.Location = new System.Drawing.Point(425, 92);
            this.btnNewManufacturer.Name = "btnNewManufacturer";
            this.btnNewManufacturer.Size = new System.Drawing.Size(21, 21);
            this.btnNewManufacturer.TabIndex = 190046;
            this.btnNewManufacturer.Text = "..";
            this.btnNewManufacturer.UseVisualStyleBackColor = true;
            this.btnNewManufacturer.Click += new System.EventHandler(this.btnNewManufacturer_Click);
            // 
            // lstBrandName
            // 
            this.lstBrandName.FormattingEnabled = true;
            this.lstBrandName.Location = new System.Drawing.Point(176, 44);
            this.lstBrandName.Name = "lstBrandName";
            this.lstBrandName.Size = new System.Drawing.Size(243, 30);
            this.lstBrandName.TabIndex = 190045;
            this.lstBrandName.Visible = false;
            this.lstBrandName.Leave += new System.EventHandler(this.lstBrandName_Leave);
            this.lstBrandName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBrandName_KeyDown);
            // 
            // cmbManufacturerCompanyName
            // 
            this.cmbManufacturerCompanyName.FormattingEnabled = true;
            this.cmbManufacturerCompanyName.Location = new System.Drawing.Point(176, 92);
            this.cmbManufacturerCompanyName.Name = "cmbManufacturerCompanyName";
            this.cmbManufacturerCompanyName.Size = new System.Drawing.Size(243, 21);
            this.cmbManufacturerCompanyName.TabIndex = 2;
            // 
            // btnLangDesc
            // 
            this.btnLangDesc.Location = new System.Drawing.Point(425, 45);
            this.btnLangDesc.Name = "btnLangDesc";
            this.btnLangDesc.Size = new System.Drawing.Size(21, 21);
            this.btnLangDesc.TabIndex = 190044;
            this.btnLangDesc.Text = "..";
            this.btnLangDesc.UseVisualStyleBackColor = true;
            this.btnLangDesc.Click += new System.EventHandler(this.btnLangDesc_Click);
            // 
            // txtLanguage
            // 
            this.txtLanguage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLanguage.Font = new System.Drawing.Font("Shivaji01", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLanguage.Location = new System.Drawing.Point(176, 44);
            this.txtLanguage.MaxLength = 25;
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(243, 25);
            this.txtLanguage.TabIndex = 1;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(46, 49);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(61, 13);
            this.lblLanguage.TabIndex = 190043;
            this.lblLanguage.Text = "Language :";
            // 
            // lblMfgComp
            // 
            this.lblMfgComp.AutoSize = true;
            this.lblMfgComp.Location = new System.Drawing.Point(70, 103);
            this.lblMfgComp.Name = "lblMfgComp";
            this.lblMfgComp.Size = new System.Drawing.Size(68, 13);
            this.lblMfgComp.TabIndex = 190046;
            this.lblMfgComp.Text = "Mfg.  Name :";
            // 
            // BrandAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 236);
            this.Controls.Add(this.lblMfgComp);
            this.Controls.Add(this.pnlMain);
            this.Name = "BrandAE";
            this.Text = "Brand Master";
            this.Load += new System.EventHandler(this.BrandAE_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StockGroupAE_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStockGroupName;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button BtnSave;
        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Button btnLangDesc;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ListBox lstBrandName;
        private System.Windows.Forms.ComboBox cmbManufacturerCompanyName;
        private System.Windows.Forms.Label lblMfgComp;
        private System.Windows.Forms.Button btnNewManufacturer;
    }
}