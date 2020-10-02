using DevComponents.DotNetBar.Controls;
using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeReplaysManager
{
    public abstract class replib
    {
        public string _key = "KEY REMOVED";
        public bool _RepExists;
        public async Task<bool> IsRecExists(string recname)
        {
            using (var dbx = new DropboxClient(_key))
            {

                var sharedLink = new SharedLink("/derm");
                var sharedFiles = await dbx.Files.ListFolderAsync(sharedLink.Url);


                foreach (var file in sharedFiles.Entries)
                {
                    if (file.Name == recname)
                    { _RepExists = true; MessageBox.Show("Replay already exists! Please rename your replay."); break; }

                }
            }
            return _RepExists ? true : false;
        }
    }
    public class RecINFO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Recname { get; set; }
        public string USERprem { get; set; }
        public string SecretId { get; set; }
        public string DeciKey { get; set; }
        
        public bool TicketKey { get; set; }
        public string Nickname { get; internal set; }
    }
    public class Reviewer
    {
        public string Nickname { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public bool isVerified { get; set; }

    }
    public class Core: replib
    {
        internal string trimKEY;

        

        public static async Task Upload(DropboxClient client, string folder, string fileName, string filelocation)
        {

            using (var stream = new MemoryStream(File.ReadAllBytes(filelocation)))
            {
                var response = await client.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: stream);

                //Console.WriteLine("Uploaded Id {0} Rev {1}", response.Id, response.Rev);
            }
        }
        public static async Task UploadLic(DropboxClient client, string folder, string fileName, string fileContent)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(fileContent);
            using (var stream = new MemoryStream(byteArray))
            {
                var response = await client.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: stream);

                //Console.WriteLine("Uploaded Id {0} Rev {1}", response.Id, response.Rev);
            }
        }
        public static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);
            output.Flush();
            output.Close();
            input.Close();
        }
        public static void CompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Write the encoder properties
            coder.WriteCoderProperties(output);

            // Write the decompressed file size.
            output.Write(BitConverter.GetBytes(input.Length), 0, 8);

            // Encode the file.
            coder.Code(input, output, input.Length, -1, null);
            output.Flush();
            output.Close();
            input.Close();
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<int> DownloadFile(string json, string savegame)
        {
            try { 
            DropboxClient client = new DropboxClient(_key);
            using (var response = await client.Files.DownloadAsync(json))
            {
                using (var fileStream = File.Create(savegame))
                {
                    (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                }
            }
            return 0;
            }
            catch (System.Net.Http.HttpRequestException) { MessageBox.Show("No Internet!"); return 0; }
        }
        
        public async Task<int> DownloadINFO(string JsonFile)
        {
            try { 
            var dbxz = new DropboxClient(_key);
            string folder = "derm";
            
            using (var response = await dbxz.Files.DownloadAsync("/" + folder + "/" + JsonFile))
            {
                using (var fileStream = File.Create(System.IO.Path.GetTempPath() + @"data.json"))
                {
                    (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                }
            }

            return 1;
        }
            catch (System.Net.Http.HttpRequestException) { MessageBox.Show("No Internet!"); return 0; }

}
        public async Task<string> DownloadSTRING(string JsonFile)
        {
            try
            {
                var dbxz = new DropboxClient(_key);
                string folder = "derm";

                using (var response = await dbxz.Files.DownloadAsync("/" + folder + "/" + JsonFile))
                {

                    return await response.GetContentAsStringAsync();
                }
            }
            catch(DropboxException)
            {
                return null;
            }
        
            catch (System.Net.Http.HttpRequestException) {  MessageBox.Show("No Internet!"); return null; }




}
    }
}
