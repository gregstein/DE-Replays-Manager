using De_Roll;
using DevComponents.DotNetBar;
using Ionic.Zip;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DeReplaysManager
{

    public partial class DTSettings : OfficeForm
    {
        public DTSettings()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            //Grab Similtaneous downloads count
            int val = 8;
            if (Int32.TryParse(RegCalls.GetREG(@"SOFTWARE\DERM", "Downloads"), out val))
                slideSIM.Value = Int32.Parse(RegCalls.GetREG(@"SOFTWARE\DERM", "Downloads"));

            DEparser dp = new DEparser();
            string pfiletype = dp.GrabID(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"exclude.txt")));
            string[] disft = pfiletype.Split('|');
            foreach (string s in disft)
            {
                fltypes.Text += s + "\n";
            }

            string[] zips = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.zip");
            if (zips != null)
            {
                foreach (string z in zips)
                {
                    rsbak.Enabled = true;
                    break;
                }
            }
            if (Directory.Exists(RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath")))
                gamepathTXT.Text = RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath");
            else
            {
                MessageBox.Show("GamePath is invalid! Please enter a correct path.", "AoE2DE Not Found!");
                gamepathTXT.Text = RegCalls.GetREG(@"SOFTWARE\DERM", "GamePath");
            }

        }

        private void excludeft_ValueChanged(object sender, EventArgs e)
        {



        }



        public void BakResFunc(string def)
        {
            DEparser dp = new DEparser();

            string[] subdirectoryEntries = Directory.GetDirectories(dp.dePATH);


            foreach (string subdirectory in subdirectoryEntries)
            {

                if (dp.IsDigitsOnly(subdirectory.Replace(dp.dePATH + "\\", "")) && subdirectory.Length > 4 && subdirectory.Replace(dp.dePATH + "\\", "") != "0")
                {

                    if (def == "bak")
                    {
                        using (ZipFile zip = new ZipFile())
                        {

                            zip.AddDirectory(subdirectory + @"\profile");
                            zip.Comment = "This zip was created by DE Replays Manager at " + System.DateTime.Now.ToString("G");
                            zip.Save(subdirectory.Replace(dp.dePATH + "\\", "") + ".zip");
                        }

                    }

                    else if (def == "res")
                    {
                        using (ZipFile zip = ZipFile.Read(subdirectory.Replace(dp.dePATH + "\\", "") + ".zip"))
                        {
                            zip.ExtractAll(subdirectory + @"\profile", ExtractExistingFileAction.OverwriteSilently);
                        }

                    }



                }
            }
        }

        private void hkbak_ValueChanged(object sender, EventArgs e)
        {

        }



        private async void fltypes_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.reload;
            await Task.Delay(1000);
            pictureBox1.Visible = true;

            string init = @"^(?!.*\.(";
            string ending = @")).*$";
            int i = 0;
            string exfile = "";
            foreach (string str in fltypes.Lines)
            {
                i++;
                if (str == "")
                    continue;

                if (i == 1)
                {
                    exfile += str;
                    continue;
                }

                if (str != "")
                    exfile += @"|" + str;
            }

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"exclude.txt"), init + exfile.Replace(Environment.NewLine, " ") + ending);
            pictureBox1.Image = Properties.Resources.icons8_checkmark_64;
        }

        private void crbak_Click(object sender, EventArgs e)
        {
            BakResFunc("bak");
            MessageBox.Show("Success!", "Hotkeys Backup Created!");
            rsbak.Enabled = true;
        }

        private void rsbak_Click(object sender, EventArgs e)
        {
            try
            {
                BakResFunc("res");
                MessageBox.Show("Success!", "Hotkeys Backup Restored!");
            }
            catch (SystemException)
            {

            }
        }

        private void changePATH_Click(object sender, EventArgs e)
        {
            RegCalls.AddREG(gamepathTXT.Text.TrimEnd('\\'), @"SOFTWARE\DERM", "GamePath");
            if (Directory.Exists(gamepathTXT.Text))
            {
                De_Roll.Form1 dr = new De_Roll.Form1();
                dr.GameDirInternal = gamepathTXT.Text.TrimEnd('\\');
                dr.Close();
                MessageBox.Show("Success!", "Game Path Updated!");
            }
            else
            {
                MessageBox.Show("GamePath is invalid! Please enter a correct path.", "Wrong Path!");
            }
        }

        private async void slideSIM_ValueChanged(object sender, EventArgs e)
        {
            disVAL.Text = slideSIM.Value.ToString();
            RegCalls.AddREG(slideSIM.Value.ToString(), @"SOFTWARE\DERM", "Downloads");
            await Task.Delay(200);
        }
    }
}
