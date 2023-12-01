using MetroSet_UI.Forms;

namespace TOTKActorRepack
{
    partial class MainForm : MetroSetForm
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
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_GameSettings = new System.Windows.Forms.TableLayoutPanel();
            this.txt_GameVersion = new System.Windows.Forms.TextBox();
            this.lbl_Gameversion = new System.Windows.Forms.Label();
            this.lbl_GameFiles = new System.Windows.Forms.Label();
            this.txt_GameFilesDir = new System.Windows.Forms.TextBox();
            this.groupBox_BYMLs = new System.Windows.Forms.GroupBox();
            this.listBox_BYMLs = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_BYMLs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_EditedValues = new System.Windows.Forms.GroupBox();
            this.listBox_Changes = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_Changes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFieldNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCommentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGrid_Changes = new System.Windows.Forms.PropertyGrid();
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportModFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlp_Main.SuspendLayout();
            this.tlp_GameSettings.SuspendLayout();
            this.groupBox_BYMLs.SuspendLayout();
            this.contextMenuStrip_BYMLs.SuspendLayout();
            this.groupBox_EditedValues.SuspendLayout();
            this.contextMenuStrip_Changes.SuspendLayout();
            this.menuStrip_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.tlp_GameSettings, 0, 0);
            this.tlp_Main.Controls.Add(this.groupBox_BYMLs, 1, 0);
            this.tlp_Main.Controls.Add(this.groupBox_EditedValues, 1, 1);
            this.tlp_Main.Controls.Add(this.propertyGrid_Changes, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(2, 28);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Size = new System.Drawing.Size(796, 420);
            this.tlp_Main.TabIndex = 0;
            // 
            // tlp_GameSettings
            // 
            this.tlp_GameSettings.ColumnCount = 2;
            this.tlp_GameSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.27551F));
            this.tlp_GameSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.72449F));
            this.tlp_GameSettings.Controls.Add(this.txt_GameVersion, 1, 1);
            this.tlp_GameSettings.Controls.Add(this.lbl_Gameversion, 0, 1);
            this.tlp_GameSettings.Controls.Add(this.lbl_GameFiles, 0, 0);
            this.tlp_GameSettings.Controls.Add(this.txt_GameFilesDir, 1, 0);
            this.tlp_GameSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_GameSettings.Location = new System.Drawing.Point(3, 3);
            this.tlp_GameSettings.Name = "tlp_GameSettings";
            this.tlp_GameSettings.RowCount = 2;
            this.tlp_GameSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_GameSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_GameSettings.Size = new System.Drawing.Size(392, 204);
            this.tlp_GameSettings.TabIndex = 0;
            // 
            // txt_GameVersion
            // 
            this.txt_GameVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_GameVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_GameVersion.Location = new System.Drawing.Point(106, 137);
            this.txt_GameVersion.Name = "txt_GameVersion";
            this.txt_GameVersion.Size = new System.Drawing.Size(283, 32);
            this.txt_GameVersion.TabIndex = 3;
            this.txt_GameVersion.Text = "1.2.1";
            this.txt_GameVersion.TextChanged += new System.EventHandler(this.GameVer_Changed);
            // 
            // lbl_Gameversion
            // 
            this.lbl_Gameversion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Gameversion.AutoSize = true;
            this.lbl_Gameversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_Gameversion.ForeColor = System.Drawing.Color.Black;
            this.lbl_Gameversion.Location = new System.Drawing.Point(29, 133);
            this.lbl_Gameversion.Name = "lbl_Gameversion";
            this.lbl_Gameversion.Size = new System.Drawing.Size(71, 40);
            this.lbl_Gameversion.TabIndex = 2;
            this.lbl_Gameversion.Text = "Game Version:";
            // 
            // lbl_GameFiles
            // 
            this.lbl_GameFiles.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_GameFiles.AutoSize = true;
            this.lbl_GameFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbl_GameFiles.ForeColor = System.Drawing.Color.Black;
            this.lbl_GameFiles.Location = new System.Drawing.Point(5, 31);
            this.lbl_GameFiles.Name = "lbl_GameFiles";
            this.lbl_GameFiles.Size = new System.Drawing.Size(95, 40);
            this.lbl_GameFiles.TabIndex = 0;
            this.lbl_GameFiles.Text = "Game Files Directory:";
            // 
            // txt_GameFilesDir
            // 
            this.txt_GameFilesDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_GameFilesDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_GameFilesDir.Location = new System.Drawing.Point(106, 35);
            this.txt_GameFilesDir.Name = "txt_GameFilesDir";
            this.txt_GameFilesDir.Size = new System.Drawing.Size(283, 32);
            this.txt_GameFilesDir.TabIndex = 1;
            this.txt_GameFilesDir.TextChanged += new System.EventHandler(this.GameFileDir_Changed);
            // 
            // groupBox_BYMLs
            // 
            this.groupBox_BYMLs.Controls.Add(this.listBox_BYMLs);
            this.groupBox_BYMLs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_BYMLs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox_BYMLs.Location = new System.Drawing.Point(401, 3);
            this.groupBox_BYMLs.Name = "groupBox_BYMLs";
            this.groupBox_BYMLs.Size = new System.Drawing.Size(392, 204);
            this.groupBox_BYMLs.TabIndex = 1;
            this.groupBox_BYMLs.TabStop = false;
            this.groupBox_BYMLs.Text = "Edited BYMLs";
            // 
            // listBox_BYMLs
            // 
            this.listBox_BYMLs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_BYMLs.ContextMenuStrip = this.contextMenuStrip_BYMLs;
            this.listBox_BYMLs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_BYMLs.FormattingEnabled = true;
            this.listBox_BYMLs.ItemHeight = 20;
            this.listBox_BYMLs.Location = new System.Drawing.Point(3, 22);
            this.listBox_BYMLs.Name = "listBox_BYMLs";
            this.listBox_BYMLs.Size = new System.Drawing.Size(386, 179);
            this.listBox_BYMLs.TabIndex = 0;
            this.listBox_BYMLs.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // contextMenuStrip_BYMLs
            // 
            this.contextMenuStrip_BYMLs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_BYMLs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.setPathToolStripMenuItem,
            this.setCommentToolStripMenuItem});
            this.contextMenuStrip_BYMLs.Name = "contextMenuStrip_BYMLs";
            this.contextMenuStrip_BYMLs.Size = new System.Drawing.Size(169, 100);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddBYML_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveByml_Click);
            // 
            // setPathToolStripMenuItem
            // 
            this.setPathToolStripMenuItem.Name = "setPathToolStripMenuItem";
            this.setPathToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.setPathToolStripMenuItem.Text = "Set Path";
            this.setPathToolStripMenuItem.Click += new System.EventHandler(this.SetBymlPath_Click);
            // 
            // setCommentToolStripMenuItem
            // 
            this.setCommentToolStripMenuItem.Name = "setCommentToolStripMenuItem";
            this.setCommentToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.setCommentToolStripMenuItem.Text = "Set Comment";
            this.setCommentToolStripMenuItem.Click += new System.EventHandler(this.SetBymlComment_Click);
            // 
            // groupBox_EditedValues
            // 
            this.groupBox_EditedValues.Controls.Add(this.listBox_Changes);
            this.groupBox_EditedValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_EditedValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox_EditedValues.Location = new System.Drawing.Point(401, 213);
            this.groupBox_EditedValues.Name = "groupBox_EditedValues";
            this.groupBox_EditedValues.Size = new System.Drawing.Size(392, 204);
            this.groupBox_EditedValues.TabIndex = 2;
            this.groupBox_EditedValues.TabStop = false;
            this.groupBox_EditedValues.Text = "Edited Values";
            // 
            // listBox_Changes
            // 
            this.listBox_Changes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_Changes.ContextMenuStrip = this.contextMenuStrip_Changes;
            this.listBox_Changes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Changes.FormattingEnabled = true;
            this.listBox_Changes.ItemHeight = 20;
            this.listBox_Changes.Location = new System.Drawing.Point(3, 22);
            this.listBox_Changes.Name = "listBox_Changes";
            this.listBox_Changes.Size = new System.Drawing.Size(386, 179);
            this.listBox_Changes.TabIndex = 1;
            this.listBox_Changes.SelectedIndexChanged += new System.EventHandler(this.SelectedChange_Changed);
            // 
            // contextMenuStrip_Changes
            // 
            this.contextMenuStrip_Changes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Changes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.removeToolStripMenuItem1,
            this.setNameToolStripMenuItem,
            this.setFieldNameToolStripMenuItem,
            this.setCommentToolStripMenuItem1});
            this.contextMenuStrip_Changes.Name = "contextMenuStrip_Changes";
            this.contextMenuStrip_Changes.Size = new System.Drawing.Size(198, 124);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(197, 24);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.AddChange_Click);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(197, 24);
            this.removeToolStripMenuItem1.Text = "Remove";
            // 
            // setNameToolStripMenuItem
            // 
            this.setNameToolStripMenuItem.Name = "setNameToolStripMenuItem";
            this.setNameToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.setNameToolStripMenuItem.Text = "Set Change Name";
            // 
            // setFieldNameToolStripMenuItem
            // 
            this.setFieldNameToolStripMenuItem.Name = "setFieldNameToolStripMenuItem";
            this.setFieldNameToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.setFieldNameToolStripMenuItem.Text = "Set Field Name";
            // 
            // setCommentToolStripMenuItem1
            // 
            this.setCommentToolStripMenuItem1.Name = "setCommentToolStripMenuItem1";
            this.setCommentToolStripMenuItem1.Size = new System.Drawing.Size(197, 24);
            this.setCommentToolStripMenuItem1.Text = "Set Comment";
            // 
            // propertyGrid_Changes
            // 
            this.propertyGrid_Changes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_Changes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.propertyGrid_Changes.LineColor = System.Drawing.SystemColors.Highlight;
            this.propertyGrid_Changes.Location = new System.Drawing.Point(3, 213);
            this.propertyGrid_Changes.Name = "propertyGrid_Changes";
            this.propertyGrid_Changes.Size = new System.Drawing.Size(392, 204);
            this.propertyGrid_Changes.TabIndex = 3;
            this.propertyGrid_Changes.ToolbarVisible = false;
            this.propertyGrid_Changes.ViewBackColor = System.Drawing.SystemColors.Highlight;
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toggleThemeToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(2, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Size = new System.Drawing.Size(796, 28);
            this.menuStrip_Main.TabIndex = 1;
            this.menuStrip_Main.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.exportModFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.SaveProject_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.LoadProject_Click);
            // 
            // exportModFilesToolStripMenuItem
            // 
            this.exportModFilesToolStripMenuItem.Name = "exportModFilesToolStripMenuItem";
            this.exportModFilesToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.exportModFilesToolStripMenuItem.Text = "Export Mod Files";
            // 
            // toggleThemeToolStripMenuItem
            // 
            this.toggleThemeToolStripMenuItem.Name = "toggleThemeToolStripMenuItem";
            this.toggleThemeToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.toggleThemeToolStripMenuItem.Text = "Toggle Theme";
            this.toggleThemeToolStripMenuItem.Click += new System.EventHandler(this.ToggleTheme_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.menuStrip_Main);
            this.DropShadowEffect = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.HeaderHeight = -40;
            this.MainMenuStrip = this.menuStrip_Main;
            this.Name = "MainForm";
            this.Opacity = 0.99D;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowHeader = true;
            this.ShowLeftRect = false;
            this.ShowTitle = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Style = MetroSet_UI.Enums.Style.Dark;
            this.Text = "TOTKActorRepack";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.tlp_Main.ResumeLayout(false);
            this.tlp_GameSettings.ResumeLayout(false);
            this.tlp_GameSettings.PerformLayout();
            this.groupBox_BYMLs.ResumeLayout(false);
            this.contextMenuStrip_BYMLs.ResumeLayout(false);
            this.groupBox_EditedValues.ResumeLayout(false);
            this.contextMenuStrip_Changes.ResumeLayout(false);
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.MenuStrip menuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleThemeToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlp_GameSettings;
        private System.Windows.Forms.Label lbl_GameFiles;
        private System.Windows.Forms.TextBox txt_GameFilesDir;
        private System.Windows.Forms.Label lbl_Gameversion;
        private System.Windows.Forms.TextBox txt_GameVersion;
        private System.Windows.Forms.GroupBox groupBox_BYMLs;
        private System.Windows.Forms.GroupBox groupBox_EditedValues;
        private System.Windows.Forms.ListBox listBox_BYMLs;
        private System.Windows.Forms.ToolStripMenuItem exportModFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_BYMLs;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCommentToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Changes;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFieldNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCommentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setPathToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid_Changes;
        private System.Windows.Forms.ListBox listBox_Changes;
    }
}

