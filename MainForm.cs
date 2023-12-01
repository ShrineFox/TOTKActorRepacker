using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using TOTKActorRepack.Properties;
using static System.Windows.Forms.Design.AxImporter;

namespace TOTKActorRepack
{
    public partial class MainForm : MetroSetForm
    {
        string defaultProjectPath = "./DefaultProject.json";
        string outputPath = "./Output";
        Project loadedProject = new Project();
        BymlFile selectedByml = null;

        public MainForm()
        {
            InitializeComponent();
            Theme.ApplyToForm(this);
            SetListBoxDataSource();
            //Output.Logging = true;
            //Output.LogControl = rtb_Log;
#if DEBUG
            //Output.VerboseLogging = true;
#endif

            if (File.Exists(defaultProjectPath))
                LoadProject(defaultProjectPath);
        }

        private void LoadProject(string jsonPath)
        {
            loadedProject = JsonConvert.DeserializeObject<Project>(File.ReadAllText(jsonPath));

            // Update form data with list of changes in project object
            txt_GameFilesDir.Text = loadedProject.GameFilesDir;
            txt_GameVersion.Text = loadedProject.GameVer;

            if (listBox_BYMLs.Items.Count > 0)
                listBox_BYMLs.SelectedIndex = 0;

            MessageBox.Show($"Loaded project from:\n{jsonPath}", "Project Loaded");
        }

        private void SaveProject()
        {
            // Get output path from file select prompt
            var outPaths = WinFormsDialogs.SelectFile("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
            if (outPaths == null || outPaths.Count == 0 || string.IsNullOrEmpty(outPaths.First()))
                return;

            // Ensure output path ends with .json
            string outPath = outPaths.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath += ".json";

            // Save to .json file
            string jsonText = JsonConvert.SerializeObject(loadedProject, Formatting.Indented);
            File.WriteAllText(outPath, jsonText);
            MessageBox.Show($"Saved project file to:\n{outPath}", "Project Saved");
        }


        BindingSource bs_BYMLListBox = new BindingSource();

        private void SetListBoxDataSource()
        {
            bs_BYMLListBox.DataSource = loadedProject.BymlFiles;

            listBox_BYMLs.DataSource = null;
            listBox_BYMLs.DataSource = bs_BYMLListBox;
            listBox_BYMLs.DisplayMember = "Path";
            listBox_BYMLs.ValueMember = "Path";
            //listBox_BYMLs.FormattingEnabled = true;
            //listBox_BYMLs.Format += ListBoxDirs_Format;
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex == -1)
                return;

            selectedByml = (BymlFile)listBox_BYMLs.SelectedItem;
            UpdateChangesList();
        }

        BindingSource bs_ChangesListBox = new BindingSource();

        private void UpdateChangesList()
        {
            bs_ChangesListBox.DataSource = selectedByml.Changes;

            listBox_Changes.DataSource = null;
            listBox_Changes.DataSource = bs_ChangesListBox;
            listBox_Changes.DisplayMember = "ChangeName";
            listBox_Changes.ValueMember = "ChangeName";
        }


        private void GameFileDir_Changed(object sender, EventArgs e)
        {
            loadedProject.GameFilesDir = txt_GameFilesDir.Text;
        }

        private void GameVer_Changed(object sender, EventArgs e)
        {
            loadedProject.GameVer = txt_GameVersion.Text;
        }

        private void SaveProject_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void LoadProject_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            LoadProject(filePaths.First());
        }

        public class Project
        {
            public string GameFilesDir { get; set; } = "";
            public string GameVer { get; set; } = "1.2.1";
            public List<BymlFile> BymlFiles { get; set; } = new List<BymlFile>();
        }

        public class BymlFile
        {
            public string Path { get; set; } = "";
            public string Comment { get; set; } = "";
            public List<BymlChange> Changes { get; set; } = new List<BymlChange>();

        }

        public class BymlChange
        {
            [Description("The purpose of this BYML change. Only there to help the user.")]
            public string ChangeName { get; set; } = "Unnamed Change";

            [Description("The name of the value to change in the BYML.")]
            public string FieldName { get; set; } = "Unnamed Field";

            [Description("The original value of the field (before being changed).")]
            public string OGValue { get; set; } = "0";

            [Description("The default value of the field (after being changed).")]
            public string FieldValue { get; set; } = "1";

            [Description("More context to help the user decide what value to use.")]
            public string Comment { get; set; } = "ADD COMMENT HERE";

            [Description("Whether the changed value will be used when outputting mod files.")] 
            public bool Enabled { get; set; } = true;
        }

        private void AddBYML_Click(object sender, EventArgs e)
        {
            RenameForm renameForm = new RenameForm("Set BYML Relative Path", "./Path/To/File.byml");
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                BymlFile newByml = new BymlFile() { Path = renameForm.RenameText };
                loadedProject.BymlFiles.Add(newByml);

                SetListBoxDataSource();
            }
        }

        private void RemoveByml_Click(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex != -1)
                loadedProject.BymlFiles.RemoveAt(listBox_BYMLs.SelectedIndex);
        }

        private void SetBymlPath_Click(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex == -1)
                return;

            var byml = (BymlFile)listBox_BYMLs.SelectedItem;
            RenameForm renameForm = new RenameForm("Set BYML Relative Path", selectedByml.Path);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadedProject.BymlFiles[listBox_BYMLs.SelectedIndex].Path = renameForm.RenameText;
            }
        }

        private void SetBymlComment_Click(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex == -1)
                return;

            var byml = (BymlFile)listBox_BYMLs.SelectedItem;
            RenameForm renameForm = new RenameForm("Set BYML Comment", selectedByml.Comment);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadedProject.BymlFiles[listBox_BYMLs.SelectedIndex].Comment = renameForm.RenameText;
            }
        }

        private void AddChange_Click(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex == -1)
                return;

            loadedProject.BymlFiles[listBox_BYMLs.SelectedIndex].Changes.Add(new BymlChange());
        }

        private void SelectedChange_Changed(object sender, EventArgs e)
        {
            if (listBox_BYMLs.SelectedIndex == -1 || listBox_Changes.SelectedIndex == -1)
                return;

            propertyGrid_Changes.SelectedObject = loadedProject.BymlFiles[listBox_BYMLs.SelectedIndex].Changes[listBox_Changes.SelectedIndex];
        }

        private void ToggleTheme_Click(object sender, EventArgs e)
        {
            if (Theme.ThemeStyle == MetroSet_UI.Enums.Style.Light)
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Dark;
            else
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Light;
            Theme.ApplyToForm(this);
        }
    }
}
