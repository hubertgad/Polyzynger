using PolyzyngerApplication.Downloaders;
using PolyzyngerApplication.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class InstallationController : Controller
    {
        protected readonly static SemaphoreSlim _installationSemaphore = new SemaphoreSlim(1);

        protected IScanner Scanner;

        protected IDownloader Downloader;

        protected IExecutor Executor;

        /// <summary>
        /// Path from which installer file is downloaded.
        /// </summary>
        protected string InstallerUri;

        private string _installerFileName;

        /// <summary>
        /// Installer file name.
        /// </summary>
        public string InstallerFileName
        {
            get { return _installerFileName ?? InstallerUri.Split('/').LastOrDefault(); }
            set { _installerFileName = value; }
        }

        /// <summary>
        /// Full temporary path to an installer file.
        /// </summary>
        protected string InstallerTempPath => Path.Combine(Path.GetTempPath(), InstallerFileName);

        /// <summary>
        /// Installation arguments to be passed to installation process.
        /// </summary>
        protected string InstallationArguments;

        protected InstallationController(EventHandler<State> handler, 
            IExecutor executor, IScanner scanner = null, IDownloader downloader = null)
            : base (handler)
        {
            Scanner = scanner;

            Downloader = downloader ?? new DefaultDownloader();

            Executor = executor;
        }

        internal override async Task InstallAsync()
        {
            Stage finalStage = Stage.DONE;

            try
            {
                await ScannAsync();

                await DownloadAsync();

                await PutSemaphoreAsync();

                await ExecuteAsync();
            }
            catch
            {
                finalStage = Stage.ERROR;
            }
            finally
            {
                CleanUp(finalStage);
            }
        }

        protected virtual async Task ScannAsync()
        {
            if (Scanner != null)
            {
                State.Stage = Stage.SCANNING;
                var newInstallerUri = await Scanner.CheckLatestVersionPathAsync(InstallerUri);
                AssignNewUriIfValid(ref InstallerUri, newInstallerUri);
            }
        }

        protected virtual async Task DownloadAsync()
        {
            State.Stage = Stage.DOWNLOADING;
            await Downloader.DownloadAsync(InstallerUri, InstallerTempPath, State);
        }

        protected virtual Task PutSemaphoreAsync()
        {
            State.Stage = Stage.WAITING;
            return _installationSemaphore.WaitAsync();
        }

        protected virtual Task ExecuteAsync()
        {
            State.Stage = Stage.INSTALLING;
            return Executor.ExecuteAsync(InstallerTempPath, InstallationArguments);
        }

        protected virtual void CleanUp(Stage finalStage)
        {
            _installationSemaphore.Release();
            DeleteTempFile(InstallerTempPath);

            State.Stage = finalStage;
        }

        protected void DeleteTempFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        protected bool IsUrlValid(string url)
        {
            try
            {
                using WebClient webClient = new WebClient();
                using Stream stream = webClient.OpenRead(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void AssignNewUriIfValid(ref string currentUri, string newUri)
        {
            if (IsUrlValid(newUri))
            {
                currentUri = newUri;
            }
        }
    }
}