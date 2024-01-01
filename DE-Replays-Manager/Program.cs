using DeReplaysManager;
using DeReplaysManager.Forms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace DEReplaysManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        [STAThread]
        static void Main(string[] args)
        {
            bool _ignoreUI = false;
            string iconPath = "<path_to_icon>"; // Replace with the path to your icon
            DeReplaysManager.Libraries.Association.SetAssociation(".derm", "DERMFile", "DERM File", Application.ExecutablePath, iconPath);
            DeReplaysManager.Libraries.Association.RegisterCustomProtocol("derm", Application.ExecutablePath, iconPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                string data = "";
                _ignoreUI = true;

                if (args[0].EndsWith(".derm"))
                {
                    string filePath2 = args[0];
                    data = File.ReadAllText(filePath2);
                }
                else
                {
                    string filePath = args[0];
                    string base64String = filePath.Substring("derm://".Length);
                    base64String = Uri.UnescapeDataString(base64String);
                    if (base64String.EndsWith("/"))
                    {
                        base64String = base64String.Substring(0, base64String.Length - 1);
                    }

                    byte[] dataBytes = Convert.FromBase64String(base64String);
                    data = Encoding.UTF8.GetString(dataBytes);
                }
                

                string[] lines = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // Open DERM reader
                InputDialogForm form = new InputDialogForm();
                form._depotcount = lines[1];
                form._downgradedate = lines[0];
                Application.Run(form);

                //form.Show();

            }
            if (!_ignoreUI)
            {
                Application.Run(new Form1());

            }


        }

    }
}