using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace XinstApp.Installers
{
    abstract class Installer
    {
        protected string status;
        protected string remotePath;
        protected string tempPath;
        protected string fileName;
        protected string arguments;
        protected string entryDir;
        public Controls Controls { get; set; }
        protected string offlinePath => Path.Combine(this.entryDir, "files", this.fileName);

        protected Installer()
        {
            this.status = "Initializing...";
            this.entryDir = GetEntryAssemblyDirName();
            this.Controls = new Controls();
            this.arguments = "/qn";
        }

        public virtual Task Install()
        {
            return null;
        }
        
        public virtual Task<int> DownloadAsync(DownloadProgressChangedEventHandler downloadProgress) => DownloadFileAsync(downloadProgress, this.offlinePath, this.remotePath, this.tempPath);
        
        protected virtual Task<int> DownloadFileAsync(DownloadProgressChangedEventHandler downloadProgress, string offlinePath, string remotePath, string tempPath)
        {
            if (File.Exists(offlinePath))
            {
                this.tempPath = this.offlinePath;
                return null;
            }

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += downloadProgress;
                client.DownloadFileCompleted += (sender, e) => tcs.SetResult(0);
                client.DownloadFileAsync(new Uri(remotePath), tempPath);
            }
            catch
            {
                tcs.SetResult(1);
            }
            return tcs.Task;
        }

        public virtual void DeleteTempFiles() => DeleteTempFiles(this.tempPath, this.offlinePath);

        public virtual void DeleteTempFiles(string tempPath, string offlinePath)
        {
            if (tempPath == offlinePath) return;
            if (File.Exists(tempPath)) File.Delete(tempPath);
        }

        private string GetEntryAssemblyDirName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).DirectoryName;
        }
    }
}