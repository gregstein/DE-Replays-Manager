using ComponentFactory.Krypton.Toolkit;
using De_Roll;
using DeReplaysManager.Properties;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DeReplaysManager.Forms
{
    
    public partial class DERM_Reader : OfficeForm
    {
        private static Form form;
        //DERM Reader
        public string _downgradedate { get; set; }
        public string _user_box { get; set; }
        public string _pass_box { get; set; }
        public string _depotcount { get; set; }
        //Other Core
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
        private AsyncManualResetEvent _inputEvent = new AsyncManualResetEvent();
        private bool _noinput;
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
                    if (Directory.Exists(RegCalls.GetREG("SOFTWARE\\DERM", "GamePath")))
                    {
                        saveDirectoryPath = RegCalls.GetREG("SOFTWARE\\DERM", "GamePath");
                        steamLBL.ForeColor = Color.ForestGreen;
                        steamLBL.Text = "Steam Already Verified!";
                        _passed = true;
                        timer1.Stop();

                    }
                }
                return _passed;
            }
            catch (SystemException)
            {
                return false;
            }

        }
        public DERM_Reader()
        {
            InitializeComponent();
        }

        private async void DERM_Reader_Load(object sender, EventArgs e)
        {
            bool _isver = await isVERIFED();
            ddate.Text = _downgradedate;
            dcount.Text = (Int32.Parse(_depotcount)/2).ToString();
            //string input2 = "";
            //string input3 = "";
            //ShowInputDialog(ref input2, ref input3, "Enter Your Steam User Name:");
            //_user_box = input2;
            //ShowInputDialog(ref input3, "Enter Your Steam Password:", true);
            //_pass_box = input3;
            if(_user_box == "" || _pass_box == "")
            {
                MessageBox.Show("Please enter your steam credentials to proceed!", "Error!");
                Application.Exit();
            }
            if(_isver)
            {
                DepotCommander(_downgradedate);
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
                            

                            if (cmdSCREEN.IsHandleCreated)
                            {
                                cmdSCREEN.Invoke((Action)delegate
                                {
                                    if (_FailedDepot != null)
                                    {
                                        //cmdSCREEN.Text = "Failed To Download These depots: " + _FailedDepot.TrimEnd(' ', ',') + Environment.NewLine + "Try upgrading using your steam credentials";
                                    }
                                    else
                                    {
                                        cmdSCREEN.Text = "Successfully Downgraded To " + ddate.Text + ".";
                                    }
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
                    if(e.Data.Contains("Downloading") || e.Data.Contains("Got AppInfo"))
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
                    ShowInputDialog2(ref input);
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.Flush();
                    _isvalidated = true;
                    _noinput = true;
                    await Task.Delay(100); // Adjust the sleep duration as needed
                }
                
            }

            _inputEvent.Set(); // Signal that input handling is complete
        }
        private DialogResult ShowInputDialog(ref string input, ref string password, string wintitle, bool _ispass = false)
        {
            Office2007Form form2 = new Office2007Form();

        Size clientSize = new Size(400, 150);
           
            StyleManager styleManager = new StyleManager();
            styleManager.ManagerStyle = eStyle.Office2007VistaGlass;


            //Office2007VistaGlass

            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.TopMost = true;
            form2.TopLevel = true;
            form2.FormBorderStyle = FormBorderStyle.FixedDialog;
            form2.ClientSize = clientSize;
            form2.Text = wintitle;
            
            //form.BackColor = Color.Black;

            LabelX userLabel = new LabelX();
            userLabel.Text = "Steam User";
            userLabel.Location = new Point(5, 5);
            userLabel.ForeColor = Color.White;
            form2.Controls.Add(userLabel);

            TextBoxX userTextBox = new TextBoxX();
            userTextBox.Size = new Size(clientSize.Width - 10, 23);
            userTextBox.Location = new Point(5, 25);
            userTextBox.Text = input;
            userTextBox.ForeColor = Color.White;
            //userTextBox.Enter += (s, e) => { userTextBox.BackColor = Color.DarkGray; };
            //userTextBox.Leave += (s, e) => { userTextBox.BackColor = Color.Black; };
            form2.Controls.Add(userTextBox);

            LabelX passwordLabel = new LabelX();
            passwordLabel.Text = "Steam Password";
            passwordLabel.Location = new Point(5, 55);
            passwordLabel.ForeColor = Color.White;
            form2.Controls.Add(passwordLabel);

            TextBoxX passwordTextBox = new TextBoxX();
            passwordTextBox.UseSystemPasswordChar = true;
            passwordTextBox.Size = new Size(clientSize.Width - 10, 23);
            passwordTextBox.Location = new Point(5, 75);
            
            passwordTextBox.Text = password;
            passwordTextBox.ForeColor = Color.White;
            //passwordTextBox.Enter += (s, e) => { passwordTextBox.BackColor = Color.DarkGray; };
            //passwordTextBox.Leave += (s, e) => { passwordTextBox.BackColor = Color.Black; };
            form2.Controls.Add(passwordTextBox);

            ButtonX button = new ButtonX();
            button.DialogResult = DialogResult.OK;
            button.Name = "okButton";
            button.Size = new Size(75, 23);
            button.Text = "&OK";
            button.Location = new Point(clientSize.Width - 80 - 80, 105);
            button.ColorTable = eButtonColor.OrangeWithBackground;
            form2.Controls.Add(button);

            ButtonX button2 = new ButtonX();
            button2.DialogResult = DialogResult.Cancel;
            button2.Name = "cancelButton";
            button2.Size = new Size(75, 23);
            button2.Text = "&Cancel";
            button2.Location = new Point(clientSize.Width - 80, 105);
            button2.ColorTable = eButtonColor.OrangeWithBackground;
            form2.Controls.Add(button2);

            form2.AcceptButton = button;
            form2.CancelButton = button2;

            form2.Shown += (s, e) => { form2.Opacity = 0; for (double i = 0; i <= 1; i += 0.1) { form2.Opacity = i; System.Threading.Thread.Sleep(50); } };

            DialogResult result = form2.ShowDialog();
            input = userTextBox.Text;
            password = passwordTextBox.Text;
            return result;
        }



        private static DialogResult ShowInputDialog2(ref string input)
        {
            
            Size clientSize = new Size(400, 70);
            form = new Form();
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

                string escapedUserBox = System.Security.SecurityElement.Escape(_user_box);

                argz = $"/c dotnet \"{AppDomain.CurrentDomain.BaseDirectory}DepotDownloader.dll\" -app 813780 -depot {depot} -manifest {manifest} -dir \"{SaveDirectoryPath()}\" -username \"{escapedUserBox}\" -remember-password -password \"{_pass_box}\" -validate -max-downloads {_downldCNT} --exclude *.wmv,*.avi";

                File.AppendAllText(@"C:\Users\shock\source\repos\DE-Replays-Manager\DE-Replays-Manager\bin\x86\Debug\myarg.txt", argz);

                //await ExecuteCommandAsync(argz);
                await OUTproc(argz: argz, pr: new Process());
                return true;
            }
            catch (SystemException)
            {
                return false;
            }
        }
        public static void CloseForm()
        {
            form?.Close();
        }

        // Method to click the OK button
        public static void ClickOkButton()
        {
            Button okButton = form?.Controls.OfType<Button>().FirstOrDefault(b => b.Name == "okButton");
            okButton?.PerformClick();
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
                        return;
                    }
                    steamLBL.ForeColor = Color.Gold;
                    await Task.Delay(200);
                    steamLBL.Text = "Steam OFF";
                    await Task.Delay(100);
                    steamLBL.ForeColor = Color.Maroon;
                    return;
                }
                steamLBL.ForeColor = Color.ForestGreen;
                steamLBL.Text = "Steam ON";
                
                await Task.Delay(200);
                if (!SteamApps.BIsAppInstalled(new AppId_t(813780u)))
                {
                    MessageBox.Show("Please Buy or Install Age of Empires II: Definitive Edition before you proceed!", "Game Missing");
                    timer1.Stop();
                }
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(SaveDirectoryPath() + "\\AoE2DE_s.exe");
                string version = versionInfo.FileVersion;
                
                RegCalls.AddREG(SaveDirectoryPath(), "SOFTWARE\\DERM", "GamePath");
                DepotCommander(_downgradedate);
                timer1.Stop();
            }
            catch (SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
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
            await Task.Delay(5);
        }

        private void DERM_Reader_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
