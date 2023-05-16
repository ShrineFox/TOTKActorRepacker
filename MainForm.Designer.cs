namespace TOTKActorRepacker
{
    partial class MainForm : DarkUI.Forms.DarkForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip_Main = new DarkUI.Controls.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.btn_GenerateMod = new DarkUI.Controls.DarkButton();
            this.groupBox_Paths = new DarkUI.Controls.DarkGroupBox();
            this.comboBox_File = new DarkUI.Controls.DarkComboBox();
            this.tlp_Paths = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OutputPath = new DarkUI.Controls.DarkButton();
            this.lbl_GamePath = new DarkUI.Controls.DarkLabel();
            this.lbl_OutputPath = new DarkUI.Controls.DarkLabel();
            this.txt_GamePath = new DarkUI.Controls.DarkTextBox();
            this.txt_OutputPath = new DarkUI.Controls.DarkTextBox();
            this.btn_GamePath = new DarkUI.Controls.DarkButton();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.menuStrip_Main.SuspendLayout();
            this.tlp_Main.SuspendLayout();
            this.groupBox_Paths.SuspendLayout();
            this.tlp_Paths.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.menuStrip_Main.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.menuStrip_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip_Main.Size = new System.Drawing.Size(800, 28);
            this.menuStrip_Main.TabIndex = 0;
            this.menuStrip_Main.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveConfigToolStripMenuItem,
            this.loadConfigToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.saveConfigToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.loadConfigToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_Main.Controls.Add(this.btn_GenerateMod, 1, 2);
            this.tlp_Main.Controls.Add(this.groupBox_Paths, 0, 0);
            this.tlp_Main.Controls.Add(this.pnl_Main, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 28);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 3;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Main.Size = new System.Drawing.Size(800, 422);
            this.tlp_Main.TabIndex = 1;
            // 
            // btn_GenerateMod
            // 
            this.btn_GenerateMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_GenerateMod.Location = new System.Drawing.Point(563, 361);
            this.btn_GenerateMod.Name = "btn_GenerateMod";
            this.btn_GenerateMod.Padding = new System.Windows.Forms.Padding(5);
            this.btn_GenerateMod.Size = new System.Drawing.Size(234, 58);
            this.btn_GenerateMod.TabIndex = 0;
            this.btn_GenerateMod.Text = "Generate Mod";
            // 
            // groupBox_Paths
            // 
            this.groupBox_Paths.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tlp_Main.SetColumnSpan(this.groupBox_Paths, 2);
            this.groupBox_Paths.Controls.Add(this.comboBox_File);
            this.groupBox_Paths.Controls.Add(this.tlp_Paths);
            this.groupBox_Paths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Paths.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Paths.Name = "groupBox_Paths";
            this.groupBox_Paths.Size = new System.Drawing.Size(794, 99);
            this.groupBox_Paths.TabIndex = 1;
            this.groupBox_Paths.TabStop = false;
            this.groupBox_Paths.Text = "Paths";
            // 
            // comboBox_File
            // 
            this.comboBox_File.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_File.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_File.FormattingEnabled = true;
            this.comboBox_File.Location = new System.Drawing.Point(560, 0);
            this.comboBox_File.Name = "comboBox_File";
            this.comboBox_File.Size = new System.Drawing.Size(231, 28);
            this.comboBox_File.TabIndex = 1;
            // 
            // tlp_Paths
            // 
            this.tlp_Paths.ColumnCount = 3;
            this.tlp_Paths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Paths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlp_Paths.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_Paths.Controls.Add(this.btn_OutputPath, 2, 1);
            this.tlp_Paths.Controls.Add(this.lbl_GamePath, 0, 0);
            this.tlp_Paths.Controls.Add(this.lbl_OutputPath, 0, 1);
            this.tlp_Paths.Controls.Add(this.txt_GamePath, 1, 0);
            this.tlp_Paths.Controls.Add(this.txt_OutputPath, 1, 1);
            this.tlp_Paths.Controls.Add(this.btn_GamePath, 2, 0);
            this.tlp_Paths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Paths.Location = new System.Drawing.Point(3, 23);
            this.tlp_Paths.Name = "tlp_Paths";
            this.tlp_Paths.RowCount = 2;
            this.tlp_Paths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Paths.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Paths.Size = new System.Drawing.Size(788, 73);
            this.tlp_Paths.TabIndex = 0;
            // 
            // btn_OutputPath
            // 
            this.btn_OutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OutputPath.Location = new System.Drawing.Point(712, 40);
            this.btn_OutputPath.Name = "btn_OutputPath";
            this.btn_OutputPath.Padding = new System.Windows.Forms.Padding(5);
            this.btn_OutputPath.Size = new System.Drawing.Size(73, 29);
            this.btn_OutputPath.TabIndex = 5;
            this.btn_OutputPath.Text = ". . .";
            // 
            // lbl_GamePath
            // 
            this.lbl_GamePath.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_GamePath.AutoSize = true;
            this.lbl_GamePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_GamePath.Location = new System.Drawing.Point(12, 8);
            this.lbl_GamePath.Name = "lbl_GamePath";
            this.lbl_GamePath.Size = new System.Drawing.Size(182, 20);
            this.lbl_GamePath.TabIndex = 0;
            this.lbl_GamePath.Text = "Extracted Game Directory:";
            // 
            // lbl_OutputPath
            // 
            this.lbl_OutputPath.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_OutputPath.AutoSize = true;
            this.lbl_OutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_OutputPath.Location = new System.Drawing.Point(71, 44);
            this.lbl_OutputPath.Name = "lbl_OutputPath";
            this.lbl_OutputPath.Size = new System.Drawing.Size(123, 20);
            this.lbl_OutputPath.TabIndex = 1;
            this.lbl_OutputPath.Text = "Output Directory:";
            // 
            // txt_GamePath
            // 
            this.txt_GamePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_GamePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_GamePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_GamePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_GamePath.Location = new System.Drawing.Point(200, 4);
            this.txt_GamePath.Name = "txt_GamePath";
            this.txt_GamePath.Size = new System.Drawing.Size(506, 27);
            this.txt_GamePath.TabIndex = 2;
            // 
            // txt_OutputPath
            // 
            this.txt_OutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OutputPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_OutputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_OutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_OutputPath.Location = new System.Drawing.Point(200, 41);
            this.txt_OutputPath.Name = "txt_OutputPath";
            this.txt_OutputPath.Size = new System.Drawing.Size(506, 27);
            this.txt_OutputPath.TabIndex = 3;
            // 
            // btn_GamePath
            // 
            this.btn_GamePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GamePath.Location = new System.Drawing.Point(712, 3);
            this.btn_GamePath.Name = "btn_GamePath";
            this.btn_GamePath.Padding = new System.Windows.Forms.Padding(5);
            this.btn_GamePath.Size = new System.Drawing.Size(73, 29);
            this.btn_GamePath.TabIndex = 4;
            this.btn_GamePath.Text = ". . .";
            // 
            // pnl_Main
            // 
            this.pnl_Main.AutoScroll = true;
            this.tlp_Main.SetColumnSpan(this.pnl_Main, 2);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(3, 108);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(794, 247);
            this.pnl_Main.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.MainMenuStrip = this.menuStrip_Main;
            this.Name = "MainForm";
            this.Text = "TotK Actor Repacker";
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.tlp_Main.ResumeLayout(false);
            this.groupBox_Paths.ResumeLayout(false);
            this.tlp_Paths.ResumeLayout(false);
            this.tlp_Paths.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkMenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveConfigToolStripMenuItem;
        private ToolStripMenuItem loadConfigToolStripMenuItem;
        private TableLayoutPanel tlp_Main;
        private DarkUI.Controls.DarkButton btn_GenerateMod;
        private DarkUI.Controls.DarkGroupBox groupBox_Paths;
        private TableLayoutPanel tlp_Paths;
        private Panel pnl_Main;
        private DarkUI.Controls.DarkLabel lbl_GamePath;
        private DarkUI.Controls.DarkLabel lbl_OutputPath;
        private DarkUI.Controls.DarkTextBox txt_GamePath;
        private DarkUI.Controls.DarkTextBox txt_OutputPath;
        private DarkUI.Controls.DarkButton btn_OutputPath;
        private DarkUI.Controls.DarkButton btn_GamePath;
        private DarkUI.Controls.DarkComboBox comboBox_File;
    }
}