using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace XinstApp.Installers
{
    abstract class Installer
    {
        protected string status;
        protected string remotePath;
        protected string localPath;
        protected string fileName;
        protected string arguments;
        protected string entryDir;
        public Controls Controls { get; set; }
        protected string offlinePath => Path.Combine(this.entryDir, this.fileName);

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
        
        public virtual Task<int> DownloadAsync(DownloadProgressChangedEventHandler downloadProgress)
        {
            if (File.Exists(this.offlinePath))
            {
                this.localPath = this.offlinePath;
                return null;
            }

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += downloadProgress;
                client.DownloadFileCompleted += (sender, e) =>
                {
                    tcs.SetResult(0);
                };
                client.DownloadFileAsync(new Uri(this.remotePath), this.localPath);
            }
            catch
            {
                tcs.SetResult(1);
            }
            return tcs.Task;
        }

        public virtual void DeleteTempFiles()
        {
            if (this.localPath == this.offlinePath) return;
            if (File.Exists(this.localPath)) File.Delete(this.localPath);
        }

        private string GetEntryAssemblyDirName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).DirectoryName;
        }
    }
}
