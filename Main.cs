using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using De_Roll;
using DevComponents.DotNetBar;
using ComponentFactory.Krypton.Toolkit;
using System.Net;
using System.Threading.Tasks;
using DEBoard;
using System.Reflection;

namespace DEReplaysManager
{
    public partial class Form1 : OfficeForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<string> DIRprofiles = new List<string>();
        Dictionary<string, string> USERprofiles = new Dictionary<string, string>();
        //public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        private string filterText = String.Empty;
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
         
        public void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length) + @".db";

                using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(decompressionStream))
                    {
                        string player = reader.ReadToEnd();
                        
                        File.WriteAllText(newFileName, player);
                        DEparser stp = new DEparser();
                        stp.GetPlayernfp(newFileName);
                        




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
        private void ScanDirectories()
        {
            DEparser dp = new DEparser();
            string[] subdirectoryEntries = Directory.GetDirectories(dp.dePATH);


            foreach (string subdirectory in subdirectoryEntries)
            {

                if (dp.IsDigitsOnly(subdirectory.Replace(dp.dePATH + "\\", "")) && subdirectory.Length > 4 && subdirectory.Replace(dp.dePATH + "\\", "") != "0")
                {
                    DIRprofiles.Add(subdirectory);
                    FileInfo fl = new FileInfo(subdirectory + @"\profile\Player.nfp");
                    Decompress(fl);

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
            else if (fDate >= p15)
                return @"25 August 2020 (40220)";
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

        private void Form1_Load(object sender, EventArgs e)
        {
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
                ShowInputDialog(ref input, oldrec);
                repLIST.Items.Clear();
                RefreshSaves(SaveGame.Replace(@"\savegame", ""));
            }
        }
        private DialogResult ShowInputDialog(ref string input, string oldNameFullPath)
        {
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
            textBox.Text = input;
            textBox.Select(0, 0);

            textBox.Font = new Font(textBox.Font.FontFamily, 12);
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            okButton.Click += (s, e) => {
                if (textBox.Text.Contains(".aoe2record"))
                    textBox.Text = textBox.Text.Replace(".aoe2record", "");

                if (File.Exists(SaveGame + @"\" + textBox.Text + ".aoe2record") && !textBox.Text.Contains("AgeIIDE_Replay"))
                {
                    MessageBox.Show("Replay Name Already Exists! Use different name");
                }
                else
                {
                    try
                    {
                        File.Move(oldNameFullPath, SaveGame + @"\" + textBox.Text + ".aoe2record");
                    }
                    catch (SystemException ez)
                    {
                        MessageBox.Show(ez.ToString());
                    }
                    repLIST.Items.Clear();
                    RefreshSaves(SaveGame.Replace(@"\savegame", ""));
                    int index = repLIST.FindString(textBox.Text + ".aoe2record");
                    // Determine if a valid index is returned. Select the item if it is valid.
                    if (index != -1)
                        repLIST.SetSelected(index, true);
                }

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
            input = textBox.Text;
            return result;
        }
        private void deleteREC_Click(object sender, EventArgs e)
        {
            if (repLIST.SelectedIndex >= 0)
            {
                string recname = SaveGame + @"\" + repLIST.SelectedItem.ToString();
                DialogResult dialogResult = MessageBox.Show("Delete " + repLIST.SelectedItem.ToString() + @"?", "Confirm Replay Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(recname);
                        repLIST.Items.Clear();
                        RefreshSaves(SaveGame.Replace(@"\savegame", ""));
                    }
                    catch (SystemException ez)
                    {
                        MessageBox.Show(ez.ToString());
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
                foreach (string str in recp.playerDB)
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
                
                WebClient webClient = new WebClient();
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
            { KryptonMessageBox.Show("Your internet dropped! Restart AOE2Tools and try again!", "Connection Lost"); 
            }
        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;

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
            try{File.Delete(RepZip);}catch(SystemException){}
            //Prompt Replay Renamer
            string oldrec = recname;
            string input = recname.Replace(".aoe2record", "").Replace(SaveGame + @"\","") ;
            ShowInputDialog(ref input, oldrec);
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
            RefreshSaves(SaveGame.Replace(@"\savegame", ""));
        }
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            string output = null;
            bool cancelledOrError = false;
            using (var client = new WebClient())
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
        private async void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            kryptonDataGridView3.Rows.Clear();
            await Task.Delay(500);

            if (filterText != kryptonTextBox2.Text)
            {
                filterText = kryptonTextBox2.Text;
                Uri apiLOB = new Uri(@"https://aoe2.net/api/leaderboard?game=aoe2de&leaderboard_id=3&start=1&search=" + kryptonTextBox2.Text);
                string jsonLOBBIES = await DownloadStringAsync(apiLOB);
               
                var ldb = QueryPlayer.FromJson(jsonLOBBIES);

                int i = 1;
                foreach (var l in ldb.Leaderboard)
                {
                    i++;
                    LinkLabel ln = new LinkLabel();
                    Button btn = new Button();
                    ln.Text = "View";
                    ln.Name = "view" + i.ToString();
                    btn.Name = "btn" + i.ToString();
                    btn.Text = "Join";
                    kryptonDataGridView3.Rows.Add(l.Rank, l.Name, l.Rating, l.Games, "View", "View", @"https://www.ageofempires.com/stats/?profileId=" + l.ProfileId + @"&game=age2", @"http://steamcommunity.com/profiles/" + l.SteamId);
                }
                RemoveDuplicate(kryptonDataGridView3);
            }
            if (kryptonTextBox2.Text == null)
            {
                kryptonDataGridView3.Rows.Clear();
            }
        }

        private void kryptonDataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[6].Value.ToString());

                }
                if (e.ColumnIndex == 5)//if ur link columnIndex (complain_no ColumnIndex) is zero 
                {
                    Process.Start(kryptonDataGridView3.SelectedCells[7].Value.ToString());

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
            DERMChecker();
        }
        void DERMChecker()
        {
            try
            {
                //Get latest version code
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "version.txt"))
                {
                    string version = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "version.txt");

                    WebClient wk = new WebClient();
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
            catch(System.Net.WebException)
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
                        foreach(string sd in refvst)
                        {
                            if(sd.Contains(id.ToString()))
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
    }
}