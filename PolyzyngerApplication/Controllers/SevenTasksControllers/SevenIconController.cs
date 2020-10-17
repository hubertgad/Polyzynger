using System;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class SevenIconController : SevenController
    {
        public SevenIconController(EventHandler<State> handler)
            : base(handler) { }

        internal override async Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;

            await CopyIconAsync();

            await CopyLinkAsync();

            _state.Stage = Stage.DONE;
        }

        private Task CopyIconAsync()
        {
            string sevenDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(), "Seven");

            Directory.CreateDirectory(sevenDir);

            string iconDestination = Path.Combine(sevenDir, "Seven.ico");

            return CopyResourceAsync("Seven.ico", iconDestination);
        }

        private async Task CopyLinkAsync()
        {
            string linkDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), "Prosimy o opinię!.url");

            string linkText = await GetResourceAsync("Prosimy o opinie!.url");

            linkText = linkText.Replace("USERNAME", Environment.UserName.ToString());

            await SaveResourceAsFile(linkText, linkDestination);
        }
    }
}