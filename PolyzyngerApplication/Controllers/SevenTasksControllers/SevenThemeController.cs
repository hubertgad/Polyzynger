using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    //#TODO: Ta klasa zawiera metodę InstallAsync(), która jest kiepsko napisana.
    // Na chwilę obecną nie mam pomysłu, jak zrobić to lepiej.
    internal class SevenThemeController : SevenController
    {
        public SevenThemeController(EventHandler<State> handler)
            : base(handler) { }

        internal override async Task InstallAsync()
        {
            _state.Stage = Stage.INSTALLING;

            string path = Path.Combine(Path.GetTempPath(), "Seven.deskthemepack");
            await CopyResourceAsync("Seven.deskthemepack", path);

            Process p = new Process
            {
                StartInfo =
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C start /wait { path }",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                    }
            };
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                Process p2 = new Process
                {
                    StartInfo =
                        {
                            FileName = "taskkill",
                            Arguments = "/F /IM systemsettings.exe",
                            Verb = "runas",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                };
                p2.Start();
            };
            p.Start();

            _state.Stage = Stage.DONE;
        }
    }
}