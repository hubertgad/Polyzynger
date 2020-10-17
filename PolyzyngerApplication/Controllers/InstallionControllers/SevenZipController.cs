using PolyzyngerApplication.Scanners;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class SevenZipController : InstallationController
    {
        internal SevenZipController(EventHandler<State> handler)
            : base(handler, new ExecutorMsi(), new SevenZipScanner())
        {
            InstallerUri = "https://www.7-zip.org/a/7z1900-x64.msi";
        }
    }
}