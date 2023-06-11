using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Newtonsoft.Json;

namespace TOTKActorRepacker
{
    public partial class SettingsForm : DarkForm
    {
        public SettingsForm()
        {
            InitializeComponent();

            // Get current values for form settings object
            comboBox_Game.SelectedIndex = comboBox_Game.Items.IndexOf(MainForm.formSettings.Game);
            txt_Version.Text = MainForm.formSettings.Version;
            num_Padding.Value = Convert.ToDecimal(MainForm.formSettings.Padding);
            chk_FullSARCRebuild.Checked = MainForm.formSettings.FullSARCRebuild;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            // Update values in form settings object
            MainForm.formSettings.Game = comboBox_Game.SelectedItem.ToString();
            MainForm.formSettings.Version = txt_Version.Text;
            MainForm.formSettings.Padding = Convert.ToInt32(num_Padding.Value);
            MainForm.formSettings.FullSARCRebuild = chk_FullSARCRebuild.Checked;
            MainForm.formSettings.Save();
        }
    }

    public class FormSettings
    {
        public string GamePath { get; set; } = "";
        public string OutputPath { get; set; } = "./Output";
        // Default values for form settings object
        public string Game { get; set; } = "Tears of the Kingdom";
        public string Version { get; set; } = "1.1.2";
        public int Padding { get; set; } = 58000;
        public bool FullSARCRebuild { get; set; } = false;
        public string DefaultJson { get; set; } = "./Dependencies/bettersagesmod.json";
        public void Load()
        {
            if (File.Exists("./formsettings.json"))
                MainForm.formSettings = JsonConvert.DeserializeObject<FormSettings>(File.ReadAllText("./formsettings.json"));
        }
        public void Save()
        {
            File.WriteAllText("./formsettings.json", JsonConvert.SerializeObject(MainForm.formSettings, Formatting.Indented));
            Console.WriteLine("Saved Settings to ./formsettings.json", ConsoleColor.Green);
        }
    }
}
