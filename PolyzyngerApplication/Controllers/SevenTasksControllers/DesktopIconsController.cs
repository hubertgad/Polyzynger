using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using PolyzyngerApplication.Resources;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class DesktopIconsController : Controller
    {
        public DesktopIconsController(EventHandler<State> handler)
            : base(handler)
        {
            Executor = new ExecutorPS();
        }

        internal override async Task InstallAsync()
        {
            State.Stage = Stage.INSTALLING;

            var script = await ResourcesManager.GetResourceAsync("AdjustDesktopIcons.ps1");

            await Executor.ExecuteAsync(script);

            DeleteEdgeShotcut();

            State.Stage = Stage.DONE;
        }

        private void DeleteEdgeShotcut()
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