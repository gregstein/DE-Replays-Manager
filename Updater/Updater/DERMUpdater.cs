

using Microsoft.Win32;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using System.Runtime.InteropServices;
using System.Threading;
namespace Updater
{

    public partial class DERMUpdater : Form
    {

        public DERMUpdater()
        {

            InitializeComponent();

        }
        bool downloadDone = false;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        private const UInt32 BOTTOM_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private static void Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    //MessageBox.Show(".NET Framework Version: " + CheckFor45PlusVersion((int)ndpKey.GetValue("Release")));
                }
                else
                {
                    MessageBox.Show("DeReplaysManager Requires .NET Framework Version 4.5 or later. Click OK to access Microsoft official website and download .NET 4.5 framework.");
                    Process.Start(@"https://www.microsoft.com/en-us/download/details.aspx?id=30653");
                }
            }
        }

        // Checking the version using >= will enable forward compatibility.
        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2 or later";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";
            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }
        async Task<bool> GetVer()
        {
            try
            {
                //Exit DeReplaysManager
                Process[] processes = Process.GetProcessesByName("DeReplaysManager");
                if (processes.Length > 0)
                {
                    processes[0].Kill();
                }
                //Check File Permissions
                try
                {
                    File.WriteAllText(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\updatercheck.txt", "Intentionally created by DeReplaysManager");
                    File.Delete(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\updatercheck.txt");
                    File.Delete(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\DeReplaysManager.exe.tmp");
                }
                catch (UnauthorizedAccessException)
                {
                    var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    startInfo.Verb = "runas";
                    try
                    {
                        System.Diagnostics.Process.Start(startInfo);
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                    Process.GetCurrentProcess().Kill();
                }
                //Check Updates
                bool result = await CheckForInternetConnection();
                if (result == true)
                {
                    WebClient wk = new WebClient();
                    wk.Headers.Add("user-agent", "DeReplaysManager");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.Expect100Continue = false; ServicePointManager.MaxServicePointIdleTime = 0;
                    var _strwk = wk.DownloadString("https://api.github.com/repos/gregstein/DE-Replays-Manager/releases/latest");
                    var jObject = Newtonsoft.Json.Linq.JObject.Parse(_strwk);
                    //string uptag = (string)jObject["tag_name"];
                    string updatezip = (string)jObject["assets"][2]["browser_download_url"];
                    string updatename = (string)jObject["assets"][2]["name"];
                    string getupsize = (string)jObject["assets"][2]["size"];
                    var convsize = Math.Round(Convert.ToDouble(getupsize) / 1024.0 / 1024.0, 2);
                    sizeupdate.Text = convsize.ToString() + " Mb";
                    zipname.Text = updatename.Replace("X_", "").Replace("--ignore", "");
                    zipdownload.Text = updatezip;
                    if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\version.txt") && File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "version.txt") != zipname.Text && !zipname.Text.Contains("exe"))
                    {
                        await Task.Run(() => DownloadArchiveAsync(zipdownload.Text));
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(this, "No Updates Found!");
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "DeReplaysManager.exe");
                        var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                        ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                        Process.GetCurrentProcess().Kill();
                        return true;
                    }


                }
                else
                {
                    MessageBox.Show(this, "Internet is disconnected!");
                    return false;
                }
            }
            catch (SystemException)
            {
                updateit.Enabled = true;
                //MessageBox.Show(this, "No Updates Found!");
                //Process.Start(AppDomain.CurrentDomain.BaseDirectory + "AoE2Tools.exe");
                //var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                //Process.GetCurrentProcess().Kill();
                return false;
            }


        }
        public static async Task<bool> CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        async Task<bool> DownloadArchiveAsync(string fileUrl)
        {
            var downloadLink = new Uri(fileUrl);
            var saveFilename = System.IO.Path.GetFileName(downloadLink.AbsolutePath.Replace("X_", "").Replace("--ignore", "").Replace(".", ""));

            DownloadProgressChangedEventHandler DownloadProgressChangedEvent = (s, e) =>
            {
                progressBar.BeginInvoke((Action)(() =>
                {
                    progressBar.Value = e.ProgressPercentage;
                }));

                var downloadProgress = string.Format("{0} MB / {1} MB",
                        (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                        (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));

                statusLabel.BeginInvoke((Action)(() =>
                {
                    statusLabel.Text = "Downloading " + downloadProgress + " ...";
                }));
            };
            AsyncCompletedEventHandler AsyncCompletedEvent = (s, e) =>
            {
                // Todo: Extract
                downloadDone = true;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll.old"))
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll.old");

                File.Move(AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll", AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll.old");

                progressBar.BeginInvoke((Action)(() =>
                {

                    pictureBox1.Visible = true;
                    //progressBar.Value = 0;
                }));
                if (downloadDone == true)
                {

                    //File.Move(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe.old"));
                    using (ZipFile zip = ZipFile.Read(AppDomain.CurrentDomain.BaseDirectory + "newupdate.zip"))
                    {
                        zip.ExtractProgress +=
                           new EventHandler<ExtractProgressEventArgs>(zip_ExtractProgress);
                        progressBar.BeginInvoke((Action)(() =>
                        {
                            try
                            {
                                zip.ExtractAll(AppDomain.CurrentDomain.BaseDirectory, ExtractExistingFileAction.OverwriteSilently);
                            }
                            catch (SystemException)
                            {

                            }

                        }));


                    }
                }
                if (zipname.Text != null)
                {
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "version.txt", zipname.Text);
                }
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll.old"))
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll.old");


                Thread.Sleep(1000);
                pictureBox2.Visible = true;
                //To Restore
                //**********
                try
                {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "DeReplaysManager.exe");
                    var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                    ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                    Process.GetCurrentProcess().Kill();
                }
                catch (SystemException)
                {
                    Process.GetCurrentProcess().Kill();
                }

            };
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                webClient.DownloadProgressChanged += DownloadProgressChangedEvent;
                webClient.DownloadFileCompleted += AsyncCompletedEvent;
                await webClient.DownloadFileTaskAsync(downloadLink, System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\newupdate.zip");
                zipdownload.Text = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\newupdate.zip";
            }

            return true;
        }



        void zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                progressBar1.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //File.Delete("Newtonsoft.Json.dll.old");


            //File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe.bak");
            //File.Move(AppDomain.CurrentDomain.BaseDirectory + "Updater.exe", AppDomain.CurrentDomain.BaseDirectory + "Updater.exe.bak");
            //Get45PlusFromRegistry();
            await Task.Run(() => GetLang());

            await Task.Run(() => Get45PlusFromRegistry());

            updateit.BackColor = Color.Gainsboro;
            updateit.Enabled = false;
            await Task.Run(() => GetVer());
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            updateit.BackColor = Color.Gainsboro;
            updateit.Enabled = false;
            await Task.Run(() => GetVer());
        }
        void GetLang()
        {
            try
            {
                if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("fr"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "Mettre à jour";
                    });

                }
                else if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("ar"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "تحديث";
                    });

                }
                else if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("de"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "Aktualisieren";
                    });


                }
                else if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("es"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "Actualizar";
                    });

                }
                else if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("pt"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "Atualizar";
                    });

                }
                else if (InputLanguage.CurrentInputLanguage.Culture.Name.Contains("nl"))
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        updateit.Text = "Bijwerken";
                    });

                }
            }
            catch (SystemException)
            {

            }

        }

        private void AoE2ToolsUpdater_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "DeReplaysManager.exe");
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                Process.GetCurrentProcess().Kill();
            }
            catch (SystemException)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
