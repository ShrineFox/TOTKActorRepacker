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

namespace TOTKActorRepacker
{
    public partial class MainForm : DarkUI.Forms.DarkForm
    {
        public static List<Option> options { get; set; } = new List<Option>();
        public static FormSettings formSettings = new FormSettings();

        TableLayoutPanel tlp { get; set; } = new TableLayoutPanel() { Name = "tlp_Inner", Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

        public MainForm()
        {
            SetupLogging();

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

        private void SetupLogging()
        {
            Output.Logging = true;
            Output.LogPath = "log.txt";
        }

        private void GenerateMod_Click(object sender, EventArgs e)
        {
            Output.Log($"Starting mod generation...");

            ExtractSARCs();

            CopyDependencyYMLs();

            UpdateYMLValues();

            RebuildSARC();

            // Recompress .pack files to .zs 
            string actorPath = Path.Combine(txt_OutputPath.Text, "Pack/Actor");
            ZStdHelper.CompressFolder(actorPath, actorPath, false);
            Output.Log($"Compressed .pack files to .zs in: {actorPath}");

            PatchRESTBL();

            Output.Log($"\n\nMod generation complete", ConsoleColor.Green);

            MessageBox.Show("Done generating output!");
        }

        private void PatchRESTBL()
        {
            string gameResTbl = Path.Combine(txt_GamePath.Text, 
                $"System/Resource/ResourceSizeTable.Product.{formSettings.Version.Replace(".","")}.rsizetable.zs");

            Output.Log($"Creating newly patched Resource Table file...");

            if (File.Exists(gameResTbl))
            {
                // Decompress RESTBL data in memory
                Restbl restbl = Restbl.FromBinary(Decompress(gameResTbl));

                foreach (var file in Directory.GetFiles(txt_OutputPath.Text, "*", SearchOption.AllDirectories))
                {
                    if (file.EndsWith(".pack.zs"))
                    {
                        // Add uncompressed sizes to CRC32 table
                        UpdatePackRSTBEntry(restbl, file);
                    }
                }

                // Save new RESTBL file
                string newResTbl = Path.Combine(txt_OutputPath.Text,
                    $"System/Resource/ResourceSizeTable.Product.{formSettings.Version.Replace(".", "")}.rsizetable.zs");

                Compress(restbl.ToBinary(), newResTbl);
                Output.Log($"Saved new Resource Table file to: {newResTbl}", ConsoleColor.Green);
            }
            else
                Output.Log($"Could not find Resource Table file at: {gameResTbl}", ConsoleColor.Red);
        }

        private void UpdatePackRSTBEntry(Restbl restbl, string path)
        {
            string uncompressedSARC = path.Replace(".zs", "");
            string relativePath = uncompressedSARC.Substring(txt_OutputPath.Text.Length + 1).Replace("\\", "/");

            // Add/update CRC32 entry for the decompressed .pack itself
            if (File.Exists(uncompressedSARC))
            {
                FileInfo fi = new FileInfo(uncompressedSARC);
                uint pathCrc = StringToCRC32(relativePath);
                uint newSize = Convert.ToUInt32(fi.Length + formSettings.Padding);

                if (restbl.CrcTable.Any(x => x.Hash.Equals(pathCrc)))
                {
                    Output.Log($"Updating RESTBL entry for: {relativePath}");

                    var entry = restbl.CrcTable.First(x => x.Hash.Equals(pathCrc));
                    restbl.CrcTable.Remove(restbl.CrcTable.First(x => x.Hash.Equals(pathCrc)));
                    Output.Log($"\tRemoved CRC32 Table Entry:\n\t\tHash: {entry.Hash}\n\t\tSize: {entry.Size}", ConsoleColor.Yellow);

                    restbl.CrcTable.Add(new CrcEntry(pathCrc, newSize));
                    Output.Log($"\tAdded CRC32 Table Entry:\n\t\tHash: {pathCrc}\n\t\tSize: {newSize}");
                }

                // Add/update CRC32 entry for the decompressed files within matching Actor Pack
                string tempFolder = $"./Temp/Pack/Actor/{Path.GetFileName(uncompressedSARC)}";
                if (Directory.Exists(tempFolder))
                {
                    foreach (var ymlFile in Directory.GetFiles(tempFolder, "*.yml", SearchOption.AllDirectories))
                    {
                        string relativeYmlPath = ymlFile.Substring(tempFolder.Length + 1).Replace("\\", "/").Replace(".yml", ".bgyml");
                        pathCrc = StringToCRC32(relativeYmlPath);
                        newSize = Convert.ToUInt32(Byml.FromText(File.ReadAllText(ymlFile)).ToBinary(true, 3).AsSpan().Length + formSettings.Padding);

                        if (restbl.CrcTable.Any(x => x.Hash.Equals(pathCrc)))
                        {
                            Output.Log($"Updating RESTBL entry for: {relativeYmlPath}");

                            var entry = restbl.CrcTable.First(x => x.Hash.Equals(pathCrc));
                            restbl.CrcTable.Remove(restbl.CrcTable.First(x => x.Hash.Equals(pathCrc)));
                            Output.Log($"\tRemoved CRC32 Table Entry:\n\t\tHash: {entry.Hash}\n\t\tSize: {entry.Size}", ConsoleColor.Yellow);

                            restbl.CrcTable.Add(new CrcEntry(pathCrc, newSize));
                            Output.Log($"\tAdded CRC32 Table Entry:\n\t\tHash: {pathCrc}\n\t\tSize: {newSize}");
                        }
                    }
                    
                }
            }
        }

        private static uint StringToCRC32(string path)
        {
            return CRC.Crc32(Encoding.ASCII.GetBytes(path));
        }

        private void RebuildSARC()
        {
            Output.Log("Rebuilding SARC archives...");

            foreach (var sarcFile in Directory.GetFiles($"./Temp/Pack/Actor/", "*.sarc", SearchOption.TopDirectoryOnly))
            {
                string sarcDir = sarcFile.Replace(".sarc", ".pack");

                using Sarc sarc = Sarc.FromBinary(File.ReadAllBytes(sarcFile));

                // For each yml file in matching SARC dir...
                foreach (var ymlFile in Directory.GetFiles(sarcDir, "*.yml", SearchOption.AllDirectories))
                {
                    // Add to SARC
                    string relativePath = ymlFile.Substring(sarcDir.Length + 1);
                    Byml newByml = Byml.FromText(File.ReadAllText(ymlFile));

                    // TODO: verify endian/version matches OG file
                    Output.Log($"Updating SARC: {sarcFile}\n\twith .BGYML: {relativePath.Replace(".yml", ".bgyml")}");

                    sarc.Add(relativePath.Replace(".yml",".bgyml").Replace("\\", "/"), newByml.ToBinary(true, 3));
                }

                // Save new .pack to output dir
                string outDir = Path.Combine(txt_OutputPath.Text, "Pack/Actor");
                string outPath = Path.Combine(outDir, Path.GetFileNameWithoutExtension(sarcFile) + ".pack");
                Directory.CreateDirectory(outDir);
                using (FileStream fs = new FileStream(outPath, FileMode.OpenOrCreate))
                {
                    fs.Write(sarc.ToBinary());
                    Output.Log($"Saving new .pack to output: {outPath}");
                }
            }

            Output.Log("Done rebuilding SARC archives.", ConsoleColor.Green);
        }

        private void UpdateYMLValues()
        {
            foreach (var option in options)
            {
                foreach(var change in option.Changes)
                {
                    string ymlPath = $"./Temp/Pack/Actor/{change.File}/{change.Path.Replace(".bgyml",".yml")}";
                    if (File.Exists(ymlPath))
                    {
                        UpdateYMLValue(ymlPath, change, option.Enabled);
                    }
                }
            }
        }

        private void UpdateYMLValue(string ymlPath, Change change, bool enabled)
        {
            string[] ymlLines = File.ReadAllLines(ymlPath);
            string value = change.Value;
            if (!enabled)
                value = change.OGValue;

            for (int i = 0; i < ymlLines.Count(); i++)
            {
                if (ymlLines[i].Contains(change.FieldName))
                {
                    if (change.FieldName.Contains(":"))
                    {
                        string lineStart = ymlLines[i].Split(change.FieldName).First();
                        string lineEnd = ymlLines[i].Split(change.FieldName).Last();
                        string[] splitLineEnd = lineEnd.Split(' ');
                        string newLineEnd = "";

                        if (splitLineEnd.Count() > 2)
                        {
                            newLineEnd = ", " + string.Join("", splitLineEnd.Skip(2));
                        }
                        else
                            newLineEnd = " " + string.Join("", splitLineEnd.Skip(1));

                        string newLine = $"{lineStart}{change.FieldName}{value}{newLineEnd}";

                        Output.Log($"\nChanged line {i} in YML: {ymlPath}");
                        Output.Log($"\tOld: ", ConsoleColor.Yellow);
                        Output.Log(ymlLines[i].Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);
                        Output.Log($"\n\tNew: ", ConsoleColor.Yellow);
                        Output.Log(newLine.Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);

                        ymlLines[i] = newLine;
                    }
                    else
                    {
                        string newLine = change.FieldName + ": " + value;
                        int whiteSpaceCount = ymlLines[i].TakeWhile(c => c == ' ').Count() + newLine.Length;
                        newLine = newLine.PadLeft(whiteSpaceCount);

                        Output.Log($"\nChanged line {i} in YML: {ymlPath}");
                        Output.Log($"\tOld: ", ConsoleColor.Yellow);
                        Output.Log(ymlLines[i].Replace("{","{{").Replace("}","}}"), ConsoleColor.Yellow);
                        Output.Log($"\n\tNew: ", ConsoleColor.Yellow);
                        Output.Log(newLine.Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);

                        ymlLines[i] = newLine;
                    }
                }
            }

            File.WriteAllLines(ymlPath, ymlLines);
        }

        private void CopyDependencyYMLs()
        {
            if (Directory.Exists("./Dependencies"))
            {
                foreach (var depFolder in Directory.GetDirectories("./Dependencies"))
                {
                    foreach (var packFolder in Directory.GetDirectories(depFolder))
                    {
                        // Copy directory contents to temp folder if used by any active options
                        if (options.Any(o => o.Enabled == true && o.Changes.Any(c => Path.GetFileName(packFolder).Equals(c.File))))
                        {
                            foreach (var subDir in Directory.GetDirectories(packFolder))
                            {
                                CopyDir(subDir, $"./Temp/Pack/Actor/{Path.GetFileName(packFolder)}/{Path.GetFileName(subDir)}");
                            }
                            Output.Log($"Copied dependency \"{Path.GetFileName(depFolder)}\" contents to temp folder: {Path.GetFileName(packFolder)}", ConsoleColor.White);
                        }
                    }
                }
            }
        }

        private void ExtractSARCs()
        {
            string actorPath = Path.Combine(txt_GamePath.Text, "Pack/Actor");
            string tempPath = $"./Temp";
            string tempActorPath = $"./Temp/Pack/Actor";

            Output.Log($"Extracting YML from SARC files...", ConsoleColor.White);

            // If temp path already exists, delete it
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
                Output.Log($"Deleted existing temp folder: {tempPath}", ConsoleColor.Yellow);
            }

            if (Directory.Exists(actorPath))
            {
                // For each .zs file in Actor/Pack matching the name of an enabled option's changed file...
                foreach (var zsFile in Directory.GetFiles(actorPath, "*.zs", SearchOption.TopDirectoryOnly)
                    .Where(x => options.Any(o => o.Enabled == true && o.Changes.Any(c => Path.GetFileNameWithoutExtension(x).Equals(c.File)))))
                {
                    // Copy to temp dir
                    Directory.CreateDirectory(tempActorPath);
                    string zsCopy = Path.Combine(tempActorPath, Path.GetFileName(zsFile));
                    File.Copy(zsFile, zsCopy);
                    Output.Log($"Copied .zs file matching form option: {zsCopy}", ConsoleColor.White);
                }

                // Decompress files
                ZStdHelper.DecompressFolder(tempActorPath, tempActorPath, false);
                Output.Log($"Decompressed temp folder: {tempActorPath}", ConsoleColor.White);

                // Delete .zs files
                foreach (var file in Directory.GetFiles(tempActorPath, "*.zs", SearchOption.AllDirectories))
                {
                    File.Delete(file);
                }
                Output.Log($"Deleted .zs files in temp folder.", ConsoleColor.Yellow);

                // Rename .pack files to .sarc
                foreach (var file in Directory.GetFiles(tempActorPath, "*.pack", SearchOption.AllDirectories))
                {
                    File.Move(file, Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".sarc"));
                }
                Output.Log($"Renamed .pack files in temp folder to .sarc", ConsoleColor.Yellow);


                // For each .sarc file in temp dir...
                foreach (var sarcFile in Directory.GetFiles(tempActorPath, "*.sarc", SearchOption.TopDirectoryOnly))
                {
                    // Open SARC
                    using Sarc sarc = Sarc.FromBinary(File.ReadAllBytes(sarcFile));

                    // For each byml file in SARC...
                    foreach ((var bymlFileName, var bymlFile) in sarc.Where(x => x.Key.EndsWith(".bgyml")))
                    {
                        string tempSarcPath = Path.Combine(tempActorPath, Path.GetFileNameWithoutExtension(sarcFile) + ".pack");
                        string outFile = Path.Combine(tempSarcPath, bymlFileName.Replace("bgyml", "yml"));

                        // Extract YML to temp folder
                        Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                        File.WriteAllText(outFile, Byml.FromBinary(bymlFile.AsSpan()).ToText());
                        Output.Log($"Extracted .yml file to temp folder: {outFile}", ConsoleColor.White);
                    }
                }

                Output.Log($"Done extracting YML from SARC files.", ConsoleColor.Green);
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
            using (WaitForFile(jsonPath)) { };
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(options, Formatting.Indented));
            Output.Log($"Saved options to: {jsonPath}", ConsoleColor.Green);
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

            Output.Log($"Comparing Actor Pack files between\n\tOriginal: {gameActorPath}\n\tMod: {modActorPath}", ConsoleColor.White);

            // If comparison path already exists, delete it
            if (Directory.Exists(comparisonPath))
            {
                Output.Log($"Deleting existing comparison output directory: {comparisonPath}", ConsoleColor.Yellow);
                Directory.Delete(comparisonPath, true);
            }

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

                        Output.Log($"Copied matching files to Temp directory: {Path.GetFileName(outFolder)}", ConsoleColor.White);

                        // Decompress files
                        ZStdHelper.DecompressFolder(outFolder, outFolder, false);
                        tempModFile = tempModFile.Replace(".zs", "");
                        tempGameFile = tempGameFile.Replace(".zs", "");

                        Output.Log($"Decompressed .zs files in Temp directory", ConsoleColor.White);

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
                                    Output.Log($"Extracted modified .yml file to: {outFile}", ConsoleColor.Green);
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
                Output.Log($"Deleted .pack and .zs files in comparison directory", ConsoleColor.Yellow);

                // Delete empty output directories
                foreach (var dir in Directory.GetDirectories(comparisonPath, "*", SearchOption.AllDirectories))
                {
                    if (Directory.GetFiles(dir, "*.yml", SearchOption.AllDirectories).Count() == 0)
                        Directory.Delete(dir, true);
                }
                Output.Log($"Deleted empty folders in comparison directory", ConsoleColor.Yellow);

                MessageBox.Show($"Comparison complete, modded .yml files can be found at:\n\n{Path.GetFullPath(comparisonPath)}");
                Output.Log($"Comparison succeeded.", ConsoleColor.Green);
            }
            else
            {
                MessageBox.Show("Failed to compare mod to game files, please ensure " +
                    "/Pack/Actor/ directory exists in both folders!");
                Output.Log($"Comparison failed.", ConsoleColor.Red);
            }
        }

        internal static byte[] Decompress(string input)
        {
            using (var ms = new MemoryStream(File.ReadAllBytes(input)))
            using (var compressionStream = new ZstandardStream(ms, CompressionMode.Decompress))
            using (var temp = new MemoryStream())
            {
                compressionStream.CopyTo(temp);
                byte[] outputBytes = temp.ToArray();
                return outputBytes;
            }
        }

        internal static void Compress(byte[] input, string output)
        {
            ZstdNet.Compressor compressor = new ZstdNet.Compressor();
            var outputBytes = compressor.Wrap(input);

            Directory.CreateDirectory(Path.GetDirectoryName(output));

            File.WriteAllBytes(output, outputBytes);
        }

        public static void CopyDir(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder) && !File.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            // Get Files & Copy
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }

            // Get dirs recursively and copy files
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyDir(folder, dest);
            }
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