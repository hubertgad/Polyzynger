using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class SevenZipController : Controller
    {
        internal SevenZipController(EventHandler<State> handler)
            : base(handler, new ExecutorMsi(), new SevenZipChecker())
        {
            InstallerUri = "https://www.7-zip.org/a/7z1900-x64.msi";
        }
    }
}