using PolyzyngerApplication.Interfaces;
using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class Controller
    {
        protected readonly State State;

        protected IExecutor Executor;

        protected Controller(EventHandler<State> handler, IExecutor executor = null)
        {
            State = new State(handler);

            Executor = executor;
        }

        internal abstract Task InstallAsync();
    }
}