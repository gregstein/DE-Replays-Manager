using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.VisualBasic.FileIO;
using Steamworks;
using DevComponents.DotNetBar;
using DeReplaysManager;
using DevComponents.DotNetBar.Rendering;

namespace De_Roll
{
    public partial class Form1 : OfficeForm
    {
        public string SetPatchVer { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        public int _currentDepots;
        public string depo;
        public string manif;
        public List<string> AllDepots = new List<string>();
        public List<string> AllManif = new List<string>();
        public bool Tdone = false;
        private string saveDirectoryPath = "";
        public string exeDIR = System.AppDomain.CurrentDomain.BaseDirectory;
        public bool archOS = Environment.Is64BitOperatingSystem;
        public static bool IsConnected()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            if(IsConnected())
            {

                string stid = await DownloadStringAsync(new Uri(@"https://github.com/gregstein/DE-Replays-Manager/raw/master/depotpatches.txt"));
                if(stid != null)
                {

                
                cbPATCH.Items.Clear();
                cbPATCH.Items.Add("Select Patch Version");
                using (StringReader sr = new StringReader(stid))
                {
                    while (sr.Peek() >= 0)
                    {
                        string str;
                        //string[] strArray;
                        str = sr.ReadLine();
                        string[] arrstr = str.Split('>');
                        cbPATCH.Items.Add(arrstr[0]);
                    }
                }
                cbPATCH.Text = "Select Patch Version";
                this.TopMost = true;
                }


            }
            if (SetPatchVer != null)
                cbPATCH.Text = SetPatchVer;

            string result = await CheckDotNet();
            if(!result.Contains("Usage"))
            {
                DialogResult dialogResult = MessageBox.Show(new Form { TopMost = true }, "DotNet Runtime is missing. Download it now?", "Missing Component!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (archOS)
                        Process.Start("https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x64-installer");
                    else
                        Process.Start("https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x86-installer");
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    
                }
            }
        }
        
         async Task<string> CheckDotNet()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.EXE";
            p.StartInfo.Arguments = "/C dotnet && exit";
            p.Start();
            
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            await Task.Delay(100);
            return output;
        }
        private  void btnINSTALL_Click(object sender, EventArgs e)
        {
            
        }

        private void userBOX_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonHeader1_Paint(object sender, PaintEventArgs e)
        {

        }

