using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

        private string patchRemotePath = "ftp://ftp.adobe.com/pub/adobe/reader/win/AcrobatDC/2000920065/AcroRdrDCUpd2000920065.msp"; //TODO: latest patch 
        private string patchFileName = "AcroRdrDCUpd2000920065.msp"; //TODO: latest patch name (string patchFileName => Regex.(...));
        private string PatchLocalPath => Path.Combine(Path.GetTempPath(), patchFileName);

        private InstallerAdobeReader()
        {
            this.remotePath = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";
            this.fileName = "AcroRdrDC1900820071_pl_PL.exe";
            this.arguments = "/sAll";
            this.localPath = Path.Combine(Path.GetTempPath(), this.fileName);
            //this.patchRemotePath = GetLatestPatch();

            this.Controls.CheckBox.Content = "Adobe Reader";
        }

        public override Task<int> DownloadAsync(DownloadProgressChangedEventHandler downloadProgress)
        {
            if (File.Exists(this.offlinePath))
            {
                this.localPath = this.offlinePath;
                return null;
            }

            object locker = new object();
            int downloadsCompleted = 0;
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += downloadProgress;
                    client.DownloadFileCompleted += DownloadCompleted;
                    client.DownloadFileAsync(new Uri(this.remotePath), this.localPath);
                }

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += downloadProgress;
                    client.DownloadFileCompleted += DownloadCompleted;
                    client.DownloadFileAsync(new Uri(this.patchRemotePath), this.PatchLocalPath);
                }
            }
            catch { tcs.SetResult(1); }

            void DownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
            {
                lock (locker) { downloadsCompleted++; }
                if (downloadsCompleted == 2) { tcs.SetResult(0); }
            };

            return tcs.Task;
        }

        public override async Task Install()
        {
            await this.BeginInstall();
            this.Controls.Status.Content = "UPDATING...";
            await UpdateReaderDC();
        }

        private Task BeginInstall()
        {
            var tcs = new TaskCompletionSource<object>();
            Process p = new Process()
            {
                StartInfo = { FileName = $"\"{ this.localPath }\"",
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
                sw.Write($"START /WAIT msiexec /update %TEMP%\\{ this.patchFileName } /qn");
            //TODO: sw.Write($"START /WAIT msiexec /update { this.patchLocalPatch } /qn");
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
    }
}