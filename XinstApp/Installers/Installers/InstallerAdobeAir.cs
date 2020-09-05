using System.Net;
using System.Text.RegularExpressions;

namespace Polyzynger.Installers
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
            this.RemotePath = "https://airdownload.adobe.com/air/win/download/32.0/AdobeAIRInstaller.exe";
            this.FileName = "AdobeAIRInstaller.exe";
            this.Arguments = "-silent -pingbackAllowed";
            this.Controls.CheckBox.Content = "Adobe Air";
        }

        protected override string EstablishLastestVersionPath()
        {
            using (WebClient client = new WebClient())
            {
                string page = client.DownloadString("https://get.adobe.com/air/");
                string version = Regex.Match(page, "Version [0-9]+[.][0-9]+", RegexOptions.IgnoreCase).Value;
                string versionNo = Regex.Match(version, "[0-9]+[.][0-9]+").Value;
                this.NewRemotePath = this.RemotePath.Replace("32.0", versionNo);

                return base.EstablishLastestVersionPath();
            }
        }
    }
}