        private  void kryptonButton1_Click(object sender, EventArgs e)
        {
            kryptonButton1.Enabled = false;
            if (cbPATCH.Text != "Select Patch Version")
            DepotCommander(cbPATCH.Text);
            VerCheck();
            kryptonButton1.Enabled = true;

        }
        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //* Do your stuff with the output (write to console/log/StringBuilder)
            MessageBox.Show(outLine.Data);
        }
        private void cmdDEPOT(string depot, string manifest)
        {
            try
            {

            
            string argz = "/K dotnet \"" + System.AppDomain.CurrentDomain.BaseDirectory + "\\DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir " + "\"" + SaveDirectoryPath() + "\"" + " -username " + userBOX.Text + " -password " + "\"" + passBOX.Text + "\"" + " -remember-password -filelist " + "\"" + System.AppDomain.CurrentDomain.BaseDirectory + "exclude.txt" + "\"" + " && exit";
            //MessageBox.Show(argz);

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "CMD.EXE";
            psi.Verb = "runas";
            psi.Arguments = argz;
            p.StartInfo = psi;
            p.Start();

            p.WaitForExit();
            }
            catch (SystemException)
            {

            }

            //Copier(depot);

        }
        static void ErrorOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //* Do your stuff with the output (write to console/log/StringBuilder)
            MessageBox.Show(outLine.Data);
        }
        private void cmdDEPOTS(string depot, string manifest)
        {
            if(SaveDirectoryPath() != null)
            {
                string argz = "/K dotnet \"" + System.AppDomain.CurrentDomain.BaseDirectory + "\\DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir " + "\"" + SaveDirectoryPath() + "\"" + " -username " + userBOX.Text + " -password " + "\"" + passBOX.Text + "\"" + " -remember-password -filelist " + "\"" + System.AppDomain.CurrentDomain.BaseDirectory + "exclude.txt" + "\"" + " && exit";
                MessageBox.Show(argz);

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "CMD.EXE";
            psi.Verb = "runas";
            psi.Arguments = argz;
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
            }
            else
            {
                MessageBox.Show("Game directory for AoE2 DE was not found! Please connect to your steam account or download the game.", "Opps!");
            }
            //Copier(depot);

        }
        private static int LinePicker(string ver, string mydir)
        {
            int i = 0;
            foreach (string line in File.ReadLines(mydir + "\\depatches.txt"))
            {
                i++;
                if(line.Contains(ver))
                {
                    return i;
                }
            }
            return 0;
        }
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            try { 
            string output = null;
            bool cancelledOrError = false;
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "DE Replays Manager");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = false; ServicePointManager.MaxServicePointIdleTime = 0;
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
            catch (SystemException)
            {

                return await Task.FromResult(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "depatches.txt") ? File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "depatches.txt"):"");
            }
        }
        private async void DepotCommander(string version)
        {
            try
            {

            
            AllDepots.Clear();
            AllManif.Clear();
            int i = 0;
            string line;
            
            string Releases = await DownloadStringAsync(new Uri(@"https://github.com/gregstein/DE-Replays-Manager/raw/master/depotpatches.txt"));
                File.WriteAllText(exeDIR + "\\depatches.txt", Releases);

                StreamReader file = new StreamReader(exeDIR + "\\depatches.txt");
                while ((line = file.ReadLine()) != null)
                {
                   
                
            //    foreach (string line in File.ReadLines(exeDIR + "\\depatches.txt"))
            //{
                i++;
                if(i == LinePicker(version, exeDIR) && i != 0)
                {

                
                string[] spline = line.Split('>');
                string[] subline = spline[1].Split('#');
                _currentDepots = subline.Length;
                    
                    int j = 0;
                foreach(string id in subline)
                {
                    j++;
                    //Depot command
                    if(j>=1)
                    {
                        string[] fsplit = id.Split('+');
                        
                        AllDepots.Add(fsplit[0]);
                        depo = fsplit[0];
                        string[] ffsplit = fsplit[1].Split('<');
                        
                        AllManif.Add(ffsplit[0]);
                        manif = ffsplit[0];
                    }
                        
                        

                }
                        MessageBox.Show(AllDepots.Count.ToString() + " Depots To Download.");
                //Depot Counter
                if (AllDepots.Count == 1)
                    {
                        cmdDEPOT(depo, manif);
                        Tdone = true;
                        VerCheck();
                            
                        }
                    

                else if(AllDepots.Count == 2)
                {
                    cmdDEPOT(AllDepots[0], AllManif[0]);
                    cmdDEPOT(AllDepots[1], AllManif[1]);
                    Tdone = true;
                    VerCheck();
                        }
                else if (AllDepots.Count == 3)
                {
                            cmdDEPOT(AllDepots[0], AllManif[0]);
                            cmdDEPOT(AllDepots[1], AllManif[1]);
                            cmdDEPOT(AllDepots[2], AllManif[2]);
                            Tdone = true;
                            VerCheck();

                        }
                else if (AllDepots.Count == 4)
                {
                            cmdDEPOT(AllDepots[0], AllManif[0]);
                            cmdDEPOT(AllDepots[1], AllManif[1]);
                            cmdDEPOT(AllDepots[2], AllManif[2]);
                            cmdDEPOT(AllDepots[3], AllManif[3]);
                            Tdone = true;
                            VerCheck();
                        }



                    //TESTING
                }
                //break;
                //END LINES
            }
            file.Close();

        }
            catch (System.IndexOutOfRangeException)
            {
                MessageBox.Show("Filed to retrieve Patch notes.", "Error!");
            }
        }

        private void VerCheck()
        {
            //Re-check game version
            var versionInfo = FileVersionInfo.GetVersionInfo(SaveDirectoryPath() + @"\AoE2DE_s.exe");
            string verzion = versionInfo.FileVersion;
            string[] sver = verzion.Split('.');
            label1.Text = "Current Version: " + sver[2];
        }

        private void Copier(string depot)
        {
            try
            {
                //Source from where we copy the file
                string sourceFolderPath = System.AppDomain.CurrentDomain.BaseDirectory + @"tmp";
                // Destination for copied files.
                string destinationFolderPath = @"C:\test";
                FileSystem.CopyDirectory(sourceFolderPath, destinationFolderPath,UIOption.AllDialogs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string SaveDirectoryPath()
        {
            if (this.saveDirectoryPath == "")
            {
                uint size = SteamApps.GetAppInstallDir((AppId_t)813780, out this.saveDirectoryPath, 500u);

                if (size <= 0)
                {

                }
                else
                {

                }
            }

            return this.saveDirectoryPath;
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!SteamAPI.Init())
                {
                    if (SteamAPI.IsSteamRunning())
                    {
                        steamLBL.ForeColor = Color.DarkBlue;
                        steamLBL.Text = "Steam Running..";
                        steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotred;
                    }
                    else
                    {
                        steamLBL.ForeColor = Color.Gold;
                        await Task.Delay(200);
                        steamLBL.Text = "Steam OFF";
                        await Task.Delay(100);
                        steamLBL.ForeColor = Color.Maroon;
                        steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotred;
                    }

                    //SteamUtils.GetAppID();

                }
                else
                {

                    steamLBL.ForeColor = Color.ForestGreen;
                    steamLBL.Text = "Steam ON";
                    steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotgreen;
                    cbPATCH.Enabled = true;
                    kryptonButton1.Enabled = true;
                    await Task.Delay(200);

                    bool IsDE = SteamApps.BIsAppInstalled(new AppId_t(813780));
                    if (!IsDE)
                    {
                        MessageBox.Show("Please Buy or Install Age of Empires II: Definitive Edition before you proceed!", "Game Missing");
                        cbPATCH.Enabled = false;
                        timer1.Stop();
                    }

                    var versionInfo = FileVersionInfo.GetVersionInfo(SaveDirectoryPath() + @"\AoE2DE_s.exe");
                    string version = versionInfo.FileVersion;
                    string[] sver = version.Split('.');
                    label1.Text = "Current Version: " + sver[2];
                    timer1.Stop();
                }
            }
            catch(SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
                steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotred;

            }
                
            }

        private void cbPATCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cbPATCH_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void browseREP_Click(object sender, EventArgs e)
        {
           
        }

        private void settingbtn_Click(object sender, EventArgs e)
        {
            DTSettings dt = new DTSettings();
            dt.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
    }

