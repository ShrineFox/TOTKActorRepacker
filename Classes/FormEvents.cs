using DarkUI.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Cead;
using System.IO.Compression;
using Zstandard.Net;
using CsRestbl.Managed;
using System.Text;
using Soft160.Data.Cryptography;
using System.Runtime.InteropServices;
using ShrineFox.IO;
using static Cead.Sarc;

namespace TOTKActorRepacker
{
    public partial class MainForm : DarkUI.Forms.DarkForm
    {
        private void ValidateModGeneration()
        {
            if (options != null && options.Where(x => x.Enabled).Count() > 0)
                btn_GenerateMod.Enabled = true;
            else
                btn_GenerateMod.Enabled = false;
        }

        private void SetupLogging()
        {
            Output.Logging = true;
            Output.LogPath = "log.txt";
            Output.LogToFile = true;
#if DEBUG
            Output.VerboseLogging = true;
#endif
        }

        private void GenerateMod_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateMod();
            }
            catch
            {
                MessageBox.Show("There was an error generating the mod. " +
                    "This is known to happen at random for some reason.\n\nPlease try again!");
                Output.Log("\n\nMod generation failed", ConsoleColor.Red);
            }
        }


        private void LoadUserDefaults()
        {
            // Automatically set previously used paths
            txt_GamePath.Text = formSettings.GamePath;
            txt_OutputPath.Text = formSettings.OutputPath;

            // Automatically load default .json file if found
            if (File.Exists(formSettings.DefaultJson))
                LoadConfig(formSettings.DefaultJson);
        }

        private void GamePath_Click(object sender, EventArgs e)
        {
            string path = ChooseFolder("Choose extracted game folder");
            txt_GamePath.Text = path;
        }

        private void OutputPath_Click(object sender, EventArgs e)
        {
            string path = ChooseFolder("Choose output mod files folder");
            txt_OutputPath.Text = path;
        }

        public static string ChooseFolder(string windowTitle)
        {
            // Prompt the user to pick a folder path
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = windowTitle;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return "";
        }

        private void ValidatePaths()
        {
            // If paths are valid...
            if (Directory.Exists(txt_GamePath.Text) && Directory.Exists(txt_OutputPath.Text)
                && File.Exists(Path.Combine(txt_GamePath.Text, "Pack\\ZsDic.pack.zs")))
            {
                // Update config with chosen paths
                formSettings.GamePath = txt_GamePath.Text;
                formSettings.OutputPath = txt_OutputPath.Text;

                // Enable options if mod files are found & output folder is set
                btn_GenerateMod.Enabled = true;
                loadConfigToolStripMenuItem.Enabled = true;
                addFileToolStripMenuItem.Enabled = true;
                compareModFilesToolStripMenuItem.Enabled = true;
            }
            else
            {
                // Disiable options
                btn_GenerateMod.Enabled = false;
                loadConfigToolStripMenuItem.Enabled = false;
                addFileToolStripMenuItem.Enabled = false;
                compareModFilesToolStripMenuItem.Enabled = false;
            }
        }

        private void Path_Changed(object sender, EventArgs e)
        {
            ValidatePaths();
        }

        private void LoadConfig_Click(object sender, EventArgs e)
        {
            string file = ChooseFile("Choose config file", ".json");
            if (File.Exists(file))
            {
                LoadConfig(file);
            }
        }

        public void UpdateFilesList()
        {
            List<string> files = new List<string>();

            if (options.Count > 0)
            {
                // TODO: Iterate over changes in option
                // foreach (var option in options)
                //if (!files.Any(x => x.Equals(option.File)))
                //files.Add(option.File);

                comboBox_File.Items.Clear();
                // Add blank first item
                comboBox_File.Items.Add("");

                foreach (var file in files)
                    comboBox_File.Items.Add(file);

                comboBox_File.Enabled = true;
            }
            else
            {
                comboBox_File.Items.Clear();
                comboBox_File.Items.Add("");
                comboBox_File.Enabled = false;
            }
            comboBox_File.SelectedIndex = 0;
            lbl_ChooseFile.Visible = true;
        }

        private string ChooseFile(string title, string filter)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();

            dialog.Title = title;
            dialog.Multiselect = false;
            dialog.Filters.Add(new CommonFileDialogFilter("File", filter));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return "";
        }

        public void LoadConfig(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                options = JsonConvert.DeserializeObject<List<Option>>(File.ReadAllText(jsonPath));
                Output.Log($"Loaded options from: {jsonPath}", ConsoleColor.White);
            }
            if (options.Count > 0)
            {
                UpdateFilesList();
            }
        }

        public void SaveConfig(string jsonPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(jsonPath));
            using (FileSys.WaitForFile(jsonPath)) { };
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(options, Formatting.Indented));
            Output.Log($"Saved options to: {jsonPath}", ConsoleColor.Green);
        }

        private void SelectedFile_Changed(object sender, EventArgs e)
        {
            var comboBox = (DarkComboBox)sender;
            LoadOptions(comboBox.SelectedItem.ToString());

            saveConfigToolStripMenuItem.Enabled = true;
            lbl_ChooseFile.Visible = false;
        }

        private void LoadOptions(string fileName)
        {
            // TODO: Filter options by file
            //List<Option> fileOptions = options.Where(x => x.File == fileName).ToList();
            //List<Option> fileOptions = options;

            // Reset TableLayoutPanel contents
            tlp.Controls.Clear();
            tlp.RowStyles.Clear();

            AnchorStyles anchorStyle = (AnchorStyles.Left | AnchorStyles.Right);

            // Add Header Row
            tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 50f });
            TableLayoutPanel header = new TableLayoutPanel() { Name = "tlp_Header", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
            foreach (var width in new float[] { 5f, 55f, 40f })
                header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            header.Controls.Add(new DarkLabel { Name = "lbl_Enabled", Text = "", Anchor = anchorStyle }, 0, 0);
            header.Controls.Add(new DarkLabel { Name = "lbl_Name", Text = "Name", Anchor = anchorStyle }, 1, 0);
            header.Controls.Add(new DarkLabel { Name = "lbl_Value", Text = "Value", Anchor = anchorStyle }, 2, 0);
            tlp.Controls.Add(header, 0, 0);

            // Add individual options
            for (int i = 0; i < options.Count; i++)
            {
                tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 50f });
                TableLayoutPanel option = new TableLayoutPanel() { Name = $"tlp_Option_{i}", Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
                foreach (var width in new float[] { 5f, 55f, 40f })
                    option.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));

                // Add checkbox
                DarkCheckBox chkBox = new DarkCheckBox { Name = $"chk_Enabled_{i}", Checked = options[i].Enabled, Anchor = anchorStyle };
                chkBox.CheckedChanged += ChkBox_Checked_Changed;
                option.Controls.Add(chkBox, 0, 0);

                // Add label
                DarkLabel lbl = new DarkLabel { Name = $"lbl_Name_{i}", Text = options[i].Name, Anchor = anchorStyle };
                // Add tooltip
                ToolTip lbl_ToolTip = new ToolTip();
                lbl_ToolTip.SetToolTip(lbl, options[i].Hint);
                option.Controls.Add(lbl, 1, 0);

                // Add textbox
                DarkTextBox txtBox = new DarkTextBox { Name = $"txt_Value_{i}", Text = options[i].Changes.First().Value, Anchor = anchorStyle };
                // Add tooltip
                ToolTip txt_ToolTip = new ToolTip();
                txt_ToolTip.SetToolTip(txtBox, options[i].Hint);
                // Add text changed event
                txtBox.TextChanged += TxtBox_Value_Changed;
                option.Controls.Add(txtBox, 2, 0);

                tlp.Controls.Add(option, 0, i + 1);
            }

            addOptionToolStripMenuItem.Enabled = true;
        }

        private void TxtBox_Value_Changed(object? sender, EventArgs e)
        {
            DarkTextBox txtBox = (DarkTextBox)sender;

            // Skip this if options is currently disabled
            if (!txtBox.Enabled)
                return;

            // Update value of option matching label name
            var optionLabel = (DarkLabel)this.Controls.Find($"lbl_Name_{txtBox.Name.Split('_').Last()}", true).First();
            if (options.Any(x => x.Name.Equals(optionLabel.Text)))
            {
                foreach (var change in options.First(x => x.Name.Equals(optionLabel.Text)).Changes)
                {
                    change.Value = txtBox.Text;
                }
            }
        }

        private void ChkBox_Checked_Changed(object? sender, EventArgs e)
        {
            DarkCheckBox chkBox = (DarkCheckBox)sender;

            // Update enabled status of option matching label name
            string optionIndex = chkBox.Name.Split('_').Last();

            var optionLabel = (DarkLabel)this.Controls.Find($"lbl_Name_{optionIndex}", true).First();
            if (options.Any(x => x.Name.Equals(optionLabel.Text)))
            {
                var option = options.First(x => x.Name.Equals(optionLabel.Text));
                option.Enabled = chkBox.Checked;

                var txtBox = (DarkTextBox)this.Controls.Find($"txt_Value_{optionIndex}", true).First();
                if (!option.Enabled)
                {
                    txtBox.Enabled = false;
                    // Show OG value if textbox is disabled
                    txtBox.Text = option.Changes.First().OGValue;
                }
                else
                {
                    txtBox.Enabled = true;
                    // Show user-defined value if textbox is enabled
                    txtBox.Text = option.Changes.First().Value;
                }
            }

            ValidateModGeneration();
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            string file = SaveFile("Save config file", ".json");
            if (!string.IsNullOrEmpty(file))
            {
                SaveConfig(file);
            }
        }

        private string SaveFile(string title, string filter)
        {
            CommonSaveFileDialog dialog = new CommonSaveFileDialog();

            dialog.Title = title;
            dialog.Filters.Add(new CommonFileDialogFilter("File", filter));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return "";
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
                settingsForm.ShowDialog();
        }

        private void BymlToYml_Click(object sender, EventArgs e)
        {
            List<string> bymlPaths = WinFormsEvents.FilePath_Click("Choose BYML file...", true, new string[] { "bgyml (.bgyml)", "byml (.byml)" }, false);

            foreach (var bymlPath in bymlPaths)
            {
                if (File.Exists(bymlPath))
                {
                    string ymlPath = bymlPath.Replace(".bgyml", ".yml").Replace(".byml", ".yml");
                    File.WriteAllText(ymlPath, Byml.FromBinary(File.ReadAllBytes(bymlPath)).ToText());
                }
            }

            MessageBox.Show($"Saved output .YML successfully.");
        }

        private void Compare_Click(object sender, EventArgs e)
        {
            CompareModToOGFiles();
        }

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        readonly static IntPtr handle = GetConsoleWindow();
        [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Hide()
        {
            ShowWindow(handle, SW_HIDE); //hide the console
        }
        public static void Show()
        {
            ShowWindow(handle, SW_SHOW); //show the console
        }
    }
}
