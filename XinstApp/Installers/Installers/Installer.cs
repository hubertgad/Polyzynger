using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace XinstApp.Installers
{
    abstract class Installer
    {
        /// <summary>
        /// Path from which file is downloaded.
        /// </summary>
        protected string remotePath { get; set; }

        /// <summary>
        /// Full temporary path to an installer file.
        /// </summary>
        protected string tempPath { get; set; }

        /// <summary>
        /// Installer file name.
        /// </summary>
        protected string fileName { get; set; }

        /// <summary>
        /// Installation arguments to be passed to installation process.
        /// </summary>
        protected string arguments { get; set; }

        /// <summary>
        /// GUI controls model.
        /// </summary>
        public Controls Controls { get; set; }

        protected Installer()
        {
            this.Controls = new Controls();
            this.arguments = " /qn";
        }

        /// <summary>
        /// Perform an installation of .exe or .msi file using System.Diagnostics.Process class.
        /// </summary>
        public virtual Task Install()
        {
            var tcs = new TaskCompletionSource<object>();

            Process p = new Process();
            p.StartInfo = this.tempPath.Contains(".msi") ? 
                new ProcessStartInfo 
                {
                    FileName = $"msiexec.exe",
                    Arguments = $"/i \"{ this.tempPath }\" /q /norestart"
                } : 
                new ProcessStartInfo 
                {
                    FileName = $"\"{ this.tempPath }\"",
                    Arguments = $"{ this.arguments }",
                };
            p.StartInfo.Verb = "runas";
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                tcs.SetResult(null);
            };
            p.Start();

            return tcs.Task;
        }

        /// <summary>
        /// Downloads a file from location specified in the constructor and saves it in the temporary location specified in the constructor.
        /// </summary>
        /// <param name="downloadProgress">Progress bar handler.</param>
        /// <returns>If completed successfully returns 0. If not returns 1.</returns>
        public virtual Task DownloadAsync() => DownloadFileAsync(this.remotePath, this.tempPath);

        /// <summary>
        /// Downloads file from given location and saves it using given local path.
        /// </summary>
        /// <param name="downloadProgress">Progress bar handler.</param>
        /// <param name="remotePath">Remote location of the file.</param>
        /// <param name="tempPath">Local temporary path where the file has to be saved.</param>
        /// <returns>If completed successfully returns 0. If not returns 1.</returns>
        public virtual Task DownloadFileAsync(string remotePath, string tempPath)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    Controls.ProgressBar.Value = e.ProgressPercentage;
                };
                client.DownloadFileCompleted += (sender, e) => tcs.SetResult(0);
                client.DownloadFileAsync(new Uri(remotePath), tempPath);

                return tcs.Task;
            }
        }

        /// <summary>
        /// Deletes temporary files in previously specified temp path.
        /// </summary>
        public virtual void DeleteTempFiles() => DeleteTempFiles(this.tempPath);

        /// <summary>
        /// Deletes temporary files in given path.
        /// </summary>
        /// <param name="path">Path to temporary files.</param>
        public virtual void DeleteTempFiles(string path)
        {
            if (File.Exists(path)) { File.Delete(path); }
        }

        /// <summary>
        /// Checks if given Url is valid.
        /// </summary>
        /// <param name="url">Url address to be checked.</param>
        /// <returns>Returns true if Url is valid, otherwise returns false.</returns>
        protected bool IsUrlValid(string url)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    using (Stream stream = webClient.OpenRead(url))
                    { 
                        return true; 
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}