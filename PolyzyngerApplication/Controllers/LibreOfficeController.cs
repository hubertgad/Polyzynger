using System;

namespace PolyzyngerApplication.Controllers
{
    internal class LibreOfficeController : Controller
    {
        public LibreOfficeController(EventHandler<State> handler) 
            : base(handler)
        {
            InstallerUri = "https://download.documentfoundation.org/libreoffice/stable/6.3.6/win/x86_64/LibreOffice_6.3.6_Win_x64.msi";
        }
    }
}