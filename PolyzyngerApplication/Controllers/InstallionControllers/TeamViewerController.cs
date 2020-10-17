using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class TeamViewerController : InstallationController
    {
        internal TeamViewerController(EventHandler<State> handler)
            : base(handler, new Executor())
        {
            InstallerUri = "https://download.teamviewer.com/download/TeamViewer_Setup.exe";

            InstallationArguments = "/S /norestart";
        }
    }
}