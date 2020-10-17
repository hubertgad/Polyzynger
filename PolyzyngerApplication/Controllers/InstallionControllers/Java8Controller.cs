using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Executors;
using System;

namespace PolyzyngerApplication.InstallationControllers.Controllers
{
    internal class Java8Controller : InstallationController
    {
        internal Java8Controller(EventHandler<State> handler)
            : base(handler, new Executor())
        {
            InstallerUri = "https://javadl.oracle.com/webapps/download/AutoDL?BundleId=242990_a4634525489241b9a9e1aa73d9e118e6";

            InstallerFileName = "java.exe";

            InstallationArguments = "/s";
        }
    }
}