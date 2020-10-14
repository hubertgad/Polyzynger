using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Installers;
using System;

namespace PolyzyngerApplication.Controllers
{
    class KLiteController : Controller
    {
        internal KLiteController(EventHandler<State> handler)
            : base(handler)
        {
            InstallerUri = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";

            InstallationArguments = "/verysilent";

            Checker = new KLiteChecker();

            Installer = new InstallerExe();
        }
    }
}