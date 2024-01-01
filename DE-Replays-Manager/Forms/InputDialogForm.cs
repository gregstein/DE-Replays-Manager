using DeReplaysManager.Libraries;
using DevComponents.DotNetBar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeReplaysManager.Forms
{
    public partial class InputDialogForm : OfficeForm
    {
        public string UserInput { get; private set; }
        public string PassInput { get; private set; }
        public bool SaveSession { get; private set; }
        public string _depotcount { get; set; }
        public string _downgradedate { get; set; }

        public InputDialogForm()
        {
            InitializeComponent();
            
        }

        private void InputDialogForm_Load(object sender, EventArgs e)
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
                    labelPrompt.Text = DC.Decrypt(encryptedUser);
                    passPrompt.Text = DC.Decrypt(encryptedPass);
                    savesession.Checked = true;
                }
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            UserInput = labelPrompt.Text;
            PassInput = passPrompt.Text;
            SaveSession = savesession.Checked;

            if (SaveSession)
            {
                // Encrypt and save the credentials to the registry
                var encryptedUser = DC.Encrypt(UserInput);
                var encryptedPass = DC.Encrypt(PassInput);
                var key = Registry.CurrentUser.CreateSubKey(@"Software\DERM");
                key.SetValue("User", encryptedUser);
                key.SetValue("Pass", encryptedPass);
            }

            
            DERM_Reader dr = new DERM_Reader();
            dr._depotcount = _depotcount;
            dr._downgradedate = _downgradedate;
            dr._user_box = UserInput;
            dr._pass_box = PassInput;
            dr.Show();
            this.Hide();
            //DialogResult = DialogResult.OK;
            //Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
    }
}
