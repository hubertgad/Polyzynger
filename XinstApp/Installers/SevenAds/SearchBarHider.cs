using System.Threading.Tasks;

namespace Polyzynger.Installers.SevenAds
{
    class SearchBarHider : Script
    {
        private static SearchBarHider _instance = null;

        public static SearchBarHider Instance
        {
            get
            {
                if (_instance == null) { _instance = new SearchBarHider(); }
                return _instance;
            }
        }

        private SearchBarHider()
        {
            this.Controls.CheckBox.Content = "Hide Search Bar";
        }
        public override Task Perform()
        {
            return Task.Run(() => ExecuteScript("Set-ItemProperty -Path \"HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Search\" -Name \"SearchboxTaskbarMode\" -Type DWord -Value 0"));
        }
    }
}