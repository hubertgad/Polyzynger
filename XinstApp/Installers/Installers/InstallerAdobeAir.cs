using System.IO;

namespace XinstApp.Installers
{
    class InstallerAdobeAir : Installer
    {

        private static InstallerAdobeAir _instance;
        public static InstallerAdobeAir Instance
        {
            get
            {
                if (_instance == null) { _instance = new InstallerAdobeAir(); }
                return _instance;
            }
        }

        private InstallerAdobeAir()
        {
            this.remotePath = "https://airdownload.adobe.com/air/win/download/32.0/AdobeAIRInstaller.exe"; //TODO: Air Updater
            this.fileName = "AdobeAIRInstaller.exe";
            this.arguments = "-silent -pingbackAllowed";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "Adobe Air";
        }

        //TODO: Air Installer
    }
}