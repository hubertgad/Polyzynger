using System.IO;
using System.Net;
using System.Text.RegularExpressions;

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
            this.remotePath = "https://airdownload.adobe.com/air/win/download/32.0/AdobeAIRInstaller.exe";
            this.fileName = "AdobeAIRInstaller.exe";
            this.arguments = "-silent -pingbackAllowed";
            this.tempPath = Path.Combine(Path.GetTempPath(), this.fileName);
            this.Controls.CheckBox.Content = "Adobe Air";
        }

        protected override string EstablishLastestVersionPath()
        {
            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://get.adobe.com/air/");
                string version = Regex.Match(page, "Version [0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;
                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
                this.newRemotePath = this.remotePath.Replace("32.0", versionNo);

                return base.EstablishLastestVersionPath();
            }
        }
    }
}
