using PolyzyngerApplication.API;
using PolyzyngerApplication.Cleaners;
using PolyzyngerApplication.Downloaders;
using PolyzyngerApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class Controller
    {
        private readonly State _state;

        protected IChecker checker;

        private IDownloader downloader;

        private IInstaller installer;

        private IUpdater updater;

        private ICleaner cleaner;
       
        /// <summary>
        /// Path from which file is downloaded.
        /// </summary>
        protected string remotePath;

        /// <summary>
        /// Full temporary path to an installer file.
        /// </summary>

        /// <summary>
        /// Installer file name.
        /// </summary>
        protected string fileName;

        protected string patchRemotePath;

        protected string PatchFileName => "patch.msp";
        
        /// <summary>
        /// Installation arguments to be passed to installation process.
        /// </summary>
        protected string arguments = " /qn";

        protected string TempPath => Path.Combine(Path.GetTempPath(), fileName);

        protected string PatchTempPath => Path.Combine(Path.GetTempPath(), PatchFileName); 

        protected Controller(EventHandler<State> handler)
        {
            _state = new State(handler);
            downloader = new DefaultDownloader();
            cleaner = new DefaultCleaner();
        }

        internal async Task ExecuteInstallationAsync()
        {
            try
            {
                if (checker != null)
                {
                    _state.Stage = Stage.Checking;

                    var newRemotePaths = await checker.CheckLatestVersionPathAsync();

                    if (IsUrlValid(newRemotePaths.installer))
                    {
                        remotePath = newRemotePaths.installer;
                    }

                    if (IsUrlValid(newRemotePaths.patch))
                    {
                        patchRemotePath = newRemotePaths.patch;
                    }
                }

                _state.Stage = Stage.Downloading;

                await downloader.DownloadAsync(remotePath, TempPath, _state, patchRemotePath, PatchTempPath);
                
                //Task d2 = Task.CompletedTask;
                
                //if (!string.IsNullOrEmpty(patchRemotePath))
                //{
                //    d2 = downloader.DownloadAsync(patchRemotePath, PatchTempPath, _state);
                //}

                //await Task.WhenAll(d1, d2);

                _state.Stage = Stage.Waiting;

                for (int i = 0; i <= 2; i++)
                {
                    await Task.Delay(1000);
                }

                _state.Stage = Stage.Installing;

                for (int i = 0; i <= 2; i++)
                {
                    await Task.Delay(1000);
                }

                _state.Stage = Stage.Done;
            }
            catch
            {
                _state.Stage = Stage.Error;
            }
            finally
            {
                cleaner.DeleteTempFilesAsync(TempPath);
                cleaner.DeleteTempFilesAsync(PatchTempPath);
            }
        }

        private bool IsUrlValid(string url)
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