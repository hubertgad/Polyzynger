using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class LibreOfficeController : Controller
    {
        internal LibreOfficeController(EventHandler<State> handler) 
            : base(handler, new ExecutorMsi(), new LibreOfficeChecker())
        {
            InstallerUri = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi";
        }
    }
}