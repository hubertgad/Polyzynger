using System.IO;
using System.Threading.Tasks;

namespace XinstApp.Installers.SevenAds
{
    class Themepack : SevenBase
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
        public override Task Perform()
        {
            return Task.Run(() =>
                {
                string path = Path.Combine(Path.GetTempPath(), "Seven.deskthemepack");
                CopyResource("Seven.deskthemepack", path);
                ExecuteScript($"start { path }");
            });
        }
    }
}