using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XinstApp.Installers
{
    class InstallerAdobeReader : Installer
    {
        private static InstallerAdobeReader _instance;
        public static InstallerAdobeReader Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerAdobeReader(); }
                return _instance;
            }
        }

        private string patchRemotePath = "";
        private string patchFileName = "";
        private string PatchTempPath => Path.Combine(Path.GetTempPath(), patchFileName);
        protected string PatchOfflinePath => Path.Combine(this.entryDir, "files", this.patchFileName);

        private InstallerAdobeReader()
        {
            this.remotePath = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";
            this.fileName = "AcroRdrDC1900820071_pl_PL.exe";
            this.arguments = "/sAll";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "Adobe Reader";
        }

        public override async Task<int> DownloadAsync(DownloadProgressChangedEventHandler downloadProgress)
        {
            await EstablishLastestPatchLocation();
            Task<int> downloadInstaller = DownloadFileAsync(downloadProgress, this.offlinePath, this.remotePath, this.tempPath);
            Task<int> downloadPatch = DownloadFileAsync(downloadProgress, this.PatchOfflinePath, this.patchRemotePath, this.PatchTempPath);
            return await downloadInstaller + await downloadPatch;
        }

        public override async Task Install()
        {
            await InstallReaderDC();
            this.Controls.Status.Content = "UPDATING...";
            await UpdateReaderDC();
            DeleteDesktopShotcut();
            this.Controls.Status.Content = "CLEANING...";
            try
            {
                DeleteTempFiles();
                DeleteTempFiles(this.PatchTempPath, this.PatchOfflinePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e);
            }
        }

        private Task InstallReaderDC()
        {
            var tcs = new TaskCompletionSource<object>();
            Process p = new Process()
            {
                StartInfo = { FileName = $"\"{ this.tempPath }\"",
                            Arguments = $"{ this.arguments }",
                            Verb = "runas" }
            };
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                tcs.SetResult(null);
            };
            p.Start();
            return tcs.Task;
        }

        private Task UpdateReaderDC()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();            
            string tempPath = Path.Combine(Path.GetTempPath(), "temp.cmd");

            using (Stream fs = File.Create(tempPath))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.Write($"START /WAIT msiexec /update { this.patchFileName } /qn");

            Process p = new Process()
            {
                StartInfo = { FileName = tempPath,
                            Verb = "runas",
                            CreateNoWindow = true,
                            UseShellExecute = false }
            };
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);
                tcs.SetResult(null);
            };
            p.Start();

            return tcs.Task;
        }

        private async Task EstablishLastestPatchLocation()
        {
            string initialFTPDirPath = "ftp://ftp.adobe.com/pub/adobe/reader/win/AcrobatDC/";
            List<string> initialDirLst = await ListFTPDirectoryAsync(initialFTPDirPath);
            int latestPatchNo = GetLatestPatchNo(initialDirLst);
            string endFTPDirPath = Path.Combine(initialFTPDirPath, latestPatchNo.ToString());
            List<string> endDirLst = await ListFTPDirectoryAsync(endFTPDirPath);

            this.patchFileName = MatchPatchFileName(endDirLst, latestPatchNo);
            this.patchRemotePath = Path.Combine(endFTPDirPath, this.patchFileName);
        }

        private string MatchPatchFileName(List<string> list, int versionNo)
        {
            Regex regex = new Regex($"(\\w+{ versionNo }\\.(msp))");
            return regex.Match(list.FirstOrDefault(q => regex.Match(q).Success)).Value;
        }

        private int GetLatestPatchNo(List<string> directories) 
            => directories.Where(q => Int32.TryParse(q, out _) == true).Select(q => Int32.Parse(q)).Max();

        private async Task<List<string>> ListFTPDirectoryAsync(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            List<string> result = new List<string>();

            using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())
            {
                Stream responseStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(responseStream))
                    while (!reader.EndOfStream) { result.Add(await reader.ReadLineAsync()); }
            }
            return result;
        }

        private void DeleteDesktopShotcut()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            string shortcutPath = Path.Combine(desktopPath, "Acrobat Reader DC.lnk");
            if (File.Exists(shortcutPath)) File.Delete(shortcutPath);
        }
    }
}