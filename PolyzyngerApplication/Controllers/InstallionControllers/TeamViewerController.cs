using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class TeamViewerController : Controller
    {
        internal TeamViewerController(EventHandler<State> handler)
            : base(handler, new ExecutorExe())
        {
            InstallerUri = "https://download.teamviewer.com/download/TeamViewer_Setup.exe";

            InstallationArguments = "/S /norestart";
        }
    }
}