using PolyzyngerApplication.Executors;
using PolyzyngerApplication.Interfaces;
using PolyzyngerApplication.Resources;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class WiFiProfileController : Controller
    {
        private readonly string _ssid;
        
        private readonly string _password;

        protected IExecutor Executor;

        public WiFiProfileController(EventHandler<State> handler, string ssid, string password)
            : base(handler)
        {
            _ssid = ssid;

            _password = password;

            Executor = new Executor();
        }

        internal override async Task InstallAsync()
        {
            State.Stage = Stage.INSTALLING;

            string tempPath = Path.Combine(Path.GetTempPath(), "profile.xml");

            var template = await ResourcesManager.GetResourceAsync("WiFiProfileTemplate.xml");

            string profile = Regex.Replace(template, "{SSID}", _ssid).Replace("{password}", _password);

            await ResourcesManager.SaveStringAsFile(profile, tempPath);

            await Executor.ExecuteAsync("netsh", $"wlan add profile filename=\"{tempPath}\"");

            File.Delete(tempPath);

            State.Stage = Stage.DONE;
        }
    }
}