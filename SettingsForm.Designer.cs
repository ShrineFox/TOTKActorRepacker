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
            this.tlp_SettingsMain = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Apply = new DarkUI.Controls.DarkButton();
            this.lbl_Game = new DarkUI.Controls.DarkLabel();
            this.lbl_Version = new DarkUI.Controls.DarkLabel();
            this.lbl_Padding = new DarkUI.Controls.DarkLabel();
            this.num_Padding = new DarkUI.Controls.DarkNumericUpDown();
            this.txt_Version = new DarkUI.Controls.DarkTextBox();
            this.comboBox_Game = new DarkUI.Controls.DarkComboBox();
            this.lbl_DefaultJson = new DarkUI.Controls.DarkLabel();
            this.txt_DefaultJson = new DarkUI.Controls.DarkTextBox();
            this.tlp_SettingsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Padding)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_SettingsMain
            // 
            this.tlp_SettingsMain.ColumnCount = 2;
            this.tlp_SettingsMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.88889F));
            this.tlp_SettingsMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.11111F));
            this.tlp_SettingsMain.Controls.Add(this.btn_Apply, 1, 4);
            this.tlp_SettingsMain.Controls.Add(this.lbl_Game, 0, 0);
            this.tlp_SettingsMain.Controls.Add(this.lbl_Version, 0, 1);
            this.tlp_SettingsMain.Controls.Add(this.lbl_Padding, 0, 2);
            this.tlp_SettingsMain.Controls.Add(this.num_Padding, 1, 2);
            this.tlp_SettingsMain.Controls.Add(this.txt_Version, 1, 1);
            this.tlp_SettingsMain.Controls.Add(this.comboBox_Game, 1, 0);
            this.tlp_SettingsMain.Controls.Add(this.lbl_DefaultJson, 0, 3);
            this.tlp_SettingsMain.Controls.Add(this.txt_DefaultJson, 1, 3);
            this.tlp_SettingsMain.Location = new System.Drawing.Point(12, 12);
            this.tlp_SettingsMain.Name = "tlp_SettingsMain";
            this.tlp_SettingsMain.RowCount = 5;
            this.tlp_SettingsMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_SettingsMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_SettingsMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_SettingsMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_SettingsMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_SettingsMain.Size = new System.Drawing.Size(481, 242);
            this.tlp_SettingsMain.TabIndex = 0;
            // 
            // btn_Apply
            // 
            this.btn_Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Apply.Location = new System.Drawing.Point(190, 195);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Padding = new System.Windows.Forms.Padding(5);
            this.btn_Apply.Size = new System.Drawing.Size(288, 44);
            this.btn_Apply.TabIndex = 0;
            this.btn_Apply.Text = "Apply Changes";
            this.btn_Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // lbl_Game
            // 
            this.lbl_Game.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Game.AutoSize = true;
            this.lbl_Game.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Game.Location = new System.Drawing.Point(133, 14);
            this.lbl_Game.Name = "lbl_Game";
            this.lbl_Game.Size = new System.Drawing.Size(51, 20);
            this.lbl_Game.TabIndex = 1;
            this.lbl_Game.Text = "Game:";
            // 
            // lbl_Version
            // 
            this.lbl_Version.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Version.Location = new System.Drawing.Point(124, 62);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(60, 20);
            this.lbl_Version.TabIndex = 2;
            this.lbl_Version.Text = "Version:";
            // 
            // lbl_Padding
            // 
            this.lbl_Padding.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Padding.AutoSize = true;
            this.lbl_Padding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_Padding.Location = new System.Drawing.Point(80, 110);
            this.lbl_Padding.Name = "lbl_Padding";
            this.lbl_Padding.Size = new System.Drawing.Size(104, 20);
            this.lbl_Padding.TabIndex = 3;
            this.lbl_Padding.Text = "RSTB Padding:";
            // 
            // num_Padding
            // 
            this.num_Padding.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.num_Padding.Location = new System.Drawing.Point(197, 109);
            this.num_Padding.Margin = new System.Windows.Forms.Padding(10, 8, 3, 3);
            this.num_Padding.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.num_Padding.Name = "num_Padding";
            this.num_Padding.Size = new System.Drawing.Size(126, 27);
            this.num_Padding.TabIndex = 4;
            this.num_Padding.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // txt_Version
            // 
            this.txt_Version.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_Version.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_Version.Location = new System.Drawing.Point(197, 55);
            this.txt_Version.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.txt_Version.MaxLength = 9;
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(281, 27);
            this.txt_Version.TabIndex = 5;
            this.txt_Version.Text = "1.1.1";
            // 
            // comboBox_Game
            // 
            this.comboBox_Game.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox_Game.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Game.FormattingEnabled = true;
            this.comboBox_Game.Items.AddRange(new object[] {
            "Tears of the Kingdom"});
            this.comboBox_Game.Location = new System.Drawing.Point(197, 12);
            this.comboBox_Game.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.comboBox_Game.Name = "comboBox_Game";
            this.comboBox_Game.Size = new System.Drawing.Size(281, 28);
            this.comboBox_Game.TabIndex = 6;
            // 
            // lbl_DefaultJson
            // 
            this.lbl_DefaultJson.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_DefaultJson.AutoSize = true;
            this.lbl_DefaultJson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lbl_DefaultJson.Location = new System.Drawing.Point(57, 158);
            this.lbl_DefaultJson.Name = "lbl_DefaultJson";
            this.lbl_DefaultJson.Size = new System.Drawing.Size(127, 20);
            this.lbl_DefaultJson.TabIndex = 7;
            this.lbl_DefaultJson.Text = "Default .json Path:";
            // 
            // txt_DefaultJson
            // 
            this.txt_DefaultJson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txt_DefaultJson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DefaultJson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txt_DefaultJson.Location = new System.Drawing.Point(197, 151);
            this.txt_DefaultJson.Margin = new System.Windows.Forms.Padding(10, 7, 3, 3);
            this.txt_DefaultJson.MaxLength = 9;
            this.txt_DefaultJson.Name = "txt_DefaultJson";
            this.txt_DefaultJson.Size = new System.Drawing.Size(281, 27);
            this.txt_DefaultJson.TabIndex = 8;
            this.txt_DefaultJson.Text = "./totk.json";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 266);
            this.Controls.Add(this.tlp_SettingsMain);
            this.Name = "SettingsForm";
            this.Text = "TotK Actor Repacker - Settings";
            this.tlp_SettingsMain.ResumeLayout(false);
            this.tlp_SettingsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Padding)).EndInit();
            this.ResumeLayout(false);

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
    }
}