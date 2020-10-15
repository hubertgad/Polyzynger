using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class EsetSSPController : Controller
    {
        internal EsetSSPController(EventHandler<State> handler)
            : base(handler, new ExecutorExe())
        {
            InstallerUri = "https://download.eset.com/com/eset/tools/installers/live_essp/latest/eset_smart_security_premium_live_installer.exe";

            InstallationArguments = "--silent --accepteula --avr-disable";
        }
    }
}