using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class GoogleChromeController : InstallationController
    {
        internal GoogleChromeController(EventHandler<State> handler)
            : base(handler, new ExecutorExe())
        {
            InstallerUri = "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B7A3ACD62-E5A4-2144-FC0C-A17C986F1B00%7D%26lang%3Dpl%26browser%3D4%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe";
            
            InstallationArguments = "/silent /install";
        }
    }
}