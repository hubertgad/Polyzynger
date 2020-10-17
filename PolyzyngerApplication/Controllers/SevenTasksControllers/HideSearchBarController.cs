using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using PolyzyngerApplication.Resources;
using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class HideSearchBarController : Controller
    {
        protected IExecutor Executor;

        public HideSearchBarController(EventHandler<State> handler)
            : base(handler) 
        {
            Executor = new ExecutorPS();
        }

        internal override async Task InstallAsync()
        {
            State.Stage = Stage.INSTALLING;

            var script = await ResourcesManager.GetResourceAsync("HideSearchBar.ps1");

            await Executor.ExecuteAsync(script);

            State.Stage = Stage.DONE;
        }
    }
}