using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Installers;
using PolyzyngerApplication.Updaters;
using System;

namespace PolyzyngerApplication.Controllers
{
    internal class AdobeReaderController : Controller
    {
        public AdobeReaderController(EventHandler<State> handler) 
            : base(handler)
        {
            InstallerUri = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";

            Arguments = "/sAll";
            
            Checker = new AdobeReaderChecker();
            
            Installer = new InstallerExe();
            
            Updater = new AdobeReaderUpdater();
        }
    }
}