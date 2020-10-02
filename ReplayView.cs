using DevComponents.DotNetBar;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeReplaysManager
{
    public partial class ReplayView : OfficeForm
    {

        public ReplayView()
        {
            InitializeComponent();
        }
        public string GrabRECNAME;
        public string JsonFile { get; set; }
        public string ThisREC;
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        public List<int> lstrev = new List<int>();
        private int afx;

        private void importrec_Click(object sender, EventArgs e)
        {
            ImportREC ir = new ImportREC();
            ir.GetReplayLink = ThisREC;
            ir.Show();

        }
       
        private async void ReplayView_Load(object sender, EventArgs e)
        {
            
            Core rd = new Core();
            
            
            int reslt = await rd.DownloadINFO(JsonFile);
            ReplayParser rp = new ReplayParser();
            rp = ReplayParser.FromJson(System.IO.Path.GetTempPath() + "data.json");
            GrabRECNAME = rp.Recname;
            lbltitle.ForeColor = Color.Black;
            lbltitle.Text = rp.Title;
            bodytext.ForeColor = Color.Black;
            bodytext.Text = rp.Description;
            ThisREC = rp.Recname;
            importrec.Text = rp.Recname.Replace(".derm", ".aoe2record");
            label2.Visible = true;
            lbltitle.Visible = true;
            bodytext.Visible = true;
            importrec.Visible = true;
            expaNEL.TitleText = "Replay sent by " + rp.UseRprem;
            pgsbar.Visible = false;

        }

        private void expandablePanel2_EnabledChanged(object sender, EventArgs e)
        {
           
        }

        private void expaNEL_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            if (!expaNEL.Expanded)
                subody.Size = new Size(671, 250);
            else
                subody.Size = new Size(671, 133);
        }
        private string EnSerial(string s)
        {
            string serial = s.Replace(@"-", @"/");
            return serial + @"==";
        }
        public static string DecryptTicket(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (SystemException)
            {
                MessageBox.Show("Wrong Ticket key!");
                return null;

            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
        public async Task<int> IterateReviewsCount()
        {
            pgsbar.Visible = true;
            Core it = new Core();
         
            using (var dbx = new DropboxClient(it._key))
            {
                var sharedLink = new SharedLink("derm");
                var sharedFiles = await dbx.Files.ListFolderAsync("", true);

                foreach (var file in sharedFiles.Entries)
                {
                    ListBoxItem st = new ListBoxItem();
                    if (file.PathDisplay.EndsWith(".rev") && file.Name != "derm" && file.Name.StartsWith(Path.GetFileName(GrabRECNAME).Replace(".derm", "")))
                    {
                        Regex word = new Regex(@"(.*)-(\d+?).rev");
                        Match m = word.Match(file.Name);
                        

                        if (!lstrev.Contains(int.Parse(m.Groups[2].Value)))
                            {
                            lstrev.Add(int.Parse(m.Groups[2].Value));
                            }
                        


                    }


                }

                pgsbar.Visible = false;
                return lstrev.Count != 0 ? lstrev.LastOrDefault() : 0;

            }
            
            
        }
        private async void titlerec_TextChanged(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            
            nickFIELD.Enabled = String.IsNullOrEmpty(titlerec.Text);

            Core rep = new Core();
            Reviewer nfo = new Reviewer();
            string DeciKey = "";
            if (titlerec.Text != "")
            {
                rep.trimKEY = EnSerial(titlerec.Text);
                DeciKey = DecryptTicket(rep.trimKEY, "DERM8Z70");
                if (DeciKey == "")
                {
                    picVER.Image = Properties.Resources.bullet_valid_icon;
                    MessageBox.Show("Wrong ticket Key! Enter valid key!");
                    subreview.Enabled = true;
                    return;
                }

                picVER.Image = Properties.Resources.no_icon;
                string[] extPAR = DeciKey.Split(new string[] { "<+>AOE2BUILDS" }, StringSplitOptions.None);
                nfo.Nickname = extPAR[0].Split('>').FirstOrDefault();
                nfo.Description = subody.Text;
                nickFIELD.Text = nfo.Nickname;
                string JSON = JsonConvert.SerializeObject(nfo);
                //Check and store available rev files
               
                
                nickFIELD.Enabled = false;
                vidLBL.Visible = true;
                vidFIELD.Visible = true;
                vidFIELD.Enabled = true;
                picVER.Image = Properties.Resources.bullet_valid_icon;
                
            }

        }
        public async Task<int> CheckRevs(string recname)
        {
           
            Core it = new Core();
           
            using (var dbx = new DropboxClient(it._key))
            {
                var sharedLink = new SharedLink("derm");
                var sharedFiles = await dbx.Files.ListFolderAsync("", true);

                foreach (var file in sharedFiles.Entries)
                {
                   
                    if (file.Name.StartsWith(recname) && file.PathDisplay.EndsWith(".rev") && file.Name != "derm")
                    {

                    }


                }

            }
            
            return 0;
        }
        private async void subreview_Click(object sender, EventArgs e)
        {
            subreview.Enabled = false;

            try { 
            
                Reviewer nfo = new Reviewer();
                Core rep = new Core();
                var dbxz = new DropboxClient(rep._key);
                string JSON;
                if (nickFIELD.Enabled == false)
                {
                    string DeciKey = titlerec.Text;
                    string[] extPAR = DeciKey.Split(new string[] { "<+>AOE2BUILDS" }, StringSplitOptions.None);
                    nfo.Nickname = nickFIELD.Text;
                    nfo.Description = subody.Text;
                    nfo.Video = vidFIELD.Text;
                    nfo.isVerified = true;
                    JSON = JsonConvert.SerializeObject(nfo);
                }
                else
                {
                    nfo.Nickname = nickFIELD.Text;
                    nfo.Description = subody.Text;
                    nfo.Video = "";
                    nfo.isVerified = false;
                    JSON = JsonConvert.SerializeObject(nfo);
                }
                    
                progressBarX1.Value = 10;
                //Grab new affix for rev file
                afx = await IterateReviewsCount() + 1;
                //new name for rev file
                string REVname = Path.GetFileName(GrabRECNAME).Replace(".derm", "") + "-" + afx.ToString() + ".rev";

                //upload new .rev file
                await Core.UploadLic(dbxz, "/derm", REVname, JSON);
                //close window
                MessageBox.Show("Thank you! \n Your Review has been submitted <3");
                this.Close();
            }
            catch (System.Net.Http.HttpRequestException) { pgsbar.Visible = false; MessageBox.Show("No Internet!"); }


        }

        private void subody_TextChanged(object sender, EventArgs e)
        {

        }

        private void subody_Click(object sender, EventArgs e)
        {
            if(subody.Text == "Type here..")
            {
                subody.Text = "";
            }
        }

        private void picVER_Click(object sender, EventArgs e)
        {

        }

        private void picVER_BackgroundImageChanged(object sender, EventArgs e)
        {
           

        }
    }
}
