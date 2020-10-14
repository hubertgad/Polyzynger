using PolyzyngerApplication.Cleaners;
using PolyzyngerApplication.Downloaders;
using PolyzyngerApplication.Installers;
using PolyzyngerApplication.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class Controller
    {
        protected readonly State _state;

        protected readonly static SemaphoreSlim _installationSemaphore = new SemaphoreSlim(1);

        protected IChecker Checker;

        protected IDownloader Downloader;

        protected IInstaller Installer;

        protected ICleaner Cleaner;
       
        /// <summary>
        /// Path from which installer file is downloaded.
        /// </summary>
        protected string InstallerUri;

        /// <summary>
        /// Installer file name.
        /// </summary>
        protected string InstallerFileName => InstallerUri.Split('/').LastOrDefault();

        /// <summary>
        /// Full temporary path to an installer file.
        /// </summary>
        protected string InstallerTempPath => Path.Combine(Path.GetTempPath(), InstallerFileName);

        /// <summary>
        /// Installation arguments to be passed to installation process.
        /// </summary>
        protected string InstallationArguments = " /qn";

        protected Controller(EventHandler<State> handler)
        {
            _state = new State(handler);

            Downloader = new DefaultDownloader();

            Installer = new InstallerMsi();
            
            Cleaner = new DefaultCleaner();
        }

        internal virtual async Task ExecuteInstallationStepsAsync()
        {
            Stage finalStage = Stage.DONE;

            try
            {
                await ScannAsync();

                await DownloadAsync();

                await PutSemaphoreAsync();

                await InstallAsync();
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
            if (Checker != null)
            {
                _state.Stage = Stage.SCANNING;
                var newInstallerUri = await Checker.CheckLatestVersionPathAsync(InstallerUri);
                AssignNewUriIfValid(ref InstallerUri, newInstallerUri);
            }
        }

        protected virtual async Task DownloadAsync()
        {
            _state.Stage = Stage.DOWNLOADING;
            await Downloader.DownloadAsync(InstallerUri, InstallerTempPath, _state);
        }

        protected virtual Task PutSemaphoreAsync()
        {
            _state.Stage = Stage.WAITING;
            return _installationSemaphore.WaitAsync();
        }

        protected virtual Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;
            return Installer.InstallAsync(InstallerTempPath, InstallationArguments);
        }

        protected virtual void CleanUp(Stage finalStage)
        {
            _state.Stage = Stage.CLEANING;
            _installationSemaphore.Release();
            Cleaner.DeleteTempFiles(InstallerTempPath);

            _state.Stage = finalStage;
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