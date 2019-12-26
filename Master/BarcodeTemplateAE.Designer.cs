namespace Yadi.Master
{
    partial class BarcodeTemplateAE
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
            this.pnlMain = new OMControls.OMBPanel();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgHelp = new System.Windows.Forms.DataGridView();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnDefault = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.cmbBarcodeTemplate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.chkReadOnly);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.dgHelp);
            this.pnlMain.Controls.Add(this.BtnExit);
            this.pnlMain.Controls.Add(this.BtnDefault);
            this.pnlMain.Controls.Add(this.BtnCancel);
            this.pnlMain.Controls.Add(this.BtnSave);
            this.pnlMain.Controls.Add(this.txtScript);
            this.pnlMain.Controls.Add(this.cmbPrinter);
            this.pnlMain.Controls.Add(this.txtSize);
            this.pnlMain.Controls.Add(this.cmbBarcodeTemplate);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Location = new System.Drawing.Point(8, 6);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(668, 533);
            this.pnlMain.TabIndex = 0;
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.AutoSize = true;
            this.chkReadOnly.Location = new System.Drawing.Point(115, 140);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(76, 17);
            this.chkReadOnly.TabIndex = 32;
            this.chkReadOnly.Text = "Read Only";
            this.chkReadOnly.UseVisualStyleBackColor = true;
            this.chkReadOnly.CheckedChanged += new System.EventHandler(this.chkReadOnly_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 460);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 39);
            this.label4.TabIndex = 31;
            this.label4.Text = "Note :  \r\nVariable names are case sensitive. \r\nPlease use Proper Case in Script.";
            // 
            // dgHelp
            // 
            this.dgHelp.AllowUserToAddRows = false;
            this.dgHelp.AllowUserToDeleteRows = false;
            this.dgHelp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgHelp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHelp.Location = new System.Drawing.Point(404, 159);
            this.dgHelp.Name = "dgHelp";
            this.dgHelp.ReadOnly = true;
            this.dgHelp.Size = new System.Drawing.Size(249, 289);
            this.dgHelp.TabIndex = 30;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(283, 460);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 60);
            this.BtnExit.TabIndex = 6;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnDefault
            // 
            this.BtnDefault.Location = new System.Drawing.Point(197, 460);
            this.BtnDefault.Name = "BtnDefault";
            this.BtnDefault.Size = new System.Drawing.Size(80, 60);
            this.BtnDefault.TabIndex = 5;
            this.BtnDefault.Text = "Default";
            this.BtnDefault.UseVisualStyleBackColor = true;
            this.BtnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(111, 460);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 60);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(25, 460);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 60);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(15, 159);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ReadOnly = true;
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtScript.Size = new System.Drawing.Size(373, 289);
            this.txtScript.TabIndex = 2;
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.FormattingEnabled = true;
            this.cmbPrinter.Location = new System.Drawing.Point(115, 103);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(269, 21);
            this.cmbPrinter.TabIndex = 1;
            this.cmbPrinter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrinter_KeyDown);
            // 
            // txtSize
            // 
            this.txtSize.Enabled = false;
            this.txtSize.Location = new System.Drawing.Point(115, 64);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(269, 20);
            this.txtSize.TabIndex = 27;
            // 
            // cmbBarcodeTemplate
            // 
            this.cmbBarcodeTemplate.FormattingEnabled = true;
            this.cmbBarcodeTemplate.Location = new System.Drawing.Point(115, 23);
            this.cmbBarcodeTemplate.Name = "cmbBarcodeTemplate";
            this.cmbBarcodeTemplate.Size = new System.Drawing.Size(269, 21);
            this.cmbBarcodeTemplate.TabIndex = 0;
            this.cmbBarcodeTemplate.Leave += new System.EventHandler(this.cmbBarcodeTemplate_Leave);
            this.cmbBarcodeTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBarcodeTemplate_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Script :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Printer :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Size :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Template :";
            // 
            // BarcodeTemplateAE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 551);
            this.Controls.Add(this.pnlMain);
            this.Name = "BarcodeTemplateAE";
            this.Text = "Barcode Template";
            this.Load += new System.EventHandler(this.BarcodeTemplateAE_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHelp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.ComboBox cmbBarcodeTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Button BtnDefault;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.DataGridView dgHelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkReadOnly;
    }
}