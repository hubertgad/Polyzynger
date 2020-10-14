using PolyzyngerApplication.Controllers;
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
            var kLiteController = new KLiteController(stateHandler);
            await kLiteController.ExecuteInstallationStepsAsync();
        }

        public async Task InstallGoogleChromeAsync(EventHandler<State> stateHandler)
        {
            var googleChromeController = new GoogleChromeController(stateHandler);
            await googleChromeController.ExecuteInstallationStepsAsync();
        }

        public async Task InstallAdobeReaderAsync(EventHandler<State> stateHandler)
        {
            var adobeReaderController = new AdobeReaderController(stateHandler);
            await adobeReaderController.ExecuteInstallationStepsAsync();
        }

        public async Task InstallLibreOfficeAsync(EventHandler<State> stateHandler)
        {
            var libreOfficeController = new LibreOfficeController(stateHandler);
            await libreOfficeController.ExecuteInstallationStepsAsync();
        }

        public async Task Install7ZipAsync(EventHandler<State> stateHandler)
        {
            var sevenZipController = new SevenZipController(stateHandler);
            await sevenZipController.ExecuteInstallationStepsAsync();
        }

        public async Task InstallTeamViewerAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallJavaAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallAdobeAirAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallESETNOD32Async(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallESETISAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallESETSSPAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
        }

        public async Task InstallEndPointAsync(EventHandler<State> stateHandler)
        {
            throw new NotImplementedException();
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
    }
}