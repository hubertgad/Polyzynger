using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

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

        public Installer()
        {
            this.status = "Initializing...";
            this.entryDir = GetEntryAssemblyDirName();
            this.Controls = new Controls();
        }

        public virtual void Install()
        {

        }
        public virtual Task DownloadAsync(DownloadProgressChangedEventHandler downloadProgress)
        {
            if (File.Exists(this.offlinePath))
            {
                this.localPath = this.offlinePath;
                return null;
            }

            this.Controls.ProgressBar.Visibility = Visibility.Visible;
            this.Controls.Status.Content = "DOWNLOADING...";

            var tcs = new TaskCompletionSource<object>();
            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += downloadProgress;
                client.DownloadFileCompleted += (sender, e) =>
                {
                    this.Controls.Status.Content = "WAITING FOR INSTALL...";
                    this.Controls.ProgressBar.Visibility = Visibility.Hidden;
                    tcs.SetResult(null);
                };
                client.DownloadFileAsync(new Uri(this.remotePath), this.localPath);

            }
            catch
            {
                this.Controls.Status.Content = "DOWNLOAD ERROR";
                this.Controls.ProgressBar.Visibility = Visibility.Hidden;
                tcs.SetResult(null);
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