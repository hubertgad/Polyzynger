using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class Controller
    {
        protected readonly State State;

        protected Controller(EventHandler<State> handler)
        {
            State = new State(handler);
        }

        internal abstract Task InstallAsync();
    }
}