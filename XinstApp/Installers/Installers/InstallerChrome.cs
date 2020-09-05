namespace Polyzynger.Installers
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
            this.RemotePath = "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B7A3ACD62-E5A4-2144-FC0C-A17C986F1B00%7D%26lang%3Dpl%26browser%3D4%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe";
            this.FileName = "ChromeSetup.exe";
            this.Arguments = "/silent /install";
            this.Controls.CheckBox.Content = "Google Chrome";
        }
    }
}