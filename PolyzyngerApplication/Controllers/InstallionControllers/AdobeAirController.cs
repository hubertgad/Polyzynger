using PolyzyngerApplication.Scanners;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class AdobeAirController : InstallationController
    {
        internal AdobeAirController(EventHandler<State> handler)
            : base(handler, new Executor(), new AdobeAirScanner())
        {
            InstallerUri = "https://airdownload.adobe.com/air/win/download/32.0/AdobeAIRInstaller.exe";

            InstallationArguments = "-silent -pingbackAllowed";
        }
    }
}