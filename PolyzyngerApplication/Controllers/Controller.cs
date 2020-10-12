using PolyzyngerApplication.Cleaners;
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
    internal abstract class Controller
    {
        private readonly State _state;

        private readonly static SemaphoreSlim _installationSemaphore = new SemaphoreSlim(1);

        protected IChecker Checker;

        protected IDownloader Downloader;

        protected IInstaller Installer;

        protected IUpdater Updater;

        protected ICleaner Cleaner;
       
        /// <summary>
        /// Path from which installer file is downloaded.
        /// </summary>
        protected string InstallerUri;

        /// <summary>
        /// Installer file name.
        /// </summary>
        protected string FileName => InstallerUri.Split('/').LastOrDefault();

        /// <summary>
        /// Path from which patch file is downloaded.
        /// </summary>
        protected string PatchUri = string.Empty;

        protected string PatchFileName => "patch.msp";
        
        /// <summary>
        /// Installation arguments to be passed to installation process.
        /// </summary>
        protected string Arguments = " /qn";

        /// <summary>
        /// Full temporary path to an installer file.
        /// </summary>
        protected string InstallerTempPath => Path.Combine(Path.GetTempPath(), FileName);

        /// <summary>
        /// Full temporary path to a patch file.
        /// </summary>
        protected string PatchTempPath => Path.Combine(Path.GetTempPath(), PatchFileName); 

        protected Controller(EventHandler<State> handler)
        {
            _state = new State(handler);

            Downloader = new DefaultDownloader();
            
            Cleaner = new DefaultCleaner();
        }

        internal async Task ExecuteInstallationAsync()
        {
            Stage finalStage = Stage.DONE;

            try
            {
                if (Checker != null)
                {
                    _state.Stage = Stage.SCANNING;
                    var (installerUri, patchUri) = await Checker.CheckLatestVersionPathAsync(InstallerUri);

                    AssigNewPathsIfValid(installerUri, patchUri);
                }

                _state.Stage = Stage.DOWNLOADING;
                await Downloader.DownloadAsync(InstallerUri, InstallerTempPath, _state, PatchUri, PatchTempPath);

                _state.Stage = Stage.WAITING;
                await _installationSemaphore.WaitAsync();

                _state.Stage = Stage.INSTALLING;
                await Installer.InstallAsync(InstallerTempPath, Arguments);

                if (Updater != null)
                {
                    _state.Stage = Stage.UPDATING;
                    await Updater.UdateAsync(PatchTempPath);
                }

                _state.Stage = Stage.CLEANING;
            }
            catch
            {
                finalStage = Stage.ERROR;
            }
            finally
            {
                _installationSemaphore.Release();

                Cleaner.DeleteTempFilesAsync(InstallerTempPath);
                Cleaner.DeleteTempFilesAsync(PatchTempPath);

                _state.Stage = finalStage;
            }
        }

        private bool IsUrlValid(string url)
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

        private void AssigNewPathsIfValid(string installerUri, string patchUri)
        {
            if (IsUrlValid(installerUri))
            {
                InstallerUri = installerUri;
            }

            if (IsUrlValid(patchUri))
            {
                PatchUri = patchUri;
            }
        }
    }
}