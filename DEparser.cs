using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
        public string currentFileName;
        public string newFileName;
        public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
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

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static void CopyStreamHeader(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = null;
            byte[] bufferHeaderlen = new byte[4];
            byte[] bufferSaved_chapter = new byte[4];
            int len;
            int lencnt = 0;

            int header_len = 0;
            int saved_chapter = 0;

            //. header_len, saved_chapter
            lencnt = input.Read(bufferHeaderlen, 0, 4);
            if (lencnt > 0)
            {
                header_len = BitConverter.ToInt32(bufferHeaderlen, 0);
            }
            lencnt = input.Read(bufferSaved_chapter, 0, 4);
            if (lencnt > 0)
            {
                saved_chapter = BitConverter.ToInt32(bufferSaved_chapter, 0);
            }

            //. read header buffer
            header_len = header_len - 8;

            buffer = new byte[header_len + 2];
            buffer[0] = 0x78;
            buffer[1] = 0x9c;

            if ((len = input.Read(buffer, 2, header_len - 6)) > 0)
            {
                output.Write(buffer, 0, header_len - 6);
            }
            output.Flush();
        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        public void decompressFile(string inFile, string outFile)
        {
            var outputStream = new MemoryStream();
            using (var compressedStream = new MemoryStream(File.ReadAllBytes(inFile)))
            using (var inputStream = new InflaterInputStream(compressedStream))
            {
                inputStream.CopyTo(outputStream);
                outputStream.Position = 0;
                File.WriteAllBytes("test.txt", ReadToEnd(outputStream));
            }

        }
        private byte[] DeREC(MemoryStream ms)
        {
            ms.Seek(0, SeekOrigin.Begin);
            Inflater inflater = new Inflater(true);
            InflaterInputStream inStream = new InflaterInputStream(ms, inflater);
            byte[] buf = new byte[5000000];

            int buf_pos = 0;
            int count = buf.Length;

            while (true)
            {
                int numRead = inStream.Read(buf, buf_pos, count);
                if (numRead <= 0)
                {
                    break;
                }
                buf_pos += numRead;
                count -= numRead;
            }
            File.WriteAllBytes("test.txt", buf);
            return buf;
        }
        public string GetPlayernfp(string myrec)
        {
            string rosText = File.ReadAllText(myrec);
            Regex word = new Regex(@"[^\s]{2}[^?](.*).hki");
            Match m = word.Match(rosText);
            MessageBox.Show("Result:" + m.Value.Substring(16).Replace(".hki", ""));
            playerTEXT = m.Value.Substring(16).Replace(".hki", "");
            return m.Value.Substring(16).Replace(".hki", "");
            //if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe"))
            //{
            //    Process p = new Process();
            //    p.StartInfo.CreateNoWindow = true;
            //    p.StartInfo.UseShellExecute = false;
            //    p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //    p.StartInfo.RedirectStandardOutput = true;
            //    p.StartInfo.FileName = "CMD.EXE";
            //    string args = "/C " + "\"" + System.AppDomain.CurrentDomain.BaseDirectory + "strings.exe\" "  + "\"" + myrec + "\"";
            //    System.Windows.Forms.Clipboard.SetText(args);

            //    p.StartInfo.Arguments = args;
            //    p.Start();

            //    string output = p.StandardOutput.ReadToEnd();
                
            //    p.WaitForExit();
            //    playerTEXT = output;
            //    MessageBox.Show(output);

            //    return output;
            //}
            //else
            //{
            //    MessageBox.Show("strings.exe not found!", "error!");
            //    return null;
            //}
            
        }
        public string GrabID(string verstring)
        {
            return verstring.Split('(', ')')[2];
            
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
                else if(str.Contains(":"))
                {
                    Regex pattern = new Regex(@"@#\d+ (.*)");
                    Match match = pattern.Match(str);
                    playerDB.Add(match.Groups[1].Value.Split(':').FirstOrDefault());
                }
                else { continue; }
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
                        if (!str.Contains("advanced"))
                        {

                            Regex pattern = new Regex(@"@#\d+ (.*)");
                            Match match = pattern.Match(str);
                            chatDB.Add(match.Groups[1].Value);

                        }
                    }

                    // do something with the line
                }
                else { continue; }
            }




        }
        public void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                currentFileName = fileToDecompress.FullName;
                newFileName = currentFileName.Replace(@".nfp", ".db");

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);

                        //File.WriteAllBytes("test.txt", output.ToArray());
                        //Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
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
