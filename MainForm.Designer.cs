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
            menuStrip_Main = new DarkUI.Controls.DarkMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveConfigToolStripMenuItem = new ToolStripMenuItem();
            loadConfigToolStripMenuItem = new ToolStripMenuItem();
            compareModFilesToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            addFileToolStripMenuItem = new ToolStripMenuItem();
            addOptionToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            tlp_Main = new TableLayoutPanel();
            comboBox_File = new DarkUI.Controls.DarkComboBox();
            groupBox_Paths = new DarkUI.Controls.DarkGroupBox();
            tlp_Paths = new TableLayoutPanel();
            btn_OutputPath = new DarkUI.Controls.DarkButton();
            lbl_GamePath = new DarkUI.Controls.DarkLabel();
            lbl_OutputPath = new DarkUI.Controls.DarkLabel();
            txt_GamePath = new DarkUI.Controls.DarkTextBox();
            txt_OutputPath = new DarkUI.Controls.DarkTextBox();
            btn_GamePath = new DarkUI.Controls.DarkButton();
            btn_GenerateMod = new DarkUI.Controls.DarkButton();
            pnl_Main = new Panel();
            lbl_ChooseFile = new DarkUI.Controls.DarkLabel();
            bYMLToYMLToolStripMenuItem = new ToolStripMenuItem();
            menuStrip_Main.SuspendLayout();
            tlp_Main.SuspendLayout();
            groupBox_Paths.SuspendLayout();
            tlp_Paths.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip_Main
            // 
            menuStrip_Main.BackColor = Color.FromArgb(60, 63, 65);
            menuStrip_Main.ForeColor = Color.FromArgb(220, 220, 220);
            menuStrip_Main.ImageScalingSize = new Size(20, 20);
            menuStrip_Main.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, addToolStripMenuItem, settingsToolStripMenuItem });
            menuStrip_Main.Location = new Point(0, 0);
            menuStrip_Main.Name = "menuStrip_Main";
            menuStrip_Main.Padding = new Padding(3, 2, 0, 2);
            menuStrip_Main.Size = new Size(800, 28);
            menuStrip_Main.TabIndex = 0;
            menuStrip_Main.Text = "darkMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveConfigToolStripMenuItem, loadConfigToolStripMenuItem, compareModFilesToolStripMenuItem, bYMLToYMLToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveConfigToolStripMenuItem
            // 
            saveConfigToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            saveConfigToolStripMenuItem.Enabled = false;
            saveConfigToolStripMenuItem.ForeColor = Color.FromArgb(153, 153, 153);
            saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            saveConfigToolStripMenuItem.Size = new Size(230, 26);
            saveConfigToolStripMenuItem.Text = "Save Config";
            saveConfigToolStripMenuItem.Click += SaveConfig_Click;
            // 
            // loadConfigToolStripMenuItem
            // 
            loadConfigToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            loadConfigToolStripMenuItem.Enabled = false;
            loadConfigToolStripMenuItem.ForeColor = Color.FromArgb(153, 153, 153);
            loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            loadConfigToolStripMenuItem.Size = new Size(230, 26);
            loadConfigToolStripMenuItem.Text = "Load Config";
            loadConfigToolStripMenuItem.Click += LoadConfig_Click;
            // 
            // compareModFilesToolStripMenuItem
            // 
            compareModFilesToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            compareModFilesToolStripMenuItem.Enabled = false;
            compareModFilesToolStripMenuItem.ForeColor = Color.FromArgb(153, 153, 153);
            compareModFilesToolStripMenuItem.Name = "compareModFilesToolStripMenuItem";
            compareModFilesToolStripMenuItem.Size = new Size(230, 26);
            compareModFilesToolStripMenuItem.Text = "Compare Mod Files...";
            compareModFilesToolStripMenuItem.Click += Compare_Click;
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addFileToolStripMenuItem, addOptionToolStripMenuItem });
            addToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(60, 24);
            addToolStripMenuItem.Text = "Add...";
            // 
            // addFileToolStripMenuItem
            // 
            addFileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addFileToolStripMenuItem.Enabled = false;
            addFileToolStripMenuItem.ForeColor = Color.FromArgb(153, 153, 153);
            addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            addFileToolStripMenuItem.Size = new Size(138, 26);
            addFileToolStripMenuItem.Text = "File";
            // 
            // addOptionToolStripMenuItem
            // 
            addOptionToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            addOptionToolStripMenuItem.Enabled = false;
            addOptionToolStripMenuItem.ForeColor = Color.FromArgb(153, 153, 153);
            addOptionToolStripMenuItem.Name = "addOptionToolStripMenuItem";
            addOptionToolStripMenuItem.Size = new Size(138, 26);
            addOptionToolStripMenuItem.Text = "Option";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            settingsToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(76, 24);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += Settings_Click;
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 2;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlp_Main.Controls.Add(comboBox_File, 0, 1);
            tlp_Main.Controls.Add(groupBox_Paths, 0, 0);
            tlp_Main.Controls.Add(btn_GenerateMod, 1, 3);
            tlp_Main.Controls.Add(pnl_Main, 0, 2);
            tlp_Main.Controls.Add(lbl_ChooseFile, 1, 1);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 28);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 4;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tlp_Main.Size = new Size(800, 422);
            tlp_Main.TabIndex = 1;
            // 
            // comboBox_File
            // 
            comboBox_File.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            comboBox_File.DrawMode = DrawMode.OwnerDrawVariable;
            comboBox_File.Enabled = false;
            comboBox_File.FormattingEnabled = true;
            comboBox_File.Location = new Point(10, 112);
            comboBox_File.Margin = new Padding(10, 3, 3, 3);
            comboBox_File.Name = "comboBox_File";
            comboBox_File.Size = new Size(547, 28);
            comboBox_File.TabIndex = 1;
            comboBox_File.SelectedIndexChanged += SelectedFile_Changed;
            // 
            // groupBox_Paths
            // 
            groupBox_Paths.BorderColor = Color.FromArgb(51, 51, 51);
            tlp_Main.SetColumnSpan(groupBox_Paths, 2);
            groupBox_Paths.Controls.Add(tlp_Paths);
            groupBox_Paths.Dock = DockStyle.Fill;
            groupBox_Paths.Location = new Point(3, 3);
            groupBox_Paths.Name = "groupBox_Paths";
            groupBox_Paths.Size = new Size(794, 99);
            groupBox_Paths.TabIndex = 1;
            groupBox_Paths.TabStop = false;
            groupBox_Paths.Text = "Paths";
            // 
            // tlp_Paths
            // 
            tlp_Paths.ColumnCount = 3;
            tlp_Paths.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp_Paths.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp_Paths.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tlp_Paths.Controls.Add(btn_OutputPath, 2, 1);
            tlp_Paths.Controls.Add(lbl_GamePath, 0, 0);
            tlp_Paths.Controls.Add(lbl_OutputPath, 0, 1);
            tlp_Paths.Controls.Add(txt_GamePath, 1, 0);
            tlp_Paths.Controls.Add(txt_OutputPath, 1, 1);
            tlp_Paths.Controls.Add(btn_GamePath, 2, 0);
            tlp_Paths.Dock = DockStyle.Fill;
            tlp_Paths.Location = new Point(3, 23);
            tlp_Paths.Name = "tlp_Paths";
            tlp_Paths.RowCount = 2;
            tlp_Paths.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Paths.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Paths.Size = new Size(788, 73);
            tlp_Paths.TabIndex = 0;
            // 
            // btn_OutputPath
            // 
            btn_OutputPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_OutputPath.Location = new Point(712, 40);
            btn_OutputPath.Name = "btn_OutputPath";
            btn_OutputPath.Padding = new Padding(5);
            btn_OutputPath.Size = new Size(73, 29);
            btn_OutputPath.TabIndex = 5;
            btn_OutputPath.Text = ". . .";
            btn_OutputPath.Click += OutputPath_Click;
            // 
            // lbl_GamePath
            // 
            lbl_GamePath.Anchor = AnchorStyles.Right;
            lbl_GamePath.AutoSize = true;
            lbl_GamePath.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_GamePath.Location = new Point(45, 8);
            lbl_GamePath.Name = "lbl_GamePath";
            lbl_GamePath.Size = new Size(149, 20);
            lbl_GamePath.TabIndex = 0;
            lbl_GamePath.Text = "Game Files Directory:";
            // 
            // lbl_OutputPath
            // 
            lbl_OutputPath.Anchor = AnchorStyles.Right;
            lbl_OutputPath.AutoSize = true;
            lbl_OutputPath.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_OutputPath.Location = new Point(71, 44);
            lbl_OutputPath.Name = "lbl_OutputPath";
            lbl_OutputPath.Size = new Size(123, 20);
            lbl_OutputPath.TabIndex = 1;
            lbl_OutputPath.Text = "Output Directory:";
            // 
            // txt_GamePath
            // 
            txt_GamePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txt_GamePath.BackColor = Color.FromArgb(69, 73, 74);
            txt_GamePath.BorderStyle = BorderStyle.FixedSingle;
            txt_GamePath.ForeColor = Color.FromArgb(220, 220, 220);
            txt_GamePath.Location = new Point(200, 4);
            txt_GamePath.Name = "txt_GamePath";
            txt_GamePath.Size = new Size(506, 27);
            txt_GamePath.TabIndex = 2;
            txt_GamePath.TextChanged += Path_Changed;
            // 
            // txt_OutputPath
            // 
            txt_OutputPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txt_OutputPath.BackColor = Color.FromArgb(69, 73, 74);
            txt_OutputPath.BorderStyle = BorderStyle.FixedSingle;
            txt_OutputPath.ForeColor = Color.FromArgb(220, 220, 220);
            txt_OutputPath.Location = new Point(200, 41);
            txt_OutputPath.Name = "txt_OutputPath";
            txt_OutputPath.Size = new Size(506, 27);
            txt_OutputPath.TabIndex = 3;
            txt_OutputPath.TextChanged += Path_Changed;
            // 
            // btn_GamePath
            // 
            btn_GamePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_GamePath.Location = new Point(712, 3);
            btn_GamePath.Name = "btn_GamePath";
            btn_GamePath.Padding = new Padding(5);
            btn_GamePath.Size = new Size(73, 29);
            btn_GamePath.TabIndex = 4;
            btn_GamePath.Text = ". . .";
            btn_GamePath.Click += GamePath_Click;
            // 
            // btn_GenerateMod
            // 
            btn_GenerateMod.Dock = DockStyle.Fill;
            btn_GenerateMod.Enabled = false;
            btn_GenerateMod.Location = new Point(570, 368);
            btn_GenerateMod.Margin = new Padding(10);
            btn_GenerateMod.Name = "btn_GenerateMod";
            btn_GenerateMod.Padding = new Padding(15);
            btn_GenerateMod.Size = new Size(220, 44);
            btn_GenerateMod.TabIndex = 0;
            btn_GenerateMod.Text = "Generate Mod";
            btn_GenerateMod.Click += GenerateMod_Click;
            // 
            // pnl_Main
            // 
            pnl_Main.AutoScroll = true;
            tlp_Main.SetColumnSpan(pnl_Main, 2);
            pnl_Main.Dock = DockStyle.Fill;
            pnl_Main.Location = new Point(3, 150);
            pnl_Main.Name = "pnl_Main";
            pnl_Main.Size = new Size(794, 205);
            pnl_Main.TabIndex = 2;
            // 
            // lbl_ChooseFile
            // 
            lbl_ChooseFile.Anchor = AnchorStyles.Left;
            lbl_ChooseFile.AutoSize = true;
            lbl_ChooseFile.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_ChooseFile.Location = new Point(563, 116);
            lbl_ChooseFile.Name = "lbl_ChooseFile";
            lbl_ChooseFile.Size = new Size(218, 20);
            lbl_ChooseFile.TabIndex = 3;
            lbl_ChooseFile.Text = "<= Choose a File to Get Started";
            lbl_ChooseFile.Visible = false;
            // 
            // bYMLToYMLToolStripMenuItem
            // 
            bYMLToYMLToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            bYMLToYMLToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            bYMLToYMLToolStripMenuItem.Name = "bYMLToYMLToolStripMenuItem";
            bYMLToYMLToolStripMenuItem.Size = new Size(230, 26);
            bYMLToYMLToolStripMenuItem.Text = "BYML to YML";
            bYMLToYMLToolStripMenuItem.Click += BymlToYml_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tlp_Main);
            Controls.Add(menuStrip_Main);
            MainMenuStrip = menuStrip_Main;
            Name = "MainForm";
            Text = "TotK Actor Repacker";
            menuStrip_Main.ResumeLayout(false);
            menuStrip_Main.PerformLayout();
            tlp_Main.ResumeLayout(false);
            tlp_Main.PerformLayout();
            groupBox_Paths.ResumeLayout(false);
            tlp_Paths.ResumeLayout(false);
            tlp_Paths.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem addFileToolStripMenuItem;
        private ToolStripMenuItem addOptionToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private DarkUI.Controls.DarkLabel lbl_ChooseFile;
        private ToolStripMenuItem compareModFilesToolStripMenuItem;
        private ToolStripMenuItem bYMLToYMLToolStripMenuItem;
    }
}