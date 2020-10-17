using PolyzyngerApplication.Scanners;
using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Resources;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class AdobeReaderController : InstallationController
    {
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
            : base(handler, new Executor(), new AdobeReaderScanner())
        {
            InstallerUri = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";

            InstallationArguments = "/sAll";

            PatchUri = "ftp://ftp.adobe.com/pub/adobe/reader/win/AcrobatDC/";
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
            State.Stage = Stage.SCANNING;
            PatchUri = await Scanner.CheckLatestVersionPathAsync(PatchUri);
        }

        protected override async Task DownloadAsync()
        {
            State.Stage = Stage.DOWNLOADING;
            await Downloader.DownloadAsync(InstallerUri, InstallerTempPath, State, 2);
            await Downloader.DownloadAsync(PatchUri, PatchTempPath, State, 2);
        }

        protected override void CleanUp(Stage finalStage)
        {
            State.Stage = Stage.CLEANING;
            _installationSemaphore.Release();
            DeleteTempFile(InstallerTempPath);
            DeleteTempFile(PatchTempPath);

            State.Stage = finalStage;
        }

        private async Task UpdateAsync()
        {
            State.Stage = Stage.UPDATING;
            string tempPath = Path.Combine(Path.GetTempPath(), "temp.cmd");
            await ResourcesManager.SaveStringAsFile($"START /WAIT msiexec /update \"{ PatchTempPath }\" /qn", tempPath);
            await Executor.ExecuteAsync(tempPath);
            DeleteTempFile(tempPath);
        }
    }
}