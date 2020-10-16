using PolyzyngerApplication.Checkers;
using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class KLiteController : InstallationController
    {
        internal KLiteController(EventHandler<State> handler)
            : base(handler, new ExecutorExe(), new KLiteChecker())
        {
            InstallerUri = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";

            InstallationArguments = "/verysilent";
        }
    }
}