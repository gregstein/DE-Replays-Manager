using DeReplaysManager;
using DeReplaysManager.Forms;
using DeReplaysManager.Libraries;
using DeReplaysManager.Properties;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De_Roll
{
    public partial class Form1 : OfficeForm
    {
        private static Form form;
        public string UserInput { get; private set; }
        public string PassInput { get; private set; }
        public bool SaveSession { get; private set; }
        public string SetPatchVer { get; set; }
        private TextBox outputTextBox;
        public bool _stopinput = false;
        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            outputTextBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both
            };

            Controls.Add(outputTextBox);
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
        public static void CloseForm()
        {
            form?.Close();
        }
        public async Task<bool> IsConnected()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.OpenReadCompleted += delegate (object sender, OpenReadCompletedEventArgs e)
                {
                    if (e.Result != null)
                    {
                        _connected = true;
                    }
                };
                await webClient.OpenReadTaskAsync(new Uri("http://google.com/generate_204", UriKind.Absolute));
                return _connected;
            }
            catch (SystemException)
            {
                return _connected;
            }
        }

        private async Task<string> CheckDotNet()
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
            // Check if the registry has the encrypted credentials
            var key = Registry.CurrentUser.OpenSubKey(@"Software\DERM");
            if (key != null)
            {
                var encryptedUser = key.GetValue("User") as string;
                var encryptedPass = key.GetValue("Pass") as string;
                if (!string.IsNullOrEmpty(encryptedUser) && !string.IsNullOrEmpty(encryptedPass))
                {
                    // Decrypt the credentials and set them to the input fields
                    userBOX.Text = DC.Decrypt(encryptedUser);
                    passBOX.Text = DC.Decrypt(encryptedPass);
                    savesession.Checked = true;
                }
            }
            ((Control)(object)kryptonButton1).Enabled = false;
            if (((Control)(object)cbPATCH).Text != "Select Patch Version")
            {
                DepotCommander(((Control)(object)cbPATCH).Text);
            }
            VerCheck();
        }

        private static float _getPROG(string line)
        {
            if (line == null)
            {
                return 0f;
            }
            string pattern = "[+-]?([0-9]*[.])[0-9]+";
            MatchCollection matchCollection = Regex.Matches(line, pattern);
            if (matchCollection.Count > 0 && matchCollection[0].Groups[0].Value != null && float.TryParse(matchCollection[0].Groups[0].Value, out var _))
            {
                return float.Parse(matchCollection[0].Groups[0].Value);
            }
            return 0f;
        }

        private void AppendTextInBox(RichTextBox box, string text)
        {
            if (base.InvokeRequired)
            {
                Invoke(new Action<RichTextBox, string>(AppendTextInBox), cmdSCREEN, text);
            }
            else
            {
                box.Text += text;
            }
        }

        private void AppendTextProgress(ProgressBarX pb, string text)
        {
            if (base.InvokeRequired)
            {
                Invoke(new Action<ProgressBarX, string>(AppendTextProgress), pb, text);
            }
            else if (_getPROG(text) != 0f)
            {
                progCMD.Value = (int)_getPROG(text);
                pb.Text = "[" + _currentDepot + "\\" + _currentDepots + " Depots] (" + _getPROG(text) + "%)";
            }
        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            Size clientSize = new Size(200, 70);
            Form form = new Form();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.TopMost = true;
            form.TopLevel = true;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ClientSize = clientSize;
            form.Text = "Steam Guard Code!";
            TextBox textBox = new TextBox();
            textBox.Size = new Size(clientSize.Width - 10, 23);
            textBox.Location = new Point(5, 5);
            textBox.Text = input;
            form.Controls.Add(textBox);
            Button button = new Button();
            button.DialogResult = DialogResult.OK;
            button.Name = "okButton";
            button.Size = new Size(75, 23);
            button.Text = "&OK";
            button.Location = new Point(clientSize.Width - 80 - 80, 39);
            form.Controls.Add(button);
            Button button2 = new Button();
            button2.DialogResult = DialogResult.Cancel;
            button2.Name = "cancelButton";
            button2.Size = new Size(75, 23);
            button2.Text = "&Cancel";
            button2.Location = new Point(clientSize.Width - 80, 39);
            form.Controls.Add(button2);
            form.AcceptButton = button;
            form.CancelButton = button2;
            DialogResult result = form.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private AsyncManualResetEvent _inputEvent = new AsyncManualResetEvent();
        private bool _noinput;
        public async Task<bool> OUTproc(string argz, Process pr)
        {
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.FileName = "cmd.exe";
            pr.StartInfo.Arguments = argz;
            pr.StartInfo.RedirectStandardInput = true;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.RedirectStandardError = true;
            pr.StartInfo.CreateNoWindow = true;
            pr.EnableRaisingEvents = true;
            pr.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            pr.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    // logic...
                    if (e.Data == "")
                    {
                        _noinput = true;
                    }
                    if (e.Data.Contains("Downloading") || e.Data.Contains("Got AppInfo"))
                    {
                        _noinput = false;
                        //Shutdown Steam guard window
                        CloseForm();
                    }
                }

                AppendTextInBox(cmdSCREEN, e.Data + Environment.NewLine);
                AppendTextProgress(progCMD, e.Data);
            };

            pr.Exited += delegate
            {
                _curStatus = true;
                _InterENDED = false;
                _inputEvent.Set(); // Signal the input thread to stop waiting
            };

            pr.Start();
            pr.BeginOutputReadLine(); // Start reading the standard output

            // Separate thread to handle user input
            Task.Run(async () => await HandleUserInput(pr));

            // Wait for the input thread to complete
            await _inputEvent.WaitAsync(); // Wait for the input thread to finish processing the user input

            // Wait for the process to exit
            await WaitForExitAsync(pr);

            return true;
        }

        private async Task WaitForExitAsync(Process process)
        {
            await Task.Run(() =>
            {
                process.WaitForExit();
            });
        }
        
        public async Task HandleUserInput(Process process)
        {
            while (!_InterENDED && !_curStatus)
            {
                if (!_isvalidated)
                {
                    string input = "";
                    ShowInputDialog(ref input);
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.Flush();
                    _isvalidated = true;
                    await Task.Delay(100); // Adjust the sleep duration as needed
                }
               

            }

            _inputEvent.Set(); // Signal that input handling is complete
        }
        private async Task<string> ShowInputDialogAsync(string prompt)
        {
            var inputDialogForm = new InputDialogForm();

            if (inputDialogForm.ShowDialog() == DialogResult.OK)
            {
                return await Task.FromResult(inputDialogForm.UserInput);
            }
            else
            {
                return await Task.FromResult(string.Empty);
            }
        }

        private async Task<bool> cmdDEPOT(string depot, string manifest)
        {
            try
            {
                _ = ConfigurationManager.AppSettings;
                string _downldCNT = ((RegCalls.GetREG("SOFTWARE\\DERM", "Downloads") == null) ? "16" : RegCalls.GetREG("SOFTWARE\\DERM", "Downloads"));
                if (autoSTEAM.Checked)
                {
                    argz = "/c dotnet \"" + AppDomain.CurrentDomain.BaseDirectory + "DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir \"" + SaveDirectoryPath() + "\" -username " + depoc.systemdir + " -remember-password -password \"" + depoc.MDebuf("B+3oaKZs13CtBUYGRDXwafbGLh4EprTak6qAl8m8Kfl8GTs8McxmVY9yl/dtLy12IrIbL5Jdk1KBQN3Dc/6wOg==", "971X42324212874454D512") + "\" -remember-password  -validate -max-downloads " + _downldCNT + " & exit";
                }
                else
                {
                    argz = "/c dotnet \"" + AppDomain.CurrentDomain.BaseDirectory + "DepotDownloader.dll\" -app 813780 -depot " + depot + " -manifest " + manifest + " -dir \"" + SaveDirectoryPath() + "\" -username " + ((Control)(object)userBOX).Text + " -remember-password -password \"" + ((Control)(object)passBOX).Text + "\" -validate -max-downloads " + _downldCNT + " & exit";
                }


                //await ExecuteCommandAsync(argz);
                await OUTproc(argz: argz, pr: new Process());
                return true;
            }
            catch (SystemException)
            {
                return false;
            }
        }

        private static int LinePicker(string ver, string mydir)
        {
            int num = 0;
            foreach (string item in File.ReadLines(mydir + "\\depatches.txt"))
            {
                num++;
                if (item.Contains(ver))
                {
                    return num;
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
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "DE Replays Manager");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.MaxServicePointIdleTime = 0;
                    client.Encoding = Encoding.UTF8;
                    client.Proxy = null;
                    client.DownloadStringCompleted += delegate (object sender, DownloadStringCompletedEventArgs e)
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
                    DateTime i = DateTime.Now;
                    while (output == null && !cancelledOrError && DateTime.Now.Subtract(i).TotalMilliseconds < (double)timeOut)
                    {
                        await Task.Delay(100);
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

                File.WriteAllText(contents: await DownloadStringAsync(new Uri("https://github.com/gregstein/DE-Replays-Manager/raw/master/depotpatches.txt")), path: exeDIR + "\\depatches.txt");

                using (StreamReader file = new StreamReader(exeDIR + "\\depatches.txt"))
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        i++;

                        if (i != LinePicker(version, exeDIR) || i == 0)
                            continue;

                        string[] spline = line.Split('>');
                        string[] subline = spline[1].Split('#');
                        _currentDepots = subline.Length;

                        int j = 0;
                        string[] array = subline;

                        foreach (string id in array)
                        {
                            j++;
                            if (j < 1) continue;

                            string[] fsplit = id.Split('+');
                            AllDepots.Add(fsplit[0]);
                            depo = fsplit[0];
                            string[] ffsplit = fsplit[1].Split('<');
                            AllManif.Add(ffsplit[0]);
                            manif = ffsplit[0];
                        }

                        _pass = null;
                        _FailedDepot = null;
                        _currentDepots = AllDepots.Count;

                        if (AllDepots.Count == 1)
                        {
                            await cmdDEPOT(depo, manif);
                            Tdone = true;
                            VerCheck();
                        }
                        else
                        {
                            if (AllDepots.Count <= 1)
                                continue;

                            for (int k = 0; k < AllDepots.Count; k++)
                            {
                                _curStatus = false;
                                _strCLEAN = false;
                                _currentDepot = (k + 1).ToString();
                                _Stoprog = true;

                                if (cmdSCREEN.IsHandleCreated)
                                {
                                    cmdSCREEN.Invoke((Action)delegate
                                    {
                                        cmdSCREEN.Text = "";
                                        progCMD.Text = "[" + _currentDepot + "\\" + _currentDepots + " Depots] (" + 0 + "%)";
                                        progCMD.Value = 0;
                                    });
                                }

                                await cmdDEPOT(AllDepots[k], AllManif[k]);

                                while (!_curStatus && !_InterENDED)
                                {
                                    await Task.Delay(1000);
                                }
                            }

                            Tdone = true;
                            VerCheck();

                            if (cmdSCREEN.IsHandleCreated)
                            {
                                cmdSCREEN.Invoke((Action)delegate
                                {
                                    if (_FailedDepot != null)
                                    {
                                        cmdSCREEN.Text = "Failed To Download These depots: " + _FailedDepot.TrimEnd(' ', ',') + Environment.NewLine + "Try upgrading using your steam credentials";
                                    }
                                    else
                                    {
                                        cmdSCREEN.Text = "Successfully Downgraded To " + label1.Text + ".";
                                    }
                                });
                            }

                            if (kryptonButton1.IsHandleCreated)
                            {
                                kryptonButton1.Invoke((Action)delegate
                                {
                                    kryptonButton1.Enabled = true;
                                });
                            }
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Failed to retrieve Patch notes.", "Error!");
            }
        }



        private void VerCheck()
        {
            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(SaveDirectoryPath() + "\\AoE2DE_s.exe");
                string fileVersion = versionInfo.FileVersion;
                string[] array = fileVersion.Split('.');
                label1.Text = "Current Version: " + array[2];
            }
            catch (SystemException)
            {

            }
            
        }

        private void Copier(string depot)
        {
            try
            {
                string sourceDirectoryName = AppDomain.CurrentDomain.BaseDirectory + "tmp";
                string destinationDirectoryName = "C:\\test";
                FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, UIOption.AllDialogs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string SaveDirectoryPath()
        {
            if (!(saveDirectoryPath == "") || SteamApps.GetAppInstallDir((AppId_t)813780u, out saveDirectoryPath, 500u) == 0)
            {
            }
            return saveDirectoryPath;
        }

        private async Task<bool> isVERIFED()
        {
            try
            {
                bool _passed = false;
                if (RegCalls.GetREG("SOFTWARE\\DERM", "GamePath") != null)
                {
                    timer1.Stop();
                    if (Directory.Exists(RegCalls.GetREG("SOFTWARE\\DERM", "GamePath")))
                    {
                        saveDirectoryPath = RegCalls.GetREG("SOFTWARE\\DERM", "GamePath");
                        steamLBL.ForeColor = Color.ForestGreen;
                        steamLBL.Text = "Steam Already Verified!";
                        steamSTATUS.Image = Resources.dotgreen;
                        ((Control)(object)cbPATCH).Enabled = true;
                        ((Control)(object)kryptonButton1).Enabled = true;
                        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(saveDirectoryPath + "\\AoE2DE_s.exe");
                        string version = versionInfo.FileVersion;
                        string[] sver = version.Split('.');
                        label1.Text = "Current Version: " + sver[2];
                        timer1.Stop();
                        _passed = true;
                    }
                }
                return _passed;
            }
            catch (SystemException)
            {
                return false;
            }
            
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
                        steamSTATUS.Image = Resources.dotred;
                        return;
                    }
                    steamLBL.ForeColor = Color.Gold;
                    await Task.Delay(200);
                    steamLBL.Text = "Steam OFF";
                    await Task.Delay(100);
                    steamLBL.ForeColor = Color.Maroon;
                    steamSTATUS.Image = Resources.dotred;
                    return;
                }
                steamLBL.ForeColor = Color.ForestGreen;
                steamLBL.Text = "Steam ON";
                steamSTATUS.Image = Resources.dotgreen;
                ((Control)(object)cbPATCH).Enabled = true;
                ((Control)(object)kryptonButton1).Enabled = true;
                await Task.Delay(200);
                if (!SteamApps.BIsAppInstalled(new AppId_t(813780u)))
                {
                    MessageBox.Show("Please Buy or Install Age of Empires II: Definitive Edition before you proceed!", "Game Missing");
                    ((Control)(object)cbPATCH).Enabled = false;
                    timer1.Stop();
                }
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(SaveDirectoryPath() + "\\AoE2DE_s.exe");
                string version = versionInfo.FileVersion;
                string[] sver = version.Split('.');
                label1.Text = "Current Version: " + sver[2];
                RegCalls.AddREG(SaveDirectoryPath(), "SOFTWARE\\DERM", "GamePath");
                timer1.Stop();
            }
            catch (SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
                steamSTATUS.Image = Resources.dotred;
            }
        }

        private void settingbtn_Click(object sender, EventArgs e)
        {
            DTSettings dTSettings = new DTSettings();
            dTSettings.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((progCMD.Value <= 0 || progCMD.Value >= 100) && ((Control)(object)kryptonButton1).Enabled)
            {
                return;
            }
            switch (MessageBox.Show(this, "Close window now?", "Close?", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    {
                        e.Cancel = false;
                        Dispose();
                        Process[] processesByName = Process.GetProcessesByName("dotnet");
                        foreach (Process process in processesByName)
                        {
                            process.Kill();
                        }
                        Close();
                        break;
                    }
                case DialogResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void autoSTEAM_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSTEAM.Checked)
            {
                ((Control)(object)userBOX).Enabled = false;
                ((Control)(object)passBOX).Enabled = false;
                ((Control)(object)cbPATCH).Enabled = true;
            }
            else
            {
                ((Control)(object)userBOX).Enabled = true;
                ((Control)(object)passBOX).Enabled = true;
            }
        }

        private async void cmdSCREEN_TextChanged(object sender, EventArgs e)
        {
            cmdSCREEN.SelectionStart = cmdSCREEN.Text.Length;
            cmdSCREEN.ScrollToCaret();
            if (cmdSCREEN.Lines.Length > 60)
            {
                cmdSCREEN.Text = "";
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check if the registry has the encrypted credentials
            var key = Registry.CurrentUser.OpenSubKey(@"Software\DERM");
            if (key != null)
            {
                var encryptedUser = key.GetValue("User") as string;
                var encryptedPass = key.GetValue("Pass") as string;
                if (!string.IsNullOrEmpty(encryptedUser) && !string.IsNullOrEmpty(encryptedPass))
                {
                    // Decrypt the credentials and set them to the input fields
                    userBOX.Text = DC.Decrypt(encryptedUser);
                    passBOX.Text = DC.Decrypt(encryptedPass);
                    savesession.Checked = true;
                }
            }
        }
    }
    public class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        public Task WaitAsync() => tcs.Task;

        public void Set() => tcs.TrySetResult(true);

        public void Reset()
        {
            while (true)
            {
                var tcs = this.tcs;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref this.tcs, new TaskCompletionSource<bool>(), tcs) == tcs)
                    return;
            }
        }
    }
}
public static class ProcessExtensions
{
    public static Task<bool> WaitForExitAsync(this Process process, int milliseconds = -1)
    {
        var tcs = new TaskCompletionSource<bool>();
        process.EnableRaisingEvents = true;
        process.Exited += (s, e) => tcs.TrySetResult(true);
        if (milliseconds >= 0)
        {
            _ = Task.Delay(milliseconds).ContinueWith(_ => tcs.TrySetResult(false));
        }
        return tcs.Task;
    }
}


