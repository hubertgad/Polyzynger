using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.InstallationControllers.Controllers;
using System;
using System.Threading.Tasks;

namespace PolyzyngerApplication.API
{
    public class Facade
    {
        private static Facade _instance;

        public static Facade Instance
        {
            get { return _instance ??= new Facade(); }
        }

        private Facade() { }

        public async Task InstallKLiteCodecsAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new KLiteController(stateHandler));
        }

        public async Task InstallGoogleChromeAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new GoogleChromeController(stateHandler));
        }

        public async Task InstallAdobeReaderAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new AdobeReaderController(stateHandler));
        }

        public async Task InstallLibreOfficeAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new LibreOfficeController(stateHandler));
        }

        public async Task Install7ZipAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new SevenZipController(stateHandler));
        }

        public async Task InstallTeamViewerAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new TeamViewerController(stateHandler));
        }

        public async Task InstallJavaAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new Java8Controller(stateHandler));
        }

        public async Task InstallAdobeAirAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new AdobeAirController(stateHandler));
        }

        public async Task InstallESETNOD32Async(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new EsetNod32Controller(stateHandler));
        }

        public async Task InstallESETISAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new EsetISController(stateHandler));
        }

        public async Task InstallESETSSPAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new EsetSSPController(stateHandler));
        }

        public async Task InstallEndpointAsync(EventHandler<State> stateHandler)
        {
            await ExecuteInstallationStepsAsync(new EsetEEAController(stateHandler));
        }

        public async Task CopySevenIconAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task ApplySevenThemeAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task HideSearchBarAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task ArrangeDesktopItemsAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task ConnectToWiFi(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        private async Task ExecuteInstallationStepsAsync(Controller controller)
        {
            await controller.InstallAsync();
        }
    }
}