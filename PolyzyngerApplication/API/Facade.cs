using PolyzyngerApplication.Controllers;
using PolyzyngerApplication.Controllers.SevenTasksControllers;
using PolyzyngerApplication.InstallationControllers.Controllers;
using PolyzyngerApplication.Utilities;
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
            await InstallAsync(new KLiteController(stateHandler));
        }

        public async Task InstallGoogleChromeAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new GoogleChromeController(stateHandler));
        }

        public async Task InstallAdobeReaderAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new AdobeReaderController(stateHandler));
        }

        public async Task InstallLibreOfficeAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new LibreOfficeController(stateHandler));
        }

        public async Task Install7ZipAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new SevenZipController(stateHandler));
        }

        public async Task InstallTeamViewerAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new TeamViewerController(stateHandler));
        }

        public async Task InstallJavaAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new Java8Controller(stateHandler));
        }

        public async Task InstallAdobeAirAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new AdobeAirController(stateHandler));
        }

        public async Task InstallESETNOD32Async(EventHandler<State> stateHandler)
        {
            await InstallAsync(new EsetNod32Controller(stateHandler));
        }

        public async Task InstallESETISAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new EsetISController(stateHandler));
        }

        public async Task InstallESETSSPAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new EsetSSPController(stateHandler));
        }

        public async Task InstallEndpointAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new EsetEEAController(stateHandler));
        }

        public async Task CopySevenIconAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new SevenIconController(stateHandler));
        }

        public async Task ApplySevenThemeAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new SevenThemeController(stateHandler));
        }

        public async Task HideSearchBarAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new HideSearchBarController(stateHandler));
        }

        public async Task ArrangeDesktopItemsAsync(EventHandler<State> stateHandler)
        {
            await InstallAsync(new DesktopIconsController(stateHandler));
        }

        public async Task ConnectToWiFi(EventHandler<State> stateHandler, string ssid, string password)
        {
            await InstallAsync(new WiFiController(stateHandler, ssid, password));
        }

        public bool IsConnectedToInternet()
        {
            return ConnectionChecker.IsConnectedToInternet();
        }

        private async Task InstallAsync(Controller controller)
        {
            await controller.InstallAsync();
        }
    }
}