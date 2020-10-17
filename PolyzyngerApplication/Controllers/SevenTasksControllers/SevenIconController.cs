using PolyzyngerApplication.Resources;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class SevenIconController : Controller
    {
        public SevenIconController(EventHandler<State> handler)
            : base(handler) { }

        internal override async Task InstallAsync()
        {
            State.Stage = Stage.INSTALLING;

            await CopyIconAsync();

            await CopyLinkAsync();

            State.Stage = Stage.DONE;
        }

        private Task CopyIconAsync()
        {
            string sevenDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(), "Seven");

            Directory.CreateDirectory(sevenDir);

            string iconDestination = Path.Combine(sevenDir, "Seven.ico");

            return ResourcesManager.CopyResourceAsync("Seven.ico", iconDestination);
        }

        private async Task CopyLinkAsync()
        {
            string linkDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), "Prosimy o opinię!.url");

            string linkText = await ResourcesManager.GetResourceAsync("Prosimy o opinie!.url");

            linkText = linkText.Replace("USERNAME", Environment.UserName.ToString());

            await ResourcesManager.SaveStringAsFile(linkText, linkDestination);
        }
    }
}