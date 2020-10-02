using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using De_Roll;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Dropbox.Api;

namespace DeReplaysManager
{
    public partial class ImportREC : OfficeForm
    {
        public List<string> DIRprofiles = new List<string>();
        Dictionary<string, string> USERprofiles = new Dictionary<string, string>();
        //public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        private string filterText = String.Empty;
        public string SaveGame;
        public string GetReplayLink { get; set; }
        
        public ImportREC()
        {
            InitializeComponent();
        }
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
        private void ScanDirectories()
        {
            DEparser dp = new DEparser();
            //string[] subdirectoryEntries = Directory.GetDirectories(dp.dePATH);
            var subdirectoryEntries = new DirectoryInfo(dp.dePATH).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime);
            bool isONCE = false;

            foreach (DirectoryInfo subdirectory in subdirectoryEntries)
            {

                if (dp.IsDigitsOnly(subdirectory.FullName.Replace(dp.dePATH + "\\", "")) && subdirectory.FullName.Length > 4 && subdirectory.FullName.Replace(dp.dePATH + "\\", "") != "0")
                {
                    DIRprofiles.Add(subdirectory.FullName);
                    DirectoryInfo info = new DirectoryInfo(subdirectory.FullName + "\\profile");
                    FileInfo[] files = info.GetFiles("*.hki").OrderByDescending(p => p.LastWriteTime).ToArray();
                    foreach (FileInfo file in files)
                    {
                        if (listPROFILE.Items.Contains(file.Name.Replace(".hki", "")))
                        {
                            listPROFILE.Items.Add(file.Name.Replace(".hki", "") + @"(2)");
                            USERprofiles.Add(file.Name.Replace(".hki", "") + @"(2)", subdirectory.FullName);
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

        }

        private void ImportREC_Load(object sender, EventArgs e)
        {
            ScanDirectories();
        }

        private void repLIST_SelectedIndexChanged(object sender, EventArgs e)
        {

           
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
        public async Task<int> DownloadIprog(string fullpathfile, string localPath)
        {
            Core core = new Core();
            ServicePointManager.DefaultConnectionLimit = 1000;
            var dbxz = new DropboxClient(core._key);
            var responsez = await dbxz.Files.DownloadAsync(fullpathfile);
            //ulong fileSizez = responsez.Response.Size;
            ulong fileSizez = responsez.Response.Size;

            const int bufferSize = 1024 * 1024;
            var buffer = new byte[bufferSize];
            string folderNamez = localPath;
            using (var stream = await responsez.GetContentAsStreamAsync())
            {
                using (var localfilez = new FileStream(folderNamez, FileMode.OpenOrCreate))
                {
                    var lengthz = stream.Read(buffer, 0, bufferSize);
                    
                        while (lengthz > 0)
                        {

                            localfilez.Write(buffer, 0, lengthz);
                            // Console.WriteLine(localfile.);
                            var percentage = 100 * (ulong)localfilez.Length / fileSizez;
                            // Update progress bar with the percentage.
                            progBAR.Value = (int)percentage;
                            lengthz = stream.Read(buffer, 0, bufferSize);
                        }
                    


                }

            }

            return 1;

        }
        private async void importrecord_Click(object sender, EventArgs e)
        {
            importrecord.Enabled = false;
           
            if (USERprofiles.TryGetValue(listPROFILE.Text, out string savepath))
            {
                Core core = new Core();
                if (!Core.CheckForInternetConnection())
                {
                    MessageBox.Show("No internet!", "Offline!");
                    importrecord.Enabled = true;
                    return;
                }
                await DownloadIprog("/derm/" + GetReplayLink, System.IO.Path.GetTempPath() + GetReplayLink);
                //Decompress
                Core.DecompressFileLZMA(System.IO.Path.GetTempPath() + GetReplayLink, savepath + @"\savegame\" + GetReplayLink.Replace(".derm",".aoe2record"));
                
                //Done
                MessageBox.Show("Replay Successfully imported to your savegame path: \n" + savepath + @"\savegame\" + GetReplayLink.Replace(".derm", ".aoe2record") + "\nClick Ok To close this window.");
                this.Close();
            }
        }

        private void ImportREC_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete(System.IO.Path.GetTempPath() + GetReplayLink);
        }
    }
}
