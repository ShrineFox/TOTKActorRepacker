using DarkUI.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;
using ZstdSharp;
using Cead;
using YamlDotNet.Serialization;
using System.Dynamic;
using System.IO.Compression;
using Zstandard.Net;
using ObjectsComparer;
using System.Linq;

namespace TOTKActorRepacker
{
    public partial class MainForm : DarkUI.Forms.DarkForm
    {
        public static List<Option> options { get; set; } = new List<Option>();
        public static FormSettings formSettings = new FormSettings();

        TableLayoutPanel tlp { get; set; } = new TableLayoutPanel() { Name = "tlp_Inner", Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

        public MainForm()
        {
            InitializeComponent();

            // Load default form settings from json if it exists
            formSettings.Load();

            // Load field values from default json if it exists
            LoadUserDefaults();

            // TODO: Dynamically compare YML files, for now we're doing semi-hardcoded BYML editing
            saveConfigToolStripMenuItem.Visible = false;
            lbl_ChooseFile.Visible = false;
            comboBox_File.Visible = false;
            loadConfigToolStripMenuItem.Visible = false;
            addToolStripMenuItem.Visible = false;

            // Create options table
            pnl_Main.Controls.Add(tlp);
        }

        private void GenerateMod_Click(object sender, EventArgs e)
        {
            // Extract SARCs

            // Copy dependency mod files to output

            // For each enabled option, update with newest values

            // For each disabled option, revert to OG values

            // Convert YMLs to BGYML and replace

            // Rebuild SARCs

            // Patch RSTB

            // Done! Notify user
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
            if (Directory.Exists(txt_GamePath.Text)
                && File.Exists(Path.Combine(txt_GamePath.Text, "Pack\\ZsDic.pack.zs")))
            {
                // Update config with chosen paths
                formSettings.GamePath = txt_GamePath.Text;
                formSettings.OutputPath = txt_OutputPath.Text;
                formSettings.Save();

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
            options = JsonConvert.DeserializeObject<List<Option>>(File.ReadAllText(jsonPath));
            if (options.Count > 0)
            {
                UpdateFilesList();
            }
        }

        public void SaveConfig(string jsonPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(jsonPath));
            using (WaitForFile(jsonPath)) { };
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(options, Formatting.Indented));
        }

