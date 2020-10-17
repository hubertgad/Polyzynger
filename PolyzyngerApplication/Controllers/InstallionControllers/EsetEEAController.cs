using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class EsetEEAController : InstallationController
    {
        internal EsetEEAController(EventHandler<State> handler)
            : base(handler, new ExecutorMsi())
        {
            InstallerUri = "https://download.eset.com/com/eset/apps/business/eea/windows/latest/eea_nt64.msi";
        }
    }
}