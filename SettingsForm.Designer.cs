namespace TOTKActorRepacker
{
    partial class SettingsForm : DarkUI.Forms.DarkForm
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
            tlp_SettingsMain = new TableLayoutPanel();
            btn_Apply = new DarkUI.Controls.DarkButton();
            lbl_Game = new DarkUI.Controls.DarkLabel();
            lbl_Version = new DarkUI.Controls.DarkLabel();
            lbl_Padding = new DarkUI.Controls.DarkLabel();
            num_Padding = new DarkUI.Controls.DarkNumericUpDown();
            txt_Version = new DarkUI.Controls.DarkTextBox();
            comboBox_Game = new DarkUI.Controls.DarkComboBox();
            lbl_DefaultJson = new DarkUI.Controls.DarkLabel();
            txt_DefaultJson = new DarkUI.Controls.DarkTextBox();
            chk_FullSARCRebuild = new DarkUI.Controls.DarkCheckBox();
            tlp_SettingsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_Padding).BeginInit();
            SuspendLayout();
            // 
            // tlp_SettingsMain
            // 
            tlp_SettingsMain.ColumnCount = 2;
            tlp_SettingsMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.88889F));
            tlp_SettingsMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.11111F));
            tlp_SettingsMain.Controls.Add(btn_Apply, 1, 4);
            tlp_SettingsMain.Controls.Add(lbl_Game, 0, 0);
            tlp_SettingsMain.Controls.Add(lbl_Version, 0, 1);
            tlp_SettingsMain.Controls.Add(lbl_Padding, 0, 2);
            tlp_SettingsMain.Controls.Add(num_Padding, 1, 2);
            tlp_SettingsMain.Controls.Add(txt_Version, 1, 1);
            tlp_SettingsMain.Controls.Add(comboBox_Game, 1, 0);
            tlp_SettingsMain.Controls.Add(lbl_DefaultJson, 0, 3);
            tlp_SettingsMain.Controls.Add(txt_DefaultJson, 1, 3);
            tlp_SettingsMain.Controls.Add(chk_FullSARCRebuild, 0, 4);
            tlp_SettingsMain.Location = new Point(12, 12);
            tlp_SettingsMain.Name = "tlp_SettingsMain";
            tlp_SettingsMain.RowCount = 5;
            tlp_SettingsMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_SettingsMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_SettingsMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_SettingsMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_SettingsMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_SettingsMain.Size = new Size(481, 242);
            tlp_SettingsMain.TabIndex = 0;
            // 
            // btn_Apply
            // 
            btn_Apply.DialogResult = DialogResult.OK;
            btn_Apply.Location = new Point(190, 195);
            btn_Apply.Name = "btn_Apply";
            btn_Apply.Padding = new Padding(5);
            btn_Apply.Size = new Size(288, 44);
            btn_Apply.TabIndex = 0;
            btn_Apply.Text = "Apply Changes";
            btn_Apply.Click += Apply_Click;
            // 
            // lbl_Game
            // 
            lbl_Game.Anchor = AnchorStyles.Right;
            lbl_Game.AutoSize = true;
            lbl_Game.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_Game.Location = new Point(133, 14);
            lbl_Game.Name = "lbl_Game";
            lbl_Game.Size = new Size(51, 20);
            lbl_Game.TabIndex = 1;
            lbl_Game.Text = "Game:";
            // 
            // lbl_Version
            // 
            lbl_Version.Anchor = AnchorStyles.Right;
            lbl_Version.AutoSize = true;
            lbl_Version.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_Version.Location = new Point(124, 62);
            lbl_Version.Name = "lbl_Version";
            lbl_Version.Size = new Size(60, 20);
            lbl_Version.TabIndex = 2;
            lbl_Version.Text = "Version:";
            // 
            // lbl_Padding
            // 
            lbl_Padding.Anchor = AnchorStyles.Right;
            lbl_Padding.AutoSize = true;
            lbl_Padding.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_Padding.Location = new Point(80, 110);
            lbl_Padding.Name = "lbl_Padding";
            lbl_Padding.Size = new Size(104, 20);
            lbl_Padding.TabIndex = 3;
            lbl_Padding.Text = "RSTB Padding:";
            // 
            // num_Padding
            // 
            num_Padding.Anchor = AnchorStyles.Left;
            num_Padding.Location = new Point(197, 109);
            num_Padding.Margin = new Padding(10, 8, 3, 3);
            num_Padding.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            num_Padding.Name = "num_Padding";
            num_Padding.Size = new Size(126, 27);
            num_Padding.TabIndex = 4;
            num_Padding.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // txt_Version
            // 
            txt_Version.BackColor = Color.FromArgb(69, 73, 74);
            txt_Version.BorderStyle = BorderStyle.FixedSingle;
            txt_Version.ForeColor = Color.FromArgb(220, 220, 220);
            txt_Version.Location = new Point(197, 55);
            txt_Version.Margin = new Padding(10, 7, 3, 3);
            txt_Version.MaxLength = 9;
            txt_Version.Name = "txt_Version";
            txt_Version.Size = new Size(281, 27);
            txt_Version.TabIndex = 5;
            txt_Version.Text = "1.1.1";
            // 
            // comboBox_Game
            // 
            comboBox_Game.Anchor = AnchorStyles.Left;
            comboBox_Game.DrawMode = DrawMode.OwnerDrawVariable;
            comboBox_Game.FormattingEnabled = true;
            comboBox_Game.Items.AddRange(new object[] { "Tears of the Kingdom" });
            comboBox_Game.Location = new Point(197, 12);
            comboBox_Game.Margin = new Padding(10, 7, 3, 3);
            comboBox_Game.Name = "comboBox_Game";
            comboBox_Game.Size = new Size(281, 28);
            comboBox_Game.TabIndex = 6;
            // 
            // lbl_DefaultJson
            // 
            lbl_DefaultJson.Anchor = AnchorStyles.Right;
            lbl_DefaultJson.AutoSize = true;
            lbl_DefaultJson.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_DefaultJson.Location = new Point(57, 158);
            lbl_DefaultJson.Name = "lbl_DefaultJson";
            lbl_DefaultJson.Size = new Size(127, 20);
            lbl_DefaultJson.TabIndex = 7;
            lbl_DefaultJson.Text = "Default .json Path:";
            // 
            // txt_DefaultJson
            // 
            txt_DefaultJson.BackColor = Color.FromArgb(69, 73, 74);
            txt_DefaultJson.BorderStyle = BorderStyle.FixedSingle;
            txt_DefaultJson.ForeColor = Color.FromArgb(220, 220, 220);
            txt_DefaultJson.Location = new Point(197, 151);
            txt_DefaultJson.Margin = new Padding(10, 7, 3, 3);
            txt_DefaultJson.MaxLength = 9;
            txt_DefaultJson.Name = "txt_DefaultJson";
            txt_DefaultJson.Size = new Size(281, 27);
            txt_DefaultJson.TabIndex = 8;
            txt_DefaultJson.Text = "./totk.json";
            // 
            // chk_FullSARCRebuild
            // 
            chk_FullSARCRebuild.Anchor = AnchorStyles.Right;
            chk_FullSARCRebuild.AutoSize = true;
            chk_FullSARCRebuild.Location = new Point(35, 205);
            chk_FullSARCRebuild.Name = "chk_FullSARCRebuild";
            chk_FullSARCRebuild.Size = new Size(149, 24);
            chk_FullSARCRebuild.TabIndex = 9;
            chk_FullSARCRebuild.Text = "Full SARC Rebuild";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 266);
            Controls.Add(tlp_SettingsMain);
            Name = "SettingsForm";
            Text = "TotK Actor Repacker - Settings";
            tlp_SettingsMain.ResumeLayout(false);
            tlp_SettingsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_Padding).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlp_SettingsMain;
        private DarkUI.Controls.DarkButton btn_Apply;
        private DarkUI.Controls.DarkLabel lbl_Game;
        private DarkUI.Controls.DarkLabel lbl_Version;
        private DarkUI.Controls.DarkLabel lbl_Padding;
        private DarkUI.Controls.DarkNumericUpDown num_Padding;
        private DarkUI.Controls.DarkTextBox txt_Version;
        private DarkUI.Controls.DarkComboBox comboBox_Game;
        private DarkUI.Controls.DarkLabel lbl_DefaultJson;
        private DarkUI.Controls.DarkTextBox txt_DefaultJson;
        private DarkUI.Controls.DarkCheckBox chk_FullSARCRebuild;
    }
}