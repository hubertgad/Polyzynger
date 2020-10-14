using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Installers;
using System;

namespace PolyzyngerApplication.Controllers
{
    internal class SevenZipController : Controller
    {
        internal SevenZipController(EventHandler<State> handler)
            : base(handler)
        {
            InstallerUri = "https://www.7-zip.org/a/7z1900-x64.msi";

            InstallationArguments = "/qn";

            Installer = new InstallerMsi();

            Checker = new SevenZipChecker();
        }
    }
}