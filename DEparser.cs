using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De_Roll
{
    class DEparser
    {
        public string recTEXT;
        public string playerTEXT;
        public List<string> playerDB = new List<string>();
        public List<string> chatDB = new List<string>();
        public string GetStrings(string myrec)
        {
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe"))
            {
                Process p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "CMD.EXE";
                string args = "/C " + System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe  -n 15 " + "\"" + myrec + "\"";
                p.StartInfo.Arguments = args;
                p.Start();

                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                recTEXT = output;
                return output;
            }
            else
            {
                MessageBox.Show("strings.exe not found!", "error!");
                return null;
            }

        }
        public string GetPlayernfp(string myrec)
        {
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe"))
            {
                Process p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "CMD.EXE";
                string args = "/C " + System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe  -n 4 " + "\"" + myrec + "\"";
                p.StartInfo.Arguments = args;
                p.Start();

                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                playerTEXT = output;
                return output;
            }
            else
            {
                MessageBox.Show("strings.exe not found!", "error!");
                return null;
            }

        }
        public string RetName(string newline)
        {
            // Part 1: the input string.
            string input = newline;

            // Part 2: call Regex.Match.
            Match match = Regex.Match(input, @"@#\d+ (.*) advanced",
                RegexOptions.IgnoreCase);

            // Part 3: check the Match for Success.
            if (match.Success)
            {
                // Part 4: get the Group value and display it.
                string key = match.Groups[1].Value;
                return key;
            }
            return null;
        }
        public void CollectPlayers(string rec)
        {
            foreach (string str in rec.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str.Contains("advanced"))
                {
                    if (!playerDB.Contains(RetName(str)) && RetName(str) != null)
                        playerDB.Add(RetName(str));
                    // do something with the line
                }
            }




        }
        public void CollectChat(string rec)
        {
            foreach (string str in rec.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str.Contains(@"@#"))
                {
                    foreach (string ln in playerDB)
                    {
                        if (str.Contains(ln) & !str.Contains("advanced"))
                        {

                            Regex pattern = new Regex(@"@#\d+ (.*)");
                            Match match = pattern.Match(str);
                            chatDB.Add(match.Groups[1].Value);

                        }
                    }

                    // do something with the line
                }
            }




        }
        
            public string CollectPlayerName(string nfp)
        {
            foreach (string str in nfp.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!str.StartsWith(@"??") && str.Contains(@".hki"))
                {
                    return str;

                    // do something with the line
                }
            }

            return null;


        }

    }
}
