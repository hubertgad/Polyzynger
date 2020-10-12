using PolyzyngerApplication.Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal class AdobeReaderController : Controller
    {
        public AdobeReaderController(EventHandler<State> handler) : base(handler)
        {
            remotePath = "http://ardownload.adobe.com/pub/adobe/reader/win/AcrobatDC/1900820071/AcroRdrDC1900820071_pl_PL.exe";
            fileName = "AcroRdrDC1900820071_pl_PL.exe";
            arguments = "/sAll";
            checker = new AdobeReaderChecker();
        }
    }
}