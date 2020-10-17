using PolyzyngerApplication.Scanners;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class LibreOfficeController : InstallationController
    {
        internal LibreOfficeController(EventHandler<State> handler) 
            : base(handler, new ExecutorMsi(), new LibreOfficeScanner())
        {
            InstallerUri = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi";
        }
    }
}