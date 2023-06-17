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

            // Enable mod generation button
            ValidateModGeneration();
        }

        private void GenerateMod()
        {
            Output.Log($"Starting mod generation...");

            // Extract .pack.zs contents to program's "Temp" folder
            ExtractPacks(txt_GamePath.Text, "./Temp");

            // Convert .bgyml files in temp folder  to .yml if changed by options
            ConvertBYMLtoYML("./Temp", true);

            CopyDependencyFiles();

            UpdateYMLValues();

            // Delete existing .pack.zs files in output dir
            DeleteZsFiles(txt_OutputPath.Text);

            RebuildPacks();

            // Recompress .pack files to .zs 
            string actorPath = Path.Combine(txt_OutputPath.Text, "Pack/Actor");
            ZStdHelper.CompressFolder(actorPath, actorPath, false);
            Output.Log($"Compressed .pack files to .zs in: {actorPath}");

            PatchRESTBL();

            // Delete .pack files in output folder
            foreach (var packFile in Directory.GetFiles(actorPath, "*.pack", SearchOption.TopDirectoryOnly))
                File.Delete(packFile);
            Output.Log($"Deleted uncompressed .pack files in output folder");

            Output.Log($"\n\nMod generation complete", ConsoleColor.Green);

            MessageBox.Show("Done generating output!");
        }

        private void ExtractPacks(string inDir, string outDir, bool deletePacks = false)
        {
            string actorPathIn = Path.Combine(inDir, "Pack/Actor");
            string actorPathOut = Path.Combine(outDir, "Pack/Actor");

            // If out directory already exists, delete it
            if (Directory.Exists(outDir))
            {
                Directory.Delete(outDir, true);
                Output.VerboseLog($"Deleted existing extraction output folder: {outDir}", ConsoleColor.Yellow);
            }
            Directory.CreateDirectory(actorPathOut);

            if (Directory.Exists(actorPathIn))
            {
                // Copy .zs files to destination folder
                foreach (var zsFile in Directory.GetFiles(actorPathIn, "*.zs", SearchOption.TopDirectoryOnly))
                    File.Copy(zsFile, Path.Combine(actorPathOut, Path.GetFileName(zsFile)));
                // Decompress .zs files
                ZStdHelper.DecompressFolder(actorPathOut, actorPathOut, false);
                Output.Log($"Decompressed .zs files in: {actorPathOut}", ConsoleColor.White);
                // Delete .zs files
                foreach (var zsFile in Directory.GetFiles(actorPathOut, "*.zs", SearchOption.TopDirectoryOnly))
                    File.Delete(zsFile);

                // Delete .zs files after decompressing
                DeleteZsFiles(actorPathOut);

                // For each .pack file in temp dir...
                foreach (var packFile in Directory.GetFiles(actorPathOut, "*.pack", SearchOption.TopDirectoryOnly))
                {
                    // Make temp folder for unpacked .pack contents and modified .pack contents (for later)
                    string unpackedPath = Path.Combine(actorPathOut, Path.GetFileNameWithoutExtension(packFile) + "_pack");
                    Directory.CreateDirectory(unpackedPath);

                    // Extract SARC contents to temp folder
                    SARC.Extract(packFile, unpackedPath, true, false);
                    Output.VerboseLog($"Extracted files from: {packFile}\n\tto: {unpackedPath}", ConsoleColor.White);

                    // Delete .pack files if they aren't needed anymore
                    if (deletePacks)
                        File.Delete(packFile);
                }
                Output.Log($"Done extracting files from SARCs.", ConsoleColor.Green);
            }
        }

        private void ConvertBYMLtoYML(string bymlPath, bool onlyChangedFiles)
        {
            // For each file that ends with .bgyml in path...
            foreach (string file in Directory.GetFiles(bymlPath, "*",
                        SearchOption.AllDirectories).Where(x => Path.GetExtension(x).Equals(".bgyml")))
            {
                // If an option modifies this file...
                if (!onlyChangedFiles || options.Any(o => o.Changes.Any(c => Path.GetFileName(file).Equals(c.File))))
                {
                    // Convert file to YML
                    string outFile = file.Replace(".bgyml", ".yml");
                    File.WriteAllText(outFile, Byml.FromBinary(File.ReadAllBytes(file)).ToText());
                    Output.VerboseLog($"Converted file to .yml:\n\t\t{outFile}", ConsoleColor.Gray);
                }
            }
        }

        private void CopyDependencyFiles()
        {
            if (Directory.Exists("./Dependencies"))
            {
                foreach (var depFolder in Directory.GetDirectories("./Dependencies"))
                {
                    foreach (var packFolder in Directory.GetDirectories(depFolder))
                    {
                        string packfolderName = Path.GetFileName(packFolder);
                        foreach (var subDir in Directory.GetDirectories(packFolder))
                        {
                            FileSys.CopyDir(subDir, $"./Temp/Pack/Actor/{packfolderName}/{Path.GetFileName(subDir)}");
                        }
                        Output.Log($"Copied dependency \"{Path.GetFileName(depFolder)}\" contents to temp folder: {packfolderName}", ConsoleColor.White);
                    }
                }
            }
        }

        private void UpdateYMLValues()
        {
            // Update values in YML to reflect changes made by user options
            foreach (var option in options)
            {
                foreach (var change in option.Changes)
                {
                    string ymlPath = $"./Temp/Pack/Actor/{change.File.Replace(".pack", "_pack_new")}/{change.Path.Replace(".bgyml", ".yml")}";

                    if (File.Exists(ymlPath))
                        UpdateYMLValue(ymlPath, change, option.Enabled);
                    else
                        Output.Log($"Could not find YML file: {ymlPath}", ConsoleColor.Red);
                }
            }

            // Convert YML files to BYML and delete YML
            ConvertYMLsToBYML();
        }

        private void UpdateYMLValue(string ymlPath, Change change, bool enabled)
        {
            using (FileSys.WaitForFile(ymlPath)) { }

            string value = change.Value;
            if (!enabled)
                value = change.OGValue;

            // Ensure decimals end with a decimals place and integers do not
            switch (change.Type)
            {
                case "Int32":
                    value = Math.Floor(Convert.ToDouble(value)).ToString();
                    break;
                case "Single":
                    value = Convert.ToDouble(value).ToString("0.0");
                    break;
                default:
                    break;
            }

            if (change.FieldName.Contains(":"))
            {
                string[] ymlLines = File.ReadAllLines(ymlPath);
                for (int i = 0; i < ymlLines.Count(); i++)
                {
                    if (ymlLines[i].Contains(change.FieldName))
                    {
                        string[] splitLine = ymlLines[i].Split(' ');
                        for (int x = 0; x < splitLine.Length; x++)
                        {
                            if (splitLine[x].Replace("{", "").StartsWith(change.FieldName))
                            {
                                string strEnd = "";
                                if (splitLine[x + 1].EndsWith(","))
                                    strEnd += ",";
                                if (splitLine[x + 1].EndsWith("}}"))
                                    strEnd += "}}";
                                else if (splitLine[x + 1].EndsWith("}"))
                                    strEnd += "}";
                                splitLine[x + 1] = value + strEnd;
                            }
                        }

                        string newLine = string.Join(' ', splitLine);

                        Output.Log($"\nChanged line {i} in YML: {ymlPath}");
                        Output.Log($"\tOld: ", ConsoleColor.Yellow);
                        Output.Log(ymlLines[i].Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);
                        Output.Log($"\n\tNew: ", ConsoleColor.Yellow);
                        Output.Log(newLine.Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);

                        ymlLines[i] = newLine;

                        File.WriteAllLines(ymlPath, ymlLines);
                    }
                }
            }
            else
            {
                string[] ymlLines = File.ReadAllLines(ymlPath);
                for (int i = 0; i < ymlLines.Count(); i++)
                {
                    if (ymlLines[i].Contains(change.FieldName))
                    {
                        string newLine = change.FieldName + ": " + value;
                        int whiteSpaceCount = ymlLines[i].TakeWhile(c => c == ' ').Count() + newLine.Length;
                        newLine = newLine.PadLeft(whiteSpaceCount);

                        Output.Log($"\nChanged line {i} in YML: {ymlPath}");
                        Output.Log($"\tOld: ", ConsoleColor.Yellow);
                        Output.Log(ymlLines[i].Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Yellow);
                        Output.Log($"\n\tNew: ", ConsoleColor.Green);
                        Output.Log(newLine.Replace("{", "{{").Replace("}", "}}"), ConsoleColor.Green);

                        ymlLines[i] = newLine;

                        File.WriteAllLines(ymlPath, ymlLines);
                    }
                }
            }
        }

        private void ConvertYMLsToBYML()
        {
            Output.Log("Converting YML files back to BYML...");

            foreach (var file in Directory.GetFiles("./Temp", "*.yml", SearchOption.AllDirectories))
            {
                string outFile = file.Replace("_pack", "_pack_new").Replace(".yml", ".bgyml");
                // Create BYML from YML
                using (FileStream fs = new FileStream(outFile, FileMode.OpenOrCreate))
                {
                    int version = 7;
                    fs.Write(Byml.FromText(File.ReadAllText(file)).ToBinary(false, version).AsSpan());
                    Output.VerboseLog($"\tConverted .yml back to .bgyml: {outFile}\n\t\t(ver {version}, little endian)");
                }
            }
            Output.Log("Done converting to BYML.");
        }

        private void DeleteZsFiles(string directoryPath)
        {
            foreach (var file in Directory.GetFiles(directoryPath, "*.zs", SearchOption.AllDirectories))
            {
                using (FileSys.WaitForFile(file)) { }
                File.Delete(file);
            }
            Output.Log($"Deleted .zs files in directory: {directoryPath}", ConsoleColor.Yellow);
        }

        private void RebuildPacks()
        {
            Output.Log("Rebuilding .pack files...\n");

            foreach (var packFile in Directory.GetFiles($"./Temp/Pack/Actor/", "*.pack", SearchOption.TopDirectoryOnly))
            {
                string packDir = packFile.Replace(".pack", "_pack");
                string outDir = Path.Combine(txt_OutputPath.Text, "Pack/Actor");
                string outPath = Path.Combine(outDir, Path.GetFileName(packFile));

                // If matching folder for .pack exists...
                if (Directory.Exists(packDir))
                {
                    Output.Log($"Building .pack: {outPath}");

                    if (!formSettings.FullSARCRebuild)
                    {
                        // Replace only modded files in pack
                        PartialSARCRebuild(packDir + "_new", outPath);
                    }
                    else
                    {
                        // Copy modified files to pack dir
                        FileSys.CopyDir(packDir + "_new", packDir);
                        // Build new SARC from entire pack dir
                        SARC.Build(packDir, outPath);
                        Output.Log("Done rebuilding .pack.");
                    }
                }
            }

            Output.Log("Done rebuilding .pack files.", ConsoleColor.Green);
        }

        private void PartialSARCRebuild(string packDir, string outPath)
        {
            string packFile = packDir.Replace("_pack_new", ".pack");

            Sarc sarc = Sarc.FromBinary(File.ReadAllBytes(packFile));
            using (sarc)
            {
                // Get all files in Temp Sarc dir
                var sarcFilesDir = Directory.GetFiles(packDir + "/", "*", SearchOption.AllDirectories);

                // For each file in matching SARC dir...
                foreach (var newFile in sarcFilesDir)
                {
                    string relativePath = newFile.Substring(packDir.Length + 1).Replace("\\", "/");

                    // Add to SARC
                    Output.Log($"\tAdding file: {relativePath}", ConsoleColor.Gray);
                    sarc.Add(relativePath, File.ReadAllBytes(newFile));
                }

                // Create output directory if it doesn't already exist
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                // Save new .pack to output dir
                using (FileStream fs = new FileStream(outPath, FileMode.OpenOrCreate))
                {
                    fs.Write(sarc.ToBinary());
                    Output.Log("Done building SARC.");
                }
            }
        }

        private void PatchRESTBL()
        {
            string gameResTbl = Path.Combine(txt_GamePath.Text,
                $"System/Resource/ResourceSizeTable.Product.{formSettings.Version.Replace(".", "")}.rsizetable.zs");

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
                        UpdatePackRSTBEntries(restbl, file);
                    }
                }

                // Add additional entry updates via ./RSTB.txt
                UpdateEntriesFromTXT(restbl);

                // Save new RESTBL file
                string newResTbl = Path.Combine(txt_OutputPath.Text,
                    $"System/Resource/ResourceSizeTable.Product.{formSettings.Version.Replace(".", "")}.rsizetable.zs");

                Compress(restbl.ToBinary(), newResTbl);
                Output.Log($"Saved new Resource Table file to: {newResTbl}", ConsoleColor.Green);
            }
            else
                Output.Log($"Could not find Resource Table file at: {gameResTbl}", ConsoleColor.Red);
        }

        private void UpdateEntriesFromTXT(Restbl restbl)
        {
            if (File.Exists("./RSTB.txt"))
            {
                foreach (var line in File.ReadAllLines("./RSTB.txt"))
                {
                    string[] splitLine = line.Split(' ');
                    if (splitLine.Count() > 1)
                    {
                        string filePath = splitLine[0].Replace("\\", "/");
                        uint size = Convert.ToUInt32(splitLine[1]) + (uint)formSettings.Padding;

                        // Completely remove RSTB entry instead of replacing if padding is negative
                        if (formSettings.Padding > 0)
                            UpdateRSTBEntry(restbl, filePath, size, true);
                        else
                            RemoveRSTBEntry(restbl, filePath);
                    }
                }
            }
        }

        private void RemoveRSTBEntry(Restbl restbl, string relativePath)
        {
            uint pathCrc = StringToCRC32(relativePath);

            if (restbl.CrcTable.Any(x => x.Hash.Equals(pathCrc)))
            {
                Output.Log($"Updating RESTBL entry for: {relativePath}");

                var entry = restbl.CrcTable.First(x => x.Hash.Equals(pathCrc));
                restbl.CrcTable.Remove(restbl.CrcTable.First(x => x.Hash.Equals(pathCrc)));
                Output.Log($"\tRemoved CRC32 Table Entry:\n\t\tHash: {entry.Hash}\n\t\tSize: {entry.Size}", ConsoleColor.DarkYellow);
            }
        }

        private void UpdatePackRSTBEntries(Restbl restbl, string path)
        {
            string uncompressedSARC = path.Replace(".zs", "");
            string relativePath = uncompressedSARC.Substring(txt_OutputPath.Text.Length + 1).Replace("\\", "/");

            // Add/update CRC32 entry for the decompressed .pack itself
            if (File.Exists(uncompressedSARC))
            {
                long fileLength = new FileInfo(uncompressedSARC).Length;
                uint newSize = Convert.ToUInt32(fileLength + formSettings.Padding);

                // Completely remove RSTB entry instead of replacing if padding is negative
                if (formSettings.Padding > 0)
                    UpdateRSTBEntry(restbl, relativePath, newSize, true);
                else
                    RemoveRSTBEntry(restbl, relativePath);

                // Add/update CRC32 entry for the decompressed files within matching Actor Pack
                string tempFolder = $"./Temp/Pack/Actor/{Path.GetFileNameWithoutExtension(uncompressedSARC)}_pack_new";
                if (Directory.Exists(tempFolder))
                {
                    foreach (var file in Directory.GetFiles(tempFolder, "*", SearchOption.AllDirectories))
                    {
                        string relativeFilePath = file.Substring(tempFolder.Length + 1).Replace("\\", "/");
                        newSize = Convert.ToUInt32(new FileInfo(file).Length + formSettings.Padding);

                        // Completely remove RSTB entry instead of replacing if padding is negative
                        if (formSettings.Padding > 0)
                            UpdateRSTBEntry(restbl, relativeFilePath, newSize, true);
                        else
                            RemoveRSTBEntry(restbl, relativeFilePath);
                    }
                }
            }
        }

        private void UpdateRSTBEntry(Restbl restbl, string relativePath, uint newSize, bool forceAdd = false)
        {
            uint pathCrc = StringToCRC32(relativePath);

            if (restbl.CrcTable.Any(x => x.Hash.Equals(pathCrc)))
            {
                Output.Log($"Updating RESTBL entry for: {relativePath}");

                var entry = restbl.CrcTable.First(x => x.Hash.Equals(pathCrc));
                restbl.CrcTable.Remove(restbl.CrcTable.First(x => x.Hash.Equals(pathCrc)));
                Output.Log($"\tRemoved CRC32 Table Entry:\n\t\tHash: {entry.Hash}\n\t\tSize: {entry.Size}", ConsoleColor.Yellow);

                restbl.CrcTable.Add(new CrcEntry(pathCrc, newSize));
                Output.Log($"\tAdded CRC32 Table Entry:\n\t\tHash: {pathCrc}\n\t\tSize: {newSize}");
            }
            else if (forceAdd)
            {
                Output.Log($"Adding new RESTBL entry for: {relativePath}");
                restbl.CrcTable.Add(new CrcEntry(pathCrc, newSize));
                Output.Log($"\tAdded CRC32 Table Entry:\n\t\tHash: {pathCrc}\n\t\tSize: {newSize}");
            }
        }

        private static uint StringToCRC32(string path)
        {
            return CRC.Crc32(Encoding.ASCII.GetBytes(path));
        }

        private void CompareModToOGFiles()
        {
            string modPath = ChooseFolder("Choose mod folder to compare with game files");
            string modActorPath = Path.Combine(modPath, "Pack/Actor");
            string gameActorPath = Path.Combine(formSettings.GamePath, "Pack/Actor");

            // Warn user if required directories aren't present, otherwise continue
            if (!Directory.Exists(modActorPath) || !Directory.Exists(gameActorPath))
            {
                MessageBox.Show("Failed to compare mod to game files, please ensure " +
                    "/Pack/Actor/ directory exists in both folders!");
                Output.Log($"Comparison failed.", ConsoleColor.Red);
            }
            else
            {
                // Get mod name from user
                string modName = $"Compare_{Path.GetFileName(modPath)}";
                RenameForm rename = new RenameForm(modName);
                var result = rename.ShowDialog();
                if (result == DialogResult.OK)
                {
                    modName = rename.RenameText;
                    if (string.IsNullOrEmpty(modName))
                        modName = $"Compare_{Path.GetFileName(modPath)}";
                }
                else
                {
                    return;
                }
                string compareOutPath = $"./Dependencies/{modName}";

                // If comparison path already exists, delete it and recreate it
                if (Directory.Exists(compareOutPath))
                    Directory.Delete(compareOutPath, true);
                Directory.CreateDirectory(compareOutPath);

                Output.Log($"Comparing Actor Pack files between\n\tOriginal: {gameActorPath}\n\tMod: {modActorPath}", ConsoleColor.White);

                string compareTempPath = "./CompareTemp";
                if (Directory.Exists(compareTempPath))
                    Directory.Delete(compareTempPath, true);
                Directory.CreateDirectory(compareTempPath);

                string gameTempPath = Path.Combine(compareTempPath, "Game");
                ExtractPacks(formSettings.GamePath, gameTempPath, true);
                string modTempPath = Path.Combine(compareTempPath, "Mod");
                ExtractPacks(modPath, modTempPath, true);

                // For each extracted file in mod folder...
                foreach (var modFile in Directory.GetFiles(modTempPath, "*", SearchOption.AllDirectories))
                {
                    string modFileRelativePath = modFile.Substring(modTempPath.Length + 1);
                    string gameFile = Path.Combine(gameTempPath, modFileRelativePath);

                    // If matching file in game dir exists doesn't exist or there's a binary difference...
                    if (!File.Exists(gameFile) || !ByteArraysEqual(File.ReadAllBytes(gameFile), File.ReadAllBytes(modFile)) )
                    {
                        string outFile = Path.Combine(compareOutPath, modFileRelativePath.Replace("Pack\\Actor\\",""));
                        Directory.CreateDirectory(Path.GetDirectoryName(outFile));
                        File.Copy(modFile, outFile, true);
                        Output.VerboseLog($"Copied new or modified file to: {outFile}", ConsoleColor.DarkGreen);
                    }
                }

                // Delete temporary comparison directory
                Directory.Delete(compareTempPath, true);

                MessageBox.Show($"Comparison complete, modded files have been added to the Dependencies/{modName} folder." +
                    "\n\nContents of the Dependencies folder will automatically be merged with output generated by this tool." +
                    "\nPlease move the folder to somewhere else if you do not want it merged.");
                Output.Log($"Comparison succeeded.", ConsoleColor.Green);
            }
        }

        static bool ByteArraysEqual(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
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
    }

    public class Change
    {
        public string File = "";
        public string Path = "";
        public string FieldName = "";
        public string Value = "";
        public string OGValue = "";
        public string Type = "String";
    }

    public class Option
    {
        public string Name = "";
        public string Hint = "";
        public List<Change> Changes = new List<Change>();
        public bool Enabled = true;
    }
}