using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DeReplaysManager.Libraries
{
    internal class Association
    {
        public static void RegisterCustomProtocol(string protocolName, string applicationPath, string iconPath)
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                isElevated = new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            }

            if (!isElevated)
            {
                throw new Exception("You need to run this code as an Administrator.");
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(protocolName))
            {
                key.SetValue("", "URL:" + protocolName);
                key.SetValue("URL Protocol", "");

                using (RegistryKey defaultIconKey = key.CreateSubKey("DefaultIcon"))
                {
                    defaultIconKey.SetValue("", iconPath);
                }

                using (RegistryKey commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", "\"" + applicationPath + "\" \"%1\"");
                }
            }
        }
        public static void SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath, string iconPath)
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                isElevated = new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
            }

            if (!isElevated)
            {
                throw new Exception("You need to run this code as an Administrator.");
            }

            

            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(progId))
            {
                if (key != null)
                {
                    var defaultIconKey = key.OpenSubKey("DefaultIcon");
                    var shellOpenCommandKey = key.OpenSubKey(@"Shell\Open\Command");

                    if (defaultIconKey != null && shellOpenCommandKey != null)
                    {
                        var defaultIconValue = (string)defaultIconKey.GetValue("");
                        var shellOpenCommandValue = (string)shellOpenCommandKey.GetValue("");

                        
                    }
                }
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(extension))
            {
                key.SetValue("", progId);
            }

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(progId))
            {
                key.SetValue("", fileTypeDescription);
                key.CreateSubKey("DefaultIcon").SetValue("", "\"" + iconPath + "\",0");
                key.CreateSubKey(@"Shell\Open\Command").SetValue("", "\"" + applicationFilePath + "\" \"%1\"");
            }
        }
    }
}
