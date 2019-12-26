namespace Yadi.Master
{
    partial class StockItemIngredientDetails
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.GroupBox();
            this.btnHead3 = new System.Windows.Forms.Button();
            this.btnHead2 = new System.Windows.Forms.Button();
            this.btnHead = new System.Windows.Forms.Button();
            this.dgReceipe = new System.Windows.Forms.DataGridView();
            this.rSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceipeEng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceipeMar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLangHead3 = new System.Windows.Forms.TextBox();
            this.txtHead3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLangHead2 = new System.Windows.Forms.TextBox();
            this.txtHead2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLangHead1 = new System.Windows.Forms.TextBox();
            this.txtHead1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.GroupBox();
            this.pnlNutrition = new System.Windows.Forms.Panel();
            this.dgListNutrition = new System.Windows.Forms.DataGridView();
            this.NutritionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NutritionUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LstNutritionNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgNutrition = new System.Windows.Forms.DataGridView();
            this.txtHead = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIngredient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nutrition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NutritionNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPkSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipe)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlNutrition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListNutrition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNutrition)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Gray;
            this.pnlMain.BorderRadius = 3;
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.txtIngredient);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblItemName);
            this.pnlMain.Location = new System.Drawing.Point(8, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(619, 554);
            this.pnlMain.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(124, 510);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 39);
            this.btnExit.TabIndex = 5580;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(26, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 39);
            this.btnSave.TabIndex = 5579;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHead3);
            this.panel2.Controls.Add(this.btnHead2);
            this.panel2.Controls.Add(this.btnHead);
            this.panel2.Controls.Add(this.dgReceipe);
            this.panel2.Controls.Add(this.txtLangHead3);
            this.panel2.Controls.Add(this.txtHead3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtLangHead2);
            this.panel2.Controls.Add(this.txtHead2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtLangHead1);
            this.panel2.Controls.Add(this.txtHead1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(26, 264);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(582, 240);
            this.panel2.TabIndex = 5578;
            this.panel2.TabStop = false;
            this.panel2.Text = "Receipe";
            // 
            // btnHead3
            // 
            this.btnHead3.Location = new System.Drawing.Point(558, 89);
            this.btnHead3.Name = "btnHead3";
            this.btnHead3.Size = new System.Drawing.Size(21, 21);
            this.btnHead3.TabIndex = 190043;
            this.btnHead3.Text = "..";
            this.btnHead3.UseVisualStyleBackColor = true;
            this.btnHead3.Click += new System.EventHandler(this.btnHead3_Click);
            // 
            // btnHead2
            // 
            this.btnHead2.Location = new System.Drawing.Point(558, 58);
            this.btnHead2.Name = "btnHead2";
            this.btnHead2.Size = new System.Drawing.Size(21, 21);
            this.btnHead2.TabIndex = 190042;
            this.btnHead2.Text = "..";
            this.btnHead2.UseVisualStyleBackColor = true;
            this.btnHead2.Click += new System.EventHandler(this.btnHead2_Click);
            // 
            // btnHead
            // 
            this.btnHead.Location = new System.Drawing.Point(558, 24);
            this.btnHead.Name = "btnHead";
            this.btnHead.Size = new System.Drawing.Size(21, 21);
            this.btnHead.TabIndex = 190041;
            this.btnHead.Text = "..";
            this.btnHead.UseVisualStyleBackColor = true;
            this.btnHead.Click += new System.EventHandler(this.btnHead_Click);
            // 
            // dgReceipe
            // 
            this.dgReceipe.AllowUserToAddRows = false;
            this.dgReceipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReceipe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rSrNo,
            this.ReceipeEng,
            this.ReceipeMar,
            this.PkSrNo,
            this.Status});
            this.dgReceipe.Location = new System.Drawing.Point(6, 118);
            this.dgReceipe.Name = "dgReceipe";
            this.dgReceipe.Size = new System.Drawing.Size(547, 116);
            this.dgReceipe.TabIndex = 9;
            this.dgReceipe.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgReceipe_CellFormatting);
            this.dgReceipe.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReceipe_CellEndEdit);
            this.dgReceipe.CurrentCellChanged += new System.EventHandler(this.dgReceipe_CurrentCellChanged);
            this.dgReceipe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgReceipe_KeyDown);
            // 
            // rSrNo
            // 
            this.rSrNo.HeaderText = "SrNo";
            this.rSrNo.Name = "rSrNo";
            this.rSrNo.Width = 40;
            // 
            // ReceipeEng
            // 
            this.ReceipeEng.HeaderText = "Receipe(English)";
            this.ReceipeEng.Name = "ReceipeEng";
            this.ReceipeEng.Width = 230;
            // 
            // ReceipeMar
            // 
            this.ReceipeMar.HeaderText = "Receipe(Marathi)";
            this.ReceipeMar.Name = "ReceipeMar";
            this.ReceipeMar.Width = 230;
            // 
            // PkSrNo
            // 
            this.PkSrNo.HeaderText = "PkSrNo";
            this.PkSrNo.Name = "PkSrNo";
            this.PkSrNo.Visible = false;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Visible = false;
            // 
            // txtLangHead3
            // 
            this.txtLangHead3.Font = new System.Drawing.Font("Shivaji01", 12F);
            this.txtLangHead3.Location = new System.Drawing.Point(335, 87);
            this.txtLangHead3.Name = "txtLangHead3";
            this.txtLangHead3.Size = new System.Drawing.Size(218, 25);
            this.txtLangHead3.TabIndex = 8;
            // 
            // txtHead3
            // 
            this.txtHead3.Location = new System.Drawing.Point(105, 89);
            this.txtHead3.Name = "txtHead3";
            this.txtHead3.Size = new System.Drawing.Size(218, 20);
            this.txtHead3.TabIndex = 7;
            this.txtHead3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHead3_KeyDown);
            this.txtHead3.Leave += new System.EventHandler(this.txtHead3_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 5586;
            this.label7.Text = "Receipe Head3 :";
            // 
            // txtLangHead2
            // 
            this.txtLangHead2.Font = new System.Drawing.Font("Shivaji01", 12F);
            this.txtLangHead2.Location = new System.Drawing.Point(335, 56);
            this.txtLangHead2.Name = "txtLangHead2";
            this.txtLangHead2.Size = new System.Drawing.Size(218, 25);
            this.txtLangHead2.TabIndex = 6;
            // 
            // txtHead2
            // 
            this.txtHead2.Location = new System.Drawing.Point(105, 58);
            this.txtHead2.Name = "txtHead2";
            this.txtHead2.Size = new System.Drawing.Size(218, 20);
            this.txtHead2.TabIndex = 5;
            this.txtHead2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHead2_KeyDown);
            this.txtHead2.Leave += new System.EventHandler(this.txtHead2_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 5583;
            this.label6.Text = "Receipe Head2 :";
            // 
            // txtLangHead1
            // 
            this.txtLangHead1.Font = new System.Drawing.Font("Shivaji01", 12F);
            this.txtLangHead1.Location = new System.Drawing.Point(335, 24);
            this.txtLangHead1.Name = "txtLangHead1";
            this.txtLangHead1.Size = new System.Drawing.Size(218, 25);
            this.txtLangHead1.TabIndex = 4;
            // 
            // txtHead1
            // 
            this.txtHead1.Location = new System.Drawing.Point(105, 24);
            this.txtHead1.Name = "txtHead1";
            this.txtHead1.Size = new System.Drawing.Size(218, 20);
            this.txtHead1.TabIndex = 3;
            this.txtHead1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHead1_KeyDown);
            this.txtHead1.Leave += new System.EventHandler(this.txtHead1_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 5580;
            this.label5.Text = "Receipe Head1 :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlNutrition);
            this.panel1.Controls.Add(this.dgNutrition);
            this.panel1.Controls.Add(this.txtHead);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(26, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 178);
            this.panel1.TabIndex = 5577;
            this.panel1.TabStop = false;
            this.panel1.Text = "Nutrition";
            // 
            // pnlNutrition
            // 
            this.pnlNutrition.Controls.Add(this.dgListNutrition);
            this.pnlNutrition.Location = new System.Drawing.Point(575, 54);
            this.pnlNutrition.Name = "pnlNutrition";
            this.pnlNutrition.Size = new System.Drawing.Size(307, 148);
            this.pnlNutrition.TabIndex = 5581;
            this.pnlNutrition.Visible = false;
            // 
            // dgListNutrition
            // 
            this.dgListNutrition.AllowUserToAddRows = false;
            this.dgListNutrition.AllowUserToDeleteRows = false;
            this.dgListNutrition.AllowUserToResizeColumns = false;
            this.dgListNutrition.AllowUserToResizeRows = false;
            this.dgListNutrition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgListNutrition.ColumnHeadersVisible = false;
            this.dgListNutrition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NutritionName,
            this.NutritionUOM,
            this.LstNutritionNo});
            this.dgListNutrition.Location = new System.Drawing.Point(4, 5);
            this.dgListNutrition.Name = "dgListNutrition";
            this.dgListNutrition.ReadOnly = true;
            this.dgListNutrition.RowHeadersVisible = false;
            this.dgListNutrition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgListNutrition.Size = new System.Drawing.Size(294, 202);
            this.dgListNutrition.TabIndex = 0;
            this.dgListNutrition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgListNutrition_KeyDown);
            // 
            // NutritionName
            // 
            this.NutritionName.HeaderText = "Nutrition Name";
            this.NutritionName.Name = "NutritionName";
            this.NutritionName.ReadOnly = true;
            this.NutritionName.Width = 180;
            // 
            // NutritionUOM
            // 
            this.NutritionUOM.HeaderText = "UOM";
            this.NutritionUOM.Name = "NutritionUOM";
            this.NutritionUOM.ReadOnly = true;
            this.NutritionUOM.Width = 60;
            // 
            // LstNutritionNo
            // 
            this.LstNutritionNo.HeaderText = "NutritionNo";
            this.LstNutritionNo.Name = "LstNutritionNo";
            this.LstNutritionNo.ReadOnly = true;
            this.LstNutritionNo.Visible = false;
            // 
            // dgNutrition
            // 
            this.dgNutrition.AllowUserToAddRows = false;
            this.dgNutrition.AllowUserToDeleteRows = false;
            this.dgNutrition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNutrition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.Nutrition,
            this.Value,
            this.UOM,
            this.NutritionNo,
            this.NPkSrNo,
            this.NStatus});
            this.dgNutrition.Location = new System.Drawing.Point(6, 52);
            this.dgNutrition.Name = "dgNutrition";
            this.dgNutrition.Size = new System.Drawing.Size(563, 121);
            this.dgNutrition.TabIndex = 2;
            this.dgNutrition.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgNutrition_CellFormatting);
            this.dgNutrition.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgNutrition_CellEndEdit);
            this.dgNutrition.CurrentCellChanged += new System.EventHandler(this.dgNutrition_CurrentCellChanged);
            this.dgNutrition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgNutrition_KeyDown);
            // 
            // txtHead
            // 
            this.txtHead.Location = new System.Drawing.Point(103, 18);
            this.txtHead.Name = "txtHead";
            this.txtHead.Size = new System.Drawing.Size(467, 20);
            this.txtHead.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5578;
            this.label3.Text = "Head :";
            // 
            // txtIngredient
            // 
            this.txtIngredient.Location = new System.Drawing.Point(129, 29);
            this.txtIngredient.Multiline = true;
            this.txtIngredient.Name = "txtIngredient";
            this.txtIngredient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIngredient.Size = new System.Drawing.Size(467, 49);
            this.txtIngredient.TabIndex = 0;
            this.txtIngredient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIngredient_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5575;
            this.label1.Text = "Ingredient :";
            // 
            // lblItemName
            // 
            this.lblItemName.BackColor = System.Drawing.Color.FromArgb(255, 93, 173, 226);
            this.lblItemName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblItemName.ForeColor = System.Drawing.Color.White;
            this.lblItemName.Location = new System.Drawing.Point(0, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(619, 18);
            this.lblItemName.TabIndex = 5574;
            this.lblItemName.Text = "label16";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "SrNo";
            this.SrNo.Name = "SrNo";
            this.SrNo.ReadOnly = true;
            this.SrNo.Width = 40;
            // 
            // Nutrition
            // 
            this.Nutrition.HeaderText = "Nutrition";
            this.Nutrition.Name = "Nutrition";
            this.Nutrition.ReadOnly = true;
            this.Nutrition.Width = 250;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 150;
            // 
            // UOM
            // 
            this.UOM.HeaderText = "UOM";
            this.UOM.Name = "UOM";
            this.UOM.Width = 80;
            // 
            // NutritionNo
            // 
            this.NutritionNo.HeaderText = "NutritionNo";
            this.NutritionNo.Name = "NutritionNo";
            this.NutritionNo.Visible = false;
            // 
            // NPkSrNo
            // 
            this.NPkSrNo.HeaderText = "NPkSrNo";
            this.NPkSrNo.Name = "NPkSrNo";
            this.NPkSrNo.Visible = false;
            // 
            // NStatus
            // 
            this.NStatus.HeaderText = "NStatus";
            this.NStatus.Name = "NStatus";
            this.NStatus.Visible = false;
            // 
            // StockItemIngredientDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 578);
            this.Controls.Add(this.pnlMain);
            this.Name = "StockItemIngredientDetails";
            this.Text = "Stock Item Ingredient Details";
            this.Load += new System.EventHandler(this.StockItemIngredientDetails_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipe)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlNutrition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgListNutrition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNutrition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMBPanel pnlMain;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.GroupBox panel1;
        private System.Windows.Forms.TextBox txtIngredient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox panel2;
        private System.Windows.Forms.TextBox txtLangHead1;
        private System.Windows.Forms.TextBox txtHead1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgNutrition;
        private System.Windows.Forms.TextBox txtHead;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgReceipe;
        private System.Windows.Forms.TextBox txtLangHead3;
        private System.Windows.Forms.TextBox txtHead3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLangHead2;
        private System.Windows.Forms.TextBox txtHead2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlNutrition;
        private System.Windows.Forms.DataGridView dgListNutrition;
        private System.Windows.Forms.DataGridViewTextBoxColumn NutritionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NutritionUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LstNutritionNo;
        private System.Windows.Forms.Button btnHead3;
        private System.Windows.Forms.Button btnHead2;
        private System.Windows.Forms.Button btnHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn rSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceipeEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceipeMar;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nutrition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NutritionNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPkSrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NStatus;
    }
}