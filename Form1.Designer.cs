namespace Yadi
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.omTabControl1 = new OMControls.OMTabControl();
            this.tabPage1 = new OMControls.OMTabPage();
            this.tabPage2 = new OMControls.OMTabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.vsTabDrawer1 = new OMControls.VsTabDrawer();
            this.tabControl1 = new OMControls.OMTabControl();
            this.tabPage3 = new OMControls.OMTabPage();
            this.btnBack = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new OMControls.OMTabPage();
            this.xlTabDrawer1 = new OMControls.XlTabDrawer();
            this.button1 = new System.Windows.Forms.Button();
            this.omPopupNotify1 = new OMControls.OMPopupNotify();
            this.omTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // omTabControl1
            // 
            this.omTabControl1.ActiveColor = System.Drawing.Color.PaleGreen;
            this.omTabControl1.BackColor = System.Drawing.Color.LimeGreen;
            this.omTabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.omTabControl1.Controls.Add(this.tabPage1);
            this.omTabControl1.Controls.Add(this.tabPage2);
            this.omTabControl1.ImageIndex = -1;
            this.omTabControl1.ImageList = null;
            this.omTabControl1.InactiveColor = System.Drawing.Color.LimeGreen;
            this.omTabControl1.Location = new System.Drawing.Point(12, 12);
            this.omTabControl1.Name = "omTabControl1";
            this.omTabControl1.ScrollButtonStyle = OMControls.OMScrollButtonStyle.Never;
            this.omTabControl1.SelectedIndex = 0;
            this.omTabControl1.SelectedTab = this.tabPage1;
            this.omTabControl1.Size = new System.Drawing.Size(300, 311);
            this.omTabControl1.TabDock = System.Windows.Forms.DockStyle.Top;
            this.omTabControl1.TabDrawer = this.vsTabDrawer1;
            this.omTabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.omTabControl1.TabIndex = 0;
            this.omTabControl1.Text = "omTabControl1";
            this.omTabControl1.TabChanged += new System.EventHandler(this.omTabControl1_TabChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = -1;
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(292, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage2.ImageIndex = -1;
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(292, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // vsTabDrawer1
            // 
            this.vsTabDrawer1.GradientActiveBottom = System.Drawing.Color.LightGray;
            this.vsTabDrawer1.GradientActiveMiddle = System.Drawing.Color.White;
            this.vsTabDrawer1.GradientActiveShow = false;
            this.vsTabDrawer1.GradientActiveTop = System.Drawing.Color.Silver;
            this.vsTabDrawer1.GradientBottom = System.Drawing.Color.LightGray;
            this.vsTabDrawer1.GradientMiddle = System.Drawing.Color.White;
            this.vsTabDrawer1.GradientShow = false;
            this.vsTabDrawer1.GradientTop = System.Drawing.Color.Silver;
            // 
            // tabControl1
            // 
            this.tabControl1.ActiveColor = System.Drawing.SystemColors.Control;
            this.tabControl1.BackColor = System.Drawing.SystemColors.Control;
            this.tabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.ImageIndex = -1;
            this.tabControl1.ImageList = null;
            this.tabControl1.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.tabControl1.Location = new System.Drawing.Point(318, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.ScrollButtonStyle = OMControls.OMScrollButtonStyle.Always;
            this.tabControl1.SelectedIndex = 1;
            this.tabControl1.SelectedTab = this.tabPage4;
            this.tabControl1.Size = new System.Drawing.Size(277, 311);
            this.tabControl1.TabDock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.TabDrawer = null;
            this.tabControl1.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnBack);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.DataGridView1);
            this.tabPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage3.ImageIndex = -1;
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(269, 277);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.Location = new System.Drawing.Point(447, 396);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(77, 44);
            this.btnBack.TabIndex = 61;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(116)))), ((int)(((byte)(133)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 30);
            this.label5.TabIndex = 60;
            this.label5.Text = "Ledger Book";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(0, 61);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridView1.RowTemplate.Height = 27;
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView1.Size = new System.Drawing.Size(207, 183);
            this.DataGridView1.TabIndex = 59;
            // 
            // tabPage4
            // 
            this.tabPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage4.ImageIndex = -1;
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(269, 277);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(696, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // omPopupNotify1
            // 
            this.omPopupNotify1.ContentFont = new System.Drawing.Font("Tahoma", 8F);
            this.omPopupNotify1.ContentText = null;
            this.omPopupNotify1.Image = null;
            this.omPopupNotify1.OptionsMenu = null;
            this.omPopupNotify1.Size = new System.Drawing.Size(400, 100);
            this.omPopupNotify1.TitleFont = new System.Drawing.Font("Segoe UI", 9F);
            this.omPopupNotify1.TitleText = "Kamlesh";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1033, 658);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.omTabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.omTabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OMControls.OMTabControl omTabControl1;
        private OMControls.OMTabPage tabPage1;
        private OMControls.OMTabPage tabPage2;
        private OMControls.OMTabControl tabControl1;
        private OMControls.OMTabPage tabPage3;
        private OMControls.OMTabPage tabPage4;
        private OMControls.XlTabDrawer xlTabDrawer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private OMControls.VsTabDrawer vsTabDrawer1;
        private System.Windows.Forms.Button button1;
        private OMControls.OMPopupNotify omPopupNotify1;
    }
}