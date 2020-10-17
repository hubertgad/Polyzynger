using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
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