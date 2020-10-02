using DevComponents.DotNetBar;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeReplaysManager
{
    public partial class Submission : OfficeForm
    {
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        public string RecInstance { get; set; }
        public Submission()
        {
            InitializeComponent();
            subody.Select();
            Bubbleit(REVpro, "An Expert will record & review your replay then send it to you as a video file. ");
            Bubbleit(REVbasic, "Plaintext Reviews from Random users.");

        }
        Task<int> Bubbleit(DevComponents.DotNetBar.Controls.CheckBoxX lbl, string msg)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(lbl, msg);
            ToolTip1.ToolTipIcon = ToolTipIcon.Info;
            ToolTip1.IsBalloon = true;
            ToolTip1.ShowAlways = true;
            return Task.FromResult(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void subody_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void subody_Click(object sender, EventArgs e)
        {
            if (subody.Text == "Type here..")
            {
                subody.Text = "";
                subody.Select();
            }
        }
       
        private void aoebuildsbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void patreondermbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void tickethelp_Click(object sender, EventArgs e)
        {
            KeyInfo kf = new KeyInfo();
            kf.Show();
        }
        private void TicketField(bool val)
        {
            lblticket.Enabled = val;
            ticketcode.Enabled = val;
        }
        private void REVpro_CheckedChanged(object sender, EventArgs e)
        {
            if (REVpro.Checked)
                TicketField(true);
            else
                TicketField(false);
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
        private void Submission_Load(object sender, EventArgs e)
        {
            string staN = Path.GetFileNameWithoutExtension(RecInstance) == "" ? "" : Path.GetFileNameWithoutExtension(RecInstance);
            if (staN.Length > 30 && staN != "")
                reclabel.Text = new string(staN.Take(30).ToArray()) + "..  .aoe2record";
            else
                reclabel.Text = staN + ".aoe2record";

            RecINFO nfo = new RecINFO();
            string JSON = JsonConvert.SerializeObject(nfo);
            File.WriteAllText("rec.json", JSON);


        }
        
        
        
        private async void subreview_Click(object sender, EventArgs e)
        {
            subreview.Enabled = false;
            int chk = await Checkers();
            Core rep = new Core();
            RecINFO nfo = new RecINFO();
            if (ticketcode.Text != "")
            {
                rep.trimKEY = EnSerial(ticketcode.Text);
                nfo.DeciKey = DecryptTicket(rep.trimKEY, "DERM8Z70");
                if (nfo.DeciKey == "")
                {
                    MessageBox.Show("Wrong ticket Key! Enter valid key!");
                    subreview.Enabled = true;
                    return;
                }
                    

                string[] extPAR = nfo.DeciKey.Split(new string[] { "<+>AOE2BUILDS" }, StringSplitOptions.None);
                nfo.USERprem = extPAR[0];
                nfo.SecretId = extPAR[0] + ">" + extPAR[1];
                nfo.TicketKey = true;
            }
            
            //Uploading Replay
            //Fetch destination
            progressBar2.Value = 10;
                string RECname = Path.GetFileName(RecInstance).Replace(".aoe2record", ".derm");
                //string RECLic = Path.GetFileName(RecInstance).Replace(".aoe2record", ".pro");
                string RECDesc = Path.GetFileName(RecInstance).Replace(".aoe2record", ".json");
                string RECpath = System.IO.Path.GetTempPath() + @"\" + RECname;
                nfo.Title = titlerec.Text;
                nfo.Description = subody.Text;
                nfo.Recname = RECname;
                nfo.Nickname = textBoxX1.Text;
            //Serialization
            string JSON = JsonConvert.SerializeObject(nfo);
            //Rec check
           
            bool IsAvail = await rep.IsRecExists(RECname);

                    if (RECpath != null)
                    {
                    //Encode rec
                    Core.CompressFileLZMA(RecInstance, System.IO.Path.GetTempPath() + Path.GetFileName(RecInstance).Replace(".aoe2record", ".derm"));
                    //Upload rec
                    var dbxz = new DropboxClient(rep._key);
                            await Core.Upload(dbxz, "/derm", RECname, RECpath);
                            progressBar2.Value = 50;
                    //Upload License
                    //if(nfo.SecretId != null && nfo.USERprem != null)
                    //        await Core.UploadLic(dbxz, "/derm", RECLic, nfo.SecretId);
                    //        progressBar2.Value = 70;
                    //Upload Description
                    
                            await Core.UploadLic(dbxz, "/derm", RECDesc, JSON);
                            progressBar2.Value = 100;
                    //Mission complete?
                    subreview.Enabled = true;
                    progressBar2.Value = 0;
                MessageBox.Show("Successfully Submitted!", "Success");
                this.Close();
            }
                    else
                    { MessageBox.Show("Replay magically disappeared! Try again."); subreview.Enabled = true; }
                        


           
        }
        private Task<int> Checkers()
        {
            
            if (subody.Text == "")
            {
                MessageBox.Show("Please enter your nick name!", "Nick name empty!");
                subreview.Enabled = true;
                return Task.FromResult(0);
            }
            if (subody.Text == "" || subody.Text == "Type here..")
            {
                MessageBox.Show("Please write a few words in the description field.", "Description empty!");
                subreview.Enabled = true;
                return Task.FromResult(0);
            }
            if (!Core.CheckForInternetConnection())
            {
                MessageBox.Show("Your internet is offline!", "Offline");
                subreview.Enabled = true;
                return Task.FromResult(0);
            }

            return Task.FromResult(0);
        }
        

    }
}
