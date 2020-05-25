using System.IO;

namespace XinstApp.Installers
{
    class InstallerChrome : Installer
    {
        private static InstallerChrome _instance = null;

        public static InstallerChrome Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerChrome(); }
                return _instance;
            }
        }

        private InstallerChrome() : base()
        {
            this.remotePath = "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B52A3FC67-4810-5F27-E158-3C117C8730BE%7D%26lang%3Den%26browser%3D3%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/chrome/install/ChromeStandaloneSetup64.exe";
            this.fileName = "ChromeStandaloneSetup64.exe";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.arguments = "/silent /install";
            this.Controls.CheckBox.Content = "Google Chrome";
        }
        //TODO: Installer Chrome
    }
}
