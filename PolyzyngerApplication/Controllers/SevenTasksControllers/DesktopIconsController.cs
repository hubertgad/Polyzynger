using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class DesktopIconsController : SevenController
    {
        protected IExecutor Executor;
        
        public DesktopIconsController(EventHandler<State> handler)
            : base(handler)
        {
            Executor = new ExecutorPS();
        }

        internal override async Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;

            var script = await GetResourceAsync("AdjustDesktopIcons.ps1");

            await Executor.ExecuteAsync(script);

            DeleteEdgeShotcut();

            _state.Stage = Stage.DONE;
        }

        private static void DeleteEdgeShotcut()
        {
            Delete(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory));

            Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            static void Delete(string folder)
            {
                string path = Path.Combine(folder, "Microsoft Edge.lnk");
                
                if (File.Exists(path)) File.Delete(path);
            }
        }
    }
}