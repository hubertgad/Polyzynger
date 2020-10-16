using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class AdobeAirController : InstallationController
    {
        internal AdobeAirController(EventHandler<State> handler)
            : base(handler, new ExecutorExe(), new AdobeAirChecker())
        {
            InstallerUri = "https://airdownload.adobe.com/air/win/download/32.0/AdobeAIRInstaller.exe";

            InstallationArguments = "-silent -pingbackAllowed";
        }
    }
}