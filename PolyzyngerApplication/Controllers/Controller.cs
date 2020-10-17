using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class Controller
    {
        protected readonly State _state;

        protected Controller(EventHandler<State> handler)
        {
            _state = new State(handler);
        }

        internal abstract Task InstallAsync();
    }
}