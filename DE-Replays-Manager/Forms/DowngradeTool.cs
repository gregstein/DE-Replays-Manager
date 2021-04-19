using DeReplaysManager;
using DevComponents.DotNetBar;
using Microsoft.VisualBasic.FileIO;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public string GameDirInternal
        {
            get { return saveDirectoryPath; }
            set
            {
                saveDirectoryPath = value;
            }
        }
        public string exeDIR = System.AppDomain.CurrentDomain.BaseDirectory;
        public bool archOS = Environment.Is64BitOperatingSystem;
        public string argz = "";

        public bool _isvalidated;
        public bool[] _pass;
        private bool _curStatus;
        private string _currentDepot;
        private bool _strCLEAN;
        private bool _guardVALID;
        private bool _Stoprog;
        private bool _InterENDED;
        private string _FailedDepot;
        public bool _connected;

        public async Task<bool> IsConnected()
        {

            try
            {
                WebClient webClient = new WebClient();
                webClient.OpenReadCompleted += (sender, e) =>
                {
                    if (e.Result != null)
                    {
                        _connected = true;
                    }

                };
                await webClient.OpenReadTaskAsync(new Uri("http://google.com/generate_204", UriKind.Absolute));

                return _connected;

            }
            catch (Exception)
            {
                return _connected;
            }

            //try
            //{
            //    using (var client = new WebClient())
            //    using (client.OpenReadAsync("http://google.com/generate_204"))
            //        return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }



        async Task<string> CheckDotNet()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "CMD.EXE";
            p.StartInfo.Arguments = "/C dotnet && exit";
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Close();
            await Task.Delay(100);
            return output;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            kryptonButton1.Enabled = false;
            if (cbPATCH.Text != "Select Patch Version")
                DepotCommander(cbPATCH.Text);
            VerCheck();


        }
        private static float _getPROG(string line)
        {
            if (line == null)
                return 0;

            var pattern = @"[+-]?([0-9]*[.])[0-9]+";
            var matches = Regex.Matches(line, pattern);
            float result;
            if (matches.Count > 0 && matches[0].Groups[0].Value != null)
            {
                if (float.TryParse(matches[0].Groups[0].Value, out result))
                {
                    return float.Parse(matches[0].Groups[0].Value);
                }

            }
            return 0;
        }
        private void AppendTextInBox(RichTextBox box, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action<RichTextBox, string>)AppendTextInBox, cmdSCREEN, text);
            }
            else
            {
                //progCMD.Value = (int)_getPROG(text);
                box.Text += text;
            }
        }
        private void AppendTextProgress(DevComponents.DotNetBar.Controls.ProgressBarX pb, string text)
        {
            if (this.InvokeRequired)
            {

                this.Invoke((Action<DevComponents.DotNetBar.Controls.ProgressBarX, string>)AppendTextProgress, pb, text);

            }
            else
            {
                if (_getPROG(text) != 0)
                {
                    progCMD.Value = (int)_getPROG(text);
                    pb.Text = "[" + _currentDepot + @"\" + _currentDepots + " Depots] (" + _getPROG(text).ToString() + "%)";
                }

            }
        }



        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.TopMost = true;
            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Steam Guard Code!";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
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
        async Task<bool> OUTproc(string argz, Process pr)
        {

            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.FileName = @"cmd.exe";
            pr.StartInfo.Arguments = argz;
            pr.StartInfo.RedirectStandardInput = true;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.RedirectStandardError = true;
            pr.StartInfo.CreateNoWindow = true;
            pr.EnableRaisingEvents = true;
            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //pr.OutputDataReceived += new DataReceivedEventHandler(InterProcOutputHandler);
            //pr.Exited += new EventHandler(OnCompleted);
            pr.OutputDataReceived += (sender, e) =>
            {
                //Check if guard bool is true before anything else
                if (!_guardVALID)
                {
                    if (e.Data != null && e.Data.Contains("Guard") && !_isvalidated)
                    {
                        string input = "";
                        ShowInputDialog(ref input);
                        pr.StandardInput.Write(input);
                        pr.StandardInput.Flush();
                        pr.StandardInput.Close();
                        _guardVALID = true;

                    }
                }
                //Check bool before running - filtering out a few string lines - condition
                //if (!_strCLEAN)
                //{
                if (e.Data != null)
                {
                    if (e.Data.Contains("4UGuysDedicated") || e.Data.Contains("Using filelist") || e.Data.Contains("licenses for account"))
                    {
                        AppendTextInBox(cmdSCREEN, "");
                        return;
                    }
                    if (e.Data.Contains("not available from this account"))
                    {
                        _FailedDepot += "Depot " + _currentDepot + ", ";
                    }

                }
                //}

                AppendTextInBox(cmdSCREEN, e.Data + Environment.NewLine);
                AppendTextProgress(progCMD, e.Data);
            };
            pr.Exited += (sender, e) =>
            {

                _curStatus = true;
                _InterENDED = false;

            };

            bool started = pr.Start();
            pr.BeginOutputReadLine();


            return true;
        }

        async Task<bool> cmdDEPOT(string depot, string manifest)
        {
            try
            {


                NameValueCollection sAll;
                sAll = ConfigurationManager.AppSettings;
                string _downldCNT = RegCalls.GetREG(@"SOFTWARE\DERM", "Downloads") == null ? "8" : RegCalls.GetREG(@"SOFTWARE\DERM", "Downloads");

                if (autoSTEAM.Checked)
                    /*Editted this line to request -beta. The Compiled binary uses a steam account*/
                    argz = "/c dotnet \"" + System.AppDomain.CurrentDomain.BaseDirectory + "\\DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir " + "\"" + SaveDirectoryPath() + "\"" + " -beta " + "-filelist " + "\"" + System.AppDomain.CurrentDomain.BaseDirectory + "exclude.txt" + "\" -validate -beta -max-downloads " + _downldCNT + " & exit";
                else
                    argz = "/c dotnet \"" + System.AppDomain.CurrentDomain.BaseDirectory + "\\DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir " + "\"" + SaveDirectoryPath() + "\"" + " -username " + userBOX.Text + " -password " + "\"" + passBOX.Text + "\"" + " -remember-password -filelist " + "\"" + System.AppDomain.CurrentDomain.BaseDirectory + "exclude.txt" + "\" -validate -max-downloads " + _downldCNT + " & exit";
                //output proccess lines

                Process mypr = new Process();
                await OUTproc(argz, mypr);
                return true;


            }
            catch (SystemException)
            {
                return false;
            }

        }

        private static int LinePicker(string ver, string mydir)
        {
            int i = 0;
            foreach (string line in File.ReadLines(mydir + "\\depatches.txt"))
            {
                i++;
                if (line.Contains(ver))
                {
                    return i;
                }
            }
            return 0;
        }
        public async Task<string> DownloadStringAsync(Uri uri, int timeOut = 60000)
        {
            try
            {
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

                return await Task.FromResult(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "depatches.txt") ? File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "depatches.txt") : "");
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
                    i++;
                    if (i == LinePicker(version, exeDIR) && i != 0)
                    {


                        string[] spline = line.Split('>');
                        string[] subline = spline[1].Split('#');
                        _currentDepots = subline.Length;

                        int j = 0;
                        foreach (string id in subline)
                        {
                            j++;
                            //Depot command
                            if (j >= 1)
                            {
                                string[] fsplit = id.Split('+');

                                AllDepots.Add(fsplit[0]);
                                depo = fsplit[0];
                                string[] ffsplit = fsplit[1].Split('<');

                                AllManif.Add(ffsplit[0]);
                                manif = ffsplit[0];
                            }



                        }
                        //MessageBox.Show(AllDepots.Count.ToString() + " Depots To Download.");
                        _pass = null;
                        _FailedDepot = null;
                        _currentDepots = AllDepots.Count;
                        //Depot Counter
                        if (AllDepots.Count == 1)
                        {
                            await cmdDEPOT(depo, manif);
                            Tdone = true;
                            VerCheck();

                        }
                        else if (AllDepots.Count > 1)
                        {

                            for (int k = 0; k < AllDepots.Count; k++)
                            {
                                _curStatus = false;
                                _strCLEAN = false;
                                bool acmd = false;
                                _currentDepot = (k + 1).ToString();
                                _Stoprog = true;
                                this.BeginInvoke((Action)(() =>
                                {
                                    cmdSCREEN.Text = "";
                                    progCMD.Text = "[" + _currentDepot + @"\" + _currentDepots + " Depots] (" + 0 + "%)";
                                    progCMD.Value = 0;

                                }));


                                acmd = await cmdDEPOT(AllDepots[k], AllManif[k]);

                                while (!_curStatus && !_InterENDED)
                                {
                                    await Task.Delay(1000);
                                }
                            }

                            Tdone = true;
                            VerCheck();
                            cmdSCREEN.BeginInvoke((Action)(() =>
                            {
                                if (_FailedDepot != null)
                                {
                                    cmdSCREEN.Text = "Failed To Download These depots: " + _FailedDepot.TrimEnd(new char[] { ' ', ',' }) + Environment.NewLine + "Try upgrading using your steam credentials";
                                }
                                else
                                    cmdSCREEN.Text = "Successfully Downgraded To " + label1.Text + ".";



                            }));
                            kryptonButton1.BeginInvoke((Action)(() =>
                            {
                                kryptonButton1.Enabled = true;

                            }));

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
                FileSystem.CopyDirectory(sourceFolderPath, destinationFolderPath, UIOption.AllDialogs);
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
        async Task<bool> isVERIFED()
        {
            bool _passed = false; ;
            //this.Invoke((Action)(async () =>
            //{

            if (RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath") != null)
            {
                timer1.Stop();
                if (Directory.Exists(RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath")))
                {
                    saveDirectoryPath = RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath");
                    steamLBL.ForeColor = Color.ForestGreen;
                    steamLBL.Text = "Steam Already Verified!";
                    steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotgreen;
                    cbPATCH.Enabled = true;
                    kryptonButton1.Enabled = true;


                    var versionInfo = FileVersionInfo.GetVersionInfo(saveDirectoryPath + @"\AoE2DE_s.exe");
                    string version = versionInfo.FileVersion;
                    string[] sver = version.Split('.');
                    label1.Text = "Current Version: " + sver[2];
                    timer1.Stop();
                    _passed = true;

                }

            }

            //}));

            return _passed;
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //var tsk = await isVERIFED();
                //bool verify = tsk;

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
                    RegCalls.AddREG(SaveDirectoryPath(), @"SOFTWARE\DERM", "GamePath");
                    timer1.Stop();
                }
            }
            catch (SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
                steamSTATUS.Image = DeReplaysManager.Properties.Resources.dotred;

            }

        }



        private void settingbtn_Click(object sender, EventArgs e)
        {
            DTSettings dt = new DTSettings();
            dt.Show();
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if ((progCMD.Value > 0 && progCMD.Value < 100) || kryptonButton1.Enabled == false)
            {
                DialogResult dialogResult = MessageBox.Show(this, "Close window now?", "Close?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = false;
                    this.Dispose();
                    foreach (var process in Process.GetProcessesByName("dotnet"))
                        process.Kill();
                    this.Close();

                }
                else if (dialogResult == DialogResult.No) e.Cancel = true;

            }

        }

        private void autoSTEAM_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSTEAM.Checked)
            {
                userBOX.Enabled = false;
                passBOX.Enabled = false;
                cbPATCH.Enabled = true;
            }
            else
            {
                userBOX.Enabled = true;
                passBOX.Enabled = true;
            }
        }

        private async void cmdSCREEN_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            cmdSCREEN.SelectionStart = cmdSCREEN.Text.Length;
            // scroll it automatically
            cmdSCREEN.ScrollToCaret();
            if (cmdSCREEN.Lines.Length > 60)
                cmdSCREEN.Text = "";
            await Task.Delay(50);
        }



        private async void Form1_Shown(object sender, EventArgs e)
        {

            var tsk = await isVERIFED();
            bool verify = tsk;

            if (await IsConnected())
            {

                string stid = await DownloadStringAsync(new Uri(@"https://github.com/gregstein/DE-Replays-Manager/raw/master/depotpatches.txt"));
                if (stid != null)
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
            if (!result.Contains("Usage"))
            {
                DialogResult dialogResult = MessageBox.Show(this, "DotNet Runtime is missing. Download it now?", "Missing Component!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    Process.Start("https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.6-windows-x64-installer");

                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
        }


    }
}

