using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using PolyzyngerApplication.Updaters;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class AdobeReaderController : InstallationController
    {
        protected IUpdater Updater;

        /// <summary>
        /// Path from which patch file is downloaded.
        /// </summary>
        protected string PatchUri;

        /// <summary>
        /// Patch file name.
        /// </summary>
        protected string PatchFileName => PatchUri.Split('/').LastOrDefault();

        /// <summary>
        /// Full temporary path to a patch file.
        /// </summary>
        protected string PatchTempPath => Path.Combine(Path.GetTempPath(), PatchFileName);

        internal AdobeReaderController(EventHandler<State> handler)
            : base(handler, new Executor(), new AdobeReaderChecker())
        {
            InstallerUri = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";

            InstallationArguments = "/sAll";

            PatchUri = "ftp://ftp.adobe.com/pub/adobe/reader/win/AcrobatDC/";

            Updater = new AdobeReaderUpdater();
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

                await UpdateAsync();
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

        protected override async Task ScannAsync()
        {
            _state.Stage = Stage.SCANNING;
            PatchUri = await Checker.CheckLatestVersionPathAsync(PatchUri);
        }

        protected override async Task DownloadAsync()
        {
            _state.Stage = Stage.DOWNLOADING;
            await Downloader.DownloadAsync(InstallerUri, InstallerTempPath, _state, 2);
            await Downloader.DownloadAsync(PatchUri, PatchTempPath, _state, 2);
        }

        protected override void CleanUp(Stage finalStage)
        {
            _state.Stage = Stage.CLEANING;
            _installationSemaphore.Release();
            DeleteTempFile(InstallerTempPath);
            DeleteTempFile(PatchTempPath);

            _state.Stage = finalStage;
        }

        private Task UpdateAsync()
        {
            _state.Stage = Stage.UPDATING;
            return Updater.UdateAsync(PatchTempPath);
        }
    }
}