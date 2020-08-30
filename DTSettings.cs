using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using De_Roll;
using DevComponents.DotNetBar;
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
            DEparser dp = new DEparser();
            string pfiletype = dp.GrabID(File.ReadAllText(@"exclude.txt"));
            string[] disft = pfiletype.Split('|');
            foreach(string s in disft)
            {
                fltypes.Text += s + "\n";
            }

            string[] zips = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.zip");
            if(zips != null)
            {
                foreach (string z in zips)
                {
                    rsbak.Enabled = true;
                    break;
                }
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
   
                        if(def == "bak")
                    {
                        using (ZipFile zip = new ZipFile())
                            {
                              
                              zip.AddDirectory(subdirectory + @"\profile");
                              zip.Comment = "This zip was created by DE Replays Manager at " + System.DateTime.Now.ToString("G");
                              zip.Save(subdirectory.Replace(dp.dePATH + "\\", "") + ".zip");
                            }
                        
                    }
                        
                        else if(def == "res")
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
                if(str == "")
                    continue;

                if (i == 1)
                {
                    exfile += str;
                    continue;
                }

                if(str != "")
                exfile += @"|" + str;
            }
            
            File.WriteAllText("exclude.txt", init + exfile.Replace(Environment.NewLine, " ") + ending);
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
    }
}