        public static FileStream WaitForFile(string fullPath,
            FileMode mode = FileMode.Open,
            FileAccess access = FileAccess.ReadWrite,
            FileShare share = FileShare.None)
        {
            for (int numTries = 0; numTries < 10; numTries++)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fullPath, mode, access, share);
                    return fs;
                }
                catch (IOException)
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                    Thread.Sleep(2000);
                }
            }
            return null;
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

                // Add fields
                DarkCheckBox chkBox = new DarkCheckBox { Name = $"chk_Enabled_{i}", Checked = options[i].Enabled, Anchor = anchorStyle };
                chkBox.CheckedChanged += ChkBox_Checked_Changed;
                option.Controls.Add(chkBox, 0, 0);
                option.Controls.Add(new DarkLabel { Name = $"lbl_Name_{i}", Text = options[i].Name, Anchor = anchorStyle }, 1, 0);
                DarkTextBox txtBox = new DarkTextBox { Name = $"txt_Value_{i}", Text = options[i].Changes.First().Value, Anchor = anchorStyle };
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

        private void Compare_Click(object sender, EventArgs e)
        {
            string modPath = ChooseFolder("Choose mod folder to compare with game files");
            string modActorPath = Path.Combine(modPath, "Pack/Actor");
            string gameActorPath = Path.Combine(formSettings.GamePath, "Pack/Actor");
            string comparisonPath = $"./Compare_{Path.GetFileName(modPath)}";

            // If comparison path already exists, delete it
            if (Directory.Exists(comparisonPath))
                Directory.Delete(comparisonPath, true);


            if (Directory.Exists(modActorPath) && Directory.Exists(gameActorPath))
            {
                // For each .zs file in Actor/Pack...
                foreach (var modFile in Directory.GetFiles(modActorPath, "*.zs", SearchOption.TopDirectoryOnly))
                {
                    // If matching file in game dir exists...
                    string modFileRelativePath = modFile.Substring(modPath.Length + 1);
                    string gameFile = Path.Combine(formSettings.GamePath, modFileRelativePath);
                    if (File.Exists(gameFile))
                    {
                        string fileName = Path.GetFileName(gameFile);
                        string outFolder = Path.Combine(comparisonPath, Path.GetFileNameWithoutExtension(fileName));

                        // TODO: wait for handle to be free

                        // Copy both to comparison output dir
                        Directory.CreateDirectory(outFolder);
                        string tempModFile = Path.Combine(outFolder, "modFile.pack.zs");
                        string tempGameFile = Path.Combine(outFolder, "gameFile.pack.zs");
                        File.Copy(modFile, tempModFile);
                        File.Copy(gameFile, tempGameFile);

                        // Decompress files
                        ZStdHelper.DecompressFolder(outFolder, outFolder, false);
                        tempModFile = tempModFile.Replace(".zs", "");
                        tempGameFile = tempGameFile.Replace(".zs", "");

                        // Open SARC
                        if (File.Exists(tempModFile))
                        {
                            using Sarc modPack = Sarc.FromBinary(File.ReadAllBytes(tempModFile));
                            using Sarc gamePack = Sarc.FromBinary(File.ReadAllBytes(tempGameFile));

                            // For each byml file in modified SARC...
                            foreach ((var modPackFileName, var modPackFile) in modPack.Where(x => x.Key.EndsWith(".bgyml")))
                            {
                                // By default, if a match doesn't exist in base game, consider it a modded file
                                string gameByml = "";

                                // If unmodified SARC contains same file...
                                if (gamePack.Any(x => x.Key.EndsWith(".bgyml") && x.Key.Equals(modPackFileName)))
                                {
                                    // Compare original BYML text
                                    var gamePackFile = gamePack.First(x => x.Key.EndsWith(".bgyml") && x.Key.Equals(modPackFileName));
                                    gameByml = Byml.FromBinary(gamePackFile.Value.AsSpan()).ToText();
                                }

                                // Get modified BYML text
                                string modByml = Byml.FromBinary(modPackFile.AsSpan()).ToText();

                                // If BYML texts are not identical, save .yml
                                if (modByml != gameByml)
                                {
                                    string outFile = Path.Combine(outFolder, modPackFileName.Replace("bgyml", "yml"));
                                    Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                                    File.WriteAllText(outFile, modByml);
                                }
                            }
                        }

                    }
                }

                // Delete .pack and .zs files
                foreach (var file in Directory.GetFiles(comparisonPath, "*", SearchOption.AllDirectories)
                    .Where(x => Path.GetExtension(x) == ".zs" || Path.GetExtension(x) == ".pack"))
                {
                    File.Delete(file);
                }

                // Delete empty output directories
                foreach (var dir in Directory.GetDirectories(comparisonPath, "*", SearchOption.AllDirectories))
                {
                    if (Directory.GetFiles(dir, "*.yml", SearchOption.AllDirectories).Count() == 0)
                        Directory.Delete(dir, true);
                }

                MessageBox.Show($"Comparison complete, modded .yml files can be found at:\n\n{Path.GetFullPath(comparisonPath)}");
            }
            else
            {
                MessageBox.Show("Failed to compare mod to game files, please ensure " +
                    "/Pack/Actor/ directory exists in both folders!");
            }
        }

        internal static void Decompress(string input, string output)
        {
            using (var ms = new MemoryStream(File.ReadAllBytes(input)))
            using (var compressionStream = new ZstandardStream(ms, CompressionMode.Decompress))
            using (var temp = new MemoryStream())
            {
                compressionStream.CopyTo(temp);
                byte[] outputBytes = temp.ToArray();
                File.WriteAllBytes(output, outputBytes);
            }
        }

        internal static void Compress(string input, string output)
        {
            ZstdNet.Compressor compressor = new ZstdNet.Compressor();
            var outputBytes = compressor.Wrap(File.ReadAllBytes(input));
            File.WriteAllBytes(output, outputBytes);
        }
    }

    public class Change
    {
        public string File = "";
        public string Path = "";
        public string FieldName = "";
        public string Value = "";
        public string OGValue = "";
    }

    public class Option
    {
        public string Name = "";
        public List<Change> Changes = new List<Change>();
        public bool Enabled = true;
    }
}