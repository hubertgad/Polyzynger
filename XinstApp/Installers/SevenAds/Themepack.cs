using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Polyzynger.Installers.SevenAds
{
    class Themepack : Script
    {

        private static Themepack _instance = null;

        public static Themepack Instance
        {
            get
            {
                if (_instance == null) { _instance = new Themepack(); }
                return _instance;
            }
        }

        private Themepack()
        {
            this.Controls.CheckBox.Content = "Seven Theme";
        }
        public override async Task Perform()
        {
            await Task.Run(() =>
                {
                    string path = Path.Combine(Path.GetTempPath(), "Seven.deskthemepack");
                    CopyResource("Seven.deskthemepack", path);

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
                });
        }
    }
}