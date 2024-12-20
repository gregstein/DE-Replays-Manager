﻿using ComponentFactory.Krypton.Toolkit;
using De_Roll;
using DEBoard;
using DeReplaysManager;
using DevComponents.DotNetBar;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEReplaysManager
{

    public partial class Form1 : OfficeForm
    {
        KeyboardHook hook = new KeyboardHook();
        public Form1()
        {
            InitializeComponent();
            //DEL recs event

            //hook.KeyPressed +=
            //    new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            //hook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Alt,Keys.F12);
            //hook.RegisterHotKey(Keys.Delete);
        }
        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            //MessageBox.Show(e.Modifier.ToString() + " + " + e.Key.ToString());
            DeleteRECS();

        }
        public Uri apiLOB;
        public List<string> DIRprofiles = new List<string>();
        Dictionary<string, string> USERprofiles = new Dictionary<string, string>();
        //public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        private string filterText = String.Empty;
        private string cbChanged = String.Empty;
        public string SaveGame;
        public string RepZip = System.IO.Path.GetTempPath() + @"\AgeIIDE_Replay.zip";
        public static List<int> refver = new List<int>{
                                                        40220,
                                                        39515,
                                                        39284,
                                                        37906,
                                                        37650,
                                                        36906,
                                                        35584,
                                                        35209,
                                                        34699,
                                                        34397,
                                                        34055,
                                                        33315,
                                                        33164,
                                                        33059,
                                                        32911
                                                       };
        public static List<string> refvst = new List<string>{
                                                        "21 October 2020 (41855)",
                                                        "23 September 2020 (40874)",
                                                        "25 August 2020 (40220)",
                                                        "28 July 2020 (39515)",
                                                        "21 July 2020 (39284)",
                                                        "3 June 2020 (37906)",
                                                        "28 May 2020 (37650)",
                                                        "30 April 2020 (36906)",
                                                        "27 February 2020 (35584)",
                                                        "13 February 2020 (35209)",
                                                        "21 January 2020 (34699)",
                                                        "13 January 2020 (34397)",
                                                        "17 December 2019 (34055)",
                                                        "27 November 2019 (33315)",
                                                        "22 November 2019 (33164)",
                                                        "20 November 2019 (33059)",
                                                        "16 November 2019 (32911)"
                                                        };
        private List<int> lstrev = new List<int>();
        private string GameFN;
        private List<string> Srevs = new List<string>();
        private void RefreshSaves(string mydir)
        {
            SaveGame = mydir + "\\savegame";
            repLIST.Items.Clear();
            var allFiles = GetRECFilesByLastWriteTime(mydir + "\\savegame").ToList();  //store the files in the string array

            for (int i = 0; i < allFiles.Count; i++)
            {
                repLIST.Items.Add(allFiles[i].Replace(mydir + "\\savegame\\", ""));
            }
        }
        public static IEnumerable<string> GetRECFilesByLastWriteTime(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists) return Enumerable.Empty<string>();

            var query =
                from file in directoryInfo.GetFiles()
                where file.Extension.ToLower() == ".aoe2record"
                orderby file.LastWriteTime descending
                select file.Name;

            return query;
        }
        private DateTime latestEDIT(string dir)
        {
            DateTime ftime = File.GetLastWriteTime(dir);
            return ftime;
        }
        public static string Unistring(string strg)
        {
            string unicodeString = strg;

            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            return asciiString;
        }
        public void Decompress2(string hkiprofile)
        {
            DEparser stp = new DEparser();
            string newFileName = hkiprofile.Replace(".hki", ".db");


            //File.WriteAllBytes("test.txt", Deziphki);

            stp.GetPlayernfp(newFileName);
            //MessageBox.Show(stp.playerTEXT);





            if (!USERprofiles.ContainsKey(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "")))
            {
                USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""), hkiprofile.Replace(@"\profile\Player.nfp", ""));
                listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""));
            }
            else
            {
                USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)", hkiprofile.Replace(@"\profile\Player.nfp", ""));
                listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)");
            }

        }

        public void Decompress3(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Replace(@".nfp", ".db");

                using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(decompressionStream))
                    {
                        string player = reader.ReadToEnd();

                        //File.WriteAllBytes(newFileName, bytes);
                        DEparser stp = new DEparser();
                        //stp.GetPlayernfp(newFileName);
                        System.Windows.Forms.Clipboard.SetText(newFileName);
                        MessageBox.Show(newFileName);





                        if (!USERprofiles.ContainsKey(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "")))
                        {
                            USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""), currentFileName.Replace(@"\profile\Player.nfp", ""));
                            listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""));
                        }
                        else
                        {
                            USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)", currentFileName.Replace(@"\profile\Player.nfp", ""));
                            listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)");
                        }


                        //MessageBox.Show(player);
                    }

                }
                //using (FileStream decompressedFileStream = File.Create(newFileName))
                //{
                //    using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                //    {
                //        decompressionStream.CopyTo(decompressedFileStream);
                //        MessageBox.Show("Decompressed: {0}", fileToDecompress.Name);
                //    }
                //}
            }
        }
        public string GetLastDirectory(string directory, string pattern = "*")
        {
            if (directory.Trim().Length == 0)
                return string.Empty; //Error handler can go here

            if ((pattern.Trim().Length == 0) || (pattern.Substring(pattern.Length - 1) == "."))
                return string.Empty; //Error handler can go here

            if (Directory.GetDirectories(directory, pattern).Length == 0)
                return string.Empty; //Error handler can go here

            //string pattern = "*.txt"

            var dirInfo = new DirectoryInfo(directory);
            var file = (from f in dirInfo.GetDirectories(pattern) orderby f.LastWriteTime descending select f).First();

            return file.ToString();
        }
        static bool isExcluded(List<string> exludedDirList, string target)
        {
            return exludedDirList.Any(d => new DirectoryInfo(target).Name.Equals(d));
        }
        private async void ScanDirectories()
        {
            DEparser dp = new DEparser();
            //string[] subdirectoryEntries = Directory.GetDirectories(dp.dePATH);
            var subdirectoryEntries = new DirectoryInfo(dp.dePATH).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime);
            bool isONCE = false;
            int i = 0;
            foreach (DirectoryInfo subdirectory in subdirectoryEntries)
            {

                if (dp.IsDigitsOnly(subdirectory.FullName.Replace(dp.dePATH + "\\", "")) && subdirectory.FullName.Length > 4 && subdirectory.FullName.Replace(dp.dePATH + "\\", "") != "0")
                {
                    DIRprofiles.Add(subdirectory.FullName);
                    DirectoryInfo info = new DirectoryInfo(subdirectory.FullName + "\\profile");
                    FileInfo[] files = info.GetFiles("*.hki").OrderByDescending(p => p.LastWriteTime).ToArray();
                    foreach (FileInfo file in files)
                    {
                        i++;
                        if (listPROFILE.Items.Contains(file.Name.Replace(".hki", "")))
                        {
                            listPROFILE.Items.Add(file.Name.Replace(".hki", "") + $"({i})");
                            USERprofiles.Add(file.Name.Replace(".hki", "") + $"({i})", subdirectory.FullName);
                            break;
                        }
                        else
                        {
                            if (!isONCE)
                            {
                                isONCE = true;
                                listPROFILE.Text = file.FullName.Replace(".hki", "");
                            }
                            listPROFILE.Items.Add(file.Name.Replace(".hki", ""));
                            USERprofiles.Add(file.Name.Replace(".hki", ""), subdirectory.FullName);
                            break;

                        }






                    }

                    //    FileInfo fl = new FileInfo(subdirectory + @"\profile\Player.nfp");
                    //DEparser stp = new DEparser();
                    //stp.Decompress(fl);

                    //stp.GetPlayernfp(stp.newFileName);
                    //System.Windows.Forms.Clipboard.SetText(stp.newFileName);
                    //MessageBox.Show(stp.newFileName);





                    //if (!USERprofiles.ContainsKey(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "")))
                    //{
                    //    USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""), stp.currentFileName.Replace(@"\profile\Player.nfp", ""));
                    //    listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", ""));
                    //}
                    //else
                    //{
                    //    USERprofiles.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)", stp.currentFileName.Replace(@"\profile\Player.nfp", ""));
                    //    listPROFILE.Items.Add(stp.CollectPlayerName(stp.playerTEXT).Replace(".hki", "") + @"(2)");
                    //}


                }
            }


            var lastDirectory = new DirectoryInfo(dp.dePATH).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTimeUtc);
            foreach (DirectoryInfo fdir in lastDirectory)
            {
                //MessageBox.Show(fdir);
                if (dp.IsDigitsOnly(fdir.FullName.Replace(dp.dePATH + "\\", "")) && fdir.FullName.Length > 4 && fdir.FullName.Replace(dp.dePATH + "\\", "") != "0")
                {

                    //MessageBox.Show(fdir.FullName);
                    foreach (KeyValuePair<string, string> item in USERprofiles)
                    {
                        if (fdir.FullName == item.Value)
                        {
                            RefreshSaves(fdir.FullName);
                            listPROFILE.SelectedIndex = listPROFILE.FindStringExact(item.Key);
                        }

                    }


                    break;
                }
            }

            //END
            //Check Settings

            await GrabDef();
            if (GetREG() != null)
            {
                string[] sp = GetREG().Split(new string[] { "|>" }, StringSplitOptions.None);
                if (Directory.Exists(sp[1]))
                {
                    listPROFILE.Text = sp[0];
                    defSAVE.Text = sp[0];
                }
            }
        }
        private string GetDate(string file)
        {
            DateTime modification = File.GetLastWriteTime(file);
            return modification.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
        }
        public int GetDay(string newline, int dig, int index)
        {
            // Part 1: the input string.
            string input = newline;

            // Part 2: call Regex.Match.
            Match match = Regex.Match(input, @"\d{" + dig + @"}",
                RegexOptions.IgnoreCase);

            // Part 3: check the Match for Success.
            if (match.Success)
            {
                // Part 4: get the Group value and display it.
                int key = int.Parse(match.Groups[index].Value);
                return key;
            }
            return 0;
        }
        private string FetchPatch(string date)
        {
            DateTime fDate = Convert.ToDateTime(date);
            DateTime p1 = new DateTime(2019, 11, 16);
            DateTime p2 = new DateTime(2019, 11, 20);
            DateTime p3 = new DateTime(2019, 11, 22);
            DateTime p4 = new DateTime(2019, 11, 27);
            DateTime p5 = new DateTime(2019, 12, 17);
            DateTime p6 = new DateTime(2020, 1, 13);
            DateTime p7 = new DateTime(2020, 1, 21);
            DateTime p8 = new DateTime(2020, 2, 13);
            DateTime p9 = new DateTime(2020, 2, 27);
            DateTime p10 = new DateTime(2020, 4, 30);
            DateTime p11 = new DateTime(2020, 5, 28);
            DateTime p12 = new DateTime(2020, 6, 3);
            DateTime p13 = new DateTime(2020, 7, 21);
            DateTime p14 = new DateTime(2020, 7, 28);
            DateTime p15 = new DateTime(2020, 8, 25);
            DateTime p16 = new DateTime(2020, 9, 23);
            DateTime p17 = new DateTime(2020, 10, 21);
            DateTime p18 = new DateTime(2020, 11, 17);
            DateTime p19 = new DateTime(2020, 11, 25);
            DateTime p20 = new DateTime(2021, 1, 25);
            DateTime p21 = new DateTime(2021, 1, 28);
            DateTime p22 = new DateTime(2021, 2, 11);
            DateTime p23 = new DateTime(2021, 2, 12);
            if (fDate >= p1 && fDate < p2)
                return @"16 November 2019 (32911)";
            else if (fDate >= p2 && fDate < p3)
                return @"20 November 2019 (33059)";
            else if (fDate >= p3 && fDate < p4)
                return @"22 November 2019 (33164)";
            else if (fDate >= p4 && fDate < p5)
                return @"27 November 2019 (33315)";
            else if (fDate >= p5 && fDate < p6)
                return @"17 December 2019 (34055)";
            else if (fDate >= p6 && fDate < p7)
                return @"13 January 2020 (34397)";
            else if (fDate >= p7 && fDate < p8)
                return @"21 January 2020 (34699)";
            else if (fDate >= p8 && fDate < p9)
                return @"13 February 2020 (35209)";
            else if (fDate >= p9 && fDate < p10)
                return @"27 February 2020 (35584)";
            else if (fDate >= p10 && fDate < p11)
                return @"30 April 2020 (36906)";
            else if (fDate >= p11 && fDate < p12)
                return @"28 May 2020 (37650)";
            else if (fDate >= p12 && fDate < p13)
                return @"03 June 2020 (37906)";
            else if (fDate >= p13 && fDate < p14)
                return @"21 July 2020 (39284)";
            else if (fDate >= p14 && fDate < p15)
                return @"28 July 2020 (39515)";
            else if (fDate >= p15 && fDate < p16)
                return @"25 August 2020 (40220)";
            else if (fDate >= p16 && fDate < p17)
                return @"23 September 2020 (40874)";
            else if (fDate >= p17 && fDate < p18)
                return @"21 October 2020 (41855)";
            else if (fDate >= p18 && fDate < p19)
                return @"17 November 2020 (42848)";
            else if (fDate >= p19 && fDate < p20)
                return @"25 November 2020 (43210)";
            else if (fDate >= p20 && fDate < p21)
                return @"25 January 2021 (44725)";
            else if (fDate >= p21 && fDate < p22)
                return @"28 January 2021 (44834)";
            else if (fDate >= p22 && fDate < p23)
                return @"11 February 2021 (45185)";
            else if (fDate >= p23)
                return @"12 February 2021 (45186)";
            return null;
        }






        private void UpdateSideNavDock()
        {
            if (sideNav1.EnableClose || sideNav1.EnableMaximize || sideNav1.EnableSplitter)
            {
                if (sideNav1.Dock != DockStyle.Left)
                {
                    sideNav1.Dock = DockStyle.Left;
                    sideNav1.Width = this.ClientRectangle.Width - 32;
                    ToastNotification.Close(this); // Closes any toast messages if already open
                    ToastNotification.Show(this, "", 4000);
                }
            }
            else if (sideNav1.Dock != DockStyle.Fill)
            {
                sideNav1.Dock = DockStyle.Fill;
                ToastNotification.Close(this); // Closes any toast messages if already open
                ToastNotification.Show(this, "", 2000);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxEx1.SelectedItem == null) return;
            //eStyle style = (eStyle)comboBoxEx1.SelectedItem;
            //if (styleManager1.ManagerStyle != style)
            //    styleManager1.ManagerStyle = style;
        }

        private void labelX13_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            System.Diagnostics.Process.Start("");
        }
        public static void procCMD(string mycmd)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + mycmd;
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += " " + File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "version.txt").Replace(".0.0", "");
            ScanDirectories();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                this.TopMost = true;
            else
                this.TopMost = false;
        }

        private void listPROFILE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPROFILE.SelectedIndex >= 0)
            {
                foreach (KeyValuePair<string, string> item in USERprofiles)
                {
                    if (listPROFILE.SelectedItem.ToString() == item.Key)
                    {
                        RefreshSaves(item.Value);

                    }

                }
            }
        }

        private void renameREC_Click(object sender, EventArgs e)
        {
            if (repLIST.SelectedIndex >= 0)
            {
                string oldrec = SaveGame + @"\" + repLIST.SelectedItem.ToString();
                string input = repLIST.SelectedItem.ToString().Replace(".aoe2record", "");
                ShowInputDialog(ref input, oldrec, true);
                repLIST.Items.Clear();
                RefreshSaves(SaveGame.Replace(@"\savegame", ""));
            }
        }
        async Task<bool> ProcessZIP(string input, string oldpath, bool moveit = false)
        {
            string inputn = input;
            if (input.Contains(".aoe2record"))
                inputn = input.Replace(".aoe2record", "");

            if (File.Exists(SaveGame + @"\" + input + ".aoe2record") && !input.Contains("AgeIIDE_Replay"))
            {
                MessageBox.Show("Replay Name Already Exists! Use different name");
            }
            else
            {
                try
                {
                    if (moveit == true)
                        File.Move(oldpath, SaveGame + @"\" + inputn + ".aoe2record");
                    else
                        File.Copy(oldpath, SaveGame + @"\" + inputn + ".aoe2record");

                }
                catch (SystemException ez)
                {
                    MessageBox.Show(ez.ToString());
                    return false;

                }
                repLIST.Items.Clear();
                RefreshSaves(SaveGame.Replace(@"\savegame", ""));
                int index = repLIST.FindString(inputn + ".aoe2record");
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    repLIST.SetSelected(index, true);
                return true;
            }
            await Task.Delay(5);
            return true;
        }
        private DialogResult ShowInputDialog(ref string input, string oldNameFullPath, bool moveit, bool skip = false)
        {
            string sinput = input + ".aoe2record";


            System.Drawing.Size size = new System.Drawing.Size(300, 70);
            KryptonForm inputBox = new KryptonForm();
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Rename Replay";
            inputBox.TopMost = true;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input.Replace(".aoe2record", "");
            textBox.Select(0, 0);

            textBox.Font = new Font(textBox.Font.FontFamily, 12);
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            okButton.Click += async (s, e) =>
            {
                await ProcessZIP(sinput, oldNameFullPath);

            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text.Replace(".aoe2record", "") + ".aoe2record";
            return result;
        }
        private void deleteREC_Click(object sender, EventArgs e)
        {
            DeleteRECS();
        }
        private void DeleteRECS()
        {
            List<string> todel = new List<string>();
            if (repLIST.SelectedIndex >= 0)
            {
                int cnt = repLIST.SelectedItems.Count;

                string delmsg = (cnt == 1) ? repLIST.SelectedItem.ToString() : cnt.ToString();
                DialogResult dialogResult = MessageBox.Show("Delete " + delmsg + @" Replays?", "Confirm Replay Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        foreach (string st in repLIST.SelectedItems)
                            todel.Add(st);

                        foreach (string st in todel)
                        {
                            File.Delete(Path.Combine(SaveGame, st));
                            repLIST.Items.Remove(st);
                        }


                    }
                    catch (SystemException)
                    {
                        //MessageBox.Show(ez.ToString());
                    }

                }
                else if (dialogResult == DialogResult.No)
                {

                }


            }
        }
        private void exportREC_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archive Zip|*.zip";
            saveFileDialog1.Title = "Save Zip File To";
            saveFileDialog1.ShowDialog();





            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                //backgroundWorker2.RunWorkerAsync();



                String ZipFileToCreate = saveFileDialog1.FileName;

                if (File.Exists(ZipFileToCreate))
                    File.Delete(ZipFileToCreate);

                using (ZipArchive zip = ZipFile.Open(ZipFileToCreate, ZipArchiveMode.Create))
                {
                    foreach (var mod in repLIST.SelectedItems)
                    {
                        zip.CreateEntryFromFile(SaveGame + @"\" + mod.ToString(), mod.ToString());
                    }

                }
                FileInfo sizez = new FileInfo(saveFileDialog1.FileName);
                MessageBox.Show("Zip Size: " + FormatByteSize(sizez.Length), "Replays Exported");
            }
            else
            {

            }
        }
        private static string FormatByteSize(double bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "TB" };
            int index = 0;
            do { bytes /= 1024; index++; }
            while (bytes >= 1024);
            return String.Format("{0:0.00} {1}", bytes, Suffix[index]);

        }
        private void openSAVE_Click(object sender, EventArgs e)
        {
            Process.Start(SaveGame);
        }

        private void repLIST_SelectedIndexChanged(object sender, EventArgs e)
        {

            ingameCHAT.Text = "";
            playerNAMES.Items.Clear();
            if (repLIST.SelectedIndex >= 0 && repLIST.SelectedIndices.Count == 1)
            {
                DEparser recp = new DEparser();
                string chat = recp.GetStrings(SaveGame + @"\" + repLIST.SelectedItem.ToString());
                recp.CollectPlayers(chat);
                foreach (string str in recp.playerDB.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct())
                {
                    playerNAMES.Items.Add(str);
                }
                recp.CollectChat(chat);
                foreach (string cl in recp.chatDB.Distinct().ToList())
                {
                    ingameCHAT.Text += cl + "\n";
                }
                dateREC.Text = GetDate(SaveGame + @"\" + repLIST.SelectedItem.ToString());
                patchREC.Text = FetchPatch(GetDate(SaveGame + @"\" + repLIST.SelectedItem.ToString()));

            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {


        }
        public void RepDownloader(string zipfile)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                System.Net.WebClient webClient = new System.Net.WebClient();
                //webClient.Headers.Add("User-Agent: Other"); 
                //webClient.Headers.Add("Content-Type", "application/zip");
                //webClient.UseDefaultCredentials = true;
                //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                //webClient.Headers.Add(HttpRequestHeader.UserAgent, "DE Replays Manager");
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadFileAsync(new Uri(zipfile), RepZip);
            }
            catch (SystemException)
            {
                KryptonMessageBox.Show("Your internet dropped! Restart AOE2Tools and try again!", "Connection Lost");
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            proBAR.Maximum = (int)e.TotalBytesToReceive / 100;
            proBAR.Value = (int)e.BytesReceived / 100;

        }
        private async void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string recname = "";
            using (ZipArchive archive = ZipFile.OpenRead(RepZip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    recname = Path.Combine(SaveGame, entry.FullName);

                    entry.ExtractToFile(Path.Combine(SaveGame, entry.FullName));
                }
            }
            //Delete Temp Zip File
            try { File.Delete(RepZip); } catch (SystemException) { }
            //Prompt Replay Renamer
            string oldrec = recname;
            string input = recname.Replace(".aoe2record", "").Replace(SaveGame + @"\", "");
            ShowInputDialog(ref input, oldrec, true);
            lblsuccess.Visible = true;
            picsuccess.Visible = true;
            await Task.Delay(4000);
            lblsuccess.Visible = false;
            picsuccess.Visible = false;

        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {

            RepDownloader(e.Data.GetData(DataFormats.Text).ToString());
            repLIST.Items.Clear();
            RefreshSaves(SaveGame.Replace(@"\savegame", ""));

        }

        private void panel3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void refreshersaves_Click(object sender, EventArgs e)
        {
            repLIST.Items.Clear();
            listPROFILE.Items.Clear();
            USERprofiles.Clear();
            defSAVE.Items.Clear();
            repLIST.Items.Clear();
            ScanDirectories();
            //RefreshSaves(SaveGame.Replace(@"\savegame", ""));
        }
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            string output = null;
            bool cancelledOrError = false;
            using (var client = new System.Net.WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Proxy = null;
                client.DownloadStringCompleted += (sender, e) =>
                {
                    if (e.Error != null || e.Cancelled)
                    {
                        cancelledOrError = true;
                    }
                    else
                    {
                        output = e.Result;
                    }
                };
                client.DownloadStringAsync(uri);

                var n = DateTime.Now;
                while (output == null && !cancelledOrError && DateTime.Now.Subtract(n).TotalMilliseconds < timeOut)
                {

                    await Task.Delay(100); // wait for respsonse
                }

            }

            return await Task.FromResult(output);
        }
        public void RemoveDuplicate(DataGridView grv)
        {
            for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = grv.Rows[currentRow];

                for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = grv.Rows[otherRow];

                    bool duplicateRow = true;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value))
                        {
                            duplicateRow = false;
                            break;
                        }
                    }

                    if (duplicateRow)
                    {
                        grv.Rows.Remove(row);
                        otherRow--;
                    }
                }
            }
        }
        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            PlayerELOPars();
        }
        private async void PlayerELOPars()
        {
            try
            {


                kryptonDataGridView3.Rows.Clear();
                await Task.Delay(500);

                if (filterText != kryptonTextBox2.Text || cbChanged != gameid.Text)
                {
                    cbChanged = gameid.Text;
                    filterText = kryptonTextBox2.Text;
                    if (gameid.Text == "1V1")
                        apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&search=" + kryptonTextBox2.Text);
                    else if (gameid.Text == "TG")
                        apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=4&start=1&search=" + kryptonTextBox2.Text);
                    string jsonLOBBIES = await DownloadStringAsync(apiLOB);

                    var ldb = QueryPlayer.FromJson(jsonLOBBIES);

                    int i = 1;
                    foreach (var l in ldb.Leaderboard)
                    {
                        i++;
                        LinkLabel ln = new LinkLabel();
                        Button btn = new Button();
                        Bitmap im = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + @"flags\" + l.Country.ToString().ToLower() + ".png");


                        ln.Text = "View";
                        ln.Name = "view" + i.ToString();
                        btn.Name = "btn" + i.ToString();
                        btn.Text = "Join";
                        kryptonDataGridView3.Rows.Add(im, l.Name, l.Rating, l.HighestRating, l.Wins + @" / " + l.Losses, "Replays", "Profile", @"https://www.ageofempires.com/stats/?profileId=" + l.ProfileId + @"&game=age2", @"https://steamcommunity.com/profiles/" + l.SteamId, l.Rank);
                    }
                    RemoveDuplicate(kryptonDataGridView3);
                }
                if (kryptonTextBox2.Text == null)
                {
                    kryptonDataGridView3.Rows.Clear();
                }
            }
            catch (SystemException)
            {

            }
        }
        private void kryptonDataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[7].Value.ToString());

                }
                if (e.ColumnIndex == 6)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[8].Value.ToString());

                }
            }
            catch (SystemException)
            {
            }
        }
        private void AccessLink(string link)
        {
            try
            {
                Process.Start(link);
            }
            catch (SystemException)
            {

            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccessLink("https://discord.gg/DPgk4PJ");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccessLink("https://github.com/gregstein/DE-Replays-Manager");

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AccessLink("https://forums.ageofempires.com/u/gregrising/summary");
        }

        private void sideNavItem1_Click(object sender, EventArgs e)
        {

        }

        private void updateschecker_Click(object sender, EventArgs e)
        {
            AccessLink(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe");
        }
        void DERMChecker()
        {
            try
            {
                //Get latest version code
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "version.txt"))
                {
                    string version = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "version.txt");

                    System.Net.WebClient wk = new System.Net.WebClient();
                    wk.Headers.Add("user-agent", "DE Replays Manager");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.Expect100Continue = false; ServicePointManager.MaxServicePointIdleTime = 0;
                    var _strwk = wk.DownloadString("https://api.github.com/repos/gregstein/DE-Replays-Manager/releases/latest");

                    var jObject = Newtonsoft.Json.Linq.JObject.Parse(_strwk);
                    string uptag = (string)jObject["tag_name"];
                    string updatezip = (string)jObject["assets"][1]["browser_download_url"];
                    if (version != uptag)
                    {
                        DialogResult dialogResult = MessageBox.Show("Update Now?", "New Update: DERM V " + uptag, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe");
                        }
                        else if (dialogResult == DialogResult.No)
                        {

                        }


                    }
                    else
                    {
                        KryptonMessageBox.Show("No updates found!", "No Updates");
                    }
                }
            }
            catch (System.Net.WebException)
            {
                KryptonMessageBox.Show("Error while checking for Updates! Please try later.", "Network Error");
            }

        }


        private void downgtoolbtn_Click(object sender, EventArgs e)
        {
            De_Roll.Form1 dgt = new De_Roll.Form1();
            dgt.Show();
        }

        private void patchREC_Paint(object sender, PaintEventArgs e)
        {

        }
        public int PatchVersionSensor(string myrec)
        {
            //progressBar2.Maximum = refver.Count;
            //int idc = 0;
            foreach (int id in refver)
            {
                //progressBar2.Value = idc++;
                Stream f = File.OpenRead(SaveGame + "\\" + myrec);
                BinaryReader br = new BinaryReader(f);

                for (int i = 0; i < f.Length - 10; i++)
                {
                    f.Seek(i, SeekOrigin.Begin);
                    if (br.ReadUInt16() == id)
                    {
                        foreach (string sd in refvst)
                        {
                            if (sd.Contains(id.ToString()))
                                patchREC.Text = sd;
                            return id;
                        }


                        //Console.WriteLine("Found @ {0}", i);
                    }
                }
                br.Close();
            }


            return 0;
        }
        private void patchREC_Click(object sender, EventArgs e)
        {
            //if (patchREC.Text != "Check Version")
            //{

            //    DialogResult dialogResult = DevComponents.DotNetBar.MessageBoxEx.Show("Would You like To Downgrade AoE2 DE To Patch: " + patchREC.Text + " ?", "Downgrade Tool prompt", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        //prompt downgrate tool with patch parameter
            //        De_Roll.Form1 dgt = new De_Roll.Form1();
            //        dgt.SetPatchVer = patchREC.Text;
            //        dgt.Show();

            //    }
            //    else if (dialogResult == DialogResult.No)
            //    {

            //    }

            //}
            //else
            //{
            //    PatchVersionSensor(repLIST.SelectedItem.ToString());
            //}

        }
        public bool PSubmiter(ListBoxItem rec, string title)
        {
            //ListBoxItem rec = new ListBoxItem();
            rec.Text = title;
            rec.Image = DeReplaysManager.Properties.Resources.clip_rec;
            listsubmitee.Items.Add(rec);
            return true;
        }
        public bool PReviewer(ListBoxItem rec, string user)
        {
            //ListBoxItem rec = new ListBoxItem();
            rec.Text = user;
            rec.Image = DeReplaysManager.Properties.Resources.defuser;
            listsubmitee.Items.Add(rec);
            return true;
        }
        public async Task<int> IterateReviewsCount(string recnm)
        {
            pgsbar.Visible = true;
            Core it = new Core();

            using (var dbx = new DropboxClient(it._key))
            {
                var sharedLink = new SharedLink("derm");
                var sharedFiles = await dbx.Files.ListFolderAsync("", true);
                int j = 0;
                foreach (var file in sharedFiles.Entries)
                {

                    ListBoxItem st = new ListBoxItem();
                    if (file.PathDisplay.EndsWith(".rev") && file.Name != "derm" && file.Name.StartsWith(recnm))
                    {
                        j++;
                        Regex word = new Regex(@"(.*)-(\d+?).rev");
                        Match m = word.Match(file.Name);


                        if (!lstrev.Contains(int.Parse(m.Groups[2].Value)))
                        {
                            lstrev.Add(j);
                        }



                    }


                }

                pgsbar.Visible = false;
                return lstrev.Count != 0 ? lstrev.LastOrDefault() : 0;

            }


        }
        public async Task<int> IterateRecs()
        {
            try
            {
                await Task.Delay(100);
                pgsbar.Visible = true;
                Core it = new Core();
                listsubmitee.Items.Clear();
                using (var dbx = new DropboxClient(it._key))
                {
                    var sharedLink = new SharedLink("derm");
                    var sharedFiles = await dbx.Files.ListFolderAsync("", true);

                    foreach (var file in sharedFiles.Entries.Reverse())
                    {
                        ListBoxItem st = new ListBoxItem();
                        if (file.PathDisplay.EndsWith(".derm") && file.Name != "derm")
                        {

                            PSubmiter(st, file.Name.Replace(".derm", ""));
                            st.Text += " (" + await IterateReviewsCount(file.PathDisplay.Replace(".derm", "").Replace("/derm/", "")) + " Reviewers)";
                            lstrev.Clear();

                        }


                    }

                }
                pgsbar.Visible = false;
                return 0;
            }
            catch (System.Net.Http.HttpRequestException) { pgsbar.Visible = false; MessageBox.Show("No Internet!"); return 0; }
        }
        public static string Capitalize(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("NULL STRING!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        public async Task<int> IterateRevs(string fn)
        {
            await Task.Delay(100);
            progressBarX1.Visible = true;
            Core it = new Core();
            //clear reviewer names
            reviewerslist.Items.Clear();
            //clear review link list
            Srevs.Clear();
            int j = 0;
            using (var dbx = new DropboxClient(it._key))
            {
                var sharedLink = new SharedLink("derm");
                var sharedFiles = await dbx.Files.ListFolderAsync("", true);

                foreach (var file in sharedFiles.Entries)
                {

                    ListBoxItem st = new ListBoxItem();
                    if (file.PathDisplay.EndsWith(".rev") && file.Name != "derm" && file.Name.Contains(GameFN))
                    {
                        j++;
                        Core rp = new Core();
                        Reviewer nfo = new Reviewer();
                        ReplayParser dr = new ReplayParser();
                        dr = ReplayParser.FromJsonText(await rp.DownloadSTRING(file.Name));

                        //Skip empty entries
                        if (dr == null)
                            continue;
                        //Or parse reviewers
                        st.Text = Capitalize(dr.Nickname);
                        if (dr.isVerified == true)
                            st.Symbol = "\uf00c";
                        else
                            st.Symbol = "\uf007";

                        reviewerslist.Items.Add(st);
                        Srevs.Add(file.Name);
                        //Counting reviews per recorded game
                        //reviewerslist.Items.Add(await IterateReviewsCount(file.PathDisplay.Replace(".rev", "").Replace("/derm/", "")));


                    }


                }

            }
            if (j == 0)
                reviewerslist.Items.Add("No Reviews.");

            progressBarX1.Visible = false;
            return 0;
        }
        private async void sideNavItem8_Click(object sender, EventArgs e)
        {
            sideNavItem8.Enabled = false;
            Qdesc.SelectionStart = Qdesc.Text.Length;
            await IterateRecs();
            sideNavItem8.Enabled = true;
        }

        private void subreview_Click(object sender, EventArgs e)
        {
            Submission subf = new Submission();
            subf.RecInstance = SaveGame + @"\" + repLIST.SelectedItem.ToString();
            subf.Show();
        }

        private void SendREV_Click(object sender, EventArgs e)
        {
            if (listsubmitee.SelectedItems.Count == 1)
            {
                ReplayView rv = new ReplayView();
                rv.JsonFile = listsubmitee.SelectedItem.ToString().Split(new string[] { " (" }, StringSplitOptions.None).FirstOrDefault() + ".json";
                rv.Show();
            }
        }

        private async void listsubmitee_ItemClick(object sender, EventArgs e)
        {
            try
            {
                //Replays Info
                Rprog.Visible = true;
                listsubmitee.Enabled = false;
                Core rp = new Core();
                ReplayParser dr = new ReplayParser();
                dr = ReplayParser.FromJsonText(await rp.DownloadSTRING(listsubmitee.SelectedItem.ToString().Split(new string[] { " (" }, StringSplitOptions.None).FirstOrDefault() + ".json"));
                Qdesc.Text = dr.Description;
                Qtitle.Text = dr.Title;
                if ((bool)dr.TicketKey)
                {
                    vlblb.Visible = true;
                    pictureBox4.Visible = true;
                    ticketlbl.Visible = true;
                }
                else
                {
                    vlblb.Visible = false;
                    pictureBox4.Visible = false;
                    ticketlbl.Visible = false;
                }
                GameFN = Path.GetFileNameWithoutExtension(dr.Recname);
                Rprog.Visible = false;

                //Reviews
                progressBarX1.Enabled = true;
                await IterateRevs(GameFN);
                listsubmitee.Enabled = true;
                progressBarX1.Enabled = false;
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("No Internet!");
            }
            catch (SystemException)
            {
                listsubmitee.Enabled = true;
                Rprog.Visible = false;
            }




        }

        private void reviewerslist_ItemClick(object sender, EventArgs e)
        {
            reviewerslist.Enabled = false;
            //skip defaul value
            if (reviewerslist.SelectedItem.ToString() == "No Reviews.")
                return;
            //select reviewers
            if (reviewerslist.SelectedIndex != -1)
            {

                //Grab review url
                string rlnk = Srevs[reviewerslist.SelectedIndex];
                //create new form to parse reviewer
                ReviewSingleton rs = new ReviewSingleton();
                rs.SetURL = rlnk;
                rs.SetAUTH = reviewerslist.SelectedItem.ToString();
                rs.Show();
            }
            reviewerslist.Enabled = true;

        }

        async Task<int> GrabDef()
        {
            await Task.Delay(100);

            //parse ids
            foreach (var s in USERprofiles)
            {
                defSAVE.Items.Add(s.Key);
            }


            return 0;
        }
        private void defSAVE_SelectedIndexChanged(object sender, EventArgs e)
        {
            repLIST.Items.Clear();
            RefreshSaves(SaveGame.Replace(@"\savegame", ""));
            foreach (var f in USERprofiles)
            {
                if (f.Key == defSAVE.Text)
                {
                    AddREG(defSAVE.Text + "|>" + f.Value);
                    break;
                }
            }
            string[] sy = GetREG().Split(new string[] { "|>" }, StringSplitOptions.None);
            listPROFILE.Text = sy[0];
            if (defSAVE.Focused)
                MessageBox.Show("Default SaveGame is: " + sy[1], "Success!");

        }
        public static bool checkExistance()
        {
            using (RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DERM", true))
                return (winLogonKey.GetValueNames().Contains("DERM"));
        }
        private void AddREG(string val, string path = @"SOFTWARE\DERM\SAVEGAME", string field = "SV")
        {

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(path))
            {
                //key.CreateSubKey("SAVEGAME");
                key.SetValue(field, val, RegistryValueKind.String);
            }
        }

        private string GetREG(string path = @"SOFTWARE\DERM\SAVEGAME", string val = "SV")
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(path))
            {
                if (key != null)
                    return key.GetValue(val).ToString();
                else
                {
                    foreach (var f in USERprofiles)
                    {

                        AddREG(defSAVE.Text + "|>" + f.Value);
                        defSAVE.Text = f.Key;
                        break;

                    }
                    return null;
                }
            }
        }


        private void kryptonDataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkSG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //AccessLink()
        }

        private void playerNAMES_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_SizeChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_SizeChanged(object sender, EventArgs e)
        {
            //playerNAMES.Width = flowLayoutPanel3.Width;
        }

        private void defSHOW_Click(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DERM\SAVEGAME"))
            {
                if (key != null)
                    AccessLink(key.GetValue("SV").ToString().Split(new string[] { "|>" }, StringSplitOptions.None).ElementAt(1) + "\\savegame");
                else
                {

                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AccessLink(@"https://www.aoebuilds.com/");
        }
        private async void ExtractRen(string myzip, bool ren = false)
        {
            string recname = "";
            using (ZipArchive archive = ZipFile.OpenRead(myzip))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    recname = Path.Combine(Path.GetTempPath(), entry.FullName);

                    entry.ExtractToFile(Path.Combine(Path.GetTempPath(), entry.FullName), true);
                    //Prompt Replay Renamer
                    if (ren == true)
                    {
                        string oldrec = recname;
                        string input = Path.GetFileNameWithoutExtension(recname);
                        ShowInputDialog(ref input, oldrec, false);
                    }
                    else
                    {
                        string oldrec = recname;
                        string input = Path.GetFileNameWithoutExtension(recname);
                        await ProcessZIP(input, oldrec);


                    }
                }
            }


        }
        private void repLIST_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);

            //if zip
            if (data[0].EndsWith(".zip"))
            {
                DialogResult dialogResult = MessageBox.Show("Extract & Rename Replays At The Same Time?", "Extract & Rename?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    ExtractRen(data[0], true);
                else
                {
                    ExtractRen(data[0], false);
                }

            }
            else if (data[0].EndsWith(".aoe2record"))
            {
                if (data.Length == 1)
                {
                    string input = Path.GetFileNameWithoutExtension(data[0]);
                    ShowInputDialog(ref input, data[0], false);
                }
                else if (data.Length > 1)
                {
                    foreach (string s in data)
                    {
                        string input = Path.GetFileNameWithoutExtension(s);
                        ShowInputDialog(ref input, s, false);
                    }
                }

            }
            else
            {
                MessageBox.Show(Path.GetExtension(e.Data.GetData(DataFormats.Text).ToString()) + " is not a recorded game or archive filetype! Please drag and drop .zip or .aoe2record", "Wrong File!");
            }



        }

        private void repLIST_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        public sealed class KeyboardHook : IDisposable
        {
            // Registers a hot key with Windows.
            [DllImport("user32.dll")]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
            // Unregisters the hot key with Windows.
            [DllImport("user32.dll")]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            /// <summary>
            /// Represents the window that is used internally to get the messages.
            /// </summary>
            private class Window : NativeWindow, IDisposable
            {
                private static int WM_HOTKEY = 0x0312;

                public Window()
                {
                    // create the handle for the window.
                    this.CreateHandle(new CreateParams());
                }

                /// <summary>
                /// Overridden to get the notifications.
                /// </summary>
                /// <param name="m"></param>
                protected override void WndProc(ref Message m)
                {
                    base.WndProc(ref m);

                    // check if we got a hot key pressed.
                    if (m.Msg == WM_HOTKEY)
                    {
                        // get the keys.
                        Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                        ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                        // invoke the event to notify the parent.
                        if (KeyPressed != null)
                            KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                    }
                }

                public event EventHandler<KeyPressedEventArgs> KeyPressed;

                #region IDisposable Members

                public void Dispose()
                {
                    this.DestroyHandle();
                }

                #endregion
            }

            private Window _window = new Window();
            private int _currentId;

            public KeyboardHook()
            {
                // register the event of the inner native window.
                _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
                {
                    if (KeyPressed != null)
                        KeyPressed(this, args);
                };
            }

            /// <summary>
            /// Registers a hot key in the system.
            /// </summary>
            /// <param name="modifier">The modifiers that are associated with the hot key.</param>
            /// <param name="key">The key itself that is associated with the hot key.</param>
            public void RegisterHotKey(Keys key)
            {
                // increment the counter.
                _currentId = _currentId + 1;

                // register the hot key.
                if (!RegisterHotKey(_window.Handle, _currentId, (uint)0, (uint)key))
                    throw new InvalidOperationException("Couldn’t register the hot key.");
            }

            /// <summary>
            /// A hot key has been pressed.
            /// </summary>
            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose()
            {
                // unregister all the registered hot keys.
                for (int i = _currentId; i > 0; i--)
                {
                    UnregisterHotKey(_window.Handle, i);
                }

                // dispose the inner native window.
                _window.Dispose();
            }

            #endregion
        }

        /// <summary>
        /// Event Args for the event that is fired after the hot key has been pressed.
        /// </summary>
        public class KeyPressedEventArgs : EventArgs
        {
            private ModifierKeys _modifier;
            private Keys _key;

            internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
            {
                _modifier = modifier;
                _key = key;
            }

            public ModifierKeys Modifier
            {
                get { return _modifier; }
            }

            public Keys Key
            {
                get { return _key; }
            }
        }

        /// <summary>
        /// The enumeration of possible modifiers.
        /// </summary>
        [Flags]
        public enum ModifierKeys : uint
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        private void gameid_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlayerELOPars();
        }

        private void importFLS_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();
            saveFileDialog1.Filter = "Recorded Games (*.aoe2record, *.zip)|*.aoe2record;*.zip";
            saveFileDialog1.Title = "Import Recorded Games";
            saveFileDialog1.ShowDialog();
            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                if (saveFileDialog1.FileName.EndsWith(".zip"))
                {
                    DialogResult dialogResult = MessageBox.Show("Extract & Rename Replays At The Same Time?", "Extract & Rename?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                        ExtractRen(saveFileDialog1.FileName, true);
                    else
                    {
                        ExtractRen(saveFileDialog1.FileName, false);
                    }

                }
                else if (saveFileDialog1.FileName.EndsWith(".aoe2record"))
                {
                    if (saveFileDialog1.FileNames.Length == 1)
                    {
                        string input = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                        File.Copy(saveFileDialog1.FileName, SaveGame + @"\" + input + ".aoe2record");
                        repLIST.Items.Clear();
                        RefreshSaves(SaveGame.Replace(@"\savegame", ""));
                        int index = repLIST.FindString(input + ".aoe2record");
                        // Determine if a valid index is returned. Select the item if it is valid.
                        if (index != -1)
                            repLIST.SetSelected(index, true);
                    }
                    else if (saveFileDialog1.FileNames.Length > 1)
                    {

                        foreach (string s in saveFileDialog1.FileNames)
                        {
                            string input = Path.GetFileNameWithoutExtension(s);
                            File.Copy(s, SaveGame + @"\" + input + ".aoe2record");
                        }
                        repLIST.Items.Clear();
                        RefreshSaves(SaveGame.Replace(@"\savegame", ""));
                        int index = repLIST.FindString(saveFileDialog1.FileNames[0] + ".aoe2record");
                        // Determine if a valid index is returned. Select the item if it is valid.
                        if (index != -1)
                            repLIST.SetSelected(index, true);
                    }

                }
                else
                {
                    MessageBox.Show(Path.GetExtension(saveFileDialog1.FileName) + " is not a recorded game or archive filetype! Please select .aoe2record files or a .zip File(s) ", "Wrong File!");
                }
            }

        }

        private void addDEF_ValueChanged(object sender, EventArgs e)
        {
            if (addDEF.ValueObject.ToString() == "Y" && addDEF.Focus())
            {
                if (!RegCalls.checkExistance())
                {
                    if (RegCalls.GetREG(@"SOFTWARE\DERM", "WinDEF") != "1")
                    {

                        procCMD("powershell -inputformat none -outputformat none -NonInteractive -Command " + "\"Add-MpPreference -ExclusionPath \'" + AppDomain.CurrentDomain.BaseDirectory + "\'\"");
                        AddREG("1", @"SOFTWARE\DERM", "WinDEF");
                        MessageBox.Show(this, "DERM successfully added To Windows Defender So The Downgrade Tool is not interrupted.", "Done!");
                        this.TopMost = true;
                        this.TopMost = false;
                    }

                }
            }
            else if (addDEF.ValueObject.ToString() == "N" && addDEF.Focus())
            {

                procCMD("powershell -Command \"Remove-MpPreference -ExclusionPath \'" + AppDomain.CurrentDomain.BaseDirectory + "\'\"");
                AddREG("0", @"SOFTWARE\DERM", "WinDEF");
                MessageBox.Show(this, "DERM removed From Windows Defender Exception List. If You encounter download chunk errors, enable this back to fix the problem.", "Done!");
                this.TopMost = true;
                this.TopMost = false;
            }
        }
        //END GLOBAL HOTKEYS
    }
}