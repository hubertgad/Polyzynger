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

        public async Task InstallGoogleChromeAsync(EventHandler<State> stateHandler)
        {
            var googleChromeController = new GoogleChromeController(stateHandler);
            await googleChromeController.ExecuteInstallationAsync();
        }

        public async Task InstallAdobeReaderAsync(EventHandler<State> stateHandler)
        {
            var adobeReaderController = new AdobeReaderController(stateHandler);
            await adobeReaderController.ExecuteInstallationAsync();
        }

        public async Task InstallLibreOfficeAsync(EventHandler<State> stateHandler)
        {
            var libreOfficeController = new LibreOfficeController(stateHandler);
            await libreOfficeController.ExecuteInstallationAsync();
        }

        public async Task Install7ZipAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallTeamViewerAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallJavaAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallAdobeAirAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallKLiteCodecsAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallESETNOD32Async(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallESETISAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallESETSSPAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task InstallEndPointAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task CopySevenIconAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task ApplySevenThemeAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task HideSearchBarAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }

        public async Task ArrangeDesktopItemsAsync(EventHandler<State> stateHandler)
        {
            await Task.CompletedTask;
        }
    }
}