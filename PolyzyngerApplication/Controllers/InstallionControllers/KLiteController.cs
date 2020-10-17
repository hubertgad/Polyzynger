using PolyzyngerApplication.Scanners;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.Controllers.InstallationControllers
{
    internal class KLiteController : InstallationController
    {
        internal KLiteController(EventHandler<State> handler)
            : base(handler, new Executor(), new KLiteScanner())
        {
            InstallerUri = "https://files3.codecguide.com/K-Lite_Codec_Pack_1548_Standard.exe";

            InstallationArguments = "/verysilent";
        }
    }
}