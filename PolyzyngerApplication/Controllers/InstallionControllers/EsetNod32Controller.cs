using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class EsetNod32Controller : InstallationController
    {
        internal EsetNod32Controller(EventHandler<State> handler)
            : base(handler, new Executor())
        {
            InstallerUri = "https://download.eset.com/com/eset/tools/installers/live_eav/latest/eset_nod32_antivirus_live_installer.exe";

            InstallationArguments = "--silent --accepteula --avr-disable";
        }
    }
}