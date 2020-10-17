using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class WiFiController : SevenController
    {
        private readonly string _ssid;
        
        private readonly string _password;

        protected IExecutor Executor;

        public WiFiController(EventHandler<State> handler, string ssid, string password)
            : base(handler)
        {
            _ssid = ssid;

            _password = password;

            Executor = new Executor();
        }

        internal override async Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;

            string tempPath = Path.Combine(Path.GetTempPath(), "profile.xml");

            var template = await GetResourceAsync("WiFiProfileTemplate.xml");

            string profile = Regex.Replace(template, "{SSID}", _ssid).Replace("{password}", _password);

            await SaveResourceAsFile(profile, tempPath);

            await Executor.ExecuteAsync("netsh", $"wlan add profile filename=\"{tempPath}\"");

            File.Delete(tempPath);

            _state.Stage = Stage.DONE;
        }
    }
}