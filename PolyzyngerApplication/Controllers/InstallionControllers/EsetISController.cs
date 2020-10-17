using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class EsetISController : InstallationController
    {
        internal EsetISController(EventHandler<State> handler)
            : base(handler, new Executor())
        {
            InstallerUri = "https://download.eset.com/com/eset/tools/installers/live_eis/latest/eset_internet_security_live_installer.exe";

            InstallationArguments = "--silent --accepteula --avr-disable";
        }
    }
}