using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class HideSearchBarController : SevenController
    {
        protected IExecutor Executor;

        public HideSearchBarController(EventHandler<State> handler)
            : base(handler) 
        {
            Executor = new ExecutorPS();
        }

        internal override async Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;

            var script = await GetResourceAsync("HideSearchBar.ps1");

            await Executor.ExecuteAsync(script);

            _state.Stage = Stage.DONE;
        }
    }
}