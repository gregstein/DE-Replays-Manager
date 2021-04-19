using Microsoft.Win32;

namespace DeReplaysManager
{
    class RegCalls
    {
        public static bool checkExistance()
        {
            using (RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DERM", true))
                return (winLogonKey == null);
        }
        public static void AddREG(string val, string path = @"SOFTWARE\DERM\SAVEGAME", string field = "SV")
        {

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(path))
            {
                //key.CreateSubKey("SAVEGAME");
                key.SetValue(field, val, RegistryValueKind.String);
            }
        }

        public static string GetREG(string path = @"SOFTWARE\DERM\SAVEGAME", string field = "SV")
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(path))
            {
                if (key != null && key.GetValue(field) != null)
                    return key.GetValue(field).ToString();
                else
                {

                    return null;
                }
            }
        }
        public static void procCMD(string mycmd)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + mycmd;
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
    }
}
