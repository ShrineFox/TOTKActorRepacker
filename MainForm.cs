using DarkUI.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;

namespace TOTKActorRepacker
{
    public partial class MainForm : DarkUI.Forms.DarkForm
    {
        public static List<Option> options { get; set; } = new List<Option>();
        TableLayoutPanel tlp { get; set; } = new TableLayoutPanel() { Name = "tlp_Inner", Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

        public MainForm()
        {
            InitializeComponent();

            LoadDefaults();
            // Save example config
            //options.Add(new Option { Enabled = true, File = "Pack/Actor/Npc_Tulin_Sage.pack", Path = "GameParameter/SageCommonParam/NpcSageTulinCommon.game__npc__SageCommonParam.bgyml", FieldName = "SageSkillRecastBaseTime", Value = "450" });
            //options.Add(new Option { Enabled = true, File = "Pack/Actor/Npc_Tulin_Sage.pack", Path = "GameParameter/SageCommonParam/NpcSageCommon.game__npc__SageCommonParam.bgyml", FieldName = "SageSkillRecastBaseTime", Value = "90" });
            //SaveConfig("./config.json");

            // Create options table
            pnl_Main.Controls.Add(tlp);
        }

        private void LoadDefaults()
        {
            if (File.Exists("./defaults.json"))
            {
                List<string> defaults = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("./defaults.json"));
                txt_GamePath.Text = defaults[0];
                txt_OutputPath.Text = defaults[1];
                LoadConfig(defaults[2]);
            }
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
            // Enable options if mod files are found & output folder is set
            if (Directory.Exists(txt_GamePath.Text)
                && File.Exists(Path.Combine(txt_GamePath.Text, "Pack\\ZsDic.pack.zs"))
                && Directory.Exists(txt_OutputPath.Text))
            {
                btn_GenerateMod.Enabled = true;
                loadConfigToolStripMenuItem.Enabled = true;
                addFileToolStripMenuItem.Enabled = true;
            }
            else
            {
                btn_GenerateMod.Enabled = false;
                loadConfigToolStripMenuItem.Enabled = false;
                addFileToolStripMenuItem.Enabled = false;
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
                foreach (var option in options)
                    if (!files.Any(x => x.Equals(option.File)))
                        files.Add(option.File);
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
        }

        private void LoadOptions(string fileName)
        {
            List<Option> fileOptions = options.Where(x => x.File == fileName).ToList();

            // Reset TableLayoutPanel contents
            tlp.Controls.Clear();
            tlp.RowStyles.Clear();

            AnchorStyles anchorStyle = (AnchorStyles.Left | AnchorStyles.Right);

            // Add Header Row
            tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 50f });
            TableLayoutPanel header = new TableLayoutPanel() { Name = "tlp_Header", Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1 };
            foreach (var width in new float[] { 5f, 55f, 25f, 15f })
                header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            header.Controls.Add(new DarkLabel { Name = "lbl_Enabled", Text = "", Anchor = anchorStyle }, 0, 0);
            header.Controls.Add(new DarkLabel { Name = "lbl_Path", Text = "Path", Anchor = anchorStyle }, 1, 0);
            header.Controls.Add(new DarkLabel { Name = "lbl_FieldName", Text = "Field Name", Anchor = anchorStyle }, 2, 0);
            header.Controls.Add(new DarkLabel { Name = "lbl_Value", Text = "Value", Anchor = anchorStyle }, 3, 0);
            tlp.Controls.Add(header, 0, 0);

            // Add individual options
            for (int i = 0; i < fileOptions.Count; i++)
            {
                tlp.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = 50f });
                TableLayoutPanel option = new TableLayoutPanel() { Name = $"tlp_Option_{i}", Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1 };
                foreach (var width in new float[] { 5f, 55f, 25f, 15f })
                    option.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));

                // Add fields
                option.Controls.Add(new DarkCheckBox { Name = $"chk_Enabled_{i}", Checked = fileOptions[i].Enabled, Anchor = anchorStyle }, 0, 0);
                option.Controls.Add(new DarkLabel { Name = $"lbl_Path_{i}", Text = fileOptions[i].Path, Anchor = anchorStyle }, 1, 0);
                option.Controls.Add(new DarkLabel { Name = $"lbl_FieldName_{i}", Text = fileOptions[i].FieldName, Anchor = anchorStyle }, 2, 0);
                option.Controls.Add(new DarkTextBox { Name = $"txt_Value_{i}", Text = fileOptions[i].Value, Anchor = anchorStyle }, 3, 0);
                tlp.Controls.Add(option, 0, i + 1);
            }
        }
    }

    public class Option
    {
        public string File = "";
        public string Path = "";
        public string FieldName = "";
        public string Value = "";
        public bool Enabled = true;
    }
}